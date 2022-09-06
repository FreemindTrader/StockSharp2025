// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OpenIndexSecurityPanelCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo.Expressions;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class OpenIndexSecurityPanelCommand : BaseStudioCommand
    {
        public ExpressionIndexSecurity Security { get; }

        public OpenIndexSecurityPanelCommand( ExpressionIndexSecurity security )
        {
            ExpressionIndexSecurity expressionIndexSecurity = security;
            if ( expressionIndexSecurity == null )
                throw new ArgumentNullException( nameof( security ) );
            this.Security = expressionIndexSecurity;
        }
    }
}
