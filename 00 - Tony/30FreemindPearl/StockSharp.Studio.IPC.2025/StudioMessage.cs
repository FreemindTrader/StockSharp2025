// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.IPC.StudioMessage
// Assembly: StockSharp.Studio.IPC, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65FF0FD2-B114-4B6B-959A-42B33214A877
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.IPC.dll

using Ecng.Serialization;
using System.Runtime.CompilerServices;

namespace StockSharp.Studio.IPC
{
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
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 4);
            interpolatedStringHandler.AppendFormatted( this.GetType().Name );
            interpolatedStringHandler.AppendLiteral( ", " );
            interpolatedStringHandler.AppendFormatted( this.IsResponse ? "response" : "request" );
            interpolatedStringHandler.AppendLiteral( ", from=" );
            interpolatedStringHandler.AppendFormatted<long>( this.From );
            interpolatedStringHandler.AppendLiteral( ", trans=" );
            interpolatedStringHandler.AppendFormatted<long>( this.TransactionId );
            return interpolatedStringHandler.ToStringAndClear();
        }

        /// <inheritdoc />
        public virtual void Load( SettingsStorage ss )
        {
            this.IsResponse = ( bool ) ss.GetValue<bool>( "IsResponse",  false );
            this.TransactionId = ( long ) ss.GetValue<long>( "TransactionId",  0L );
            this.From = ( long ) ss.GetValue<long>( "From",  0L );
        }

        /// <inheritdoc />
        public virtual void Save( SettingsStorage ss )
        {
            ss.Set<bool>( "IsResponse",  ( this.IsResponse  ) ).Set<long>( "TransactionId",  this.TransactionId ).Set<long>( "From",  this.From );
        }
    }
}
