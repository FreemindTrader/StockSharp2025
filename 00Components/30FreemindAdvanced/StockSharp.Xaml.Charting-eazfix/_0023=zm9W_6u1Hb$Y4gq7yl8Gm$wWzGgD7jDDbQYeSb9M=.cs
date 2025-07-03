// Decompiled with JetBrains decompiler
// Type: #=zm9W_6u1Hb$Y4gq7yl8Gm$wWzGgD7jDDbQYeSb9M=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Xml.Linq;

#nullable disable
internal static class \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u0024wWzGgD7jDDbQYeSb9M\u003D
{
  internal static string \u0023\u003DzhQSajDAhtIuy(this XElement _param0, string _param1)
  {
    return _param0.Attribute((XName) _param1)?.Value;
  }

  internal static string \u0023\u003Dzy58aQkWRKw6y(this XElement _param0, string _param1)
  {
    return (_param0.Attribute((XName) _param1) ?? throw new InvalidOperationException($"Expected attribute <{_param1}/>")).Value;
  }

  internal static string \u0023\u003DzOezjA\u0024ae5BcS(this XElement _param0, string _param1)
  {
    return (_param0.Element((XName) _param1) ?? throw new InvalidOperationException($"Expected element <{_param1}/>")).Value;
  }

  internal static string \u0023\u003DzH_WszwPLeBDF(this XElement _param0, string _param1)
  {
    return _param0.Element((XName) _param1)?.Value;
  }
}
