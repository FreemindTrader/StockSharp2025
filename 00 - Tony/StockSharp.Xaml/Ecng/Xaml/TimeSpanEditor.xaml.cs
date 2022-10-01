using Ecng.Common;
using Ecng.ComponentModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace Ecng.Xaml
{
    internal sealed class TimeSpanEditorMaskToVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            if ( _param1 == null || _param3 == null )
                return ( object )Visibility.Visible;
            int num1 = ( int )_param1;
            TimeSpanEditorMask timeSpanEditorMask = ( TimeSpanEditorMask )_param3;
            int num2 = ( int )timeSpanEditorMask;
            return ( object )( Visibility )( ( TimeSpanEditorMask )( num1 & num2 ) == timeSpanEditorMask ? 0 : 2 );
        }

        object IValueConverter.ConvertBack(
          object _param1,
          Type _param2,
          object _param3,
          CultureInfo _param4 )
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// <see cref="T:System.TimeSpan" /> editor.
    ///     </summary>
    /// <summary>TimeSpanEditor</summary>
    public partial class TimeSpanEditor : UserControl, IComponentConnector
    {
        /// <summary>
        /// The <see cref="T:System.TimeSpan" /> value.
        /// </summary>
        public TimeSpan? Value
        {
            get
            {
                return ( TimeSpan? )this.GetValue( TimeSpanEditor.ValueProperty );
            }
            set
            {
                this.SetValue( TimeSpanEditor.ValueProperty, ( object )value );
            }
        }

        /// <summary>Is nullable.</summary>
        public bool IsNullable
        {
            get
            {
                return ( bool )this.GetValue( TimeSpanEditor.IsNullableProperty );
            }
            set
            {
                this.SetValue( TimeSpanEditor.IsNullableProperty, ( object )value );
            }
        }

        /// <summary>Has value.</summary>
        public bool HasValue
        {
            get
            {
                return ( bool )this.GetValue( TimeSpanEditor.HasValueProperty );
            }
            set
            {
                this.SetValue( TimeSpanEditor.HasValueProperty, ( object )value );
            }
        }

        /// <summary>Microseconds.</summary>
        public int Microseconds
        {
            get
            {
                return ( int )this.GetValue( TimeSpanEditor.MicrosecondsProperty );
            }
            set
            {
                if ( value > 999 )
                    value = 0;
                if ( value < 0 )
                    value = 999;
                this.SetValue( TimeSpanEditor.MicrosecondsProperty, ( object )value );
            }
        }

        /// <summary>Milliseconds.</summary>
        public int Milliseconds
        {
            get
            {
                return ( int )this.GetValue( TimeSpanEditor.MillisecondsProperty );
            }
            set
            {
                if ( value > 999 )
                    value = 0;
                if ( value < 0 )
                    value = 999;
                this.SetValue( TimeSpanEditor.MillisecondsProperty, ( object )value );
            }
        }

        /// <summary>Seconds.</summary>
        public int Seconds
        {
            get
            {
                return ( int )this.GetValue( TimeSpanEditor.SecondsProperty );
            }
            set
            {
                if ( value > 59 )
                    value = 0;
                if ( value < 0 )
                    value = 59;
                this.SetValue( TimeSpanEditor.SecondsProperty, ( object )value );
            }
        }

        /// <summary>Minutes.</summary>
        public int Minutes
        {
            get
            {
                return ( int )this.GetValue( TimeSpanEditor.MinutesProperty );
            }
            set
            {
                if ( value > 59 )
                    value = 0;
                if ( value < 0 )
                    value = 59;
                this.SetValue( TimeSpanEditor.MinutesProperty, ( object )value );
            }
        }

        /// <summary>Hours.</summary>
        public int Hours
        {
            get
            {
                return ( int )this.GetValue( TimeSpanEditor.HoursProperty );
            }
            set
            {
                if ( value > 23 )
                    value = 0;
                if ( value < 0 )
                    value = 23;
                this.SetValue( TimeSpanEditor.HoursProperty, ( object )value );
            }
        }

        /// <summary>The days value.</summary>
        public int Days
        {
            get
            {
                return ( int )this.GetValue( TimeSpanEditor.DaysProperty );
            }
            set
            {
                if ( value > 364 )
                    value = 0;
                if ( value < 0 )
                    value = 364;
                this.SetValue( TimeSpanEditor.DaysProperty, ( object )value );
            }
        }

        /// <summary>Show parts mask.</summary>
        public TimeSpanEditorMask Mask
        {
            get
            {
                return ( TimeSpanEditorMask )this.GetValue( TimeSpanEditor.MaskProperty );
            }
            set
            {
                this.SetValue( TimeSpanEditor.MaskProperty, ( object )value );
            }
        }


        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Value" />.
        ///     </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register( nameof( Value ), typeof( TimeSpan? ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )TimeSpan.FromMinutes( 1.0 ), new PropertyChangedCallback( OnValueChanged ) ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.HasValue" />.
        ///     </summary>
        public static readonly DependencyProperty IsNullableProperty = DependencyProperty.Register( nameof( IsNullable ), typeof( bool ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )false ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.HasValue" />.
        ///     </summary>
        public static readonly DependencyProperty HasValueProperty = DependencyProperty.Register( nameof( HasValue ), typeof( bool ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )true, new PropertyChangedCallback( OnTimeChanged ) ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Microseconds" />.
        ///     </summary>
        public static readonly DependencyProperty MicrosecondsProperty = DependencyProperty.Register( nameof( Microseconds ), typeof( int ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )0, new PropertyChangedCallback( OnTimeChanged ) ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Milliseconds" />.
        ///     </summary>
        public static readonly DependencyProperty MillisecondsProperty = DependencyProperty.Register( nameof( Milliseconds ), typeof( int ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )0, new PropertyChangedCallback( OnTimeChanged ) ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Seconds" />.
        ///     </summary>
        public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register( nameof( Seconds ), typeof( int ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )0, new PropertyChangedCallback( OnTimeChanged ) ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Minutes" />.
        ///     </summary>
        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register( nameof( Minutes ), typeof( int ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )1, new PropertyChangedCallback( OnTimeChanged ) ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Hours" />.
        ///     </summary>
        public static readonly DependencyProperty HoursProperty = DependencyProperty.Register( nameof( Hours ), typeof( int ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )0, new PropertyChangedCallback( OnTimeChanged ) ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Days" />.
        ///     </summary>
        public static readonly DependencyProperty DaysProperty = DependencyProperty.Register( nameof( Days ), typeof( int ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )0, new PropertyChangedCallback( OnTimeChanged ) ) );
        /// <summary>
        /// <see cref="T:System.Windows.DependencyProperty" /> for <see cref="P:Ecng.Xaml.TimeSpanEditor.Mask" />.
        ///     </summary>
        public static readonly DependencyProperty MaskProperty = DependencyProperty.Register( nameof( Mask ), typeof( TimeSpanEditorMask ), typeof( TimeSpanEditor ), ( PropertyMetadata )new UIPropertyMetadata( ( object )( TimeSpanEditorMask )14 ) );

        private TextBox _focusedTextBox;

        private bool _suspendChanged;

        private readonly List<Key> _validKey = new List<Key>() { Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.Up, Key.Down, Key.Left, Key.Right, Key.Delete, Key.Back };



        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ecng.Xaml.TimeSpanEditor" />.
        /// </summary>
        public TimeSpanEditor()
        {
            this.InitializeComponent();
            this._focusedTextBox = this.TxbMinutes;
        }

        private static void OnValueChanged( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {
            TimeSpanEditor timeSpanEditor = ( TimeSpanEditor )sender;
            TimeSpan? newValue = ( TimeSpan? )e.NewValue;
            timeSpanEditor._suspendChanged = true;
            try
            {
                if ( newValue.HasValue )
                {
                    timeSpanEditor.HasValue = true;
                    timeSpanEditor.Seconds = newValue.Value.Seconds;
                    timeSpanEditor.Minutes = newValue.Value.Minutes;
                    timeSpanEditor.Hours = newValue.Value.Hours;
                    timeSpanEditor.Days = newValue.Value.Days;
                    timeSpanEditor.Milliseconds = newValue.Value.Milliseconds;
                    timeSpanEditor.Microseconds = newValue.Value.GetMicroseconds();
                }
                else
                {
                    timeSpanEditor.HasValue = false;
                    timeSpanEditor.Seconds = 0;
                    timeSpanEditor.Minutes = 0;
                    timeSpanEditor.Hours = 0;
                    timeSpanEditor.Days = 0;
                    timeSpanEditor.Milliseconds = 0;
                    timeSpanEditor.Microseconds = 0;
                }
            }
            finally
            {
                timeSpanEditor._suspendChanged = false;
            }
            // ISSUE: reference to a compiler-generated field
            Action<TimeSpan?> valueChanged = timeSpanEditor.ValueChanged;
            if ( valueChanged == null )
                return;
            valueChanged( newValue );
        }



        private static void OnTimeChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            TimeSpanEditor timeSpanEditor = ( TimeSpanEditor )d;
            if ( timeSpanEditor._suspendChanged )
                return;
            timeSpanEditor.Value = timeSpanEditor.HasValue ? new TimeSpan?( TimeHelper.AddMicroseconds( new TimeSpan( timeSpanEditor.Days, timeSpanEditor.Hours, timeSpanEditor.Minutes, timeSpanEditor.Seconds, timeSpanEditor.Milliseconds ), ( long )timeSpanEditor.Microseconds ) ) : new TimeSpan?();
        }


        /// <summary>
        /// The <see cref="P:Ecng.Xaml.TimeSpanEditor.Value" /> changed event.
        /// </summary>
        public event Action<TimeSpan?> ValueChanged;

        private void ButtonUpDownClick( object sender, RoutedEventArgs e )
        {
            if ( this._focusedTextBox == null )
                return;
            RepeatButton repeatButton = ( RepeatButton )sender;
            string name = this._focusedTextBox.Name;
            if ( !( name == "TxbMicroseconds" ) )
            {
                if ( !( name == "TxbMilliseconds" ) )
                {
                    if ( !( name == "TxbSeconds" ) )
                    {
                        if ( !( name == "TxbMinutes" ) )
                        {
                            if ( !( name == "TxbHours" ) )
                            {
                                if ( !( name == "TxbDays" ) )
                                    return;
                                if ( repeatButton.Name == "BtnUp" )
                                    ++this.Days;
                                else
                                    --this.Days;
                            }
                            else if ( repeatButton.Name == "BtnUp" )
                                ++this.Hours;
                            else
                                --this.Hours;
                        }
                        else if ( repeatButton.Name == "BtnUp" )
                            ++this.Minutes;
                        else
                            --this.Minutes;
                    }
                    else if ( repeatButton.Name == "BtnUp" )
                        ++this.Seconds;
                    else
                        --this.Seconds;
                }
                else if ( repeatButton.Name == "BtnUp" )
                    ++this.Milliseconds;
                else
                    --this.Milliseconds;
            }
            else if ( repeatButton.Name == "BtnUp" )
                ++this.Microseconds;
            else
                --this.Microseconds;
        }

        private void KeyPressed( object sender, KeyEventArgs e )
        {
            if ( !this._validKey.Contains( e.Key ) )
            {
                e.Handled = true;
            }
            else
            {
                string name = ( ( FrameworkElement )sender ).Name;
                if ( !( name == "TxbMicroseconds" ) )
                {
                    if ( !( name == "TxbMilliseconds" ) )
                    {
                        if ( !( name == "TxbSeconds" ) )
                        {
                            if ( !( name == "TxbMinutes" ) )
                            {
                                if ( !( name == "TxbHours" ) )
                                {
                                    if ( !( name == "TxbDays" ) )
                                        return;
                                    if ( e.Key == Key.Up )
                                        ++this.Days;
                                    if ( e.Key != Key.Down )
                                        return;
                                    --this.Days;
                                }
                                else
                                {
                                    if ( e.Key == Key.Up )
                                        ++this.Hours;
                                    if ( e.Key != Key.Down )
                                        return;
                                    --this.Hours;
                                }
                            }
                            else
                            {
                                if ( e.Key == Key.Up )
                                    ++this.Minutes;
                                if ( e.Key != Key.Down )
                                    return;
                                --this.Minutes;
                            }
                        }
                        else
                        {
                            if ( e.Key == Key.Up )
                                ++this.Seconds;
                            if ( e.Key != Key.Down )
                                return;
                            --this.Seconds;
                        }
                    }
                    else
                    {
                        if ( e.Key == Key.Up )
                            ++this.Milliseconds;
                        if ( e.Key != Key.Down )
                            return;
                        --this.Milliseconds;
                    }
                }
                else
                {
                    if ( e.Key == Key.Up )
                        ++this.Microseconds;
                    if ( e.Key != Key.Down )
                        return;
                    --this.Microseconds;
                }
            }
        }

        private void TextChanged( object sender, TextChangedEventArgs e )
        {
            TextBox textBox = ( TextBox )sender;
            string name = textBox.Name;
            if ( !( name == "TxbMicroseconds" ) )
            {
                if ( !( name == "TxbMilliseconds" ) )
                {
                    if ( !( name == "TxbSeconds" ) )
                    {
                        if ( !( name == "TxbMinutes" ) )
                        {
                            if ( !( name == "TxbHours" ) )
                            {
                                if ( !( name == "TxbDays" ) )
                                    return;
                                int num = TimeSpanEditor.ValidateNumber( this.Days, 364, textBox.Text );
                                this.Days = num;
                                textBox.Text = num.ToString( ( IFormatProvider )CultureInfo.InvariantCulture );
                            }
                            else
                            {
                                int num = TimeSpanEditor.ValidateNumber( this.Hours, 23, textBox.Text );
                                this.Hours = num;
                                textBox.Text = num.ToString( ( IFormatProvider )CultureInfo.InvariantCulture );
                            }
                        }
                        else
                        {
                            int num = TimeSpanEditor.ValidateNumber( this.Minutes, 59, textBox.Text );
                            this.Minutes = num;
                            textBox.Text = num.ToString( ( IFormatProvider )CultureInfo.InvariantCulture );
                        }
                    }
                    else
                    {
                        int num = TimeSpanEditor.ValidateNumber( this.Seconds, 59, textBox.Text );
                        this.Seconds = num;
                        textBox.Text = num.ToString( ( IFormatProvider )CultureInfo.InvariantCulture );
                    }
                }
                else
                {
                    int num = TimeSpanEditor.ValidateNumber( this.Milliseconds, 999, textBox.Text );
                    this.Milliseconds = num;
                    textBox.Text = num.ToString( ( IFormatProvider )CultureInfo.InvariantCulture );
                }
            }
            else
            {
                int num = TimeSpanEditor.ValidateNumber( this.Microseconds, 999, textBox.Text );
                this.Microseconds = num;
                textBox.Text = num.ToString( ( IFormatProvider )CultureInfo.InvariantCulture );
            }
        }

        private static int ValidateNumber( int lastValue, int max, string text )
        {
            if ( text.Length > max.ToString().Length )
                return lastValue;
            int result;
            if ( !int.TryParse( text, out result ) )
                return 0;
            if ( result > max )
                result = 0;
            if ( result < 0 )
                result = max;
            return result;
        }

        private void FocusedTextBox( object sender, RoutedEventArgs e )
        {
            this._focusedTextBox = ( TextBox )sender;
        }

        private void OnMouseWheel( object sender, MouseWheelEventArgs e )
        {
            string name = ( ( FrameworkElement )sender ).Name;
            if ( !( name == "TxbMicroseconds" ) )
            {
                if ( !( name == "TxbMilliseconds" ) )
                {
                    if ( !( name == "TxbSeconds" ) )
                    {
                        if ( !( name == "TxbMinutes" ) )
                        {
                            if ( !( name == "TxbHours" ) )
                            {
                                if ( !( name == "TxbDays" ) )
                                    return;
                                if ( e.Delta > 0 )
                                    ++this.Days;
                                else
                                    --this.Days;
                            }
                            else if ( e.Delta > 0 )
                                ++this.Hours;
                            else
                                --this.Hours;
                        }
                        else if ( e.Delta > 0 )
                            ++this.Minutes;
                        else
                            --this.Minutes;
                    }
                    else if ( e.Delta > 0 )
                        ++this.Seconds;
                    else
                        --this.Seconds;
                }
                else if ( e.Delta > 0 )
                    ++this.Milliseconds;
                else
                    --this.Milliseconds;
            }
            else if ( e.Delta > 0 )
                ++this.Microseconds;
            else
                --this.Microseconds;
        }
    }
}
