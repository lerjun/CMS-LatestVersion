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
using System.Drawing.Imaging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.IO;

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
        public class Userlist
        {
            public string Fullname { get; set; }
            public string Email { get; set; }

        }
        [HttpGet]
        public async Task<JsonResult> GetUserList()
        {
            var url = DBConn.HttpString + "/api/ApiOffering/UserListEmail";
            HttpClient client = new HttpClient();
           // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Bearer"));

           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<Userlist> models = JsonConvert.DeserializeObject<List<Userlist>>(response);
            return new(models);
        }
     
        [HttpPost]
        public async Task<IActionResult> SaveOffering(OfferingVM data)
        {
           try
            {
                HttpClient client = new HttpClient();
                var url =DBConn.HttpString + "/api/ApiOffering/SaveOffering";
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
        [HttpPost]
        public async Task<IActionResult> PostNotifications(NotificationInsertModel data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiNotifcation/InsertNotifications";
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
        public class NotificationInsertModel
        {

            public string? Id { get; set; }
            public string? EmployeeID { get; set; }
            public string? Details { get; set; }
            public string? Module { get; set; }
            public string? ItemID { get; set; }
            public int? isRead { get; set; }
            public int? EmailStatus { get; set; }


        }
        public class DeleteOffer
        {

            public int Id { get; set; }
        }   
        public class UserEmail
        {

            public string email { get; set; }
            public string offerid { get; set; }
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteOfferingInfolist(List<DeleteOffer> IdList)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiOffering/DeleteOfferingList";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());

                StringContent content = new StringContent(JsonConvert.SerializeObject(IdList), Encoding.UTF8, "application/json");
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
        public async Task<IActionResult> UserSendEmail(List<UserEmail> IdList)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiOffering/SendEmail";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());

                StringContent content = new StringContent(JsonConvert.SerializeObject(IdList), Encoding.UTF8, "application/json");
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
        public async Task<IActionResult> DeleteOffering(DeleteOffer data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiOffering/DeleteOffering";
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
        public async Task<IActionResult> UploadFile(List<IFormFile> postedFiles, int id)
        {

            int i;
            var stream = (dynamic)null;
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            int ctr = 0;
            string img = "";
            for (i = 0; i < Request.Form.Files.Count; i++)
            {
                if (Request.Form.Files[i].Length > 0)
                {
                    try
                    {
                        //  string uploadsFolder = @"C:\\Files\\";
                        var uploadsFolder = DBConn.Path;
                        //var filePath = Environment.WebRootPath + "\\uploads\\";
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        List<string> uploadedFiles = new List<string>();



                        var image = System.Drawing.Image.FromStream(Request.Form.Files[i].OpenReadStream());
                        var resized = new Bitmap(image, new System.Drawing.Size(400, 400));

                        using var imageStream = new MemoryStream();
                        resized.Save(imageStream, ImageFormat.Jpeg);
                        var imageBytes = imageStream;
                        string sql = "";

                        if (id != 0)
                        {
                            sql += $@"select Top(1) OfferingID from tbl_OfferingModel where StatusID =5 and id='" + id + "' order by id desc  ";

                        }
                        else
                        {
                            sql += $@"select Top(1) OfferingID from tbl_OfferingModel where StatusID =5  order by id desc  ";
                        }
                        string ext = "";
                        if (ctr == 0)
                        {
                            ext = "";
                        }
                        else
                        {
                            ext = "(" + ctr + ")";
                        }
                        DataTable table = db.SelectDb(sql).Tables[0];
                        string str = table.Rows[0]["OfferingID"].ToString() + ext;
                        //var id = table.Rows[0]["OfferingID"].ToString();
                        string getextension = Path.GetExtension(Request.Form.Files[i].FileName);
                        string MyUserDetailsIWantToAdd = str + getextension;

                        img += "https://www.alfardanoysterprivilegeclub.com/assets/img/" + MyUserDetailsIWantToAdd + ";";

                        string file = Path.Combine(uploadsFolder, MyUserDetailsIWantToAdd);
                        FileInfo f1 = new FileInfo(file);
                        if (f1.Exists)
                        {
                            f1.Delete();
                        }

                        stream = new FileStream(file, FileMode.Create);
                        await Request.Form.Files[i].CopyToAsync(stream);
                        //if (!System.IO.File.Exists(file))
                        //{
                        //    System.IO.FileStream f = System.IO.File.Create(file);
                        //    f.Close();
                        //}
                    }
                    catch (Exception ex)
                    {
                        status = "Error! " + ex.GetBaseException().ToString();
                    }

                }

                stream.Close();
                stream.Dispose();
            }
          
            if (Request.Form.Files.Count == 0) { status = "Error"; }
            return Json(new { stats = status });
        }
        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("Bearer");
            if (token == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }
        
    }
}
