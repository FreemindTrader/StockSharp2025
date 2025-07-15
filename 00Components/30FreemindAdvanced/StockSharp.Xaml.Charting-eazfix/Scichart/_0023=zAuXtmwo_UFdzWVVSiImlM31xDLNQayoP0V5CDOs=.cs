// Decompiled with JetBrains decompiler
// Type: #=zAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Windows.Input;

#nullable disable
internal static class \u0023\u003DzAuXtmwo_UFdzWVVSiImlM31xDLNQayoP0V5CDOs\u003D
{
  internal static MouseModifier \u0023\u003DzNFIr3TSkl0uk()
  {
    MouseModifier nijb7bojGmWzLupPhdCYfw = MouseModifier.None;
    switch (Keyboard.Modifiers - 1)
    {
      case 0:
        nijb7bojGmWzLupPhdCYfw = MouseModifier.Alt;
        break;
      case 1:
        nijb7bojGmWzLupPhdCYfw = MouseModifier.Ctrl;
        break;
      case 3:
        nijb7bojGmWzLupPhdCYfw = MouseModifier.Shift;
        break;
    }
    return nijb7bojGmWzLupPhdCYfw;
  }
}
