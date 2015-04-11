using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartRoom.Web.Areas.Forms.Controllers
{
    public class SyllabusController : Controller
    {
        // GET: Forms/Syllabus
        public ActionResult Index()
        {
            return View();
        }

        // GET: Forms/Syllabus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Forms/Syllabus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forms/Syllabus/Create
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

        // GET: Forms/Syllabus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Forms/Syllabus/Edit/5
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

        // GET: Forms/Syllabus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Forms/Syllabus/Delete/5
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
