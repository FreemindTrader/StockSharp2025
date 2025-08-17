// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.SpecialHeaderName
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

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
