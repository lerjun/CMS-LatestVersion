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
    public class RegisterController : Controller
    {
        
        string status="";
         private readonly QueryValueService token;
        private readonly AppSettings _appSettings;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private GlobalService _globalService;
        DbManager db = new DbManager();
        public readonly QueryValueService token_;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private string apiUrl = "http://";
        private IWebHostEnvironment Environment;
        public RegisterController(IOptions<AppSettings> appSettings, GlobalService globalService, IWebHostEnvironment _environment,
                  UserManager<ApplicationUser> userManager, QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
             token_ = _token;
            _userManager = userManager;
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
            Environment = _environment;

        }
        public async Task<String> GetUserInfo()
        {

            var url = DBConn.HttpString + "/api/ApiRegister/UserAllist";
            HttpClient client = new HttpClient();
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
            string response = await client.GetStringAsync(url);

            return response;
        }
        [HttpGet]
                public async Task<JsonResult> GetuserList()
        {
             var url = DBConn.HttpString + "/api/ApiRegister/UserAllist";
            HttpClient client = new HttpClient();
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
            string response = await client.GetStringAsync(url);
            List<UserVM> model = JsonConvert.DeserializeObject<List<UserVM>>(response);
            return new(model);
        }
       
        [HttpGet]
        public async Task<JsonResult> GetPosition()
        {
            var url = DBConn.HttpString + "/api/ApiRegister/PositionList";
            HttpClient client = new HttpClient();
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
            string response = await client.GetStringAsync(url);
            List<PositionModel> models = JsonConvert.DeserializeObject<List<PositionModel>>(response);
            return new(models);
        }
          [HttpPost]
        public async Task<IActionResult> SavePosition(PositionModel data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiRegister/SavePosition";
                   client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 

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
         public class DeletePos
        {

            public int Id { get; set; }
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> DeletePositionInfo(DeletePos data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiRegister/DeletePosition";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
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
        [HttpGet]
        public async Task<JsonResult> GetCorporateUserList()
        {
            var url = DBConn.HttpString + "/api/ApiRegister/Corporatelist";
            HttpClient client = new HttpClient();
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
           string response = await client.GetStringAsync(url);
            List<CorpUserVM> models = JsonConvert.DeserializeObject<List<CorpUserVM>>(response);
            return new(models);
        }
        public async Task<JsonResult> GetAdminUserList()
        {
            var url = DBConn.HttpString + "/api/ApiRegister/AdminList";
            HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
            string response = await client.GetStringAsync(url);
            List<AdminUserVM> models = JsonConvert.DeserializeObject<List<AdminUserVM>>(response);
            return new(models);
        }
        [HttpGet]
        public async Task<JsonResult> GetUserType()
        {
            var url = DBConn.HttpString + "/api/ApiRegister/UserTypeList";
            HttpClient client = new HttpClient();
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
           string response = await client.GetStringAsync(url);
            List<UserTypeModel> models = JsonConvert.DeserializeObject<List<UserTypeModel>>(response);
            return new(models);
        }
        [HttpPost]
        public async Task<IActionResult> SaveUserInfo(UserModel data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiRegister/UpdateUserInfo";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 

                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                    if (data.Id.ToString() == HttpContext.Session.GetString("Id"))
                    {
                        HttpContext.Session.SetString("ImgUrl", "https://www.alfardanoysterprivilegeclub.com/assets/img/" + data.FilePath);
                    }

                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = _global.Status });
        }
        public class LoginStats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserModel data)
        {
            string status = "";
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiRegister/FinalUserRegistration2";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                    status = JsonConvert.DeserializeObject<LoginStats>(_global.Status).Status;
                    HttpContext.Session.SetString("registeremail", data.Email);

                }
            }

            catch (Exception ex)
            {

            }
            return Json(new { stats = status });
        }
        public class membershiptier
        {

            public string Id { get; set; }
            public string MembershipName { get; set; }
        }
        public class corporateid
        {

            public string Id { get; set; }
        }
        public class userid
        {

            public string Id { get; set; }
        }
        public class CorporateStatus
        {

            public string status { get; set; }
        }

        public JsonResult UploadFile(List<IFormFile> postedFiles)
        {
            int i;
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            for (i = 0; i < Request.Form.Files.Count; i++)
            {
                if (Request.Form.Files[i].Length > 0)
                {
                    try
                    {
                        // var filePath = "C:\\Files\\";
                        var filePath = "C:\\inetpub\\AOPCAPP\\public\\assets\\img\\";
                        //var filePath = Environment.WebRootPath + "\\uploads\\";
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        List<string> uploadedFiles = new List<string>();


                        Guid guid = Guid.NewGuid();
                        string getextension = Path.GetExtension(Request.Form.Files[i].FileName);
                        string MyUserDetailsIWantToAdd = "EMP-01" + getextension;
                        string file = Path.Combine(filePath, Request.Form.Files[i].FileName);

                        var stream = new FileStream(file, FileMode.Create);
                        Request.Form.Files[i].CopyToAsync(stream);
                        //status = "https://www.alfardanoysterprivilegeclub.com/assets/img/" + MyUserDetailsIWantToAdd;
                        foreach (IFormFile postedFile in postedFiles)
                        {
                            string fileName = Path.GetFileName(postedFile.FileName);
                            using (FileStream streams = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                            {
                                postedFile.CopyTo(streams);
                                uploadedFiles.Add(fileName);
                                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        status = "Error! " + ex.GetBaseException().ToString();
                    }
                }
            }
            if (Request.Form.Files.Count == 0) { status = "Error"; }
            return Json(new { stats = status });
        }
        [HttpPost]
        public async Task<IActionResult> CorporateFilterMembership(corporateid data)
        {
            string result = "";
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiCorporate/MembershipCorporate";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    string res = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<membershiptier>(res).Id;

                }

            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = result });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(string id)
        {
            string result = "";
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiRegister/UpdateUserStatus";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
                StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    string res = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<membershiptier>(res).Id;

                }

            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();
            }
            return Json(new { stats = result });
        }
        [HttpPost]
        public async Task<IActionResult> VerifyOTP(OTPnumber data)
        {
            string status = "";
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiRegister/VerifyOTP";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
               StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                    status = JsonConvert.DeserializeObject<LoginStats>(_global.Status).Status;


                }
            }

            catch (Exception ex)
            {
                    
            }
            return Json(new { stats = status });
        }
        public class DeleteUser
        {

            public int Id { get; set; }
        }
        public class OTPnumber
        {
            public string? otp { get; set; }
            public string? Email { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserInfo(DeleteUser data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiRegister/DeleteUserInfo";
                   client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
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
        public class CorporateID
        {
            public string CorporateName { get; set; }

        }
        public class PositionID
        {
            public string PositionName { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> Admin(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            System.Text.Encoding.RegisterProvider(
          System.Text.CodePagesEncodingProvider.Instance);

            if (file == null)
            {
                ViewData["Message"] = "Error: Please select a file.";
            }
            else
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    if (file.FileName.Contains("Admin-Registration-Template"))
                    {

                        ViewData["Message"] = "Error: Invalid file.";
                        string filename = $"{hostingEnvironment.WebRootPath}\\excel\\{file.FileName}";
                        using (FileStream fileStream = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fileStream);
                            fileStream.Flush();
                        }

                        IExcelDataReader reader = null;
                        FileStream stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(stream);
                        if (file.FileName.EndsWith("xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);

                        }
                        if (file.FileName.EndsWith("xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                        }
                        int i = 0;

                        var data = new List<UserModel>();

                        while (reader.Read())
                        {
                            i++;

                            if (i > 1)
                            {

                                string EmployeeID = reader.GetValue(0) == null ? "none" : reader.GetValue(0).ToString();

                                string Fname = reader.GetValue(1) == null ? "none" : reader.GetValue(1).ToString();

                                string Lname = reader.GetValue(2) == null ? "none" : reader.GetValue(2).ToString();

                                string Username = reader.GetValue(3) == null ? "none" : reader.GetValue(3).ToString();

                                string Gender = reader.GetValue(5) == null ? "none" : reader.GetValue(4).ToString();
                                string Email = reader.GetValue(6) == null ? "none" : reader.GetValue(5).ToString();


                                StringBuilder str_build = new StringBuilder();
                                Random random = new Random();
                                int length = 8;
                                char letter;

                                for (int x = 0; x < length; x++)
                                {
                                    double flt = random.NextDouble();
                                    int shift = Convert.ToInt32(Math.Floor(25 * flt));
                                    letter = Convert.ToChar(shift + 2);
                                    str_build.Append(letter);
                                }
                                var token = Cryptography.Encrypt(str_build.ToString());
                                string strtokenresult = token;
                                string[] charsToRemove = new string[] { "/", ",", ".", ";", "'", "=" };
                                foreach (var c in charsToRemove)
                                {
                                    strtokenresult = strtokenresult.Replace(c, string.Empty);
                                }
                                data.Add(new UserModel
                                {
                                    Username = Username,
                                    Password = "",
                                    Fullname = Fname + " " + Lname,
                                    Fname = Fname,
                                    Lname = Lname,
                                    Gender = Gender,
                                    Email = Email,
                                    CorporateID = 14,
                                    PositionID = 1004,
                                    JWToken = string.Concat(strtokenresult.TakeLast(15)),
                                    FilePath = "",
                                    Type = 1,
                                    Active = 1,
                                    EmployeeID = EmployeeID,
                                    Id = 0,

                                });
                            }
                        }
                        reader.Close();

                        //Send Data to API
                        var status = "";
                        HttpClient client = new HttpClient();
                        var url = DBConn.HttpString + "/api/ApiRegister/Import";
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 

                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        using (var response = await client.PostAsync(url, content))
                        {
                            _global.Status = await response.Content.ReadAsStringAsync();
                        }
                        System.IO.File.Delete(filename);
                        ViewData["Message"] = "New Entry" + _global.Status;
                    }
                    else
                    {
                        ViewData["Message"] = "Error: Invalid file.";
                    }
                }
                else
                {
                    ViewData["Message"] = "Error:  Invalid file.";
                }
            }
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CorporateIndex(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            System.Text.Encoding.RegisterProvider(
          System.Text.CodePagesEncodingProvider.Instance);

            if (file == null)
            {
                ViewData["Message"] = "Error: Please select a file.";
            }
            else
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    if (file.FileName.Contains("Corporate-Admin-Registration"))
                    {

                        ViewData["Message"] = "Error: Invalid file.";
                        string filename = $"{hostingEnvironment.WebRootPath}\\excel\\{file.FileName}";
                        using (FileStream fileStream = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fileStream);
                            fileStream.Flush();
                        }

                        IExcelDataReader reader = null;
                        FileStream stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(stream);
                        if (file.FileName.EndsWith("xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);

                        }
                        if (file.FileName.EndsWith("xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                        }
                        int i = 0;

                        var data = new List<UserModel>();

                        while (reader.Read())
                        {
                            i++;

                            if (i > 1)
                            {
                                string sql = $@"select Id from tbl_CorporateModel where CorporateName='" + reader.GetValue(7).ToString() + "'";
                                DataTable dt = db.SelectDb(sql).Tables[0];
                                var corporateid = "";
                                if (dt.Rows.Count > 0)
                                {
                                    corporateid = dt.Rows[0]["Id"].ToString();
                                }
                                else
                                {
                                    corporateid = "0";
                                }
                                string pos = $@"select Id from tbl_PositionModel where Name='" + reader.GetValue(4).ToString() + "'  ";
                                DataTable dts = db.SelectDb(pos).Tables[0];
                                var positionid = "";
                                if (dts.Rows.Count > 0)
                                {
                                    positionid = dts.Rows[0]["Id"].ToString();
                                }
                                else
                                {
                                    positionid = "0";
                                }
                                string EmployeeID = reader.GetValue(0) == null ? "none" : reader.GetValue(0).ToString();

                                string Fname = reader.GetValue(1) == null ? "none" : reader.GetValue(1).ToString();

                                string Lname = reader.GetValue(2) == null ? "none" : reader.GetValue(2).ToString();

                                string Username = reader.GetValue(3) == null ? "none" : reader.GetValue(3).ToString();

                                string PositionId = reader.GetValue(4) == null ? "0" : positionid.ToString();

                                string Gender = reader.GetValue(5) == null ? "none" : reader.GetValue(5).ToString();
                                string CorporateID = reader.GetValue(6) == null ? "0" : corporateid;
                                string Email = reader.GetValue(6) == null ? "none" : reader.GetValue(6).ToString();


                                StringBuilder str_build = new StringBuilder();
                                Random random = new Random();
                                int length = 8;
                                char letter;

                                for (int x = 0; x < length; x++)
                                {
                                    double flt = random.NextDouble();
                                    int shift = Convert.ToInt32(Math.Floor(25 * flt));
                                    letter = Convert.ToChar(shift + 2);
                                    str_build.Append(letter);
                                }
                                var token = Cryptography.Encrypt(str_build.ToString());
                                string strtokenresult = token;
                                string[] charsToRemove = new string[] { "/", ",", ".", ";", "'", "=" };
                                foreach (var c in charsToRemove)
                                {
                                    strtokenresult = strtokenresult.Replace(c, string.Empty);
                                }
                                data.Add(new UserModel
                                {
                                    Username = Username,
                                    Password = "",
                                    Fullname = Fname + " " + Lname,
                                    Fname = Fname,
                                    Lname = Lname,
                                    Gender = Gender,
                                    Email = Email,
                                    CorporateID = int.Parse(corporateid),
                                    PositionID = int.Parse(PositionId),
                                    JWToken = string.Concat(strtokenresult.TakeLast(15)),
                                    FilePath = "",
                                    Type = 2,
                                    Active = 2,
                                    EmployeeID = EmployeeID,
                                    Id = 0,

                                });
                            }
                        }
                        reader.Close();

                        //Send Data to API
                        var status = "";
                        HttpClient client = new HttpClient();
                        var url = DBConn.HttpString + "/api/ApiRegister/Import";
                           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 

                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        using (var response = await client.PostAsync(url, content))
                        {
                            _global.Status = await response.Content.ReadAsStringAsync();
                        }
                        System.IO.File.Delete(filename);
                        ViewData["Message"] = "New Entry" + _global.Status;
                    }
                    else
                    {
                        ViewData["Message"] = "Error: Corporate Invalid file.";
                    }
                }
                else
                {
                    ViewData["Message"] = "Error: Invalid filesss.";
                }
            }
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            System.Text.Encoding.RegisterProvider(
          System.Text.CodePagesEncodingProvider.Instance);

            if (file == null)
            {
                ViewData["Message"] = "Error: Please select a file.";
            }
            else
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    if (file.FileName.Contains(HttpContext.Session.GetString("CorporateName")))
                    {

                        ViewData["Message"] = "Error: Invalid file.";
                        string filename = $"{hostingEnvironment.WebRootPath}\\excel\\{file.FileName}";
                        using (FileStream fileStream = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fileStream);
                            fileStream.Flush();
                        }

                        IExcelDataReader reader = null;
                        FileStream stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(stream);
                        if (file.FileName.EndsWith("xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);

                        }
                        if (file.FileName.EndsWith("xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                        }
                        int i = 0;

                        var data = new List<UserModel>();

                        while (reader.Read())
                        {
                            i++;

                            if (i > 1)
                            {
                                string sql = $@"select Id from tbl_CorporateModel where CorporateName='" + HttpContext.Session.GetString("CorporateName") + "'";
                                DataTable dt = db.SelectDb(sql).Tables[0];
                                var corporateid = "";
                                if (dt.Rows.Count > 0)
                                {
                                    corporateid = dt.Rows[0]["Id"].ToString();
                                }
                                else
                                {
                                    corporateid = "0";
                                }
                                string pos = $@"select Id from tbl_PositionModel where Name='" + reader.GetValue(4).ToString() + "'  ";
                                DataTable dts = db.SelectDb(pos).Tables[0];
                                var positionid = "";
                                if (dts.Rows.Count > 0)
                                {
                                    positionid = dts.Rows[0]["Id"].ToString();
                                }
                                else
                                {
                                    positionid = "0";
                                }
                                string EmployeeID = reader.GetValue(0) == null ? "none" : reader.GetValue(0).ToString();

                                string Fname = reader.GetValue(1) == null ? "none" : reader.GetValue(1).ToString();

                                string Lname = reader.GetValue(2) == null ? "none" : reader.GetValue(2).ToString();

                                string Username = reader.GetValue(3) == null ? "none" : reader.GetValue(3).ToString();

                                string PositionId = reader.GetValue(4) == null ? "0" : positionid.ToString();

                                string Gender = reader.GetValue(5) == null ? "none" : reader.GetValue(5).ToString();
                                string Email = reader.GetValue(6) == null ? "none" : reader.GetValue(6).ToString();


                                StringBuilder str_build = new StringBuilder();
                                Random random = new Random();
                                int length = 8;
                                char letter;

                                for (int x = 0; x < length; x++)
                                {
                                    double flt = random.NextDouble();
                                    int shift = Convert.ToInt32(Math.Floor(25 * flt));
                                    letter = Convert.ToChar(shift + 2);
                                    str_build.Append(letter);
                                }
                                var token = Cryptography.Encrypt(str_build.ToString());
                                string strtokenresult = token;
                                string[] charsToRemove = new string[] { "/", ",", ".", ";", "'", "=" };
                                foreach (var c in charsToRemove)
                                {
                                    strtokenresult = strtokenresult.Replace(c, string.Empty);
                                }
                                data.Add(new UserModel
                                {
                                    Username = Username,
                                    Password = "",
                                    Fullname = Fname + " " + Lname,
                                    Fname = Fname,
                                    Lname = Lname,
                                    Gender = Gender,
                                    Email = Email,
                                    CorporateID = int.Parse(corporateid),
                                    PositionID = int.Parse(PositionId),
                                    JWToken = string.Concat(strtokenresult.TakeLast(15)),
                                    FilePath = "",
                                    Type = 3,
                                    Active = 2,
                                    EmployeeID = EmployeeID,
                                    Id = 0,

                                });
                            }
                        }
                        reader.Close();

                        //Send Data to API
                        var status = "";
                        HttpClient client = new HttpClient();
                        var url = DBConn.HttpString + "/api/ApiRegister/Import";
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 

                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        using (var response = await client.PostAsync(url, content))
                        {
                            _global.Status = await response.Content.ReadAsStringAsync();
                        }
                        System.IO.File.Delete(filename);
                        ViewData["Message"] = "New Entry" + _global.Status;
                    }
                    else
                    {
                        ViewData["Message"] = "Error: sadaInvalid file.";
                    }
                }
                else
                {
                    ViewData["Message"] = "Error: User Invalid file.";
                }
            }
            return View("Index");
        }
        public class sendemaildata
        {
            public string? Email { get; set; }
            public string? Fname { get; set; }
            public string? Lname { get; set; }


        }
        [HttpPost]
        public IActionResult Sendemail(sendemaildata data)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("AOPC Registration", "app@alfardan.com.qa"));
            //message.To.Add(new MailboxAddress("Ace Caspe", "ace.caspe@odecci.com"));
            message.To.Add(new MailboxAddress(data.Fname + " " + data.Lname, data.Email));
            //message.To.Add(new MailboxAddress("Carl Jecson", "carl.jecson.d.galvez@odecci.com"));
            //message.To.Add(new MailboxAddress("Agabi", "allan.gabriel@odecci.com"));
            //message.To.Add(new MailboxAddress("Alibaba", "alisandro.villegas@odecci.com"));
            message.Subject = "Email Registration Link";
            var bodyBuilder = new BodyBuilder();
            string img = "https://www.alfardanoysterprivilegeclub.com/assets/img/AOPC%20Logo%20-%20White.png";
            string bg = "https://www.alfardanoysterprivilegeclub.com/build/assets/black-cover-pattern-f558a9d0.jpg";
            bodyBuilder.HtmlBody = @"<style>" +
   " @font-face {font-family: 'Montserrat-Reg';src: url('/fonts/Montserrat/Montserrat-Regular.ttf');}" +
    "@font-face {" +
      "font-family: 'Montserrat-Bold';" +
      "src: url('/fonts/Montserrat/Montserrat-Bold.ttf');" +
    "}" +
    "@font-face {" +
      "font-family: 'Montserrat-SemiBold';" +
      "src: url('/fonts/Montserrat/Montserrat-SemiBold.ttf');" +
    "}" +
    "body {margin: 0;box-sizing: border-box;}" +
    ".login-container {background-image: url(" + bg + ");height: 100vh; width: 100vw;display: flex;justify-content: center;align-items: center;flex-direction: column; background-size: cover;}" +
    ".gradient-border {height: 600px;width: 700px; display: flex;justify-content: center;background-color: transparent;border-width: 3px;box-sizing: content-box;border-style: solid;border-image-slice: 1;" +
    "gap: 20px;border-image-source: linear-gradient(" +
        "180deg," +
        "#b07b29 17.26%," +
        "#ebcc77 31.95%," +
        "#b98732 53.29%," +
        "#ecce79 74.41%," +
        "#c69840 99.86%" +
      ");" +
      "flex-direction: column;}" +
    ".login-container img {" +
      "margin: 20px auto;" +
      "width: 300px;" +
      "height: 110px;" +
    "}" +
     "h1 {" +
      " text-align: center;" +
      " color: #d7d2cb;" +
      " font-family: 'Montserrat-SemiBold';" +
      " font-size: 2rem;" +
     " font-style: italic;" +
    " }" +
     "h3 {" +
      " text-align: center;" +
      " color: #d7d2cb;" +
       "font-family: 'Montserrat-Reg';" +
       "font-size: 1.5rem;" +
      " font-style: italic;" +
     "}" +
    " a {" +
     "  text-decoration: none;" +
     "}" +
     "h4 {" +
       "text-align: center;" +
      " color: #d7d2cb;" +
      " font-family: 'Montserrat-Reg';" +
      " font-size: 1.2rem;" +
      " font-style: italic;" +
    "}" +
  " </style>" +
  " <body>" +
    " <div class='login-container'>" +
      " <div class='login-logo-conctainer'>" +
        " <div class='gradient-border'>" +
           "<img src='" + img + "' alt='AOPC' width='100%'' />" +

          " <h1>" +
            " WELCOME TO<br />ALFARDAN OYSTER <br />" +
            " PRIVILEGE CLUB" +
           "</h1>" +
          " <h3>REGISTRATION FORM</h3>" +
           "<a href='https://www.alfardanoysterprivilegeclub.com/user-registration'><h4> Click Here to Register in<br />Alfardan Oyster Privilege Club</h4></a>" +
        " </div>" +
     "  </div>" +
    " </div>" +
  " </body>";
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("app@alfardan.com.qa", "Oyster2023!");
                client.Send(message);
                client.Disconnect(true);
                status = "Successfully sent registration email";

            }

            return Json(new { stats = status });
        }
        [HttpPost]
        public IActionResult SendemailUser(sendemaildata data)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("AOPC Registration", "app@alfardan.com.qa"));
            //message.To.Add(new MailboxAddress("Ace Caspe", "ace.caspe@odecci.com"));
            message.To.Add(new MailboxAddress(data.Fname + " " + data.Lname, data.Email));
            //message.To.Add(new MailboxAddress("Carl Jecson", "carl.jecson.d.galvez@odecci.com"));
            //message.To.Add(new MailboxAddress("Agabi", "allan.gabriel@odecci.com"));
            //message.To.Add(new MailboxAddress("Alibaba", "alisandro.villegas@odecci.com"));
            message.Subject = "Email Registration Link";
            var bodyBuilder = new BodyBuilder();

            // bodyBuilder.HtmlBody = @"
            // <div style='background-color:white;color:black;padding:20px; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>
            // <h1 style='text-align: center; font-family: 'Sigmar One', cursive;'>Welcome to Alfardan Oyster Privilege Club</h1>
            // <p>Hello " + data.Fname + ",</p></br>" +
            // "<p>This email  was sent to confirm your registration for Alfardan Oyster Privilege Club</p>" + 
            // "<p>Name: " + data.Fname + " " + data.Lname + "</p>" +
            // "<p>Email Address: " + data.Email + "</p></br>" +
            // "<p>Please click this this link  " + "<a href='https://www.alfardanoysterprivilegeclub.com/user-registration'>Here</a> and complete your details and registration </br>" +
            // "<p>For any concern please contact your admin.</p></br>" +
            // "<p>Thank you,</p></br>" +
            // "<p>Alfardan Oyster Privilege Club</p></br>" +
            // @"
            // </div>         
            // ";
            string img = "../img/AOPCBlack.jpg";
            bodyBuilder.HtmlBody = @"<style>" +

    "body {margin: 0;box-sizing: border-box;}" +
    ".login-container {background-image: url(" + img + ");height: 100vh; width: 100vw;display: flex;justify-content: center;align-items: center;flex-direction: column; background-size: cover;}" +
    ".gradient-border {height: 600px;width: 700px; display: flex;justify-content: center;background-color: transparent;border-width: 3px;box-sizing: content-box;border-style: solid;border-image-slice: 1;" +
    "gap: 20px;border-image-source: linear-gradient(" +
        "180deg," +
        "#b07b29 17.26%," +
        "#ebcc77 31.95%," +
        "#b98732 53.29%," +
        "#ecce79 74.41%," +
        "#c69840 99.86%" +
      ");" +
      "flex-direction: column;}" +
    ".login-container img {" +
      "margin: 20px auto;" +
      "width: 300px;" +
      "height: 110px;" +
    "}" +
     "h1 {" +
      " text-align: center;" +
      " color: #d7d2cb;" +
      " font-family: 'Montserrat-SemiBold';" +
      " font-size: 2rem;" +
     " font-style: italic;" +
    " }" +
     "h3 {" +
      " text-align: center;" +
      " color: #d7d2cb;" +
       "font-family: 'Montserrat-Reg';" +
       "font-size: 1.5rem;" +
      " font-style: italic;" +
     "}" +
    " a {" +
     "  text-decoration: none;" +
     "}" +
     "h4 {" +
       "text-align: center;" +
      " color: #d7d2cb;" +
      " font-family: 'Montserrat-Reg';" +
      " font-size: 1.2rem;" +
      " font-style: italic;" +
    "}" +
  " </style>" +
  " <body>" +
    " <div class='login-container'>" +
      " <div class='login-logo-conctainer'>" +
        " <div class='gradient-border'>" +
           "<img src='/img/AOPCWHITEPNG.png' alt='AOPC' width='100%'' />" +

          " <h1>" +
            " WELCOME TO<br />ALFARDAN OYSTER <br />" +
            " PRIVILEGE CLUB" +
           "</h1>" +
          " <h3>REGISTRATION FORM</h3>" +
           "<a" +
             "href='https://www.alfardanoysterprivilegeclub.com/user-registration'" +
            " ><h4>" +
              " Click Here to Register in<br />Alfardan Oyster Privilege Club" +
            " </h4>" +
           "</a>" +
        " </div>" +
     "  </div>" +
    " </div>" +
  " </body>";
            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("app@alfardan.com.qa", "Oyster2023!");
                client.Send(message);
                client.Disconnect(true);
                status = "Successfully sent registration email";

            }

            return Json(new { stats = status });
        }
        public IActionResult DownloadHeader()
        {
            var stream = new MemoryStream();
            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet 1");
                ws.Cells["A1"].Value = "Employee ID";
                ws.Cells["B1"].Value = "First Name";
                ws.Cells["C1"].Value = "Last Name";
                ws.Cells["D1"].Value = "Username";
                ws.Cells["E1"].Value = "Position Name";
                ws.Cells["F1"].Value = "Gender";
                ws.Cells["G1"].Value = "Email";


                ws.Cells["I1"].Style.Font.Italic = true;
                ws.Cells["I1"].Style.Font.Color.SetColor(Color.Red);
                ws.Cells["I1"].Value = "All Fields are required";
                for (var col = 1; col <= 10; col++)
                {
                    ws.Cells[1, col].Style.Font.Bold = true;
                }

                ws.Cells.AutoFitColumns();
                pck.Save();
            }

            stream.Position = 0;
            string excelName = "" + HttpContext.Session.GetString("CorporateName") + "-Registration-Template.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult AdminDownloadHeader()
        {
            var stream = new MemoryStream();
            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet 1");
                ws.Cells["A1"].Value = "Employee ID";
                ws.Cells["B1"].Value = "First Name";
                ws.Cells["C1"].Value = "Last Name";
                ws.Cells["D1"].Value = "Username";
                ws.Cells["E1"].Value = "Gender";
                ws.Cells["F1"].Value = "Email";


                ws.Cells["I1"].Style.Font.Italic = true;
                ws.Cells["I1"].Style.Font.Color.SetColor(Color.Red);
                ws.Cells["I1"].Value = "All Fields are required";
                for (var col = 1; col <= 10; col++)
                {
                    ws.Cells[1, col].Style.Font.Bold = true;
                }

                ws.Cells.AutoFitColumns();
                pck.Save();
            }

            stream.Position = 0;
            string excelName = "Admin-Registration-Template.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult CorporateDownloadHeader()
        {
            var stream = new MemoryStream();
            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet 1");
                ws.Cells["A1"].Value = "Employee ID";
                ws.Cells["B1"].Value = "First Name";
                ws.Cells["C1"].Value = "Last Name";
                ws.Cells["D1"].Value = "Username";
                ws.Cells["E1"].Value = "Position Name";
                ws.Cells["F1"].Value = "Gender";
                ws.Cells["G1"].Value = "Email";
                ws.Cells["H1"].Value = "Corporate Name";


                ws.Cells["k1"].Style.Font.Italic = true;
                ws.Cells["k1"].Style.Font.Color.SetColor(Color.Red);
                ws.Cells["k1"].Value = "All Fields are required";
                for (var col = 1; col <= 10; col++)
                {
                    ws.Cells[1, col].Style.Font.Bold = true;
                }

                ws.Cells.AutoFitColumns();
                pck.Save();
            }

            stream.Position = 0;
            string excelName = "Corporate-Admin-Registration-Template.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult Index()
        {
              string  token = token_.GetValue();
            if (token == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }
        public IActionResult Registration()
        {

            return View();
        }
        public IActionResult OTPVerification()
        {

            return View();
        }
        public IActionResult SuccessPage()
        {

            return View();
        }
    }
}
