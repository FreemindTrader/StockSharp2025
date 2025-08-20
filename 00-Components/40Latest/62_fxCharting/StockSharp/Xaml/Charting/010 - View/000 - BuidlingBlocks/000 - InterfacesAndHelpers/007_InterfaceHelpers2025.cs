// Decompiled with JetBrains decompiler
// Type: #=zN0ICfvrLGc6u90AzzFcyQnKZ_q4KO7CPmFGx6ZQ=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Reflection;

namespace StockSharp.Xaml.Charting;

#nullable disable
public static class InterfaceHelpers2025
{
  public static void CopyInterfaceProperties<T>(T _param0, T _param1)
  {
    InterfaceHelpers2025.CheckInterfaceProperties<T>();
    foreach (PropertyInfo property in typeof (T).GetProperties())
    {
      if (property.CanRead && property.CanWrite)
        property.SetValue((object) _param1, property.GetValue((object) _param0, (object[]) null), (object[]) null);
    }
  }

  private static void CheckInterfaceProperties<T>()
  {
    if (!typeof (T).IsInterface)
      throw new Exception($"Unable to copy interface properties as typeparam {typeof (T)} is not an interface");
  }
}
