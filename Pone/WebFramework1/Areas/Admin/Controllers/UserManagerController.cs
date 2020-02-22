using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFramework1.Areas.Admin.Controllers.UserManagerView
{
    public class UserManagerController : Controller
    {
        // GET: Admin/UserManager/UserInfo
        public ActionResult UserInfo()
        {
            return View();
        }

        // GET: Admin/UserManager/PermissionInfo
        public ActionResult PermissionInfo()
        {
            return View();
        }
    }
}