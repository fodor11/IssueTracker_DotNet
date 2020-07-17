using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class UserModel
    {
        private int _id;
        private string _name;
        
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
        public List<IssueModel> Issues { get; }
        public List<NotificationModel> Notifications { get; }

        public UserModel()
        {
            _issues = new List<IssueModel>();
            _notifications = new List<NotificationModel>();
        }
        public UserModel(string name, List<IssueModel> issues, List<NotificationModel> notifications)
        {
            _name = name;
            _issues = issues;
            _notifications = notifications;
        }


        public void AddNotification(NotificationModel notification)
        {
            _notifications.Add(notification);            
        }
        //TODO: 
        //Notification remove (id), getFirst10, setRead
        //Issues add, remove
    }
}