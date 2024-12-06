using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    public class FileDal : IFileDal
    {
        IWebDataAccess _webDataAccess;
        ILocalDataAccess _localDataAccess;
        public FileDal(IWebDataAccess webDataAccess, ILocalDataAccess localDataAccess)
        {
            _webDataAccess = webDataAccess;
            _localDataAccess = localDataAccess;
        }

        public async Task<DataTable> GetLocalFileList()
        {
            // EFCore  ：本地数据库（缓存-数据量少）
            //System.Text.Json.JsonSerializer.Serialize(new { "", "" });
            return _localDataAccess.GetFileList();
        }

        public Task<string> GetServerFileList(string keyword)
        {
            string uri = "/api/File/list";
            if (!string.IsNullOrEmpty(keyword))
                uri += "?keyword=" + keyword;

            return _webDataAccess.GetDatas(uri);
        }

        public void UploadFile(string file, string filePath, Action<int> prograssChanged, Action completed)
        {
            string uri = "/api/file/upload";

            Dictionary<string, object> datas = new Dictionary<string, object>();
            datas.Add("md5", GetFileMd5(file));
            datas.Add("file_path", filePath);

            _webDataAccess.Upload(uri, file, prograssChanged, completed, datas);
        }

        public void UploadAvatar(string file, string username, Action completed)
        {
            string uri = "/api/file/u_image";

            Dictionary<string, object> datas = new Dictionary<string, object>();
            datas.Add("username", username);

            _webDataAccess.Upload(uri, file, null, completed, datas);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName">文件名称.扩展名</param>
        /// <param name="completed"></param>
        public void UploadSnapPhoto(string file, string fileName, Action completed)
        {
            string uri = "/api/file/u_snap";

            Dictionary<string, object> datas = new Dictionary<string, object>();
            datas.Add("filename", fileName);

            _webDataAccess.Upload(uri, file, null, completed, datas);
        }

        private string GetFileMd5(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new Exception("文件不存在");

                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetFileMd5() fail,error:" + ex.ToString());
            }
        }

        // 图片文本的读取   保存服务端    URL地址进行访问   
        // http://localhost:22643/api/file/1231231433432.jpg
        // http://localhost:22643/api/file/1231231433432.mp3

    }
}
