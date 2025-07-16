// Decompiled with JetBrains decompiler
// Type: #=zIKGIOuOUyRwFEgUWrfZxw422C01wi00GXG6siWuShyyh
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Reflection;

#nullable disable
internal sealed class \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw422C01wi00GXG6siWuShyyh : IDisposable
{
  
  private WeakReference \u0023\u003DzekIeoK8\u003D;
  
  private Type \u0023\u003Dz0AB1VGY\u003D;

  public \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw422C01wi00GXG6siWuShyyh(
    \u0023\u003Dz3uoqT9PJZU9sx1O75XaUuwQPg4PJT0faCb_EEayRvBbwk\u00245OFMeEiVo\u003D _param1,
    Type _param2)
  {
    if (_param1 == null)
      throw new ArgumentNullException("hub");
    if (!typeof (\u0023\u003Dzxn9vS9UX4BfDgK8stUp1bU9TbfoDtGpTtZMbxfI\u003D).IsAssignableFrom(_param2))
      throw new ArgumentOutOfRangeException("messageType");
    this.\u0023\u003DzekIeoK8\u003D = new WeakReference((object) _param1);
    this.\u0023\u003Dz0AB1VGY\u003D = _param2;
  }

  public void Dispose()
  {
    if (this.\u0023\u003DzekIeoK8\u003D.IsAlive && this.\u0023\u003DzekIeoK8\u003D.Target is \u0023\u003Dz3uoqT9PJZU9sx1O75XaUuwQPg4PJT0faCb_EEayRvBbwk\u00245OFMeEiVo\u003D target)
    {
      MethodInfo methodInfo = typeof (\u0023\u003Dz3uoqT9PJZU9sx1O75XaUuwQPg4PJT0faCb_EEayRvBbwk\u00245OFMeEiVo\u003D).GetMethod("Unsubscribe", new Type[1]
      {
        typeof (\u0023\u003DzIKGIOuOUyRwFEgUWrfZxw422C01wi00GXG6siWuShyyh)
      }).MakeGenericMethod(this.\u0023\u003Dz0AB1VGY\u003D);
      object[] parameters = new object[1]{ (object) this };
      methodInfo.Invoke((object) target, parameters);
    }
    GC.SuppressFinalize((object) this);
  }
}
