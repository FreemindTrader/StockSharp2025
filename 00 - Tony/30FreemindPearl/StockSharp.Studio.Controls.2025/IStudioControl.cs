// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.IStudioControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Serialization;
using StockSharp.Studio.Core.Commands;
using System;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    public interface IStudioControl : IPersistable, IDisposable
    {
        string Title { get; set; }

        ImageSource Icon { get; }

        string DocUrl { get; }

        string Key { get; set; }

        bool SaveWithLayout { get; }

        bool IsTitleEditable { get; }

        void FirstTimeInit();

        void SendCommand( IStudioCommand command );
    }
}
