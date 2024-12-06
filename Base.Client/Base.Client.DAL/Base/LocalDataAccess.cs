using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.IDAL;

namespace Base.Client.DAL
{
    public class LocalDataAccess : ILocalDataAccess
    {
        SQLiteConnection conn = null;
        SQLiteCommand comm = null;
        SQLiteDataAdapter adapter = null;
        SQLiteTransaction trans = null;

        /// <summary>
        /// 释放所有数据库操作对象 
        /// </summary>
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

        // 创建数据连接对象
        private bool Connection()
        {
            try
            {
                if (conn == null)
                    conn = new SQLiteConnection("data source=data.db3");

                conn.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetFileList()
        {
            // 操作数据库
            if (this.Connection())
            {
                try
                {
                    string sql = "select file_name,file_md5,file_len from file_version";
                    adapter = new SQLiteDataAdapter(sql, conn);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;//.AsEnumerable().Select(d => new string[] { d.Field<string>("file_name"), d.Field<string>("file_md5") }).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Dispose();
                }
            }

            return null;
        }

        public DataTable GetDeivceList()
        {
            if (this.Connection())
            {
                try
                {
                    string sql = "select * from device_info";
                    adapter = new SQLiteDataAdapter(sql, conn);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    this.Dispose();
                }
            }

            return null;
        }
    }
}
