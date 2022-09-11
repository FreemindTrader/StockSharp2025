using DevExpress.Xpf.PropertyGrid;
using Ecng.Xaml;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Windows;


namespace fx.Charting
{
    /// <summary>
    /// Interaction logic for UltrachartAnnotationEditor.xaml
    /// </summary>
    public partial class UltrachartAnnotationEditor : NonTopmostPopup
    {
        public static readonly DependencyProperty PropertyDefinitionsProperty = DependencyProperty.Register( nameof( PropertyDefinitions ), typeof( IEnumerable<PropertyDefinition> ), typeof( UltrachartAnnotationEditor ), new PropertyMetadata(   null ) );
        public UltrachartAnnotationEditor( )
        {
            InitializeComponent( );
        }

        public IEnumerable<PropertyDefinition> PropertyDefinitions
        {
            get
            {
                return ( IEnumerable<PropertyDefinition> ) GetValue( PropertyDefinitionsProperty );
            }
            set
            {
                SetValue( PropertyDefinitionsProperty, value );
            }
        }
    }
}
