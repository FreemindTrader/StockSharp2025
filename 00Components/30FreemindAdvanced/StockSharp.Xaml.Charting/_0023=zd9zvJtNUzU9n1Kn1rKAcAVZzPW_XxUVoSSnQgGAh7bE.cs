// Decompiled with JetBrains decompiler
// Type: #=zd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx$cYB5w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.ComponentModel;
using StockSharp.Algo.Indicators;
using StockSharp.Xaml.Charting.Ultrachart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
internal sealed class \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D : 
  ChartSettingsObjectBase<IIndicator>
{
  public \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D(
    IIndicator _param1)
    : base(_param1)
  {
    this.Orig.Reseted += new Action(this.\u0023\u003Dze7c89\u0024wRkR45b80_dQ\u003D\u003D);
  }

  public static PropertyDescriptor \u0023\u003DzANqI1s0\u003D(
    string _param0,
    object _param1,
    IIndicator _param2)
  {
    return (PropertyDescriptor) new \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003DzObY37rDWDg\u0024d(_param0, _param1, _param2);
  }

  private void \u0023\u003Dze7c89\u0024wRkR45b80_dQ\u003D\u003D()
  {
  }

  protected override PropertyDescriptor[] OnGetProperties(IIndicator _param1)
  {
    \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D vbxLeArTkallkIdHg = new \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D();
    vbxLeArTkallkIdHg.\u0023\u003DzydNJdHg\u003D = _param1;
    if (vbxLeArTkallkIdHg.\u0023\u003DzydNJdHg\u003D == null)
      return (PropertyDescriptor[]) null;
    List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
    propertyDescriptorList.AddRange(TypeDescriptor.GetProperties((object) vbxLeArTkallkIdHg.\u0023\u003DzydNJdHg\u003D, false).OfType<PropertyDescriptor>().Where<PropertyDescriptor>(\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D ?? (\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D = new Func<PropertyDescriptor, bool>(\u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzK5iFcUiP0uX6iXSUTw\u003D\u003D))).Select<PropertyDescriptor, PropertyDescriptor>(new Func<PropertyDescriptor, PropertyDescriptor>(vbxLeArTkallkIdHg.\u0023\u003Dz3OKSSmcreu7R3ztl\u0024g\u003D\u003D)));
    return propertyDescriptorList.ToArray();
  }

  private sealed class \u0023\u003Dz5VBxLeARTkallkIDHg\u003D\u003D
  {
    public IIndicator \u0023\u003DzydNJdHg\u003D;

    internal PropertyDescriptor \u0023\u003Dz3OKSSmcreu7R3ztl\u0024g\u003D\u003D(
      PropertyDescriptor _param1)
    {
      return !(_param1.GetValue((object) this.\u0023\u003DzydNJdHg\u003D) is IIndicator indicator) ? _param1 : \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003DzANqI1s0\u003D(Extensions.GetDisplayName(_param1, indicator.Name), (object) this.\u0023\u003DzydNJdHg\u003D, indicator);
    }
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<PropertyDescriptor, bool> \u0023\u003DzKkIXz5CmqQ1_DowyDQ\u003D\u003D;

    internal bool \u0023\u003DzK5iFcUiP0uX6iXSUTw\u003D\u003D(PropertyDescriptor _param1)
    {
      if (!(_param1.Name != ""))
        return false;
      BrowsableAttribute browsableAttribute = _param1.Attributes.OfType<BrowsableAttribute>().FirstOrDefault<BrowsableAttribute>();
      return browsableAttribute == null || browsableAttribute.Browsable;
    }
  }

  private sealed class \u0023\u003DzObY37rDWDg\u0024d(
    string _param1,
    object _param2,
    IIndicator _param3) : ChartSettingsObjectBase<IIndicator>.ProxyDescriptor(_param1, _param2, _param3, Enumerable.Append<Attribute>(Enumerable.Cast<Attribute>(TypeDescriptor.GetAttributes((object) _param3, false)), (Attribute) new TypeConverterAttribute(typeof (ExpandableObjectConverter))))
  {
    protected override ChartSettingsObjectBase<IIndicator> CreateWrapper(
      IIndicator _param1,
      Func<IIndicator, PropertyDescriptor, bool> _param2 = null)
    {
      return (ChartSettingsObjectBase<IIndicator>) new \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAVZzPW_XxUVoSSnQgGAh7bEx\u0024cYB5w\u003D\u003D(_param1);
    }
  }
}
