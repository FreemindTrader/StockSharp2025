// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ErrorCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public class ErrorCommand : BaseStudioCommand
    {
        public Exception Error { get; }

        public string Message { get; }

        public ErrorCommand( Exception error )
        {
            Exception exception = error;
            if ( exception == null )
                throw new ArgumentNullException( nameof( error ) );
            Error = exception;
            Message = error.Message;
        }

        public ErrorCommand( string message )
        {
            Message = message;
        }

        public override string ToString()
        {
            return Error?.ToString() ?? Message;
        }
    }
}
