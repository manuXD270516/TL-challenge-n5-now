using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.parameters
{
    public class RequestParameter
    {

        public int pageNumber { get; set; }
        public int pageSize { get; set; }


        public string orderByDirection { get; set; }

        public RequestParameter()
        {
            this.pageNumber = 1;
            this.pageSize = 10;
            this.orderByDirection = "ASC";
        }

        public RequestParameter(int pageNumber, int pageSize, string orderByDirection)
        {
            this.pageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.pageSize = pageSize < 1 ? 10 : pageSize;
            this.orderByDirection = orderByDirection;
        }
    }
}
