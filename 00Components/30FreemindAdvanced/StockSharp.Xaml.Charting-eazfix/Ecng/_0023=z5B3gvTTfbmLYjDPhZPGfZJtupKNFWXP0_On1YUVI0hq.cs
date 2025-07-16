// Decompiled with JetBrains decompiler
// Type: #=z5B3gvTTfbmLYjDPhZPGfZJtupKNFWXP0_On1YUVI0hqJ$lotV9V57okcKlXHXNUKOsbYO$c=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Xml.Serialization;

#nullable disable
public interface \u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZJtupKNFWXP0_On1YUVI0hqJ\u0024lotV9V57okcKlXHXNUKOsbYO\u0024c\u003D : 
  IDrawable,
  IXmlSerializable,
  \u0023\u003Dz5VLaAZX2bctAcuSoajSAXvZYOg6JAbLCIgQvZp9odw6FSOKg1daH3vPLNHtT2ZG4iQ\u003D\u003D,
  IRenderableSeries
{
  string StackedGroupId { get; set; }

  string get_StackedGroupId();

  void set_StackedGroupId(string _param1);

  bool IsOneHundredPercent { get; set; }

  bool get_IsOneHundredPercent();

  void set_IsOneHundredPercent(bool _param1);

  double ZeroLineY { get; set; }
}
