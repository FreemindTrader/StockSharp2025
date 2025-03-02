// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.MsgError
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65FF0FD2-B114-4B6B-959A-42B33214A877
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.IPC.dll

using Ecng.Serialization;
using System.Runtime.CompilerServices;

namespace StockSharp.Studio.IPC
{
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
            string str = base.ToString();
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
            interpolatedStringHandler.AppendLiteral( ", msg=" );
            interpolatedStringHandler.AppendFormatted( this.ErrorMessage );
            interpolatedStringHandler.AppendLiteral( ", cancel=" );
            interpolatedStringHandler.AppendFormatted<bool>( this.IsCancellation );
            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
            return str + stringAndClear;
        }

        /// <inheritdoc />
        public override void Load( SettingsStorage ss )
        {
            base.Load( ss );
            this.ErrorMessage = ( string ) ss.GetValue<string>( "ErrorMessage",  null );
            this.IsCancellation = ( bool ) ss.GetValue<bool>( "IsCancellation",  false );
        }

        /// <inheritdoc />
        public override void Save( SettingsStorage ss )
        {
            base.Save( ss );
            ss.Set<string>( "ErrorMessage",  this.ErrorMessage );
            ss.Set<bool>( "IsCancellation",  ( this.IsCancellation) );
        }
    }
}
