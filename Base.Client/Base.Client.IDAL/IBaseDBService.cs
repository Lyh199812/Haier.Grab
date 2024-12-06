using Base.Client.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.IDAL
{
    public interface IBaseDBService : IDisposable
    {
        OperateResult Commit();

        OperateResult Delete<T>(int Id) where T : class;

        OperateResult Delete<T>(T entity) where T : class;

        OperateResult Delete<T>(IEnumerable<T> entities) where T : class;

        T Find<T>(int id) where T : class;

        OperateResult Insert<T>(T entity) where T : class;

        OperateResult Insert<T>(IEnumerable<T> entities) where T : class;

        IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class;

        OperateResult Update<T>(T entity) where T : class;

        OperateResult Update<T>(IEnumerable<T> entities) where T : class;
    }
}
