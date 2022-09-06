
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace StockSharp.Diagram
{
    /// <summary>The diagram element.</summary>
    public abstract class DiagramElement : BaseLogReceiver, INotifyPropertyChanging, INotifyPropertyChanged, ICustomTypeDescriptor, INotifyPropertiesChanged, IPersistable
    {
        /// <summary>The available properties change event.</summary>
        public event Action PropertiesChanged;

        /// <summary>
        /// To call the <see cref="E:StockSharp.Diagram.DiagramElement.PropertiesChanged" /> event.
        /// </summary>
        protected virtual void RaisePropertiesChanged()
        {
            Action pc = PropertiesChanged;
            if ( pc == null )
                return;
            pc();
        }

        /// <summary>The diagram element properties value changing event.</summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// To call the <see cref="E:StockSharp.Diagram.DiagramElement.PropertyChanging" /> event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void RaisePropertyChanging( string propertyName )
        {
            RaisePropertyChanging( this, new PropertyChangingEventArgs( propertyName ) );
        }

        /// <summary>
        /// To call the <see cref="E:StockSharp.Diagram.DiagramElement.PropertyChanging" /> event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        protected virtual void RaisePropertyChanging( object sender, PropertyChangingEventArgs args )
        {
            PropertyChangingEventHandler pcing = PropertyChanging;
            if ( pcing == null )
                return;
            pcing( sender, args );
        }

        /// <summary>The diagram element properties value change event.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// To call the <see cref="E:StockSharp.Diagram.DiagramElement.PropertyChanged" /> event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void RaisePropertyChanged( string propertyName )
        {
            RaisePropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
        }

        /// <summary>
        /// To call the <see cref="E:StockSharp.Diagram.DiagramElement.PropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        protected virtual void RaisePropertyChanged( object sender, PropertyChangedEventArgs args )
        {
            PropertyChangedEventHandler pc = PropertyChanged;
            if ( pc == null )
                return;
            pc( sender, args );
        }

        /// <summary>The diagram element parameter value change event.</summary>
        public event Action<string> ParameterValueChanged;

        /// <summary>
        /// To call the <see cref="E:StockSharp.Diagram.DiagramElement.ParameterValueChanged" /> event.
        /// </summary>
        /// <param name="parameterName">Parameter name.</param>
        protected virtual void RaiseParameterValueChanged( string parameterName )
        {
            Action<string> pvc = ParameterValueChanged;
            if ( pvc == null )
                return;
            pvc( parameterName );
        }

        [Serializable]
        private sealed class SomeClass01
        {
            public static readonly SomeClass01 _someInstance01 = new SomeClass01();
            public static Func<DisplayNameAttribute, string> Function001;
            public static Func<CategoryAttribute, string> Function002;
            public static Func<DescriptionAttribute, string> Function003;
            public static Func<DiagramSocket, bool> Function004;
            public static Func<ValueTuple<DiagramSocket, DiagramSocket>, DiagramSocket> Function005;
            public static Func<ValueTuple<DiagramSocket, DiagramSocket>, DiagramElement> Function006;
            public static Func<ValueTuple<DiagramSocket, DiagramSocket>, DiagramSocket> Function007;
            public static Func<ValueTuple<DiagramSocket, DiagramSocket>, DiagramSocket> Function008;
            public static Func<IDiagramElementParam, DiagramElementProperties> Function009;
            public static Func<IDiagramElementParam, bool> Function010;

            internal string Method001( DisplayNameAttribute x )
            {
                return x.DisplayName;
            }

            internal string Method002( CategoryAttribute x )
            {
                return x.Category;
            }

            internal string Method003( DescriptionAttribute x )
            {
                return x.Description;
            }

            internal bool Method004( DiagramSocket x )
            {
                return true;
            }

            internal DiagramSocket Method005(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                return x.Item2;
            }

            internal DiagramElement Method006(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                return x.Item1.Parent;
            }

            internal DiagramSocket Method007(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                return x.Item2;
            }

            internal DiagramSocket Method008(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                return x.Item1;
            }

            internal DiagramElementProperties Method009(
              IDiagramElementParam x )
            {
                return new DiagramElementProperties( x );
            }

            internal bool Method010( IDiagramElementParam x )
            {
                return !x.IgnoreOnSave;
            }
        }

        private sealed class DiagramElementDisposable : Disposable
        {

            private readonly DiagramElement _diagramElement;

            private readonly object _object;

            public DiagramElementDisposable( DiagramElement dElement, object _param2 = null )
            {

                if ( dElement == null )
                    throw new ArgumentNullException( "dElement == null" );

                _diagramElement = dElement;
                _object = _param2;

                if ( dElement.IsUndoRedoing || !dElement.HasUndoManager || Interlocked.Increment( ref dElement._someInteger ) != 1 )
                    return;
                _diagramElement._settingsStorage = _diagramElement.Save();
                Action undo = _diagramElement.StartedUndoableOperation;
                if ( undo == null )
                    return;
                undo();
            }

            public object GetObject()
            {
                return _object;
            }

            protected override void DisposeManaged()
            {
                if ( _diagramElement != null && Interlocked.Decrement( ref _diagramElement._someInteger ) == 0 )
                {
                    SettingsStorage zH7hqRjK9LXv = _diagramElement._settingsStorage;
                    _diagramElement._settingsStorage = null;

                    Action<DiagramElement, IUndoableEdit> cuo = _diagramElement.CommittedUndoableOperation;

                    if ( cuo != null )
                        cuo( _diagramElement, new UndoableEditEx( _diagramElement, zH7hqRjK9LXv, _diagramElement.Save() ) );
                }
                base.DisposeManaged();
            }
        }
        /// <summary>The unique identifier of the diagram element type.</summary>
        public abstract Guid TypeId { get; }

        /// <summary>Incoming connections.</summary>
        [Browsable( false )]
        public IReadOnlyCollection<DiagramSocket> InputSockets
        {
            get
            {
                return _inputSockets;
            }
        }

        /// <summary>Outgoing connections.</summary>
        [Browsable( false )]
        public IReadOnlyCollection<DiagramSocket> OutputSockets
        {
            get
            {
                return _outputSockets;
            }
        }

        private string DiagramElementParamString
        {
            get
            {
                return _diagramElementParamString.Value;
            }

            set
            {
                _diagramElementParamString.Value = value;
            }

        }

        private readonly struct UndoableEditEx : IUndoableEdit
        {

            private readonly IPersistable _persistable;

            private readonly SettingsStorage _setting01;

            private readonly SettingsStorage _setting02;

            public UndoableEditEx(
              IPersistable x,
              SettingsStorage _param2,
              SettingsStorage _param3 )
            {
                _persistable = x;
                _setting01 = _param2;
                _setting02 = _param3;
            }

            private IPersistable GetPersistable()
            {
                return _persistable;
            }

            private SettingsStorage GetSetting01()
            {
                return _setting01;
            }

            private SettingsStorage GetSetting02()
            {
                return _setting02;
            }

            void IUndoableEdit.Clear()
            {
            }

            bool IUndoableEdit.CanUndo()
            {
                return GetSetting01() != null;
            }

            bool IUndoableEdit.CanRedo()
            {
                return GetSetting02() != null;
            }

            void IUndoableEdit.Undo()
            {
                GetPersistable().Load( GetSetting01() );
            }

            void IUndoableEdit.Redo()
            {
                GetPersistable().Load( GetSetting02() );
            }
        }



        /// <inheritdoc />
        public override string Name
        {
            get
            {
                return _name.Value;
            }
            set
            {
                _name.Value = value;
            }
        }

        /// <summary>The diagram element description.</summary>
        public virtual string Description
        {
            get
            {
                return _description;
            }
        }

        /// <inheritdoc />
        public override LogLevels LogLevel
        {
            get
            {
                return _logLevel.Value;
            }
            set
            {
                _logLevel.Value = value;
            }
        }

        /// <summary>Show element parameters in higher order elements.</summary>
        public bool ShowParameters
        {
            get
            {
                return _showParameters.Value;
            }
            set
            {
                _showParameters.Value = value;
            }
        }

        /// <summary>Show element sockets in higher order elements.</summary>
        public bool ShowSockets
        {
            get
            {
                return _showSockets.Value;
            }
            set
            {
                _showSockets.Value = value;
            }
        }

        /// <summary>Process null values.</summary>
        public bool ProcessNullValues
        {
            get
            {
                return _processNullValues.Value;
            }
            set
            {
                _processNullValues.Value = value;
            }
        }




        /// <summary>Diagram element settings.</summary>
        [Browsable( false )]
        public virtual IEnumerable<IDiagramElementParam> Parameters
        {
            get
            {
                return _parameters;
            }
        }

        /// <summary>
        /// The name of the group which includes a diagram element.
        /// </summary>
        public virtual string Category
        {
            get
            {
                return _category;
            }
        }

        /// <summary>Use auto naming.</summary>
        public bool CanAutoName
        {
            get
            {
                return _canAutoName;
            }
            set
            {
                _canAutoName = value;
            }
        }
        /// <summary>Parent composition this element belongs to.</summary>
        public CompositionDiagramElement ParentComposition
        {
            get
            {
                return _parentComposition;
            }

            set
            {
                _parentComposition = value;
            }
        }


        /// <summary>Whether undo/redo operation is in progress.</summary>
        public virtual bool IsUndoRedoing
        {
            get
            {
                CompositionDiagramElement parentComposition = ParentComposition;
                if ( parentComposition == null )
                    return false;
                return parentComposition.IsUndoRedoing;
            }
        }

        /// <summary>Check if undo manager is defined</summary>
        public virtual bool HasUndoManager
        {
            get
            {
                CompositionDiagramElement parentComposition = ParentComposition;
                if ( parentComposition == null )
                    return false;
                return parentComposition.HasUndoManager;
            }
        }

        /// <summary>Help url.</summary>
        public virtual string DocUrl
        {
            get
            {
                return GetType().GetDocUrl();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>Committed undoable operation.</summary>
        public event Action<DiagramElement, IUndoableEdit> CommittedUndoableOperation;

        /// <summary>Started undoable operation.</summary>
        public event Action StartedUndoableOperation;

        private sealed class LamdaShit
        {
            public string _myString;
            public DiagramElement _diagramElement;

            internal void Method00023( string x )
            {
                _diagramElement._diagramElementParamString.IgnoreOnSave = true;
                if ( _diagramElement.CanAutoName )
                {
                    // Tony
                    if ( x == "-1260199428" )
                    {
                        _diagramElement.DiagramElementParamString = ( _myString );
                        _diagramElement.CanAutoName = true;
                    }
                    if ( _diagramElement.Name == _diagramElement.DiagramElementParamString )
                        return;
                    _diagramElement.Name = _diagramElement.DiagramElementParamString;
                    _diagramElement.CanAutoName = false;
                }
                // Tony
                else if ( x == "-1260199428" )
                {
                    _diagramElement.Name = string.Empty;
                    _diagramElement.DiagramElementParamString = ( string.Empty );
                }
                else
                    _diagramElement.Name = _diagramElement.DiagramElementParamString;
            }
        }

        private readonly ObservableCollection<DiagramSocket> _inputSockets = new ObservableCollection<DiagramSocket>();

        private readonly ObservableCollection<DiagramSocket> _outputSockets = new ObservableCollection<DiagramSocket>();

        private readonly List<ValueTuple<DiagramSocket, DiagramSocket>> _connections = new List<ValueTuple<DiagramSocket, DiagramSocket>>();

        private readonly Dictionary<DiagramSocket, DiagramSocketValue> _connectionsValue = new Dictionary<DiagramSocket, DiagramSocketValue>();

        private readonly HashSet<IDiagramElementParam> _parameters = new HashSet<IDiagramElementParam>();

        private readonly HashSet<DiagramElement> _diagramElementHashSet = new HashSet<DiagramElement>();

        private bool _canAutoName = true;

        private readonly List<DiagramElementDisposable> _diagramElementDisposable = new List<DiagramElementDisposable>();

        private readonly DiagramElementParam<bool> _showParameters;

        private readonly DiagramElementParam<bool> _showSockets;

        private readonly DiagramElementParam<bool> _processNullValues;

        private int _count;

        private bool _noChange;

        private CompositionDiagramElement _parentComposition;

        private readonly string _category;

        private readonly DiagramElementParam<string> _diagramElementParamString;

        private readonly DiagramElementParam<string> _name;

        private readonly string _description;

        private readonly DiagramElementParam<LogLevels> _logLevel;

        private DebuggerSyncObject _debuggerSyncObject;

        private int _processingLevel;

        private bool _isInitialized;

        private int _someInteger;

        private SettingsStorage _settingsStorage;

        private string CheckAttributes<T>( Func<T, string> x, string _param2 ) where T : Attribute
        {
            Type type = GetType();
            T attribute = type.GetAttribute<T>( true );
            if ( attribute == null )
                return _param2 ?? type.Name;
            return x( attribute );
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.DiagramElement" />.
        /// </summary>
        protected DiagramElement()
        {
            _category = CheckAttributes<CategoryAttribute>( x => x.Category, LocalizedStrings.Common );
            _description = CheckAttributes<DescriptionAttribute>( x => x.Description, CheckAttributes<DisplayNameAttribute>( x => x.DisplayName, null ) );
            _name = AddParam( nameof( -1260199487 ), CheckAttributes<DisplayNameAttribute>( x => x.DisplayName, null ) )
                                        .SetBrowsable( false )
                                        .SetOnValueChangedHandler( new Action<string>( SetElementName ) );

            _diagramElementParamString = AddParam( nameof( -1260199468 ), CheckAttributes<DisplayNameAttribute>( x => x.DisplayName, null ) )
                                        .SetDisplay( LocalizedStrings.Common, LocalizedStrings.Name, LocalizedStrings.Str3052, 0 )
                                        .SetEditor( new EditorAttribute( typeof( INameEditor ), typeof( INameEditor ) ) )
                                        .SetOnValueChangedHandler( x =>
                                        {
                                            _diagramElementParamString.IgnoreOnSave = true;
                                            if ( CanAutoName )
                                            {
                                                // Tony
                                                if ( x == "-1260199428" )
                                                {
                                                    DiagramElementParamString = CheckAttributes<DisplayNameAttribute>( x => x.DisplayName, null );
                                                    CanAutoName = true;
                                                }
                                                if ( Name == DiagramElementParamString )
                                                    return;
                                                Name = DiagramElementParamString;
                                                CanAutoName = false;
                                            }
                                            // Tony
                                            else if ( x == "-1260199428" )
                                            {
                                                Name = string.Empty;
                                                DiagramElementParamString = ( string.Empty );
                                            }
                                            else
                                                Name = DiagramElementParamString;
                                        } );

            _logLevel = AddParam( nameof( -1260199518 ), LogLevels.Inherit )
                                        .SetDisplay( LocalizedStrings.Common, LocalizedStrings.Str9, LocalizedStrings.Str3053, 1 );

            _showParameters = AddParam( nameof( -1260199499 ), false )
                                        .SetDisplay( LocalizedStrings.Common, LocalizedStrings.Str225, LocalizedStrings.Str3054, 2 )
                                        .SetOnValueChangedHandler( x => RaisePropertiesChanged() );

            _showSockets = AddParam( nameof( -1260199522 ), false ).SetDisplay( LocalizedStrings.Common, LocalizedStrings.Sockets, LocalizedStrings.ShowSockets, 3 );
            _processNullValues = AddParam( nameof( -1260199828 ), false ).SetDisplay( LocalizedStrings.Common, LocalizedStrings.ProcessNullValues, LocalizedStrings.ProcessNullValues, 10 );
            PropertyChanging += new PropertyChangingEventHandler( OnPropertyChanging );
            PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
        }

        private void OnPropertyChanging( object x, PropertyChangingEventArgs _param2 )
        {
            _diagramElementDisposable.Add( ( DiagramElementDisposable )SaveUndoState( _param2.PropertyName ) );
        }

        private void OnPropertyChanged( object x, PropertyChangedEventArgs _param2 )
        {
            int index = _diagramElementDisposable.IndexOf( y => object.Equals( y.GetObject(), _param2.PropertyName ) );
            if ( index < 0 )
                return;

            var tobeDispose = _diagramElementDisposable[index];
            _diagramElementDisposable.RemoveAt( index );
            tobeDispose.Dispose();
        }




        /// <summary>Save state to enable undo.</summary>
        protected IDisposable SaveUndoState( object debugState = null )
        {
            return new DiagramElementDisposable( this, debugState );
        }

        /// <summary>Generate socket identifier.</summary>
        /// <param name="suffix">Suffix.</param>
        /// <returns>Identifier.</returns>
        public static string GenerateSocketId( string suffix )
        {
            return string.Concat( nameof( -1260199820 ), suffix );
        }





        /// <summary>Icon resource name.</summary>
        [Browsable( false )]
        public abstract string IconName { get; }

        /// <summary>The strategy to which the element is attached.</summary>
        public virtual DiagramStrategy Strategy
        {
            get
            {
                return ParentComposition?.Strategy;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>Connector.</summary>
        protected IConnector Connector
        {
            get
            {
                return Strategy.SafeGetConnector();
            }
        }

        /// <summary>The synchronization object for the debugger.</summary>
        public DebuggerSyncObject DebuggerSyncObject
        {
            get
            {
                return _debuggerSyncObject;
            }
            set
            {
                _debuggerSyncObject = value;
                RaisePropertyChanged( nameof( DebuggerSyncObject ) );
            }
        }

        /// <summary>New data occurring event.</summary>
        public event Action<DiagramSocketValue> ProcessOutput;

        /// <summary>The diagram element connection added event.</summary>
        public event Action<DiagramSocket> SocketAdded;

        /// <summary>The diagram element connection removed event.</summary>
        public event Action<DiagramSocket> SocketRemoved;

        /// <summary>The diagram element connection changed event.</summary>
        public event Action<DiagramSocket> SocketChanged;





        /// <summary>Set element name.</summary>
        /// <param name="name">Name.</param>
        protected void SetElementName( string name )
        {
            if ( !CanAutoName )
                return;
            DiagramElementParamString = ( name ?? GetType().GetDisplayName( null ) );
            CanAutoName = true;
        }

        /// <summary>To add a parameter.</summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        /// <returns>Parameter.</returns>
        protected DiagramElementParam<T> AddParam<T>( string name, T value = default )
        {
            DiagramElementParam<T> diagramElementParam = new DiagramElementParam<T>() { Name = name, Value = value };
            AddParam( diagramElementParam );
            return diagramElementParam;
        }

        /// <summary>To add a parameter.</summary>
        /// <param name="param">Parameter.</param>
        protected void AddParam( IDiagramElementParam param )
        {
            param.PropertyChanging += new PropertyChangingEventHandler( OnParamPropertyChanging );
            param.PropertyChanged += new PropertyChangedEventHandler( OnParamPropertyChanged );
            _parameters.Add( param );
        }

        /// <summary>To remove a parameter.</summary>
        /// <param name="param">Parameter.</param>
        protected void RemoveParam( IDiagramElementParam param )
        {
            param.PropertyChanging -= new PropertyChangingEventHandler( OnParamPropertyChanging );
            param.PropertyChanged -= new PropertyChangedEventHandler( OnParamPropertyChanged );
            _parameters.Remove( param );
        }

        private void OnParamPropertyChanging( object x, PropertyChangingEventArgs _param2 )
        {
            if ( _param2.PropertyName != nameof( -1260198550 ) )
                return;
            RaisePropertyChanging( ( ( IDiagramElementParam )x ).Name );
        }

        private void OnParamPropertyChanged( object x, PropertyChangedEventArgs _param2 )
        {
            if ( _param2.PropertyName != nameof( -1260198550 ) )
                return;
            IDiagramElementParam diagramElementParam = ( IDiagramElementParam )x;
            RaisePropertyChanged( diagramElementParam.Name );
            RaisePropertyChanged( string.Concat( diagramElementParam.Name, diagramElementParam.GetHashCode().ToString() ) );
            RaiseParameterValueChanged( diagramElementParam.Name );
        }


        /// <summary>Create new socket instance.</summary>
        protected virtual DiagramSocket CreateSocketInstance( DiagramSocketDirection dir, string socketId = null )
        {
            return new DiagramSocket( dir, socketId );
        }

        private ValueTuple<DiagramSocket, bool> GetDiagramSocketValueTuple( string x, DiagramSocketDirection direction, string name, DiagramSocketType type, Action<DiagramSocketValue> action,
          int max,
          int _param7,
          bool addType,
          bool isDynamic,
          bool _param10 )
        {
            if ( x.IsEmptyOrWhiteSpace() )
                throw new ArgumentNullException( "x is emtpy" );

            DiagramSocket socket = _inputSockets.FindById( x ) ?? _outputSockets.FindById( x );
            bool justCreated = false;

            if ( socket == null )
            {
                socket = CreateSocketInstance( direction, x );
                justCreated = true;
            }
            else if ( !_param10 )
                throw new InvalidOperationException( string.Concat( nameof( -1260199901 ), x, nameof( -1260199926 ) ) );
            socket.Name = name;
            socket.LinkableMaximum = max;
            socket.Type = type;
            socket.Action = action;
            socket.IsDynamic = isDynamic;
            if ( addType )
                socket.AddDiagramSocketTypes();

            if ( justCreated )
                InitSocket( socket, _param7 );

            return new ValueTuple<DiagramSocket, bool>( socket, justCreated );
        }

        private void InitSocket( DiagramSocket x, int _param2 )
        {
            if ( x == null )
                throw new ArgumentNullException( "x == null" );

            var sockets = x.Directon == DiagramSocketDirection.In ? _inputSockets : _outputSockets;

            if ( sockets.FindById( x.Id ) != null )
                throw new InvalidOperationException( string.Concat( nameof( -1260199901 ), x.Id, nameof( -1260199926 ) ) );

            x.Parent = this;
            x.Connected += OnSocketConnected;
            x.Disconnected += OnSocketDisconnected;

            using ( SaveUndoState( null ) )
            {
                sockets.Insert( _param2.Min( sockets.Count ), x );
                if ( _noChange )
                    return;
                Action<DiagramSocket> added = SocketAdded;
                if ( added == null )
                    return;
                added( x );
            }
        }

        /// <summary>
        /// The method is called at subscription to the processing of diagram element output values.
        /// </summary>
        /// <param name="socket">The diagram element socket.</param>
        /// <param name="source">The source diagram element socket.</param>
        protected virtual void OnSocketConnected( DiagramSocket socket, DiagramSocket source )
        {
            SomeHolder2356 l37McsbMr0cNgHzwQ = new SomeHolder2356();
            if ( socket == null )
                throw new ArgumentNullException( "socket == null" );
            if ( source == null )
                throw new ArgumentNullException( "socket == null" );
            if ( socket.IsOutput )
                return;
            l37McsbMr0cNgHzwQ._valueTuple = new ValueTuple<DiagramSocket, DiagramSocket>( source, socket );
            if ( !_connections.FirstOrDefault( new Func<ValueTuple<DiagramSocket, DiagramSocket>, bool>( l37McsbMr0cNgHzwQ.Method0353 ) ).IsDefault() )
                throw new InvalidOperationException( LocalizedStrings.Str3056Params.Put( new object[3] { source, this, socket } ) );
            _connections.Add( l37McsbMr0cNgHzwQ._valueTuple );
            _count = _connections.Select( DiagramElement.SomeClass01.Function005 ?? ( DiagramElement.SomeClass01.Function005 = new Func<ValueTuple<DiagramSocket, DiagramSocket>, DiagramSocket>( DiagramElement.SomeClass01._someInstance01.Method005 ) ) ).Distinct().Count();
            SomethingConnections();
        }

        private void SomethingConnections()
        {
            var hasParent = _connections.Select( x => x.Item1.Parent ).Distinct().ToSet();
            HashSet<DiagramElement> orphans = new HashSet<DiagramElement>( _diagramElementHashSet );
            orphans.ExceptWith( hasParent );

            HashSet<DiagramElement> opposites = new HashSet<DiagramElement>( hasParent );
            opposites.ExceptWith( _diagramElementHashSet );
            orphans.ForEach( x => x.ProcessOutput -= Process );
            opposites.ForEach( x => x.ProcessOutput += Process );
            _diagramElementHashSet.Clear();
            _diagramElementHashSet.AddRange( hasParent );
        }

        /// <summary>
        /// Element processing level. How many times Process() is reentered.
        /// </summary>
        protected int ProcessingLevel
        {
            get
            {
                return _processingLevel;
            }

            set
            {
                _processingLevel = value;
            }
        }




        /// <summary>To handle the incoming value.</summary>
        /// <param name="value">Value.</param>
        public void Process( DiagramSocketValue value )
        {

            if ( value == null )
                throw new ArgumentNullException( "value == null " );
            try
            {
                ProcessingLevel = ( ProcessingLevel + 1 );
                foreach ( DiagramSocket socket in _connections.Where( new Func<ValueTuple<DiagramSocket, DiagramSocket>, bool>( qm4Wh1xa0P9uEf7yes.Method0325 ) ).Select( DiagramElement.SomeClass01.Function007 ?? ( DiagramElement.SomeClass01.Function007 = new Func<ValueTuple<DiagramSocket, DiagramSocket>, DiagramSocket>( DiagramElement.SomeClass01._someInstance01.Method007 ) ) ).ToArray() )
                {
                    this.AddDebugLog( nameof( -1260199049 ), new object[3]
                    {
                        Name,
                        value.Socket,
                        value.Value
                    } );

                    socket.Value = value.Value;
                    Strategy.AddRemoveSocket( socket, true );
                    DebuggerSyncObject?.TryWait( socket, true );
                    if ( value.Value == null && !_processNullValues.Value )
                        return;
                    DiagramSocketValue diagramSocketValue = new DiagramSocketValue( socket, value.Time, value.Value, value.Socket, value.Subscription );
                    if ( socket.Action != null )
                    {
                        if ( !PushToStack( diagramSocketValue, value ) )
                            return;
                        socket.Action( diagramSocketValue );
                    }
                    else
                    {
                        _connectionsValue[socket] = diagramSocketValue;
                        Strategy.AddRemoveSocket( this, true );
                    }
                }
                if ( _connectionsValue.Count != _count )
                    return;
                OnProcess( value.Time, _connectionsValue, value );
                _connectionsValue.Clear();
                Strategy.AddRemoveSocket( this, false );
            }
            catch ( Exception ex )
            {
                DebuggerSyncObject?.TryWaitOnError( this, ex );
                throw;
            }
            finally
            {
                ProcessingLevel = ( ProcessingLevel - 1 );
            }
        }

        /// <summary>
        /// The method is called at the processing of the new incoming values.
        /// </summary>
        /// <param name="time">Time.</param>
        /// <param name="values">Values.</param>
        /// <param name="source">Source value.</param>
        protected virtual void OnProcess( DateTimeOffset time, IDictionary<DiagramSocket, DiagramSocketValue> values, DiagramSocketValue source )
        {
            throw new NotSupportedException();
        }

        private bool PushToStack( DiagramSocketValue x, DiagramSocketValue _param2 )
        {
            if ( x == null )
                throw new ArgumentNullException( nameof( -1260199126 ) );
            if ( _param2 != null )
            {
                x.Stack = _param2.Stack;
                x.Stack.Push( _param2 );
                if ( x.Stack.Count > Strategy.OverflowLimit )
                {
                    Strategy.AddErrorLog( string.Concat( nameof( -1260199135 ), LocalizedStrings.Overflow, nameof( -1260199135 ) ) );
                    Strategy.Stop();
                    return false;
                }
            }
            else
                x.Stack = new Stack<DiagramSocketValue>( 5 );
            return true;
        }


        /// <summary>
        /// The method is called at unsubscription from the processing of diagram element output values.
        /// </summary>
        /// <param name="socket">The diagram element socket.</param>
        /// <param name="source">The source diagram element socket.</param>
        protected virtual void OnSocketDisconnected( DiagramSocket socket, DiagramSocket source )
        {
            SomeHolder1234 y9rRpgt4RwvTn7Jayc = new SomeHolder1234();
            y9rRpgt4RwvTn7Jayc._socket01 = socket;
            y9rRpgt4RwvTn7Jayc._socket03 = source;
            if ( socket == null )
                throw new ArgumentNullException( "socket == null" );
            if ( y9rRpgt4RwvTn7Jayc._socket01.IsOutput )
                return;
            ValueTuple<DiagramSocket, DiagramSocket>[ ] array = _connections.Where( new Func<ValueTuple<DiagramSocket, DiagramSocket>, bool>( y9rRpgt4RwvTn7Jayc.Method035 ) ).ToArray();
            if ( array.Length == 0 )
                return;
            ValueTuple<DiagramSocket, DiagramSocket> valueTuple = new ValueTuple<DiagramSocket, DiagramSocket>();
            if ( array.Length == 1 )
                valueTuple = array[0];
            else if ( y9rRpgt4RwvTn7Jayc._socket03 != null )
                valueTuple = ( ( IEnumerable<ValueTuple<DiagramSocket, DiagramSocket>> )array ).SingleOrDefault( new Func<ValueTuple<DiagramSocket, DiagramSocket>, bool>( y9rRpgt4RwvTn7Jayc.Method036 ) );
            if ( valueTuple.IsDefault() )
                return;
            _connections.Remove( valueTuple );
            SomethingConnections();
        }

        private sealed class SomeHolder2356
        {
            public ValueTuple<DiagramSocket, DiagramSocket> _valueTuple;

            internal bool Method0353(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                if ( x.Item1 == _valueTuple.Item1 )
                    return x.Item2 == _valueTuple.Item2;
                return false;
            }
        }

        private sealed class SomeHolder1234
        {
            public DiagramSocket _socket01;
            public DiagramSocket _socket03;

            internal bool Method035(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                return x.Item2 == _socket01;
            }

            internal bool Method036(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                return x.Item1 == _socket03;
            }
        }


        /// <summary>
        /// To add or get existing incoming connection. isDynamic is true by default.
        /// </summary>
        /// <param name="id">The connection identifier.</param>
        /// <param name="name">The connection name.</param>
        /// <param name="type">Connection type.</param>
        /// <param name="linkableMax">The maximum number of connections.</param>
        /// <param name="isNew">True if this is a new socket.</param>
        /// <param name="process">The action is called at the processing of the new incoming value for socket.</param>
        /// <param name="index">Index in sockets list.</param>
        /// <param name="allowConvertToNumeric">Allow converting to numeric for input values.</param>
        /// <param name="isDynamic">Socket will be saved with the element. Default is true for sockets with explicit id.</param>
        /// <returns>Connection.</returns>
        protected DiagramSocket GetOrAddInput(
                          string id,
                          string name,
                          DiagramSocketType type,
                          out bool isNew,
                          Action<DiagramSocketValue> process = null,
                          int linkableMax = 1,
                          int index = 2147483647,
                          bool allowConvertToNumeric = false,
                          bool? isDynamic = null )
        {
            string str1 = id;
            string str2 = name;
            DiagramSocketType diagramSocketType = type;
            Action<DiagramSocketValue> action = process;
            int num1 = linkableMax;
            int num2 = index;
            int num3 = allowConvertToNumeric ? 1 : 0;
            bool? nullable = isDynamic;
            int num4 = nullable.HasValue ? ( nullable.GetValueOrDefault() ? 1 : 0 ) : 1;
            ValueTuple<DiagramSocket, bool> valueTuple = GetDiagramSocketValueTuple( str1, DiagramSocketDirection.In, str2, diagramSocketType, action, num1, num2, num3 != 0, num4 != 0, true );
            DiagramSocket diagramSocket = valueTuple.Item1;
            bool flag = valueTuple.Item2;
            isNew = flag;
            return diagramSocket;
        }

        /// <summary>
        /// To add or get existing incoming connection. isDynamic is false by default.
        /// </summary>
        /// <param name="id">The connection identifier.</param>
        /// <param name="name">The connection name.</param>
        /// <param name="type">Connection type.</param>
        /// <param name="linkableMax">The maximum number of connections.</param>
        /// <param name="isNew">True if this is a new socket.</param>
        /// <param name="process">The action is called at the processing of the new incoming value for socket.</param>
        /// <param name="index">Index in sockets list.</param>
        /// <param name="allowConvertToNumeric">Allow converting to numeric for input values.</param>
        /// <param name="isDynamic">Socket will be saved with the element. Default is true for sockets with explicit id.</param>
        /// <returns>Connection.</returns>
        protected DiagramSocket GetOrAddInput(
          StaticSocketIds id,
          string name,
          DiagramSocketType type,
          out bool isNew,
          Action<DiagramSocketValue> process = null,
          int linkableMax = 1,
          int index = 2147483647,
          bool allowConvertToNumeric = false,
          bool? isDynamic = null )
        {
            ValueTuple<DiagramSocket, bool> valueTuple = GetDiagramSocketValueTuple( id.ToString(), DiagramSocketDirection.In, name, type, process, linkableMax, index, allowConvertToNumeric, isDynamic.GetValueOrDefault(), true );
            DiagramSocket diagramSocket = valueTuple.Item1;
            bool flag = valueTuple.Item2;
            isNew = flag;
            return diagramSocket;
        }

        /// <summary>
        /// To add or get an outgoing connection. <paramref name="isDynamic" /> is true by default.
        /// </summary>
        /// <param name="id">The connection identifier.</param>
        /// <param name="name">The connection name.</param>
        /// <param name="type">Connection type.</param>
        /// <param name="isNew">True if this is a new socket.</param>
        /// <param name="linkableMax">The maximum number of connections.</param>
        /// <param name="index">Index in sockets list.</param>
        /// <param name="isDynamic">Dynamic sockets are removed during <see cref="M:StockSharp.Diagram.DiagramElement.Load(Ecng.Serialization.SettingsStorage)" />.</param>
        /// <returns>Connection.</returns>
        protected DiagramSocket GetOrAddOutput(
          StaticSocketIds id,
          string name,
          DiagramSocketType type,
          out bool isNew,
          int linkableMax = 2147483647,
          int index = 2147483647,
          bool? isDynamic = null )
        {
            ValueTuple<DiagramSocket, bool> valueTuple = GetDiagramSocketValueTuple( id.ToString(), DiagramSocketDirection.Out, name, type, null, linkableMax, index, false, isDynamic.GetValueOrDefault(), true );
            DiagramSocket diagramSocket = valueTuple.Item1;
            bool flag = valueTuple.Item2;
            isNew = flag;
            return diagramSocket;
        }

        /// <summary>To add or get an outgoing connection.</summary>
        /// <param name="id">The connection identifier.</param>
        /// <param name="name">The connection name.</param>
        /// <param name="type">Connection type.</param>
        /// <param name="isNew">True if this is a new socket.</param>
        /// <param name="linkableMax">The maximum number of connections.</param>
        /// <param name="index">Index in sockets list.</param>
        /// <param name="isDynamic">Dynamic sockets are removed during <see cref="M:StockSharp.Diagram.DiagramElement.Load(Ecng.Serialization.SettingsStorage)" />.</param>
        /// <returns>Connection.</returns>
        protected DiagramSocket GetOrAddOutput(
          string id,
          string name,
          DiagramSocketType type,
          out bool isNew,
          int linkableMax = 2147483647,
          int index = 2147483647,
          bool? isDynamic = null )
        {
            string str1 = id;
            string str2 = name;
            DiagramSocketType diagramSocketType = type;
            int num1 = linkableMax;
            int num2 = index;
            bool? nullable = isDynamic;
            int num3 = nullable.HasValue ? ( nullable.GetValueOrDefault() ? 1 : 0 ) : 1;
            ValueTuple<DiagramSocket, bool> valueTuple = GetDiagramSocketValueTuple( str1, DiagramSocketDirection.Out, str2, diagramSocketType, null, num1, num2, false, num3 != 0, true );
            DiagramSocket diagramSocket = valueTuple.Item1;
            bool flag = valueTuple.Item2;
            isNew = flag;
            return diagramSocket;
        }

        /// <summary>
        /// To add or get existing incoming connection. isDynamic is false by default.
        /// </summary>
        /// <param name="id">The connection identifier.</param>
        /// <param name="name">The connection name.</param>
        /// <param name="type">Connection type.</param>
        /// <param name="linkableMax">The maximum number of connections.</param>
        /// <param name="process">The action is called at the processing of the new incoming value for socket.</param>
        /// <param name="index">Index in sockets list.</param>
        /// <param name="allowConvertToNumeric">Allow converting to numeric for input values.</param>
        /// <param name="isDynamic">Socket will be saved with the element. Default is true for sockets with explicit id.</param>
        /// <returns>Connection.</returns>
        protected DiagramSocket AddInput(
          StaticSocketIds id,
          string name,
          DiagramSocketType type,
          Action<DiagramSocketValue> process = null,
          int linkableMax = 1,
          int index = 2147483647,
          bool allowConvertToNumeric = false,
          bool? isDynamic = null )
        {
            return GetDiagramSocketValueTuple( id.ToString(), DiagramSocketDirection.In, name, type, process, linkableMax, index, allowConvertToNumeric, isDynamic.GetValueOrDefault(), false ).Item1;
        }

        /// <summary>
        /// To add or get existing incoming connection. isDynamic is true by default.
        /// </summary>
        /// <param name="id">The connection identifier.</param>
        /// <param name="name">The connection name.</param>
        /// <param name="type">Connection type.</param>
        /// <param name="linkableMax">The maximum number of connections.</param>
        /// <param name="process">The action is called at the processing of the new incoming value for socket.</param>
        /// <param name="index">Index in sockets list.</param>
        /// <param name="allowConvertToNumeric">Allow converting to numeric for input values.</param>
        /// <param name="isDynamic">Socket will be saved with the element. Default is true for sockets with explicit id.</param>
        /// <returns>Connection.</returns>
        protected DiagramSocket AddInput(
          string id,
          string name,
          DiagramSocketType type,
          Action<DiagramSocketValue> process = null,
          int linkableMax = 1,
          int index = 2147483647,
          bool allowConvertToNumeric = false,
          bool? isDynamic = null )
        {
            string str1 = id;
            string str2 = name;
            DiagramSocketType diagramSocketType = type;
            Action<DiagramSocketValue> action = process;
            int num1 = linkableMax;
            int num2 = index;
            int num3 = allowConvertToNumeric ? 1 : 0;
            bool? nullable = isDynamic;
            int num4 = nullable.HasValue ? ( nullable.GetValueOrDefault() ? 1 : 0 ) : 1;
            return GetDiagramSocketValueTuple( str1, DiagramSocketDirection.In, str2, diagramSocketType, action, num1, num2, num3 != 0, num4 != 0, false ).Item1;
        }

        /// <summary>To add or get an outgoing connection.</summary>
        /// <param name="id">The connection identifier.</param>
        /// <param name="name">The connection name.</param>
        /// <param name="type">Connection type.</param>
        /// <param name="linkableMax">The maximum number of connections.</param>
        /// <param name="index">Index in sockets list.</param>
        /// <param name="isDynamic">Dynamic sockets are removed during Load().</param>
        /// <returns>Connection.</returns>
        protected DiagramSocket AddOutput(
          StaticSocketIds id,
          string name,
          DiagramSocketType type,
          int linkableMax = 2147483647,
          int index = 2147483647,
          bool? isDynamic = null )
        {
            return GetDiagramSocketValueTuple( id.ToString(), DiagramSocketDirection.Out, name, type, null, linkableMax, index, false, isDynamic.GetValueOrDefault(), false ).Item1;
        }

        /// <summary>To add or get an outgoing connection.</summary>
        /// <param name="id">The connection identifier.</param>
        /// <param name="name">The connection name.</param>
        /// <param name="type">Connection type.</param>
        /// <param name="linkableMax">The maximum number of connections.</param>
        /// <param name="index">Index in sockets list.</param>
        /// <param name="isDynamic">Dynamic sockets are removed during Load().</param>
        /// <returns>Connection.</returns>
        protected DiagramSocket AddOutput(
          string id,
          string name,
          DiagramSocketType type,
          int linkableMax = 2147483647,
          int index = 2147483647,
          bool? isDynamic = null )
        {
            string str1 = id;
            string str2 = name;
            DiagramSocketType diagramSocketType = type;
            int num1 = linkableMax;
            int num2 = index;
            bool? nullable = isDynamic;
            int num3 = nullable.HasValue ? ( nullable.GetValueOrDefault() ? 1 : 0 ) : 1;
            return GetDiagramSocketValueTuple( str1, DiagramSocketDirection.Out, str2, diagramSocketType, null, num1, num2, false, num3 != 0, false ).Item1;
        }



        /// <summary>To remove a socket by id.</summary>
        protected void RemoveSocket( string socketId )
        {
            DiagramSocket socket = InputSockets.FindById( socketId ) ?? OutputSockets.FindById( socketId );
            if ( socket == null )
                return;
            RemoveSocket( socket );
        }

        /// <summary>To remove a connection.</summary>
        /// <param name="socket">Connection.</param>
        protected void RemoveSocket( DiagramSocket socket )
        {
            RemoveSocket( socket, true );
        }

        private void RemoveSocket( DiagramSocket x, bool _param2 )
        {
            LamdaShit1234 rfwWzxAjAtIbIjmRoc = new LamdaShit1234();
            rfwWzxAjAtIbIjmRoc._diagramElement = this;
            if ( x == null )
                throw new ArgumentNullException( "x == null" );

            using ( SaveUndoState( null ) )
            {
                if ( !( x.Directon == DiagramSocketDirection.In ? _inputSockets : _outputSockets ).Remove( x ) )
                    return;
                x.Connected -= new Action<DiagramSocket, DiagramSocket>( OnSocketConnected );
                x.Disconnected -= new Action<DiagramSocket, DiagramSocket>( OnSocketDisconnected );
                rfwWzxAjAtIbIjmRoc._socket = x;
                ( ( IEnumerable<ValueTuple<DiagramSocket, DiagramSocket>> )_connections.Where( new Func<ValueTuple<DiagramSocket, DiagramSocket>, bool>( rfwWzxAjAtIbIjmRoc.Method032 ) ).ToArray() ).ForEach( new Action<ValueTuple<DiagramSocket, DiagramSocket>>( rfwWzxAjAtIbIjmRoc.Method02 ) );
                if ( _param2 && !_noChange )
                {
                    Action<DiagramSocket> sr = this.SocketRemoved;
                    if ( sr != null )
                        sr( x );
                }
                x.Dispose();
                x.Parent = null;
            }
        }

        private sealed class LamdaShit1234
        {
            public DiagramSocket _socket;
            public DiagramElement _diagramElement;

            internal bool Method032(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                return x.Item2 == this._socket;
            }

            internal void Method02(
              ValueTuple<DiagramSocket, DiagramSocket> x )
            {
                this._diagramElement.OnSocketDisconnected( this._socket, x.Item1 );
            }
        }

        /// <summary>To remove all incoming and outgoing connections.</summary>
        /// <param name="raiseSocketRemoved">Raise <see cref="E:StockSharp.Diagram.DiagramElement.SocketRemoved" /> event.</param>
        protected void RemoveSockets( bool raiseSocketRemoved = true )
        {
            RemoveSockets( DiagramElement.SomeClass01.Function004 ?? ( DiagramElement.SomeClass01.Function004 = new Func<DiagramSocket, bool>( DiagramElement.SomeClass01._someInstance01.Method004 ) ), raiseSocketRemoved );
        }

        /// <summary>To remove multiple sockets.</summary>
        /// <param name="predicate">
        /// </param>
        /// <param name="raiseSocketRemoved">Raise <see cref="E:StockSharp.Diagram.DiagramElement.SocketRemoved" /> event.</param>
        protected void RemoveSockets( Func<DiagramSocket, bool> predicate, bool raiseSocketRemoved = true )
        {

            DiagramSocket[ ] dynamicSockets = _inputSockets.Concat( _outputSockets ).Where( new Func<DiagramSocket, bool>( x =>
            {
                if ( x.IsDynamic )
                    return predicate( x );
                return false;
            } ) ).ToArray();
            using ( SaveUndoState( null ) )
                ( ( IEnumerable<DiagramSocket> )dynamicSockets ).ForEach( x => RemoveSocket( x, raiseSocketRemoved ) );
        }



        /// <summary>
        /// To call the event <see cref="E:StockSharp.Diagram.DiagramElement.SocketChanged" />.
        /// </summary>
        /// <param name="socket">Socket.</param>
        protected void RaiseSocketChanged( DiagramSocket socket )
        {
            if ( _noChange )
                return;
            Action<DiagramSocket> sc = SocketChanged;
            if ( sc == null )
                return;
            sc( socket );
        }

        private void ProcessOutputMethod( DiagramSocketValue x )
        {
            if ( x == null )
                throw new ArgumentNullException( nameof( -1260199715 ) );

            this.AddDebugLog( nameof( -1260199728 ), new object[2]
            {
                x.Socket,
                x.Value
            } );

            x.Socket.Value = x.Value;
            this.Strategy.AddRemoveSocket( x.Socket, true );
            this.DebuggerSyncObject?.TryWait( x.Socket, false );
            Action<DiagramSocketValue> po = this.ProcessOutput;
            if ( po == null )
                return;
            po( x );
        }

        /// <summary>
        /// To call the event <see cref="E:StockSharp.Diagram.DiagramElement.ProcessOutput" />.
        /// </summary>
        /// <param name="outputSocket">Output socket.</param>
        /// <param name="time">Time.</param>
        /// <param name="value">Value.</param>
        /// <param name="source">Source value.</param>
        /// <param name="subscription">Subscription.</param>
        protected void RaiseProcessOutput( DiagramSocket outputSocket, DateTimeOffset time, object value, DiagramSocketValue source, Subscription subscription = null )
        {
            if ( outputSocket == null )
                throw new ArgumentNullException( "outputSocket == null" );
            DiagramSocketValue diagramSocketValue = new DiagramSocketValue( outputSocket, time, value, null, subscription ?? source?.Subscription );
            if ( !this.PushToStack( diagramSocketValue, source ) )
                return;
            this.ProcessOutputMethod( diagramSocketValue );
        }

        /// <summary>
        /// To call the event <see cref="E:StockSharp.Diagram.DiagramElement.ProcessOutput" />.
        /// </summary>
        /// <param name="outputSocket">Output socket.</param>
        /// <param name="value">Value.</param>
        protected void RaiseProcessOutput( DiagramSocket outputSocket, object value )
        {
            this.RaiseProcessOutput( outputSocket, value, null );
        }

        /// <summary>
        /// To call the event <see cref="E:StockSharp.Diagram.DiagramElement.ProcessOutput" />.
        /// </summary>
        /// <param name="outputSocket">Output socket.</param>
        /// <param name="value">Value.</param>
        /// <param name="subscription">Subscription.</param>
        protected void RaiseProcessOutput(
          DiagramSocket outputSocket,
          object value,
          Subscription subscription )
        {
            this.RaiseProcessOutput( outputSocket, value, null, subscription );
        }

        /// <summary>
        /// To call the event <see cref="E:StockSharp.Diagram.DiagramElement.ProcessOutput" />.
        /// </summary>
        /// <param name="outputSocket">Output socket.</param>
        /// <param name="value">Value.</param>
        /// <param name="source">Source value.</param>
        /// <param name="subscription">Subscription.</param>
        protected void RaiseProcessOutput(
          DiagramSocket outputSocket,
          object value,
          DiagramSocketValue source,
          Subscription subscription = null )
        {
            this.RaiseProcessOutput( outputSocket, this.Strategy.CurrentTime, value, source, subscription );
        }

        /// <summary>
        /// To call the event <see cref="E:StockSharp.Diagram.DiagramElement.ProcessOutput" />.
        /// </summary>
        /// <param name="time">Time.</param>
        /// <param name="value">Value.</param>
        /// <param name="source">Source value.</param>
        /// <param name="subscription">Subscription.</param>
        protected void RaiseProcessOutput(
          DateTimeOffset time,
          object value,
          DiagramSocketValue source,
          Subscription subscription = null )
        {
            this.RaiseProcessOutput( this.OutputSockets.First(), time, value, source, subscription );
        }

        /// <summary>
        /// To call the event <see cref="E:StockSharp.Diagram.DiagramElement.ProcessOutput" />.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="source">Source value.</param>
        /// <param name="subscription">Subscription.</param>
        protected void RaiseProcessOutput(
          object value,
          DiagramSocketValue source,
          Subscription subscription = null )
        {
            this.RaiseProcessOutput( this.Strategy.CurrentTime, value, source, subscription );
        }

        /// <summary>Is need flush state (-1 means No).</summary>
        public virtual int NeedFlush
        {
            get
            {
                return -1;
            }
        }


        /// <summary>Flush non trigger (root) elements.</summary>
        public virtual void Flush()
        {
        }

        private void OnStrategyChanged()
        {
            this.RaisePropertyChanged( nameof( -1260198404 ) );
        }

        /// <summary>To initialize the element.</summary>
        /// <param name="parent">Parent composition or strategy.</param>
        public void Init( ILogSource parent )
        {
            if ( parent == null )
                throw new ArgumentNullException( nameof( -1260199801 ) );
            if ( this.ParentComposition == parent )
                return;
            if ( this._isInitialized )
                throw new InvalidOperationException( nameof( -1260199782 ) );
            this._isInitialized = true;
            this.ParentComposition = ( parent as CompositionDiagramElement );
            this.Parent = parent;
            this.DebuggerSyncObject = this.ParentComposition?.DebuggerSyncObject;
            if ( this.ParentComposition != null )
                this.ParentComposition.StrategyChanged += new Action( this.OnStrategyChanged );
            this.OnInit();
        }

        /// <summary>The deinitialization of the element.</summary>
        public void UnInit()
        {
            if ( !this._isInitialized )
                return;
            this._isInitialized = false;
            this.OnUnInit();
            if ( this.ParentComposition != null )
                this.ParentComposition.StrategyChanged -= new Action( this.OnStrategyChanged );
            this.Parent = null;
            this.ParentComposition = null;
            this.DebuggerSyncObject = null;
        }

        /// <summary>
        /// The method is called at initialization of the diagram element.
        /// </summary>
        protected virtual void OnInit()
        {
        }

        /// <summary>
        /// The method is called at deinitialization of the diagram element.
        /// </summary>
        protected virtual void OnUnInit()
        {
        }


        /// <summary>Get connection count.</summary>
        /// <param name="socket">Socket.</param>
        /// <returns>Count.</returns>
        public int GetNumConnections( DiagramSocket socket )
        {
            return this._connections.Count( x => x.Item2 == socket );
        }

        /// <summary>Get connected source sockets.</summary>
        /// <param name="targetInputSocket">
        /// </param>
        protected DiagramSocket[ ] GetConnectedSourceSockets( DiagramSocket targetInputSocket )
        {
            return this._connections.Where( new Func<ValueTuple<DiagramSocket, DiagramSocket>, bool>( x => x.Item2 == targetInputSocket ) ).Select( x => x.Item1 ).ToArray();
        }



        /// <summary>To start the diagram element algorithm.</summary>
        public void Start()
        {
            try
            {
                this.OnStart();
            }
            catch ( Exception ex )
            {
                this.DebuggerSyncObject?.TryWaitOnError( this, ex );
                throw;
            }
        }

        /// <summary>
        /// The method is called at the start of the diagram element algorithm.
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>To stop the diagram element algorithm.</summary>
        public void Stop()
        {
            try
            {
                this.OnStop();
            }
            catch ( Exception ex )
            {
                this.DebuggerSyncObject?.TryWaitOnError( this, ex );
                throw;
            }
        }

        /// <summary>
        /// The method is called at the stop of the diagram element algorithm.
        /// </summary>
        protected virtual void OnStop()
        {
        }

        /// <summary>To reinitialize the diagram element state.</summary>
        public void Reset()
        {
            try
            {
                this.OnReseted();
            }
            catch ( Exception ex )
            {
                this.DebuggerSyncObject?.TryWaitOnError( this, ex );
                throw;
            }
        }

        /// <summary>
        /// The method is called at re-initialisation of the diagram element state.
        /// </summary>
        protected virtual void OnReseted()
        {
        }

        /// <summary>Clear socket values.</summary>
        public virtual void ClearSocketValues()
        {
            if ( this._connectionsValue.Count <= 0 )
                return;
            foreach ( KeyValuePair<DiagramSocket, DiagramSocketValue> keyValuePair in this._connectionsValue )
            {
                keyValuePair.Key.Value = null;
                this.Strategy.AddRemoveSocket( keyValuePair.Key, false );
            }
            this._connectionsValue.Clear();
        }



        private PropertyDescriptorCollection CreatePropertyDescriptorCollection()
        {
            return new PropertyDescriptorCollection( this.Parameters.Select( x => new DiagramElementProperties( x ) ).Cast<PropertyDescriptor>().ToArray() );
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes( this, true );
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName( this, true );
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName( this, true );
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter( this, true );
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent( this, true );
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty( this, true );
        }

        object ICustomTypeDescriptor.GetEditor(
          Type x )
        {
            return TypeDescriptor.GetEditor( this, x, true );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
              Attribute[ ] x )
        {
            return TypeDescriptor.GetEvents( this, x, true );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents( this, true );
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
          Attribute[ ] x )
        {
            return this.CreatePropertyDescriptorCollection();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return this.CreatePropertyDescriptorCollection();
        }

        object ICustomTypeDescriptor.GetPropertyOwner(
          PropertyDescriptor x )
        {
            return this;
        }

        /// <summary>
        /// Create a copy of <see cref="T:StockSharp.Diagram.DiagramElement" />.
        /// </summary>
        /// <param name="cloneSockets">To create copies of connections.</param>
        /// <returns>Copy.</returns>
        public virtual DiagramElement Clone( bool cloneSockets = true )
        {
            DiagramElement instance = ( DiagramElement )Activator.CreateInstance( this.GetType() );
            SettingsStorage storage = this.Save();
            if ( !cloneSockets )
            {
                storage.Remove( nameof( Id ) );
                storage.Remove( nameof( CanAutoName ) );
                storage.Remove( nameof( SettingsStorage ) );
            }
            instance.Load( storage );
            return instance;
        }

        /// <inheritdoc />
        public override void Save( SettingsStorage storage )
        {
            storage.SetValue( nameof( Id ), this.Id );
            storage.SetValue( nameof( CanAutoName ), this.CanAutoName );
            SettingsStorage settingsStorage = new SettingsStorage();
            foreach ( IDiagramElementParam persistable in this.Parameters.Where( DiagramElement.SomeClass01.Function010 ?? ( DiagramElement.SomeClass01.Function010 = new Func<IDiagramElementParam, bool>( DiagramElement.SomeClass01._someInstance01.Method010 ) ) ).ToArray() )
                settingsStorage.SetValue( persistable.Name, persistable.Save() );
            storage.SetValue( nameof( SettingsStorage ), settingsStorage );
        }

        /// <inheritdoc />
        public override void Load( SettingsStorage storage )
        {
            try
            {
                this._noChange = true;
                this.Id = storage.GetValue( nameof( Id ), this.Id );
                this.RemoveSockets( false );
                this.CanAutoName = storage.GetValue( nameof( CanAutoName ), true );
                storage.CheckParameters( nameof( SettingsStorage ), new Action<SettingsStorage>( this.SettingStorageAction ), false );
                if ( this.CanAutoName )
                    return;
                this.DiagramElementParamString = ( this.Name );
            }
            finally
            {
                this._noChange = false;
            }
        }

        private protected IReadOnlyDictionary<string, DiagramElementParam<object>> SomeShit035(
          SettingsStorage x )
        {
            var dict = new Dictionary<string, DiagramElementParam<object>>();

            x.CheckParameters( nameof( -1260199142 ), new Action<SettingsStorage>( y => {
                

                foreach ( var keyValuePair in y )
                {
                    DiagramElementParam<object> diagramElementParam = new DiagramElementParam<object>();
                    diagramElementParam.Load( ( SettingsStorage )keyValuePair.Value );
                    dict[keyValuePair.Key] = diagramElementParam;
                }
            } ), false );

            return dict;
        }









        /// <summary>
        /// <see cref="T:Ecng.ComponentModel.IDispatcher" />.
        ///     </summary>
        public static IDispatcher Dispatcher
        {
            get
            {
                return ConfigManager.GetService<IDispatcher>();
            }
        }



        private void Method234( bool x )
        {
            this.RaisePropertiesChanged();
        }





        private void SettingStorageAction( SettingsStorage x )
        {
            foreach ( KeyValuePair<string, object> keyValuePair in x )
            {                
                IDiagramElementParam diagramElementParam = this.Parameters.FirstOrDefault( y => y.Name.EqualsIgnoreCase( keyValuePair.Key ) );
                if ( diagramElementParam != null )
                {
                    try
                    {
                        diagramElementParam.Load( ( SettingsStorage )keyValuePair.Value );
                    }
                    catch ( Exception ex )
                    {
                        this.AddErrorLog( string.Concat( nameof( -1260198933 ), diagramElementParam.Name, nameof( -1260198913 ) ), new object[1]
                        {
               ex
                        } );
                    }
                }
            }
        }

        
        private sealed class SomeHolder059
        {
            public Dictionary<string, DiagramElementParam<object>> _dict;

            internal void Metho036( SettingsStorage x )
            {
                foreach ( KeyValuePair<string, object> keyValuePair in x )
                {
                    DiagramElementParam<object> diagramElementParam = new DiagramElementParam<object>();
                    diagramElementParam.Load( ( SettingsStorage )keyValuePair.Value );
                    this._dict[keyValuePair.Key] = diagramElementParam;
                }
            }
        }























    }
}