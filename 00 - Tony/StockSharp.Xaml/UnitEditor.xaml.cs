using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

#pragma warning disable 168

namespace StockSharp.Xaml
{
    internal sealed class UnitValidationRule : ValidationRule
    {
        public override ValidationResult Validate( object _param1, CultureInfo _param2 )
        {
            if ( _param1 == null )
                return new ValidationResult( false, ( object )LocalizedStrings.Str2938 );
            try
            {
                ( ( string )_param1 ).ToUnit( ( Func<UnitTypes, Decimal?> )null );
                return ValidationResult.ValidResult;
            }
            catch ( Exception ex )
            {
                return new ValidationResult( false, ( object )LocalizedStrings.Str2938 );
            }
        }
    }

    internal sealed class MyUnitConverter : IValueConverter
    {
        object IValueConverter.Convert(
          object _param1,
          Type _param2,
          object _param3,
          CultureInfo _param4)
        {
            if ( _param1 == null )
                return ( object )null;
            return ( object )_param1.ToString( );
        }

        object IValueConverter.ConvertBack(
          object _param1,
          Type _param2,
          object _param3,
          CultureInfo _param4)
        {
            return ( object )( ( string )_param1 ).ToUnit( ( Func<UnitTypes, Decimal?> )null );
        }
    }

    public partial class UnitEditor : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register( nameof( Value ), typeof( Unit ), typeof( UnitEditor ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )null, new PropertyChangedCallback( UnitEditor.smethod_0 ) ) );
        

        public UnitEditor( )
        {
            this.InitializeComponent( );
        }

        private static void smethod_0(
          DependencyObject d,
          DependencyPropertyChangedEventArgs e )
        {
            Unit newValue = ( Unit )e.NewValue;
            ( ( UnitEditor )d ).OnValueChanged( newValue );
        }

        public Unit Value
        {
            get
            {
                return ( Unit )this.GetValue( UnitEditor.ValueProperty );
            }
            set
            {
                this.SetValue( UnitEditor.ValueProperty, ( object )value );

                this.OnValueChanged( value );
            }
        }

        public event Action<Unit> ValueChanged;

        private void OnValueChanged( Unit unit_0 )
        {
            Action<Unit> action0 = this.ValueChanged;
            if ( action0 == null )
            {
                return;
            }

            action0( unit_0 );
        }

        
    }
}
