// Decompiled with JetBrains decompiler
// Type: #=zeHqydGt1MYwtwPKPfmmWnLJ77ByVH81gaeQU2i00qjI8QhelTQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.ComponentModel;
using System.Diagnostics;

#nullable disable
internal class \u0023\u003DzeHqydGt1MYwtwPKPfmmWnLJ77ByVH81gaeQU2i00qjI8QhelTQ\u003D\u003D : 
  \u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D,
  INotifyPropertyChanged
{
  
  private string \u0023\u003DzeWnJxQc\u003D;
  
  private bool \u0023\u003DzsSzaZ5c0aQtyUjczXg\u003D\u003D;
  
  private string \u0023\u003Dzk0QvTIqzWPOhJybPtA\u003D\u003D;
  
  private string \u0023\u003Dziis2LAS1n\u0024lcleXxEQ\u003D\u003D;

  public event PropertyChangedEventHandler PropertyChanged;

  public string Text
  {
    get => this.\u0023\u003DzeWnJxQc\u003D;
    set
    {
      if (!(this.\u0023\u003DzeWnJxQc\u003D != value))
        return;
      this.\u0023\u003DzeWnJxQc\u003D = value;
      this.\u0023\u003Dz15moWio\u003D(XXX.SSS(-539427542));
    }
  }

  protected virtual void \u0023\u003Dz15moWio\u003D(string _param1)
  {
    PropertyChangedEventHandler zUapFgog = this.\u0023\u003DzUApFgog\u003D;
    if (zUapFgog == null)
      return;
    zUapFgog((object) this, new PropertyChangedEventArgs(_param1));
  }

  public virtual bool HasExponent
  {
    get => this.\u0023\u003DzsSzaZ5c0aQtyUjczXg\u003D\u003D;
    set => this.\u0023\u003DzsSzaZ5c0aQtyUjczXg\u003D\u003D = value;
  }

  public virtual string Separator
  {
    get => this.\u0023\u003Dzk0QvTIqzWPOhJybPtA\u003D\u003D;
    set => this.\u0023\u003Dzk0QvTIqzWPOhJybPtA\u003D\u003D = value;
  }

  public virtual string Exponent
  {
    get => this.\u0023\u003Dziis2LAS1n\u0024lcleXxEQ\u003D\u003D;
    set => this.\u0023\u003Dziis2LAS1n\u0024lcleXxEQ\u003D\u003D = value;
  }
}
