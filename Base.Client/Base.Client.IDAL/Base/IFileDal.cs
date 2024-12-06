using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IFileDal
    {
        Task<string> GetServerFileList(string keyword);
        Task<DataTable> GetLocalFileList();


        void UploadFile(string file, string filePath,  Action<int> prograssChanged, Action completed);
        void UploadAvatar(string file, string username, Action completed);
        void UploadSnapPhoto(string file, string fileName, Action completed);
    }
}
