using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueTracker.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListUsers()
        {
            List<UserModel> users = new List<UserModel>();
            List<IssueModel> issues = new List<IssueModel>();

            UserModel user1 = new UserModel("User1");
            users.Add(user1);
            UserModel user2 = new UserModel("User2");
            users.Add(user2);
            UserModel user3 = new UserModel("User3");
            users.Add(user3);

            IssueModel issue1 = new IssueModel("issue1", "description of issue1", user1);            
            issues.Add(issue1);
            IssueModel issue11 = new IssueModel("issue11", "description of issue11", issue1.AddedUsers.First(), issue1, IssueDifficulty.Easy);
            issues.Add(issue11);
            IssueModel issue12 = new IssueModel("issue12", "description of issue12", issue1.AddedUsers.First(), issue1, IssueDifficulty.Hard);
            issues.Add(issue12);

            issue11.Progress = 0.5;
            issue12.Progress = 0.7;

            return View(users);
        }
    }
}