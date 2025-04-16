using AuthExcelService.Domain.Dtos.FileView;
using AuthExcelService.Domain.Entities.FormatA;
using AuthExcelService.Domain.Entities.FormatC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.Contracts.IFormatRepository
{
    public interface IFormatCRepository
    {
        Task<IEnumerable<Format_C_ViewResponseDto>> GetFilesAsync(string userRole, string userCountry);
        Task UploadAsync(FormatCFile entity);
        Task<FormatCFile?> GetByIdAsync(Guid id);
    }
}
