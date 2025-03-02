// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.LoadLayoutCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Common;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class LoadLayoutCommand : BaseStudioCommand
    {
        public LoadLayoutCommand( string layout )
        {
            if ( StringHelper.IsEmpty( layout ) )
                throw new ArgumentNullException( nameof( layout ) );
            this.Layout = layout;
        }

        public string Layout { get; }
    }
}
