// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Commands.OpenWindowCommand
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using StockSharp.Studio.Core.Commands;
using System;
using System.Runtime.CompilerServices;

namespace StockSharp.Studio.Controls.Commands
{
    public class OpenWindowCommand : BaseStudioCommand
    {
        public OpenWindowCommand( Type ctrlType, bool isToolWindow )
        {
            Type type = ctrlType;
            if (  type == null )
                throw new ArgumentNullException( nameof( ctrlType ) );
            this.CtrlType = type;
            this.IsToolWindow = isToolWindow;
        }

        public Type CtrlType { get; }

        public bool IsToolWindow { get; }

        
        public (IStudioControl ctrl, bool isNew) Result { get; set; }
    }
}
