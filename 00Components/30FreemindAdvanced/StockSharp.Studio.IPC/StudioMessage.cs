// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.StudioMessage
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F4CDD47D-561A-463F-994A-61FC038C2B5F
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.IPC.xml

using Ecng.Serialization;

#nullable disable
namespace StockSharp.Studio.IPC;

/// <summary>Base IPC message.</summary>
public abstract class StudioMessage : IPersistable
{
    internal bool IsResponse { get; set; }

    internal long TransactionId { get; set; }

    /// <summary>Product id of message senders product.</summary>
    public long From { get; internal set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{this.GetType().Name}, {(this.IsResponse ? "response" : "request")}, from={this.From}, trans={this.TransactionId}";
    }

    /// <inheritdoc />
    public virtual void Load(SettingsStorage ss)
    {
        this.IsResponse = ss.GetValue<bool>("IsResponse", false);
        this.TransactionId = ss.GetValue<long>("TransactionId", 0L);
        this.From = ss.GetValue<long>("From", 0L);
    }

    /// <inheritdoc />
    public virtual void Save(SettingsStorage ss)
    {
        ss.Set<bool>("IsResponse", this.IsResponse).Set<long>("TransactionId", this.TransactionId).Set<long>("From", this.From);
    }
}
