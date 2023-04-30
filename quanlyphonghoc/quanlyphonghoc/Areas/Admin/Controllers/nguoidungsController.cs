using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using quanlyphonghoc;

namespace quanlyphonghoc.Areas.Admin.Controllers
{
    public class nguoidungsController : Controller
    {
        private quanlylichhoc2Entities db = new quanlylichhoc2Entities();

        // GET: Admin/nguoidungs
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "nguoidungs");
            }
            return View(db.nguoidungs.ToList());
        }

        // Dang nhap
      
        public ActionResult Login(nguoidung model)
        {
            var user = model.tendangnhap;
            var password = model.matkhau;
            // check if user and password are not empty
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "nhap lai ten va mat khau";
                return View();
            }

            // hash the password using MD
            string hashedPassword = GetMD5(password);
            
            // check if user exists in database and the password matches
            var dbUser = db.nguoidungs.SingleOrDefault(u => u.tendangnhap == user && u.matkhau == hashedPassword);
            if (dbUser == null)
            {
                ViewBag.Error = "Invalid user name or password";
                return View();
            }

            // set authentication cookie and redirect to home page
            Response.Cookies["YourAuthCookie"].Value = dbUser.tendangnhap;
            Response.Cookies["YourAuthCookie"].Expires = DateTime.Now.AddMinutes(15);

            // set user session and redirect to home page
            Session["user"] = dbUser.tendangnhap;
            return RedirectToAction("Index", "nguoidungs");
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //
        //public ActionResult Login(string user, string password)
        //{
        //    // check if user and password are not empty
        //    if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        //    {
        //        ViewBag.Error = "nhap lai ten va mat khau";
        //        return View();
        //    }

        //    // check if user exists in database and the password matches
        //    var dbUser = db.nguoidungs.SingleOrDefault(u => u.tendangnhap == user && u.matkhau == password);
        //    if (dbUser == null)
        //    {
        //        ViewBag.Error = "Invalid user name or password";
        //        return View();
        //    }

        //    // set user session and redirect to home page
        //    Session["user"] = dbUser.tendangnhap;
        //    return RedirectToAction("Index", "nguoidungs");
        //}


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login","nguoidungs");
        }


        // GET: Admin/nguoidungs/Details/5
        public ActionResult Details(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidung nguoidung = db.nguoidungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            return View(nguoidung);
        }

        // GET: Admin/nguoidungs/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "nguoidungs");
            }
            return View();
        }

        // POST: Admin/nguoidungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tendangnhap,matkhau,tenguoidung,quyen,motaquyen")] nguoidung nguoidung)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "nguoidungs");
            }
            if (ModelState.IsValid)
            {
                nguoidung.matkhau = GetMD5(nguoidung.matkhau);
                db.Configuration.ValidateOnSaveEnabled = false;
                db.nguoidungs.Add(nguoidung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nguoidung);
        }

        // GET: Admin/nguoidungs/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidung nguoidung = db.nguoidungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            return View(nguoidung);
        }

        // POST: Admin/nguoidungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tendangnhap,matkhau,tenguoidung,quyen,motaquyen")] nguoidung nguoidung)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "nguoidungs");
            }
            if (ModelState.IsValid)
            {
                db.Entry(nguoidung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nguoidung);
        }

        // GET: Admin/nguoidungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "nguoidungs");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nguoidung nguoidung = db.nguoidungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            return View(nguoidung);
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }





        // POST: Admin/nguoidungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "nguoidungs");
            }
            nguoidung nguoidung = db.nguoidungs.Find(id);
            db.nguoidungs.Remove(nguoidung);
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
