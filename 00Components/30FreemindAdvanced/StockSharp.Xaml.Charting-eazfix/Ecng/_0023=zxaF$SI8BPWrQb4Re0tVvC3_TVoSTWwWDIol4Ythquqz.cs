// Decompiled with JetBrains decompiler
// Type: #=zxaF$SI8BPWrQb4Re0tVvC3_TVoSTWwWDIol4YthquqzlxPX9xYtmgulb3eVd185A3uAt8zrquSD6vbsuBcHljEs=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzxaF\u0024SI8BPWrQb4Re0tVvC3_TVoSTWwWDIol4YthquqzlxPX9xYtmgulb3eVd185A3uAt8zrquSD6vbsuBcHljEs\u003D
{
  private int \u0023\u003DzvFFOduPYCnMc;
  private int \u0023\u003DzFrPr752w6PiO;
  private int \u0023\u003Dz0TL7ot7K56Z3Gld_1w\u003D\u003D;
  private int \u0023\u003DzHiXXbJILTGZw8P7GLg\u003D\u003D;
  private int \u0023\u003DzYTRoyKyN3fBR;
  private int \u0023\u003DzOxmXVfz021CI;
  private int \u0023\u003DzqJqKSr_8a1p0;
  private int \u0023\u003DzHz_rNjOVuc_j;
  private int \u0023\u003DzixZ0aQvgqt4h;

  public \u0023\u003DzxaF\u0024SI8BPWrQb4Re0tVvC3_TVoSTWwWDIol4YthquqzlxPX9xYtmgulb3eVd185A3uAt8zrquSD6vbsuBcHljEs\u003D(
    int _param1,
    int _param2)
  {
    this.\u0023\u003DzvFFOduPYCnMc = _param1 * _param1;
    this.\u0023\u003DzFrPr752w6PiO = _param2 * _param2;
    this.\u0023\u003Dz0TL7ot7K56Z3Gld_1w\u003D\u003D = this.\u0023\u003DzvFFOduPYCnMc << 1;
    this.\u0023\u003DzHiXXbJILTGZw8P7GLg\u003D\u003D = this.\u0023\u003DzFrPr752w6PiO << 1;
    this.\u0023\u003DzYTRoyKyN3fBR = 0;
    this.\u0023\u003DzOxmXVfz021CI = 0;
    this.\u0023\u003DzqJqKSr_8a1p0 = 0;
    this.\u0023\u003DzHz_rNjOVuc_j = -_param2 * this.\u0023\u003Dz0TL7ot7K56Z3Gld_1w\u003D\u003D;
    this.\u0023\u003DzixZ0aQvgqt4h = 0;
  }

  public int \u0023\u003DzOTSgnUI\u003D() => this.\u0023\u003DzYTRoyKyN3fBR;

  public int \u0023\u003Dz5QE1hBg\u003D() => this.\u0023\u003DzOxmXVfz021CI;

  public void \u0023\u003DzXiQrjbw\u003D()
  {
    int num1;
    int num2 = num1 = this.\u0023\u003DzixZ0aQvgqt4h + this.\u0023\u003DzqJqKSr_8a1p0 + this.\u0023\u003DzFrPr752w6PiO;
    if (num2 < 0)
      num2 = -num2;
    int num3;
    int num4 = num3 = this.\u0023\u003DzixZ0aQvgqt4h + this.\u0023\u003DzHz_rNjOVuc_j + this.\u0023\u003DzvFFOduPYCnMc;
    if (num4 < 0)
      num4 = -num4;
    int num5;
    int num6 = num5 = this.\u0023\u003DzixZ0aQvgqt4h + this.\u0023\u003DzqJqKSr_8a1p0 + this.\u0023\u003DzFrPr752w6PiO + this.\u0023\u003DzHz_rNjOVuc_j + this.\u0023\u003DzvFFOduPYCnMc;
    if (num6 < 0)
      num6 = -num6;
    int num7 = num2;
    bool flag = true;
    if (num7 > num4)
    {
      num7 = num4;
      flag = false;
    }
    this.\u0023\u003DzYTRoyKyN3fBR = this.\u0023\u003DzOxmXVfz021CI = 0;
    if (num7 > num6)
    {
      this.\u0023\u003DzqJqKSr_8a1p0 += this.\u0023\u003DzHiXXbJILTGZw8P7GLg\u003D\u003D;
      this.\u0023\u003DzHz_rNjOVuc_j += this.\u0023\u003Dz0TL7ot7K56Z3Gld_1w\u003D\u003D;
      this.\u0023\u003DzixZ0aQvgqt4h = num5;
      this.\u0023\u003DzYTRoyKyN3fBR = 1;
      this.\u0023\u003DzOxmXVfz021CI = 1;
    }
    else if (flag)
    {
      this.\u0023\u003DzqJqKSr_8a1p0 += this.\u0023\u003DzHiXXbJILTGZw8P7GLg\u003D\u003D;
      this.\u0023\u003DzixZ0aQvgqt4h = num1;
      this.\u0023\u003DzYTRoyKyN3fBR = 1;
    }
    else
    {
      this.\u0023\u003DzHz_rNjOVuc_j += this.\u0023\u003Dz0TL7ot7K56Z3Gld_1w\u003D\u003D;
      this.\u0023\u003DzixZ0aQvgqt4h = num3;
      this.\u0023\u003DzOxmXVfz021CI = 1;
    }
  }
}
