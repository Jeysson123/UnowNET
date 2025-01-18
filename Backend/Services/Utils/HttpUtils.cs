using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public static class HttpUtils
    {
        public static readonly int CODE_OK = 200;
        public static readonly int CODE_NO_CONTENT = 204;
        public static readonly int CODE_INTERNAL_ERROR = 500;
        public static readonly int CODE_NOT_FOUND = 404;
        public static readonly int CODE_BAD_REQUEST = 400;

        public static readonly Dictionary<int, string> HTTPS_STATUS = new Dictionary<int, string>
                {
                    { CODE_OK, "Succesfully, {0}" },
                    { CODE_NO_CONTENT, "No content, {0}" },
                    { CODE_INTERNAL_ERROR, "Internal Server Error, {0}" },
                    { CODE_NOT_FOUND, "Not Found, {0}" },
                    { CODE_BAD_REQUEST, "Bad request, {0}" },
                };

        public static HttpStatusDto httpStatusDto { set; get; }
    }
}
