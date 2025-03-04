//using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.NavBar;
using Ecng.Xaml;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Hydra.Controls
{
    public partial class TaskNavigator : NavBarControl, IComponentConnector
    {
        public static readonly DependencyProperty TasksProperty = DependencyProperty.Register( nameof( Tasks ), typeof( IList<IHydraTask> ), typeof( TaskNavigator ), new PropertyMetadata( new ObservableCollection<IHydraTask>() ) );
        
        public TaskNavigator()
        {
            InitializeComponent();
        }

        public IList<IHydraTask> Tasks
        {
            get
            {
                return ( IList<IHydraTask> )GetValue( TasksProperty );
            }
            set
            {
                SetValue( TasksProperty, value );
            }
        }

        public bool IsSourcesSelected
        {
            get
            {
                return SelectedGroup == SourcesGroup;
            }
            set
            {
                SelectedGroup = value ? SourcesGroup : ( object )ToolsGroup;
            }
        }

        public IHydraTask SelectedTask
        {
            get
            {
                return ( IHydraTask )SelectedItem;
            }
            set
            {
                SelectedItem = value;
            }
        }

        public event Action TaskSelected;

        public event Action DoubleClick;

        private void SortedSources_OnFilter( object sender, FilterEventArgs e )
        {
            e.Accepted = !IsAccept( e, MessageAdapterCategories.Tool );
        }

        private void SortedTools_OnFilter( object sender, FilterEventArgs e )
        {
            e.Accepted = IsAccept( e, MessageAdapterCategories.Tool );
        }

        private static bool IsAccept( FilterEventArgs e, MessageAdapterCategories category )
        {
            IHydraTask task = ( IHydraTask )e.Item;
            if ( task == null )
                return false;
            return task.IsCategoryOf( category );
        }

        private void NavBarViewBase_OnItemSelected( object sender, NavBarItemSelectedEventArgs e )
        {
            Action taskSelected = TaskSelected;
            if ( taskSelected == null )
                return;
            taskSelected();
        }

        private void TaskNavigator_OnLoaded( object sender, RoutedEventArgs e )
        {
            if ( this.IsDesignMode() )
                return;
            AutoRefreshCollectionViewSource resource = ( AutoRefreshCollectionViewSource )FindResource( "SortedSources" );
            if ( resource == null )
                return;
            ( ( ListCollectionView )resource.View ).CustomSort = new LanguageSorter();
        }

        private void Control_OnMouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            Action doubleClick = DoubleClick;
            if ( doubleClick == null )
                return;
            doubleClick();
        }

        

        private sealed class LanguageSorter : IComparer
        {
            int IComparer.Compare( object x, object y )
            {
                IHydraTask task1 = ( IHydraTask )x;
                IHydraTask task2 = ( IHydraTask )y;
                if ( task1.IsEnabled != task2.IsEnabled )
                    return task2.IsEnabled.CompareTo( task1.IsEnabled );
                string language1 = GetLanguage( task1 );
                string language2 = GetLanguage( task2 );
                int num1 = language1 == LocalizedStrings.ActiveLanguage ? -1 : language1.GetHashCode();
                int num2 = language2 == LocalizedStrings.ActiveLanguage ? -1 : language2.GetHashCode();
                if ( num1 == num2 )
                    return string.Compare( task1.ToString(), task2.ToString(), StringComparison.Ordinal );
                return num1.CompareTo( num2 );
            }

            private static string GetLanguage( IHydraTask task )
            {
                if ( task == null )
                    throw new ArgumentNullException( nameof( task ) );
                return task.GetType().GetCategories().GetPreferredLanguage();
            }
        }
    }
}
