using AuthExcelService.Services.IRepository;
using AuthExcelService.Services.Models.RequestModel;
using AuthExcelService.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuthExcelService.Services.Repository
{
    public class AuthExcelServices : BaseService, IAuthExcelServices
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthExcelServices> _logger;
        private string _authExcelServiceUrl;

        public AuthExcelServices(
    IHttpClientFactory clientFactory,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    ILogger<AuthExcelServices> logger)
    : base(clientFactory, httpContextAccessor, logger) // Pass the logger to the base class
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _authExcelServiceUrl = configuration.GetValue<string>("ServiceUrls:AuthAPI")
                                  ?? throw new ArgumentNullException(nameof(_authExcelServiceUrl), "AuthExcelServiceAPI URL cannot be null.");
        }

        public async Task<T> CreateAsync<T>(string url, string token)
        {
            _logger.LogInformation("CreateAsync called with URL: {Url}", url);
            var apiRequest = new ApiRequest
            {
                ApiUrl = url,
                ApiType = StaticDetails.ApiType.GET,
                Token = token,
                Data = _authExcelServiceUrl
            };
            return await SendAsync<T>(apiRequest);
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            _logger.LogInformation("DeleteAsync called with ID: {Id}", id);
            var apiRequest = new ApiRequest
            {
                ApiUrl = $"{_authExcelServiceUrl}/{id}",
                ApiType = StaticDetails.ApiType.DELETE,
                Token = token
            };
            return SendAsync<T>(apiRequest);
        }

        public async Task<T> GetAllAsync<T>(string token)
        {
            _logger.LogInformation("GetAllAsync called");
            var apiRequest = new ApiRequest
            {
                ApiUrl = _authExcelServiceUrl,
                ApiType = StaticDetails.ApiType.GET,
                Token = token
            };
            return await SendAsync<T>(apiRequest);
        }

        public async Task<T> GetAsync<T>(int id, string token)
        {
            _logger.LogInformation("GetAsync called with ID: {Id}", id);
            var apiRequest = new ApiRequest
            {
                ApiUrl = $"{_authExcelServiceUrl}/{id}",
                ApiType = StaticDetails.ApiType.GET,
                Token = token
            };
            return await SendAsync<T>(apiRequest);
        }

        public async Task<T> UpdateAsync<T>(string url, string token)
        {
            _logger.LogInformation("UpdateAsync called with URL: {Url}", url);
            var apiRequest = new ApiRequest
            {
                ApiUrl = url,
                ApiType = StaticDetails.ApiType.PUT,
                Token = token
            };
            return await SendAsync<T>(apiRequest);
        }
    }
}
