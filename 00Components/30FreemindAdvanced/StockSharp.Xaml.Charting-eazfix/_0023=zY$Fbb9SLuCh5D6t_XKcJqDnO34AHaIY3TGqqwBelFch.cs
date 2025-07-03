// Decompiled with JetBrains decompiler
// Type: #=zY$Fbb9SLuCh5D6t_XKcJqDnO34AHaIY3TGqqwBelFchkgkTjgg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
internal sealed class \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqDnO34AHaIY3TGqqwBelFchkgkTjgg\u003D\u003D : 
  IValueConverter
{
  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return !(_param1 is Border border) ? (object) Visibility.Collapsed : (object) (Visibility) (border.\u0023\u003DzoZNX5EMTl75C().FirstOrDefault<DependencyObject>(\u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqDnO34AHaIY3TGqqwBelFchkgkTjgg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D ?? (\u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqDnO34AHaIY3TGqqwBelFchkgkTjgg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D = new Func<DependencyObject, bool>(\u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqDnO34AHaIY3TGqqwBelFchkgkTjgg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzsYV7b2N1RLymHLqTEQ\u003D\u003D))) is dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd r886FtpjvsuvaEjd ? (r886FtpjvsuvaEjd.ShowVisibilityCheckboxes ? 0 : 2) : 2);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    throw new NotImplementedException();
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqDnO34AHaIY3TGqqwBelFchkgkTjgg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqDnO34AHaIY3TGqqwBelFchkgkTjgg\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<DependencyObject, bool> \u0023\u003Dz7ixUD\u002407i38pD5UcHw\u003D\u003D;

    internal bool \u0023\u003DzsYV7b2N1RLymHLqTEQ\u003D\u003D(DependencyObject _param1)
    {
      return _param1 is dje_zCGM8V4K8D6LD55V4P3QH566RVL3AKPFBLK6D9BVPTE2P4K6R886FTPJVSUVA_ejd;
    }
  }
}
