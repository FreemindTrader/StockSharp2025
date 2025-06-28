using Ecng.Serialization;
using System.Security;

#nullable disable
namespace StockSharp.Studio.Core.Remote;

public class RunnerRemoteStrategySettings : RemoteStrategySettings
{
    public string AppId { get; set; }

    public SecureString Token { get; set; }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this.AppId = storage.GetValue<string>("AppId", (string)null);
        this.Token = storage.GetValue<SecureString>("Token", (SecureString)null);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.Set<string>("AppId", this.AppId).Set<SecureString>("Token", this.Token);
    }
}
