using Microsoft.AspNetCore.Components.Web;

namespace WebUI.Utilities.Extentions;

public static class KeyboardEventArgsExtentions
{
    public static bool IsEnter(this KeyboardEventArgs e)
    {
        if (e.Key.Contains("Enter") || e.Code.Contains("Enter"))
            return true;

        return false;

    }
}
