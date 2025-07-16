// Decompiled with JetBrains decompiler
// Type: #=zsDU9XQyTsl2DjEg2HhKpBBvHsFJHy2zfRF5KcRE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
internal sealed class \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBBvHsFJHy2zfRF5KcRE\u003D : TaskScheduler
{
  protected override void QueueTask(Task _param1) => this.TryExecuteTask(_param1);

  protected override bool TryExecuteTaskInline(Task _param1, bool _param2)
  {
    return this.TryExecuteTask(_param1);
  }

  protected override IEnumerable<Task> GetScheduledTasks()
  {
    return (IEnumerable<Task>) Array.Empty<Task>();
  }

  public override int MaximumConcurrencyLevel => 1;
}
