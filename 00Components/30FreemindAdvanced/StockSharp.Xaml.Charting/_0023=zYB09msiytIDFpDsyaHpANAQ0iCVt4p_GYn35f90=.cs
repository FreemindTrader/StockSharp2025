// Decompiled with JetBrains decompiler
// Type: #=zYB09msiytIDFpDsyaHpANAQ0iCVt4p_GYn35f90=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Threading;

#nullable disable
internal sealed class \u0023\u003DzYB09msiytIDFpDsyaHpANAQ0iCVt4p_GYn35f90\u003D
{
  internal static void \u0023\u003Dz6OctssU\u003D(int _param0, int _param1, Action<int> _param2)
  {
    \u0023\u003DzYB09msiytIDFpDsyaHpANAQ0iCVt4p_GYn35f90\u003D.\u0023\u003DzSxTkBL91\u0024nQyhjyA6w\u003D\u003D sxTkBl91NQyhjyA6w = new \u0023\u003DzYB09msiytIDFpDsyaHpANAQ0iCVt4p_GYn35f90\u003D.\u0023\u003DzSxTkBL91\u0024nQyhjyA6w\u003D\u003D();
    sxTkBl91NQyhjyA6w.\u0023\u003Dz07PQx44\u003D = _param2;
    sxTkBl91NQyhjyA6w.\u0023\u003DzhsBTSIjM0aOk = _param1;
    int processorCount = Environment.ProcessorCount;
    sxTkBl91NQyhjyA6w.\u0023\u003Dzay7IytM\u003D = _param0 - 1;
    object obj = new object();
    Action action = new Action(sxTkBl91NQyhjyA6w.\u0023\u003DzQI9NbGJrbM1\u0024);
    IAsyncResult[] asyncResultArray = new IAsyncResult[processorCount];
    for (int index = 0; index < processorCount; ++index)
      asyncResultArray[index] = action.BeginInvoke((AsyncCallback) null, (object) null);
    for (int index = 0; index < processorCount; ++index)
      action.EndInvoke(asyncResultArray[index]);
  }

  private sealed class \u0023\u003DzSxTkBL91\u0024nQyhjyA6w\u003D\u003D
  {
    public Action<int> \u0023\u003Dz07PQx44\u003D;
    public int \u0023\u003Dzay7IytM\u003D;
    public int \u0023\u003DzhsBTSIjM0aOk;

    internal void \u0023\u003DzQI9NbGJrbM1\u0024()
    {
      int num;
      while ((num = Interlocked.Increment(ref this.\u0023\u003Dzay7IytM\u003D)) < this.\u0023\u003DzhsBTSIjM0aOk)
        this.\u0023\u003Dz07PQx44\u003D(num);
    }
  }
}
