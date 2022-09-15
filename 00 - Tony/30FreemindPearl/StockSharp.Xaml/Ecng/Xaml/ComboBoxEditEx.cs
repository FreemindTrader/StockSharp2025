using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.EditStrategy;
using DevExpress.Xpf.Editors.Services;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Validation.Native;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml
{
    /// <summary>The drop-down list to select single value.</summary>
    public class ComboBoxEditEx : ComboBoxEdit
    {
        private static readonly DependencyPropertyKey SomeItemSource = DependencyProperty.RegisterReadOnly( nameof( Source ), typeof( IItemsSource ), typeof( ComboBoxEditEx ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( PropertyChangedCallback )null ) );
        /// <summary>Current value.</summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register( nameof( Value ), typeof( object ), typeof( ComboBoxEditEx ), ( PropertyMetadata )new FrameworkPropertyMetadata( null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback( OnValuePropertyChanged ), new CoerceValueCallback( ValuePropertyCoerceValueCallback ), false, UpdateSourceTrigger.Explicit ) );
        /// <summary>Is nullable.</summary>
        public static readonly DependencyProperty IsNullableProperty = DependencyProperty.Register( nameof( IsNullable ), typeof( bool ), typeof( ComboBoxEditEx ), ( PropertyMetadata )new FrameworkPropertyMetadata( false, new PropertyChangedCallback( OnIsNullablePropertyChanged ) ) );
        /// <summary>Show obsolete.</summary>
        public static readonly DependencyProperty ShowObsoleteProperty = DependencyProperty.Register( nameof( ShowObsolete ), typeof( bool ), typeof( ComboBoxEditEx ), ( PropertyMetadata )new FrameworkPropertyMetadata( false, new PropertyChangedCallback( OnShowObsoletePropertyChanged ) ) );
        /// <summary>Sort order.</summary>
        public static readonly DependencyProperty SortOrderProperty = DependencyProperty.Register( nameof( SortOrder ), typeof( ListSortDirection? ), typeof( ComboBoxEditEx ), ( PropertyMetadata )new FrameworkPropertyMetadata( null, new PropertyChangedCallback( OnSortOrderPropertyChanged ) ) );
        /// <summary>Is searchable.</summary>
        public static readonly DependencyProperty IsSearchableProperty = DependencyProperty.Register( nameof( IsSearchable ), typeof( bool ), typeof( ComboBoxEditEx ), ( PropertyMetadata )new FrameworkPropertyMetadata( true ) );


        private static void OnValuePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( FrameworkElement )d ).GetBindingExpression( ComboBoxEditEx.ValueProperty )?.UpdateSource();
        }


        private static object ValuePropertyCoerceValueCallback( DependencyObject d, object o )
        {
            return ( ( ComboBoxEditEx )d ).OnValuePropertyCoerceValueCallback( o );
        }


        private object OnValuePropertyCoerceValueCallback( object d )
        {
            if ( d != null )
                return d;
            if ( ( this.Source == null || this.IsNullable ? 1 : ( ( ( BaseEdit )this ).EditValue == null ? 1 : 0 ) ) == 0 )
                return DependencyProperty.UnsetValue;
            return null;
        }

        private static void OnIsNullablePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( ComboBoxEditEx )d ).IsNullablePropertyChangedCallbackAction();
        }

        private void IsNullablePropertyChangedCallbackAction()
        {
            ( ( ButtonEdit )this ).RemoveClearButton();
            if ( !this.IsNullable )
                return;
            ( ( ButtonEdit )this ).AddClearButton( null );
        }

        private static void OnShowObsoletePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs _param2 )
        {
            ( ( ComboBoxEditEx )d ).SetItemsSourcePropertyCurrentValue();
        }

        private void SetItemsSourcePropertyCurrentValue()
        {
            ( ( DependencyObject )this ).SetCurrentValue( ( DependencyProperty )LookUpEditBase.ItemsSourceProperty, this.Source );
        }

        private static void OnSortOrderPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs _param2 )
        {
            ( ( ComboBoxEditEx )d ).SortOrderPropertyChangedCallBackAction();
        }

        private void SortOrderPropertyChangedCallBackAction()
        {
            ( ( DependencyObject )this ).SetCurrentValue( ( DependencyProperty )LookUpEditBase.ItemsSourceProperty, this.Source );
        }

        internal static IItemsSource OnItemsSourcePropertyChangedAction( object o, bool? b, ListSortDirection? d )
        {
            object val = o;
            bool? nullable = b;
            bool? excludeObsolete = nullable.HasValue ? new bool?( !nullable.GetValueOrDefault() ) : new bool?();
            ListSortDirection? sortOrder = d;
            return val.ToItemsSource( ( Type )null, excludeObsolete, sortOrder, ( Func<IItemsSourceItem, bool> )null, ( Func<object, string> )null, ( Func<object, string> )null );
        }

        private object OnItemsSourcePropertyChangedAction( object _param1 )
        {
            bool flag = ( ( DependencyObject )this ).ReadLocalValue( ComboBoxEditEx.ShowObsoleteProperty ) != DependencyProperty.UnsetValue;
            IItemsSource itemsSource = ComboBoxEditEx.OnItemsSourcePropertyChangedAction( _param1, flag ? new bool?( !this.ShowObsolete ) : new bool?(), this.SortOrder );
            this.Source = itemsSource;
            return ( object )itemsSource.Values;
        }


        protected override void OnItemsSourceChanged( object oldValue, object itemsSource )
        {
            base.OnItemsSourceChanged( oldValue, itemsSource );
            this.UpdateBindings();
        }

        private static object OnItemsSourcePropertyChanged( DependencyObject d, object o )
        {
            return ( ( ComboBoxEditEx )d ).OnItemsSourcePropertyChangedAction( o );
        }



        static ComboBoxEditEx()
        {
            // ISSUE: method pointer
            ( ( DependencyProperty )LookUpEditBase.ItemsSourceProperty ).OverrideMetadata( typeof( ComboBoxEditEx ), ( PropertyMetadata )new FrameworkPropertyMetadata( null, ( PropertyChangedCallback )null, new CoerceValueCallback( OnItemsSourcePropertyChanged ) ) );
            ComboBoxEditExSettings.RegisterDefaultUserEditor();
        }

        /// <summary>Initializes a new instance of the <see cref="T:Ecng.Xaml.ComboBoxEditEx" />. </summary>
        public ComboBoxEditEx()
        {
            ( ( FrameworkElement )this ).Style = ( Style )Application.Current.FindResource( ComboBoxEditEx.ComboBoxEditExStyleKey );
        }

        /// <summary>Current value.</summary>
        public object Value
        {
            get
            {
                return ( ( DependencyObject )this ).GetValue( ComboBoxEditEx.ValueProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( ComboBoxEditEx.ValueProperty, value );
            }
        }

        /// <summary>Is nullable.</summary>
        public bool IsNullable
        {
            get
            {
                return ( bool )( ( DependencyObject )this ).GetValue( ComboBoxEditEx.IsNullableProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( ComboBoxEditEx.IsNullableProperty, value );
            }
        }

        /// <summary>Current <see cref="T:Ecng.ComponentModel.IItemsSource" />.</summary>
        public IItemsSource Source
        {
            get
            {
                return ( IItemsSource )( ( DependencyObject )this ).GetValue( ComboBoxEditEx.SomeItemSource.DependencyProperty );
            }
            private set
            {
                ( ( DependencyObject )this ).SetValue( ComboBoxEditEx.SomeItemSource, value );
            }
        }

        /// <summary>Show obsolete.</summary>
        public bool ShowObsolete
        {
            get
            {
                return ( bool )( ( DependencyObject )this ).GetValue( ComboBoxEditEx.ShowObsoleteProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( ComboBoxEditEx.ShowObsoleteProperty, value );
            }
        }

        /// <summary>Sort order.</summary>
        public ListSortDirection? SortOrder
        {
            get
            {
                return ( ListSortDirection? )( ( DependencyObject )this ).GetValue( ComboBoxEditEx.SortOrderProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( ComboBoxEditEx.SortOrderProperty, value );
            }
        }

        /// <summary>Is searchable.</summary>
        public bool IsSearchable
        {
            get
            {
                return ( bool )( ( DependencyObject )this ).GetValue( ComboBoxEditEx.IsSearchableProperty );
            }
            set
            {
                ( ( DependencyObject )this ).SetValue( ComboBoxEditEx.IsSearchableProperty, value );
            }
        }

        /// <summary>
        /// </summary>
        public static ComponentResourceKey ComboBoxEditExStyleKey
        {
            get
            {
                return new ComponentResourceKey( typeof( ComboBoxEditEx ), "ComboBoxEditExStyleKey" );
            }
        }

        internal static IItemsSource OnDChanged(
          object _param0,
          bool? _param1,
          ListSortDirection? _param2 )
        {
            object val = _param0;
            bool? nullable = _param1;
            bool? excludeObsolete = nullable.HasValue ? new bool?( !nullable.GetValueOrDefault() ) : new bool?();
            ListSortDirection? sortOrder = _param2;
            return val.ToItemsSource( ( Type )null, excludeObsolete, sortOrder, ( Func<IItemsSourceItem, bool> )null, ( Func<object, string> )null, ( Func<object, string> )null );
        }

        private object OnDChanged( object _param1 )
        {
            bool flag = ( ( DependencyObject )this ).ReadLocalValue( ComboBoxEditEx.ShowObsoleteProperty ) != DependencyProperty.UnsetValue;
            IItemsSource itemsSource = ComboBoxEditEx.OnDChanged( _param1, flag ? new bool?( !this.ShowObsolete ) : new bool?(), this.SortOrder );
            this.Source = itemsSource;
            return itemsSource.Values;
        }









        /// <inheritdoc />
        protected override void OnValueMemberChanged( string valueMember )
        {
            base.OnValueMemberChanged( valueMember );
            this.UpdateBindings();
        }

        /// <inheritdoc />


        /// <inheritdoc />
        protected override void OnEditModeChanged( EditMode oldValue, EditMode newValue )
        {
            base.OnEditModeChanged( oldValue, newValue );
            this.UpdateBindings();
        }

        /// <summary>
        /// Auto update bindings when dependency properties changed.
        /// </summary>
        protected virtual void UpdateBindings()
        {
            PropertyGridControl propertyGrid = PropertyGridHelper.GetPropertyGrid( ( DependencyObject )this );
            object itemsSource = ( this ).ItemsSource;
            if ( EditMode != EditMode.Standalone || itemsSource == null || propertyGrid != null )
            {
                BindingOperations.ClearBinding( ( DependencyObject )this, ( DependencyProperty )BaseEdit.EditValueProperty );
            }
            else
            {
                BindingOperations.SetBinding( ( DependencyObject )this, ( DependencyProperty )BaseEdit.EditValueProperty, ( BindingBase )new Binding( nameof( 2127277753 ) )
                {
                    Source = this,
                    Mode = BindingMode.TwoWay,
                    Converter = ( IValueConverter )new ComboBoxEditEx.MyIValueConverter( this ),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                } );
            }

        }

        /// <inheritdoc />
        protected override BaseEditSettings CreateEditorSettings()
        {
            return ( BaseEditSettings )new ComboBoxEditExSettings();
        }

        /// <inheritdoc />
        protected override EditStrategyBase CreateEditStrategy()
        {
            return ( EditStrategyBase )new ComboBoxEditEx.SomeComboBoxEditStrategy( ( ComboBoxEdit )this );
        }

        /// <summary>
        /// </summary>
        protected virtual object TryConvertEditValue( object ev )
        {
            return ev;
        }

        /// <summary>
        /// </summary>
        protected virtual object TryConvertBaseValue( SelectorPropertiesCoercionHelper helper, object bv )
        {
            return helper.GetEditValueFromBaseValue( bv );
        }

        [Serializable]
        private sealed class SomeShit
        {
            public static readonly ComboBoxEditEx.SomeShit ShitMethod02 = new ComboBoxEditEx.SomeShit();

            internal object OnItemsSourceChanged( DependencyObject _param1, object _param2 )
            {
                return ( ( ComboBoxEditEx )_param1 ).OnDChanged( _param2 );
            }

            internal void OnValuePropertyChanged( DependencyObject _param1, DependencyPropertyChangedEventArgs _param2 )
            {
                ( ( FrameworkElement )_param1 ).GetBindingExpression( ComboBoxEditEx.ValueProperty )?.UpdateSource();
            }

            internal object ValuePropertyCoerceValueCallback( DependencyObject _param1, object _param2 )
            {
                return ( ( ComboBoxEditEx )_param1 ).OnValuePropertyCoerceValueCallback( _param2 );
            }

            internal void IsNullablePropertyChangedCallback(
              DependencyObject _param1,
              DependencyPropertyChangedEventArgs _param2 )
            {
                ( ( ComboBoxEditEx )_param1 ).IsNullablePropertyChangedCallbackAction();
            }

            internal void ShowObsoletePropertyPropertyChangedCallBack(
              DependencyObject _param1,
              DependencyPropertyChangedEventArgs _param2 )
            {
                ( ( ComboBoxEditEx )_param1 ).SetItemsSourcePropertyCurrentValue();
            }

            internal void SortOrderPropertyChangedCallBack(
              DependencyObject _param1,
              DependencyPropertyChangedEventArgs _param2 )
            {
                ( ( ComboBoxEditEx )_param1 ).SortOrderPropertyChangedCallBackAction();
            }
        }

        private sealed class MyTextInputValueContainerService : TextInputValueContainerService
        {
            public MyTextInputValueContainerService( TextEditBase _param1 ) : base( _param1 )
            {

            }

            public override void SetEditValue( object _param1, UpdateEditorSource _param2 )
            {
                ( ( ValueContainerService )this ).SetEditValue( ( ( ComboBoxEditEx )( this ).OwnerEdit ).TryConvertEditValue( _param1 ) ?? _param1, _param2 );
            }
        }

        private sealed class MyIValueConverter : IValueConverter
        {

            private readonly Type MyType;

            private readonly ComboBoxEditEx MyComboBoxEditEx;

            private readonly Type MyType2;

            public MyIValueConverter( ComboBoxEditEx _param1 )
            {
                ComboBoxEditEx comboBoxEditEx = _param1;
                if ( comboBoxEditEx == null )
                    throw new ArgumentNullException( "comboBoxEditEx == null" );
                this.MyComboBoxEditEx = comboBoxEditEx;
                this.MyType2 = this.MyComboBoxEditEx.Source?.ValueType;
                if ( !( this.MyType2 != ( Type )null ) )
                    return;
                this.MyType = typeof( IEnumerable<> ).Make( this.MyType2 );
            }

            object IValueConverter.Convert(
              object _param1,
              Type _param2,
              object _param3,
              CultureInfo _param4 )
            {
                return _param1;
            }

            object IValueConverter.ConvertBack(
              object _param1,
              Type _param2,
              object _param3,
              CultureInfo _param4 )
            {
                if ( this.MyType2 == ( Type )null )
                    return null;
                bool flag = this.MyComboBoxEditEx is SubsetComboBox;
                if ( _param1 == null )
                {
                    if ( !flag )
                        return null;
                    return this.MyType2.CreateArray( 0 );
                }
                if ( !flag )
                    return _param1.To( this.MyType2 );
                if ( this.MyType.IsInstanceOfType( _param1 ) )
                    return _param1;
                IEnumerable<object> source = _param1 as IEnumerable<object>;
                object[ ] objArray = source != null ? source.ToArray<object>() : ( object[ ] )null;
                if ( objArray == null )
                    return null;
                Array array = this.MyType2.CreateArray( objArray.Length );
                for ( int index = 0; index < objArray.Length; ++index )
                    array.SetValue( objArray[index].To( this.MyType2 ), index );
                return array;
            }
        }

        private sealed class SomeComboBoxEditStrategy : ComboBoxEditStrategy
        {
            public SomeComboBoxEditStrategy( ComboBoxEdit _param1 ) : base( _param1 )
            {

            }

            protected override ValueContainerService CreateValueContainerService()
            {
                return ( ValueContainerService )new ComboBoxEditEx.MyTextInputValueContainerService( ( this ).Editor );
            }

            protected override void RegisterUpdateCallbacks()
            {
                RegisterUpdateCallbacks();

                ( this ).PropertyUpdater.Register( BaseEdit.EditValueProperty, p => p,
                                                                                x =>
                                                                                {
                                                                                    ComboBoxEditEx editor = ( ComboBoxEditEx )( this ).Editor;
                                                                                    object ev = editor.TryConvertBaseValue( ( this ).SelectorUpdater, x );
                                                                                    return editor.TryConvertEditValue( ev ) ?? ev;
                                                                                } );

            }

            protected override int TextSearchCallback( string _param1, int _param2, bool _param3 )
            {
                ComboBoxEditEx.SomeComboBoxEditStrategy.SomeCultureInfoShit m8ObApxeqhyecvZg = new ComboBoxEditEx.SomeComboBoxEditStrategy.SomeCultureInfoShit();
                m8ObApxeqhyecvZg.myString = _param1;
                ComboBoxEditEx editor = ( ComboBoxEditEx )( this ).Editor;
                if ( m8ObApxeqhyecvZg.myString.IsEmptyOrWhiteSpace() || !editor.IsSearchable )
                    return ( this ).TextSearchCallback( m8ObApxeqhyecvZg.myString, _param2, _param3 );
                m8ObApxeqhyecvZg.myCultureInfo = CultureInfo.InstalledUICulture;
                m8ObApxeqhyecvZg.myString = m8ObApxeqhyecvZg.myString.ToLower( m8ObApxeqhyecvZg.myCultureInfo );
                IItemsSource source = editor.Source;
                if ( source == null )
                    return -1;
                return source.Values.IndexOf<IItemsSourceItem>( new Func<IItemsSourceItem, bool>( m8ObApxeqhyecvZg.SomeCultureStuff ) );
            }

            private object EditValuePropertyCoercionHandler( object p )
            {
                ComboBoxEditEx editor = ( ComboBoxEditEx )( this ).Editor;
                object ev = editor.TryConvertBaseValue( ( this ).SelectorUpdater, p );
                return editor.TryConvertEditValue( ev ) ?? ev;
            }

            [Serializable]
            private sealed class SomeShit
            {
                public static readonly ComboBoxEditEx.SomeComboBoxEditStrategy.SomeShit ShitMethod02 = new ComboBoxEditEx.SomeComboBoxEditStrategy.SomeShit();
                public static PropertyCoercionHandler myPropertyCoercionHandler;

                internal object SomeShitGet( object _param1 )
                {
                    return _param1;
                }
            }

            private sealed class SomeCultureInfoShit
            {
                public CultureInfo myCultureInfo;
                public string myString;

                internal bool SomeCultureStuff( IItemsSourceItem _param1 )
                {
                    string displayName = _param1.DisplayName;
                    if ( displayName == null )
                        return false;
                    return displayName.ToLower( this.myCultureInfo ).Contains( this.myString );
                }
            }
        }
    }
}
