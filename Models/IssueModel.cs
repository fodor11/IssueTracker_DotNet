using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{ 
    public enum IssueStatus
    {
        /// <summary>
        /// The issue is created, but the work did not started on it yet
        /// </summary>
        Created,
        /// <summary>
        /// The issue is being worked on
        /// </summary>
        InProgress,
        /// <summary>
        /// Work started on the issue, but it had to be suspended
        /// </summary>
        Suspended,
        /// <summary>
        /// The work is done
        /// </summary>
        Finished
    }
    public enum IssueDifficulty
    {
        Easy = 1,
        Normal,
        Hard
    }
    public enum IssuePriority
    {
        /// <summary>
        /// Not that important
        /// </summary>
        Low,
        /// <summary>
        /// Average importance
        /// </summary>
        Normal,
        /// <summary>
        /// Very important
        /// </summary>
        High
    }
    public class IssueModel
    {
        private int _id;
        private string _issueName;
        private string _description;
        private double _progressPercentage;
        private IssueStatus _status;
        private IssueDifficulty _difficulty;
        private IssuePriority _priority;
        private DateTime _creationDate;
        private DateTime _startingDate;
        private DateTime _finishingDate;
        private DateTime _deadline;
        private List<string> _addedFiles; //paths to the files uploded to this issue
        private List<UserModel> _addedUsers;
        private List<IssueModel> _subIssues;
        private IssueModel _parentIssue;

        public int Id { get { return _id; } }
        public string IssueName 
        { 
            get { return _issueName; } 
            set { _issueName = value; } 
        }
        public string Description 
        { 
            get { return _description; }
            set { _description = value; }
        }
        public IssueStatus Status 
        { 
            get { return _status; } 
        }
        /// <summary>
        /// How much is done of the issue (percentage). Can be set only if it doesn't have a child issue.
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown when the given value is out of [0.0; 1.0]</exception>
        public double Progress 
        {   get { return _progressPercentage; }
            set
            {
                if (_subIssues==null) 
                {
                    if (value < 0.0 || value > 1.0)
                    {
                        throw new ArgumentException("Progress value must be in the range of [0.0; 1.0]");
                    }
                    else
                    {
                        StartIssue();
                        _progressPercentage = value;
                        calcProgress(); // to update parent issues
                    }
                }
            }
        }
        /// <summary>
        /// How difficult the task is on a scale of [easy - normal - hard]. It acts as weight when calculating progress.
        /// (Not necessary for root issues)
        /// </summary>
        public IssueDifficulty Difficulty 
        { 
            get { return _difficulty; }
            set 
            { 
                _difficulty = value;
                calcProgress(); // the changed difficulty affects the progress of parent issues
            }
        }
        /// <summary>
        /// Priority of the issue
        /// </summary>
        public IssuePriority Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        /// <summary>
        /// The added users working on this issue. The first one is the creator/owner of the issue
        /// </summary>
        public List<UserModel> AddedUsers
        {
            get { return _addedUsers; }
        }
        /// <summary>
        /// The list of smaller issues that are part of this issue
        /// </summary>
        public List<IssueModel> SubIssues
        {
            get { return _subIssues; }
        }
        /// <summary>
        /// List of the paths to files that were uploaded to this issue
        /// </summary>
        public List<string> AddedFiles
        {
            get { return _addedFiles; }
        }
        /// <summary>
        /// When the issue was created
        /// </summary>
        public DateTime CreationDate
        {
            get { return _creationDate; }
        }
        /// <summary>
        /// When work on the issue actually started
        /// </summary>
        public DateTime StartingDate
        {
            get { return _startingDate; }
            set { _startingDate = value; }
        }
        /// <summary>
        /// The date the issue was finished
        /// </summary>
        public DateTime FinishingDate 
        { 
            get { return _finishingDate; }
        }
        /// <summary>
        /// The assigned deadline
        /// </summary>
        public DateTime DeadLine
        {
            get { return _deadline; }
            set { _deadline = value; }
        }
        /// <summary>
        /// Returns the parent issue
        /// </summary>
        public IssueModel ParentIssue 
        { 
            get { return _parentIssue; }
        }
        public IssueModel() { }
        /// <summary>
        /// Constructor of an issue
        /// </summary>
        /// <param name="name">Name of the issue</param>
        /// <param name="description">Description of the issue</param>
        /// <param name="owner">Owner of the issue</param>
        /// <param name="parentIssue">The root issue to whis this will be added (if this is not a root issue itself)</param>
        /// <param name="difficulty">Difficulty of the issue</param>
        /// <param name="status">Status of the issue</param>
        /// <param name="priority">Priority of the issue</param>
        public IssueModel(string name, string description, UserModel owner, IssueModel parentIssue = null,
                          IssueDifficulty difficulty = IssueDifficulty.Normal, IssueStatus status = IssueStatus.Created,
                          IssuePriority priority = IssuePriority.Normal)
        {
            // set values
            _issueName = name;
            _description = description;
            _difficulty = difficulty;
            _status = status;
            _priority = priority;
            _progressPercentage = 0.0;
            _creationDate = DateTime.Today;
            // add owner to the issue, add the issue to the owner
            _addedUsers = new List<UserModel>();
            _addedUsers.Add(owner);
            owner.AddIssue(this);
            // add parent issue
            if (parentIssue != null)
            {
                _parentIssue = parentIssue;
                parentIssue.AddSubIssue(this);
            }
        }       
        /// <summary>
        /// Adds a subissue to this issue
        /// </summary>
        /// <param name="issue">The issue</param>
        public void AddSubIssue(IssueModel issue)
        {
            if (_subIssues == null) _subIssues = new List<IssueModel>();
            _subIssues.Add(issue);
            calcProgress();
        }
        /// <summary>
        /// Sets the starting date (if this is the very first time) and sets the status to "In Progress". Does the same to parent issues.
        /// </summary>
        public void StartIssue()
        {
            if (_startingDate == default(DateTime))
            {
                _startingDate = DateTime.Today;
            }            
            _status = IssueStatus.InProgress;
            if (_parentIssue != null)
            {
                _parentIssue.StartIssue();
            }  
        }
        /// <summary>
        /// Sets the status to suspended. Does the same to sub issues.
        /// </summary>
        public void SuspendIssue()
        {
            _status = IssueStatus.Suspended;
            if (_subIssues!=null)
            {
                foreach (IssueModel item in _subIssues)
                {
                    item.SuspendIssue();
                }
            }
        }
        /// <summary>
        /// Sets the status to finished, progress to 100%, and sets the finishing date. Does the same to sub issues.
        /// </summary>
        public void FinishIssue()
        {            
            Progress = 1.0;
            _status = IssueStatus.Finished;
            if (_finishingDate == default(DateTime))
            {
                _finishingDate = DateTime.Today;
            }
            if (_subIssues != null)
            {
                foreach (IssueModel item in _subIssues)
                {
                    item.FinishIssue();
                }
            }
        }
        /// <summary>
        /// Add the given file to the issue's files
        /// </summary>
        /// <param name="path">Path to the file</param>
        public void AddFile(string path)
        {
            if (_addedFiles == null)
            {
                _addedFiles = new List<string>();
            }
            _addedFiles.Add(path);
        }
        /// <summary>
        /// Adds a user to this issue
        /// </summary>
        /// <param name="user">The user to be added to this issue</param>
        public void AddUser(UserModel user)
        {
            if (_addedUsers == null)
            {
                _addedUsers = new List<UserModel>();
            }            
            _addedUsers.Add(user);
            user.AddIssue(this);
        }
        /// <summary>
        /// Removes a user to this issue
        /// </summary>
        /// <param name="user">The user to be removed from this issue</param>
        public void RemoveUser(UserModel user)
        {
            if (_addedUsers != null) _addedUsers.Remove(user);
            user.RemoveIssueFromUser(this);
        }
        /// <summary>
        /// Deletes the issue, its sub issues, refreshes the progress of the parent issue
        /// </summary>
        /// <param name="root">Should always be left on TRUE. It means that this is the root issue that gets explicitly deleted.</param>
        public void DeleteIssue(bool root = true)
        {
            //delete added users, and this issue from them
            if (_addedUsers != null)
            {
                while (_addedUsers.Count > 0)
                {
                    _addedUsers[0].RemoveIssueFromUser(this);
                    _addedUsers.RemoveAt(0);
                }
            }
            //delete subissues
            if (_subIssues != null)
            {
                foreach (var item in _subIssues)
                {
                    item.DeleteIssue(false);
                }
            }
            //delete this issue from parent issue and update its progress
            if (root && _parentIssue != null)
            {
                _parentIssue.SubIssues.Remove(this);
                _parentIssue.calcProgress();
            }
            //TODO: delete uploaded files
            //TODO: delete this from DB
        }
        /// <summary>
        /// Averages the progress of sub issues weighted by their difficulty, and calls the same method for parent issues
        /// </summary>
        private void calcProgress()
        {
            if (_subIssues != null)
            {
                double sumPercentage = 0.0;
                int counter = 0;
                foreach (IssueModel child in _subIssues)
                {
                    sumPercentage += child.Progress * (int)child.Difficulty;
                    counter += (int)child.Difficulty;
                }
                _progressPercentage = sumPercentage / counter;
            }
            if (_parentIssue != null)
            {
                _parentIssue.calcProgress();
            }            
        }
        ////////////////////////////////// for testing ////////////////////////////////// 
        public void Print()
        {
            Console.WriteLine(_issueName);
            if (_subIssues != null)
            {
                foreach (IssueModel item in _subIssues)
                {
                    item.Print();
                }
            }
        }

    }
}