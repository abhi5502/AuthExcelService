using AuthExcelService.Domain.Dtos.FileView;
using AuthExcelService.Domain.Entities.FormatA;
using AuthExcelService.Repositoies.Contracts;
using AuthExcelService.Repositoies.Contracts.IFormatRepository;
using AuthExcelService.Repositoies.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.RepositoryService.FormatRepository
{
    public class FormatARepository : GenericRepository<FormatAFile>, IFormatARepository
    {
        private readonly DbContext _context;
        private readonly DbSet<FormatAFile> _dbSet;

        public FormatARepository(DbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<FormatAFile>();
        }

        public async Task<IEnumerable<Format_A_ViewResponseDto>> GetFilesAsync(string userRole, string userCountry)
        {
            var query = _dbSet.AsQueryable();

            if (!userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(x => x.Country == userCountry);
            }

            var result = await query
                .OrderByDescending(x => x.UploadedOn)
                .ToListAsync();

            return result.Select(x => new Format_A_ViewResponseDto
            {
                Id = x.Id,
                FileName = x.FileName,
                UploadedBy = x.UploadedBy,
                Country = x.Country,
                UploadedDate = x.UploadedOn,
                DownloadUrl = x.FilePath ?? string.Empty,
                FileSize = x.FilePath != null && File.Exists(x.FilePath)
                    ? new FileInfo(x.FilePath).Length
                    : 0,
                UserFullName = x.UploadedBy
            }).ToList();
        }

        public async Task<FormatAFile?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UploadAsync(FormatAFile entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(); // don't forget to save
        }
    }


}
