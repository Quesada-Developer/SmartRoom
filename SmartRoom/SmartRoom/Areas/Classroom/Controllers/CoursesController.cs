using Microsoft.AspNet.Identity;
using SmartRoom.Web.App_Start;
using SmartRoom.Web.Areas.Classroom.Models;
using SmartRoom.Web.Controllers;
using SmartRoom.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.Classroom.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SmartModel db = new SmartModel();
        private readonly AccountController Account = new AccountController();

        // GET: /Classroom/Courses/
        [Authorize]
        public ActionResult Index()
        {
            string _UserId = User.Identity.GetUserId();
            var list = db.Users.Find(_UserId);
            List<Course> Courses = (list != null)? list.CoursesByRole(CourseRole.owner).ToList(): new List<Course>();
            return View(Courses);
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult Enroll()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string DeleteUserFromCourse(RemoveUserFromCourse model)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.GetUserId().Equals(model.AccountId))
                {
                    //user is not in course 
                    Response.StatusCode = 400;
                    return "You can't remove your self ";
                }

                if (!db.Courses.ToList().Any(obj => obj.UserRelationships.Any(u => u.AccountId.Equals(model.AccountId))))
                {
                    //user is not in course 
                    Response.StatusCode = 400;
                    return "User is not part of this course";
                }

                if (db.Courses.ToList().Any(obj => obj.UserRelationships.Any(u => u.AccountId.Equals(User.Identity.GetUserId()) && u.AccountRole == CourseRole.owner)))
                {
                    var course = db.Courses.Find(model.CourseId).UserRelationships.Where(obj => obj.AccountId.Equals(model.AccountId) && obj.Course.Id == model.CourseId).ToList();
                    if(!(course.Count > 0))
                    {
                        //course is not found 
                        Response.StatusCode = 400;
                        return "Error removing user";
                    }
                    var c = db.UserRelationships.Where(obj => obj.AccountId.Equals(model.AccountId) && obj.Course.Id.Equals(model.CourseId)).FirstOrDefault();
                    foreach (var r in course)
                    {
                        r.IsDeleted = true;
                        r.ModifedBy = User.Identity.GetUserId();
                        r.ModifiedDate = DateTime.Now;
                    }
                    db.SaveChanges();

                    Response.StatusCode = 200;
                    return "User " + db.Users.Find(model.AccountId).UserName + " has been removed.";
                }
                else
                {
                    Response.StatusCode = 403;
                    return "You can not make changes to this course";
                }
            }
            Response.StatusCode = 400;
            return "All values are required";
        }

        [HttpGet]
        public ActionResult GetUsers(int CourseId)
        {
            var course = db.Courses.Find(CourseId);
            if(course == null)
            {
                Response.StatusCode = 400;
                return Content("Error finding course");
            }
            return PartialView("Partial/_UserRelationships", course.UserRelationships.Where(obj => !obj.IsDeleted));
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddUserFromModeal(AddUserToCourse model)
        {
            if (ModelState.IsValid)
            {

                if (db.Courses.ToList().Any(obj => obj.UserRelationships.Any(u => !u.IsDeleted && u.AccountId.Equals(model.AccountId))))
                {
                    //user is in course already
                    Response.StatusCode = 400;
                    return "User is already part of this course";
                }

                if(db.Courses.ToList().Any(obj => obj.UserRelationships.Any(u => u.AccountId.Equals(User.Identity.GetUserId()) && u.AccountRole == CourseRole.owner)))
                {
                    UserRelationship _UserRelationship;
                    if(db.Courses.Find(model.CourseId).UserRelationships.Any(obj => obj.AccountId == model.AccountId && obj.IsDeleted == true))
                    {
                        _UserRelationship = db.Courses.Find(model.CourseId).UserRelationships.Where(obj => obj.AccountId == model.AccountId && obj.IsDeleted == true).ToList()[0];
                        _UserRelationship.IsDeleted = false;
                        _UserRelationship.ModifiedDate = DateTime.Now;
                        _UserRelationship.ModifedBy = User.Identity.GetUserId();
                        db.SaveChanges();
                    }
                    else
                    {

                    
                    _UserRelationship = new UserRelationship() {
                        AccountId = model.AccountId, 
                        AccountRole = (CourseRole)model.AccountRole, 
                        CreateDate = DateTime.Now, 
                        /*CreatedBy = User.Identity.GetUserId(),*/ 
                        Course = db.Courses.Find(model.CourseId) 
                    };
                    db.UserRelationships.Add(_UserRelationship);
                    db.Courses.Find(model.CourseId).UserRelationships.Add(_UserRelationship);
                    db.Users.Find(model.AccountId).Courses.Add(db.Courses.Find(model.CourseId));
                    db.SaveChanges();
                }
                    

                    Response.StatusCode = 200;
                    return "User " + db.Users.Find(model.AccountId).UserName + " has been added.";
                }
                else
                {
                    Response.StatusCode = 403;
                    return "You can not make changes to this course";
                }
            }
            Response.StatusCode = 400;
            return "All values are required";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string EnrollInPage(string couse, string user)
        {
            return "error";
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll(EnrollViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (!db.Courses.Any(obj => obj.RegistrationCode.Equals(model.RegistrationCode)))
                {
                    ModelState.AddModelError("RegistrationCode", "Invalid Registration Code");
                    return View(model);
                }
                Course course = db.Courses.Where(obj => obj.RegistrationCode.Equals(model.RegistrationCode)).FirstOrDefault();
                if(course.UserRelationships.Any(c => c.AccountId.Equals(User.Identity.GetUserId())))
                {
                    ModelState.AddModelError("RegistrationCode", "You can have already register for this course.");
                    return View(model);
                }
                UserRelationship _UserRelationship = new UserRelationship() { AccountId = User.Identity.GetUserId(), AccountRole = CourseRole.student };
                db.UserRelationships.Add(_UserRelationship);
                course.UserRelationships.Add(_UserRelationship);
                db.Users.Find(User.Identity.GetUserId()).Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = course.Id });
            }

            return View(model);
        }


        /*
         * Get User Courses
         * db.Users.Find(User.Identity.GetUserId()).Courses
         * 
         * Ger User Role For A Course
         * db.Users.Find(User.Identity.GetUserId()).RoleFromCourse(_Course)
         * 
         * Get a Courses for user were user is a owner
         * db.Users.Find(User.Identity.GetUserId()).CoursesByRole(Database.Helpers.CourseRole.owner)
         * 
         * */

        // GET: /Classroom/Courses/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string _UserId = User.Identity.GetUserId();
            Course course = db.Courses.Find(id);// db.Courses.Where(obj => obj.Id == id).FirstOrDefault();
            if (course == null)
            {
                return HttpNotFound();
            }
            if (!course.UserRelationships.Any(obj => obj.AccountId.Equals(_UserId)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            
            return View(course);
        }

        // GET: /Classroom/Courses/Create
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Classroom/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Create([Bind(Include="Id,Subject,CourseNumber,Section,Title,StartDate,EndDate,Location,Term,CreateDate")] Course course)
        {
            course.CreatedById = User.Identity.GetUserId();
            course.isActive = true;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                UserRelationship _UserRelationship = new UserRelationship() { AccountId = course.CreatedById, AccountRole = CourseRole.owner, CreatedBy = User.Identity.GetUserId() };
                db.UserRelationships.Add(_UserRelationship);
                course.UserRelationships.Add(_UserRelationship);
                db.Courses.Add(course);
                db.Users.Find(User.Identity.GetUserId()).Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: /Classroom/Courses/Edit/5
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string _UserId = User.Identity.GetUserId();
            List<Course> Courses = db.UserRelationships.Where(obj => obj.Account.Id.Equals(_UserId)).Select(obj => obj.Course).ToList();
            Course course = db.Courses.Find(id);// db.Courses.Where(obj => obj.Id == id).FirstOrDefault();
            
            //User is owner of course or is a admin
            if(!course.UserRelationships.Any(obj => obj.AccountId.Equals(_UserId) && obj.AccountRole == CourseRole.owner) || !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Classroom/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult Edit([Bind(Include="Id,Subject,CourseNumber,Section,Title,StartDate,EndDate,Location,Term,CreatedBy,CreateDate,ModifedBy,ModifiedDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.ModifedById = db.Users.Find(User.Identity.GetUserId()).Id;
                course.ModifiedDate = DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }
        [Authorize(Roles = "Teacher,Admin")]
        // GET: /Classroom/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Users.Find(User.Identity.GetUserId()).Courses.Where(obj => obj.Id == id).FirstOrDefault();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Classroom/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher,Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Users.Find(User.Identity.GetUserId()).Courses.Where(obj => obj.Id == id).FirstOrDefault();
            course.isActive = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
