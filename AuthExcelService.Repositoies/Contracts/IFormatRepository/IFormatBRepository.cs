using AuthExcelService.Domain.Dtos.FileView;
using AuthExcelService.Domain.Entities.FormatA;
using AuthExcelService.Domain.Entities.FormatB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.Contracts.IFormatRepository
{
    public interface IFormatBRepository
    {
        Task<IEnumerable<Format_B_ViewResponseDto>> GetFilesAsync(string userRole, string userCountry);
        Task UploadAsync(FormatBFile entity);
        Task<FormatBFile?> GetByIdAsync(Guid id);
    }
}
