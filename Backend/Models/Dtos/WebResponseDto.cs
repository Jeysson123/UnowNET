using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class WebResponseDto <T>
    {
        public int Code { set; get; }
        public string Message { set; get; }
        public T Body { set; get; }
    }
}
