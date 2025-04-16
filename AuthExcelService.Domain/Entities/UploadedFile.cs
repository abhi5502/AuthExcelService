using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Domain.Entities
{
    public class UploadedFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid CountryId { get; set; }

        public Country Country { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
