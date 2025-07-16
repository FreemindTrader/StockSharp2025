// Decompiled with JetBrains decompiler
// Type: #=zeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
public sealed class \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay
{
  private List<\u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D> \u0023\u003Dzg6tcvbbqgix\u0024;
  private List<\u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D> \u0023\u003DzNfj_yrlKoHA\u0024;
  private static readonly \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay \u0023\u003DzWzbCnInuP5_S = new \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay();

  private \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay()
  {
    this.\u0023\u003DzNfj_yrlKoHA\u0024 = new List<\u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D>();
    this.\u0023\u003Dzg6tcvbbqgix\u0024 = new List<\u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D>();
  }

  public static \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay \u0023\u003DzFvAsfEI\u003D()
  {
    return \u0023\u003DzeHqydGt1MYwtwPKPfmmWnKpm0qsnCRrVLcBoiJu1rY232oMfVr72yIGQA4ay.\u0023\u003DzWzbCnInuP5_S;
  }

  public void \u0023\u003Dzp3SSAhg\u003D(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D _param1)
  {
    this.\u0023\u003Dzg6tcvbbqgix\u0024.Add(_param1);
  }

  public void \u0023\u003DzJEIks4EYOgFT(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D _param1,
    double _param2)
  {
    int count = this.\u0023\u003Dzg6tcvbbqgix\u0024.Count;
    if (count > 1)
      this.\u0023\u003Dzg6tcvbbqgix\u0024[count - 2].\u0023\u003DzKmatSefqd0TeJb4URLTZELU\u003D -= _param2;
    this.\u0023\u003Dzg6tcvbbqgix\u0024.RemoveAt(count - 1);
  }

  public void \u0023\u003Dzh_YrSog\u003D(
    \u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D _param1)
  {
    this.\u0023\u003DzNfj_yrlKoHA\u0024.Add(_param1);
  }

  public void Reset()
  {
    foreach (\u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D gi8m8xLSrGvDy8poU3Mw in this.\u0023\u003DzNfj_yrlKoHA\u0024)
      gi8m8xLSrGvDy8poU3Mw.Reset();
  }

  public string \u0023\u003Dzz2S8ZCo\u003D(double _param1)
  {
    StringBuilder stringBuilder = new StringBuilder("***************************************\n");
    stringBuilder.Append("Total  | No Subs| %Total |%No Subs| Name\n");
    foreach (\u0023\u003DzEJoJjwSelM_K3zbmiw1OA8iSu5G6NISegyeaxgatVzJifGI8m8xL_SrGvDY8poU3Mw\u003D\u003D gi8m8xLSrGvDy8poU3Mw in this.\u0023\u003DzNfj_yrlKoHA\u0024)
    {
      if (gi8m8xLSrGvDy8poU3Mw.\u0023\u003Dz502aPPQqdJZc() > 0.0)
      {
        stringBuilder.Append($"{$"{gi8m8xLSrGvDy8poU3Mw.\u0023\u003Dz502aPPQqdJZc() * 1000.0:000.00}"} | {$"{gi8m8xLSrGvDy8poU3Mw.\u0023\u003DzAkyA2braTgIZWO09MwShOIdybNuS() * 1000.0:000.00}"} | {$"{Math.Min(99.99, gi8m8xLSrGvDy8poU3Mw.\u0023\u003Dz502aPPQqdJZc() / _param1 * 100.0):00.00}"}% | {$"{gi8m8xLSrGvDy8poU3Mw.\u0023\u003DzAkyA2braTgIZWO09MwShOIdybNuS() / _param1 * 100.0:00.00}"}% | {gi8m8xLSrGvDy8poU3Mw.\u0023\u003DzsLDTRVg\u003D}");
        stringBuilder.Append($" ({gi8m8xLSrGvDy8poU3Mw.\u0023\u003DzM8YvCqdng9uO.ToString()})\n");
      }
    }
    stringBuilder.Append("\n\n");
    return stringBuilder.ToString();
  }

  public void \u0023\u003Dzj7RXIHDIGPdf(string _param1, double _param2)
  {
    FileStream fileStream = new FileStream(_param1, FileMode.Append, FileAccess.Write);
    StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
    streamWriter.Write(this.\u0023\u003Dzz2S8ZCo\u003D(_param2));
    streamWriter.Close();
    fileStream.Close();
  }
}
