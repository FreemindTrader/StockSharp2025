// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.JsonDateTimeMcsConverter
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Ecng.Common;
using System;

namespace Ecng.Serialization
{
  public class JsonDateTimeMcsConverter : JsonDateTimeConverter
  {
    public JsonDateTimeMcsConverter()
      : base(false)
    {
    }

    protected override DateTime Convert(double value)
    {
      return value.FromUnixMcs();
    }
  }
}
