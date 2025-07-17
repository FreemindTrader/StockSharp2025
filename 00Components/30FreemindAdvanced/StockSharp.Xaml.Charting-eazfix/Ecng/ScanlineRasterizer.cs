// Decompiled with JetBrains decompiler
// Type: #=z7tyVhFVuY8D5V$lqfWwb5cSIEExO0Gz6ZxLt_FRFNLJV2Xwd3iZoJUs8OqJAgj8X8ZOxZ8e2v_j5pzF77A==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class ScanlineRasterizer : 
  IRasterizer
{
  private \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiZot20XZZLU_SBymsgRVXpM61Ax5QggCbu8 \u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D;
  private VectorClipper \u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D;
  private int[] \u0023\u003DzY\u0024iy3H6MDQlk = new int[256 /*0x0100*/];
  private agg_basics.\u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D \u0023\u003DzVTC2U6dVKqCvrBDpM31y0b8\u003D;
  private bool \u0023\u003Dz_j7wBeHNUNvs;
  private int \u0023\u003Dz0TSvb7PdghSI;
  private int \u0023\u003Dzm1Oo\u002446Mw4uy;
  private ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D \u0023\u003DzMBCBPKkO8rfJ;
  private int \u0023\u003DzaytbZH\u00245LIlA;

  public ScanlineRasterizer()
    : this(new VectorClipper())
  {
  }

  public ScanlineRasterizer(
    VectorClipper _param1)
  {
    this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D = new \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xCW0709RsZ9Zq1vDAFmVgiZot20XZZLU_SBymsgRVXpM61Ax5QggCbu8();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D = _param1;
    this.\u0023\u003DzVTC2U6dVKqCvrBDpM31y0b8\u003D = (agg_basics.\u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D) 0;
    this.\u0023\u003Dz_j7wBeHNUNvs = true;
    this.\u0023\u003Dz0TSvb7PdghSI = 0;
    this.\u0023\u003Dzm1Oo\u002446Mw4uy = 0;
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 0;
    for (int index = 0; index < 256 /*0x0100*/; ++index)
      this.\u0023\u003DzY\u0024iy3H6MDQlk[index] = index;
  }

  public void \u0023\u003Dzp_DWHgc\u003D()
  {
    this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 0;
  }

  public void \u0023\u003DzThyAY8tY1g4ajpiZ6g\u003D\u003D()
  {
    this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzThyAY8tY1g4ajpiZ6g\u003D\u003D();
  }

  public RectangleDouble \u0023\u003Dz8RHjH3tn\u0024OgD()
  {
    return new RectangleDouble((double) this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.clipBox.\u0023\u003DzP4R7yU0\u003D), (double) this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.clipBox.\u0023\u003DzRNV_Dpk\u003D), (double) this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.clipBox.\u0023\u003Dzp55dtus\u003D), (double) this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.clipBox.\u0023\u003DzSzOWcj8\u003D));
  }

  public void \u0023\u003DzBzsW4htHBBY5(
    RectangleDouble _param1)
  {
    this.\u0023\u003DzBzsW4htHBBY5(_param1.\u0023\u003DzP4R7yU0\u003D, _param1.\u0023\u003DzRNV_Dpk\u003D, _param1.\u0023\u003Dzp55dtus\u003D, _param1.\u0023\u003DzSzOWcj8\u003D);
  }

  public void \u0023\u003DzBzsW4htHBBY5(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzbRHAWK1yd7\u00242(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param2), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param3), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param4));
  }

  public void \u0023\u003Dz9oma1EeDllFlgo5SLQ\u003D\u003D(
    agg_basics.\u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D _param1)
  {
    this.\u0023\u003DzVTC2U6dVKqCvrBDpM31y0b8\u003D = _param1;
  }

  public void \u0023\u003DzrDsTuH3\u0024W2uL(bool _param1)
  {
    this.\u0023\u003Dz_j7wBeHNUNvs = _param1;
  }

  public void \u0023\u003DzruxDfy35wG0J(
    IGammaFunction _param1)
  {
    for (int index = 0; index < 256 /*0x0100*/; ++index)
      this.\u0023\u003DzY\u0024iy3H6MDQlk[index] = agg_basics.\u0023\u003DzROReRE0C5MV7(_param1.\u0023\u003DzoxmYZFvB84ZN((double) index / (double) byte.MaxValue) * (double) byte.MaxValue);
  }

  private void \u0023\u003Dz_udhSki6JNxe(int _param1, int _param2)
  {
    if (this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    if (this.\u0023\u003Dz_j7wBeHNUNvs)
      this.\u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dz_udhSki6JNxe(this.\u0023\u003Dz0TSvb7PdghSI = this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param1), this.\u0023\u003Dzm1Oo\u002446Mw4uy = this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param2));
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 1;
  }

  private void \u0023\u003DzZi1kLvzptRpH(int _param1, int _param2)
  {
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D, this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param2));
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 2;
  }

  public void \u0023\u003DzAcNdpbnU8gEt(double _param1, double _param2)
  {
    if (this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    if (this.\u0023\u003Dz_j7wBeHNUNvs)
      this.\u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dz_udhSki6JNxe(this.\u0023\u003Dz0TSvb7PdghSI = this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param1), this.\u0023\u003Dzm1Oo\u002446Mw4uy = this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param2));
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 1;
  }

  public void \u0023\u003DzzwT9Ppmp4oNY(double _param1, double _param2)
  {
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D, this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param2));
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 2;
  }

  public void \u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D()
  {
    if (this.\u0023\u003DzMBCBPKkO8rfJ != (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 2)
      return;
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D, this.\u0023\u003Dz0TSvb7PdghSI, this.\u0023\u003Dzm1Oo\u002446Mw4uy);
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 3;
  }

  private void \u0023\u003DzNVaj9ergOekrrLbWLQ\u003D\u003D(
    double _param1,
    double _param2,
    Path.\u0023\u003Dz9kUnn38\u003D _param3)
  {
    if (Path.\u0023\u003DzxoYPNFH0kpDd(_param3))
      this.\u0023\u003DzAcNdpbnU8gEt(_param1, _param2);
    else if (Path.\u0023\u003DzepfxPD_ghBSfgm\u0024Sfw\u003D\u003D(_param3))
    {
      this.\u0023\u003DzzwT9Ppmp4oNY(_param1, _param2);
    }
    else
    {
      if (!Path.\u0023\u003DzsnnNWhx8JhO_(_param3))
        return;
      this.\u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D();
    }
  }

  private void \u0023\u003DzDV2n1hE\u003D(int _param1, int _param2, int _param3, int _param4)
  {
    if (this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dz_udhSki6JNxe(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param2));
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D, this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param3), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dzwg324_cPKCLU1ct79w\u003D\u003D(_param4));
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 1;
  }

  private void \u0023\u003DztdxBDu6EtVY5(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    if (this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003Dz_udhSki6JNxe(this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param1), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param2));
    this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzZi1kLvzptRpH(this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D, this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param3), this.\u0023\u003DztiwBhDbLuFUOsQrrVw\u003D\u003D.\u0023\u003DzE77D4bxULcsn(_param4));
    this.\u0023\u003DzMBCBPKkO8rfJ = (ScanlineRasterizer.\u0023\u003DzN1ACYJw\u003D) 1;
  }

  public void \u0023\u003DzR7pES1JXr5Q9(
    IVertexSource _param1)
  {
    this.\u0023\u003DzR7pES1JXr5Q9(_param1, 0);
  }

  public void \u0023\u003DzR7pES1JXr5Q9(
    IVertexSource _param1,
    int _param2)
  {
    double num1 = 0.0;
    double num2 = 0.0;
    _param1.\u0023\u003DzVawdK5C5Lyf_(_param2);
    if (this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzsWD0_GQ\u003D())
      this.\u0023\u003Dzp_DWHgc\u003D();
    Path.\u0023\u003Dz9kUnn38\u003D z9kUnn38;
    while (!Path.\u0023\u003DzVHztYKNVoUMf(z9kUnn38 = _param1.\u0023\u003DzxfekdAs1X3YM(out num1, out num2)))
      this.\u0023\u003DzNVaj9ergOekrrLbWLQ\u003D\u003D(num1, num2, z9kUnn38);
  }

  public int \u0023\u003DztsdJUYff4ZID()
  {
    return this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DztsdJUYff4ZID();
  }

  public int \u0023\u003Dz1IC3EdsxAbMh()
  {
    return this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003Dz1IC3EdsxAbMh();
  }

  public int \u0023\u003DzZXzraonFIgZ4()
  {
    return this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzZXzraonFIgZ4();
  }

  public int \u0023\u003DzF\u0024mRZMMmE83X()
  {
    return this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzF\u0024mRZMMmE83X();
  }

  private void \u0023\u003Dzmt1LDos\u003D()
  {
    if (this.\u0023\u003Dz_j7wBeHNUNvs)
      this.\u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D();
    this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzTwhqVHorS0SA();
  }

  public bool \u0023\u003DzptZK7icdgElf2pwnqtUfz44glEoM()
  {
    if (this.\u0023\u003Dz_j7wBeHNUNvs)
      this.\u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D();
    this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzTwhqVHorS0SA();
    if (this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzDfpyjYuVSpvp() == 0)
      return false;
    this.\u0023\u003DzaytbZH\u00245LIlA = this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003Dz1IC3EdsxAbMh();
    return true;
  }

  private bool \u0023\u003Dz0YELd17TIkydbHrlQg\u003D\u003D(int _param1)
  {
    if (this.\u0023\u003Dz_j7wBeHNUNvs)
      this.\u0023\u003DzsRNLsf05Xrz4fphw7Q\u003D\u003D();
    this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzTwhqVHorS0SA();
    if (this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzDfpyjYuVSpvp() == 0 || _param1 < this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003Dz1IC3EdsxAbMh() || _param1 > this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzF\u0024mRZMMmE83X())
      return false;
    this.\u0023\u003DzaytbZH\u00245LIlA = _param1;
    return true;
  }

  public int \u0023\u003Dz8\u0024atMZyZXD32R_3joHEocOM\u003D(int _param1)
  {
    int index = _param1 >> 9;
    if (index < 0)
      index = -index;
    if (this.\u0023\u003DzVTC2U6dVKqCvrBDpM31y0b8\u003D == (agg_basics.\u0023\u003Dz4wqgBpCiq\u00244vrUn_o2ab9Es\u003D) 1)
    {
      index &= 511 /*0x01FF*/;
      if (index > 256 /*0x0100*/)
        index = 512 /*0x0200*/ - index;
    }
    if (index > (int) byte.MaxValue)
      index = (int) byte.MaxValue;
    return this.\u0023\u003DzY\u0024iy3H6MDQlk[index];
  }

  public bool \u0023\u003DzVDkk5LAF0tc28W2lbQYHPVM\u003D(
    IScanlineCache _param1)
  {
    for (; this.\u0023\u003DzaytbZH\u00245LIlA <= this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzF\u0024mRZMMmE83X(); ++this.\u0023\u003DzaytbZH\u00245LIlA)
    {
      _param1.\u0023\u003Dz_qg8im_\u0024N_mX();
      int num1 = this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzkhL6vQ3dVWuWmnG7awTRHY0\u003D(this.\u0023\u003DzaytbZH\u00245LIlA);
      \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb[] quLbcdO8tFdwl0NmbArray;
      int index;
      this.\u0023\u003Dz6VF1vB6CCe5LW_FBpQ\u003D\u003D.\u0023\u003DzM55RxAl19tGHk1q0ZA\u003D\u003D(this.\u0023\u003DzaytbZH\u00245LIlA, out quLbcdO8tFdwl0NmbArray, out index);
      int num2 = 0;
      while (num1 != 0)
      {
        \u0023\u003DzJXDjnZfs8tGoFCupfSBAnx2SkKNxEpQGr36F8brBXcuQuLBcdO8tFDwl0NMb quLbcdO8tFdwl0Nmb = quLbcdO8tFdwl0NmbArray[index];
        int zwP120vA = quLbcdO8tFdwl0Nmb.\u0023\u003DzwP120vA\u003D;
        int zy5Rews = quLbcdO8tFdwl0Nmb.\u0023\u003Dzy_5REws\u003D;
        num2 += quLbcdO8tFdwl0Nmb.\u0023\u003Dzu6bzeD\u0024Y6N55;
        while (--num1 != 0)
        {
          ++index;
          quLbcdO8tFdwl0Nmb = quLbcdO8tFdwl0NmbArray[index];
          if (quLbcdO8tFdwl0Nmb.\u0023\u003DzwP120vA\u003D == zwP120vA)
          {
            zy5Rews += quLbcdO8tFdwl0Nmb.\u0023\u003Dzy_5REws\u003D;
            num2 += quLbcdO8tFdwl0Nmb.\u0023\u003Dzu6bzeD\u0024Y6N55;
          }
          else
            break;
        }
        if (zy5Rews != 0)
        {
          int num3 = this.\u0023\u003Dz8\u0024atMZyZXD32R_3joHEocOM\u003D((num2 << 9) - zy5Rews);
          if (num3 != 0)
            _param1.\u0023\u003Dzzb9PhvYi_sP8(zwP120vA, num3);
          ++zwP120vA;
        }
        if (num1 != 0 && quLbcdO8tFdwl0Nmb.\u0023\u003DzwP120vA\u003D > zwP120vA)
        {
          int num4 = this.\u0023\u003Dz8\u0024atMZyZXD32R_3joHEocOM\u003D(num2 << 9);
          if (num4 != 0)
            _param1.\u0023\u003DzqJOc77USXqlC(zwP120vA, quLbcdO8tFdwl0Nmb.\u0023\u003DzwP120vA\u003D - zwP120vA, num4);
        }
      }
      if (_param1.\u0023\u003DzAK83aWWCVpNB() != 0)
      {
        _param1.\u0023\u003DzDoZMoNygQSg8PGaYqw\u003D\u003D(this.\u0023\u003DzaytbZH\u00245LIlA);
        ++this.\u0023\u003DzaytbZH\u00245LIlA;
        return true;
      }
    }
    return false;
  }

  private bool \u0023\u003Dzc7hJRHEN9Ch4(int _param1, int _param2)
  {
    return this.\u0023\u003Dz0YELd17TIkydbHrlQg\u003D\u003D(_param2);
  }

  public enum \u0023\u003Dz3k\u0024kVPyJTsQg
  {
  }

  public enum \u0023\u003DzN1ACYJw\u003D
  {
  }
}
