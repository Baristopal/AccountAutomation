using Business.Abstract;
using Business.Concrete;
using Core.Utilities;
using Core.Utilities.Helpers;
using Couchbase.Extensions.DependencyInjection;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using System.Data;
using System.Data.SqlClient;
using WebUI.Services;
using WebUI.Utilities.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();


builder.Services.AddTransient<IDbConnection>(sql => new SqlConnection(builder.Configuration.GetConnectionString("default")));
builder.Services.AddMudServices();

builder.Services.AddCouchbase(opt =>
{
    opt.ConnectionString = "couchbase://localhost";
    opt.UserName = "Administrator";
    opt.Password = "Bb5b_FP8ve7s_K";
});
builder.Services.AddScoped<MessageboxHelper>();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//        .AddCookie(options =>
//        {
//            options.LoginPath = "/Auth/Login";
//            options.LogoutPath = "/Auth/Logout";
//            options.AccessDeniedPath = "/Auth/Forbidden";
//            options.ReturnUrlParameter = "returnUrl";
//            options.Cookie.Name = "auth";
//            options.Cookie.Path = "/";
//            options.Cookie.HttpOnly = true;
//        });



builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IPayrollService, PayrollManager>();
builder.Services.AddScoped<IPayrollDal, PayrollDal>();
builder.Services.AddScoped<IPersonnelService, PersonnelManager>();
builder.Services.AddScoped<IPersonnelDal, PersonnelDal>();
builder.Services.AddScoped<ICompanyService, CompanyManager>();
builder.Services.AddScoped<ICompanyDal, CompanyDal>();
builder.Services.AddScoped<IDefinationDal, DefinationDal>();
builder.Services.AddScoped<IDefinationService, DefinationManager>();
builder.Services.AddScoped<INoSqlHelper, CouchbaseHelper>();
builder.Services.AddScoped<IDataService, DataManager>();
builder.Services.AddScoped<IDataDal, DataDal>();



//DependencyInjectionExtensions.ServiceTool.Create(builder.Services);
builder.Services.AddScoped<AuthService>();
//builder.Services.AddScoped<AuthStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.MapRazorComponents<WebUI.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
