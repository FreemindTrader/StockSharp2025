// Decompiled with JetBrains decompiler
// Type: #=zK74oGPE3yyB7zop8uDdznzD7Pf89gd$MAL7CReIz$MuBMY25ahwavpE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal sealed class \u0023\u003DzK74oGPE3yyB7zop8uDdznzD7Pf89gd\u0024MAL7CReIz\u0024MuBMY25ahwavpE\u003D : 
  Exception
{
  public \u0023\u003DzK74oGPE3yyB7zop8uDdznzD7Pf89gd\u0024MAL7CReIz\u0024MuBMY25ahwavpE\u003D(
    Type _param1,
    string _param2)
    : base($"Unable to add subscription for {_param1} : {_param2}")
  {
  }

  public \u0023\u003DzK74oGPE3yyB7zop8uDdznzD7Pf89gd\u0024MAL7CReIz\u0024MuBMY25ahwavpE\u003D(
    Type _param1,
    string _param2,
    Exception _param3)
    : base($"Unable to add subscription for {_param1} : {_param2}", _param3)
  {
  }
}
