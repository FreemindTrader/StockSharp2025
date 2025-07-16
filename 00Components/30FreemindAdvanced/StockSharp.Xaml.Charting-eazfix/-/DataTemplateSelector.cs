using System;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Xaml.Charting
{
    public abstract class DataTemplateSelector : ContentControl, IDataTemplateSelector
    {
        public static readonly DependencyProperty DefaultTemplateProperty = DependencyProperty.Register(nameof (DefaultTemplate), typeof (DataTemplate), typeof (DataTemplateSelector), new PropertyMetadata((object) null, new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));

        public DataTemplate DefaultTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( DataTemplateSelector.DefaultTemplateProperty );
            }
            set
            {
                this.SetValue( DataTemplateSelector.DefaultTemplateProperty, ( object ) value );
            }
        }

        protected void UpdateControlTemplate()
        {
            if ( this.ContentTemplate != null )
            {
                return;
            }

            this.ContentTemplate = this.SelectTemplate( this.Content, ( DependencyObject ) this );
        }

        public virtual DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            return this.DefaultTemplate;
        }

        public event EventHandler DataTemplateChanged;

        protected override void OnContentChanged( object oldContent, object newContent )
        {
            base.OnContentChanged( oldContent, newContent );
            this.ContentTemplate = this.SelectTemplate( newContent, ( DependencyObject ) this );
        }

        protected void OnDataTemplateChanged()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler dataTemplateChanged = this.DataTemplateChanged;
            if ( dataTemplateChanged == null )
            {
                return;
            }

            dataTemplateChanged( ( object ) this, EventArgs.Empty );
        }

        protected static void OnDefautlTemplateDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            DataTemplateSelector templateSelector = d as DataTemplateSelector;
            if ( templateSelector == null )
            {
                return;
            }

            templateSelector.UpdateControlTemplate();
            templateSelector.OnDataTemplateChanged();
        }
    }
}
