using AuthExcelService.Services.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AuthExcelService.WebApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TestController> _logger;
        public IAuthWebService _authWebService;
        private readonly ITokenProvider _tokenProvider;


        public TestController(IMapper mapper, ILogger<TestController> logger, IAuthWebService authWebService, ITokenProvider tokenProvider)
        {
            _mapper = mapper;
            _logger = logger;
            _authWebService = authWebService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("Index action started.");

                // Mock data for testing purposes
                var mockData = new List<object>
        {
            new { Property1 = "Value1", Property2 = "Value2", Property3 = "Value3" },
            new { Property1 = "Value4", Property2 = "Value5", Property3 = "Value6" }
        };

                _logger.LogInformation("Index action completed successfully.");
                return View(mockData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the Index action.");
                _logger.LogError("Stack Trace: {StackTrace}", ex.StackTrace);
                return View("Error", ex.Message);
            }
        }

    }
}
