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
using System.Xml;
using System.Drawing;
using AuthSystem.Manager;
using _CMS.Manager;
using System.Drawing.Imaging;
using static AOPC.Controllers.BusinessController;

namespace AOPC.Controllers
{
    public class VendorController : Controller
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
        private XmlTextReader xmlreader;
        public readonly QueryValueService token_;
        public VendorController(
             GlobalService globalService, IOptions<AppSettings> appSettings, IWebHostEnvironment _environment,
                  UserManager<ApplicationUser> userManager, QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
            _globalService = globalService;
            _userManager = userManager;
            UserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
            Environment = _environment;
            token_ = _token;
        }
   
        [HttpGet]
        public async Task<JsonResult> GetVendorList()
        {
            var url = DBConn.HttpString + "/api/ApiVendor/VendorList";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
            string response = await client.GetStringAsync(url);
            List<VendorVM> models = JsonConvert.DeserializeObject<List<VendorVM>>(response);
            return new(models);
        }
        public class Deletebloc
        {

            public int Id { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> GetVendorGallery(Deletebloc data)
        {

            string result = "";
            var list = new List<BusinessArray>();
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiVendor/VendorGallery";
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
        public async Task<IActionResult> SaveVendorInfo(VendorModel data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiVendor/SaveVendor";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_.GetValue());
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(url, content))
                {
                    _global.Status = await response.Content.ReadAsStringAsync();
                }
            }

            catch (Exception ex)
            {
                string status = ex.GetBaseException().ToString();//
            }
            return Json(new { stats = _global.Status });
        }
        public class DeleteVen
        {

            public int Id { get; set; }
        }
        public class Registerstats
        {
            public string Status { get; set; }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteVendorInfo(DeleteVen data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var url = DBConn.HttpString + "/api/ApiVendor/DeleteVendor";
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

                        string file = Path.Combine(uploadsFolder, Request.Form.Files[i].FileName);
                        var stream = new FileStream(file, FileMode.Create);
                        Request.Form.Files[i].CopyToAsync(stream);

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
                    ViewData["Message"] = "Error: Invalid file.";
                    string filename = $"{hostingEnvironment.WebRootPath}\\excel\\{file.FileName}";
                    using (FileStream fileStream = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    IExcelDataReader reader = null;
                    FileStream stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read);

                    if (file.FileName.EndsWith("xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    if (file.FileName.EndsWith("xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    int i = 0;

                    var data = new List<VendorModel>();

                    while (reader.Read())
                    {
                        i++;

                        if (i > 1)
                        {
                            string VendorName = reader.GetValue(0) == null ? "none" : reader.GetValue(0).ToString();

                            string Description = reader.GetValue(1) == null ? "none" : reader.GetValue(1).ToString();

                            string Services = reader.GetValue(2) == null ? "none" : reader.GetValue(2).ToString();

                            string WebsiteUrl = reader.GetValue(3) == null ? "none" : reader.GetValue(3).ToString();

                            string Cno = reader.GetValue(4) == null ? "none" : reader.GetValue(4).ToString();

                            string Email = reader.GetValue(5) == null ? "none" : reader.GetValue(5).ToString();

                            string Address = reader.GetValue(6) == null ? "none" : reader.GetValue(6).ToString();

                            string Map = reader.GetValue(7) == null ? "none" : reader.GetValue(7).ToString();


                            data.Add(new VendorModel
                            {

                                VendorName = VendorName,
                                BusinessTypeId = 1,
                                Description = Description,
                                Services = Services,
                                WebsiteUrl = WebsiteUrl,
                                FeatureImg = "",
                                Gallery = "",
                                Email = Email,
                                Cno = Cno,
                                VideoUrl= "",
                                VrUrl = "",
                                BusinessLocationID = "",
                                Status = 5

                            });
                        }
                    }
                    reader.Close();
                    System.IO.File.Delete(filename);

                    //Send Data to API
                    var status = "";
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYWFhYWFhYWFhYWFhYSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdmVyc2lvbiI6IlYzLjUiLCJuYmYiOjE2NzQwODA5NDAsImV4cCI6MTY4MjcyMDk0MCwiaWF0IjoxNjc0MDgwOTQwfQ.D8avRMxzgrtZN-ElAxaac_sooXiGwg1gvANv4ybpLlg");
                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        using (var response = await client.PostAsync(DBConn.HttpString + "/api/ApiVendor/Import", content))
                        {
                            status = await response.Content.ReadAsStringAsync();
                        }
                    }

                    ViewData["Message"] = "New Entry" + status;
                }
                else
                {
                    ViewData["Message"] = "Error: Invalid file.";
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
                ws.Cells["A1"].Value = "VendorName";
                ws.Cells["B1"].Value = "Description";
                ws.Cells["C1"].Value = "Services";
                ws.Cells["D1"].Value = "WebsiteUrl";
                ws.Cells["E1"].Value = "Cno";
                ws.Cells["F1"].Value = "Email";
                ws.Cells["G1"].Value = "Address";
                ws.Cells["H1"].Value = "Map";
               

                ws.Cells["K1"].Style.Font.Italic = true;
                ws.Cells["K1"].Style.Font.Color.SetColor(Color.Red);
                ws.Cells["K1"].Value = "All Fields are required";
                for (var col = 1; col <= 10; col++)
                {
                    ws.Cells[1, col].Style.Font.Bold = true;
                }

                ws.Cells.AutoFitColumns();
                pck.Save();
            }

            stream.Position = 0;
            string excelName = "Vendor-Rgeistration-Template.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        
        public IActionResult Index()
        {
            string token = token_.GetValue();
            if (token == null)
            {
                return RedirectToAction("Index", "LogIn");
            }
            return View();
        }

    }
}
