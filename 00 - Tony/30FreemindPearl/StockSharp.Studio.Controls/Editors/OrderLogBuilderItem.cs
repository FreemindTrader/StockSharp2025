// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.Editors.OrderLogBuilderItem
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace StockSharp.Studio.Controls.Editors
{
  internal class OrderLogBuilderItem
  {
    public string Name { get; }

    public Type Type { get; }

    public OrderLogBuilderItem(string name, Type type)
    {
      Name = name;
      Type = type;
    }

    public static object CreateSource(out Type defaultBuilder)
    {
      defaultBuilder = typeof (OrderLogMarketDepthBuilder);
      Dictionary<Type, OrderLogBuilderItem> dictionary = new Dictionary<Type, OrderLogBuilderItem>() { { defaultBuilder, new OrderLogBuilderItem(LocalizedStrings.Default, defaultBuilder) } };
      foreach (IMessageAdapter possibleAdapter in ServicesRegistry.AdapterProvider.PossibleAdapters)
      {
        Type type = possibleAdapter.CreateOrderLogMarketDepthBuilder(new SecurityId()).GetType();
        if (!dictionary.ContainsKey(type))
          dictionary.Add(type, new OrderLogBuilderItem(possibleAdapter.StorageName, type));
      }
      return dictionary.Values;
    }
  }
}
