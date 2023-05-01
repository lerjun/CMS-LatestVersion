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
using System.Collections.Generic;

namespace AOPC.Controllers
{
    public class DashboardController : Controller
    {
        string status = "";
        private readonly QueryValueService token;
        private readonly AppSettings _appSettings;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private GlobalService _globalService;
        DbManager db = new DbManager();
        public readonly QueryValueService token_;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private string apiUrl = "http://";
        public DashboardController(IOptions<AppSettings> appSettings, GlobalService globalService,
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
        
        public IActionResult Index()
        {
            string  token = HttpContext.Session.GetString("Bearer");
            if (token == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetNewRegisteredWeekly()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetNewRegisteredWeekly";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<Usertotalcount> models = JsonConvert.DeserializeObject<List<Usertotalcount>>(response);
            return new(models);
        }  
        [HttpGet]
        public async Task<JsonResult> GetCountAllUser()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetCountAllUserlist  ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<Usertotalcount> models = JsonConvert.DeserializeObject<List<Usertotalcount>>(response);
            return new(models);
        }
            
        [HttpGet]
        public async Task<JsonResult> GetClickCounts()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetClickCountsListAll";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<ClicCountModel> models = JsonConvert.DeserializeObject<List<ClicCountModel>>(response);
            return new(models.ToList().Take(2));
        }  
        [HttpGet]
        public async Task<JsonResult> GetClickCountsGetAll()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetClickCountsListAll";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<ClicCountModel> models = JsonConvert.DeserializeObject<List<ClicCountModel>>(response);
            return new(models);
        }
         
        [HttpGet]
        public async Task<JsonResult> GetSuppoprtCount()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetSupportCountList ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<SupportModel> models = JsonConvert.DeserializeObject<List<SupportModel>>(response);
            return new(models);
        } 
        [HttpGet]
        public async Task<JsonResult> GetCallToAction()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetCallToActionsList ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<CallToActionsModel> models = JsonConvert.DeserializeObject<List<CallToActionsModel>>(response);
            return new(models.ToList().Take(5));
        }    
        [HttpGet]
        public async Task<JsonResult> GetCallToActionModal()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetCallToActionsList ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<CallToActionsModel> models = JsonConvert.DeserializeObject<List<CallToActionsModel>>(response);
            return new(models);
        }
           
        [HttpGet]
        public async Task<JsonResult> GetMostClickStore()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetMostCickStoreList ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<MostClickStoreModel> models = JsonConvert.DeserializeObject<List<MostClickStoreModel>>(response);
            return new(models.ToList().Take(4));
        }         
        [HttpGet]
        public async Task<JsonResult> GetMostClickedHospitality()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetMostClickedHospitalityList ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<MostClickHospitalityModel> models = JsonConvert.DeserializeObject<List<MostClickHospitalityModel>>(response);
            return new(models.ToList().Take(4));
        }    
        [HttpGet]
        public async Task<JsonResult> GetMostClickStoreAll()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetMostCickStoreList ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<MostClickStoreModel> models = JsonConvert.DeserializeObject<List<MostClickStoreModel>>(response);
            return new(models);
        }   
        [HttpGet]
        public async Task<JsonResult> GetMostClickedHospitalityAll()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetMostClickedHospitalityList";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<MostClickStoreModel> models = JsonConvert.DeserializeObject<List<MostClickStoreModel>>(response);
            return new(models);
        }
        [HttpGet]
        public async Task<JsonResult> GetMostClickRestaurant()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetMostClickRestaurantList ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<MostClickHospitalityModel> models = JsonConvert.DeserializeObject<List<MostClickHospitalityModel>>(response);
            return new(models.ToList().Take(4));
        }
        [HttpGet]
        public async Task<JsonResult> GetMostClickRestaurantAll()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetMostClickRestaurantList ";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<MostClickHospitalityModel> models = JsonConvert.DeserializeObject<List<MostClickHospitalityModel>>(response);
            return new(models);
        }   
        [HttpGet]
        public async Task<JsonResult> GetQrTrail()
        {
            var url = DBConn.HttpString + "/api/AuditTrail/QRTrailList";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<QrTrailVM> models = JsonConvert.DeserializeObject<List<QrTrailVM>>(response);
            return new(models);
        }
        [HttpGet]
        public async Task<JsonResult> GetSupportDetails()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetSupportDetailsList";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<SupportDetailModel> models = JsonConvert.DeserializeObject<List<SupportDetailModel>>(response);
            return new(models);
        }
        [HttpGet]
        public async Task<JsonResult> GetNotification()
        {
            var url = DBConn.HttpString + "/api/ApiNotifcation/NotificationList";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<NotificationVM> models = JsonConvert.DeserializeObject<List<NotificationVM>>(response);
            return new(models);
        }
        [HttpGet]
        public async Task<JsonResult> GetLineGraphCount()
        {
            var url = DBConn.HttpString + "/api/ApiSupport/GetLineGraphCountList";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<UserCountLineGraphModel> models = JsonConvert.DeserializeObject<List<UserCountLineGraphModel>>(response);
            return new(models);
        }
        public IActionResult QrTrailIndex()
        {
            string token = HttpContext.Session.GetString("Bearer");
            if (token == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }
        public IActionResult SupportIndex()
        {
            string token = HttpContext.Session.GetString("Bearer");
            if (token == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }
        public IActionResult NotificationIndex()
        {
            string token = HttpContext.Session.GetString("Bearer");
            if (token == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }
        #region POST
        [HttpPost]
        public async Task<IActionResult> PostNewRegisteredCount(UserFilterday data)
        {

            string result = "";
            var list = new List<Usertotalcount>();
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiSupport/PostNewRegistered ";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_.GetValue());
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    string res = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Usertotalcount>>(res);

                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(list);
        }
        [HttpPost]
        public async Task<IActionResult> PostCallToActions(UserFilterday data)
        {

            string result = "";
            var list = new List<CallToActionsModel>();
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiSupport/PostCallToActionsList";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_.GetValue());
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    string res = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<CallToActionsModel>>(res);

                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(list);
        }
        [HttpPost]
        public async Task<IActionResult> PostMostClickRestaurant(UserFilterday data)
        {

            string result = "";
            var list = new List<MostClickRestoModel>();
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiSupport/PostMostClickRestaurantList ";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_.GetValue());
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    string res = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<MostClickRestoModel>>(res);

                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(list);
        }

        [HttpPost]
        public async Task<IActionResult> PostMostClickedHospitality(UserFilterday data)
        {

            string result = "";
            var list = new List<MostClickHospitalityModel>();
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiSupport/PostMostClickedHospitalityList ";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_.GetValue());
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    string res = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<MostClickHospitalityModel>>(res);

                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(list);
        }
        [HttpPost]
        public async Task<IActionResult> PostMostCickStore(UserFilterday data)
        {

            string result = "";
            var list = new List<MostClickStoreModel>();
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiSupport/PostMostCickStoreList ";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_.GetValue());
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    string res = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<MostClickStoreModel>>(res);

                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(list);
        }  
   
        #endregion

        #region DataModels

        public class MostClickRestoModel
        {
            public string Actions { get; set; }
            public string Business { get; set; }
            public string Module { get; set; }
            public string DateCreated { get; set; }
            public int count { get; set; }
            public double Total { get; set; }

        }
        public class UserFilterCatday
        {
            public int day { get; set; }
            public string category { get; set; }

        }
        public class MostClickHospitalityModel
        {
            public string Actions { get; set; }
            public string Business { get; set; }
            public string Module { get; set; }
            public string DateCreated { get; set; }
            public int count { get; set; }
            public double Total { get; set; }

        }
        public class UserCountLineGraphModel
        {
            public string DateCreated { get; set; }
            public int count { get; set; }

        }
        public class SupportModel
        {
            public int Supportcount { get; set; }

        }
        public class Usertotalcount
        {
            public int count { get; set; }
            public int graph_count { get; set; }
            public double percentage { get; set; }
            public string Date { get; set; }

        }
        public class UserFilterday
        {
            public int day { get; set; }
            public string category { get; set; }
        }        
  
        public class NewRegCount
        {
            public int count { get; set; }

        }
        public class ClicCountModel
        {
            public string Module { get; set; }
            public int Count { get; set; }

        }
        public class SupportDetailModel
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public string EmployeeID { get; set; }
            public string Fullname { get; set; }
            public string Status { get; set; }
            public string DateCreated { get; set; }

        }
        public class NotificationVM
        {

            public string? Id { get; set; }
            public string? EmployeeID { get; set; }
            public string? Details { get; set; }
            public string? Fullname { get; set; }
            public string? isRead { get; set; }
            public string? DateCreated { get; set; }

        }

        public class QrTrailVM
        {

            public int Id { get; set; }
            public string EmployeeID { get; set; }
            public string Longtitude { get; set; }
            public string Latitude { get; set; }
            public string IPAddres { get; set; }
            public string Region { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string AreaCode { get; set; }
            public string ZipCode { get; set; }
            public string ISOCode { get; set; }
            public string MetroCode { get; set; }
            public string TimeZone { get; set; }
            public string DateCreated { get; set; }
            public string PostalCode { get; set; }
            public string Fullname { get; set; }
        }
        public class CallToActionsModel
        {
            public string Business { get; set; }
            public string Category { get; set; }
            public int EmailCount { get; set; }
            public int CallCount { get; set; }
            public int BookCount { get; set; }

        }
        public class MostClickStoreModel
        {
            public string Actions { get; set; }
            public string Business { get; set; }
            public string Module { get; set; }
            public string DateCreated { get; set; }
            public int count { get; set; }
            public double Total { get; set; }

        }

        #endregion

    }
}
