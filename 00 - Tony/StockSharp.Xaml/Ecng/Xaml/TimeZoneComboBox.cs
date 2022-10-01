using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ecng.Xaml
{
    public class TimeZoneComboBox : ComboBox
    {
        public static readonly DependencyProperty SelectedTimeZoneProperty = DependencyProperty.Register( nameof( SelectedTimeZone ), typeof( TimeZoneInfo ), typeof( TimeZoneComboBox ), ( PropertyMetadata )new UIPropertyMetadata( ( object )TimeZoneInfo.Utc, ( PropertyChangedCallback )( ( s, e ) => ( ( Selector )s ).SelectedItem = e.NewValue ) ) );

        public TimeZoneComboBox()
        {
            this.ItemsSource = ( IEnumerable )TimeZoneInfo.GetSystemTimeZones();
        }

        public TimeZoneInfo SelectedTimeZone
        {
            get
            {
                return ( TimeZoneInfo )this.GetValue( TimeZoneComboBox.SelectedTimeZoneProperty );
            }
            set
            {
                this.SetValue( TimeZoneComboBox.SelectedTimeZoneProperty, ( object )value );
            }
        }

        protected override void OnSelectionChanged( SelectionChangedEventArgs e )
        {
            this.SelectedTimeZone = ( TimeZoneInfo )this.SelectedItem;
            base.OnSelectionChanged( e );
        }
    }
}
