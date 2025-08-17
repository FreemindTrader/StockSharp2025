// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.IStudioControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Windows.Media;
using Ecng.Serialization;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

public interface IStudioControl : IPersistable, IDisposable
{
    string Title { get; set; }

    ImageSource Icon { get; }

    string DocUrl { get; }

    string Key { get; set; }

    bool SaveWithLayout { get; }

    bool IsTitleEditable { get; }

    void FirstTimeInit();

    void SendCommand(IStudioCommand command);
}
