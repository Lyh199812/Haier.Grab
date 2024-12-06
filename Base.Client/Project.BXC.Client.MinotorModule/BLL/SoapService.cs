using Base.Client.Common;
using BXC.Client.MinotorModule.Models;
using Device.DataConvertLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace BXC.Client.MinotorModule.BLL
{
    public class SoapService
    {

        public static  OperateResult UpdateMes(string soapRequest)
        {
            string officeUrl = "http://10.27.80.183:8080/N2/services/DeviceAutoWork";
            string productionUrl = "http://192.168.250.183:8080/N2/services/DeviceAutoWork";
            string url = productionUrl; // 可以根据需要切换到 productionUrl
            string soapAction = "http://tempuri.org/UPDATE_WEIGH";
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(soapRequest, Encoding.UTF8, "text/xml")
            };

            // 设置SOAPAction头
            request.Headers.Add("SOAPAction", soapAction);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 发送请求并等待响应
                    HttpResponseMessage response = client.Send(request);
                    string result = response.Content.ReadAsStringAsync().Result;
                    string formattedResult = Regex.Replace(result, "(>)(<)", "$1\n$2");
                    //return OperateResult.CreateSuccessResult(result);
                    return new OperateResult() { IsSuccess=true,Message = formattedResult }; 
                }
              
            }
            catch (TaskCanceledException)
            {
                // 超时或任务取消
                return OperateResult.CreateFailResult("Request timed out.");
            }
            catch (Exception ex)
            {
                // 处理其他异常
                Console.WriteLine("Error occurred:");
                return OperateResult.CreateFailResult(ex.ToString());

            }
        }
    }

}
