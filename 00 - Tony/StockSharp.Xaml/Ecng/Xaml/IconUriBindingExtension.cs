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

        private static readonly DependencyProperty BindingDependencyProperty = DependencyProperty.RegisterAttached( "IconSink", typeof( Uri ), typeof( IconUriBindingExtension ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )null, FrameworkPropertyMetadataOptions.Inherits ) );

        private readonly BindingBase _binding;

        /// <summary>
        /// </summary>
        public IconUriBindingExtension( BindingBase binding )
        {
            this._binding = binding;
        }

        /// <summary>
        /// </summary>
        public BindingBase Binding
        {
            get
            {
                return this._binding;
            }
        }

        /// <inheritdoc />
        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            IProvideValueTarget service = serviceProvider.GetService( typeof( IProvideValueTarget ) ) as IProvideValueTarget;
            DependencyObject targetObject = service?.TargetObject as DependencyObject;
            if ( targetObject == null || !( service.TargetProperty is DependencyProperty ) )
                return ( object )this;
            BindingOperations.SetBinding( targetObject, IconUriBindingExtension.BindingDependencyProperty, this.Binding );
            return IconUriBindingExtension.CreateMultiBinding().ProvideValue( serviceProvider );
        }

        private static MultiBinding CreateMultiBinding()
        {
            MultiBinding multiBinding = new MultiBinding()
            {
                Converter = ( IMultiValueConverter )new IconUriBindingExtension.IconUriBindingConverter(),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            multiBinding.Bindings.Add( ( BindingBase )new System.Windows.Data.Binding()
            {
                Path = new PropertyPath( string.Empty, Array.Empty<object>() ),
                RelativeSource = RelativeSource.Self,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            } );

            multiBinding.Bindings.Add( ( BindingBase )new System.Windows.Data.Binding()
            {
                Path = new PropertyPath( "(0)", new object[1] { ThemeManager.TreeWalkerProperty } ),
                RelativeSource = RelativeSource.Self,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            } );

            multiBinding.Bindings.Add( ( BindingBase )new System.Windows.Data.Binding()
            {
                Path = new PropertyPath( "(0)", new object[1] { SvgImageHelper.StateProperty } ),
                RelativeSource = RelativeSource.Self,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            } );

            multiBinding.Bindings.Add( ( BindingBase )new System.Windows.Data.Binding()
            {
                Path = new PropertyPath( "(0)", new object[1] { WpfSvgPalette.PaletteProperty } ),
                RelativeSource = RelativeSource.Self,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            } );
            multiBinding.Bindings.Add( ( BindingBase )new System.Windows.Data.Binding(){ Path = new PropertyPath( "(0)", new object[1] { IconUriBindingExtension.BindingDependencyProperty } ),
                RelativeSource = RelativeSource.Self,
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            } );
            return multiBinding;
        }

        private sealed class IconUriBindingConverter : IMultiValueConverter
        {


            private Uri _uri;

            private bool _isSvgFile;

            private SvgImage _mySvgImage;

            private static InplaceResourceProvider _myProvider;

            private static InplaceResourceProvider SomeMultiBindingsConverterM01()
            {
                return IconUriBindingExtension.IconUriBindingConverter._myProvider ?? ( IconUriBindingExtension.IconUriBindingConverter._myProvider = new InplaceResourceProvider( Theme.Win10Light.Name ) );
            }

            object IMultiValueConverter.Convert( object[ ] value, Type targetType, object parameter, CultureInfo culture )
            {
                if ( value.Length == 5 )
                {
                    Uri uriSource = value[4] as Uri;
                    if ( ( object )uriSource != null )
                    {
                        if ( !uriSource.IsAbsoluteUri )
                        {
                            string str1 = "pack://application:,,,"; 
                            Uri uri2 = uriSource;
                            string str2 = ( object )uri2 != null ? uri2.ToString() : ( string )null;
                            uriSource = new Uri( str1 + str2, UriKind.Absolute );
                        }
                        if ( this._uri != uriSource )
                        {
                            this._uri = uriSource;
                            this._isSvgFile = uriSource.ToString().ToLowerInvariant().EndsWith( ".svg" );
                            this._mySvgImage = !this._isSvgFile || !( this._uri != ( Uri )null ) ? ( SvgImage )null : SvgImageHelper.GetOrCreate( this._uri, new Func<Uri, SvgImage>( SvgImageHelper.CreateImage ) );
                        }
                        if ( !this._isSvgFile )
                        {
                            if ( !( this._uri != ( Uri )null ) )
                                return ( object )null;
                            return ( object )new BitmapImage( this._uri );
                        }
                        DependencyObject dependencyObject = value[0] as DependencyObject;
                        WpfSvgPalette svgPalette = value[3] as WpfSvgPalette;
                        if ( svgPalette == null )
                        {
                            ThemeTreeWalker themeTreeWalker = value[1] as ThemeTreeWalker;
                            svgPalette = ( themeTreeWalker != null ? themeTreeWalker.InplaceResourceProvider : IconUriBindingExtension.IconUriBindingConverter.SomeMultiBindingsConverterM01() ).GetSvgPalette( ( object )dependencyObject, ( ThemeTreeWalker )null );
                        }
                        WpfSvgPalette wpfSvgPalette = svgPalette;
                        string str = value[2] as string;
                        if ( this._mySvgImage != null )
                        {
                            Size size = new Size( this._mySvgImage.Width, this._mySvgImage.Height );
                            
                            return ( object )WpfSvgRenderer.CreateImageSource( this._mySvgImage, size, wpfSvgPalette, str, true, true );
                        }
                        if ( this._uri == ( Uri )null )
                            return ( object )null;
                        this._mySvgImage = SvgImageHelper.GetOrCreate( this._uri, ( Func<Uri, SvgImage> )null );
                        if ( this._mySvgImage != null )
                            return ( object )WpfSvgRenderer.CreateImageSource( this._mySvgImage, new Size( this._mySvgImage.Width, this._mySvgImage.Height ), wpfSvgPalette, str, true, true );
                        return ( object )null;
                    }
                }
                return ( object )null;
            }

            object[ ] IMultiValueConverter.ConvertBack(
              object _param1,
              Type[ ] _param2,
              object _param3,
              CultureInfo _param4 )
            {
                throw new NotSupportedException();
            }
        }
    }
}
