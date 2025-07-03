// Decompiled with JetBrains decompiler
// Type: -.dje_zUWZK4BU846MEUQCR5WKT9QZA85UZEQW6M2YZEKBE72N8R2PVRLRQA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zUWZK4BU846MEUQCR5WKT9QZA85UZEQW6M2YZEKBE72N8R2PVRLRQA_ejd : 
  \u0023\u003Dzv8\u00244HkchJrALy0ZPvOtbXZjKloG7lJww3\u00248riNI9I7z_
{
  
  private static readonly List<Type> \u0023\u003DzVGdWd1PKAs\u00242 = ((IEnumerable<Type>) new Type[1]
  {
    typeof (TimeSpan)
  }).ToList<Type>();

  public dje_zUWZK4BU846MEUQCR5WKT9QZA85UZEQW6M2YZEKBE72N8R2PVRLRQA_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zUWZK4BU846MEUQCR5WKT9QZA85UZEQW6M2YZEKBE72N8R2PVRLRQA_ejd);
    this.DefaultLabelProvider = (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH) new \u0023\u003DzzsyKnUNUDKjF7rDv70izN0tWzkIzz0JM4yPz3ydodySc();
    this.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz1bLZaITSYGdx, (object) new \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u8VlgKY4UpXGtvXPXNaRp9rB());
  }

  public override IRange \u0023\u003DzspbjXJnVtbB\u0024()
  {
    return (IRange) new \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D();
  }

  protected override IRange \u0023\u003DzsB7Y9t30CQ63(
    IRange _param1)
  {
    return (IRange) this.\u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D().AsDoubleRange();
  }

  public override IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D()
  {
    return (IRange) new \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D(TimeSpan.Zero, TimeSpan.FromSeconds(1.0));
  }

  protected override IRange \u0023\u003DzJMGFyjEoHSQY(
    IComparable _param1,
    IComparable _param2)
  {
    return (IRange) new \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D(_param1.\u0023\u003Dzto51K8pl8UAh(), _param2.\u0023\u003Dzto51K8pl8UAh());
  }

  protected override \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D \u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D()
  {
    return (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D) \u0023\u003DzVH_2qrK9zbX0c0InJYxonP2mY7VmcEn2HiL1lqN9RvXlRNSInA\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
  }

  protected override IComparable \u0023\u003Dz3ZiX3E6vqtLl(IComparable _param1)
  {
    return (IComparable) _param1.\u0023\u003Dzto51K8pl8UAh();
  }

  public override bool \u0023\u003Dz9yvpaTXy3ucx(
    IRange _param1)
  {
    return _param1 is \u0023\u003DzAtYWtSRxk8WC\u0024EcJQ7b1L7dHXBxHQWas0w\u003D\u003D;
  }

  protected override List<Type> \u0023\u003DzvwDcRtQA0c4T()
  {
    return dje_zUWZK4BU846MEUQCR5WKT9QZA85UZEQW6M2YZEKBE72N8R2PVRLRQA_ejd.\u0023\u003DzVGdWd1PKAs\u00242;
  }
}
