using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimsPrincipalExtentions
{
    public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        List<string> result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();


        return result;
    }


    public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims(ClaimTypes.Role);
    }
    //public static int GetMerchantId(this ClaimsPrincipal claimsPrincipal)
    //{
    //    _ = int.TryParse(claimsPrincipal.FindFirst("MerchantId")?.Value, out int merchantId);
    //    return merchantId;
    //}
    public static int GetCompanyId(this IHttpContextAccessor httpcontext)
    {
        int companyId = 0;

        if (httpcontext.HttpContext is not null)
            _ = int.TryParse(httpcontext.HttpContext.User.Claims.FirstOrDefault(s => s.Type.Contains("nameidentifier")).Value, out companyId);

        return companyId;
    }

    public static int GetUserId(this IHttpContextAccessor httpcontext)
    {
        int userId = 0;

        if (httpcontext.HttpContext is not null)
            _ = int.TryParse(httpcontext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);

        return userId;
    }
    public static string GetUserIP(this IHttpContextAccessor httpcontext)
    {
        string userIP = "";

        if (httpcontext.HttpContext is not null)
            userIP = httpcontext.HttpContext.Connection?.RemoteIpAddress?.ToString();

        return userIP;
    }

    public static string GetUserFullName(this IHttpContextAccessor httpcontext)
    {
        string userName = "";
        if (httpcontext.HttpContext is not null)
            userName = httpcontext.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

        return userName;
    }

    public static string GetUserFirstName(this IHttpContextAccessor httpcontext)
    {
        string userName = "";
        if (httpcontext.HttpContext is not null)
            userName = httpcontext.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;

        return userName;
    }
    public static string GetUserLastName(this IHttpContextAccessor httpcontext)
    {
        string userName = "";
        if (httpcontext.HttpContext is not null)
            userName = httpcontext.HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value;

        return userName;
    }
}
