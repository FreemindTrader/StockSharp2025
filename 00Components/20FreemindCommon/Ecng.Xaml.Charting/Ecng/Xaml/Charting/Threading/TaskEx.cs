// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Threading.TaskEx
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Threading.Tasks;

namespace fx.Xaml.Charting
{
    internal static class TaskEx
    {
        private static readonly ImmediateScheduler _immediateScheduler = new ImmediateScheduler();

        internal static Task<TResult> FromResult<TResult>( TResult result )
        {
            TaskCompletionSource<TResult> completionSource = new TaskCompletionSource<TResult>();
            completionSource.SetResult( result );
            return completionSource.Task;
        }

        internal static TaskScheduler ImmediateScheduler()
        {
            return ( TaskScheduler ) TaskEx._immediateScheduler;
        }
    }
}
