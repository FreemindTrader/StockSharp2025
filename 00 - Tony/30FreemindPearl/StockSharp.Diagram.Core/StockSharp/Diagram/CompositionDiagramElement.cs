// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.CompositionDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.BusinessEntities;
using StockSharp.Diagram.Elements;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockSharp.Diagram
{
    /// <summary>Composite element.</summary>
    [DisplayNameLoc( "CompositeElement" )]
    [DescriptionLoc( "CompositeElement", true )]
    [CategoryLoc( "Composition" )]
    public class CompositionDiagramElement : DiagramElement
    {
        /// <summary>Child element added.</summary>
        public event Action<DiagramElement> ElementAdded;

        /// <summary>Child element removed.</summary>
        public event Action<DiagramElement> ElementRemoved;

        /// <summary>Raised when strategy changed.</summary>
        public event Action StrategyChanged;

        /// <summary>The composite element diagram change event.</summary>
        public event Action Changed;

        /// <summary>
        /// Invoke <see cref="E:StockSharp.Diagram.CompositionDiagramElement.ElementAdded" />.
        /// </summary>
        /// <param name="element">
        ///   <see cref="T:StockSharp.Diagram.DiagramElement" />
        /// </param>
        protected void RaiseElementAdded( DiagramElement element )
        {
            Action<DiagramElement> zwF1Azeo = ElementAdded;
            if ( zwF1Azeo == null )
                return;
            zwF1Azeo( element );
        }

        /// <summary>
        /// Invoke <see cref="E:StockSharp.Diagram.CompositionDiagramElement.ElementRemoved" />.
        /// </summary>
        /// <param name="element">
        ///   <see cref="T:StockSharp.Diagram.DiagramElement" />
        /// </param>
        protected void RaiseElementRemoved( DiagramElement element )
        {
            Action<DiagramElement> zpUvmMdo = ElementRemoved;
            if ( zpUvmMdo == null )
                return;
            zpUvmMdo( element );
        }

        /// <summary>
        /// Invoke <see cref="E:StockSharp.Diagram.CompositionDiagramElement.Changed" />.
        /// </summary>
        protected void RaiseChanged()
        {
            Action zwTYi4 = Changed;
            if ( zwTYi4 == null )
                return;
            zwTYi4();
        }

        /// <summary>
        /// Invoke  <see cref="E:StockSharp.Diagram.CompositionDiagramElement.StrategyChanged" />.
        /// </summary>
        protected void RaiseStrategyChanged()
        {
            Action zEloocTk = StrategyChanged;
            if ( zEloocTk == null )
                return;
            zEloocTk();
        }


        private int _schemaVersion = 1;

        private Guid _typeId = Guid.NewGuid();

        private readonly string _iconName = string.Empty;

        private CompositionDiagramElement.DiagramElementParamChanged[ ] _diagramElementParamChanged;



        private string _category;

        private string _description;

        private string _docUrl;

        private SchemeTypes _type;

        private bool _isLoaded;

        private long _revision;

        private DiagramStrategy _diagramStrategy;

        private DiagramElement[ ] _diagramElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />.
        /// </summary>
        /// <param name="model">
        ///   <see cref="T:StockSharp.Diagram.ICompositionModel" />
        /// </param>
        public CompositionDiagramElement( ICompositionModel model )
        {
            this.Model = model;
            this.ShowParameters = true;
            this.IsLoaded = true;
        }


        /// <summary>
        ///   <see cref="P:StockSharp.Diagram.CompositionDiagramElement.TypeId" />
        /// </summary>
        /// <param name="typeId">
        ///   <see cref="P:StockSharp.Diagram.CompositionDiagramElement.TypeId" />
        /// </param>
        protected void SetTypeId( Guid typeId )
        {
            this._typeId = typeId;
        }

        /// <summary>
        ///   <see cref="P:StockSharp.Diagram.ICompositionModel.Elements" />
        /// </summary>
        public IEnumerable<DiagramElement> Elements
        {
            get
            {
                return this.Model.Elements.Where<DiagramElement>( CompositionDiagramElement.LamdaShit.Func001 ?? ( CompositionDiagramElement.LamdaShit.Func001 = new Func<DiagramElement, bool>( CompositionDiagramElement.LamdaShit._lamdaShit.Func1 ) ) );
            }
        }

        /// <summary>Scheme type.</summary>
        public SchemeTypes Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        /// <summary>
        ///   <see cref="P:StockSharp.Diagram.ICompositionModel.HasErrors" />
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return this.Model.HasErrors;
            }
        }

        /// <summary>Is composite diagram element loaded.</summary>
        public bool IsLoaded
        {
            get
            {
                return this._isLoaded;
            }
            set
            {
                this._isLoaded = value;
                this.RaisePropertyChanged( nameof( IsLoaded ) );
            }
        }

        /// <summary>The content revision.</summary>
        public long Revision
        {
            get
            {
                return this._revision;
            }
            set
            {
                this._revision = value;
            }
        }

        /// <inheritdoc />
        public override string IconName
        {
            get
            {
                return this._iconName;
            }
        }

        /// <summary>
        ///   <see cref="P:StockSharp.Diagram.ICompositionModel.Modifiable" />
        /// </summary>
        public bool IsModifiable
        {
            get
            {
                return this.Model.Modifiable;
            }
            set
            {
                if ( this.Model.Modifiable == value )
                    return;
                this.Model.Modifiable = value;
                this.RaisePropertyChanged( nameof( IsModifiable ) );
            }
        }

        /// <inheritdoc />
        public override bool IsUndoRedoing
        {
            get
            {
                ICompositionModel model = this.Model;
                if ( model == null )
                    return false;
                bool? isUndoingRedoing = model.UndoManager?.IsUndoingRedoing;
                return isUndoingRedoing.GetValueOrDefault() & isUndoingRedoing.HasValue;
            }
        }

        /// <inheritdoc />
        public override bool HasUndoManager
        {
            get
            {
                return this.Model?.UndoManager != null;
            }
        }

        /// <summary>The strategy to which the element is attached.</summary>
        public override DiagramStrategy Strategy
        {
            get
            {
                DiagramStrategy zQt0kNnU = this._diagramStrategy;
                if ( zQt0kNnU != null )
                    return zQt0kNnU;
                return this.ParentComposition?.Strategy;
            }
            set
            {
                if ( this._diagramStrategy == value )
                    return;
                this._diagramStrategy = value;
                this.RaisePropertyChanged( nameof( Strategy ) );
                this.RaiseStrategyChanged();
            }
        }

        private ICompositionModel _model;

        [Browsable( false )]
        public ICompositionModel Model
        {
            get
            {
                return this._model;
            }
            set
            {
                if ( value == null )
                    throw new ArgumentNullException( "Model value == null" );

                if ( this._model == value )
                    return;

                if ( this._model != null )
                    this.Uninitialized();

                this._model = value;
                if ( this._model == null )
                    return;

                this.Initialized();
                this.IsModifiable = false;
            }
        }

        private void Uninitialized()
        {
            ICompositionModel model = this.Model;
            model.ModelChanged -= new Action( this.OnModelChanged );
            model.ElementAdded -= new Action<DiagramElement>( this.OnElementAdded );
            model.ElementRemoved -= new Action<DiagramElement>( this.OnElementRemoved );
            this.Elements.ForEach<DiagramElement>( new Action<DiagramElement>( this.OnEachElementUninitialied ) );
            this.RemoveSockets( true );
            this._diagramElementParamChanged = ( CompositionDiagramElement.DiagramElementParamChanged[ ] )null;
        }

        private void Initialized()
        {
            ICompositionModel model = this.Model;
            model.ModelChanged += new Action( this.OnModelChanged );
            model.ElementAdded += new Action<DiagramElement>( this.OnElementAdded );
            model.ElementRemoved += new Action<DiagramElement>( this.OnElementRemoved );
            this.Elements.ForEach<DiagramElement>( new Action<DiagramElement>( this.OnEachElementInitialied ) );
            this.DoSomeShit();
        }


        private void OnModelChanged()
        {
            this.DoSomeShit();
            this.RaiseChanged();
        }

        private void OnElementAdded( DiagramElement _param1 )
        {
            this.OnEachElementInitialied( _param1 );
            this.DoSomeShit();
            this.RaiseElementAdded( _param1 );
            this.RaisePropertiesChanged();
        }

        private void OnElementRemoved( DiagramElement _param1 )
        {
            this.OnEachElementUninitialied( _param1 );
            this.DoSomeShit();
            this.RaiseElementRemoved( _param1 );
            this.RaisePropertiesChanged();
        }

        private void OnEachElementUninitialied( DiagramElement _param1 )
        {
            _param1.PropertyChanged -= new PropertyChangedEventHandler( ( ( DiagramElement )this ).RaisePropertyChanged );
            _param1.PropertiesChanged -= new Action( this.OnPropertiesChanged );
            _param1.ParameterValueChanged -= new Action<string>( ( ( DiagramElement )this ).RaiseParameterValueChanged );
            _param1.UnInit();
        }


        private void OnEachElementInitialied( DiagramElement _param1 )
        {
            _param1.Init( ( ILogSource )this );
            _param1.PropertyChanged += new PropertyChangedEventHandler( ( ( DiagramElement )this ).RaisePropertyChanged );
            _param1.PropertiesChanged += new Action( this.OnPropertiesChanged );
            _param1.ParameterValueChanged += new Action<string>( ( ( DiagramElement )this ).RaiseParameterValueChanged );
        }

        private sealed class SomeHolder1209
        {
            public ISet<ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int>> ISet;
      public CompositionDiagramElement _diagramElement;

            internal bool Method03( DiagramSocket _param1 )
            {
                CompositionDiagramElement.DiagramSocketEx zrmiMtHt09McF = ( CompositionDiagramElement.DiagramSocketEx )_param1;
                DiagramSocket diagramSocket = zrmiMtHt09McF.GetDiagramSocket();
                ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int> valueTuple = CompositionDiagramElement.DontKnowFunction( zrmiMtHt09McF.MyStringOne(), diagramSocket );
                if ( diagramSocket != null )
                    return !this.ISet.Contains( valueTuple );
                return true;
            }

            internal void Method04(
              ValueTuple<string, DiagramSocket> _param1 )
            {
                CompositionDiagramElement.\u0023\u003DzkUig\u0024CITF5U3k2faXmHakbo\u003D citF5U3k2faXmHakbo = new CompositionDiagramElement.\u0023\u003DzkUig\u0024CITF5U3k2faXmHakbo\u003D();
                ValueTuple<string, DiagramSocket> valueTuple = _param1;
                string str = valueTuple.Item1;
                DiagramSocket diagramSocket = valueTuple.Item2;
                citF5U3k2faXmHakbo._valueTuple = CompositionDiagramElement.DontKnowFunction( str, diagramSocket );
                citF5U3k2faXmHakbo._diagramSocketEx = ( CompositionDiagramElement.DiagramSocketEx )this._diagramElement.InputSockets.Concat<DiagramSocket>( ( IEnumerable<DiagramSocket> )this._diagramElement.OutputSockets ).FirstOrDefault<DiagramSocket>( new Func<DiagramSocket, bool>( citF5U3k2faXmHakbo.Method03) );
                if ( citF5U3k2faXmHakbo._diagramSocketEx != null)
        {
                    ( ( IEnumerable<DiagramSocket> )this._diagramElement.GetConnectedSourceSockets( ( DiagramSocket )citF5U3k2faXmHakbo._diagramSocketEx) ).ForEach<DiagramSocket>( new Action<DiagramSocket>( citF5U3k2faXmHakbo.\u0023\u003DzNyDHYpIEme78mUo_0w\u003D\u003D) );
                }
        else
                {
                    string id = CompositionDiagramElement.\u0023\u003Dzcwdh5BbdSGF3( str, diagramSocket.Id );
                    CompositionDiagramElement.DiagramSocketEx zrmiMtHt09McF = diagramSocket.Directon == DiagramSocketDirection.In ? ( CompositionDiagramElement.DiagramSocketEx )this._diagramElement.AddInput( id, diagramSocket.Name, diagramSocket.Type, ( Action<DiagramSocketValue> )null, diagramSocket.LinkableMaximum, int.MaxValue, false, new bool?() ) : ( CompositionDiagramElement.DiagramSocketEx )this._diagramElement.AddOutput( id, diagramSocket.Name, diagramSocket.Type, diagramSocket.LinkableMaximum, int.MaxValue, new bool?() );
                    zrmiMtHt09McF.MyStringOne = ( str );
                    zrmiMtHt09McF.MyStringTwo = ( diagramSocket.Id );
                }
            }
        }

        private sealed class \u0023\u003DzkUig\u0024CITF5U3k2faXmHakbo\u003D
    {
      public ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int> _valueTuple;
      public CompositionDiagramElement.DiagramSocketEx _diagramSocketEx;

      internal bool Method03(DiagramSocket _param1)
      {
        CompositionDiagramElement.DiagramSocketEx zrmiMtHt09McF = ( CompositionDiagramElement.DiagramSocketEx )_param1;
        return this._valueTuple.Equals( CompositionDiagramElement.DontKnowFunction(zrmiMtHt09McF.MyStringOne(), zrmiMtHt09McF.GetDiagramSocket()));
      }

    internal void \u0023\u003DzNyDHYpIEme78mUo_0w\u003D\u003D(DiagramSocket _param1)
      {
        DiagramSocket diagramSocket = this._diagramSocketEx.GetDiagramSocket();
    diagramSocket.Disconnect(_param1);
        diagramSocket.Connect(_param1);
      }
    }

        private void DoSomeShit()
        {
            CompositionDiagramElement.SomeHolder1209 zn9T4vqolEhwdyme = new CompositionDiagramElement.SomeHolder1209();
            zn9T4vqolEhwdyme._diagramElement = this;
            this.\u0023\u003DztxeN8cN4sv_O10G7MQ\u003D\u003D();
            ValueTuple<string, DiagramSocket>[ ] array = this.Model.GetDisconnectedSockets().ToArray<ValueTuple<string, DiagramSocket>>();
            zn9T4vqolEhwdyme.ISet = ( ( IEnumerable<ValueTuple<string, DiagramSocket>> )array ).Select<ValueTuple<string, DiagramSocket>, ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int>>( CompositionDiagramElement.LamdaShit.Func004 ?? ( CompositionDiagramElement.LamdaShit.Func004 = new Func<ValueTuple<string, DiagramSocket>, ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int>>( CompositionDiagramElement.LamdaShit._lamdaShit.Function1234 ) ) ).ToSet<ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int>>();
            this.RemoveSockets( new Func<DiagramSocket, bool>( zn9T4vqolEhwdyme.Method03 ), true );
            ( ( IEnumerable<ValueTuple<string, DiagramSocket>> )array ).ForEach<ValueTuple<string, DiagramSocket>>( new Action<ValueTuple<string, DiagramSocket>>( zn9T4vqolEhwdyme.Method04 ) );
            this.\u0023\u003DzfEpHIOtBXxeR();
            this.SetDiagramElementParamChanged();
        }

        private void OnPropertiesChanged()
        {
            this.SetDiagramElementParamChanged();
            this.RaisePropertiesChanged();
        }

        private void SetDiagramElementParamChanged()
        {
            this._diagramElementParamChanged = this.Elements.Where<DiagramElement>( CompositionDiagramElement.LamdaShit.Func005 ?? ( CompositionDiagramElement.LamdaShit.Func005 = new Func<DiagramElement, bool>( CompositionDiagramElement.LamdaShit._lamdaShit.ShowParameters ) ) ).SelectMany<DiagramElement, CompositionDiagramElement.DiagramElementParamChanged>( CompositionDiagramElement.LamdaShit.Func007 ?? ( CompositionDiagramElement.LamdaShit.Func007 = new Func<DiagramElement, IEnumerable<CompositionDiagramElement.DiagramElementParamChanged>>( CompositionDiagramElement.LamdaShit._lamdaShit.DontKnowFunction02 ) ) ).ToArray<CompositionDiagramElement.DiagramElementParamChanged>();
        }

        /// <summary>
        /// Suspend undo/redo manager for <see cref="P:StockSharp.Diagram.CompositionDiagramElement.Model" />.
        /// </summary>
        public void SuspendUndoManager()
        {
            this.Model.IsUndoManagerSuspended = true;
            this.Elements.OfType<CompositionDiagramElement>().ForEach<CompositionDiagramElement>( CompositionDiagramElement.LamdaShit.Action001 ?? ( CompositionDiagramElement.LamdaShit.Action001 = new Action<CompositionDiagramElement>( CompositionDiagramElement.LamdaShit._lamdaShit.Func2 ) ) );
        }

        /// <summary>
        /// Resume undo/redo manager for <see cref="P:StockSharp.Diagram.CompositionDiagramElement.Model" />.
        /// </summary>
        public void ResumeUndoManager()
        {
            this.Model.IsUndoManagerSuspended = false;
            this.Elements.OfType<CompositionDiagramElement>().ForEach<CompositionDiagramElement>( CompositionDiagramElement.LamdaShit.Action002 ?? ( CompositionDiagramElement.LamdaShit.Action002 = new Action<CompositionDiagramElement>( CompositionDiagramElement.LamdaShit._lamdaShit.ResumeUndoManager ) ) );
        }

        private sealed class DiagramElementParamChanged : IDiagramElementParam, IPersistable, INotifyPropertyChanging, INotifyPropertyChanged
        {

            private readonly DiagramElement _diagramElement;

            private readonly IDiagramElementParam _component;

            private readonly bool _isVariableDiagramElement;

            private readonly IList<Attribute> _attributesList;

            public event PropertyChangingEventHandler PropertyChanging;
            public event PropertyChangedEventHandler PropertyChanged;

            public DiagramElementParamChanged( DiagramElement dElement, IDiagramElementParam deParam )
            {
                if ( dElement == null )
                    throw new ArgumentNullException( "dElement == null" );
                this._diagramElement = dElement;


                if ( deParam == null )
                    throw new ArgumentNullException( "deParam == null" );
                this._component = deParam;
                this._isVariableDiagramElement = dElement is VariableDiagramElement;
                this._attributesList = ( IList<Attribute> )this._component.Attributes.Where<Attribute>( x => !( x is DisplayAttribute ) ).ToList<Attribute>();
            }

            string IDiagramElementParam.Name
            {
                get
                {
                    return string.Concat( this._diagramElement.Id.ToString(), this._component.Name );
                }

                set
                {
                    throw new NotSupportedException();
                }

            }


            string IDiagramElementParam.DisplayName
            {
                get
                {
                    if ( !this._isVariableDiagramElement )
                        return string.Concat( this._component.DisplayName, "(", this._diagramElement.Name, ")" );
                    return this._diagramElement.Name;
                }

                set
                {
                    throw new NotSupportedException();
                }
            }


            string IDiagramElementParam.Description
            {
                get
                {
                    return this._component.Description;
                }

                set
                {
                    throw new NotSupportedException();
                }
            }

            string IDiagramElementParam.Category
            {
                get
                {
                    return LocalizedStrings.Str3041;
                }

                set
                {
                    throw new NotSupportedException();
                }
            }

            Type IDiagramElementParam.Type
            {
                get
                {
                    return this._component.Type;
                }
            }



            IList<Attribute> IDiagramElementParam.Attributes
            {
                get
                {
                    return this._attributesList;
                }
            }

            object IDiagramElementParam.Value
            {
                get
                {
                    return this._component.Value;
                }

                set
                {
                    this._component.Value = value;
                }
            }


            bool IDiagramElementParam.IsDefault
            {
                get
                {
                    return this._component.IsDefault;
                }
            }


            bool IDiagramElementParam.IsParam
            {
                get
                {
                    return this._component.IsParam;
                }

                set
                {
                    throw new NotSupportedException();
                }
            }



            bool IDiagramElementParam.IgnoreOnSave
            {
                get
                {
                    return this._component.IgnoreOnSave;
                }

                set
                {
                    this._component.IgnoreOnSave = value;
                }

            }


            bool IDiagramElementParam.NotifyOnChanged
            {
                get
                {
                    throw new NotSupportedException();
                }

                set
                {
                    throw new NotSupportedException();
                }

            }

            void IDiagramElementParam.SetValueWithIgnoreOnSave( object _param1 )
            {
                this._component.SetValueWithIgnoreOnSave( _param1 );
            }

            public void Load( SettingsStorage _param1 )
            {
                this._component.Load( _param1 );
            }

            public void Save( SettingsStorage _param1 )
            {
                this._component.Save( _param1 );
            }

            public override string ToString()
            {
                return ( ( object )this._component ).ToString();
            }

            //void INotifyPropertyChanged.\u0023\u003Dz_C4tIKqQRl2__R8Sh6kALg6MFNPr(
            //  PropertyChangedEventHandler _param1)
            //{
            //}

            //void INotifyPropertyChanged.\u0023\u003DzEZNRDUBeXonGAE1nD9wYj4cSHyj\u0024(
            //  PropertyChangedEventHandler _param1)
            //{
            //}

            //void INotifyPropertyChanging.\u0023\u003DzyoILPW0fYVGtP7mintDmCZMpc\u0024XF(
            //  PropertyChangingEventHandler _param1)
            //{
            //}

            //void INotifyPropertyChanging.\u0023\u003DzV9l3WEaucWH5H\u0024TqrDM99ZOhRkYR(
            //  PropertyChangingEventHandler _param1)
            //{
            //}


        }


        [Serializable]
        private sealed class LamdaShit
        {
            public static readonly CompositionDiagramElement.LamdaShit _lamdaShit = new CompositionDiagramElement.LamdaShit();
            public static Func<DiagramElement, bool> Func001;
            public static Action<CompositionDiagramElement> Action001;
            public static Action<CompositionDiagramElement> Action002;
            public static Action<DiagramElement> Action003;
            public static Action<DiagramElement> Action004;
            public static Action<DiagramElement> Action005;
            public static Action<DiagramElement> Action006;
            public static Func<DiagramElement, bool> Func002;
            public static Func<DiagramElement, int> Func003;
            public static Action<DiagramElement> Action007;
            public static Func<ValueTuple<string, DiagramSocket>, ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int>> Func004;
            public static Func<DiagramElement, bool> Func005;
            public static Func<IDiagramElementParam, bool> Func006;
            public static Func<DiagramElement, IEnumerable<CompositionDiagramElement.DiagramElementParamChanged>> Func007;
            public static Func<CompositionDiagramElement.DiagramSocketEx, DiagramElement> Func008;
            public static Func<DiagramElement, bool> Func009;
            public static Func<VariableDiagramElement, bool> Func010;
            public static Func<VariableDiagramElement, object> Func011;
            public static Func<CompositionDiagramElement, IEnumerable<Portfolio>> Func012;

            internal bool Func1( DiagramElement _param1 )
            {
                return _param1 != null;
            }

            internal void Func2( CompositionDiagramElement _param1 )
            {
                _param1.SuspendUndoManager();
            }

            internal void ResumeUndoManager( CompositionDiagramElement _param1 )
            {
                _param1.ResumeUndoManager();
            }

            internal void UnInit( DiagramElement _param1 )
            {
                _param1.UnInit();
            }

            internal void Start( DiagramElement _param1 )
            {
                _param1.Start();
            }

            internal void Stop( DiagramElement _param1 )
            {
                _param1.Stop();
            }

            internal void Reset( DiagramElement _param1 )
            {
                _param1.Reset();
            }

            internal bool NeedFlush( DiagramElement _param1 )
            {
                return _param1.NeedFlush >= 0;
            }

            internal int NeedFlush( DiagramElement _param1 )
            {
                return _param1.NeedFlush;
            }

            internal void Flush( DiagramElement _param1 )
            {
                _param1.Flush();
            }

            internal ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int> Function1234(
              ValueTuple<string, DiagramSocket> _param1 )
            {
                return CompositionDiagramElement.DontKnowFunction( _param1.Item1, _param1.Item2 );
            }

            internal bool ShowParameters( DiagramElement _param1 )
            {
                return _param1.ShowParameters;
            }

            internal IEnumerable<CompositionDiagramElement.DiagramElementParamChanged> DontKnowFunction02(
              DiagramElement _param1 )
            {
                CompositionDiagramElement.DontKnowFunction03 qlcUlCdzS7oLlBrvbw = new CompositionDiagramElement.DontKnowFunction03();
                qlcUlCdzS7oLlBrvbw._diagramElement = _param1;
                return qlcUlCdzS7oLlBrvbw._diagramElement.Parameters.Where<IDiagramElementParam>( CompositionDiagramElement.LamdaShit.Func006 ?? ( CompositionDiagramElement.LamdaShit.Func006 = new Func<IDiagramElementParam, bool>( CompositionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzGeOH5bFP1eljY\u0024zO56obRF8\u003D) ) ).Select<IDiagramElementParam, CompositionDiagramElement.DiagramElementParamChanged>( new Func<IDiagramElementParam, CompositionDiagramElement.DiagramElementParamChanged>( qlcUlCdzS7oLlBrvbw.Method00032 ) );
            }

            internal bool \u0023\u003DzGeOH5bFP1eljY\u0024zO56obRF8\u003D(IDiagramElementParam _param1)
      {
    return _param1.IsParam;
}

        internal DiagramElement \u0023\u003DzlG443prdoWRf5yF07V9_6YA\u003D(
          CompositionDiagramElement.DiagramSocketEx _param1)
      {
    return _param1.GetDiagramElement();
}

    internal bool \u0023\u003Dz6Fukn0Jc4Dx5gwI_oFE9yLk\u003D(DiagramElement _param1)
      {
    return _param1 != null;
}

internal bool \u0023\u003DzmNm4PSpNQ_xto0gURoJhTe\u0024eG53fKoVoGw\u003D\u003D(
  VariableDiagramElement _param1)
      {
    return ( Equatable<DiagramSocketType> )_param1.Type == DiagramSocketType.Portfolio;
}

internal object \u0023\u003DzBN8AVPSYtq9fkOBy7FzaJwLTiZa6eqEldA\u003D\u003D(
  VariableDiagramElement _param1)
      {
    return _param1.Value;
}

internal IEnumerable<Portfolio> \u0023\u003DzZ8b4NH3Bzv2Mrk0OVcBFoTn33EyroX8T0g\u003D\u003D(
  CompositionDiagramElement _param1)
      {
    return _param1.FindPortfolios();
}
    }

        
/// <inheritdoc />
public override IEnumerable<IDiagramElementParam> Parameters
{
    get
    {
        IEnumerable<IDiagramElementParam> first = base.Parameters;
        if ( this._diagramElementParamChanged != null )
            first = first.Concat<IDiagramElementParam>( ( IEnumerable<IDiagramElementParam> )this._diagramElementParamChanged );
        return first;
    }
}

/// <summary>
///   <see cref="T:StockSharp.Diagram.ICompositionModel" />
/// </summary>


/// <summary>Other sockets if this one is connected.</summary>
/// <param name="socket">
///   <see cref="T:StockSharp.Diagram.DiagramSocket" />
/// </param>
/// <returns>Connected sockets.</returns>
public IEnumerable<DiagramSocket> ConnectedToSockets(
  DiagramSocket socket )
{
    return this.Model.GetConnectedSocketsFor( ( DiagramElement )this, socket ) ?? Enumerable.Empty<DiagramSocket>();
}

/// <summary>
/// The name of the group which includes a diagram element.
/// </summary>
public new string Category
{
    get
    {
        return this._category;
    }
    set
    {
        this._category = value;
        this.RaisePropertyChanged( nameof( Category ) );
    }
}

/// <summary>The diagram element description.</summary>
public new string Description
{
    get
    {
        return this._description;
    }
    set
    {
        this._description = value;
        this.RaisePropertyChanged( nameof( Description ) );
    }
}

/// <summary>Schema version.</summary>
public int SchemaVersion
{
    get
    {
        return this._schemaVersion;
    }
    set
    {
        this._schemaVersion = value;
    }
}

/// <inheritdoc />
public override string DocUrl
{
    get
    {
        return this._docUrl;
    }
    set
    {
        this._docUrl = value;
    }
}

/// <inheritdoc />
public override Guid TypeId
{
    get
    {
        return this._typeId;
    }
}






/// <inheritdoc />
protected override void RaiseParameterValueChanged( string parameterName )
{
    base.RaiseParameterValueChanged( parameterName );
    this.RaiseChanged();
}

/// <inheritdoc />
public override void Load( SettingsStorage storage )
{
    base.Load( storage );
    this._typeId = storage.GetValue<string>( nameof( TypeId ), ( string )null ).To<Guid>();
    this.Type = storage.GetValue<SchemeTypes>( nameof( Type ), this.Type );
    this.Revision = storage.GetValue<long>( nameof( Revision ), this.Revision );
    this.DoSomeShit();
}

/// <inheritdoc />
public override void Save( SettingsStorage storage )
{
    base.Save( storage );
    storage.Set<Guid>( nameof( -1260198449 ), this._typeId ).Set<SchemeTypes>( nameof( -1260198464 ), this.Type ).Set<long>( nameof( -1260198441 ), this.Revision );
}

/// <inheritdoc />
protected override void OnInit()
{
    base.OnInit();
    this.Elements.ForEach<DiagramElement>( new Action<DiagramElement>( this.\u0023\u003Dz_zIXfnS8hYmr7z4lk5OuGkc\u003D) );
}

/// <inheritdoc />
protected override void OnUnInit()
{
    this.Elements.ForEach<DiagramElement>( CompositionDiagramElement.LamdaShit.Action003 ?? ( CompositionDiagramElement.LamdaShit.Action003 = new Action<DiagramElement>( CompositionDiagramElement.LamdaShit._lamdaShit.UnInit ) ) );
    base.OnUnInit();
}

/// <inheritdoc />
protected override void OnStart()
{
    this.\u0023\u003DzWM6PcXNfqCij();
    this.Elements.ForEach<DiagramElement>( CompositionDiagramElement.LamdaShit.Action004 ?? ( CompositionDiagramElement.LamdaShit.Action004 = new Action<DiagramElement>( CompositionDiagramElement.LamdaShit._lamdaShit.Start ) ) );
    base.OnStart();
}

/// <inheritdoc />
protected override void OnStop()
{
    this.\u0023\u003DzWM6PcXNfqCij();
    this.Elements.ForEach<DiagramElement>( CompositionDiagramElement.LamdaShit.Action005 ?? ( CompositionDiagramElement.LamdaShit.Action005 = new Action<DiagramElement>( CompositionDiagramElement.LamdaShit._lamdaShit.Stop ) ) );
    base.OnStop();
}

/// <inheritdoc />
protected override void OnReseted()
{
    this.Elements.ForEach<DiagramElement>( CompositionDiagramElement.LamdaShit.Action006 ?? ( CompositionDiagramElement.LamdaShit.Action006 = new Action<DiagramElement>( CompositionDiagramElement.LamdaShit._lamdaShit.Reset ) ) );
}

private void \u0023\u003DzWM6PcXNfqCij()
    {
    if ( this.HasErrors )
        throw new InvalidOperationException( LocalizedStrings.Str3044 );
}

/// <inheritdoc />
public override int NeedFlush
{
    get
    {
        return 0;
    }
}

/// <inheritdoc />
public override void Flush()
{
    this.Elements.Where<DiagramElement>( CompositionDiagramElement.LamdaShit.Func002 ?? ( CompositionDiagramElement.LamdaShit.Func002 = new Func<DiagramElement, bool>( CompositionDiagramElement.LamdaShit._lamdaShit.NeedFlush ) ) ).OrderBy<DiagramElement, int>( CompositionDiagramElement.LamdaShit.Func003 ?? ( CompositionDiagramElement.LamdaShit.Func003 = new Func<DiagramElement, int>( CompositionDiagramElement.LamdaShit._lamdaShit.NeedFlush ) ) ).ForEach<DiagramElement>( CompositionDiagramElement.LamdaShit.Action007 ?? ( CompositionDiagramElement.LamdaShit.Action007 = new Action<DiagramElement>( CompositionDiagramElement.LamdaShit._lamdaShit.Flush ) ) );
}

/// <inheritdoc />
protected override void OnProcess(
  DateTimeOffset time,
  IDictionary<DiagramSocket, DiagramSocketValue> values,
  DiagramSocketValue source )
{
}














/// <inheritdoc />
protected override DiagramSocket CreateSocketInstance(
  DiagramSocketDirection dir,
  string socketId = null )
{
    return ( DiagramSocket )new CompositionDiagramElement.DiagramSocketEx( dir, socketId );
}

private static string \u0023\u003Dzcwdh5BbdSGF3( string _param0, string _param1 )
    {
    return string.Concat( _param0, nameof( -1260198490 ), _param1 );
}



private void \u0023\u003DzfEpHIOtBXxeR()
    {
    this.\u0023\u003DztxeN8cN4sv_O10G7MQ\u003D\u003D();
    this._diagramElements = this.\u0023\u003DznL0TKllItfT3().ToArray<DiagramElement>();
    ( ( IEnumerable<DiagramElement> )this._diagramElements ).ForEach<DiagramElement>( new Action<DiagramElement>( this.\u0023\u003DzmTaYAc6h6Qqbh2lDVQb7QsE\u003D) );
}

private void \u0023\u003DztxeN8cN4sv_O10G7MQ\u003D\u003D()
    {
    if ( this._diagramElements == null )
        return;
    ( ( IEnumerable<DiagramElement> )this._diagramElements ).ForEach<DiagramElement>( new Action<DiagramElement>( this.\u0023\u003DzekIpOCiF4OUtAOpaIfsOL2EWHrkY ) );
    this._diagramElements = ( DiagramElement[ ] )null;
}





/// <inheritdoc />
protected override void OnSocketConnected( DiagramSocket socket, DiagramSocket source )
{
    base.OnSocketConnected( socket, source );
    if ( socket.IsOutput )
        return;
    ( ( CompositionDiagramElement.DiagramSocketEx )socket ).GetDiagramSocket()?.Connect( source );
}

/// <inheritdoc />
protected override void OnSocketDisconnected( DiagramSocket socket, DiagramSocket source )
{
    base.OnSocketDisconnected( socket, source );
    if ( socket.IsOutput )
        return;
    ( ( CompositionDiagramElement.DiagramSocketEx )socket ).GetDiagramSocket()?.Disconnect( source );
}

private IEnumerable<DiagramElement> \u0023\u003DznL0TKllItfT3()
    {
    if ( this.Model == null )
        return Enumerable.Empty<DiagramElement>();
    return ( IEnumerable<DiagramElement> )this.OutputSockets.OfType<CompositionDiagramElement.DiagramSocketEx>().Select<CompositionDiagramElement.DiagramSocketEx, DiagramElement>( CompositionDiagramElement.LamdaShit.Func008 ?? ( CompositionDiagramElement.LamdaShit.Func008 = new Func<CompositionDiagramElement.DiagramSocketEx, DiagramElement>( CompositionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzlG443prdoWRf5yF07V9_6YA\u003D) ) ).Where<DiagramElement>( CompositionDiagramElement.LamdaShit.Func009 ?? ( CompositionDiagramElement.LamdaShit.Func009 = new Func<DiagramElement, bool>( CompositionDiagramElement.LamdaShit._lamdaShit.\u0023\u003Dz6Fukn0Jc4Dx5gwI_oFE9yLk\u003D) ) ).Distinct<DiagramElement>().ToArray<DiagramElement>();
}

private void \u0023\u003DznyundRwXqVjW( DiagramSocketValue _param1 )
    {
    CompositionDiagramElement.\u0023\u003DzK_k2NFG29BxdvlvbqLcogpI\u003D g29BxdvlvbqLcogpI = new CompositionDiagramElement.\u0023\u003DzK_k2NFG29BxdvlvbqLcogpI\u003D() { _dsv = _param1 };
    g29BxdvlvbqLcogpI.\u0023\u003DznGzHvo4\u003D = this.Model?.GetElementKey( g29BxdvlvbqLcogpI._dsv.Socket.Parent );
    CompositionDiagramElement.DiagramSocketEx zrmiMtHt09McF = this.OutputSockets.OfType<CompositionDiagramElement.DiagramSocketEx>().FirstOrDefault<CompositionDiagramElement.DiagramSocketEx>( new Func<CompositionDiagramElement.DiagramSocketEx, bool>( g29BxdvlvbqLcogpI.\u0023\u003DzbtZvIgB8wcd_NCW7Jw\u003D\u003D) );
    if ( zrmiMtHt09McF == null )
        return;
    this.RaiseProcessOutput( ( DiagramSocket )zrmiMtHt09McF, g29BxdvlvbqLcogpI._dsv.Time, g29BxdvlvbqLcogpI._dsv.Value, g29BxdvlvbqLcogpI._dsv, ( Subscription )null );
}

/// <summary>
/// This predicate is true when one can call <see cref="M:StockSharp.Diagram.CompositionDiagramElement.Undo" />.
/// </summary>
/// <returns>Check result.</returns>
public bool CanUndo()
{
    if ( this.Model != null && this.Model.UndoManager != null )
        return this.Model.UndoManager.CanUndo();
    return false;
}

/// <summary>
/// Restore the state of some models to before the current state.
/// </summary>
public void Undo()
{
    this.Model?.UndoManager?.Undo();
}

/// <summary>
/// This predicate is true when one can call <see cref="M:StockSharp.Diagram.CompositionDiagramElement.Redo" />.
/// </summary>
/// <returns>Check result.</returns>
public bool CanRedo()
{
    if ( this.Model != null && this.Model.UndoManager != null )
        return this.Model.UndoManager.CanRedo();
    return false;
}

/// <summary>
/// Restore the state of some models to after the current state.
/// </summary>
public void Redo()
{
    this.Model?.UndoManager?.Redo();
}

/// <summary>Find all portfolios in elements.</summary>
/// <returns>
/// </returns>
public IEnumerable<Portfolio> FindPortfolios()
{
    return this.Elements.OfType<VariableDiagramElement>().Where<VariableDiagramElement>( CompositionDiagramElement.LamdaShit.Func010 ?? ( CompositionDiagramElement.LamdaShit.Func010 = new Func<VariableDiagramElement, bool>( CompositionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzmNm4PSpNQ_xto0gURoJhTe\u0024eG53fKoVoGw\u003D\u003D) ) ).Select<VariableDiagramElement, object>( CompositionDiagramElement.LamdaShit.Func011 ?? ( CompositionDiagramElement.LamdaShit.Func011 = new Func<VariableDiagramElement, object>( CompositionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzBN8AVPSYtq9fkOBy7FzaJwLTiZa6eqEldA\u003D\u003D) ) ).OfType<Portfolio>().Concat<Portfolio>( this.Elements.OfType<CompositionDiagramElement>().SelectMany<CompositionDiagramElement, Portfolio>( CompositionDiagramElement.LamdaShit.Func012 ?? ( CompositionDiagramElement.LamdaShit.Func012 = new Func<CompositionDiagramElement, IEnumerable<Portfolio>>( CompositionDiagramElement.LamdaShit._lamdaShit.\u0023\u003DzZ8b4NH3Bzv2Mrk0OVcBFoTn33EyroX8T0g\u003D\u003D)))).Distinct<Portfolio>();
    }

    private void \u0023\u003DzXSezL7EUpNUc(List<ValueTuple<Guid, Guid>> _param1, Guid? _param2)
    {
      CompositionDiagramElement.\u0023\u003DzSUQeI2YLmehxlyGnkviMUck\u003D ylmehxlyGnkviMuck = new CompositionDiagramElement.\u0023\u003DzSUQeI2YLmehxlyGnkviMUck\u003D();
      ylmehxlyGnkviMuck._diagramElement = this;
      ylmehxlyGnkviMuck.\u0023\u003DzbuSfVBo\u003D = _param1;
      ValueTuple<Guid, Guid> valueTuple = ylmehxlyGnkviMuck.\u0023\u003DzbuSfVBo\u003D.FirstOrDefault<ValueTuple<Guid, Guid>>(new Func<ValueTuple<Guid, Guid>, bool>(ylmehxlyGnkviMuck.\u0023\u003Dztte9z4\u0024xJ1N0CfXEkQ\u003D\u003D));
      int num = !valueTuple.IsDefault<ValueTuple<Guid, Guid>>() ? 1 : 0;
      Guid typeId = num != 0 ? valueTuple.Item2 : _param2 ?? Guid.NewGuid();
      if (_param2.HasValue)
      {
        Guid? nullable = _param2;
        Guid guid = typeId;
        if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != guid ? 1 : 0) : 0) : 1) != 0)
          throw new InvalidOperationException(nameof(-1260198466));
      }
      if (num == 0)
        ylmehxlyGnkviMuck.\u0023\u003DzbuSfVBo\u003D.Add(new ValueTuple<Guid, Guid>(this.TypeId, typeId));
      this.SetTypeId(typeId);
      this.Elements.OfType<CompositionDiagramElement>().ForEach<CompositionDiagramElement>(new Action<CompositionDiagramElement>(ylmehxlyGnkviMuck.\u0023\u003Dze6mGEtFyDQYQqR6UNg\u003D\u003D));
    }

    /// <summary>
    /// Update <see cref="P:StockSharp.Diagram.DiagramElement.TypeId" /> for composition elements.
    /// </summary>
    /// <param name="id">New value for <see cref="P:StockSharp.Diagram.DiagramElement.TypeId" />. Can be <see langword="null" />.</param>
    public void UpdateTypeId(Guid? id = null)
    {
      this.\u0023\u003DzXSezL7EUpNUc(new List<ValueTuple<Guid, Guid>>(), id);
    }

    /// <summary>
    /// Create a copy of <see cref="T:StockSharp.Diagram.CompositionDiagramElement" />.
    /// </summary>
    /// <param name="cloneSockets">To create copies of connections.</param>
    /// <returns>Copy.</returns>
    public override DiagramElement Clone(bool cloneSockets = true)
    {
      CompositionDiagramElement compositionDiagramElement = new CompositionDiagramElement(this.Model.Clone());
      compositionDiagramElement.Category = this.Category;
      compositionDiagramElement.Description = this.Description;
      compositionDiagramElement.DocUrl = this.DocUrl;
      SettingsStorage storage = this.Save();
      if (!cloneSockets)
      {
        storage.Remove(nameof(-1260198520));
        storage.Remove(nameof(-1260198505));
      }
      compositionDiagramElement.Load(storage);
      return (DiagramElement) compositionDiagramElement;
    }

    private void \u0023\u003Dz_zIXfnS8hYmr7z4lk5OuGkc\u003D(DiagramElement _param1)
    {
      _param1.Init((ILogSource) this);
    }

    internal static ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int> DontKnowFunction(
      string _param0,
      DiagramSocket _param1)
    {
      return new ValueTuple<string, DiagramSocketDirection, string, DiagramSocketType, int>(_param0, _param1 != null ? _param1.Directon : DiagramSocketDirection.In, _param1?.Id, _param1?.Type, _param1 != null ? _param1.LinkableMaximum : 0);
    }

    private void \u0023\u003DzmTaYAc6h6Qqbh2lDVQb7QsE\u003D(DiagramElement _param1)
    {
      _param1.ProcessOutput += new Action<DiagramSocketValue>(this.\u0023\u003DznyundRwXqVjW);
    }

    private void \u0023\u003DzekIpOCiF4OUtAOpaIfsOL2EWHrkY(DiagramElement _param1)
    {
      _param1.ProcessOutput -= new Action<DiagramSocketValue>(this.\u0023\u003DznyundRwXqVjW);
    }

    

    

    private sealed class \u0023\u003DzK_k2NFG29BxdvlvbqLcogpI\u003D
    {
      public string \u0023\u003DznGzHvo4\u003D;
      public DiagramSocketValue _dsv;

      internal bool \u0023\u003DzbtZvIgB8wcd_NCW7Jw\u003D\u003D(
        CompositionDiagramElement.DiagramSocketEx _param1)
      {
        if (_param1.MyStringOne().EqualsIgnoreCase(this.\u0023\u003DznGzHvo4\u003D))
          return _param1.MyStringTwo() == this._dsv.Socket.Id;
        return false;
      }
    }

    private sealed class \u0023\u003DzSUQeI2YLmehxlyGnkviMUck\u003D
    {
      public CompositionDiagramElement _diagramElement;
      public List<ValueTuple<Guid, Guid>> \u0023\u003DzbuSfVBo\u003D;

      internal bool \u0023\u003Dztte9z4\u0024xJ1N0CfXEkQ\u003D\u003D(ValueTuple<Guid, Guid> _param1)
      {
        return _param1.Item1 == this._diagramElement.TypeId;
      }

      internal void \u0023\u003Dze6mGEtFyDQYQqR6UNg\u003D\u003D(CompositionDiagramElement _param1)
      {
        _param1.\u0023\u003DzXSezL7EUpNUc(this.\u0023\u003DzbuSfVBo\u003D, new Guid?());
      }
    }

    

    

    

    
}
