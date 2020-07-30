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
        public ActionResult Index(int id = -1)
        {
            //basic dummies
            UserModel dummyOwner = new UserModel("dummy owner");
            IssueModel specialSnowflake = new IssueModel("Special_snowflake", "The most special issue of them all, but also suspended", dummyOwner, null, IssueDifficulty.Hard, IssueStatus.InProgress, IssuePriority.High);
            specialSnowflake.Progress = 0.86;
            specialSnowflake.SuspendIssue();
            IssueModel mainIssue1 = new IssueModel("Main_issue1", "Desription of the issue, it is finished. However lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin dictum sem non lorem efficitur feugiat. Duis eu arcu eget odio imperdiet rhoncus. Praesent eu faucibus orci. Curabitur vel turpis risus. Suspendisse fermentum neque sit amet consequat tincidunt. Etiam varius purus rutrum feugiat vulputate. Sed lacus ante, ullamcorper sed diam at, fermentum mattis nunc. Pellentesque ac ligula eget risus cursus ultricies in vitae nunc. Vestibulum interdum vel lectus eget vestibulum. Ut ut mi quis velit faucibus fringilla nec sollicitudin enim. Nunc enim orci, convallis ornare lectus non, mollis vehicula felis.", dummyOwner, null, IssueDifficulty.Easy, IssueStatus.InProgress, IssuePriority.Low);
            mainIssue1.FinishIssue();
            IssueModel mainIssue2 = new IssueModel("Main_issue2", "Desription of second issue, 46% progress", dummyOwner);
            mainIssue2.Progress = 0.46;
            IssueModel mainIssue3 = new IssueModel("Main_issue3", "You guessed right bro. Description of the 3rd issue. Just started", dummyOwner);
            mainIssue3.StartIssue();

            List<IssueModel> dummyIssues = new List<IssueModel>
            {
                mainIssue1,
                mainIssue2,
                mainIssue3,
                specialSnowflake
            };

            //deeper issues 
            //List<UserModel> users = new List<UserModel>();
            UserModel user1 = new UserModel("User1");
            //users.Add(user1);
            //UserModel user2 = new UserModel("User2");
            //users.Add(user2);
            //UserModel user3 = new UserModel("User3");
            //users.Add(user3);

            IssueModel issue1 = new IssueModel("issue1", "Description of issue1. This one has got children", user1);
            dummyIssues.Add(issue1);
            IssueModel issue11 = new IssueModel("issue11", "description of issue11", issue1.AddedUsers.First(), issue1, IssueDifficulty.Easy);
            //dummyIssues.Add(issue11);
            IssueModel issue12 = new IssueModel("issue12", "description of issue12", issue1.AddedUsers.First(), issue1, IssueDifficulty.Hard);
            //dummyIssues.Add(issue12);
            issue11.Progress = 0.5;
            issue12.Progress = 0.7;

            if (id >= 0 && dummyIssues[id].SubIssues != null)
            {
                return View(dummyIssues[id].SubIssues);
            }
            else return View(dummyIssues);
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
