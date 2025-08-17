// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LastDirSelector
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Studio.Core.Services;

#nullable disable
namespace StockSharp.Studio.Controls;

internal class LastDirSelector(IPersistableService service) : ILastDirSelector
{
    private readonly IPersistableService _service = service ?? throw new ArgumentNullException(nameof(service));

    void ILastDirSelector.SetValue(string ctrlName, string value)
    {
        this._service.SetLastDir(ctrlName, value);
    }

    bool ILastDirSelector.TryGetValue(string ctrlName, out string value)
    {
        value = this._service.GetLastDir(ctrlName);
        return !StringHelper.IsEmpty(value);
    }
}
