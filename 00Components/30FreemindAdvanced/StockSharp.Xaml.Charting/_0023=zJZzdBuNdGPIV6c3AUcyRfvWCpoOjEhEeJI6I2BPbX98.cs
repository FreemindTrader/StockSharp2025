// Decompiled with JetBrains decompiler
// Type: #=zJZzdBuNdGPIV6c3AUcyRfvWCpoOjEhEeJI6I2BPbX98a
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Xml;

#nullable disable
internal static class \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfvWCpoOjEhEeJI6I2BPbX98a
{
  public static void \u0023\u003DzVjDFK7Q\u003D(
    this XmlWriter _param0,
    object _param1,
    string _param2)
  {
    \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfvWCpoOjEhEeJI6I2BPbX98a.\u0023\u003DzSxTkBL91\u0024nQyhjyA6w\u003D\u003D sxTkBl91NQyhjyA6w = new \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfvWCpoOjEhEeJI6I2BPbX98a.\u0023\u003DzSxTkBL91\u0024nQyhjyA6w\u003D\u003D();
    sxTkBl91NQyhjyA6w.\u0023\u003Dza5poCxI\u003D = _param2;
    object obj = ((IEnumerable<PropertyInfo>) _param1.GetType().GetProperties()).First<PropertyInfo>(new Func<PropertyInfo, bool>(sxTkBl91NQyhjyA6w.\u0023\u003DzdgBO79K5qXBebtgGdA\u003D\u003D)).GetValue(_param1, (object[]) null);
    if (obj == null)
      return;
    string str = \u0023\u003DzJZzdBuNdGPIV6c3AUcyRfvWCpoOjEhEeJI6I2BPbX98a.\u0023\u003DznsVM9io\u003D(obj);
    _param0.WriteAttributeString(sxTkBl91NQyhjyA6w.\u0023\u003Dza5poCxI\u003D, str);
  }

  private static string \u0023\u003DznsVM9io\u003D(object _param0)
  {
    string str;
    switch (_param0)
    {
      case Brush _:
        Color color1 = ((Brush) _param0).\u0023\u003DzTI4bfbI\u003D();
        str = string.Format("", (object) color1.A, (object) color1.R, (object) color1.G, (object) color1.B);
        break;
      case Color color2:
        str = string.Format("", (object) color2.A, (object) color2.R, (object) color2.G, (object) color2.B);
        break;
      case IRange _:
        Type type = _param0.GetType();
        DoubleRange klqcJ87Zm8UwE3WEjd = ((IRange) _param0).AsDoubleRange();
        str = string.Format("", (object) type.FullName, (object) klqcJ87Zm8UwE3WEjd.Min, (object) klqcJ87Zm8UwE3WEjd.Max);
        break;
      case Thickness thickness:
        str = string.Format("", (object) thickness.Left, (object) thickness.Top, (object) thickness.Right, (object) thickness.Bottom);
        break;
      default:
        str = _param0.ToString();
        break;
    }
    return str;
  }

  private sealed class \u0023\u003DzSxTkBL91\u0024nQyhjyA6w\u003D\u003D
  {
    public string \u0023\u003Dza5poCxI\u003D;

    internal bool \u0023\u003DzdgBO79K5qXBebtgGdA\u003D\u003D(PropertyInfo _param1)
    {
      return _param1.Name == this.\u0023\u003Dza5poCxI\u003D;
    }
  }
}
