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
    using Newtonsoft.Json;
    using System.Data;
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

        public ActionResult onSearch(string SPName)
        {
            DataTable dt = null;
            ResultEntity re = new ResultEntity();

            using (DbProcess ub = new DbProcess())
            {
                dt = ub.GetData<DataTable>("Select * From SPListDataTable where SPName like @SPName", new SqlParameter("@SPName", $"%{SPName}%"));
            }
            re.Data = dt;
            return Json(re);
        }

        public ActionResult onSaveData(string entity)
        {
            System.Diagnostics.Debugger.Launch();
            ResultEntity re = new ResultEntity();
            Dictionary<string, string> en = JsonConvert.DeserializeObject<Dictionary<string, string>>(entity);
            List<string> sql_li = new List<string>();
            List<string> sql_liI = new List<string>();
            List<SqlParameter> pars = new List<SqlParameter>();
            string par = "", sql = "";
            int spid = Convert.ToInt32(en["SPID"]);
            en.Remove("SPID");
            en.Add("UpdateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            int i = 0;
            if (spid == 0)
            {
                sql = "Insert into SPListDataTable";
            }
            else
            {
                sql = "Update SPListDataTable set ";
            }
            foreach (var item in en)
            {
                if (spid == 0)
                {
                    sql_liI.Add(item.Key);
                    sql_li.Add((par = "@par" + i));
                }
                else
                {
                    sql_li.Add(item.Key + "=" + (par = "@par" + i));
                }
                pars.Add(new SqlParameter(par, item.Value));
                i++;
            }
            if (spid == 0)
            {
                sql += $"({string.Join(",", sql_liI)})" + " Values(" + string.Join(",", sql_li) + ") ;select SCOPE_IDENTITY();";
            }
            else
            {
                sql += string.Join(",", sql_li) + " where SPID=" + spid + " ; select " + spid;
            }
            DataRow dr = null;
            using (DbProcess ub = new DbProcess())
            {

                dr = ub.GetData<DataRow>(sql, pars.ToArray());
            }
            re.Data = dr[0];
            return Json(re);
        }

    }


}