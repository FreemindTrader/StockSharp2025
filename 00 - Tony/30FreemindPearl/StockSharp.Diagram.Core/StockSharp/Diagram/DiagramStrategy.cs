// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.DiagramStrategy
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Risk;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockSharp.Diagram
{
    /// <summary>
    /// The strategy whose algorithm is presented in the form of a diagram.
    /// </summary>
    public class DiagramStrategy : Strategy, ICustomTypeDescriptor, INotifyPropertiesChanged
    {

        private readonly List<IDiagramElementParam> _diagramElementParams = new List<IDiagramElementParam>();

        private IEnumerable<IRiskRule> _riskRules = Enumerable.Empty<IRiskRule>();

        private int _overflowLimit = 1000;

        private readonly HashSet<DiagramElement> _diagramElements = new HashSet<DiagramElement>();

        private readonly HashSet<DiagramSocket> _diagramSockets = new HashSet<DiagramSocket>();

        private CompositionDiagramElement _composition;

        private SettingsStorage _settingStorage;

        private bool _useStrategyParameterValues;

        /// <summary>The strategy diagram.</summary>
        public CompositionDiagramElement Composition
        {
            get
            {
                return this._composition;
            }
            set
            {
                if ( this._composition == value )
                    return;
                this.Uninitialized();
                this._composition = value;
                if ( this._composition == null )
                    return;
                this.Initialized();
            }
        }

        /// <summary>
        /// Use strategy parameters values for composition properties.
        /// </summary>
        [Browsable( false )]
        public bool UseStrategyParameterValues
        {
            get
            {
                return this._useStrategyParameterValues;
            }
            set
            {
                this._useStrategyParameterValues = value;
            }
        }

        /// <summary>The risk rules.</summary>
        [Display( Description = "RiskSettings", GroupName = "Settings", Name = "XamlStr613", Order = 300, ResourceType = typeof( LocalizedStrings ) )]
        public IEnumerable<IRiskRule> RiskRules
        {
            get
            {
                return this._riskRules;
            }
            set
            {
                this._riskRules = value;
                this.RaiseParametersChanged( nameof( RiskRules ) );
            }
        }

        /// <summary>
        /// Max allowed elements per iteration to prevent stack overflow.
        /// </summary>
        [Display( Description = "OverflowLimit", GroupName = "Settings", Name = "Overflow", Order = 301, ResourceType = typeof( LocalizedStrings ) )]
        public int OverflowLimit
        {
            get
            {
                return this._overflowLimit;
            }
            set
            {
                if ( value < 1 )
                    throw new ArgumentOutOfRangeException( "OverflowLimit < 1" );
                this._overflowLimit = value;
            }
        }

        /// <summary>The strategy diagram change event.</summary>
        public event Action<CompositionDiagramElement> CompositionChanged;

        private void Initialized()
        {
            if ( this._settingStorage != null )
                this._composition.Load( this._settingStorage );
            this._composition.Changed += new Action( this.OnCompositionChanged );
            this._composition.PropertiesChanged += new Action( this.RaisePropertiesChanged );
            this._composition.CanAutoName = false;
            this._composition.Strategy = this;
            this._composition.Init( ( ILogSource )this );
            this.SelectParameters().ForEach<IDiagramElementParam>( new Action<IDiagramElementParam>( this.\u0023\u003DzyhSjV0Wmz\u0024PT7ZPDCaBadK4\u003D) );
            Action<CompositionDiagramElement> zpDemcRi = this.\u0023\u003DzpDEmcRI\u003D;
            if ( zpDemcRi == null )
                return;
            zpDemcRi( this._composition );
        }

        private void Uninitialized()
        {
            if ( this._composition == null )
                return;
            this.SelectParameters().ForEach<IDiagramElementParam>( new Action<IDiagramElementParam>( this.\u0023\u003DzBsp2WnybUyoHP43wNQSRyRc\u003D) );
            this._composition.UnInit();
            this._composition.Parent = ( ILogSource )null;
            this._settingStorage = this._composition.Save();
            this._composition.Changed -= new Action( this.OnCompositionChanged );
            this._composition.PropertiesChanged -= new Action( this.RaisePropertiesChanged );
            this._composition = ( CompositionDiagramElement )null;
        }

        private void OnCompositionChanged()
        {
            this.RaiseParametersChanged( nameof( -1260198317 ) );
            this.RaisePropertiesChanged();
        }

        private IEnumerable<IDiagramElementParam> SelectParameters()
        {
            return this._composition.Parameters.Where<IDiagramElementParam>( DiagramStrategy.LamdaShit.\u0023\u003Dz\u0024QwiOfJP2JrdNjU2IQ\u003D\u003D ?? ( DiagramStrategy.LamdaShit.\u0023\u003Dz\u0024QwiOfJP2JrdNjU2IQ\u003D\u003D = new Func<IDiagramElementParam, bool>( DiagramStrategy.LamdaShit._lamdaShit.\u0023\u003DzdoqS0yMjKnDerzxTCPNZEww\u003D) ));
        }

        /// <inheritdoc />
        protected override void OnStarted()
        {
            if ( this._composition == null )
                throw new InvalidOperationException( LocalizedStrings.DiagramNotSet );
            if ( this._composition.HasErrors )
                throw new InvalidOperationException( LocalizedStrings.DiagramContainsErrors );
            this.Composition.SuspendUndoManager();
            this.RiskManager.Rules.Clear();
            this.RiskManager.Rules.AddRange( this.RiskRules.Select<IRiskRule, IRiskRule>( DiagramStrategy.LamdaShit.\u0023\u003DzDn8LXxH59b_lAsaWOw\u003D\u003D ?? ( DiagramStrategy.LamdaShit.\u0023\u003DzDn8LXxH59b_lAsaWOw\u003D\u003D = new Func<IRiskRule, IRiskRule>( DiagramStrategy.LamdaShit._lamdaShit.\u0023\u003Dz0OTXahL0DXjzAJ3cFKqwFPY\u003D) ) ));
            try
            {
                this.Method01();
            }
            catch ( object ex )
            {
                this.Method02();
                throw;
            }
            if ( this.UseStrategyParameterValues )
            {
                foreach ( IDiagramElementParam diagramElementParam in this.SelectParameters())
        {
                    IStrategyParam strategyParam = this.Parameters.TryGetValue<string, IStrategyParam>( diagramElementParam.Name );
                    if ( strategyParam != null && strategyParam.Value != null )
                        diagramElementParam.Value = strategyParam.Value;
                }
            }
            this.SuspendRules( new Action( this.\u0023\u003DzRo_Zghg6iNXziSB9YDeDJ9o\u003D) );
            base.OnStarted();
        }

        /// <inheritdoc />
        protected override void OnStopped()
        {
            this.Method04();
            this.Method05();
            this.Method06();
            base.OnStopped();
        }

        private void Method01()
        {
            this._composition.Parameters.ForEach<IDiagramElementParam>( new Action<IDiagramElementParam>( this.\u0023\u003DzBHU38fVqFR1j3VqPGlEJpck\u003D) );
        }

        private void Method02()
        {
            ( ( IEnumerable<IDiagramElementParam> )this._diagramElementParams.CopyAndClear<IDiagramElementParam>() ).ForEach<IDiagramElementParam>( DiagramStrategy.LamdaShit.\u0023\u003Dzw_lAJuljRaKBPhiOSw\u003D\u003D ?? ( DiagramStrategy.LamdaShit.\u0023\u003Dzw_lAJuljRaKBPhiOSw\u003D\u003D = new Action<IDiagramElementParam>( DiagramStrategy.LamdaShit._lamdaShit.\u0023\u003Dzno7Q302kLAZt30fKDwKQgLI\u003D) ));
        }

        private void Method03()
        {
            this._composition.Start();
        }

        private void Method04()
        {
            this._composition.Stop();
            this.Method02();
            this.Composition.ResumeUndoManager();
        }

        /// <summary>Flush non trigger (root) elements.</summary>
        public void Flush()
        {
            this.Composition.Flush();
            this.Method05();
            this.Method06();
        }

        private void Method06()
        {
            foreach ( DiagramSocket diagramSocket in this._diagramSockets )
                diagramSocket.Value = ( object )null;
            this._diagramSockets.Clear();
        }

        private void Method05()
        {
            foreach ( DiagramElement diagramElement in this._diagramElements )
                diagramElement.ClearSocketValues();
            this._diagramElements.Clear();
        }

        internal void AddRemoveSocket( DiagramElement _param1, bool _param2 )
        {
            if ( _param1 == null )
                throw new ArgumentNullException( nameof( -1260196435 ) );
            if ( _param2 )
                this._diagramElements.Add( _param1 );
            else
                this._diagramElements.Remove( _param1 );
        }

        internal void AddRemoveSocket( DiagramSocket socket, bool isAdded )
        {
            if ( socket == null )
                throw new ArgumentNullException( "socket == null");
            if ( isAdded )
                this._diagramSockets.Add( socket );
            else
                this._diagramSockets.Remove( socket );
        }

        /// <inheritdoc />
        protected override void OnReseted()
        {
            if ( this._composition != null && !this._composition.HasErrors )
                this._composition.Reset();
            base.OnReseted();
        }

        /// <inheritdoc />
        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            if ( this._composition != null )
                storage.SetValue<SettingsStorage>( nameof( -1260196425 ), this._settingStorage = this._composition.Save() );
            storage.SetValue<SettingsStorage[ ]>( nameof( -1260196410 ), this.RiskRules.Select<IRiskRule, SettingsStorage>( DiagramStrategy.LamdaShit.\u0023\u003DzX_JsuGovqy_xCOvxjg\u003D\u003D ?? ( DiagramStrategy.LamdaShit.\u0023\u003DzX_JsuGovqy_xCOvxjg\u003D\u003D = new Func<IRiskRule, SettingsStorage>( DiagramStrategy.LamdaShit._lamdaShit.\u0023\u003DzwzOWMw_UE3osS1sBwQ\u003D\u003D) ) ).ToArray<SettingsStorage>());
            storage.SetValue<int>( nameof( -1260196451 ), this.OverflowLimit );
        }

        /// <inheritdoc />
        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this._settingStorage = storage.GetValue<SettingsStorage>( nameof( -1260196425 ), ( SettingsStorage )null );
            if ( this._composition != null && this._settingStorage != null )
                this._composition.Load( this._settingStorage );
            SettingsStorage[ ] settingsStorageArray = storage.GetValue<SettingsStorage[ ]>( nameof( -1260196410 ), ( SettingsStorage[ ] )null );
            if ( settingsStorageArray != null )
                this.RiskRules = ( IEnumerable<IRiskRule> )( ( IEnumerable<SettingsStorage> )settingsStorageArray ).Select<SettingsStorage, IRiskRule>( DiagramStrategy.LamdaShit.\u0023\u003DzcZspeb942UmdRUVUIQ\u003D\u003D ?? ( DiagramStrategy.LamdaShit.\u0023\u003DzcZspeb942UmdRUVUIQ\u003D\u003D = new Func<SettingsStorage, IRiskRule>( DiagramStrategy.LamdaShit._lamdaShit.\u0023\u003DzrymIxmcSku3uBNXdFg\u003D\u003D) )).ToArray<IRiskRule>();
            this.OverflowLimit = storage.GetValue<int>( nameof( -1260196451 ), this.OverflowLimit );
        }

        /// <summary>
        /// Create a copy of <see cref="T:StockSharp.Diagram.DiagramStrategy" />.
        /// </summary>
        /// <returns>Copy.</returns>
        public override Strategy Clone()
        {
            DiagramStrategy diagramStrategy = ( DiagramStrategy )base.Clone();
            diagramStrategy.Composition = ( CompositionDiagramElement )this.Composition.Clone( true );
            return ( Strategy )diagramStrategy;
        }

        private PropertyDescriptorCollection CreatePropertyDescriptorCollection()
        {
            IEnumerable<PropertyDescriptor> propertyDescriptors = TypeDescriptor.GetProperties( ( object )this, true ).Cast<PropertyDescriptor>();
            ICustomTypeDescriptor zeq1j1w = ( ICustomTypeDescriptor )this._composition;
            if ( zeq1j1w != null )
                propertyDescriptors = propertyDescriptors.Concat<PropertyDescriptor>( zeq1j1w.GetProperties().Cast<PropertyDescriptor>() );
            return new PropertyDescriptorCollection( propertyDescriptors.Where<PropertyDescriptor>( new Func<PropertyDescriptor, bool>( this.NeedShowProperty ) ).ToArray<PropertyDescriptor>() );
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes( ( object )this, true );
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName( ( object )this, true );
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName( ( object )this, true );
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter( ( object )this, true );
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent( ( object )this, true );
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty( ( object )this, true );
        }

        object ICustomTypeDescriptor.GetEditor( Type _param1 )
        {
            return TypeDescriptor.GetEditor( ( object )this, _param1, true );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents( Attribute[ ] _param1 )
        {
            return TypeDescriptor.GetEvents( ( object )this, _param1, true );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents( ( object )this, true );
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( Attribute[ ] _param1 )
        {
            return this.CreatePropertyDescriptorCollection();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return this.CreatePropertyDescriptorCollection();
        }

        object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor _param1 )
        {
            return ( object )this;
        }

        /// <summary>
        /// It returns <see langword="true" />, if the property is to be displayed in the settings.
        /// </summary>
        /// <param name="propertyDescriptor">The property description.</param>
        /// <returns>
        /// <see langword="true" />, if necessary to show the property, otherwise <see langword="false" />.</returns>
        protected virtual bool NeedShowProperty( PropertyDescriptor propertyDescriptor )
        {
            DisplayAttribute displayAttribute = propertyDescriptor.Attributes.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
            string str = displayAttribute != null ? displayAttribute.GetGroupName() : propertyDescriptor.Category;
            if ( str != LocalizedStrings.Str436 && str != LocalizedStrings.Common && str != LocalizedStrings.Composition )
                return str != LocalizedStrings.General;
            return false;
        }

        /// <summary>The available properties change event.</summary>
        public event Action PropertiesChanged;

        /// <summary>To call the available properties change event.</summary>
        protected virtual void RaisePropertiesChanged()
        {
            Action zJnbNyrs = this.\u0023\u003DzJNbNyrs\u003D;
            if ( zJnbNyrs == null )
                return;
            zJnbNyrs();
        }

        private void \u0023\u003DzyhSjV0Wmz\u0024PT7ZPDCaBadK4\u003D(IDiagramElementParam _param1)
    {
      StrategyParam strategyParam = new StrategyParam( ( Strategy )this, _param1.Name, _param1.DisplayName, _param1.Type, _param1.Value );
    }

    private void \u0023\u003DzBsp2WnybUyoHP43wNQSRyRc\u003D(IDiagramElementParam _param1)
    {
      this.Parameters.Remove(_param1.Name);
    }

    private void \u0023\u003DzRo_Zghg6iNXziSB9YDeDJ9o\u003D()
    {
      try
      {
        this.Method03();
    }
      catch (Exception ex)
      {
        this.Method04();
        throw;
    }
}

private void \u0023\u003DzBHU38fVqFR1j3VqPGlEJpck\u003D( IDiagramElementParam _param1)
    {
    if ( _param1.Type == typeof( Security ) && _param1.Value == null )
    {
        if ( this.Security == null )
            throw new InvalidOperationException( LocalizedStrings.Str1380 );
        this._diagramElementParams.Add( _param1 );
        _param1.SetValueWithIgnoreOnSave( ( object )this.Security );
    }
    else
    {
        if ( !( _param1.Type == typeof( Portfolio ) ) || _param1.Value != null )
            return;
        if ( this.Portfolio == null )
            throw new InvalidOperationException( LocalizedStrings.Str1381 );
        this._diagramElementParams.Add( _param1 );
        _param1.SetValueWithIgnoreOnSave( ( object )this.Portfolio );
    }
}

[Serializable]
private sealed class LamdaShit
{
    public static readonly DiagramStrategy.LamdaShit _lamdaShit = new DiagramStrategy.LamdaShit();
    public static Func<IDiagramElementParam, bool> \u0023\u003Dz\u0024QwiOfJP2JrdNjU2IQ\u003D\u003D;
      public static Func<IRiskRule, IRiskRule> \u0023\u003DzDn8LXxH59b_lAsaWOw\u003D\u003D;
      public static Action<IDiagramElementParam> \u0023\u003Dzw_lAJuljRaKBPhiOSw\u003D\u003D;
      public static Func<IRiskRule, SettingsStorage> \u0023\u003DzX_JsuGovqy_xCOvxjg\u003D\u003D;
      public static Func<SettingsStorage, IRiskRule> \u0023\u003DzcZspeb942UmdRUVUIQ\u003D\u003D;

      internal bool \u0023\u003DzdoqS0yMjKnDerzxTCPNZEww\u003D(IDiagramElementParam _param1)
      {
        if (_param1.IsParam)
          return _param1.Type.CanOptimize();
        return false;
      }

internal IRiskRule \u0023\u003Dz0OTXahL0DXjzAJ3cFKqwFPY\u003D( IRiskRule _param1)
      {
    return _param1.Clone<IRiskRule>();
}

internal void \u0023\u003Dzno7Q302kLAZt30fKDwKQgLI\u003D(IDiagramElementParam _param1)
      {
        _param1.Value = (object) null;
      }

      internal SettingsStorage \u0023\u003DzwzOWMw_UE3osS1sBwQ\u003D\u003D(
        IRiskRule _param1)
      {
        return _param1.SaveEntire(false);
      }

      internal IRiskRule \u0023\u003DzrymIxmcSku3uBNXdFg\u003D\u003D(
        SettingsStorage _param1)
      {
        return _param1.LoadEntire<IRiskRule>();
      }
    }
  }
}
