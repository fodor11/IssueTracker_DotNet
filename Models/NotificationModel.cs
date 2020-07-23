using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public enum NotificationType
    {
        /// <summary>
        /// A new subissue was assigned to an issue, on which the user is working
        /// </summary>
        NewSubIssue,
        /// <summary>
        /// Someone made progress on an issue, on which the user is working
        /// </summary>
        Progress,
        /// <summary>
        /// An issue was finished, on which the user was working
        /// </summary>
        FinishedIssue,
        /// <summary>
        /// The user was added to an issue
        /// </summary>
        AssignedToIssue,
        /// <summary>
        /// The user was removed from an issue
        /// </summary>
        RemovedFromIssue,
        /// <summary>
        /// For any other case
        /// </summary>
        Other
    }
    public class NotificationModel
    {
        private int _id;
        private string _message;
        private DateTime _date;
        private NotificationType _notifType;
        private bool _seen = false;

        public int Id
        { 
            get { return _id; }
        }
        public string Message 
        { 
            get { return _message; }
            set { _message = value; }
        }
        public DateTime Date
        {
            get { return _date; }
        }
        public NotificationType NotificationType
        {
            get { return _notifType; }
            set { _notifType = value; }
        }
        public bool Seen
        {
            get { return _seen; }
            set { _seen = value; }
        }
        public NotificationModel() {}
        /// <summary>
        /// Creates a notification and adds it to a user
        /// </summary>
        /// <param name="message">Text of the notification</param>
        /// <param name="nType">Type of the notification (NotificationType enumeration)</param>
        /// <param name="owner">The user the notification has to be added to</param>
        public NotificationModel(string message, NotificationType nType, UserModel owner)
        {
            _message = message;
            _notifType = nType;
            _date = DateTime.Today;
            owner.AddNotification(this);
        }
    }
}