using AspectCore.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities.Extensions;
using System.Security.Claims;
using Core.Extensions;

namespace Core.Aspects.AspectCore;

public class AuthorizeAspect : AbstractInterceptorAttribute
{
    private readonly string[] _roles;
    private readonly ILogger _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizeAspect(string roles)
    {
        _httpContextAccessor = DependencyInjectionExtensions.ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        _logger = DependencyInjectionExtensions.ServiceTool.ServiceProvider.GetService<ILoggerFactory>().CreateLogger<ILogger>();
        _roles = roles.Split(',');
    }
    public override async Task Invoke(AspectContext context, AspectDelegate next)
    {

        var userClaimRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();

        foreach (var role in _roles)
        {
            if (userClaimRoles.Contains(role) == true)
            {
                await next(context);

                string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string userIP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                string methodName = $"{context.ImplementationMethod.DeclaringType.FullName}.{context.ImplementationMethod.Name}";

                string param = string.Join(',', context.Parameters);
                string message = $"Method:{methodName} Params:{param} userId:{userId} userIp:{userIP}";

                _logger.LogWarning("Authorizing error {message}", message);
                return;
            }
        }

        throw new ApplicationException("you are not authorized");
    }
}
