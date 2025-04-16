using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Dtos.FileView
{
    public class Format_B_ViewResponseDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
        public bool IsArchived { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
        public long FileSize { get; set; } // in bytes
    }
}
