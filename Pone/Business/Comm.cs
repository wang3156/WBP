using CommLibrary.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Comm
    {
        static SqlServerDBHelper db = new SqlServerDBHelper();
        /// <summary>
        /// 获取选择题选项
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCSelectOpetion(int QID)
        {
            return db.GetDataSet("select * From [dbo].[E_CSelectQuestions] where QID=" + QID).Tables[0];
        }
    }
}
