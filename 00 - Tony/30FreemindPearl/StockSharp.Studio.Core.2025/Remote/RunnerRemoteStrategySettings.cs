// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Remote.RunnerRemoteStrategySettings
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Serialization;
using System.Security;

namespace StockSharp.Studio.Core.Remote
{
    public class RunnerRemoteStrategySettings : RemoteStrategySettings
    {
        public string AppId { get; set; }

        public SecureString Token { get; set; }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.AppId = ( string ) storage.GetValue<string>( "AppId",  null );
            this.Token = ( SecureString ) storage.GetValue<SecureString>( "Token",  null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Set<string>( "AppId",  this.AppId ).Set<SecureString>( "Token",  this.Token );
        }
    }
}
