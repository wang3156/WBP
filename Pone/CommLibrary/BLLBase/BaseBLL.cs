using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.BLLBase
{
    using CommLibrary.DALBase;

    /// <summary>
    /// BLL基类.添加一些基础方法
    /// </summary>
    public class BaseBLL<T> where T : BaseDAL
    {
        protected T dal;
        /// <summary>
        /// 创建一个baseDAL对象.
        /// </summary>
        /// <param name="connStr">初始化对象时用的连接串.不传则使用配置信息</param>
        public BaseBLL(string connStr = "")
        {
            dal = Activator.CreateInstance(typeof(T), new string[] { connStr }) as T;
        }

        /// <summary>
        /// 释放Dal
        /// </summary>
        public void Dispose()
        {
            dal.Dispose();
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            dal.BeginTransaction();
        }
        /// <summary>
        /// 圆滚事务(未开事务不抛异常)
        /// </summary>
        public void Rollback()
        {
            dal.Rollback();
        }
        /// <summary>
        /// 提交事务(未开事务不抛异常)
        /// </summary>
        public void Commit()
        {
            dal.Commit();
        }
    }
}
