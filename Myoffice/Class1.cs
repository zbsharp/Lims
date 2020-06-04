using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Myoffice
{
    public static class Class1
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dt"></param>
        public static void DatatoExcel(DataTable dt)
        {
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);
            //表头
            IRow rowHeader = sheet.CreateRow(0);
            rowHeader.Height = 20 * 20;//设置行高

            //设置字体
            IFont font = workbook.CreateFont();
            font.Color = IndexedColors.Black.Index;//字体颜色
            font.FontName = "宋体";
            font.IsBold = false;//是否加粗
            font.IsItalic = false;//是否斜体
            font.IsStrikeout = false;//是否加删除线

            ICellStyle style0 = workbook.CreateCellStyle();//创建ICellStyle
            style0.Alignment = HorizontalAlignment.Center;
            //填充前景颜色
            style0.FillForegroundColor = IndexedColors.Green.Index;
            style0.FillPattern = FillPattern.SolidForeground;
            style0.SetFont(font);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = rowHeader.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
                sheet.SetColumnWidth(i, 20 * 256);//设置列宽
                rowHeader.GetCell(i).CellStyle = style0;
            }
            //数据

            IFont font1 = workbook.CreateFont();
            font1.Color = IndexedColors.Black.Index;//字体颜色
            font1.FontName = "宋体";
            font1.IsBold = false;//是否加粗
            font1.IsItalic = false;//是否斜体
            font1.IsStrikeout = false;//是否加删除线
            ICellStyle style1 = workbook.CreateCellStyle();//创建ICellStyle
            style1.SetFont(font1);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow rowData = sheet.CreateRow(i + 1);
                rowData.Height = 20 * 20;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = rowData.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                    rowData.GetCell(j).CellStyle = style1;
                }
            }
            //转为字节
            MemoryStream memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            DownLoad(memoryStream);
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="fs"></param>
        private static void DownLoad(MemoryStream fs)
        {
            Random seed = new Random();
            Random randomNum = new Random(seed.Next());
            string file = randomNum.Next().ToString() + "开案数据.xls";//客户端保存文件名、以字符流的形式下载
            byte[] bytes = fs.ToArray();
            fs.Read(bytes, 0, bytes.Length);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
