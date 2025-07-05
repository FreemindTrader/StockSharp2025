// Decompiled with JetBrains decompiler
// Type: #=zzSV9ePAnJ860K$gH7z$ORjrmDYZJziML8SgUAERSAV0$lfQskg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Charting;
using StockSharp.Xaml.Charting.Ultrachart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

#nullable enable
internal sealed class ChartComponentElementSettings : 
  ChartSettingsObjectBase<
  #nullable disable
  IChartComponent>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Func<IChartComponent, PropertyDescriptor, bool> \u0023\u003DzZdxZPcbMFUEQ;

  public ChartComponentElementSettings(
    IChartComponent _param1,
    Func<IChartComponent, PropertyDescriptor, bool> _param2 = null)
    : base(_param1)
  {
    this.\u0023\u003DzZdxZPcbMFUEQ = _param2;
    this.Orig.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003Dzf9AYwNNDizJfTAg5WA\u003D\u003D);
  }

  public static PropertyDescriptor \u0023\u003DzANqI1s0\u003D(
    string _param0,
    object _param1,
    IChartComponent _param2,
    Func<IChartComponent, PropertyDescriptor, bool> _param3)
  {
    return (PropertyDescriptor) new ChartComponentElementSettings.\u0023\u003DzxnJZ3YUHC\u00248n(_param0, _param1, _param2, _param3);
  }

  protected override PropertyDescriptor[] OnGetProperties(
    IChartComponent _param1)
  {
    ChartComponentElementSettings.SomeWheireosoe vbxLeArTkallkIdHg = new ChartComponentElementSettings.SomeWheireosoe();
    vbxLeArTkallkIdHg.\u0023\u003DzRRvwDu67s9Rm = this;
    vbxLeArTkallkIdHg.\u0023\u003DzLICojrU\u003D = _param1;
    vbxLeArTkallkIdHg.\u0023\u003DzaEWfwlo\u003D = new HashSet<string>();
    if (vbxLeArTkallkIdHg.\u0023\u003DzLICojrU\u003D == null)
      return (PropertyDescriptor[]) null;
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    propertyDescriptorList.AddRange(TypeDescriptor.GetProperties((object) vbxLeArTkallkIdHg.\u0023\u003DzLICojrU\u003D, false).OfType<PropertyDescriptor>().Where<PropertyDescriptor>(new Func<PropertyDescriptor, bool>(vbxLeArTkallkIdHg.SomeMethod383)).SelectMany<PropertyDescriptor, PropertyDescriptor>(new Func<PropertyDescriptor, IEnumerable<PropertyDescriptor>>(vbxLeArTkallkIdHg.\u0023\u003Dz7Ouzu6Mg\u0024YJrs1NoTA\u003D\u003D)));
    return propertyDescriptorList.ToArray();
  }

  private void \u0023\u003Dzf9AYwNNDizJfTAg5WA\u003D\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    this.NotifyChanged(_param2.PropertyName);
  }

  private sealed class SomeWheireosoe
  {
    public 
    #nullable disable
    HashSet<string> \u0023\u003DzaEWfwlo\u003D;
    public ChartComponentElementSettings \u0023\u003DzRRvwDu67s9Rm;
    public IChartComponent \u0023\u003DzLICojrU\u003D;

    internal string \u0023\u003DqXZI6v4OexM03brYrnueLBCm8b848g80JJ6Fxnx4QkFKHFME4ysV0qdDrSmWCfxM2(
      string _param1)
    {
      string str = _param1;
      int num = 0;
      while (!this.\u0023\u003DzaEWfwlo\u003D.Add(str))
        str = _param1 + (++num).ToString();
      return str;
    }

    internal bool SomeMethod383(PropertyDescriptor p)
    {
      Func<IChartComponent, PropertyDescriptor, bool> zZdxZpcbMfueq = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzZdxZPcbMFUEQ;
      if ((zZdxZpcbMfueq != null ? (zZdxZpcbMfueq(this.\u0023\u003DzLICojrU\u003D, p) ? 1 : 0) : 1) == 0)
        return false;
      IChartComponent zLiCojrU = this.\u0023\u003DzLICojrU\u003D;
      return zLiCojrU == null || !zLiCojrU.AdditionalName(p.Name);
    }

    internal 
    #nullable enable
    IEnumerable<
    #nullable disable
    PropertyDescriptor> \u0023\u003Dz7Ouzu6Mg\u0024YJrs1NoTA\u003D\u003D(PropertyDescriptor _param1)
    {
      object obj = _param1.GetValue((object) this.\u0023\u003DzLICojrU\u003D);
      ChartComponentElementSettings.\u0023\u003DzmD295WW4skLs1H\u0024zBQ\u003D\u003D d295Ww4skLs1HZBq = new ChartComponentElementSettings.\u0023\u003DzmD295WW4skLs1H\u0024zBQ\u003D\u003D();
      d295Ww4skLs1HZBq.\u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D = this;
      IEnumerable<PropertyDescriptor> propertyDescriptors;
      if (!(obj is IChartComponent ddznyiGmdRlAevOq))
      {
        d295Ww4skLs1HZBq.\u0023\u003DzT8iWFU1Lr3ZdgvcQELUnNho\u003D = obj as IChartIndicatorPainter;
        propertyDescriptors = d295Ww4skLs1HZBq.\u0023\u003DzT8iWFU1Lr3ZdgvcQELUnNho\u003D != null ? TypeDescriptor.GetProperties((object) d295Ww4skLs1HZBq.\u0023\u003DzT8iWFU1Lr3ZdgvcQELUnNho\u003D, false).OfType<PropertyDescriptor>().Where<PropertyDescriptor>(ChartComponentElementSettings.SomeClass34343383.\u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D ?? (ChartComponentElementSettings.SomeClass34343383.\u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D = new Func<PropertyDescriptor, bool>(ChartComponentElementSettings.SomeClass34343383.SomeMethond0343.\u0023\u003DzAlVcETnEQ\u0024a0RzdLig\u003D\u003D))).Select<PropertyDescriptor, PropertyDescriptor>(new Func<PropertyDescriptor, PropertyDescriptor>(d295Ww4skLs1HZBq.\u0023\u003DzGOsnTXDdP0nJN43aQA\u003D\u003D)) : (IEnumerable<PropertyDescriptor>) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<PropertyDescriptor>(_param1);
      }
      else
        propertyDescriptors = (IEnumerable<PropertyDescriptor>) new \u0023\u003DzxOY_ppISsiadppaSwGkbOR8\u003D<PropertyDescriptor>(ChartComponentElementSettings.\u0023\u003DzANqI1s0\u003D(this.\u0023\u003DqXZI6v4OexM03brYrnueLBCm8b848g80JJ6Fxnx4QkFKHFME4ysV0qdDrSmWCfxM2(this.\u0023\u003DzLICojrU\u003D?.GetName((IChartElement) ddznyiGmdRlAevOq) ?? Extensions.GetDisplayName(_param1, (string) null)), (object) this.\u0023\u003DzLICojrU\u003D, ddznyiGmdRlAevOq, this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzZdxZPcbMFUEQ));
      return propertyDescriptors;
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly ChartComponentElementSettings.SomeClass34343383 SomeMethond0343 = new ChartComponentElementSettings.SomeClass34343383();
    public static Func<PropertyDescriptor, bool> \u0023\u003Dz08eHob2PYGlidFcWEw\u003D\u003D;

    internal bool \u0023\u003DzAlVcETnEQ\u0024a0RzdLig\u003D\u003D(PropertyDescriptor _param1)
    {
      BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
      return browsableAttribute == null || browsableAttribute.Browsable;
    }
  }

  private sealed class \u0023\u003DzmD295WW4skLs1H\u0024zBQ\u003D\u003D
  {
    public IChartIndicatorPainter \u0023\u003DzT8iWFU1Lr3ZdgvcQELUnNho\u003D;
    public ChartComponentElementSettings.SomeWheireosoe \u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D;

    internal PropertyDescriptor \u0023\u003DzGOsnTXDdP0nJN43aQA\u003D\u003D(
      PropertyDescriptor _param1)
    {
      return !TypeHelper.Is<IChartElement>(_param1.PropertyType, true) ? _param1 : ChartComponentElementSettings.\u0023\u003DzANqI1s0\u003D(this.\u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D.\u0023\u003DqXZI6v4OexM03brYrnueLBCm8b848g80JJ6Fxnx4QkFKHFME4ysV0qdDrSmWCfxM2(Extensions.GetDisplayName(_param1, (string) null)), (object) this.\u0023\u003DzT8iWFU1Lr3ZdgvcQELUnNho\u003D, (IChartComponent) _param1.GetValue((object) this.\u0023\u003DzT8iWFU1Lr3ZdgvcQELUnNho\u003D), this.\u0023\u003Dq9oPAsd_XX7A0iOT0n\u0024XoJRH5BDywfM7Sl5FCgaUG6WI\u003D.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzZdxZPcbMFUEQ);
    }
  }

  private sealed class \u0023\u003DzxnJZ3YUHC\u00248n(
    string _param1,
    object _param2,
    IChartComponent _param3,
    Func<IChartComponent, PropertyDescriptor, bool> _param4) : 
    ChartSettingsObjectBase<IChartComponent>.ProxyDescriptor(_param1, _param2, _param3, Enumerable.Cast<Attribute>(TypeDescriptor.GetAttributes((object) _param3, false)), _param4)
  {
    protected override ChartSettingsObjectBase<IChartComponent> CreateWrapper(
      IChartComponent _param1,
      Func<IChartComponent, PropertyDescriptor, bool> _param2 = null)
    {
      return (ChartSettingsObjectBase<IChartComponent>) new ChartComponentElementSettings(_param1, _param2);
    }
  }
}
