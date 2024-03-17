using Business.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using System.Security.Claims;

namespace WebUI.Authentication;
// This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user 
// every 30 minutes an interactive circuit is connected.
internal sealed class CustomRevalidatingAuthenticationStateProvider(
        ILoggerFactory loggerFactory,
        IServiceScopeFactory scopeFactory)
    : RevalidatingServerAuthenticationStateProvider(loggerFactory)
{
    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    protected override async Task<bool> ValidateAuthenticationStateAsync(
        AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        bool userStatus = false;
        if (authenticationState?.User?.Identity?.IsAuthenticated == true)
        {
            var companyId = Convert.ToInt32(authenticationState.User.FindFirstValue(ClaimTypes.NameIdentifier));

            await using var scope = scopeFactory.CreateAsyncScope();
            var userService = scope.ServiceProvider.GetRequiredService<ICompanyService>();

            var user = await userService.GetCompanyById(companyId);

            if (user.Success == true || user?.Data is not null)
                userStatus = true;
        }


        return await Task.FromResult(userStatus);

    }
}
