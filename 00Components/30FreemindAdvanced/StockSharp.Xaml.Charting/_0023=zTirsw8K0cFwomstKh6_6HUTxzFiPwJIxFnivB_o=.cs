// Decompiled with JetBrains decompiler
// Type: #=zTirsw8K0cFwomstKh6_6HUTxzFiPwJIxFnivB_o=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Threading.Tasks;

#nullable disable
internal static class \u0023\u003DzTirsw8K0cFwomstKh6_6HUTxzFiPwJIxFnivB_o\u003D
{
  private static readonly \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBBvHsFJHy2zfRF5KcRE\u003D \u0023\u003DzVY\u0024fBpsjIo4u = new \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBBvHsFJHy2zfRF5KcRE\u003D();

  internal static Task<TResult> \u0023\u003DzE7HN918\u003D<TResult>(TResult _param0)
  {
    TaskCompletionSource<TResult> completionSource = new TaskCompletionSource<TResult>();
    completionSource.SetResult(_param0);
    return completionSource.Task;
  }

  internal static TaskScheduler \u0023\u003Dz7VLD9AiY5L2y()
  {
    return (TaskScheduler) \u0023\u003DzTirsw8K0cFwomstKh6_6HUTxzFiPwJIxFnivB_o\u003D.\u0023\u003DzVY\u0024fBpsjIo4u;
  }
}
