using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Dtos.FileUpload
{
    public class Format_C_UploadRequestDto
    {
        public IFormFile ExcelFile { get; set; } = default!;
        public string UploadedBy { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsArchived { get; set; }
    }
}
