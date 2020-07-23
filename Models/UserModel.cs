using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace IssueTracker.Models
{
    public class UserModel
    {
        private int _id;
        private string _name;
        private string _email;
        private string _distortedPassword;
        private string _profilePictrePath;
        private List<IssueModel> _issues;
        private List<NotificationModel> _notifications;

        public int Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// Path to the uploaded profile picture of the user
        /// </summary>
        public string ProfilePictrePath
        {
            get { return _profilePictrePath; }
            set { _profilePictrePath = value; }
        }
        /// <summary>
        /// Issues that are assigned to this user
        /// </summary>
        public List<IssueModel> Issues
        {
            get { return _issues; }
        }
        /// <summary>
        /// Notifications that belong to this user
        /// </summary>
        public List<NotificationModel> Notifications
        {
            get { return _notifications; }
        }

        public UserModel() { }
        /// <summary>
        /// Constructor for the user
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <param name="email">Email of the user</param>
        /// <param name="issues">List of issues that are assigned to the user</param>
        /// <param name="notifications">List of notifications that are assigned to the user</param>
        /// <param name="profilePicturePath">Path to the profile picture of the user</param>
        /// <param name="password">Password chosen by the user</param>
        public UserModel(string name, string email = "e@e.e", List<IssueModel> issues = null, List<NotificationModel> notifications = null, string profilePicturePath = default(string), string password = "defaultPswd")
        {
            _name = name;
            _email = email;
            _issues = issues;
            _notifications = notifications;
            _profilePictrePath = profilePicturePath;
            _distortedPassword = DistortPassword(password);
        }
        /// <summary>
        /// Adds the notification to the user
        /// </summary>
        /// <param name="notification">The notification to be added to the user</param>
        public void AddNotification(NotificationModel notification)
        {
            if (_notifications == null)
            {
                _notifications = new List<NotificationModel>();
            }
            _notifications.Add(notification);
        }
        /// <summary>
        /// Returns the list of unread notifications
        /// </summary>
        /// <returns>A list of notifications, that are unread. Can be null!</returns>
        public List<NotificationModel> GetUnreadNotifications()
        {
            if (_notifications != null)
            {
                List<NotificationModel> unreadNotfis = new List<NotificationModel>();
                foreach (var item in _notifications)
                {
                    if (item.Seen == true)
                    {
                        unreadNotfis.Add(item);
                    }
                }
                return unreadNotfis;
            }
            else return null;
        }
        /// <summary>
        /// Removes the given notification from the user
        /// </summary>
        /// <param name="notification">The notification that has to be deleted</param>
        public void RemoveNotification(NotificationModel notification)
        {
            if (_notifications != null)
            {
                _notifications.Remove(notification);
            }
        }
        /// <summary>
        /// Adds an issue to the user
        /// </summary>
        /// <param name="issue">The issue to be added to the user</param>
        public void AddIssue(IssueModel issue)
        {
            if (_issues == null)
            {
                _issues = new List<IssueModel>();
            }
            _issues.Add(issue);
        }
        /// <summary>
        /// Removes the given issue from the user
        /// </summary>
        /// <param name="issue">The issue to be removed from the user</param>
        public void RemoveIssueFromUser(IssueModel issue)
        {
            if (_issues != null)
            {
                _issues.Remove(issue);
            }
        }
        /// <summary>
        /// Checks if the given password is the same as the stored one
        /// </summary>
        /// <param name="password">The password to be checked</param>
        /// <returns>Returns true, if the password is correct, returns false otherwise</returns>
        public bool CheckPassword(string password)
        {
            if (_distortedPassword.Equals(DistortPassword(password), StringComparison.Ordinal))
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Simulates "salting" and hashing the real password
        /// </summary>
        /// <param name="password">The password to be distorted</param>
        /// <returns>Returns the distorted password</returns>
        private string DistortPassword(string password)
        {
            StringBuilder saltedpswd = new StringBuilder();
            foreach (var item in password)
            {
                char c = (char)(item * 2);
                saltedpswd.Append(c);
            }
             return saltedpswd.ToString();
        }
    }
}