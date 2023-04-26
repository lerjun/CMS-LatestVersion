using AuthSystem.Models;
using _CMS.Manager;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
builder.Services.AddSession();
builder.Services.AddTransient<QueryValueService>();
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider(); 
builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500", "http://192.168.100.90:8000", "http://192.168.100.83","http://127.0.0.1:8000","http://localhost:5083","http://172.31.17.67","https://cms.alfardanoysterprivilegeclub.com",
            "https://api.alfardanoysterprivilegeclub.com","https://www.alfardanoysterprivilegeclub.com","https://www.alfardanoysterprivilegeclub.com/assets/fonts/Montserrat/Montserrat-Regular.ttf")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LogIn}/{action=Index}/{id?}");

app.Run();
