using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Entity;

namespace Base.Client.IBLL
{
    public interface IFileBLL
    {
        // 登录窗口打开的时候需要服务器更新列表
        Task<List<UpgradeFileModel>> GetServerFileList(string keyword = "");

        Task<List<UpgradeFileModel>> GetLocalFileList();

        void UploadFile(string file, string filePath, Action<int> prograssChanged, Action completed);

        void UploadAvatar(string file, string username, Action completed);
        void UploadSnapPhoto(string file, string username, Action completed);
    }
}
