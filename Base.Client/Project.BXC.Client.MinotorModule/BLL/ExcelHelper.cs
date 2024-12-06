using Microsoft.Win32;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace BXC.Client.MinotorModule.BLL
{

    public class ExcelHelper
    {
        /// <summary>
        /// 使用 MiniExcel 生成 Excel 文件
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">要写入的数据</param>
        public static void ExportToExcel<T>(List<T> data)
        {
            string filePath = GetSavePath();

            if (!string.IsNullOrEmpty(filePath))
            {
                // 使用 MiniExcel 生成 Excel 文件
                MiniExcel.SaveAs(filePath, data);
            }
        }

        /// <summary>
        /// 打开保存文件对话框以获取文件路径
        /// </summary>
        /// <returns>选择的文件保存路径</returns>
        private static string GetSavePath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "保存 Excel 文件",
                DefaultExt = "xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }

            return null;
        }
    }
}





