// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.AspNetPath
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;

namespace StockSharp.Web.Common
{
    public static class AspNetPath
    {
        private static string Domain( string domainCode )
        {
            if ( !AspNetPath.IsLocalhost )
                return "https://stocksharp." + domainCode;
            return "http://localhost/stocksharp";
        }

        public static bool IsLocalhost { get; set; }

        public static string ToFullAbsolute( this string virtualPath, string domainCode, bool urlEscape = true )
        {
            if ( !StringHelper.StartsWithIgnoreCase( virtualPath, "http" ) )
                virtualPath = !StringHelper.StartsWithIgnoreCase( virtualPath, "/stocksharp" ) ? ( !virtualPath.StartsWith( "/" ) ? ( !virtualPath.StartsWith( "~" ) ? AspNetPath.Domain( domainCode ) + "/" + virtualPath : virtualPath.Replace( "~", AspNetPath.Domain( domainCode ) ) ) : AspNetPath.Domain( domainCode ) + virtualPath ) : "http://localhost" + virtualPath;
            if ( urlEscape )
                virtualPath = StringHelper.UrlEscape( virtualPath );
            return virtualPath;
        }
    }
}
