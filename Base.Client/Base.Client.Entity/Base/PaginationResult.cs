using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.Entity
{
    public class PaginationResult<T> : ResultEntity<T>
    {
        public PageInfo pageInfo { get; set; }
    }

    public class PageInfo
    {
        /// <summary>
        /// 请求的页码
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int recordCount { get; set; }
    }
}
