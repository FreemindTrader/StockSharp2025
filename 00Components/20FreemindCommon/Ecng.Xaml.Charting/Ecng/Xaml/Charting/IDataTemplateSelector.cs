// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.IDataTemplateSelector
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows;

namespace fx.Xaml.Charting
{
    public interface IDataTemplateSelector
    {
        DataTemplate SelectTemplate( object item, DependencyObject container );

        event EventHandler DataTemplateChanged;
    }
}
