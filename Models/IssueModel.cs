using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{ 
    public enum IssueStatus
    {
        Created,
        InProgress,
        Finished
    }
    public enum IssueDifficulty
    {
        Easy = 1,
        Normal,
        Hard
    }
    public class IssueModel
    {
        private int _id;
        private string _issueName;
        private string _description;
        private IssueStatus _status;
        private double _progressPercenage;
        private IssueDifficulty _difficulty;
        private List<UserModel> _addedUsers;
        private List<IssueModel> _subIssues;
        private IssueModel _parentIssue;
        private DateTime _creationDate;
        private DateTime _startingDate;
        private DateTime _finishingDate;

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
        /// How much is done of the issue (percentage)
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown when the given value is out of [0.0; 1.0]</exception>
        public double Progress 
        {   get { return _progressPercenage; }
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
                        _progressPercenage = value;
                        calcProgress();
                    }
                }
            }
        }
        /// <summary>
        /// How difficult the task is on a scale of [easy - normal - hard]
        /// (Not necessary for root issues)
        /// </summary>
        public IssueDifficulty Difficulty 
        { 
            get { return _difficulty; }
            set { _difficulty = value; }
        }
        /// <summary>
        /// The added users working on this issue. The first one is the creator/owner of the issue
        /// </summary>
        public List<UserModel> AddedUsers
        {
            get { return _addedUsers; }
            set { _addedUsers = value; }
        }
        /// <summary>
        /// The list of smaller issues that are part of this issue
        /// </summary>
        public List<IssueModel> SubIssues
        {
            get { return _subIssues; }
            set { _subIssues = value; }
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

        public IssueModel()
        {

        }
        public IssueModel(string name, string description, UserModel owner, IssueDifficulty difficulty = IssueDifficulty.Hard)
        {
            // set values
            _issueName = name;
            _description = description;
            _difficulty = difficulty;
            _progressPercenage = 0.0;
            _creationDate = DateTime.Today;
            // add owner
            _addedUsers = new List<UserModel>();
            _addedUsers.Add(owner);
        }
        public IssueModel(string name, string description, IssueModel parentIssue, IssueDifficulty difficulty)
        {
            // set values
            _issueName = name;
            _description = description;
            _difficulty = difficulty;
            _progressPercenage = 0.0;
            _creationDate = DateTime.Today;
            // add owner
            _addedUsers = new List<UserModel>();
            _addedUsers.Add(parentIssue.AddedUsers[0]); //owner of the parent issue will be the owner of this issue
            // add parent issue
            _parentIssue = parentIssue;
            parentIssue.AddSubIssue(this);
        }
        public void AddSubIssue(IssueModel issue)
        {
            if (_subIssues == null) _subIssues = new List<IssueModel>();
            _subIssues.Add(issue);
            calcProgress();
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
                _progressPercenage = sumPercentage / counter;
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