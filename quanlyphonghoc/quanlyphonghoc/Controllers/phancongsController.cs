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
    public class phancongsController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: phancongs
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            var phancongs = db.phancongs.Include(p => p.giangvien).Include(p => p.lop).Include(p => p.lop1).Include(p => p.monhoc);
            //var phancongs = db.phancongs.Include(p => p.giangvien).Include(p => p.lop).Include(p => p.monhoc);
            return View(phancongs.ToList());
        }

        // GET: phancongs/Details/5
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
            phancong phancong = db.phancongs.Find(id);
            if (phancong == null)
            {
                return HttpNotFound();
            }
            return View(phancong);
        }

        // GET: phancongs/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten");
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh");
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh");
            ViewBag.mamh = new SelectList(db.monhocs, "mamh", "tenmh");
            return View();
        }

        // POST: phancongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mapc,magv,mamh,malop,hocky,namhoc")] phancong phancong)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.phancongs.Add(phancong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", phancong.magv);
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", phancong.malop);
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", phancong.malop);
            ViewBag.mamh = new SelectList(db.monhocs, "mamh", "tenmh", phancong.mamh);
            return View(phancong);
        }

        // GET: phancongs/Edit/5
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
            phancong phancong = db.phancongs.Find(id);
            if (phancong == null)
            {
                return HttpNotFound();
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", phancong.magv);
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", phancong.malop);
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", phancong.malop);
            ViewBag.mamh = new SelectList(db.monhocs, "mamh", "tenmh", phancong.mamh);
            return View(phancong);
        }

        // POST: phancongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mapc,magv,mamh,malop,hocky,namhoc")] phancong phancong)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.Entry(phancong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", phancong.magv);
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", phancong.malop);
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", phancong.malop);
            ViewBag.mamh = new SelectList(db.monhocs, "mamh", "tenmh", phancong.mamh);
            return View(phancong);
        }

        // GET: phancongs/Delete/5
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
            phancong phancong = db.phancongs.Find(id);
            if (phancong == null)
            {
                return HttpNotFound();
            }
            return View(phancong);
        }

        // POST: phancongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            phancong phancong = db.phancongs.Find(id);
            db.phancongs.Remove(phancong);
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
