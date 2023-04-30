using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using quanlyphonghoc;

namespace quanlyphonghoc.Controllers
{
    public class tkbsController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: tkbs
        public ActionResult Index(string searchBy, string searchValue, int? page, int? pageSize)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }

            var tkbs = db.tkbs.Include(t => t.buoihoc1).Include(t => t.phancong).Include(t => t.phonghoc);

            if (!String.IsNullOrEmpty(searchBy) && !String.IsNullOrEmpty(searchValue))
            {
                switch (searchBy)
                {
                    case "mapc":
                        tkbs = tkbs.Where(s => s.mapc.Contains(searchValue));
                        break;
                    case "magv":
                        tkbs = tkbs.Where(s => s.phancong.magv.Contains(searchValue));
                        break;
                    case "maphong":
                        tkbs = tkbs.Where(s => s.maphong.Contains(searchValue));
                        break;
                    case "malop":
                        tkbs = tkbs.Where(s => s.phancong.malop.Contains(searchValue));
                        break;
                    default:
                        break;
                }
            }
            int pageNumber = page ?? 1;
            int defaSize = pageSize ?? 5;
            var pagetkb = tkbs.OrderBy(s => s.mapc).ToPagedList(pageNumber, defaSize);
            return View(pagetkb);
            //return View(tkbs.ToList());
        }

        // GET: tkbs/Details/5
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
            tkb tkb = db.tkbs.Find(id);
            if (tkb == null)
            {
                return HttpNotFound();
            }
            return View(tkb);
        }

        // GET: tkbs/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            ViewBag.buoihoc = new SelectList(db.buoihocs, "buoihoc1", "buoihoc1");
            ViewBag.mapc = new SelectList(db.phancongs, "mapc", "magv");
            ViewBag.maphong = new SelectList(db.phonghocs, "maphong", "chucnang");
            return View();
        }

        // POST: tkbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mapc,buoihoc,maphong")] tkb tkb)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.tkbs.Add(tkb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.buoihoc = new SelectList(db.buoihocs, "buoihoc1", "buoihoc1", tkb.buoihoc);
            ViewBag.mapc = new SelectList(db.phancongs, "mapc", "magv", tkb.mapc);
            ViewBag.maphong = new SelectList(db.phonghocs, "maphong", "chucnang", tkb.maphong);
            return View(tkb);
        }

        // GET: tkbs/Edit/5
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
            tkb tkb = db.tkbs.Find(id);
            if (tkb == null)
            {
                return HttpNotFound();
            }
            ViewBag.buoihoc = new SelectList(db.buoihocs, "buoihoc1", "buoihoc1", tkb.buoihoc);
            ViewBag.mapc = new SelectList(db.phancongs, "mapc", "magv", tkb.mapc);
            ViewBag.maphong = new SelectList(db.phonghocs, "maphong", "chucnang", tkb.maphong);
            return View(tkb);
        }

        // POST: tkbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mapc,buoihoc,maphong")] tkb tkb)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.Entry(tkb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.buoihoc = new SelectList(db.buoihocs, "buoihoc1", "buoihoc1", tkb.buoihoc);
            ViewBag.mapc = new SelectList(db.phancongs, "mapc", "magv", tkb.mapc);
            ViewBag.maphong = new SelectList(db.phonghocs, "maphong", "chucnang", tkb.maphong);
            return View(tkb);
        }

        // GET: tkbs/Delete/5
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
            tkb tkb = db.tkbs.Find(id);
            if (tkb == null)
            {
                return HttpNotFound();
            }
            return View(tkb);
        }

        // POST: tkbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            tkb tkb = db.tkbs.Find(id);
            db.tkbs.Remove(tkb);
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
