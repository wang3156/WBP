using CommLibrary.MVC.Controllers;
using ShoppingPro2.Code.ConnDB;
using ShoppingPro2.Code.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingPro2.Controllers
{
    using System.Data.SqlClient;
    public class AdminHomeController : CutBaseController
    {
        // GET: AdminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string UserID, string PassWord)
        {
            UserInfo uif = null;
            using (DbProcess ub = new DbProcess())
            {
                Session["UserInfo"] = uif = ub.GetData<UserInfo>(@"select * From UserInfo where AccountNumber=@an and Password=@pw", new SqlParameter("@an", UserID), new SqlParameter("@pw", PassWord));
            }
            ResultEntity re = new ResultEntity();
            if (uif == null)
            {
                re.Success = false;
                re.Msg = "输入的用户名或密码不正确!";
            }
            else
            {
                re.Success = true;
                re.Data = uif;
            }

            return Json(re);
        }

        public ActionResult MainPage()
        {
            return View();
        }

    }


}