using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Ecng.Common;
using Ecng.Xaml;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace StockSharp.Xaml.GridControl
{
    public class SearchBox : TextEdit
    {
        public static readonly DependencyProperty ItemsPanelProperty = DependencyProperty.Register(nameof (ItemsPanel), typeof (FrameworkElement), typeof (SearchBox), new PropertyMetadata((PropertyChangedCallback) null));

        public SearchBox( )
        {            
            ( ( UIElement ) this ).IsVisibleChanged += new DependencyPropertyChangedEventHandler( this.SearchBox_IsVisibleChanged );
        }

        public FrameworkElement ItemsPanel
        {
            get
            {
                return ( FrameworkElement ) GetValue( SearchBox.ItemsPanelProperty );
            }
            set
            {
                SetValue( ItemsPanelProperty, value );
            }
        }

        private void SearchBox_IsVisibleChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            if ( !( bool ) e.NewValue )
                return;

            this.GuiAsync( () => this.Focus()  );            
        }

        protected override void OnTextChanged( string oldText, string text )
        {
            base.OnTextChanged( oldText, text );

            if ( this.ItemsPanel == null )
                return;

            foreach ( var element in  SearchBox.OnVisibleChange( this.ItemsPanel, x => x is GridColumnHeader ) )
            {
                var dataContext = element.DataContext;
                var header = ((BaseColumn) dataContext).Header;

                string str;

                if ( header == null )
                {
                    str = null;
                }
                else
                {
                    str = header.ToString();
                    if ( str != null )
                    {
                        element.Visibility = StringHelper.ContainsIgnoreCase( str, ( ( TextEditBase ) this ).Text ) ? Visibility.Visible : Visibility.Collapsed;

                        return;
                    }                        
                }
                str = ( ( ColumnBase ) dataContext ).FieldName;                            
            }
        }

        private static FrameworkElements OnVisibleChange( FrameworkElement element, Predicate<FrameworkElement> predicate_0 )
        {
            FrameworkElements frameworkElements = new FrameworkElements();
            var visualTreeEnumerator = new VisualTreeEnumerator( (DependencyObject) element);

            while ( ( ( NestedObjectEnumeratorBase ) visualTreeEnumerator ).MoveNext() )
            {
                FrameworkElement current;
                if ( ( current = visualTreeEnumerator.Current as FrameworkElement ) != null && predicate_0( current ) )
                {
                    frameworkElements.Add( current );
                }
                    
            }
            return frameworkElements;
        }

        

        
    }
}
