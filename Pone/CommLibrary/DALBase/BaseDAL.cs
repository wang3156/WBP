using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.DALBase
{
    using CommLibrary.DBHelper.BaseClass;
    /// <summary>
    /// DAL基类.添加一些基础方法
    /// </summary>
    public class BaseDAL : IDisposable
    {
        BaseDBHelper db = new BaseDBHelper();

        /// <summary>
        /// 释放使用的实例 
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            db.BeginTransaction();
        }
        /// <summary>
        /// 圆滚事务(未开事务不抛异常)
        /// </summary>
        public void Rollback()
        {
            db.Rollback();
        }
        /// <summary>
        /// 提交事务(未开事务不抛异常)
        /// </summary>
        public void Commit()
        {
            db.Commit();
        }

    }
}
