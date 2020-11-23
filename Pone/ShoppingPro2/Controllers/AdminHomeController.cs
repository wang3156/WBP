using CommLibrary.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingPro2.Controllers
{
    public class AdminHomeController : CutBaseController
    {
        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}