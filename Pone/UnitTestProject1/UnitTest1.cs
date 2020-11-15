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
            string col = NPOIHelper.GetColNameFromColIndex(2048);
            Console.WriteLine(col);
        }
    }
}
