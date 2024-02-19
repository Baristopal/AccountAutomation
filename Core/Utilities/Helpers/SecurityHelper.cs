namespace Library.Core.Utilities.Helpers;

public static class SecurityHelper
{
    public static string GeneratePassword()
    {
        return Guid.NewGuid()
            .ToString()
            .Replace("-", "")
            .Substring(3, 11);
    }
}
