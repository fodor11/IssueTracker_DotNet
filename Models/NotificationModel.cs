using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class NotificationModel
    {
        private int _id;
        private string _message;
        private UserModel _owner;

        public int Id
        { 
            get { return _id; }
        }
        public string Message 
        { 
            get { return _message; }
            set { _message = value; }
        }
        public NotificationModel() {}
        public NotificationModel(string message, UserModel owner)
        {
            _message = message;
            _owner = owner;
        }
    }
}