// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.MsgPublish
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F4CDD47D-561A-463F-994A-61FC038C2B5F
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Studio.IPC;

/// <summary>Message from app to publish content.</summary>
public class MsgPublish : StudioMessage
{
    /// <summary>
    /// <see cref="T:StockSharp.Web.DomainModel.ProductGroup" />.
    /// </summary>
    public long[] Groups { get; set; }

    /// <summary>User id of content.</summary>
    public string UserId { get; set; }

    /// <summary>Name.</summary>
    public string Name { get; set; }

    /// <summary>Description.</summary>
    public string Description { get; set; }

    /// <summary>Content type.</summary>
    public ProductContentTypes2 ContentType { get; set; }

    /// <summary>Content path.</summary>
    public string ContentPath { get; set; }

    /// <summary>Icon identifier.</summary>
    public long IconId { get; set; }

    /// <inheritdoc />
    public override void Load(SettingsStorage ss)
    {
        base.Load(ss);
        this.Groups = ss.GetValue<long[]>("Groups", (long[])null);
        this.UserId = ss.GetValue<string>("UserId", (string)null);
        this.Name = ss.GetValue<string>("Name", (string)null);
        this.Description = ss.GetValue<string>("Description", (string)null);
        this.IconId = ss.GetValue<long>("IconId", 0L);
        this.ContentType = ss.GetValue<ProductContentTypes2>("ContentType", (ProductContentTypes2)0);
        this.ContentPath = ss.GetValue<string>("ContentPath", (string)null);
    }

    /// <inheritdoc />
    public override void Save(SettingsStorage ss)
    {
        base.Save(ss);
        ss.Set<long[]>("Groups", this.Groups).Set<string>("UserId", this.UserId).Set<string>("Name", this.Name).Set<string>("Description", this.Description).Set<long>("IconId", this.IconId).Set<ProductContentTypes2>("ContentType", this.ContentType).Set<string>("ContentPath", this.ContentPath);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        string str1 = base.ToString();
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 5);
        interpolatedStringHandler.AppendLiteral(", name='");
        interpolatedStringHandler.AppendFormatted(this.Name);
        interpolatedStringHandler.AppendLiteral("', icon=");
        interpolatedStringHandler.AppendFormatted<long>(this.IconId);
        interpolatedStringHandler.AppendLiteral(", type=");
        interpolatedStringHandler.AppendFormatted<ProductContentTypes2>(this.ContentType);
        interpolatedStringHandler.AppendLiteral(", path='");
        interpolatedStringHandler.AppendFormatted(this.ContentPath);
        interpolatedStringHandler.AppendLiteral("', Groups=(");
        ref DefaultInterpolatedStringHandler local = ref interpolatedStringHandler;
        long[] groups = this.Groups;
        string str2 = groups != null ? StringHelper.JoinCommaSpace(((IEnumerable<long>)groups).Select<long, string>((Func<long, string>)(u => u.ToString()))) : (string)null;
        local.AppendFormatted(str2);
        interpolatedStringHandler.AppendLiteral(")");
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        return str1 + stringAndClear;
    }
}
