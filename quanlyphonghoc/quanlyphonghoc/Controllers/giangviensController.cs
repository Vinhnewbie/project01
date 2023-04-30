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
    public class giangviensController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: giangviens
        public ActionResult Index(string searchBy, string searchValue, int? page, int? pageSize)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            var giangviens = db.giangviens.Include(g => g.trinhdo);

            if (!String.IsNullOrEmpty(searchBy) && !String.IsNullOrEmpty(searchValue))
            {
                switch (searchBy)
                {
                    case "magv":
                        giangviens = giangviens.Where(s => s.magv.Contains(searchValue));
                        break;
                    case "hoten":
                        giangviens = giangviens.Where(s => s.hoten.Contains(searchValue));
                        break;
                    case "trentrinhdo":
                        giangviens = giangviens.Where(s => s.trinhdo.trentrinhdo.Contains(searchValue));
                        break;
                    default:
                        break;
                }
            }
            
            int pageNumber = page ?? 1;
            int defaSize = (pageSize ?? 5);
            var pagegiangvien = giangviens.OrderBy(s => s.hoten).ToPagedList(pageNumber, defaSize);
            return View(pagegiangvien);
        }

        // GET: giangviens/Details/5
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
            giangvien giangvien = db.giangviens.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // GET: giangviens/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            ViewBag.matrinhdo = new SelectList(db.trinhdoes, "matrinhdo", "trentrinhdo");
            return View();
        }

        // POST: giangviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "magv,hoten,gioitinh,diachi,matrinhdo")] giangvien giangvien, HttpPostedFileBase HinhAnh)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (HinhAnh.ContentLength > 0)
            {
                //Luu anh
                string rootFolder = Server.MapPath("/anh/");
                string pathimages = rootFolder + HinhAnh.FileName;
                HinhAnh.SaveAs(pathimages);
                // luu thuoc tinh url HinhAnh
                giangvien.HinhAnh = "/anh/" + HinhAnh.FileName;
            }
            if (ModelState.IsValid)
            {
                db.giangviens.Add(giangvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.matrinhdo = new SelectList(db.trinhdoes, "matrinhdo", "trentrinhdo", giangvien.matrinhdo);
            return View(giangvien);
        }

        // GET: giangviens/Edit/5
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
            giangvien giangvien = db.giangviens.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.matrinhdo = new SelectList(db.trinhdoes, "matrinhdo", "trentrinhdo", giangvien.matrinhdo);
            return View(giangvien);
        }

        // POST: giangviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "magv,hoten,gioitinh,diachi,matrinhdo")] giangvien giangvien, HttpPostedFileBase HinhAnh)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            if (HinhAnh.ContentLength > 0)
            {
                //Luu anh
                string rootFolder = Server.MapPath("/anh/");
                string pathimages = rootFolder + HinhAnh.FileName;
                HinhAnh.SaveAs(pathimages);
                // luu thuoc tinh url HinhAnh
                giangvien.HinhAnh = "/anh/" + HinhAnh.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(giangvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.matrinhdo = new SelectList(db.trinhdoes, "matrinhdo", "trentrinhdo", giangvien.matrinhdo);
            return View(giangvien);
        }

        // GET: giangviens/Delete/5
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
            giangvien giangvien = db.giangviens.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // POST: giangviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            giangvien giangvien = db.giangviens.Find(id);
            db.giangviens.Remove(giangvien);
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
