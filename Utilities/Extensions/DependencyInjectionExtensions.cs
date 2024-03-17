using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Utilities.Extensions;

public static class DependencyInjectionExtensions
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();

            return services;
        }
    }

    public static void SetHttpContext(int companyId)
    {
        var httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.Name, "Tommy Life"),
                 new Claim(ClaimTypes.Email, "hasantopal0234@gmail.com"),
                 new Claim("CompanyId", "1001"),
        })));

        //daha önce set etmişmiyiz kontrol ediyorum.
        if (httpContextAccessor.HttpContext is null)
        {
            var context = new CustomHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                {
                    new Claim("CompanyId", companyId.ToString()),
                }))
            };
            httpContextAccessor.HttpContext = context;
        }
        else
        {
            var companyClaim = httpContextAccessor.HttpContext.User.FindFirst("CompanyId");

            var claims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;


            //daha önce set etmişsek silip yeni merchantıd yi gönderiyoruz.
            //bunu yapmamızın sebebi trendyol servisi bütün satıcılar adına işlem yaptığı için:
            if (companyClaim != null)
                claims.TryRemoveClaim(companyClaim);

            claims.AddClaim(new Claim("CompanyId", companyId.ToString()));
        }

    }

    private class CustomHttpContext : HttpContext
    {
        public override IFeatureCollection Features => null;

        public override HttpRequest Request => null;

        public override HttpResponse Response => null;

        public override ConnectionInfo Connection => null;

        public override WebSocketManager WebSockets => null;


        public override ClaimsPrincipal User { get; set; }
        public override IDictionary<object, object> Items { get; set; }
        public override IServiceProvider RequestServices { get; set; }
        public override CancellationToken RequestAborted { get; set; }
        public override string TraceIdentifier { get; set; }
        public override ISession Session { get; set; }

        public override void Abort() { }

    }
}
