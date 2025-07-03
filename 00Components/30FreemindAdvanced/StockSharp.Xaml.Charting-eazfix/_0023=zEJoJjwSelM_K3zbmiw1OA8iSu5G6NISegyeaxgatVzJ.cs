// Decompiled with JetBrains decompiler
// Type: #=zEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D
{
  internal long \u0023\u003DzN0El7idsXiwE;
  internal uint \u0023\u003DzxhCmePtx6PYr;
  internal uint \u0023\u003DzM8YvCqdng9uO;
  internal string \u0023\u003DzsLDTRVg\u003D;
  internal double \u0023\u003DzAwVoCztpd7ox;
  internal double \u0023\u003DzKmatSefqd0TeJb4URLTZELU\u003D;
  private static Stopwatch \u0023\u003DzUx3FRH0EnNc_;

  public \u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D(
    string _param1)
  {
    this.Reset();
    this.\u0023\u003DzsLDTRVg\u003D = _param1;
    \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dzh_YrSog\u003D(this);
  }

  private long \u0023\u003DzkUt0dbqm_eO6()
  {
    if (\u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D.\u0023\u003DzUx3FRH0EnNc_ == null)
    {
      \u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D.\u0023\u003DzUx3FRH0EnNc_ = new Stopwatch();
      \u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D.\u0023\u003DzUx3FRH0EnNc_.Start();
    }
    return \u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D.\u0023\u003DzUx3FRH0EnNc_.ElapsedTicks;
  }

  public void \u0023\u003DzCghbVqE\u003D()
  {
    if (this.\u0023\u003DzxhCmePtx6PYr != 0U)
      throw new NotImplementedException();
    this.\u0023\u003DzN0El7idsXiwE = this.\u0023\u003DzkUt0dbqm_eO6();
    \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dzp3SSAhg\u003D(this);
    ++this.\u0023\u003DzM8YvCqdng9uO;
    ++this.\u0023\u003DzxhCmePtx6PYr;
  }

  public bool \u0023\u003Dz14yvyRo\u003D() => this.\u0023\u003DzN0El7idsXiwE > 0L;

  public void \u0023\u003DzIqPbhiE\u003D()
  {
    if (this.\u0023\u003DzxhCmePtx6PYr == 0U)
      throw new InvalidOperationException();
    --this.\u0023\u003DzxhCmePtx6PYr;
    if (this.\u0023\u003DzxhCmePtx6PYr != 0U)
      return;
    long num1 = this.\u0023\u003DzkUt0dbqm_eO6() - this.\u0023\u003DzN0El7idsXiwE;
    this.\u0023\u003DzN0El7idsXiwE = 0L;
    double num2 = (double) num1 / (double) Stopwatch.Frequency;
    this.\u0023\u003DzAwVoCztpd7ox += num2;
    this.\u0023\u003DzKmatSefqd0TeJb4URLTZELU\u003D += num2;
    \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay.\u0023\u003DzFvAsfEI\u003D().\u0023\u003DzJEIks4EYOgFT(this, num2);
  }

  internal void Reset()
  {
    this.\u0023\u003DzN0El7idsXiwE = 0L;
    this.\u0023\u003DzxhCmePtx6PYr = 0U;
    this.\u0023\u003DzM8YvCqdng9uO = 0U;
    this.\u0023\u003DzAwVoCztpd7ox = 0.0;
    this.\u0023\u003DzKmatSefqd0TeJb4URLTZELU\u003D = 0.0;
  }

  public double \u0023\u003Dz502aPPQqdJZc() => this.\u0023\u003DzAwVoCztpd7ox;

  public double \u0023\u003DzAkyA2braTgIZWO09MwShOIdybNuS()
  {
    return this.\u0023\u003DzKmatSefqd0TeJb4URLTZELU\u003D;
  }
}
