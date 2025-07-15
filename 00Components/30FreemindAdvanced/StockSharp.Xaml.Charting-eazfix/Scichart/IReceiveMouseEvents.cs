// Decompiled with JetBrains decompiler
// Type: #=zTNhhT9A_S5PTAzjbiBFcpOgj$HEwAG4ZlfwSGT7i2APW
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
internal interface IReceiveMouseEvents 
{
  bool IsEnabled { get; set; }

  bool get_IsEnabled();

  void set_IsEnabled(bool _param1);

  string MouseEventGroup { get; set; }

  string get_MouseEventGroup();

  void set_MouseEventGroup(string _param1);

  bool \u0023\u003Dzo7mdr1Y1DFNe();

  void \u0023\u003Dz5y8F1YNwkhnW(
    ModifierMouseArgs _param1);

  void \u0023\u003DzsXEfcKpqchyX(
    ModifierMouseArgs _param1);

  void OnModifierMouseMove(
    ModifierMouseArgs _param1);

  void OnModifierMouseUp(
    ModifierMouseArgs _param1);

  void \u0023\u003DzQTINWhMByBmJ(
    ModifierMouseArgs _param1);

  void \u0023\u003Dz0yya794Z8OaI(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1);

  void \u0023\u003DzpmQpuKvOtHIk(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1);

  void \u0023\u003DzsSwjrBzrsGPJ(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OAyK9MPuNjG5KW8R74IqEWckf _param1);

  void OnMasterMouseLeave(
    ModifierMouseArgs _param1);
}
