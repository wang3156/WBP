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
        /// 从DataTable导出一个Excel 默认生成2007以上的
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dic">DataTable(key)和Excel(value)中列名对照 </param>
        ///<param name="FilePath">生成的文件保存路径</param>
        ///<param name="Is2007">是否生成2007或以上的Excel 如果FilePath有值并带文件名则以该文件类型为准 默认生成2007以上的</param>
        ///<param name="SheetName">需要生成的Sheet名.默认为 sheet1</param>
        ///<returns>如果有传文件路径则保存到路径返回文件名,否则返回生成的文件字节数组</returns>
        public static object ExportxlsxFromDataTable(DataTable dt, Dictionary<string, string> dic = null, string FilePath = "", bool Is2007 = true, string SheetName = "sheet1")
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

        /// <summary>
        /// 将Excel的内容读出成DataTable 
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="sheetName">读取的sheetName,默认为sheet1</param>       
        /// <returns></returns>
        public static DataTable GetDataTableFromExcel(string fileName, string sheetName = "sheet1")
        {
            IWorkbook wr;
            // IFormulaEvaluator ife;
            FileStream fi = new FileStream(fileName, FileMode.Open);
            if (fileName.EndsWith(".xlsx"))
            {
                wr = new XSSFWorkbook(fi);
                // ife = new XSSFFormulaEvaluator(wr);
            }
            else
            {
                wr = new HSSFWorkbook(fi);
                //  ife = new HSSFFormulaEvaluator(wr);
            }


            DataTable dt = new DataTable();

            // int rowIndex = 0, cellIndex = 0;
            ISheet sh = wr.GetSheet(sheetName);


            IRow row = sh.GetRow(0);
            ICell cell;
            DataRow nrow;

            int lastCellNum = row.LastCellNum;
            for (int i = 0; i < lastCellNum; i++)
            {
                dt.Columns.Add(row.Cells[i].StringCellValue);
            }


            try
            {
                var igrow = sh.GetRowEnumerator();
                igrow.MoveNext();//跳过首行
                while (igrow.MoveNext())
                {
                    row = igrow.Current as IRow;
                    nrow = dt.NewRow();
                    dt.Rows.Add(nrow);
                    for (int i = 0; i < lastCellNum; i++)
                    {
                        cell = row.Cells[i];

                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                nrow[i] = DBNull.Value;
                                break;
                            case CellType.Boolean:
                                nrow[i] = cell.BooleanCellValue;
                                break;
                            case CellType.Numeric:
                                //short format = cell.CellStyle.DataFormat;
                                //if (format == 14 || format == 31 || format == 57 || format == 58)
                                if (DateUtil.IsCellDateFormatted(cell))
                                    nrow[i] = cell.DateCellValue;
                                else
                                    nrow[i] = cell.NumericCellValue;
                                break;

                            case CellType.String:
                                nrow[i] = cell.StringCellValue;
                                break;
                            case CellType.Error:
                                nrow[i] = cell.ErrorCellValue;
                                break;
                            case CellType.Formula:
                                //计算公式的内容   
                                //ife.EvaluateInCell(cell)
                                nrow[i] = GetFormulaCellValue(cell);
                                break;
                            default:
                                nrow[i] = "=" + cell.CellFormula;
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            finally
            {
                wr.Close();
                fi.Dispose();
            }

            return dt;
        }
        /// <summary>
        /// 获取公式列的值 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        static object GetFormulaCellValue(ICell cell)
        {

            switch (cell.CachedFormulaResultType)
            {

                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue;
                    else
                        return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Unknown:
                case CellType.Blank:
                    return DBNull.Value;
                default:
                    return DBNull.Value;
            }
        }

    }


}
