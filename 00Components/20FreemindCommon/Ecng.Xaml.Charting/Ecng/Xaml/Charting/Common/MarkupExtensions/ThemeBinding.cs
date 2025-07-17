// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.MarkupExtensions.ThemeBinding
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
namespace Ecng.Xaml.Charting
{
    public class ThemeBinding : MarkupExtension
    {
        private static readonly IValueConverter Converter = (IValueConverter) new ThemeBinding.ThemeConverter();
        private static readonly RelativeSource Self = RelativeSource.Self;
        private static readonly object[] EmptyArray = new object[0];

        public ThemeBinding()
        {
            this.Mode = BindingMode.OneWay;
        }

        public string Path
        {
            get; set;
        }

        public BindingMode Mode
        {
            get; set;
        }

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            IProvideValueTarget service = serviceProvider.GetService(typeof (IProvideValueTarget)) as IProvideValueTarget;
            if ( service == null || service.TargetObject.GetType().Name == "SharedDp" )
                return ( object ) this;
            if ( service.TargetObject is Setter )
                return ( object ) new Binding()
                {
                    Path = new PropertyPath( ( object ) ThemeManager.ThemeProperty ),
                    ConverterParameter = ( object ) typeof( IThemeProvider ).GetProperty( this.Path ).GetGetMethod(),
                    Converter = ThemeBinding.Converter,
                    Mode = BindingMode.OneWay,
                    RelativeSource = ThemeBinding.Self
                };
            DependencyObject targetObject = service.TargetObject as DependencyObject;
            if ( targetObject == null )
                throw new Exception( "Not a DependencyObject" );
            return this.ProvideValueForDependencyObject( targetObject );
        }

        private object ProvideValueForDependencyObject( DependencyObject depObj )
        {
            return typeof( IThemeProvider ).GetProperty( this.Path ).GetValue( ( object ) ThemeManager.GetThemeProvider( ThemeManager.GetTheme( depObj ) ), ThemeBinding.EmptyArray );
        }

        internal class ThemeConverter : IValueConverter
        {
            public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
            {
                return ( parameter as MethodInfo ).Invoke( ( object ) ThemeManager.GetThemeProvider( value as string ), ThemeBinding.EmptyArray );
            }

            public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
            {
                throw new NotImplementedException();
            }
        }
    }
}
