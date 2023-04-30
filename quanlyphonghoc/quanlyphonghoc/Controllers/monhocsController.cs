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
    public class monhocsController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: monhocs
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View(db.monhocs.ToList());
        }

        // GET: monhocs/Details/5
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
            monhoc monhoc = db.monhocs.Find(id);
            if (monhoc == null)
            {
                return HttpNotFound();
            }
            return View(monhoc);
        }

        // GET: monhocs/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View();
        }

        // POST: monhocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mamh,tenmh,stc,lythuyet,thuchanh")] monhoc monhoc)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.monhocs.Add(monhoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monhoc);
        }

        // GET: monhocs/Edit/5
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
            monhoc monhoc = db.monhocs.Find(id);
            if (monhoc == null)
            {
                return HttpNotFound();
            }
            return View(monhoc);
        }

        // POST: monhocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mamh,tenmh,stc,lythuyet,thuchanh")] monhoc monhoc)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.Entry(monhoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monhoc);
        }

        // GET: monhocs/Delete/5
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
            monhoc monhoc = db.monhocs.Find(id);
            if (monhoc == null)
            {
                return HttpNotFound();
            }
            return View(monhoc);
        }

        // POST: monhocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            monhoc monhoc = db.monhocs.Find(id);
            db.monhocs.Remove(monhoc);
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
