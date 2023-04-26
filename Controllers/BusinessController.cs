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
using _CMS.Manager;
using ExcelDataReader;
namespace AOPC.Controllers
{
    public class BusinessController : Controller
    {
        string status="";
        private readonly AppSettings _appSettings;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private GlobalService _globalService;
        DbManager db = new DbManager();
        public readonly QueryValueService token_;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private string apiUrl = "http://";
        public BusinessController(IOptions<AppSettings> appSettings, GlobalService globalService,
                  UserManager<ApplicationUser> userManager, QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
            _userManager = userManager;
            token_ = _token;
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
        }    
        [HttpGet]
        public async Task<JsonResult> GetBusLoc()
        {
            string test = token_.GetValue();
            var url = DBConn.HttpString + "/api/ApiBusinessLoc/BusinessLocList";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 

             string response = await client.GetStringAsync(url);
            List<BusinessLocVM> models = JsonConvert.DeserializeObject<List<BusinessLocVM>>(response);
            return new(models);
        }
        [HttpGet]
        public async Task<JsonResult> GetBusinessTypeList()
        {
             var url = DBConn.HttpString + "/api/ApiBusinessType/BusinessTypeList";
            HttpClient client = new HttpClient();
              client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 
            string response = await client.GetStringAsync(url);
            List<BusinessTypeVM> models = JsonConvert.DeserializeObject<List<BusinessTypeVM>>(response);
            return new(models);
        }
        [HttpGet]
        public async Task<JsonResult> GetBusinessList()
        {
            var url = DBConn.HttpString + "/api/ApiBusiness/BusinessList";
            HttpClient client = new HttpClient();
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 
            string response = await client.GetStringAsync(url);
            List<BusinessModelVM> models = JsonConvert.DeserializeObject<List<BusinessModelVM>>(response);
            return new(models);
        }
          public class Deletebloc
        {

            public int Id { get; set; }
        }
        public class LoginStats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteBusinessLocInfo(Deletebloc data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiBusinessLoc/DeleteBusinessLoc";
                   client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 
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
        [HttpPost]
        public async Task<IActionResult> DeleteBusinessTypeInfo(Deletebloc data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiBusinessType/DeleteBusinessType";
                  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 
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
             [HttpPost]
        public async Task<IActionResult> DeleteBusinessInfo(Deletebloc data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiBusiness/DeleteBusiness";
                  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 
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
        [HttpPost]
        public async Task<IActionResult> SaveBusinessLoc(BusinessLocation data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiBusinessLoc/UpdateBusinessLoc";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = _global.Status });
        }
        [HttpPost]
        public async Task<IActionResult> SaveBusinessinfo(BusinessModel data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiBusiness/SaveBusiness";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = _global.Status });
        }
         [HttpPost]
        public async Task<IActionResult> SaveBusinessType(BusinessTypeModel data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiBusinessType/UpdateBusinessType";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( token_.GetValue()); 
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = _global.Status });
        }
        public IActionResult Index()
        {
            string  token = HttpContext.Session.GetString("Bearer");
            if (token == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }

    }
}
