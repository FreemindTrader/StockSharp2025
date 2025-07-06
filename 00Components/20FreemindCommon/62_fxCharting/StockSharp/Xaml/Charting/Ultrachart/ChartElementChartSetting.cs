

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Xaml.Charting.Ultrachart;
using StockSharp.Xaml.Charting;
using StockSharp.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using DevExpress.Utils;


//internal sealed class ChartComponentChartSettings : ChartSettingsObjectBase<IChartComponent>
//{

//    private readonly Func<IChartComponent, PropertyDescriptor, bool> _propertyDescSelector;

//    public ChartComponentChartSettings( IChartComponent chartCom, Func<IChartComponent, PropertyDescriptor, bool> selector = null )
//      : base( chartCom )
//    {
//        this._propertyDescSelector = selector;
//        this.Orig.PropertyChanged += new PropertyChangedEventHandler( this.OnPropertyChanged );
//    }

//    public static PropertyDescriptor Create( string name, object thisObject, IChartComponent component, Func<IChartComponent, PropertyDescriptor, bool> selector )
//    {
//        return new ProxyDescriptorEx( name, thisObject, component, selector );
//    }

//    private sealed class ProxyDescriptorEx( string name, object thisObject, IChartComponent component, Func<IChartComponent, PropertyDescriptor, bool> selector ) : ProxyDescriptor( name, thisObject, component, Enumerable.Cast<Attribute>( TypeDescriptor.GetAttributes( component, false ) ), selector )
//    {
//        protected override ChartSettingsObjectBase<IChartComponent> CreateWrapper( IChartComponent component, Func<IChartComponent, PropertyDescriptor, bool> selector = null )
//        {
//            return new ChartComponentChartSettings( component, selector );
//        }
//    }

//    protected override PropertyDescriptor[ ] OnGetProperties( IChartComponent element )
//    {
//        if ( element == null )
//            return null;

//        var pdList = new List<PropertyDescriptor>();

//        ChartComponentChartSettings.UniquePropertyNameHelper vbxLeArTkallkIdHg = new ChartComponentChartSettings.UniquePropertyNameHelper();
//        vbxLeArTkallkIdHg._ChartComponentChartSettings001 = this;
//        vbxLeArTkallkIdHg._IChartComponent001 = element;
//        vbxLeArTkallkIdHg._hashsetstring001 = new HashSet<string>();


//        pdList.AddRange( TypeDescriptor.GetProperties( element, false ).OfType<PropertyDescriptor>().Where( p =>
//        {
//            if(_propertyDescSelector == null || !_propertyDescSelector( element, p ) )
//                return false;            

//            return element == null || !element.AdditionalName( p.Name );

//        } ).SelectMany<PropertyDescriptor, PropertyDescriptor>( s =>
//        {
//            object myProperties = s.GetValue( element );

//            ChartComponentChartSettings.SomeSealClass0365 d295Ww4skLs1HZBq = new ChartComponentChartSettings.SomeSealClass0365();
//            d295Ww4skLs1HZBq.SomeMoreMEthod34313413413 = this;
//            IEnumerable<PropertyDescriptor> propertyDescriptors;

//            if ( !( myProperties is IChartComponent myChartComponentProp ) )
//            {
//                d295Ww4skLs1HZBq._IChartIndicatorPainter = myProperties as StockSharp.Charting.IChartIndicatorPainter;
//                propertyDescriptors = d295Ww4skLs1HZBq._IChartIndicatorPainter != null ? TypeDescriptor.GetProperties( d295Ww4skLs1HZBq._IChartIndicatorPainter, false ).OfType<PropertyDescriptor>().Where<PropertyDescriptor>( ChartComponentChartSettings.SomeSealClass034._Func_PropertyDescriptor_bool ?? ( ChartComponentChartSettings.SomeSealClass034._Func_PropertyDescriptor_bool = new Func<PropertyDescriptor, bool>( ChartComponentChartSettings.SomeSealClass034._instanceMemember2343.PropertyDescriptorMethod00555 ) ) ).Select<PropertyDescriptor, PropertyDescriptor>( new Func<PropertyDescriptor, PropertyDescriptor>( d295Ww4skLs1HZBq.PropertyDescriptorMethod003 ) ) : ( IEnumerable<PropertyDescriptor> ) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D< PropertyDescriptor > ( s );
//            }
//            else
//            {
//                propertyDescriptors = new List<PropertyDescriptor>( ChartComponentChartSettings.Create( this._someStringMethod001( element?.GetName( myChartComponentProp ) ?? Extensions.GetDisplayName( s, null ) ), element, myChartComponentProp, _propertyDescSelector ) );
//            }

//            return propertyDescriptors;

//        } ));

//        return pdList.ToArray();
//    }

//    private sealed class SomeSealClass0365
//    {
//        public StockSharp.Charting.IChartIndicatorPainter _IChartIndicatorPainter;
//        public ChartComponentChartSettings.UniquePropertyNameHelper SomeMoreMEthod34313413413;

//        internal PropertyDescriptor PropertyDescriptorMethod003(
//          PropertyDescriptor _param1 )
//        {
//            return !TypeHelper.Is<StockSharp.Charting.IChartElement>( _param1.PropertyType, true ) ? _param1 : ChartComponentChartSettings.Create( this.SomeMoreMEthod34313413413._someStringMethod001( Extensions.GetDisplayName( _param1, ( string ) null ) ), this._IChartIndicatorPainter, ( IChartComponent ) _param1.GetValue( this._IChartIndicatorPainter ), this.SomeMoreMEthod34313413413._ChartComponentChartSettings001._propertyDescSelector );
//        }
//    }


//    private void OnPropertyChanged( object? sender, PropertyChangedEventArgs e )
//    {
//        this.NotifyChanged( e.PropertyName );
//    }

//    private sealed class UniquePropertyNameHelper
//    {
//        public HashSet<string> _hashsetstring001;
//        public ChartComponentChartSettings _ChartComponentChartSettings001;
//        public IChartComponent _IChartComponent001;

//        internal string _someStringMethod001( string s )
//        {
//            string str = s;
//            int num = 0;
//            while ( !this._hashsetstring001.Add( str ) )
//                str = s + ( ++num ).ToString();
//            return str;
//        }

//        internal bool _someStringMethod002( PropertyDescriptor p )
//        {
//            Func<IChartComponent, PropertyDescriptor, bool> _someInstance0034 = this._ChartComponentChartSettings001._propertyDescSelector;
//            if ( ( _someInstance0034 != null ? ( _someInstance0034( this._IChartComponent001, p ) ? 1 : 0 ) : 1 ) == 0 )
//                return false;
//            IChartComponent zLiCojrU = this._IChartComponent001;
//            return zLiCojrU == null || !zLiCojrU.AdditionalName( p.Name );
//        }

//        internal IEnumerable<PropertyDescriptor> SomeMethod03413( PropertyDescriptor _param1 )
//        {
//            object obj = _param1.GetValue((object) this._IChartComponent001);
//            ChartComponentChartSettings.SomeSealClass0365 d295Ww4skLs1HZBq = new ChartComponentChartSettings.SomeSealClass0365();
//            d295Ww4skLs1HZBq.SomeMoreMEthod34313413413 = this;
//            IEnumerable<PropertyDescriptor> propertyDescriptors;
//            if ( !( obj is IChartComponent myChartComponentProp ) )
//            {
//                d295Ww4skLs1HZBq._IChartIndicatorPainter = obj as StockSharp.Charting.IChartIndicatorPainter;
//                propertyDescriptors = d295Ww4skLs1HZBq._IChartIndicatorPainter != null ? TypeDescriptor.GetProperties( d295Ww4skLs1HZBq._IChartIndicatorPainter, false ).OfType<PropertyDescriptor>().Where<PropertyDescriptor>( ChartComponentChartSettings.SomeSealClass034._Func_PropertyDescriptor_bool ?? ( ChartComponentChartSettings.SomeSealClass034._Func_PropertyDescriptor_bool = new Func<PropertyDescriptor, bool>( ChartComponentChartSettings.SomeSealClass034._instanceMemember2343.PropertyDescriptorMethod00555) )).Select<PropertyDescriptor, PropertyDescriptor>( new Func<PropertyDescriptor, PropertyDescriptor>( d295Ww4skLs1HZBq.PropertyDescriptorMethod003) ) : ( IEnumerable<PropertyDescriptor> ) new List< PropertyDescriptor > ( s );
//            }
//            else
//                propertyDescriptors = ( IEnumerable<PropertyDescriptor> ) new List< PropertyDescriptor > ( ChartComponentChartSettings.Create( this._someStringMethod001( this._IChartComponent001?.GetName(  myChartComponentProp ) ?? Extensions.GetDisplayName( _param1, ( string ) null ) ), this._IChartComponent001, myChartComponentProp, this._ChartComponentChartSettings001._propertyDescSelector ) );
//            return propertyDescriptors;
//        }
//    }

//    [Serializable]
//    private sealed class SomeSealClass034
//    {
//        public static readonly ChartComponentChartSettings.SomeSealClass034 _instanceMemember2343 = new ChartComponentChartSettings.SomeSealClass034();
//    public static Func<PropertyDescriptor, bool> _Func_PropertyDescriptor_bool;

//    internal bool PropertyDescriptorMethod00555(PropertyDescriptor _param1)
//    {
//      BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
//      return browsableAttribute == null || browsableAttribute.Browsable;
//    }
//}



//}


internal sealed class ChartComponentChartSettings : ChartSettingsObjectBase<IChartComponent>
{
    //    private sealed class ProxyDescriptorEx( string name, object thisObject, IChartComponent component, Func<IChartComponent, PropertyDescriptor, bool> selector ) : ProxyDescriptor( name, thisObject, component, Enumerable.Cast<Attribute>( TypeDescriptor.GetAttributes( component, false ) ), selector )
    private sealed class ProxyDescriptorEx( string name, object thisObject, IChartComponent component, Func<IChartComponent, PropertyDescriptor, bool> selector ) : ProxyDescriptor( name, thisObject, component, Enumerable.Cast<Attribute>( TypeDescriptor.GetAttributes( component, false ) ), selector )
    {
        protected override ChartSettingsObjectBase<IChartComponent> CreateWrapper( IChartComponent com, Func<IChartComponent, PropertyDescriptor, bool> selector = null )
        {
            return ( ChartSettingsObjectBase<IChartComponent> ) new ChartComponentChartSettings( com, selector );
        }
    }
    private readonly Func<IChartComponent, PropertyDescriptor, bool> _propertyDescSelector;

    public ChartComponentChartSettings( IChartComponent com, Func<IChartComponent, PropertyDescriptor, bool> selector = null )
      : base( com )
    {
        this._propertyDescSelector = selector;
        this.Orig.PropertyChanged += new PropertyChangedEventHandler( this.OnPropertyChanged );
    }

    public static PropertyDescriptor Create( string name, object thisobject, IChartComponent com, Func<IChartComponent, PropertyDescriptor, bool> selector )
    {
        return ( PropertyDescriptor ) new ChartComponentChartSettings.ProxyDescriptorEx( name, thisobject, com, selector );
    }

    protected override PropertyDescriptor[ ] OnGetProperties(
      IChartComponent _param1 )
    {
        ChartComponentChartSettings.UniquePropertyNameHelper helper = new ChartComponentChartSettings.UniquePropertyNameHelper();
        helper._settings = this;
        helper._component = _param1;
        helper._hashSet = new HashSet<string>();
        if ( helper._component == null )
            return ( PropertyDescriptor[ ] ) null;
        List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
        propertyDescriptorList.AddRange( TypeDescriptor.GetProperties( helper._component, false ).OfType<PropertyDescriptor>().Where<PropertyDescriptor>( new Func<PropertyDescriptor, bool>( helper.AddAdditionalName ) ).SelectMany<PropertyDescriptor, PropertyDescriptor>( new Func<PropertyDescriptor, IEnumerable<PropertyDescriptor>>( helper.SelectManyConditions ) ) );
        return propertyDescriptorList.ToArray();
    }

    private void OnPropertyChanged( object? _param1, PropertyChangedEventArgs _param2 )
    {
        this.NotifyChanged( _param2.PropertyName );
    }

    private sealed class UniquePropertyNameHelper
    {
        public HashSet<string> _hashSet;
        public ChartComponentChartSettings _settings;
        public IChartComponent _component;

        internal string CreateName( string _param1 )
        {
            string str = _param1;
            int num = 0;
            while ( !this._hashSet.Add( str ) )
                str = _param1 + ( ++num ).ToString();
            return str;
        }

        internal bool AddAdditionalName( PropertyDescriptor _param1 )
        {
            Func<IChartComponent, PropertyDescriptor, bool> sel = this._settings._propertyDescSelector;
            if ( ( sel != null ? ( sel( this._component, _param1 ) ? 1 : 0 ) : 1 ) == 0 )
                return false;
            IChartComponent zLiCojrU = this._component;
            return zLiCojrU == null || !zLiCojrU.AdditionalName( _param1.Name );
        }

        internal IEnumerable<PropertyDescriptor> SelectManyConditions( PropertyDescriptor propDesc )
        {
            object obj = propDesc.GetValue((object) this._component);
            
            IEnumerable<PropertyDescriptor>  pd = new List<PropertyDescriptor>();

            var one = new List<PropertyDescriptor>();
            one.Add(propDesc);

            if ( !( obj is IChartComponent chartCom ) )
            {
                var painter = obj as StockSharp.Charting.IChartIndicatorPainter;

                pd = painter != null ? TypeDescriptor.GetProperties( painter, false ).OfType<PropertyDescriptor>().Where( p =>
                {
                    var attr = p.Attributes.OfType<BrowsableAttribute>().FirstOrDefault();
                    return attr == null || attr.Browsable;
                } ).Select<PropertyDescriptor, PropertyDescriptor>( pd =>
                {
                    return !TypeHelper.Is<StockSharp.Charting.IChartElement>( pd.PropertyType, true ) ? pd : ChartComponentChartSettings.Create( CreateName( Extensions.GetDisplayName( pd, null ) ), painter, ( IChartComponent ) pd.GetValue( painter ), _settings._propertyDescSelector );

                } ) : ( IEnumerable<PropertyDescriptor> ) one;
            }
            else
            {
                var pdl = new List<PropertyDescriptor>();
                pdl.Add( Create( this.CreateName( this._component?.GetName( ( StockSharp.Xaml.Charting.IChartElement ) chartCom ) ?? Extensions.GetDisplayName( propDesc, ( string ) null ) ), ( object ) this._component, chartCom, this._settings._propertyDescSelector ) );

                pd = ( IEnumerable<PropertyDescriptor> ) pdl;
            }

            return pd;
        }
    }
}

