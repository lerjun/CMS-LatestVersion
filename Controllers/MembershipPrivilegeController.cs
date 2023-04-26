using AuthSystem.Models;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static Humanizer.On;
using System.Net.Http.Headers;
using AuthSystem.Services;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text;
using System;
using AuthSystem.Models;
using System.Data;
using AuthSystem.Manager;
using Microsoft.Extensions.Options;
using ExcelDataReader;
using AuthSystem.ViewModel;
using MimeKit;
using MailKit.Net.Smtp;
using NPOI.SS.Formula.Functions;
using System.Xml.Linq;
using NPOI.HPSF;
using NPOI.Util;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Collections;
using NPOI.POIFS.Properties;
using System.Linq;
using _CMS.Manager;
namespace AOPC.Controllers
{
    public class MembershipPrivilegeController : Controller
    {
     
        private ApiGlobalModel _global = new ApiGlobalModel();
        DbManager db = new DbManager();
        private IWebHostEnvironment Environment;
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _environment;
        private GlobalService _globalService;
        private readonly UserManager<ApplicationUser> _userManager;
        public static string UserId;
        private IConfiguration _configuration;
        private string apiUrl = "http://";
        private string status = "";
        public readonly QueryValueService token_;
        private XmlTextReader xmlreader;
        public MembershipPrivilegeController( GlobalService globalService, IOptions<AppSettings> appSettings, IWebHostEnvironment _environment,
                  UserManager<ApplicationUser> userManager, QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
            _globalService = globalService;
            token_ = _token;
            _userManager = userManager;
            UserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
            Environment = _environment;

        }
        [HttpGet]
        public async Task<JsonResult> GetPrivilegeList()
        {
            var url = DBConn.HttpString + "/api/ApiPrivilege/PrivilegeList";
            HttpClient client = new HttpClient();
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));

             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<PrivilegeVM> models = JsonConvert.DeserializeObject<List<PrivilegeVM>>(response);
            return new(models);
        }
        public class LoginStats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> SavePrivilegeInfo(PrivilegeVM data)
        {
            string status = "";
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiPrivilege/SavePrivilege";
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));
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
                status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = status });
        }
        public class DeletePriv
        {

            public int Id { get; set; }
        }
    public class PrivMem
        {


            public string? privilegeID { get; set; }
            public string? usercount { get; set; }
            public string? vipcount { get; set; }
            public string? MembershipID { get; set; }
            public string? status { get; set; }
            public string? stats { get; set; }
        }
       [HttpPost]
        public async Task<IActionResult> Saveprivlist(List<PrivMem> IdList)
        {
            string status = "";
            try
            {
                // HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiPrivilege/SavePrivilegeList";
                // // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());

                // StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                // using (var response = await client.PostAsync(url, content))
                // {
                //     _global.Status = await response.Content.ReadAsStringAsync();
                //     status = JsonConvert.DeserializeObject<LoginStats>(_global.Status).Status;

                // }

                using (var client = new HttpClient())
                {
                     client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
                    StringContent content = new StringContent(JsonConvert.SerializeObject(IdList), Encoding.UTF8, "application/json");
                    using (var response = await client.PostAsync(url, content))
                    {
                        status = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            catch (Exception ex)
            {
                status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = status });
        }
          public class PrivMemListItem
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string PrivilegeID { get; set; }
            public string MembershipID { get; set; }
            public string MembershipName { get; set; }
            public string UserCount { get; set; }
            public string VIPCount { get; set; }
            public string Status { get; set; }

        }
        public class privID
        {
            public string Id { get; set; }

        }
         [HttpPost]
        public async Task<IActionResult> GetPrivilegeMembershipList(privID data)
        {

            List<PrivMemListItem> model = new List<PrivMemListItem>();
            HttpClient client = new HttpClient();
            var url = DBConn.HttpString + "/api/ApiPrivilege/PrivilegeMembershipList";
            _global.Token = _global.GenerateToken("token", _appSettings.Key.ToString());
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(url, content))
            {
                var res = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<PrivMemListItem>>(res);
            }


            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveMembershipTier(MembershipModelVM data)
        {
            string status = "";
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiMembership/SaveMembershipTier";
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));
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
                status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = status });
        }
        [HttpPost]
        public async Task<IActionResult> DeletePrivilegeInfo(DeletePriv data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiPrivilege/DeletePrivilege";
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));
                 client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());

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
        public class DeleteMem
        {

            public int Id { get; set; }
        }
          [HttpPost]
        public async Task<IActionResult> DeleteMemInfo(DeleteMem data)
        {      string status = "";
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiMembership/DeleteMemship";
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
               status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = status });
        }
        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("Bearer");
            if (token == "")
            {
                return RedirectToAction("Index", "LogIn");
            }

            return View();
        }

    }
}
