using CommLibrary.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class StudentB : IDisposable
    {
        SqlServerDBHelper db = new SqlServerDBHelper();

        public void BeginTransaction()
        {
            db.BeginTransaction();
        }

        public void Commit()
        {
            db.Commit();
        }
        public void Rollback()
        {
            db.Rollback();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void SaveData(DataTable dt)
        {
            DataRow row = dt.Rows[0];
            BeginTransaction();
            try
            {
                db.ExecuteNonQuery($"Delete E_StudentAnswer Where EID={row["EID"]} and PID={row["PID"]} and ZKZH='{row["ZKZH"]}'");
                db.BulkCopyToDB(dt, "E_StudentAnswer");
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
            }

        }

        public DataTable GetKSAnswer(int EID, int PID, string ZKZH)
        {
            return db.GetDataSet($"Select [EID],[PID],[ZKZH],[Answers],[QID],[QType] From E_StudentAnswer Where EID={EID} and PID={PID} and ZKZH='{ZKZH}'").Tables[0];
        }
    }
}
