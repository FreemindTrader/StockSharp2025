// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.AspNetPath
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using Ecng.Common;

#nullable disable
namespace StockSharp.Web.Common;

public static class AspNetPath
{
    private static string Domain(string domainCode)
    {
        return !AspNetPath.IsLocalhost ? "https://stocksharp." + domainCode : "http://localhost/stocksharp";
    }

    public static bool IsLocalhost { get; set; }

    public static string ToFullAbsolute(this string virtualPath, string domainCode, bool urlEscape = true)
    {
        if (!StringHelper.StartsWithIgnoreCase(virtualPath, "http"))
            virtualPath = !StringHelper.StartsWithIgnoreCase(virtualPath, "/stocksharp") ? (!virtualPath.StartsWith("/") ? (!virtualPath.StartsWith("~") ? $"{AspNetPath.Domain(domainCode)}/{virtualPath}" : virtualPath.Replace("~", AspNetPath.Domain(domainCode))) : AspNetPath.Domain(domainCode) + virtualPath) : "http://localhost" + virtualPath;
        if (urlEscape)
            virtualPath = StringHelper.UrlEscape(virtualPath);
        return virtualPath;
    }
}
