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
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;
using static AOPC.Controllers.DashboardController;

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

        private IWebHostEnvironment Environment;
        public BusinessController(IOptions<AppSettings> appSettings, GlobalService globalService, IWebHostEnvironment _environment,
                  UserManager<ApplicationUser> userManager, QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
            _userManager = userManager;
            token_ = _token;
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
            Environment = _environment;
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
        public class BusinessArray
        {
           
            public string Id { get; set; }
            public string Gallery { get; set; }

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
        public async Task<IActionResult> GetBusinessGalArray(Deletebloc data)
        {

            string result = "";
            var list = new List<BusinessArray>();
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiBusiness/BusinessArrray";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_.GetValue());
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    string res = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<BusinessArray>>(res);

                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(list);
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
                        var uploadsFolder = DBConn.Path;
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
                            sql += $@"select Top(1) BusinessID from tbl_BusinessModel where Active =5 and id='" + id + "' order by id desc  ";

                        }
                        else
                        {
                            sql += $@"select Top(1) BusinessID from tbl_BusinessModel where Active =5   order by id desc   ";
                        }
                        string ext = "";
                        if(ctr == 0)
                        {
                            ext ="";
                        }
                        else
                        {
                            ext = "(" + ctr + ")";
                        }
                        DataTable table = db.SelectDb(sql).Tables[0];
                        string str = table.Rows[0]["BusinessID"].ToString() + ext;
                        //var id = table.Rows[0]["OfferingID"].ToString();
                        string getextension = Path.GetExtension(Request.Form.Files[i].FileName);
                        string MyUserDetailsIWantToAdd = str + ".jpg";
    

                        img += "https://www.alfardanoysterprivilegeclub.com/assets/img/"+ Request.Form.Files[i].FileName + ";";

                        string file = Path.Combine(uploadsFolder, Request.Form.Files[i].FileName.Trim());
                        FileInfo f1 = new FileInfo(file);
                        if(f1.Exists)
                        {
                            f1.Delete();
                        }

                        stream = new FileStream(file, FileMode.Create);
                        await  Request.Form.Files[i].CopyToAsync(stream);
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
                ctr++;
                stream.Close();
                stream.Dispose();
            }
        
            if (id != 0 && Request.Form.Files.Count != 0)
            {
               string query = $@"update  tbl_BusinessModel set Gallery ='" + img + "'  where  Id='" + id+ "' ";
                db.AUIDB_WithParam(query);
            }
            if (Request.Form.Files.Count == 0) { status = "Error"; }
            return Json(new { stats = status });
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
