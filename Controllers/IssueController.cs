using IssueTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueTracker.Controllers
{
    public class IssueController : Controller
    {
        // GET: Issue
        public ActionResult Index()
        {
            //basic dummies
            UserModel dummyOwner = new UserModel("dummy owner");
            IssueModel specialSnowflake = new IssueModel("Special snowflake", "The most special issue of them all", dummyOwner, null, IssueDifficulty.Hard, IssueStatus.InProgress, IssuePriority.High);
            specialSnowflake.Progress = 0.86;
            specialSnowflake.SuspendIssue();
            List<IssueModel> dummyIssues = new List<IssueModel> 
            { 
                new IssueModel("Main issue1", "Desription of the issue", dummyOwner),
                new IssueModel("Main issue2", "Desription of second issue", dummyOwner),
                new IssueModel("Main issue3", "You guessed right bro. Description of the 3rd issue", dummyOwner),
                specialSnowflake
            };

            //deeper issues for another view
            //List<UserModel> users = new List<UserModel>();
            //UserModel user1 = new UserModel("User1");
            //users.Add(user1);
            //UserModel user2 = new UserModel("User2");
            //users.Add(user2);
            //UserModel user3 = new UserModel("User3");
            //users.Add(user3);

            //IssueModel issue1 = new IssueModel("issue1", "description of issue1", user1);
            //dummyIssues.Add(issue1);
            //IssueModel issue11 = new IssueModel("issue11", "description of issue11", issue1.AddedUsers.First(), issue1, IssueDifficulty.Easy);
            //dummyIssues.Add(issue11);
            //IssueModel issue12 = new IssueModel("issue12", "description of issue12", issue1.AddedUsers.First(), issue1, IssueDifficulty.Hard);
            //dummyIssues.Add(issue12);
            //issue11.Progress = 0.5;
            //issue12.Progress = 0.7;

            return View(dummyIssues);
        }

        // GET: Issue/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Issue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Issue/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Issue/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Issue/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Issue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Issue/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
