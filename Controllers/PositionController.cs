using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using AuthSystem.ViewModel;
using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Data;
using AuthSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using AuthSystem.Services;
using System.Text;
using System;
using AuthSystem.Manager;
using ExcelDataReader;
using _CMS.Manager;

namespace AOPC.Controllers
{
    public class PositionController : Controller
    {

        private readonly AppSettings _appSettings;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private GlobalService _globalService;
        DbManager db = new DbManager();
        private readonly UserManager<ApplicationUser> _userManager;
        public static string UserId;
        private IConfiguration _configuration;
        public readonly QueryValueService token_;
        private string apiUrl = "http://";
        public PositionController(IOptions<AppSettings> appSettings, GlobalService globalService,
                  UserManager<ApplicationUser> userManager, QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
            _userManager = userManager;
            UserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
            token_ = _token;
        }
      [HttpGet]
        public async Task<JsonResult> GetPosition()
        {
            var url = DBConn.HttpString + "/api/ApiRegister/PositionList";
            HttpClient client = new HttpClient();
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));

             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<PositionModel> models = JsonConvert.DeserializeObject<List<PositionModel>>(response);
            return new(models);
        }
        public IActionResult Index()
        {
            string  token = HttpContext.Session.GetString("Bearer");
              if (token == "")
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }

    }
}
