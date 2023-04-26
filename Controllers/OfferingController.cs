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
using MimeKit;
using MailKit.Net.Smtp;
using OfficeOpenXml;
using _CMS.Manager;
using System.Drawing;
namespace AOPC.Controllers
{
    public class OfferingController : Controller
    {
        private string status = "";
        private readonly AppSettings _appSettings;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private GlobalService _globalService;
        DbManager db = new DbManager();
        private readonly UserManager<ApplicationUser> _userManager;
        public static string UserId;
        private IConfiguration _configuration;
        private string apiUrl = "http://";
        public readonly QueryValueService token_;
        private IWebHostEnvironment Environment;
        public OfferingController(IOptions<AppSettings> appSettings, GlobalService globalService, IWebHostEnvironment _environment,
                  UserManager<ApplicationUser> userManager, QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
            token_ = _token;
            _userManager = userManager;
            UserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
            Environment = _environment;
            
        }
        [HttpGet]
        public async Task<JsonResult> GetOfferingList()
        {
            var url = DBConn.HttpString + "/api/ApiOffering/CMSOfferingList";
            HttpClient client = new HttpClient();
           // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));

           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<OfferingVM> models = JsonConvert.DeserializeObject<List<OfferingVM>>(response);
            return new(models);
        }
              public class LoginStats
        {
            public string Status { get; set; }

        }
         [HttpPost]
        public async Task<IActionResult> SaveOffering(OfferingVM data)
        {
           try
            {
                HttpClient client = new HttpClient();
                var url =DBConn.HttpString + "/api/ApiOffering/SaveOffering";
                _global.Token = _global.GenerateToken("Token", _appSettings.Key.ToString());
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _global.Token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                    status = JsonConvert.DeserializeObject<LoginStats>(_global.Status).Status;
                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = status });
        }
         public class DeleteOffer
        {

            public int Id { get; set; }
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteOfferingInfo(DeleteOffer data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiOffering/DeleteOffering";
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));
                 client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());

                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                      status = JsonConvert.DeserializeObject<LoginStats>(_global.Status).Status;
                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = status });
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
