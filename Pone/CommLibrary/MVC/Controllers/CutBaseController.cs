using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommLibrary.MVC.Controllers
{
    /// <summary>
    /// 控制器的父类
    /// </summary>
    public class CutBaseController : Controller
    {
        public ActionResult Json(object o)
        {

            return Content(JsonConvert.SerializeObject(o));
        }
    }


}
