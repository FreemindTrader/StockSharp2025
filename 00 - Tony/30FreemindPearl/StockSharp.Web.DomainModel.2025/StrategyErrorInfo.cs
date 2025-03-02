// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyErrorInfo
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;

namespace StockSharp.Web.DomainModel
{
    public class StrategyErrorInfo : IPersistable
    {
        public string Message { get; set; }

        public string ElementId { get; set; }

        public string ElementType { get; set; }

        public string StackTrace { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Message = ( string ) storage.GetValue<string>( "Message", null );
            this.ElementId = ( string ) storage.GetValue<string>( "ElementId", null );
            this.ElementType = ( string ) storage.GetValue<string>( "ElementType", null );
            this.StackTrace = ( string ) storage.GetValue<string>( "StackTrace", null );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Message", this.Message ).Set<string>( "ElementId", this.ElementId ).Set<string>( "ElementType", this.ElementType ).Set<string>( "StackTrace", this.StackTrace );
        }
    }
}
