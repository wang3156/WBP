using System;
using System.Linq;
using System.Reflection;


namespace CommLibrary.OfficeHelper.Excel
{
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;

    /// <summary>
    /// 通过NPOI操作Excel的帮助类
    /// </summary>
    public class NPOIHelper
    {
        /// <summary>
        /// Excel生成或获取数据的SheetName,默认为 sheet1
        /// </summary>
        public string SheetName = "sheet1";
        //public string FileName { get; set; }

        //public List<T> DataList { get; set; }

        //public List<string> Nofileds { get; set; } = new List<string>();

        //public List<string> TitleRow { get; set; }

        //public override void ExecuteResult(ControllerContext context)
        //{
        //    if (DataList == null)
        //    {
        //        throw new InvalidDataException("DataList");
        //    }
        //    if (string.IsNullOrWhiteSpace(this.SheetName))
        //    {
        //        this.SheetName = "Sheet1";
        //    }
        //    if (string.IsNullOrWhiteSpace(this.FileName))
        //    {
        //        this.FileName = string.Concat(
        //            "ExportData_",
        //            DateTime.Now.ToString("yyyyMMddHHmmss"),
        //            ".xls");
        //    }

        //    this.ExportExcelEventHandler(context);
        //}

        ///// <summary>
        ///// Exports the excel event handler.
        ///// </summary>
        ///// <param name="context">The context.</param>
        //private void ExportExcelEventHandler(ControllerContext context)
        //{
        //    try
        //    {
        //        string fileExt = Path.GetExtension(FileName).ToLower();
        //        if (fileExt == ".xlsx")
        //            Exportxlsx(context);
        //        else
        //            Exportxls(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// 从DataTable导出一个Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dic">DataTable(key)和Excel(value)中列名对照 </param>
        ///<param name="FilePath">生成的文件保存路径</param>
        ///<param name="Is2007">是否生成2007或以上的Excel 如果FilePath有值并带文件名则以该文件类型为准</param>
        ///<returns>如果有传文件路径则保存到路径返回文件名,否则返回生成的文件字节数组</returns>
        private object ExportxlsxFromDataTable(DataTable dt, Dictionary<string, string> dic = null, string FilePath = "", bool Is2007 = true)
        {
            if (dt.Rows.Count == 0)
            {
                throw new Exception("没有需要生成的数据!!");
            }

            #region 确认生成文件类型. 如果有路径 没文件名则需要生成文件名
            string fileExt = ".xlsx";
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                if (!Is2007)
                {
                    fileExt = ".xls";
                }
            }
            else if (string.IsNullOrWhiteSpace(fileExt = Path.GetExtension(FilePath)))
            {
                fileExt = ".xlsx";
                FilePath = Path.Combine(FilePath, Guid.NewGuid().ToString("n") + fileExt);
            }

            string[] EType = new string[] { ".xlsx", ".xls" };
            if (!EType.Contains(fileExt))
            {
                throw new Exception("文件类型不被支持!!");
            }
            #endregion


            IWorkbook workbook = new HSSFWorkbook();
            if (fileExt == ".xlsx")
                workbook = new XSSFWorkbook();

            int col_index = 0;
            int row_index = 0;
            ISheet sheet = workbook.CreateSheet(SheetName);
            IRow row = sheet.CreateRow(row_index);
            row_index++;
            ICell cell;

            #region 生成第一行表头信息
            if (dic != null)
            {
                foreach (var col in dic)
                {
                    cell = row.CreateCell(col_index);
                    cell.SetCellValue(col.Value);
                    col_index++;
                }

            }
            else
            {
                dic = new Dictionary<string, string>();
                foreach (DataColumn col in dt.Columns)
                {
                    cell = row.CreateCell(col_index);
                    cell.SetCellValue(col.ColumnName);
                    col_index++;
                    dic.Add(col.ColumnName, col.ColumnName);
                }
            }
            #endregion

            //生成数据
            dt.AsEnumerable().ToList().ForEach(c =>
            {
                col_index = 0;
                row = sheet.CreateRow(row_index);

                foreach (var item in dic)
                {
                    cell = row.CreateCell(col_index);
                    cell.SetCellValue(Convert.ToString(c[item.Key]));
                    col_index++;
                }
                row_index++;
            });

            //设置单元格自动列宽(未实用过不知道有没效 2020年3月10日16:57:23 mw )
            col_index = 0;
            foreach (var item in dic)
            {
                sheet.AutoSizeColumn(col_index);
                col_index++;
            }

            #region 返回数据
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                Byte[] bys = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);
                    bys = ms.ToArray();
                }
                workbook.Dispose();
                return bys;
            }
            else
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Create))
                {
                    workbook.Write(fs);
                }
                return Path.GetFileName(FilePath);
            }
            #endregion


        }

        //private void Exportxls(ControllerContext context)
        //{
        //    if (DataList == null)
        //    {
        //        return;
        //    }
        //    HSSFWorkbook workbook = new HSSFWorkbook();
        //    ISheet sheet = workbook.CreateSheet(SheetName);
        //    if (DataList != null)
        //    {
        //        context.HttpContext.Response.Clear();

        //        // 编码
        //        context.HttpContext.Response.ContentEncoding = Encoding.UTF8;

        //        // 设置网页ContentType
        //        context.HttpContext.Response.ContentType =
        //            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //        // 导出名字
        //        var browser = context.HttpContext.Request.Browser.Browser;
        //        var exportFileName = browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase)
        //            ? FileName
        //            : Uri.EscapeDataString(FileName);

        //        context.HttpContext.Response.AddHeader(
        //            "Content-Disposition",
        //            string.Format("attachment;filename={0}", exportFileName));

        //        //第一行
        //        IRow row = sheet.CreateRow(0);
        //        int i = 0;
        //        //数据行
        //        int j = 1;//rows
        //        int cells = 0; //cells

        //        IEnumerable<PropertyInfo> modelName = DataList[0].GetType().GetProperties().Where(a => a.Name != "Id");

        //        if (Nofileds.Count > 0)
        //        {
        //            if (TitleRow == null)
        //            {
        //                foreach (PropertyInfo propertyInfo in modelName)
        //                {
        //                    if (!Nofileds.Contains(propertyInfo.Name))
        //                    {
        //                        row.CreateCell(i).SetCellValue(propertyInfo.Name.Replace("ForShow", ""));
        //                        i++;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                foreach (string title in TitleRow)
        //                {
        //                    row.CreateCell(i).SetCellValue(title);
        //                    i++;
        //                }
        //            }


        //            foreach (var item in DataList)
        //            {
        //                cells = 0;
        //                IRow rows = sheet.CreateRow(j);
        //                foreach (PropertyInfo Info in modelName)
        //                {
        //                    if (!Nofileds.Contains(Info.Name))
        //                    {
        //                        rows.CreateCell(cells).SetCellValue(Info.GetValue(item) == null ? "" : Info.GetValue(item).ToString());
        //                        cells++;
        //                    }

        //                }
        //                j++;
        //            }

        //        }
        //        else
        //        {
        //            if (TitleRow == null)
        //            {
        //                foreach (PropertyInfo propertyInfo in modelName)
        //                {
        //                    row.CreateCell(i).SetCellValue(propertyInfo.Name);
        //                    i++;
        //                }
        //            }
        //            else
        //            {
        //                foreach (string title in TitleRow)
        //                {
        //                    row.CreateCell(i).SetCellValue(title);
        //                    i++;
        //                }
        //            }


        //            foreach (var item in DataList)
        //            {
        //                cells = 0;
        //                IRow rows = sheet.CreateRow(j);
        //                foreach (PropertyInfo Info in modelName)
        //                {

        //                    rows.CreateCell(cells).SetCellValue(Info.GetValue(item) == null ? "" : Info.GetValue(item).ToString());
        //                    cells++;
        //                }
        //                j++;
        //            }
        //        }

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            workbook.Write(ms);
        //            ms.WriteTo(context.HttpContext.Response.OutputStream);
        //            ms.Close();
        //        }
        //    }
        //}
    }


}
