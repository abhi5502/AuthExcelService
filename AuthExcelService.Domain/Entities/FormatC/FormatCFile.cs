using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Entities.FormatC
{
    public class FormatCFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime UploadedOn { get; set; }
        public string? FilePath { get; set; }
        public bool IsArchived { get; set; } = false;
    }
}
