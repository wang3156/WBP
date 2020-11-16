using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommLibrary.OfficeHelper.Excel;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var n = NPOIHelper.GetPositionByExcelCol("BD1");
            string col = NPOIHelper.GetColNameByColIndex(25);
            Console.WriteLine(col);
        }
    }
}
