using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using quanlyphonghoc;

namespace quanlyphonghoc.Controllers
{
    public class trinhdoesController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: trinhdoes
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View(db.trinhdoes.ToList());
        }

        // GET: trinhdoes/Details/5
        public ActionResult Details(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trinhdo trinhdo = db.trinhdoes.Find(id);
            if (trinhdo == null)
            {
                return HttpNotFound();
            }
            return View(trinhdo);
        }

        // GET: trinhdoes/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View();
        }

        // POST: trinhdoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "matrinhdo,trentrinhdo,hesoluong")] trinhdo trinhdo)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.trinhdoes.Add(trinhdo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trinhdo);
        }

        // GET: trinhdoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trinhdo trinhdo = db.trinhdoes.Find(id);
            if (trinhdo == null)
            {
                return HttpNotFound();
            }
            return View(trinhdo);
        }

        // POST: trinhdoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "matrinhdo,trentrinhdo,hesoluong")] trinhdo trinhdo)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.Entry(trinhdo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trinhdo);
        }

        // GET: trinhdoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trinhdo trinhdo = db.trinhdoes.Find(id);
            if (trinhdo == null)
            {
                return HttpNotFound();
            }
            return View(trinhdo);
        }

        // POST: trinhdoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            trinhdo trinhdo = db.trinhdoes.Find(id);
            db.trinhdoes.Remove(trinhdo);
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
