using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using quanlyphonghoc;

namespace quanlyphonghoc.Controllers
{
    public class phonghocsController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: phonghocs
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View(db.phonghocs.ToList());
        }

        // GET: phonghocs/Details/5
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
            phonghoc phonghoc = db.phonghocs.Find(id);
            if (phonghoc == null)
            {
                return HttpNotFound();
            }
            return View(phonghoc);
        }

        // GET: phonghocs/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            return View();
        }

        // POST: phonghocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maphong,chucnang,succhua,diachi")] phonghoc phonghoc)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.phonghocs.Add(phonghoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phonghoc);
        }

        // GET: phonghocs/Edit/5
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
            phonghoc phonghoc = db.phonghocs.Find(id);
            if (phonghoc == null)
            {
                return HttpNotFound();
            }
            return View(phonghoc);

           
        }

        // POST: phonghocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maphong,chucnang,succhua,diachi")] phonghoc phonghoc)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.Entry(phonghoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phonghoc);
        }

        // GET: phonghocs/Delete/5
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
            phonghoc phonghoc = db.phonghocs.Find(id);
            if (phonghoc == null)
            {
                return HttpNotFound();
            }
            return View(phonghoc);
        }

        // POST: phonghocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            phonghoc phonghoc = db.phonghocs.Find(id);
            db.phonghocs.Remove(phonghoc);
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
