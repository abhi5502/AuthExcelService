namespace AuthExcelService.Services.IRepository
{
    public interface IAuthExcelServices
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(string url, string token);
        Task<T> UpdateAsync<T>(string url, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
