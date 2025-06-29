// Decompiled with JetBrains decompiler
// Type: #=zYH1zUE63H2wnu5PkgVdj8HLVZa82jmIxAK$iEjE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal static class \u0023\u003DzYH1zUE63H2wnu5PkgVdj8HLVZa82jmIxAK\u0024iEjE\u003D
{
  public static string \u0023\u003Dzb_Ih6a0\u003D(this Type _param0)
  {
    string fullName = _param0.FullName;
    return !(Type.GetType(fullName) != (Type) null) ? _param0.AssemblyQualifiedName : fullName;
  }
}
