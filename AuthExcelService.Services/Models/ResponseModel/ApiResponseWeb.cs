using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Services.Models.ResponseModel
{
    public class ApiResponseWeb
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public object Result { get; set; }
        public string Token { get; set; }

        public string  Message { get; set; }

    }
}
