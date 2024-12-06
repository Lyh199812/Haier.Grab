using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Upgrade.DataAccess
{
    public class LocalDataAccess
    {
        SQLiteConnection conn = null;
        SQLiteCommand comm = null;
        SQLiteDataAdapter adapter = null;
        SQLiteTransaction trans = null;

        private void Dispose()
        {
            if (trans != null)
            {
                trans.Rollback();
                trans.Dispose();
                trans = null;
            }
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        private bool Connection()
        {
            try
            {
                if (conn == null)
                    conn = new SQLiteConnection("data source=data.db3");

                conn.Open();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateFileInfo(string fileName, string fileMd5, int fileLen)
        {
            try
            {
                if (!this.Connection()) throw new Exception("创建本地缓存连接出现异常");

                string sql = $"update file_version set file_md5 = '{fileMd5}',file_len={fileLen} where file_name='{fileName}'";
                comm = new SQLiteCommand(sql, conn);
                int count = comm.ExecuteNonQuery();// 先做更新，返回结果表示影响行数
                if (count == 0)
                {
                    // 如果没有修改，只能做新增
                    comm.CommandText = $"insert into file_version(file_name,file_md5,file_len) values('{fileName}','{fileMd5}',{fileLen})";
                    count = comm.ExecuteNonQuery();
                    if (count == 0)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                this.Dispose();
            }
        }
    }
}
