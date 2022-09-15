// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.ThemedIconBinding
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Xpf.Core;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml
{
  /// <summary>Icon binding.</summary>
  public class ThemedIconBinding : MultiBinding
  {
    
    private readonly Binding \u0023\u003DzJ4Usb6U\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.ThemedIconBinding" />.
    /// </summary>
    /// <param name="image">Drawing image.</param>
    public ThemedIconBinding(DrawingImage image)
    {
      if (image == null)
        throw new ArgumentNullException(nameof(2127279235));
      this.\u0023\u003DzMW3YySc\u003D();
      this.Converter = (IMultiValueConverter) new ThemedImageConverter(image);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Xaml.ThemedIconBinding" />.
    /// </summary>
    public ThemedIconBinding()
    {
      this.\u0023\u003DzMW3YySc\u003D();
      this.Bindings.Add((BindingBase) (this.\u0023\u003DzJ4Usb6U\u003D = new Binding()));
      this.Converter = (IMultiValueConverter) new ThemedImageConverter();
    }

    /// <summary>Gets or sets the path to the binding source property.</summary>
    public PropertyPath Path
    {
      get
      {
        return this.\u0023\u003DzJ4Usb6U\u003D.Path;
      }
      set
      {
        this.\u0023\u003DzJ4Usb6U\u003D.Path = value;
      }
    }

    private void \u0023\u003DzMW3YySc\u003D()
    {
      this.Bindings.Add((BindingBase) new Binding()
      {
        RelativeSource = RelativeSource.Self
      });
      this.Bindings.Add((BindingBase) new Binding()
      {
        Path = new PropertyPath((object) ThemeManager.TreeWalkerProperty),
        RelativeSource = RelativeSource.Self
      });
      this.Bindings.Add((BindingBase) new Binding()
      {
        Path = new PropertyPath((object) WpfSvgPalette.PaletteProperty),
        RelativeSource = RelativeSource.Self
      });
    }
  }
}
