using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Entities.FormatB
{
    public class FormatBFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime UploadedOn { get; set; }
        public string? FilePath { get; set; }
        public string? Notes { get; set; }
    }
}
