using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using System;
using System.ComponentModel;
using System.Windows;

namespace Ecng.Xaml
{
    /// <summary>
    /// Edit settings for <see cref="T:Ecng.Xaml.ComboBoxEditEx" />.
    /// </summary>
    public class ComboBoxEditExSettings : ComboBoxEditSettings
    {
        /// <summary>Current value.</summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register( nameof( Value ), typeof( object ), typeof( ComboBoxEditExSettings ) );
        /// <summary>Is searchable.</summary>
        public static readonly DependencyProperty IsSearchableProperty = DependencyProperty.Register( nameof( IsSearchable ), typeof( bool ), typeof( ComboBoxEditExSettings ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )true ) );

        static ComboBoxEditExSettings()
        {

            ( ( DependencyProperty )LookUpEditSettingsBase.ItemsSourceProperty ).OverrideMetadata( typeof( ComboBoxEditExSettings ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )null, ( PropertyChangedCallback )null, new CoerceValueCallback( OnItemsSourcePropertyChanged ) ) );
            ComboBoxEditExSettings.RegisterDefaultUserEditor();
        }

        private static void OnShowCandlePatternProperty( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {

        }

        private static object OnItemsSourcePropertyChanged( DependencyObject d, object e )
        {
            return ( ( ComboBoxEditExSettings )d ).OnDChanged( e );
        }

        private object OnDChanged( object _param1 )
        {
            return ( object )ComboBoxEditEx.OnDChanged( _param1, new bool?(), new ListSortDirection?() ).Values;
        }

        //internal void IsNullablePropertyChangedCallback( DependencyObject d, DependencyPropertyChangedEventArgs e )
        //{
        //    var editEx = ( ( ComboBoxEditEx )d );

        //    editEx.IsNullablePropertyChangedCallbackAction();
        //}


        /// <summary>Initializes a new instance of the <see cref="T:Ecng.Xaml.ComboBoxEditExSettings" />. </summary>
        public ComboBoxEditExSettings()
        {
            ( ( FrameworkContentElement )this ).Style = ( Style )Application.Current.FindResource( ( object )ComboBoxEditExSettings.ComboBoxEditExSettingsStyleKey );
        }

        /// <summary>
        /// </summary>
        public static ComponentResourceKey ComboBoxEditExSettingsStyleKey
        {
            get
            {
                return new ComponentResourceKey( typeof( ComboBoxEditEx ), "ComboBoxEditExSettingsStyleKey" );
            }
        }



        internal static void RegisterDefaultUserEditor()
        {

            EditorSettingsProvider.Default.RegisterUserEditor(  typeof( ComboBoxEditEx ), 
                                                                typeof( ComboBoxEditExSettings ),                                                                 
                                                                () => ( IBaseEdit )new ComboBoxEditEx(),
                                                                () => ( BaseEditSettings )new ComboBoxEditExSettings()
                                                             );
        }

        /// <summary>Current value.</summary>
        public object Value
        {
            get
            {
                return ( ( DependencyObject )this ).GetValue( ComboBoxEditExSettings.ValueProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( ComboBoxEditExSettings.ValueProperty, value );
            }
        }

        /// <summary>Is searchable.</summary>
        public bool IsSearchable
        {
            get
            {
                return ( bool )( ( DependencyObject )this ).GetValue( ComboBoxEditExSettings.IsSearchableProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( ComboBoxEditExSettings.IsSearchableProperty, ( object )value );
            }
        }

        /// <inheritdoc />
        protected override void AssignToEditCore( IBaseEdit e )
        {            
            var cbEx = e as ComboBoxEditEx;
            
            if ( cbEx != null )
            {
                SetValueFromSettings( ComboBoxEditExSettings.ValueProperty,        () => cbEx.Value = this.Value );
                SetValueFromSettings( ComboBoxEditExSettings.IsSearchableProperty, () => cbEx.IsSearchable = this.IsSearchable );
            }

            base.AssignToEditCore( e );
        }

        

        //[Serializable]
        //private sealed class SomeShit
        //{
        //    public static readonly ComboBoxEditExSettings.SomeShit ShitMethod02 = new ComboBoxEditExSettings.SomeShit();
        //    public static CreateEditorMethod CreateEditorMethod001;
        //    public static CreateEditorSettingsMethod CreateEditorSettingsMethod002;

        //    internal object OnItemsSourcePropertyChanged(
        //      DependencyObject _param1,
        //      object _param2 )
        //    {
        //        return ( ( ComboBoxEditExSettings )_param1 ).OnDChanged( _param2 );
        //    }

        //    internal IBaseEdit CreateComboBoxEditEx()
        //    {
        //        return ( IBaseEdit )new ComboBoxEditEx();
        //    }

        //    internal BaseEditSettings CreateComboBoxEditExSettings()
        //    {
        //        return ( BaseEditSettings )new ComboBoxEditExSettings();
        //    }
        //}

        
    }
}
