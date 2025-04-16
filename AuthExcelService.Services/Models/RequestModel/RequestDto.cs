using AuthExcelService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AuthExcelService.Utility.StaticDetails;

namespace AuthExcelService.Services.Models.RequestModel
{
    public  class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }

        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
