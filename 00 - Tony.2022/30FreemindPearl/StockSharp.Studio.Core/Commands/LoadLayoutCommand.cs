// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.LoadLayoutCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Common;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class LoadLayoutCommand : BaseStudioCommand
    {
        public LoadLayoutCommand( string layout )
        {
            if ( layout.IsEmpty() )
                throw new ArgumentNullException( nameof( layout ) );
            Layout = layout;
        }

        public string Layout { get; }
    }
}
