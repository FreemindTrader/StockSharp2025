// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ToggleButtonExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Ecng.Xaml.Charting
{
    public class ToggleButtonExtensions : DependencyObject
    {
        private static Dictionary<string, List<ToggleButton>> _elementToGroupNames = new Dictionary<string, List<ToggleButton>>();
        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.RegisterAttached("GroupName", typeof (string), typeof (ToggleButtonExtensions), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(ToggleButtonExtensions.OnGroupNameChanged)));

        public static void SetGroupName( ToggleButton element, string value )
        {
            element.SetValue( ToggleButtonExtensions.GroupNameProperty, ( object ) value );
        }

        public static string GetGroupName( ToggleButton element )
        {
            return element.GetValue( ToggleButtonExtensions.GroupNameProperty ).ToString();
        }

        private static void OnGroupNameChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ToggleButton checkBox = d as ToggleButton;
            if ( checkBox == null )
                return;
            string groupName1 = e.NewValue.ToString();
            string groupName2 = e.OldValue.ToString();
            if ( string.IsNullOrEmpty( groupName1 ) )
            {
                ToggleButtonExtensions.RemoveCheckboxFromGrouping( groupName1, checkBox );
            }
            else
            {
                if ( !( groupName1 != groupName2 ) )
                    return;
                if ( !string.IsNullOrEmpty( groupName2 ) )
                    ToggleButtonExtensions.RemoveCheckboxFromGrouping( groupName2, checkBox );
                ToggleButtonExtensions.AddCheckboxToGrouping( checkBox, e.NewValue.ToString() );
            }
        }

        private static void RemoveCheckboxFromGrouping( string groupName, ToggleButton checkBox )
        {
            List<ToggleButton> toggleButtonList;
            if ( ToggleButtonExtensions._elementToGroupNames.TryGetValue( groupName, out toggleButtonList ) )
            {
                toggleButtonList.Remove( checkBox );
                if ( toggleButtonList.Count == 0 )
                    ToggleButtonExtensions._elementToGroupNames.Remove( groupName );
            }
            checkBox.Click -= new RoutedEventHandler( ToggleButtonExtensions.ToggleButtonChecked );
            checkBox.Checked -= new RoutedEventHandler( ToggleButtonExtensions.ToggleButtonChecked );
            checkBox.Unloaded -= new RoutedEventHandler( ToggleButtonExtensions.ToggleButtonUnloaded );
        }

        private static void AddCheckboxToGrouping( ToggleButton checkBox, string groupName )
        {
            List<ToggleButton> toggleButtonList1;
            if ( !ToggleButtonExtensions._elementToGroupNames.TryGetValue( groupName, out toggleButtonList1 ) )
            {
                List<ToggleButton> toggleButtonList2 = new List<ToggleButton>();
                ToggleButtonExtensions._elementToGroupNames.Add( groupName, toggleButtonList2 );
            }
            ToggleButtonExtensions._elementToGroupNames[ groupName ].Add( checkBox );
            checkBox.Click += new RoutedEventHandler( ToggleButtonExtensions.ToggleButtonChecked );
            checkBox.Checked += new RoutedEventHandler( ToggleButtonExtensions.ToggleButtonChecked );
            checkBox.Unloaded += new RoutedEventHandler( ToggleButtonExtensions.ToggleButtonUnloaded );
        }

        private static void ToggleButtonUnloaded( object sender, RoutedEventArgs e )
        {
            ToggleButton toggleButton = (ToggleButton) sender;
            ToggleButtonExtensions.RemoveCheckboxFromGrouping( ToggleButtonExtensions.GetGroupName( toggleButton ), toggleButton );
        }

        private static void ToggleButtonChecked( object sender, RoutedEventArgs e )
        {
            ToggleButton originalSource = e.OriginalSource as ToggleButton;
            foreach ( ToggleButton toggleButton in ToggleButtonExtensions._elementToGroupNames[ ToggleButtonExtensions.GetGroupName( originalSource ) ] )
                toggleButton.IsChecked = new bool?( toggleButton == originalSource );
        }
    }
}
