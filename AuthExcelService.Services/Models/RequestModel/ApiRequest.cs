using AuthExcelService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static AuthExcelService.Utility.StaticDetails;
using ContentType = AuthExcelService.Utility.StaticDetails.ContentType;

namespace AuthExcelService.Services.Models.RequestModel
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string ApiUrl { get; set; } = "";
        public object Data { get; set; } = string.Empty;
        public string Token { get; set; } = "";



        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";

        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
