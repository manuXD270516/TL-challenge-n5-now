using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.wrappers
{
    public class Response<T>
    {
        public bool successed { get; set; }
        public List<string> errors { get; set; }

        public string message { get; set; }

        public T data { get; set; }

        public Response() : this(default(T), null)
        {

        }

        public Response(T data, string message = null)
        {
            this.successed = true;
            this.data = data;
            this.message = message;
        }

        public Response(string message = null)
        {
            this.successed = false;
            this.message = message;
        }


    }
}
