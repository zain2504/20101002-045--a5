using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Indexuser()
        {
            return View();
        }


        public ActionResult Indexadmin()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        Model1 db = new Model1();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(M_Admin a)
        {
            int r = db.M_Admin.Where(x => x.Admin_Email == a.Admin_Email && x.Admin_Password == a.Admin_Password).Count();

            if (r == 1)
            {
                return RedirectToAction("Indexadmin", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid credentials";
                return View();
            }

           

        }
        public ActionResult FPassword(string Admin_Email, string Admin_Password, string ConfirmPassword)
        {

            var admin = db.M_Admin.SingleOrDefault(x => x.Admin_Email == Admin_Email);

            if (admin != null)
            {
               
                if (Admin_Password == ConfirmPassword)
                {
                    
                    admin.Admin_Password = Admin_Password;
                    db.SaveChanges();

                    ViewBag.Message = "Password updated successfully!";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Did not Proceed";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Email not found!";
                return View();
            }
        }
        public ActionResult Cart()
        {

            return View();
        }

        public ActionResult AddtoCart(int id)
        {
            List<M_Product> lst;
            if (Session["Cart"] == null)
            { lst = new List<M_Product>(); }
            else { lst = (List<M_Product>)Session["Cart"]; }

            lst.Add(db.M_Product.Where(p => p.Product_ID == id).FirstOrDefault());
            lst[lst.Count - 1].qty = 1;
            Session["Cart"] = lst;
            return RedirectToAction("Cart");
        }
        public ActionResult Minus(int RowNo)
        {
            List<M_Product> lst = (List<M_Product>)Session["Cart"];
            lst[RowNo].qty--;
            Session["Cart"] = lst;
            return RedirectToAction("Cart");
        }
        public ActionResult Plus(int RowNo)
        {
            List<M_Product> lst = (List<M_Product>)Session["Cart"];
            lst[RowNo].qty++;
            Session["Cart"] = lst;
            return RedirectToAction("Cart");
        }
        public ActionResult Trash(int RowNo)
        {
            List<M_Product> lst = (List<M_Product>)Session["Cart"];
            lst.RemoveAt(RowNo);
            Session["Cart"] = lst;
            return RedirectToAction("Cart");
        }

        public ActionResult Feedback()
        {

            return View();
        }

        public ActionResult shop(int? id)
        {
            shopmodel s = new shopmodel();
            s.cat = db.M_Category.ToList();
            if (id == null)
                s.prod = db.M_Product.ToList();
            else
                s.prod = db.M_Product.Where(p => p.Product_Category_FID == id).ToList();
            return View(s);
        }
        
    }
}