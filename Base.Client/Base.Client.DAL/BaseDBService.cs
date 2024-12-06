using Base.Client.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.DAL
{
    public class BaseDBService 
    {
        public DbContext dbContext { get; set; }

        public BaseDBService(DbContext idbConfig)
        {
            dbContext = idbConfig;
        }

        public OperateResult Commit()
        {
            try
            {
                dbContext.SaveChanges();
                return new OperateResult { IsSuccess = true, Message = "提交成功" };
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10001 };
            }
        }

        public OperateResult Delete<T>(int Id) where T : class
        {
            try
            {
                T entity = this.Find<T>(Id);
                if (entity == null)
                    return new OperateResult { IsSuccess = false, Message = "实体不存在", ErrorCode = 10002 };

                dbContext.Set<T>().Remove(entity);
                return Commit();
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10003 };
            }
        }

        public OperateResult Delete<T>(T entity) where T : class
        {
            try
            {
                if (entity == null)
                    return new OperateResult { IsSuccess = false, Message = "实体为空", ErrorCode = 10004 };

                dbContext.Set<T>().Attach(entity);
                dbContext.Set<T>().Remove(entity);
                return Commit();
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10005 };
            }
        }

        public OperateResult Delete<T>(IEnumerable<T> entities) where T : class
        {
            try
            {
                foreach (var entity in entities)
                {
                    dbContext.Set<T>().Attach(entity);
                }
                dbContext.Set<T>().RemoveRange(entities);
                return Commit();
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10006 };
            }
        }

        public T Find<T>(int id) where T : class
        {
            return dbContext.Set<T>().Find(id);
        }

        public OperateResult Insert<T>(T entity) where T : class
        {
            try
            {
                dbContext.Set<T>().Add(entity);
                return Commit();
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10007 };
            }
        }

        public OperateResult Insert<T>(IEnumerable<T> entities) where T : class
        {
            try
            {
                dbContext.Set<T>().AddRange(entities);
                return Commit();
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10008 };
            }
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
             return dbContext.Set<T>().AsNoTracking().Where(funcWhere);
            // return dbContext.Set<T>().Where(funcWhere);
        }

        public IQueryable<T> QueryAsTracking<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return dbContext.Set<T>().Where(funcWhere);
        }

        public OperateResult Update<T>(T entity) where T : class
        {
            try
            {
                if (entity == null)
                    return new OperateResult { IsSuccess = false, Message = "实体为空", ErrorCode = 10009 };

                // 检查实体是否已被追踪
                var entry = dbContext.Entry(entity);

                if (entry.State == EntityState.Detached)
                {
                    // 如果实体没有被追踪，则手动附加实体并设置状态为修改
                    dbContext.Set<T>().Attach(entity);
                    entry.State = EntityState.Modified;
                }
                else
                {
                    // 如果实体已被追踪，直接标记为已修改
                    entry.State = EntityState.Modified;
                }

                return Commit();
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10010 };
            }
        }


        public OperateResult Update<T>(IEnumerable<T> entities) where T : class
        {
            try
            {
                foreach (var entity in entities)
                {
                    dbContext.Set<T>().Attach(entity);
                    dbContext.Entry<T>(entity).State = EntityState.Modified;
                }
                return Commit();
            }
            catch (Exception ex)
            {
                return new OperateResult { IsSuccess = false, Message = ex.ToString(), ErrorCode = 10011 };
            }
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }

}
