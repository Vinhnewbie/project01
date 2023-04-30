using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using quanlyphonghoc;

namespace quanlyphonghoc.Controllers
{
    public class sinhviensController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        //get: sinhviens
        public ActionResult Index(string searchv2, int? page, int? pageSize)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            var sinhviens = db.sinhviens.Include(s => s.lop).Include(s => s.lop.nganhhoc);


            if (!String.IsNullOrEmpty(searchv2))
            {

                sinhviens = sinhviens.Where(s => s.mssv.Contains(searchv2)

                || s.hoten.Contains(searchv2)

                || s.diachi.Contains(searchv2)

                || s.malop.Contains(searchv2)

                || s.lop.nganhhoc.tennghanh.Contains(searchv2)

                || s.gioitinh.Contains(searchv2));

            }
           //var results = sinhviens.ToList();

            if (sinhviens.ToList().Count == 0)
            {
                ViewBag.Message = "khong tim thay!.";
            }
            //if (page == null)
            //{
            //    page = 1;
            //}
            //int pageIndex = 1;
            //pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            //var tamtenid = db.sinhviens;
            //int count = tamtenid.Count();

            //var ten = (from i in db.sinhviens select i).OrderBy(s => s.hoten);

            int defaSize = (pageSize ?? 5);
            int pageNumber = page ?? 1;
            //return View(results);
           
            var pagedSinhViens = sinhviens.OrderBy(s => s.hoten).ToPagedList(pageNumber, defaSize);
            return View(pagedSinhViens);
        }



        //public ActionResult Index(string searchBy, string searchValue)
        //{
        //    var sinhviens = db.sinhviens.Include(s => s.lop).Include(s => s.lop.nganhhoc);


        //    if (!String.IsNullOrEmpty(searchBy) && !String.IsNullOrEmpty(searchValue))
        //    {
        //        switch (searchBy)
        //        {
        //            case "mssv":
        //                sinhviens = sinhviens.Where(s => s.mssv.Contains(searchValue));
        //                break;
        //            case "hoten":
        //                sinhviens = sinhviens.Where(s => s.hoten.Contains(searchValue));
        //                break;
        //            case "malop":
        //                sinhviens = sinhviens.Where(s => s.malop.Contains(searchValue));
        //                break;
        //            case "tennghanh":
        //                sinhviens = sinhviens.Where(s => s.lop.nganhhoc.tennghanh.Contains(searchValue));
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    return View(sinhviens.ToList());
        //}

        //public ActionResult Index(string search, int? i)
        //{
        //    var sinhviens = db.sinhviens.Include(s => s.lop).Include(s => s.lop.nganhhoc);

        //      List<sinhvien> listtemp = db.sinhviens.ToList();
        

        //    return View(db.sinhviens.Where(x => x.hoten.StartsWith(search) || search == null).ToList().ToPagedList(i ?? 1, 3));
        //}

        // GET: sinhviens/Details/5
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
            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // GET: sinhviens/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh");
            return View();
        }

        // POST: sinhviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mssv,hoten,gioitinh,ngaysinh,diachi,malop")] sinhvien sinhvien, HttpPostedFileBase HinhAnh)
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
                sinhvien.HinhAnh = "/anh/" + HinhAnh.FileName;
            }
            if (ModelState.IsValid)
            {
                db.sinhviens.Add(sinhvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", sinhvien.malop);
            return View(sinhvien);
        }

        // GET: sinhviens/Edit/5
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
            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", sinhvien.malop);
            return View(sinhvien);
        }

        // POST: sinhviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "mssv,hoten,ngaysinh,diachi,malop,HinhAnh")] sinhvien sinhvien, HttpPostedFileBase HinhAnh)
        public ActionResult Edit([Bind(Include = "mssv,hoten,gioitinh,ngaysinh,diachi,malop")] sinhvien sinhvien, HttpPostedFileBase HinhAnh)
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
                sinhvien.HinhAnh = "/anh/" + HinhAnh.FileName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(sinhvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.malop = new SelectList(db.lops, "malop", "manghanh", sinhvien.malop);
            return View(sinhvien);
        }

        // GET: sinhviens/Delete/5
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
            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // POST: sinhviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Admin/nguoidungs");
            }
            sinhvien sinhvien = db.sinhviens.Find(id);
            db.sinhviens.Remove(sinhvien);
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
