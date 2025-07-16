// Decompiled with JetBrains decompiler
// Type: #=zd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx$cYB5w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.ComponentModel;
using StockSharp.Algo.Indicators;
using StockSharp.Xaml.Charting.Ultrachart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
public sealed class IndicatorSettingsObject :
  ChartSettingsObjectBase<IIndicator>
{
    public IndicatorSettingsObject( IIndicator _param1 )
      : base( _param1 )
    {
        this.Orig.Reseted += new Action( this.OnResetCallback );
    }

    public static PropertyDescriptor FuncBool(
      string _param0,
      object _param1,
      IIndicator _param2 )
    {
        return ( PropertyDescriptor ) new IndicatorSettingsObject.SomeClass434343( _param0, _param1, _param2 );
    }

    private void OnResetCallback()
    {
    }

    protected override PropertyDescriptor[ ] OnGetProperties( IIndicator ind )
    {
        if ( ind == null )
            return null;


        List<PropertyDescriptor> pdl = new List<PropertyDescriptor>();
        pdl.AddRange(
            TypeDescriptor.GetProperties( ind, false )
                .OfType<PropertyDescriptor>()
                .Where(
                    p =>
                    {
                        if ( !( p.Name != "Name" ) )
                            return false;
                        var b = p.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
                        return b == null || b.Browsable;
                    } )
                .Select(
                    p =>
                    {
                        return !( p.GetValue( ind ) is IIndicator indicator )
                            ? p
                            : IndicatorSettingsObject.FuncBool(
                                        Extensions.GetDisplayName( p, indicator.Name ),
                                        ind,
                                        indicator );
                    } ) );

        return pdl.ToArray();
    }

    private sealed class SomeWheireosoe
    {
        public IIndicator SomeEthmoed;

        public PropertyDescriptor SomeMethod383(
          PropertyDescriptor p )
        {
            return !( p.GetValue( this.SomeEthmoed ) is IIndicator indicator ) ? p : IndicatorSettingsObject.FuncBool( Extensions.GetDisplayName( p, indicator.Name ), ( object ) this.SomeEthmoed, indicator );
        }
    }

    [Serializable]
    private sealed class SomeClass34343383
    {
        public static readonly IndicatorSettingsObject.SomeClass34343383 SomeMethond0343 = new IndicatorSettingsObject.SomeClass34343383();
        public static Func<PropertyDescriptor, bool> \u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D;

public bool Abcd343( PropertyDescriptor p )
        {
            if ( !( p.Name != "Name" ) )
                return false;
            BrowsableAttribute browsableAttribute = p.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
            return browsableAttribute == null || browsableAttribute.Browsable;
        }
    }

    private sealed class SomeClass434343(
      string _param1,
      object _param2,
      IIndicator _param3 ) : ChartSettingsObjectBase<IIndicator>.ProxyDescriptor( _param1, _param2, _param3, Enumerable.Append<Attribute>( Enumerable.Cast<Attribute>( TypeDescriptor.GetAttributes( ( object ) _param3, false ) ), ( Attribute ) new TypeConverterAttribute( typeof( ExpandableObjectConverter ) ) ) )
    {
        protected override ChartSettingsObjectBase<IIndicator> CreateWrapper(
          IIndicator _param1,
          Func<IIndicator, PropertyDescriptor, bool> _param2 = null )
        {
            return ( ChartSettingsObjectBase<IIndicator> ) new IndicatorSettingsObject( _param1 );
        }
    }
}
