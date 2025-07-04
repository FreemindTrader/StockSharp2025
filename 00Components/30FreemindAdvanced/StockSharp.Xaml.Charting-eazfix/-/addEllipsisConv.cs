// Decompiled with JetBrains decompiler
// Type: -.addEllipsisConv
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace \u002D;

internal sealed class addEllipsisConv : IValueConverter
{
    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4 )
    {
        string str = _param1 as string;
        return StringHelper.IsEmpty( str ) ? _param1 : ( object ) ( str + "..." );
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4 )
    {
        throw new NotSupportedException();
    }
}
