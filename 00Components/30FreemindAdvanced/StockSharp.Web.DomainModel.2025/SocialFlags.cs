// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.SocialFlags
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using System;

namespace StockSharp.Web.DomainModel
{
    [Flags]
    public enum SocialFlags : long
    {
        OAuth = 1,
        Blog = 2,
        Register = 4,
        ApproveByOAuth = 8,
        ApproveByCode = 16, // 0x0000000000000010
    }
}
