using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IDAL;

namespace Base.Client.BLL
{
    public class FileBLL : IFileBLL
    {
        IFileDal _fileDAL;
        public FileBLL(IFileDal fileDAL)
        {
            _fileDAL = fileDAL;
        }

        public async Task<List<UpgradeFileModel>> GetLocalFileList()
        {
            DataTable datas = await _fileDAL.GetLocalFileList();

            return datas.AsEnumerable().Select(row => new UpgradeFileModel
            {
                FileName = row["file_name"].ToString(),
                FileMd5 = row["file_md5"].ToString(),
                Length = row["file_len"] == null ? 0 : int.Parse(row["file_len"].ToString())
            }).ToList();
        }

        public async Task<List<UpgradeFileModel>> GetServerFileList(string keyword = "")
        {
            string fileListJson = await _fileDAL.GetServerFileList(keyword);

            var result = System.Text.Json.JsonSerializer.Deserialize<ResultEntity<List<UpgradeFileModel>>>(fileListJson);

            if (result.state == 200)
            {
                return result.data;
            }

            throw new Exception(result.exceptionMessage);
        }

        public void UploadFile(string file, string filePath, Action<int> prograssChanged, Action completed)
        {
            _fileDAL.UploadFile(file, filePath, prograssChanged, completed);
        }

        public void UploadAvatar(string file, string username, Action completed)
        {
            _fileDAL.UploadAvatar(file, username, completed);
        }

        public void UploadSnapPhoto(string file, string fileName, Action completed)
        {
            _fileDAL.UploadSnapPhoto(file, fileName, completed);
        }
    }
}
