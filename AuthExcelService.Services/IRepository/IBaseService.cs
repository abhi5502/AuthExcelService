using AuthExcelService.Services.Models.RequestModel;
using AuthExcelService.Services.Models.ResponseModel;

namespace AuthExcelService.Services.IRepository
{
    public interface IBaseService
    {
        //ApiResponseWeb responseModel { get; set; }
        Task<ApiResponseWeb?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
