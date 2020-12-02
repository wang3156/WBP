using ShoppingPro2.Code.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingPro2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            UserInfo uf = (Session["UserInfo"] as UserInfo);
            
            if (uf!=null)
            {
                ViewBag.Logon = "true";
                ViewBag.Zh = uf.AccountNumber;
            }
            else
            {
                ViewBag.Logon = "false";
            }
            return View();
        }
    }
}