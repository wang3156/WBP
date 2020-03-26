using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

namespace WebFramework1.Areas.Admin.Controllers.UserManagerView
{
    public class UserManagerController : Controller
    {
        // GET: Admin/UserManager/UserInfo
        public ActionResult UserInfo()
        {
            return View();
        }

        public ActionResult GetUserInfo(ExpandoObject where) {
            return Content("");
        }

        // GET: Admin/UserManager/PermissionInfo
        public ActionResult PermissionInfo()
        {
            return View();
        }
    }
}