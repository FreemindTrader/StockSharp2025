// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.IconUriBindingExtension
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Utils.Svg;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors.Internal;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Ecng.Xaml
{
  /// <summary>
  /// Extension receives binding for icon (svg/png) and creates image source binding which will auto update when theme changed.
  /// </summary>
  public class IconUriBindingExtension : MarkupExtension
  {
    
    private static readonly DependencyProperty \u0023\u003Dz7KPUcwjY58OA = DependencyProperty.RegisterAttached(nameof(2127279751), typeof (Uri), typeof (IconUriBindingExtension), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, FrameworkPropertyMetadataOptions.Inherits));
    
    private readonly BindingBase \u0023\u003Dzw7_RWgcSiwyGz9OXHg\u003D\u003D;

    /// <summary>
    /// </summary>
    public IconUriBindingExtension(BindingBase binding)
    {
      this.\u0023\u003Dzw7_RWgcSiwyGz9OXHg\u003D\u003D = binding;
    }

    /// <summary>
    /// </summary>
    public BindingBase Binding
    {
      get
      {
        return this.\u0023\u003Dzw7_RWgcSiwyGz9OXHg\u003D\u003D;
      }
    }

    /// <inheritdoc />
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      IProvideValueTarget service = serviceProvider.GetService(typeof (IProvideValueTarget)) as IProvideValueTarget;
      DependencyObject targetObject = service?.TargetObject as DependencyObject;
      if (targetObject == null || !(service.TargetProperty is DependencyProperty))
        return (object) this;
      BindingOperations.SetBinding(targetObject, IconUriBindingExtension.\u0023\u003Dz7KPUcwjY58OA, this.Binding);
      return IconUriBindingExtension.\u0023\u003DzW7Y3sS0\u003D().ProvideValue(serviceProvider);
    }

    private static MultiBinding \u0023\u003DzW7Y3sS0\u003D()
    {
      MultiBinding multiBinding = new MultiBinding() { Converter = (IMultiValueConverter) new IconUriBindingExtension.\u0023\u003DzaINtwFytSDIH(), Mode = BindingMode.OneWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
      multiBinding.Bindings.Add((BindingBase) new System.Windows.Data.Binding()
      {
        Path = new PropertyPath(string.Empty, Array.Empty<object>()),
        RelativeSource = RelativeSource.Self,
        Mode = BindingMode.OneWay,
        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
      });
      multiBinding.Bindings.Add((BindingBase) new System.Windows.Data.Binding()
      {
        Path = new PropertyPath(nameof(2127279990), new object[1]
        {
          (object) ThemeManager.TreeWalkerProperty
        }),
        RelativeSource = RelativeSource.Self,
        Mode = BindingMode.OneWay,
        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
      });
      multiBinding.Bindings.Add((BindingBase) new System.Windows.Data.Binding()
      {
        Path = new PropertyPath(nameof(2127279990), new object[1]
        {
          (object) SvgImageHelper.StateProperty
        }),
        RelativeSource = RelativeSource.Self,
        Mode = BindingMode.OneWay,
        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
      });
      multiBinding.Bindings.Add((BindingBase) new System.Windows.Data.Binding()
      {
        Path = new PropertyPath(nameof(2127279990), new object[1]
        {
          (object) WpfSvgPalette.PaletteProperty
        }),
        RelativeSource = RelativeSource.Self,
        Mode = BindingMode.OneWay,
        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
      });
      multiBinding.Bindings.Add((BindingBase) new System.Windows.Data.Binding()
      {
        Path = new PropertyPath(nameof(2127279990), new object[1]
        {
          (object) IconUriBindingExtension.\u0023\u003Dz7KPUcwjY58OA
        }),
        RelativeSource = RelativeSource.Self,
        Mode = BindingMode.OneWay,
        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
      });
      return multiBinding;
    }

    private sealed class \u0023\u003DzaINtwFytSDIH : IMultiValueConverter
    {
      
      private Uri \u0023\u003DzfeQBshA\u003D;
      
      private bool \u0023\u003DzcgnnmsKlatXr;
      
      private SvgImage \u0023\u003Dz3uN9haLxyL\u0024_;
      
      private static InplaceResourceProvider \u0023\u003DzMRwf5\u0024AYEjfh;

      private static InplaceResourceProvider \u0023\u003DzYbORc67au_oH()
      {
        return IconUriBindingExtension.\u0023\u003DzaINtwFytSDIH.\u0023\u003DzMRwf5\u0024AYEjfh ?? (IconUriBindingExtension.\u0023\u003DzaINtwFytSDIH.\u0023\u003DzMRwf5\u0024AYEjfh = new InplaceResourceProvider(nameof(2127279752)));
      }

      object IMultiValueConverter.Convert(
        object[] _param1,
        Type _param2,
        object _param3,
        CultureInfo _param4)
      {
        if (_param1.Length == 5)
        {
          Uri uri1 = _param1[4] as Uri;
          if ((object) uri1 != null)
          {
            if (!uri1.IsAbsoluteUri)
            {
              string str1 = nameof(2127281427);
              Uri uri2 = uri1;
              string str2 = (object) uri2 != null ? uri2.ToString() : (string) null;
              uri1 = new Uri(str1 + str2, UriKind.Absolute);
            }
            if (this.\u0023\u003DzfeQBshA\u003D != uri1)
            {
              this.\u0023\u003DzfeQBshA\u003D = uri1;
              this.\u0023\u003DzcgnnmsKlatXr = uri1.ToString().ToLowerInvariant().EndsWith(nameof(2127278040));
              this.\u0023\u003Dz3uN9haLxyL\u0024_ = !this.\u0023\u003DzcgnnmsKlatXr || !(this.\u0023\u003DzfeQBshA\u003D != (Uri) null) ? (SvgImage) null : SvgImageHelper.GetOrCreate(this.\u0023\u003DzfeQBshA\u003D, new Func<Uri, SvgImage>(SvgImageHelper.CreateImage));
            }
            if (!this.\u0023\u003DzcgnnmsKlatXr)
            {
              if (!(this.\u0023\u003DzfeQBshA\u003D != (Uri) null))
                return (object) null;
              return (object) new BitmapImage(this.\u0023\u003DzfeQBshA\u003D);
            }
            DependencyObject dependencyObject = _param1[0] as DependencyObject;
            WpfSvgPalette svgPalette = _param1[3] as WpfSvgPalette;
            if (svgPalette == null)
            {
              ThemeTreeWalker themeTreeWalker = _param1[1] as ThemeTreeWalker;
              svgPalette = (themeTreeWalker != null ? themeTreeWalker.get_InplaceResourceProvider() : IconUriBindingExtension.\u0023\u003DzaINtwFytSDIH.\u0023\u003DzYbORc67au_oH()).GetSvgPalette((object) dependencyObject, (ThemeTreeWalker) null);
            }
            WpfSvgPalette wpfSvgPalette = svgPalette;
            string str = _param1[2] as string;
            if (this.\u0023\u003Dz3uN9haLxyL\u0024_ != null)
            {
              Size size;
              ((Size) ref size).\u002Ector(this.\u0023\u003Dz3uN9haLxyL\u0024_.get_Width(), this.\u0023\u003Dz3uN9haLxyL\u0024_.get_Height());
              return (object) WpfSvgRenderer.CreateImageSource(this.\u0023\u003Dz3uN9haLxyL\u0024_, size, wpfSvgPalette, str, true, true);
            }
            if (this.\u0023\u003DzfeQBshA\u003D == (Uri) null)
              return (object) null;
            this.\u0023\u003Dz3uN9haLxyL\u0024_ = SvgImageHelper.GetOrCreate(this.\u0023\u003DzfeQBshA\u003D, (Func<Uri, SvgImage>) null);
            if (this.\u0023\u003Dz3uN9haLxyL\u0024_ != null)
              return (object) WpfSvgRenderer.CreateImageSource(this.\u0023\u003Dz3uN9haLxyL\u0024_, new Size(this.\u0023\u003Dz3uN9haLxyL\u0024_.get_Width(), this.\u0023\u003Dz3uN9haLxyL\u0024_.get_Height()), wpfSvgPalette, str, true, true);
            return (object) null;
          }
        }
        return (object) null;
      }

      object[] IMultiValueConverter.ConvertBack(
        object _param1,
        Type[] _param2,
        object _param3,
        CultureInfo _param4)
      {
        throw new NotSupportedException();
      }
    }
  }
}
