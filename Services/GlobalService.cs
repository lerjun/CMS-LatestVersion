using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace AuthSystem.Services
{
    public class GlobalService
    { 
        private readonly UserManager<ApplicationUser> _userManager;
        public static string UserId;
        private IConfiguration _configuration;
        private string apiUrl = "http://";

        public GlobalService( 
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IConfiguration configuration)
        {
            _userManager = userManager;
            UserId = _userManager.GetUserId(contextAccessor.HttpContext.User);
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:WebApiURL");
        }

        public async Task<String> ApiGet(string token, string uri)
        {
            var url = apiUrl + uri;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string response = await client.GetStringAsync(url);

            return response;
        }

        public async Task<String> ApiPost<T>(string token, string uri, T entity) where T : class
        {
            string status = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(apiUrl + uri, content))
                {
                    status = await response.Content.ReadAsStringAsync();
                }
            }
           
            return status;
        }

        public async Task<String> ApiDelete(string token, string uri, int id)
        {
            string status = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var response = await client.DeleteAsync(apiUrl + uri + id))
                {
                    status = await response.Content.ReadAsStringAsync();
                }
            }

            return status;
        }

        public async Task<String> generateBearerToken(string user) 
        {
            var url = apiUrl + "/api/jwt/" + user;
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);         
            string result = JsonConvert.DeserializeObject<GlobalModel>(response).Token;
            return result;
        }

        public async Task<int> PageCheck(string pagelink, string token = "")
        {
            int result = 0;
            try
            {
                var model = new GlobalModel();
                model.PageLink = pagelink;
                model.UserId = UserId;

                string status = "0";

                status = await ApiPost(token, "/api/pagecheck/", model);
                result = Int32.Parse(status);

            }
            catch
            {
                result = 0;
            }
            return result;

        }
        public async Task<int> ButtonCheck(string pagelink, string token = "")
        {
            int result = 0;
            try
            {
                var model = new GlobalModel();
                model.PageLink = pagelink;
                model.UserId = UserId;

                string status = "0";

                status = await ApiPost(token, "/api/buttoncheck/", model);
                result = Int32.Parse(status);

            }
            catch
            {
                result = 0;
            }
            return result;

        }

      
        public async Task<int> GetEmployeeIdByUser(string userId, string token)
        {
   
            int employeeId = 0;
            try
            {
                var url = apiUrl + "/api/p_employee/employeeId/" + userId;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string response = await client.GetStringAsync(url);
                employeeId = Int32.Parse(response);
            }
            catch
            {
                employeeId = 0;
            }
            return employeeId;
        }

        public async Task<String> GetUserByEmployeeId(string employeeId, string token)
        {

            string userId = "";
            try
            {
                var url = apiUrl + "/api/p_employee/userid/" + employeeId;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                userId = await client.GetStringAsync(url);
            }
            catch
            {
                userId = "";
            }
            return userId;
        }

        public async Task<String> GetSettingVal(string p_name, string token)
        {

            string pval = "";
            try
            {
                var url = apiUrl + "/api/getsettingval/" + p_name;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                pval = await client.GetStringAsync(url);
            }
            catch
            {
                pval = "";
            }
            return pval;
        }

        public List<String> GetAllSubordinate(int id, string token)
        {
            List<String> result = new List<String>();
            try
            {
                var url = apiUrl + "/api/AssignEmployee/getsub/" + id;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string response = client.GetStringAsync(url).Result;
                result = JsonConvert.DeserializeObject<List<String>>(response);
            }
            catch
            {
                result = new List<String>();
            }
            return result;
        }
    }
}
