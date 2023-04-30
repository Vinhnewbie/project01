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
    public class buoihocsController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: buoihocs
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View(db.buoihocs.ToList());
        }

        // GET: buoihocs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buoihoc buoihoc = db.buoihocs.Find(id);
            if (buoihoc == null)
            {
                return HttpNotFound();
            }
            return View(buoihoc);
        }

        // GET: buoihocs/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View();
        }

        // POST: buoihocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "buoihoc1,ngay,tietbatdau,sotiet")] buoihoc buoihoc)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.buoihocs.Add(buoihoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(buoihoc);
        }

        // GET: buoihocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buoihoc buoihoc = db.buoihocs.Find(id);
            if (buoihoc == null)
            {
                return HttpNotFound();
            }
            return View(buoihoc);
        }

        // POST: buoihocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "buoihoc1,ngay,tietbatdau,sotiet")] buoihoc buoihoc)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.Entry(buoihoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buoihoc);
        }

        // GET: buoihocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buoihoc buoihoc = db.buoihocs.Find(id);
            if (buoihoc == null)
            {
                return HttpNotFound();
            }
            return View(buoihoc);
        }

        // POST: buoihocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            buoihoc buoihoc = db.buoihocs.Find(id);
            db.buoihocs.Remove(buoihoc);
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
