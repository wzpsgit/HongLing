using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using System.Data;

namespace HongLingProject.Helper
{
    public class ExcelHelper
    {
        /// <summary>
        /// 读取整个Excel（所有Sheet）
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<DataTable> ReadWholeExcel(string filePath)
        {
            List<DataTable> lsDt= new List<DataTable>();
            Workbook wb = new Workbook(filePath);
            foreach (Worksheet sheet in wb.Worksheets)
            {
                DataTable exportDataTable = sheet.Cells.ExportDataTable(0, 0, sheet.Cells.MaxRow + 1, sheet.Cells.MaxColumn+1, true);
                lsDt.Add(exportDataTable);
            }
            return lsDt;
        }

        /// <summary>
        /// 读取Excel(第一个Sheet)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable RedExcel(string filePath)
        {
            var sheet = new Workbook(filePath).Worksheets[0];
            return sheet.Cells.ExportDataTable(0, 0, sheet.Cells.MaxRow + 1, sheet.Cells.MaxColumn + 1, true);
        }


    }
}
