// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Services.IPersistableService
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79FA112F-39E9-4D2F-8DA4-EB9B4E826551
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Core.dll

#nullable disable
using System;

namespace StockSharp.Studio.Core.Services;

public interface IPersistableService
{
    bool ContainsKey(string key);

    TValue GetValue<TValue>(string key, TValue defaultValue = default);

    void SetValue(string key, object value);

    void SetDelayValue(string key, Func<object> value);
}
