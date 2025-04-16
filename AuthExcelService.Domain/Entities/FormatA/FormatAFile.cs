using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Entities.FormatA
{
    public class FormatAFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime UploadedOn { get; set; }
        public string? FilePath { get; set; } // optional: to store physical path if needed
        public string? Description { get; set; }
    }
}
