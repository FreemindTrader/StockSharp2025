// Decompiled with JetBrains decompiler
// Type: #=zN0ICfvrLGc6u90AzzFcyQnKZ_q4KO7CPmFGx6ZQ=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Reflection;

#nullable disable
internal static class \u0023\u003DzN0ICfvrLGc6u90AzzFcyQnKZ_q4KO7CPmFGx6ZQ\u003D
{
  public static void \u0023\u003DzBZeItLeUMY2I<T>(T _param0, T _param1)
  {
    \u0023\u003DzN0ICfvrLGc6u90AzzFcyQnKZ_q4KO7CPmFGx6ZQ\u003D.\u0023\u003Dzu\u0024i\u0024b\u0024xmE81L<T>();
    foreach (PropertyInfo property in typeof (T).GetProperties())
    {
      if (property.CanRead && property.CanWrite)
        property.SetValue((object) _param1, property.GetValue((object) _param0, (object[]) null), (object[]) null);
    }
  }

  private static void \u0023\u003Dzu\u0024i\u0024b\u0024xmE81L<T>()
  {
    if (!typeof (T).IsInterface)
      throw new Exception(string.Format(XXX.SSS(-539441071), (object) typeof (T)));
  }
}
