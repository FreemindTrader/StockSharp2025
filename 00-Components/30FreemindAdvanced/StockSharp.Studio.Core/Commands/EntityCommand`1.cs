// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.EntityCommand`1
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79FA112F-39E9-4D2F-8DA4-EB9B4E826551
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Core.dll

#nullable disable
using System;
using StockSharp.BusinessEntities;

namespace StockSharp.Studio.Core.Commands;

public class EntityCommand<TEntity>(Subscription subscription, TEntity entity) : BaseStudioCommand where TEntity : class
{
    public Subscription Subscription { get; } = subscription ?? throw new ArgumentNullException(nameof(subscription));

    public TEntity Entity { get; } = entity ?? throw new ArgumentNullException(nameof(entity));
}
