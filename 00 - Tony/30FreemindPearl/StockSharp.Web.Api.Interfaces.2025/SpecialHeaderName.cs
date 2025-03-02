// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.SpecialHeaderName
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using System;

namespace StockSharp.Web.Api.Interfaces
{
    public static class SpecialHeaderName
    {
        [Obsolete]
        public const string AuthToken = "SS-AUTH";
        [Obsolete]
        public const string AuthLogin = "SS-LGN";
        [Obsolete]
        public const string AuthPwd = "SS-PWD";
        [Obsolete]
        public const string AuthPwd64 = "SS-PWD-64";
        public const string Address = "SS-ADDR";
        public const string AsUser = "SS-ASU";
        public const string Extended = "SS-EX";
        public const string ErrorInfo = "SS-EI";
        public const string ErrorInfo64 = "SS-EI-64";
    }
}
