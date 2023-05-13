using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using AuthSystem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Data;

using CMS.Models;
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
using System.Xml;
using _CMS.Manager;
using System.Drawing;
using OfficeOpenXml;
using ExcelDataReader;
namespace AOPC.Controllers
{
    public class CorporateController : Controller
    {
         string status="";
         public readonly QueryValueService token_;
        private readonly AppSettings _appSettings;
        private ApiGlobalModel _global = new ApiGlobalModel();
        private GlobalService _globalService;
        DbManager db = new DbManager();
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private string apiUrl = "http://";
        private IWebHostEnvironment Environment;
        private readonly IWebHostEnvironment _environment;
        public static string UserId;
        private XmlTextReader xmlreader;
        public CorporateController(
             GlobalService globalService, IOptions<AppSettings> appSettings, IWebHostEnvironment _environment,
                  UserManager<ApplicationUser> userManager, QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
              token_ = _token;
            _globalService = globalService;
            _userManager = userManager;
            UserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
            Environment = _environment;
        }
        [HttpGet]
        public async Task<JsonResult> GetCorporateList()
        {
            var url = DBConn.HttpString + "/api/ApiCorporate/CompanyList";
            HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
           string response = await client.GetStringAsync(url);
            List<CorporateModelVM> model = JsonConvert.DeserializeObject<List<CorporateModelVM>>(response);
            return new(model);
        }
#region ModelView
 public class membershipid
        {

            public string Id { get; set; }
        }
        public class DeleteCorporate
        {

            public int Id { get; set; }
        }
           public class LoginStats
        {
            public string Status { get; set; }

        }
           public class CorporateID
        {
            public string Id { get; set; }

        }

        public class membershipprivilegedata
        {
            public int MembershipID { get; set; }
            public int Count { get; set; }
            public int VipCount { get; set; }
        }
#endregion

       
        [HttpPost]
        public async Task<IActionResult> DeleteCorporateInfo(DeleteCorporate data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiCorporate/DeleteCorproate";
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
        [HttpPost]
        public async Task<IActionResult> GetMembershipDescList(membershipid data)
        {

            List<MembershipModelVM> model = new List<MembershipModelVM>();
            HttpClient client = new HttpClient();
            var url = DBConn.HttpString + "/api/ApiMembership/MembershipFilterbyID";
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(url, content))
            {
                var res = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<MembershipModelVM>>(res);
            }


            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetCorporateAdminUserList(CorporateID data)
        {

            List<UserVM> model = new List<UserVM>();
            HttpClient client = new HttpClient();
            var url = DBConn.HttpString + "/api/ApiRegister/CorporateAdminUserList";
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(url, content))
            {
                var res = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<UserVM>>(res);
            }


            return Json(model);
        }
        [HttpGet]
        public async Task<JsonResult> GetMembershipList()
        {
            var url = DBConn.HttpString + "/api/ApiMembership/MembershipList";
            HttpClient client = new HttpClient();
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(  token_.GetValue()); 
            string response = await client.GetStringAsync(url);
            List<MembershipVM> model = JsonConvert.DeserializeObject<List<MembershipVM>>(response);
            return new(model);
        }
     
        [HttpPost]
        public async Task<IActionResult> SaveCorporate(CorporateModel data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiCorporate/UpdateCorporate";
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
        [HttpPost]
        public async Task<IActionResult> SaveMembershipPrivilegeCount(membershipprivilegedata data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiCorporate/UpdateMembershipPrivilege";
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
                    if (file.FileName.Contains("Corporate-Registration-Template"))
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

                        var data = new List<CorporateModel>();

                        while (reader.Read())
                        {
                            i++;

                            if (i > 1)
                            {

                                string sql = $@"select Id from tbl_MembershipModel where Name='" + reader.GetValue(5).ToString() + "'";
                                DataTable dt = db.SelectDb(sql).Tables[0];
                                var memid = "";
                                if (dt.Rows.Count > 0)
                                {
                                    memid = dt.Rows[0]["Id"].ToString();
                                }
                                string CorporateName = reader.GetValue(0) == null ? "none" : reader.GetValue(0).ToString();

                                string Address = reader.GetValue(1) == null ? "none" : reader.GetValue(1).ToString();

                                string CNo = reader.GetValue(2) == null ? "none" : reader.GetValue(2).ToString();

                                string EmailAddress = reader.GetValue(3) == null ? "none" : reader.GetValue(3).ToString();
                                string Description = reader.GetValue(4) == null ? "none" : reader.GetValue(4).ToString();

                                string MembershipID = reader.GetValue(5) == null ? "0" : memid;


                                data.Add(new CorporateModel
                                {
                                    CorporateName = CorporateName,
                                    Address = Address,
                                    CNo = CNo,
                                    EmailAddress = EmailAddress,
                                    Description = Description,
                                    MembershipID = MembershipID,
                                    Id = 0,
                                    Status = 1

                                });
                            }
                        }
                        reader.Close();

                        //Send Data to API
                        var status = "";
                        HttpClient client = new HttpClient();
                        var url = DBConn.HttpString + "/api/ApiCorporate/ImportCorporate";
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
        public IActionResult DownloadHeader()
        {
            var stream = new MemoryStream();
            using (var pck = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet 1");
                ws.Cells["A1"].Value = "Company Name";
                ws.Cells["B1"].Value = "Address";
                ws.Cells["C1"].Value = "Contact Number";
                ws.Cells["D1"].Value = "Email Address";
                ws.Cells["E1"].Value = "Description";
                ws.Cells["F1"].Value = "Tier";

                ws.Cells["I1"].Style.Font.Italic = true;
                ws.Cells["I1"].Style.Font.Color.SetColor(Color.Red);
                ws.Cells["J1"].Value = "All Fields are required";
                ws.Cells["J1"].Style.Font.Italic = true;
                ws.Cells["J1"].Style.Font.Color.SetColor(Color.Red);

                for (var col = 1; col <= 7; col++)
                {
                    ws.Cells[1, col].Style.Font.Bold = true;
                }

                ws.Cells.AutoFitColumns();
                pck.Save();
            }

            stream.Position = 0;
            string excelName = "Corporate-Registration-Template.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
        public IActionResult CorporateIndex()
        {
            string token = HttpContext.Session.GetString("Bearer");
            if (token == "")
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CorpAdminIndex(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
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
                                    CorporateID = int.Parse(HttpContext.Session.GetString("CorporateID")),
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
            return View("CorporateIndex");
        }

    }
}
