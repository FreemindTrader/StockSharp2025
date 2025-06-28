// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.MsgError
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F4CDD47D-561A-463F-994A-61FC038C2B5F
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.xml

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Studio.IPC;

/// <summary>
/// Response which means that there was error during message handling on server side.
/// </summary>
public class MsgError : StudioMessage
{
    /// <summary>Error message.</summary>
    public string ErrorMessage { get; set; }

    /// <summary>Is cancellation.</summary>
    public bool IsCancellation { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString() + $", msg={this.ErrorMessage}, cancel={this.IsCancellation}";
    }

    /// <inheritdoc />
    public override void Load(SettingsStorage ss)
    {
        base.Load(ss);
        this.ErrorMessage = ss.GetValue<string>("ErrorMessage", (string)null);
        this.IsCancellation = ss.GetValue<bool>("IsCancellation", false);
    }

    /// <inheritdoc />
    public override void Save(SettingsStorage ss)
    {
        base.Save(ss);
        ss.Set<string>("ErrorMessage", this.ErrorMessage);
        ss.Set<bool>("IsCancellation", this.IsCancellation);
    }
}
