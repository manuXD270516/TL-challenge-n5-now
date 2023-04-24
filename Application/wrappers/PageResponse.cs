using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.wrappers
{
    public class PageResponse<T> : Response<T>
    {

        public int pageNumber { get; set; }
        public int pageSize { get; set; }

        public int totalCount { get; set; }

        public PageResponse(T data, int pageNumber, int pageSize, int totalCount = 0)
        {
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
            this.totalCount = totalCount;
            /*this.totalCount = (int)data.GetType().
                GetProperty("Count").GetValue(data);*/
            this.data = data;
            this.message = null;
            this.successed = true;
            this.errors = null;
        }


    }
}
