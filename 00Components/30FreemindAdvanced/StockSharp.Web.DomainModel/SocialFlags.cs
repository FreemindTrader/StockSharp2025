// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SocialFlags
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;

#nullable disable
namespace StockSharp.Web.DomainModel;

[Flags]
public enum SocialFlags : long
{
    OAuth = 1,
    Blog = 2,
    Register = 4,
    ApproveByOAuth = 8,
    ApproveByCode = 16, // 0x0000000000000010
}
