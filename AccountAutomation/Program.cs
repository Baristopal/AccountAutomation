using Business.Abstract;
using Business.Concrete;
using Core.Utilities;
using Core.Utilities.Helpers;
using Couchbase.Extensions.DependencyInjection;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Models;
using MudBlazor.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Datadog.Logs;
using Serilog.Sinks.SystemConsole.Themes;
using System.Text;
using WebUI.Services;
using WebUI.Utilities.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettigns"));

builder.Services.AddMudServices();

builder.Services.AddCouchbase(opt =>
{
    opt.ConnectionString = "couchbases://cb.butoerculhxhiieq.cloud.couchbase.com";
    opt.UserName = "accountantautomation";
    opt.Password = "Baristopal,02";
    opt.Buckets = new List<string>
    {
        "Data"
    };
    opt.TracingOptions.Enabled = false;
    opt.ThresholdOptions.Enabled = false;
    opt.OrphanTracingOptions.Enabled = false;
    opt.LoggingMeterOptions.Enabled(false);
    opt.QueryTimeout = TimeSpan.FromSeconds(30);
    opt.SearchTimeout = TimeSpan.FromSeconds(30);
});
builder.Services.AddScoped<MessageboxHelper>();


#region Serilog

string environment = $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}";

if (string.IsNullOrEmpty(environment))
    environment = "Production";
else
    Console.OutputEncoding = Encoding.UTF8;

LogEventLevel level = (environment == "Production" ? LogEventLevel.Warning : LogEventLevel.Information);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", level)
            .MinimumLevel.Override("Worker", LogEventLevel.Information)
            .MinimumLevel.Override("Host", level)
            .MinimumLevel.Override("System", level)
            .MinimumLevel.Override("Function", level)
            .MinimumLevel.Override("Azure.Core", level)
            .MinimumLevel.Override("Couchbase", LogEventLevel.Error)
            .MinimumLevel.Override("Azure.Core", LogEventLevel.Error)
            .MinimumLevel.Override("Azure.Messaging.ServiceBus", LogEventLevel.Warning)
    .WriteTo.DatadogLogs(
    apiKey: "d2ad5f5ed765d175124ddbc553fd963c",
    configuration: new DatadogConfiguration() { Url = "https://http-intake.logs.datadoghq.com" })
     .WriteTo.Console(theme: SystemConsoleTheme.Colored
            , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();

#endregion


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
builder.Services.AddScoped<IMongoDbHelper, MongoDbHelper>();


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
