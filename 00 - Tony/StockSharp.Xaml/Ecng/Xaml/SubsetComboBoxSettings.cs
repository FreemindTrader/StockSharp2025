using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using System;
using System.Windows;

namespace Ecng.Xaml
{
    /// <summary>
    /// Edit settings for <see cref="T:Ecng.Xaml.SubsetComboBox" />.
    /// </summary>
    public class SubsetComboBoxSettings : ComboBoxEditExSettings
    {
        /// <summary>Display selected items count.</summary>
        public static readonly DependencyProperty DisplaySelectedItemsCountProperty = DependencyProperty.Register( nameof( DisplaySelectedItemsCount ), typeof( bool ), typeof( SubsetComboBoxSettings ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )false ) );

        static SubsetComboBoxSettings()
        {
            SubsetComboBoxSettings.RegisterDefaultUserEditor();
        }

        /// <inheritdoc />
        public SubsetComboBoxSettings()
        {
            ( ( FrameworkContentElement )this ).Style = ( Style )Application.Current.FindResource( ( object )SubsetComboBoxSettings.SubsetComboBoxSettingsStyleKey );
        }

        /// <summary>
        /// </summary>
        public static ComponentResourceKey SubsetComboBoxSettingsStyleKey
        {
            get
            {
                return new ComponentResourceKey( typeof( ComboBoxEditEx ), nameof( SubsetComboBoxSettingsStyleKey ) );
            }
        }

        /// <summary>Display selected items count.</summary>
        public bool DisplaySelectedItemsCount
        {
            get
            {
                return ( bool )( ( DependencyObject )this ).GetValue( SubsetComboBoxSettings.DisplaySelectedItemsCountProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( SubsetComboBoxSettings.DisplaySelectedItemsCountProperty, ( object )value );
            }
        }

        internal new static void RegisterDefaultUserEditor()
        {            
            EditorSettingsProvider.Default.RegisterUserEditor( 
                                                                typeof( SubsetComboBox ), 
                                                                typeof( SubsetComboBoxSettings ), 
                                                                () => new SubsetComboBox(), 
                                                                () => new SubsetComboBoxSettings() 
                                                             );
        }

        /// <inheritdoc />
        protected override void AssignToEditCore( IBaseEdit e )
        {
            var cb = e as SubsetComboBox;
            if ( cb != null )
            {
                this.SetValueFromSettings( SubsetComboBoxSettings.DisplaySelectedItemsCountProperty, () => cb.DisplaySelectedItemsCount = this.DisplaySelectedItemsCount );
            }

            base.AssignToEditCore( e );
        }

    }
}

