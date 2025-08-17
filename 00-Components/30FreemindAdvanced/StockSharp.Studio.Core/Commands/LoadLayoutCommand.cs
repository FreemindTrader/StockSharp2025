// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.LoadLayoutCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79FA112F-39E9-4D2F-8DA4-EB9B4E826551
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Core.dll

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class LoadLayoutCommand : BaseStudioCommand
{
    public LoadLayoutCommand(string layout)
    {
        this.Layout = !Ecng.Common.StringHelper.IsEmpty(layout) ? layout : throw new System.ArgumentNullException(nameof(layout));
    }

    public string Layout { get; }
}
