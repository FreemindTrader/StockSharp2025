// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Threading.ImmediateScheduler
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecng.Xaml.Charting.Threading
{
    internal class ImmediateScheduler : TaskScheduler
    {
        protected override void QueueTask( Task task )
        {
            this.TryExecuteTask( task );
        }

        protected override bool TryExecuteTaskInline( Task task, bool taskWasPreviouslyQueued )
        {
            return this.TryExecuteTask( task );
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Enumerable.Empty<Task>();
        }

        public override int MaximumConcurrencyLevel
        {
            get
            {
                return 1;
            }
        }
    }
}
