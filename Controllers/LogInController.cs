using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using AuthSystem.ViewModel;
using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using AuthSystem.Services;
using System.Diagnostics;
using static Humanizer.On;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Options;
using System.Text;
using System.IO;
using System;
using System.Data;
using AuthSystem.Manager;
using _CMS.Manager;
namespace AOPC.Controllers
{

    public class LogInController : Controller
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
        private readonly QueryValueService token_val;
        public LogInController(
             GlobalService globalService, IOptions<AppSettings> appSettings, IWebHostEnvironment _environment,
                  UserManager<ApplicationUser> userManager,QueryValueService _token,
                  IHttpContextAccessor contextAccessor,
                  IConfiguration configuration)
        {
            _globalService = globalService;
            _userManager = userManager;
             token_val = _token;
            UserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
            _appSettings = appSettings.Value;
            Environment = _environment;

        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(UserModel data)
        {

       

            status = await LogIn(data);
         
            if (status == "Successfully Log In")
            {
                if (HttpContext.Session.GetString("UserType") == "Corporate")
                {
                    return Json(new { redirectToUrl = Url.Action("CorporateIndex", "Corporate") });
                }
                else if (HttpContext.Session.GetString("UserType") == "ADMIN")
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Dashboard") });
                }
                else if (HttpContext.Session.GetString("UserType") == "User Level")
                {
                    status = "Invalid Login";
                     return Json(new { redirectToUrl = Url.Action("Index", "LogIn") });
                     
                }
            }
            else
            {
                
            }
            return Json(new { stats = status });

        }
        public class LoginStats
        {
            public string Status { get; set; }

        }
        public async Task<String> LogIn(UserModel data)
        {
            string result = "";
            try
            {
                //var pass3 = Cryptography.Decrypt("8UFD7eD4sGtZ9r7Y1QXOc5qaxX7LBbkxTOEXLSlAZj0=");
                string sql = $@"SELECT        UsersModel.Id, UsersModel.Username, UsersModel.Password, UsersModel.Fname, UsersModel.Lname, UsersModel.Active, tbl_UserTypeModel.UserType, tbl_CorporateModel.CorporateName, 
                         tbl_PositionModel.Name AS PositionName, UsersModel.JWToken, UsersModel.FilePath, UsersModel.CorporateID, tbl_MembershipModel.Name as MembershipName
FROM            UsersModel INNER JOIN
                         tbl_UserTypeModel ON UsersModel.Type = tbl_UserTypeModel.Id INNER JOIN
                         tbl_CorporateModel ON UsersModel.CorporateID = tbl_CorporateModel.Id INNER JOIN
                         tbl_PositionModel ON UsersModel.PositionID = tbl_PositionModel.Id INNER JOIN
                         tbl_MembershipModel ON tbl_CorporateModel.MembershipID = tbl_MembershipModel.Id
                        WHERE        (UsersModel.Username = '" + data.Username + "' COLLATE Latin1_General_CS_AS) and (UsersModel.Password = '" + Cryptography.Encrypt(data.Password) + "' COLLATE Latin1_General_CS_AS) AND (UsersModel.Active = 1)";
                DataTable dt = db.SelectDb(sql).Tables[0];
                if (dt.Rows.Count != 0)
                {
                            HttpContext.Session.SetString("Name", dt.Rows[0]["Fname"].ToString() + dt.Rows[0]["Lname"].ToString());
                            HttpContext.Session.SetString("Position", dt.Rows[0]["PositionName"].ToString());
                            HttpContext.Session.SetString("UserType", dt.Rows[0]["UserType"].ToString());
                            HttpContext.Session.SetString("CorporateName", dt.Rows[0]["CorporateName"].ToString());
                            HttpContext.Session.SetString("CorporateID", dt.Rows[0]["CorporateID"].ToString());
                            HttpContext.Session.SetString("Id", dt.Rows[0]["Id"].ToString());
                            HttpContext.Session.SetString("MembershipName", dt.Rows[0]["MembershipName"].ToString());
                            if (dt.Rows[0]["FilePath"].ToString() == null || dt.Rows[0]["FilePath"].ToString() == string.Empty)
                            {
                                HttpContext.Session.SetString("ImgUrl", "https://www.alfardanoysterprivilegeclub.com/assets/img/defaultavatar.png");
                            }
                            else
                            {
                                HttpContext.Session.SetString("ImgUrl", dt.Rows[0]["FilePath"].ToString());
                            }
                            HttpClient client = new HttpClient();
                            var url = DBConn.HttpString + "/api/ApiLogIn/LogIn";
                            var token = _global.GenerateToken(data.Username, _appSettings.Key.ToString());
               
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token_val.GetValue()); 
                            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                            using (var response = await client.PostAsync(url, content))
                            {

                                status = await response.Content.ReadAsStringAsync();
                                //List<LoginStats> models = JsonConvert.DeserializeObject<List<LoginStats>>(status);
                                string asdas = JsonConvert.DeserializeObject<LoginStats>(status).Status;
                                result = asdas;
                                

                            }
                            if(result == "Successfully Log In")
                            {
                                HttpContext.Session.SetString("Bearer", token.ToString());
                                string test = token_val.GetValue();
                                token_val.GetValue();

                                
                            }
              
                }
                else
                {
                    result = "Invalid Log IN";
                }
                   
                    
            }

            catch (Exception ex)
            {
                status = ex.GetBaseException().ToString();
            }
            return result;
        

        }
 
        // Displays the index of the current user.
        public IActionResult Index()
        {
           string token = HttpContext.Session.GetString("Bearer");
            if (token != "")
            {
                 if (HttpContext.Session.GetString("UserType") == "Corporate")
                {
                     return RedirectToAction("CorporateIndex", "Corporate");   
                }
                else if (HttpContext.Session.GetString("UserType") == "ADMIN")
                {
                    return RedirectToAction("Index", "CMS");   
                }
               
                
            }
            else
            {
                        return View();
            }
            return View();
        }
        public IActionResult Logout()
        {
          HttpContext.Session.SetString("Bearer","");
          return RedirectToAction("Index", "LogIn");
        }
    }
}
