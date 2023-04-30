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
    public class nganhhocsController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: nganhhocs
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View(db.nganhhocs.ToList());
        }

        // GET: nganhhocs/Details/5
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
            nganhhoc nganhhoc = db.nganhhocs.Find(id);
            if (nganhhoc == null)
            {
                return HttpNotFound();
            }
            return View(nganhhoc);
        }

        // GET: nganhhocs/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View();
        }

        // POST: nganhhocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "manghanh,tennghanh,khoa")] nganhhoc nganhhoc)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.nganhhocs.Add(nganhhoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nganhhoc);
        }

        // GET: nganhhocs/Edit/5
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
            nganhhoc nganhhoc = db.nganhhocs.Find(id);
            if (nganhhoc == null)
            {
                return HttpNotFound();
            }
            return View(nganhhoc);
        }

        // POST: nganhhocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "manghanh,tennghanh,khoa")] nganhhoc nganhhoc)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.Entry(nganhhoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nganhhoc);
        }

        // GET: nganhhocs/Delete/5
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
            nganhhoc nganhhoc = db.nganhhocs.Find(id);
            if (nganhhoc == null)
            {
                return HttpNotFound();
            }
            return View(nganhhoc);
        }

        // POST: nganhhocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            nganhhoc nganhhoc = db.nganhhocs.Find(id);
            db.nganhhocs.Remove(nganhhoc);
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
