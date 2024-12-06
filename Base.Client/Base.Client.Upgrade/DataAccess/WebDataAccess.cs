using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Upgrade.Models;

namespace Base.Client.Upgrade.DataAccess
{
    public class WebDataAccess
    {
        public Action<int> DownloadPrograssChanged;
        public Action DownloadCompleted;

        WebClient webClient = new WebClient();
        public async void DownloadAsync(FileModel fileModel, string localFileName)
        {
            // HttpWebRequest   相对来更底层
            webClient.DownloadProgressChanged += (se, ev) =>
            {
                    //ev.BytesReceived 
                    fileModel.Progress = ev.ProgressPercentage;

                DownloadPrograssChanged?.Invoke(ev.ProgressPercentage);
            };
            webClient.DownloadFileCompleted += (se, ev) =>
            {
                DownloadCompleted?.Invoke();
            };
            // 下载的网络文件地址；这个文件要保存在本地的哪个目录
            webClient.DownloadFileAsync(new Uri("http://localhost:22643/api/file/download/" + fileModel.FileName), localFileName);
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //e.BytesReceived    :   接收的字节数
            //e.ProgressPercentage    :  接收的百分比


        }
    }
}
