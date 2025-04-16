using AuthExcelService.Domain.Dtos.FileView;
using AuthExcelService.Domain.Entities.FormatA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.Contracts.IFormatRepository
{
    public interface IFormatARepository
    {
        Task<IEnumerable<Format_A_ViewResponseDto>> GetFilesAsync(string userRole, string userCountry);
        Task UploadAsync(FormatAFile entity);
        Task<FormatAFile?> GetByIdAsync(Guid id);
        // Add other methods like Delete, Update if needed
    }
}
