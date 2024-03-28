using Business.Abstract;
using Business.Concrete;
using Core.Utilities;
using Core.Utilities.Helpers;
using Couchbase.Extensions.DependencyInjection;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using MudBlazor.Services;
using Serilog;
using Serilog.Sinks.Datadog.Logs;
using Serilog.Sinks.SystemConsole.Themes;
using System.Data;
using System.Globalization;
using WebUI.Authentication;
using WebUI.Services;
using WebUI.Utilities.Helpers;
using static Utilities.Extensions.DependencyInjectionExtensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddHubOptions(opt =>
{
    opt.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
    opt.HandshakeTimeout = TimeSpan.FromSeconds(30);
});



#region database

builder.Services.AddTransient<IDbConnection>(sql => new SqlConnection(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCouchbase(opt =>
{
    opt.ConnectionString = "couchbases://cb.butoerculhxhiieq.cloud.couchbase.com";
    opt.UserName = "accountantautomation";
    opt.Password = "Baristopal,02";
    opt.Buckets = ["Data"];
    opt.TracingOptions.Enabled = false;
    opt.ThresholdOptions.Enabled = false;
    opt.OrphanTracingOptions.Enabled = false;
    opt.LoggingMeterOptions.Enabled(false);
    opt.QueryTimeout = TimeSpan.FromSeconds(30);
    opt.SearchTimeout = TimeSpan.FromSeconds(30);
});

#endregion

#region Serilog

Log.Logger = new LoggerConfiguration()
    .WriteTo.DatadogLogs(
    apiKey: "d2ad5f5ed765d175124ddbc553fd963c",
    configuration: new DatadogConfiguration() { Url = "https://http-intake.logs.us5.datadoghq.com", Port = 443 })
            .WriteTo.Console(theme: SystemConsoleTheme.Colored
            , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();



#endregion


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
        options.ReturnUrlParameter = "returnUrl";
    });



builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();


builder.Host.UseSerilog();

builder.Services.AddAuthorization();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddMudServices();

ServiceTool.Create(builder.Services);

#region IoC
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
builder.Services.AddScoped<IMongoDbHelper, MongoDbHelper>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MessageboxHelper>();
builder.Services.AddScoped<IProductTrackingDal, ProductTrackingDal>();
builder.Services.AddScoped<IProductTrackingService, ProductTrackingManager>();
builder.Services.AddScoped<ICheckDal, CheckDal>();
builder.Services.AddScoped<ICheckService, CheckManager>();
builder.Services.AddScoped<IInstantDal, InstantDal>();
builder.Services.AddScoped<IInstantService, InstantManager>();
builder.Services.AddScoped<IRepositoryBase, DapperRepositoryBase>();

#endregion

var culture = new CultureInfo("tr-TR", true);
culture.NumberFormat.NumberDecimalSeparator = ",";
culture.NumberFormat.CurrencyDecimalSeparator = ",";

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
app.UseCookiePolicy();

app.UseAntiforgery();

app.UseAuthorization();

app.UseWebSockets();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.MapRazorComponents<WebUI.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
