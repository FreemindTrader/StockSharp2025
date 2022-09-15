// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.ContinueOnExceptionContext
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Ecng.Common;
using System;

namespace Ecng.Serialization
{
    public class ContinueOnExceptionContext
    {
        public event Action<Exception> Error;

        public bool DoNotEncrypt { get; set; }

        public static bool TryProcess( Exception ex )
        {
            if ( ex == null )
                throw new ArgumentNullException( nameof( ex ) );
            Scope<ContinueOnExceptionContext> current = Scope<ContinueOnExceptionContext>.Current;
            if ( current == null )
                return false;
            current.Value.Process( ex );
            return true;
        }

        public void Process( Exception ex )
        {
            if ( ex == null )
                throw new ArgumentNullException( nameof( ex ) );
            Action<Exception> error = this.Error;
            if ( error == null )
                return;
            error( ex );
        }
    }
}
