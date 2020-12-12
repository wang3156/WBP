using Newtonsoft.Json;
using ShoppingPro2.Code.ConnDB;
using ShoppingPro2.Code.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingPro2.Controllers
{
    using System.Data;
    using System.Data.SqlClient;
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrder(string entity)
        {
            ResultEntity re = new ResultEntity();
            UserInfo uf = (Session["UserInfo"] as UserInfo);
            if (uf == null)
            {
                re.Msg = "未登录或登录失效!";
                return Json(re);
            }


            Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(entity);
            int OrderId = Convert.ToInt32(dic["OrderId"]);
            List<string> spid = new List<string>();
            using (DbProcess ub = new DbProcess())
            {
                if (OrderId == 0)
                {

                    #region 添加 订单
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"]);
                    ub.BeginTransaction();
                    try
                    {
                        string name;
                        foreach (DataRow item in dt.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(name = ub.ExecuteScalarSql<string>($@"if exists(select  1 From [dbo].[SPListDataTable] where SPID={item["SPID"]} and  (SPStock-{item["Qty"]})>=0) begin update SPListDataTable set SPStock=SPStock-{item["Qty"]} where SPID={item["SPID"]}; select '' end else   select [SPName]  From [dbo].[SPListDataTable] where SPID={item["SPID"]}  ")))
                            {
                                spid.Add(name);
                            }
                        }
                        if (spid.Count > 0)
                        {

                            re.Msg = string.Join(",", spid);
                            ub.Rollback();

                        }
                        else
                        {
                            int orderid = ub.ExecuteScalarSql<int>("Insert into [dbo].[OrderTable]([OrderAddress],[OrderPhone],[Discount],[UID],OrderUser) values(@p1,@p2,@p3,@p4,@p5) ;select SCOPE_IDENTITY() ; ", new SqlParameter("p1", dic["OrderAddress"]), new SqlParameter("p2", dic["OrderPhone"]), new SqlParameter("p3", dic["Discount"]), new SqlParameter("p4", uf.UID), new SqlParameter("p5", dic["OrderUser"]));

                            dt.Columns.Add(new DataColumn("OrderId", typeof(int)));
                            foreach (DataRow item in dt.Rows)
                            {
                                item["OrderId"] = orderid;
                            }
                            ub.BulkCopyToDB(dt, "OrderDetail");
                            ub.Commit();

                        }



                    }
                    catch (Exception ex)
                    {
                        re.Success = false;
                        re.Msg = ex.Message;
                        ub.Rollback();

                    }
                    #endregion
                }
                else
                {
                    ub.ExecSql("update [dbo].[OrderTable] set [OrderUser]=@p1,[OrderAddress]=@p2,[OrderPhone]=@p3  where OrderId=" + OrderId, new SqlParameter("@p1", dic["OrderUser"]), new SqlParameter("@p2", dic["OrderAddress"]), new SqlParameter("@p3", dic["OrderPhone"]));
                }
            }
            return Json(re);

        }

        public ActionResult GetMyOrder()
        {
            ResultEntity re = new ResultEntity();
            UserInfo uf = (Session["UserInfo"] as UserInfo);
            if (uf == null)
            {
                re.Msg = "未登录或登录失效!";
                return Json(re);
            }
            using (DbProcess ub = new DbProcess())
            {
                DataSet ds = ub.GetData<DataSet>($"select * From OrderTable where UID={uf.UID} and [OrderStatus]!=3 order by [OrderId]; select c.*,a.Qty as QCount,a.OrderId,a.DId From  [dbo].[OrderDetail] a ,[dbo].[SPListDataTable] c where exists(select 1 From OrderTable b where b.UID={uf.UID} and b.[OrderStatus]!=3 and b.[OrderId]=a.[OrderId] ) and c.SPID=a.SPID order by [OrderId],[DId]");
                ds.Tables[0].TableName = "OrderTable";
                ds.Tables[1].TableName = "OrderDetail";
                re.Data = ds;
            }

            return Content(JsonConvert.SerializeObject(re));

        }

        public ActionResult CancelOrder(int oid)
        {

            ResultEntity re = new ResultEntity();
            UserInfo uf = (Session["UserInfo"] as UserInfo);
            if (uf == null)
            {
                re.Msg = "未登录或登录失效!";
                return Json(re);
            }
            using (DbProcess ub = new DbProcess())
            {
                ub.ExecSql($@"
if exists(select 1 From OrderTable where OrderId={oid} and OrderStatus=3)
  return
update a set a.SPStock=a.SPStock+b.Qty From [dbo].[SPListDataTable] a,[dbo].[OrderDetail] b where b.OrderId={oid} and a.SPID=b.SPID
update OrderTable set OrderStatus=3 where OrderId={oid} ");

            }

            return Json(re);

        }

        public ActionResult RemoveSP(int DId)
        {
            ResultEntity re = new ResultEntity();
            UserInfo uf = (Session["UserInfo"] as UserInfo);
            if (uf == null)
            {
                re.Msg = "未登录或登录失效!";
                return Json(re);
            }
            using (DbProcess ub = new DbProcess())
            {
                ub.ExecSql($@"if exists(select 1 From OrderTable a where exists(select 1 From OrderDetail b where b.DId={DId} and b.OrderId=a.OrderId) and a.OrderStatus=3) or not exists(select 1 From OrderDetail where DId={DId})
   begin
	select N'订单数据异常,请刷新页面后再试!'
	return
   end

update a set a.SPStock=a.SPStock+b.Qty From SPListDataTable a,OrderDetail b where b.DId={DId} and a.SPID=b.SPID 
declare @oid int
select @oid=OrderId From OrderDetail where DId={DId}
delete OrderDetail   where DId={DId}
if not exists(select 1 From OrderDetail where OrderId=@oid)
   delete   OrderTable where OrderId=@oid");
            }
            return Json(re);
        }
    }
}