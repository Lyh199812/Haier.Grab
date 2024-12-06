using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.Common;
using Base.Client.Entity;
using ClosedXML.Excel;

namespace Base.Client.Tools
{
    public class ExcelHelper<T> where T : class, new()
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);  // 用于文件锁定


        // 异步写入泛型列表到 Excel，返回操作结果
        public static async Task<OperateResult> WriteLogAsync(IEnumerable<T> logData,string _filePath)
        {
            await _semaphore.WaitAsync();  // 加锁
            try
            {
                bool fileExists = File.Exists(_filePath);

                using (var workbook = fileExists ? new XLWorkbook(_filePath) : new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault(w => w.Name == "Logs") ?? workbook.Worksheets.Add("Logs");
                    var properties = typeof(T).GetProperties();

                    // 如果文件不存在或是新建的文件，创建表头
                    if (!fileExists || worksheet.LastRowUsed() == null)
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = properties[i].Name;
                        }
                    }

                    // 查找下一行用于数据写入
                    int startRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 2;

                    foreach (var item in logData)
                    {
                        for (int col = 0; col < properties.Length; col++)
                        {
                            var value = properties[col].GetValue(item);
                            worksheet.Cell(startRow, col + 1).Value = value?.ToString() ?? "";
                        }
                        startRow++;
                    }

                    // 异步保存文件
                    await Task.Run(() => workbook.SaveAs(_filePath));
                }

                return OperateResult.CreateSuccessResult(); // 操作成功
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult($"写入日志时出错：{ex.ToString()}");

            }
            finally
            {
                _semaphore.Release();  // 释放锁
            }
        }

        // 从 Excel 读取数据到泛型列表（可以保留同步版本）
        public static OperateResult<List<T>> ReadLogs(string _filePath)
        {
            List<T> logData = new List<T>();

            if (!File.Exists(_filePath))
                return new OperateResult<List<T>> { IsSuccess = false, Message = $"该路径下文件不存在，路径:{_filePath}" };
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet("Logs");
                var properties = typeof(T).GetProperties();

                // 获取表头，确保属性名和列对应
                var headers = worksheet.Row(1).CellsUsed().Select(cell => cell.GetString()).ToList();

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var obj = new T();
                    foreach (var prop in properties)
                    {
                        int colIndex = headers.IndexOf(prop.Name) + 1;
                        if (colIndex > 0)
                        {
                            var cellValue = row.Cell(colIndex).GetString();
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                // 将单元格数据转换为对应属性的数据类型
                                var convertedValue = Convert.ChangeType(cellValue, prop.PropertyType);
                                prop.SetValue(obj, convertedValue);
                            }
                        }
                    }
                    logData.Add(obj);
                }
            }

            return new OperateResult<List<T>> { IsSuccess = true, Content = logData };
        }

    }
}
   
