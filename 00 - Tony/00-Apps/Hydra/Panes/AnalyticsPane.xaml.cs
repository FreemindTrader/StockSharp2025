using DevExpress.Xpf.Docking;
using DevExpress.Xpf.PropertyGrid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Compilation;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Strategies;
using StockSharp.Algo.Strategies.Analytics;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Windows;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml.Code;
using StockSharp.Xaml.CodeEditor;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    [DisplayNameLoc( "Analytics" )]
    public partial class AnalyticsPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {
        private static IEnumerable<CodeReference> _refs;
        private readonly AnalyticsScriptParameters _parameters;
        private readonly SimpleResettableTimer _timer;
        private const string _parametersKey = "Parameters";
        
        public ICommand StartCommand { get; }

        public ICommand UndoCommand
        {
            get
            {
                return CodePanel.UndoCommand;
            }
        }

        public ICommand RedoCommand
        {
            get
            {
                return CodePanel.RedoCommand;
            }
        }

        public ICommand ReferencesCommand
        {
            get
            {
                return CodePanel.ReferencesCommand;
            }
        }

        public AnalyticsPane()
        {
            InitializeComponent();
            _timer = new SimpleResettableTimer( TimeSpan.FromSeconds( 2.0 ) );
            _timer.Elapsed += () => GuiDispatcher.GlobalDispatcher.AddAction( new Action( CompileCode ) );
            PropertyGridEx propertyGrid = PropertyGrid;
            AnalyticsScriptParameters scriptParameters1 = new AnalyticsScriptParameters( this );
            scriptParameters1.Drive = ServicesRegistry.DriveCache.DefaultDrive;
            AnalyticsScriptParameters scriptParameters2 = scriptParameters1;
            _parameters = scriptParameters1;
            AnalyticsScriptParameters scriptParameters3 = scriptParameters2;
            propertyGrid.SelectedObject = scriptParameters3;
            if ( _refs == null )
                _refs = CodeExtensions.DefaultReferences.Where( s => !s.EqualsIgnoreCase( "StockSharp.Xaml.Diagram" ) ).ToReferences();
            CodePanel.References.AddRange( _refs );
            StartCommand = new DelegateCommand( new Action<object>( ExecutedStartCommand ), p =>
            {
                BaseAnalyticsStrategy analyticsStrategy = _parameters.AnalyticsStrategy;
                if ( analyticsStrategy == null )
                    return false;
                return analyticsStrategy.ProcessState == ProcessStates.Stopped;
            } );
        }

        public void Init( AnalyticsTemplate template )
        {
            if ( template == null )
                throw new ArgumentNullException( nameof( template ) );
            _parameters.Title = template.Title;
            Code = template.Body;
            CompileCode();
        }

        private string Code
        {
            get
            {
                return CodePanel.Code;
            }
            set
            {
                CodePanel.Code = value;
            }
        }

        public override void Load( SettingsStorage storage )
        {
            Code = storage.GetValue<string>( "Code", null );
            CodePanel.References.Clear();
            object obj;
            if ( storage.TryGetValue( "References", out obj ) )
                CodePanel.References.AddRange( obj.To<IEnumerable<SettingsStorage>>().Select( s => s.Load<CodeReference>() ) );
            CodePanel.Load( storage.GetValue<SettingsStorage>( "CodePanel", null ) );
            _parameters.Load( storage.GetValue<SettingsStorage>( "Parameters", null ) );
            base.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            storage.SetValue( "Code", Code );
            storage.SetValue( "References", CodePanel.References.Select( r => r.Save() ).ToArray() );
            storage.SetValue( "CodePanel", CodePanel.Save() );
            storage.SetValue( "Parameters", _parameters.Save() );
            base.Save( storage );
        }

        bool IPane.IsValid
        {
            get
            {
                return true;
            }
        }

        private void CodePanel_OnReferencesUpdated()
        {
            CompileCode();
        }

        private void CodePanel_OnCompilingCode()
        {
            CompileCode();
        }

        private void CodePanel_OnCodeChanged()
        {
            _timer.Reset();
        }

        private void CompileCode()
        {
            CompilationResult result = ServicesRegistry.CompilerService.GetCompiler( CompilationLanguages.CSharp ).CompileCode( Code, "Analytic", CodePanel.References );
            CodePanel.ShowCompilationResult( result, false );
            if ( result.HasErrors() )
                return;
            Type type = result.Assembly.GetTypes().FirstOrDefault( t =>
            {
                if ( !t.IsAbstract )
                    return t.IsSubclassOf( typeof( Strategy ) );
                return false;
            } );
            try
            {
                _parameters.AnalyticsStrategy = type.CreateInstance<BaseAnalyticsStrategy>();
            }
            catch ( Exception ex )
            {
                ex.LogError( null );
            }
        }

        private void ExecutedStartCommand( object parameter )
        {
            BaseAnalyticsStrategy analyticsStrategy = _parameters.AnalyticsStrategy;
            analyticsStrategy.From = _parameters.From;
            analyticsStrategy.To = _parameters.To;
            analyticsStrategy.ResultType = _parameters.ResultType;
            analyticsStrategy.Environment.SetValue( "Drive", _parameters.Drive );
            analyticsStrategy.Environment.SetValue( "StorageFormat", _parameters.StorageFormat );
            StorageRegistry storageRegistry = new StorageRegistry();
            IMarketDataDrive drive = _parameters.Drive;
            if ( drive != null )
                storageRegistry.DefaultDrive = drive;
            analyticsStrategy.StorateRegistry = storageRegistry;
            AnalyticsResultPane analyticsResultPane = new AnalyticsResultPane();
            MainWindow.Instance.ShowPane( analyticsResultPane );
            analyticsResultPane.Bind( analyticsStrategy );
        }

        private void PropertyGrid_OnCustomExpand( object sender, CustomExpandEventArgs args )
        {
            if ( !args.IsInitializing || !args.Row.Path.EqualsIgnoreCase( "AnalyticsStrategy" ) )
                return;
            args.IsExpanded = true;
        }

        

        [DisplayNameLoc( "Str225" )]
        [DescriptionLoc( "Str2836", false )]
        private class AnalyticsScriptParameters : NotifiableObject, IPersistable
        {
            private readonly AnalyticsPane _parent;
            private string _title;
            private Security _security;
            private DateTime _from;
            private DateTime _to;
            private IMarketDataDrive _drive;
            private StorageFormats _storageFormat;
            private AnalyticsResultTypes _resultType;
            private BaseAnalyticsStrategy _analyticsStrategy;

            public AnalyticsScriptParameters( AnalyticsPane parent )
            {
                AnalyticsPane analyticsPane = parent;
                if ( analyticsPane == null )
                    throw new ArgumentNullException( nameof( parent ) );
                _parent = analyticsPane;
                To = DateTime.MaxValue;
            }

            [Display( Description = "Str1359", GroupName = "General", Name = "Name", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
            public string Title
            {
                get
                {
                    return _title;
                }
                set
                {
                    _title = value;
                    NotifyChanged( nameof( Title ) );
                    _parent.Title = LocalizedStrings.Analytics + " " + _title;
                    if ( _analyticsStrategy == null )
                        return;
                    _analyticsStrategy.Name = Title;
                }
            }

            [Display( Description = "SecurityDot", GroupName = "General", Name = "Security", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
            public Security Security
            {
                get
                {
                    return _security;
                }
                set
                {
                    _security = value;
                    NotifyChanged( nameof( Security ) );
                    if ( _analyticsStrategy == null )
                        return;
                    _analyticsStrategy.Security = Security;
                }
            }

            [Display( Description = "Str1222", GroupName = "General", Name = "Str343", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
            public DateTime From
            {
                get
                {
                    return _from;
                }
                set
                {
                    _from = value;
                    NotifyChanged( nameof( From ) );
                }
            }

            [Display( Description = "Str418", GroupName = "General", Name = "Str345", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
            public DateTime To
            {
                get
                {
                    return _to;
                }
                set
                {
                    _to = value;
                    NotifyChanged( nameof( To ) );
                }
            }

            [Display( Description = "Str2838", GroupName = "General", Name = "Str2804", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
            [Editor( typeof( DriveComboBoxEditor ), typeof( DriveComboBoxEditor ) )]
            public IMarketDataDrive Drive
            {
                get
                {
                    return _drive;
                }
                set
                {
                    _drive = value;
                    NotifyChanged( nameof( Drive ) );
                }
            }

            [Display( Description = "Str2240", GroupName = "General", Name = "Str2239", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
            public StorageFormats StorageFormat
            {
                get
                {
                    return _storageFormat;
                }
                set
                {
                    _storageFormat = value;
                    NotifyChanged( nameof( StorageFormat ) );
                }
            }

            [Display( Description = "ResultTypeDot", GroupName = "General", Name = "Str1738", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
            public AnalyticsResultTypes ResultType
            {
                get
                {
                    return _resultType;
                }
                set
                {
                    _resultType = value;
                    NotifyChanged( nameof( ResultType ) );
                }
            }

            [TypeConverter( typeof( ExpandableObjectConverter ) )]
            public BaseAnalyticsStrategy AnalyticsStrategy
            {
                get
                {
                    return _analyticsStrategy;
                }
                set
                {
                    _analyticsStrategy = value;
                    NotifyChanged( nameof( AnalyticsStrategy ) );
                    if ( _analyticsStrategy == null )
                        return;
                    _analyticsStrategy.Security = Security;
                    _analyticsStrategy.Name = Title;
                }
            }

            public void Load( SettingsStorage storage )
            {
                Title = storage.GetValue<string>( "Title", null );
                From = storage.GetValue( "From", new DateTime() );
                To = storage.GetValue( "To", new DateTime() );
                if ( storage.ContainsKey( "Drive" ) )
                    Drive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "Drive", null ) );
                StorageFormat = storage.GetValue( "StorageFormat", StorageFormats.Binary );
                ResultType = storage.GetValue( "ResultType", AnalyticsResultTypes.Grid );
                _parent.CompileCode();
                if ( AnalyticsStrategy != null && storage.ContainsKey( "AnalyticsStrategy" ) )
                    AnalyticsStrategy.Load( storage.GetValue<SettingsStorage>( "AnalyticsStrategy", null ) );
                if ( !storage.ContainsKey( "Security" ) )
                    return;
                Security = SecurityProvider.LookupById( storage.GetValue<string>( "Security", null ) );
            }

            public void Save( SettingsStorage storage )
            {
                storage.SetValue( "Title", Title );
                storage.SetValue( "From", From );
                storage.SetValue( "To", To );
                if ( Drive != null )
                    storage.SetValue( "Drive", Drive.Path );
                storage.SetValue( "StorageFormat", StorageFormat.To<string>() );
                storage.SetValue( "ResultType", ResultType.To<string>() );
                if ( AnalyticsStrategy != null )
                    storage.SetValue( "AnalyticsStrategy", AnalyticsStrategy.Save() );
                if ( Security == null )
                    return;
                storage.SetValue( "Security", Security.Id );
            }
        }
    }
}
