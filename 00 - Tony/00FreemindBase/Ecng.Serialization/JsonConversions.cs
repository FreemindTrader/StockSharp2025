// Decompiled with JetBrains decompiler
// Type: Ecng.Serialization.JsonConversions
// Assembly: Ecng.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D99F6F-5E8A-42DB-B327-361BEEF69266
// Assembly location: T:\00-FreemindTrader\packages\ecng.serialization\1.0.127\lib\netstandard2.0\Ecng.Serialization.dll

using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace Ecng.Serialization
{
  internal static class JsonConversions
  {
    static JsonConversions()
    {
      Converter.AddTypedConverter<object[], SecureString>((Func<object[], SecureString>) (val => SecureStringEncryptor.Instance.Decrypt(((IEnumerable<object>) val).Select<object, byte>((Func<object, byte>) (i => i.To<byte>())).ToArray<byte>())));
    }
  }
}
