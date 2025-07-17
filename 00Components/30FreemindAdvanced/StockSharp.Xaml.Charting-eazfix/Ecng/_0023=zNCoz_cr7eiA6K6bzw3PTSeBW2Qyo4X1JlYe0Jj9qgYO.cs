// Decompiled with JetBrains decompiler
// Type: #=zNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
public sealed class \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D : 
  IImage,
  IImageFloat
{
  protected int[] \u0023\u003Dz7Y6yp9wrhlh7;
  protected int[] \u0023\u003DzKei7JjOVfWOI;
  private float[] \u0023\u003DznaRoot_wQHeu;
  private int \u0023\u003DzSNURIo8cO\u0024iR;
  private int \u0023\u003Dz4ohp_xe1a9WAapdZZA\u003D\u003D;
  private int \u0023\u003Dz9LgI12vZMy\u0024F;
  private int \u0023\u003Dzh_FblSMKXulX;
  private int \u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D;
  private int \u0023\u003Dz\u0024wxbMCh_3XSWn2xljEt8YDpBlP5T;
  private int \u0023\u003DzppSlQB5wmDk_;
  private Vector2 \u0023\u003DzGBMU9r9dm\u0024nB = new Vector2(0.0, 0.0);
  private \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D \u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D;
  private int \u0023\u003DzbLh66mY\u003D;

  public \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D()
  {
  }

  public \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D(
    \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D _param1)
  {
    this.\u0023\u003DzgDbPoFoJnrrrHveVDA\u003D\u003D(_param1);
  }

  public \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D(
    IImageFloat _param1,
    \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D _param2)
  {
    this.\u0023\u003DzoEcAzFBHboD2PhE9QAhTock\u003D(_param1.Width, _param1.Height, _param1.\u0023\u003DzzzpYZKdbZZc8Pfg5Vw\u003D\u003D(), _param1.\u0023\u003DzHstjD51XfGa0(), _param1.\u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D());
    int num = _param1.\u0023\u003DzHlHGfKJZNJsq(0, 0);
    float[] numArray1 = _param1.\u0023\u003Dz9b1_JhA\u003D();
    float[] numArray2 = new float[numArray1.Length];
    agg_basics.\u0023\u003DzQBDTcr7NcY0a(numArray2, num, numArray1, num, numArray1.Length - num);
    this.\u0023\u003DzdrMgpSc\u003D(numArray2, num);
    this.\u0023\u003DzgDbPoFoJnrrrHveVDA\u003D\u003D(_param2);
  }

  public \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D _param4)
  {
    this.\u0023\u003DzWIEZ7Zw\u003D(_param1, _param2, _param1 * (_param3 / 32 /*0x20*/), _param3);
    this.\u0023\u003DzgDbPoFoJnrrrHveVDA\u003D\u003D(_param4);
  }

  public \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D(
    IImageFloat _param1,
    \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DzoEcAzFBHboD2PhE9QAhTock\u003D(_param1.Width, _param1.Height, _param1.\u0023\u003DzzzpYZKdbZZc8Pfg5Vw\u003D\u003D(), _param5, _param3);
    _param1.\u0023\u003DzHlHGfKJZNJsq(0, 0);
    int length = _param1.\u0023\u003Dz9b1_JhA\u003D().Length;
    throw new NotImplementedException();
  }

  public int \u0023\u003Dzlp_WDNCl80Mj() => this.\u0023\u003DzbLh66mY\u003D;

  public void \u0023\u003DzuO1BGcahegmc() => ++this.\u0023\u003DzbLh66mY\u003D;

  public void \u0023\u003DzpkI5N4G_lFqr(
    float[] _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7)
  {
    this.\u0023\u003DznaRoot_wQHeu = (float[]) null;
    this.\u0023\u003DzoEcAzFBHboD2PhE9QAhTock\u003D(_param3, _param4, _param5, _param6, _param7);
    this.\u0023\u003DzdrMgpSc\u003D(_param1, _param2);
  }

  public void \u0023\u003Dz3YqGsmQ\u003D(
    IImageFloat _param1,
    \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DzoEcAzFBHboD2PhE9QAhTock\u003D(_param1.Width, _param1.Height, _param1.\u0023\u003DzzzpYZKdbZZc8Pfg5Vw\u003D\u003D(), _param5, _param3);
    int num = _param1.\u0023\u003DzHlHGfKJZNJsq(0, 0);
    this.\u0023\u003DzdrMgpSc\u003D(_param1.\u0023\u003Dz9b1_JhA\u003D(), num + _param4);
    this.\u0023\u003DzgDbPoFoJnrrrHveVDA\u003D\u003D(_param2);
  }

  public void \u0023\u003Dz3YqGsmQ\u003D(
    IImageFloat _param1,
    \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D _param2)
  {
    this.\u0023\u003Dz3YqGsmQ\u003D(_param1, _param2, _param1.\u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D(), 0, _param1.\u0023\u003DzHstjD51XfGa0());
  }

  public bool \u0023\u003Dz3YqGsmQ\u003D(
    IImageFloat _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    this.\u0023\u003DznaRoot_wQHeu = (float[]) null;
    this.\u0023\u003Dz6dSZRTGrExKsSL6\u0024lg\u003D\u003D();
    if (_param2 > _param4 || _param3 > _param5)
      throw new Exception("You need to have your x1 and y1 be the lower left corner of your sub image.");
    RectangleInt xvhBayrK2CmzoKas = new RectangleInt(_param2, _param3, _param4, _param5);
    if (!xvhBayrK2CmzoKas.\u0023\u003DzPHB5nPY\u003D(new RectangleInt(0, 0, _param1.Width - 1, _param1.Height - 1)))
      return false;
    this.\u0023\u003DzoEcAzFBHboD2PhE9QAhTock\u003D(xvhBayrK2CmzoKas.Width, xvhBayrK2CmzoKas.Height, _param1.\u0023\u003DzzzpYZKdbZZc8Pfg5Vw\u003D\u003D(), _param1.\u0023\u003DzHstjD51XfGa0(), _param1.\u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D());
    int num = _param1.\u0023\u003DzHlHGfKJZNJsq(xvhBayrK2CmzoKas.\u0023\u003DzP4R7yU0\u003D, xvhBayrK2CmzoKas.\u0023\u003DzRNV_Dpk\u003D);
    this.\u0023\u003DzdrMgpSc\u003D(_param1.\u0023\u003Dz9b1_JhA\u003D(), num);
    return true;
  }

  public void \u0023\u003DzQlhuVZc\u003D(byte _param1)
  {
    if (this.\u0023\u003DzHstjD51XfGa0() != 32 /*0x20*/)
      throw new Exception($"You don't have alpha channel to set.  Your image has a bit depth of {this.\u0023\u003DzHstjD51XfGa0().ToString()}.");
    int num1 = this.Width * this.Height;
    int num2;
    float[] numArray = this.\u0023\u003Dz9b1_JhA\u003D(out num2);
    for (int index = 0; index < num1; ++index)
      numArray[num2 + index * 4 + 3] = (float) _param1;
  }

  private void \u0023\u003Dz05SWyVw\u003D()
  {
    this.\u0023\u003DznaRoot_wQHeu = (float[]) null;
    this.\u0023\u003DzoEcAzFBHboD2PhE9QAhTock\u003D(0, 0, 0, 32 /*0x20*/, 4);
  }

  public void \u0023\u003DzWIEZ7Zw\u003D(int _param1, int _param2, int _param3, int _param4)
  {
    if (_param4 != 128 /*0x80*/ && _param4 != 96 /*0x60*/ && _param4 != 32 /*0x20*/)
      throw new Exception("Unsupported bits per pixel.");
    if (_param3 < _param1 * (_param4 / 32 /*0x20*/))
      throw new Exception("Your scan width is not big enough to hold your width and height.");
    this.\u0023\u003DzoEcAzFBHboD2PhE9QAhTock\u003D(_param1, _param2, _param3, _param4, _param4 / 32 /*0x20*/);
    this.\u0023\u003DznaRoot_wQHeu = new float[this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D * this.\u0023\u003Dzh_FblSMKXulX];
    this.\u0023\u003DziiJRC89LTMtY();
  }

  public Graphics2D \u0023\u003Dz9Yt\u0024vKcgxNiu()
  {
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D.\u0023\u003Dzo5XZ2jPFuNIh5iLb1w\u003D\u003D xz2jPfuNih5iLb1w = new \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D.\u0023\u003Dzo5XZ2jPFuNIh5iLb1w\u003D\u003D(this);
    xz2jPfuNih5iLb1w.\u0023\u003DzpFPvRIy2dIIbCgyeOpBw_g0\u003D().\u0023\u003DzBzsW4htHBBY5(0.0, 0.0, (double) this.Width, (double) this.Height);
    return (Graphics2D) xz2jPfuNih5iLb1w;
  }

  public void \u0023\u003DzCadMMgc\u003D(
    IImageFloat _param1)
  {
    this.\u0023\u003DzCadMMgc\u003D(_param1, _param1.\u0023\u003DzFghv7EA\u003D(), 0, 0);
  }

  protected void \u0023\u003DzeO3pDJyWLhgh(
    IImageFloat _param1,
    RectangleInt _param2,
    int _param3,
    int _param4)
  {
    if (this.\u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D() != this.\u0023\u003DzHstjD51XfGa0() / 32 /*0x20*/ || _param1.\u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D() != _param1.\u0023\u003DzHstjD51XfGa0() / 32 /*0x20*/)
      throw new Exception("WIP we only support packed pixel formats at this time.");
    if (this.\u0023\u003DzHstjD51XfGa0() == _param1.\u0023\u003DzHstjD51XfGa0())
    {
      int num1 = _param2.Width * this.\u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D();
      int num2 = _param1.\u0023\u003DzHlHGfKJZNJsq(_param2.\u0023\u003DzP4R7yU0\u003D, _param2.\u0023\u003DzRNV_Dpk\u003D);
      float[] numArray1 = _param1.\u0023\u003Dz9b1_JhA\u003D();
      int num3;
      float[] numArray2 = this.\u0023\u003DznPLKTp_rfCU9(_param2.\u0023\u003DzP4R7yU0\u003D + _param3, _param2.\u0023\u003DzRNV_Dpk\u003D + _param4, out num3);
      for (int index = 0; index < _param2.Height; ++index)
      {
        agg_basics.\u0023\u003DzifsHAOd0B2qk(numArray2, num3, numArray1, num2, num1);
        num2 += _param1.\u0023\u003DzzzpYZKdbZZc8Pfg5Vw\u003D\u003D();
        num3 += this.\u0023\u003DzzzpYZKdbZZc8Pfg5Vw\u003D\u003D();
      }
    }
    else
    {
      bool flag = true;
      if (_param1.\u0023\u003DzHstjD51XfGa0() == 24)
      {
        if (this.\u0023\u003DzHstjD51XfGa0() == 32 /*0x20*/)
        {
          int width = _param2.Width;
          for (int zRnvDpk = _param2.\u0023\u003DzRNV_Dpk\u003D; zRnvDpk < _param2.\u0023\u003DzSzOWcj8\u003D; ++zRnvDpk)
          {
            int num4 = _param1.\u0023\u003DzHlHGfKJZNJsq(_param2.\u0023\u003DzP4R7yU0\u003D, _param2.\u0023\u003DzRNV_Dpk\u003D + zRnvDpk);
            float[] numArray3 = _param1.\u0023\u003Dz9b1_JhA\u003D();
            int num5;
            float[] numArray4 = this.\u0023\u003DznPLKTp_rfCU9(_param2.\u0023\u003DzP4R7yU0\u003D + _param3, _param2.\u0023\u003DzRNV_Dpk\u003D + zRnvDpk + _param4, out num5);
            for (int index1 = 0; index1 < width; ++index1)
            {
              float[] numArray5 = numArray4;
              int index2 = num5;
              int num6 = index2 + 1;
              float[] numArray6 = numArray3;
              int index3 = num4;
              int num7 = index3 + 1;
              double num8 = (double) numArray6[index3];
              numArray5[index2] = (float) num8;
              float[] numArray7 = numArray4;
              int index4 = num6;
              int num9 = index4 + 1;
              float[] numArray8 = numArray3;
              int index5 = num7;
              int num10 = index5 + 1;
              double num11 = (double) numArray8[index5];
              numArray7[index4] = (float) num11;
              float[] numArray9 = numArray4;
              int index6 = num9;
              int num12 = index6 + 1;
              float[] numArray10 = numArray3;
              int index7 = num10;
              num4 = index7 + 1;
              double num13 = (double) numArray10[index7];
              numArray9[index6] = (float) num13;
              float[] numArray11 = numArray4;
              int index8 = num12;
              num5 = index8 + 1;
              numArray11[index8] = (float) byte.MaxValue;
            }
          }
        }
        else
          flag = false;
      }
      else
        flag = false;
      if (!flag)
        throw new NotImplementedException($"You need to write the {_param1.\u0023\u003DzHstjD51XfGa0().ToString()} to {this.\u0023\u003DzHstjD51XfGa0().ToString()} conversion");
    }
  }

  public void \u0023\u003DzCadMMgc\u003D(
    IImageFloat _param1,
    RectangleInt _param2,
    int _param3,
    int _param4)
  {
    RectangleInt xvhBayrK2CmzoKas1 = _param1.\u0023\u003DzFghv7EA\u003D();
    RectangleInt xvhBayrK2CmzoKas2 = new RectangleInt();
    if (!xvhBayrK2CmzoKas2.\u0023\u003DzvBhLthYerYntQLEPioo2lZw\u003D(_param2, xvhBayrK2CmzoKas1))
      return;
    RectangleInt xvhBayrK2CmzoKas3 = xvhBayrK2CmzoKas2;
    xvhBayrK2CmzoKas3.\u0023\u003DznR9\u00242Eg\u003D(_param3, _param4);
    RectangleInt xvhBayrK2CmzoKas4 = this.\u0023\u003DzFghv7EA\u003D();
    RectangleInt xvhBayrK2CmzoKas5 = new RectangleInt();
    if (!xvhBayrK2CmzoKas5.\u0023\u003DzvBhLthYerYntQLEPioo2lZw\u003D(xvhBayrK2CmzoKas3, xvhBayrK2CmzoKas4))
      return;
    RectangleInt xvhBayrK2CmzoKas6 = xvhBayrK2CmzoKas5;
    xvhBayrK2CmzoKas6.\u0023\u003DznR9\u00242Eg\u003D(-_param3, -_param4);
    this.\u0023\u003DzeO3pDJyWLhgh(_param1, xvhBayrK2CmzoKas6, _param3, _param4);
  }

  [SpecialName]
  public Vector2 \u0023\u003DzSm8CGbTqdI1U()
  {
    return this.\u0023\u003DzGBMU9r9dm\u0024nB;
  }

  [SpecialName]
  public void \u0023\u003DzEN24ToKSPvcM(
    Vector2 _param1)
  {
    this.\u0023\u003DzGBMU9r9dm\u0024nB = _param1;
  }

  public int Width => this.\u0023\u003Dz9LgI12vZMy\u0024F;

  public int Height => this.\u0023\u003Dzh_FblSMKXulX;

  public int \u0023\u003DzzzpYZKdbZZc8Pfg5Vw\u003D\u003D()
  {
    return this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D;
  }

  public int \u0023\u003DzY_LHm_Vxz84xXBUPBw\u003D\u003D()
  {
    return Math.Abs(this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D);
  }

  public int \u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D()
  {
    return this.\u0023\u003Dz\u0024wxbMCh_3XSWn2xljEt8YDpBlP5T;
  }

  [SpecialName]
  public int \u0023\u003DzHstjD51XfGa0() => this.\u0023\u003DzppSlQB5wmDk_;

  public virtual RectangleInt \u0023\u003DzFghv7EA\u003D()
  {
    return new RectangleInt(-(int) this.\u0023\u003DzGBMU9r9dm\u0024nB.dje_z3GBAX47U_ejd, -(int) this.\u0023\u003DzGBMU9r9dm\u0024nB.dje_zLPL6EZPA_ejd, this.Width - (int) this.\u0023\u003DzGBMU9r9dm\u0024nB.dje_z3GBAX47U_ejd, this.Height - (int) this.\u0023\u003DzGBMU9r9dm\u0024nB.dje_zLPL6EZPA_ejd);
  }

  public \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D \u0023\u003DztvwmoyKu8oUlLP4_gA\u003D\u003D()
  {
    return this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D;
  }

  public void \u0023\u003DzgDbPoFoJnrrrHveVDA\u003D\u003D(
    \u0023\u003DzKasBY8yFp0kHGchcdspopAZazsv9Xi3W4H2ktC4tix4XTs9ceql5jwjqb5HosybZ1aLYsVs\u003D _param1)
  {
    this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D = _param1 == null || _param1.\u0023\u003DzVAoYC\u0024MJfjKU3EUhiA\u003D\u003D() == this.\u0023\u003DzHstjD51XfGa0() ? _param1 : throw new NotSupportedException("The blender has to support the bit depth of this image.");
  }

  private void \u0023\u003DziiJRC89LTMtY()
  {
    this.\u0023\u003Dz7Y6yp9wrhlh7 = new int[this.\u0023\u003Dzh_FblSMKXulX];
    for (int index = 0; index < this.\u0023\u003Dzh_FblSMKXulX; ++index)
      this.\u0023\u003Dz7Y6yp9wrhlh7[index] = index * this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D;
    this.\u0023\u003DzKei7JjOVfWOI = new int[this.\u0023\u003Dz9LgI12vZMy\u0024F];
    for (int index = 0; index < this.\u0023\u003Dz9LgI12vZMy\u0024F; ++index)
      this.\u0023\u003DzKei7JjOVfWOI[index] = index * this.\u0023\u003Dz\u0024wxbMCh_3XSWn2xljEt8YDpBlP5T;
  }

  public void \u0023\u003Dzc7jB9VM\u003D()
  {
    this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D *= -1;
    this.\u0023\u003Dz4ohp_xe1a9WAapdZZA\u003D\u003D = this.\u0023\u003DzSNURIo8cO\u0024iR;
    if (this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D < 0)
      this.\u0023\u003Dz4ohp_xe1a9WAapdZZA\u003D\u003D = -((this.\u0023\u003Dzh_FblSMKXulX - 1) * this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D) + this.\u0023\u003DzSNURIo8cO\u0024iR;
    this.\u0023\u003DziiJRC89LTMtY();
  }

  public void \u0023\u003DzdrMgpSc\u003D(float[] _param1, int _param2)
  {
    if (_param1.Length < this.\u0023\u003Dzh_FblSMKXulX * this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D)
      throw new Exception("Your buffer does not have enough room it it for your height and strideInBytes.");
    this.\u0023\u003DznaRoot_wQHeu = _param1;
    this.\u0023\u003DzSNURIo8cO\u0024iR = this.\u0023\u003Dz4ohp_xe1a9WAapdZZA\u003D\u003D = _param2;
    if (this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D < 0)
      this.\u0023\u003Dz4ohp_xe1a9WAapdZZA\u003D\u003D = -((this.\u0023\u003Dzh_FblSMKXulX - 1) * this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D) + this.\u0023\u003DzSNURIo8cO\u0024iR;
    this.\u0023\u003DziiJRC89LTMtY();
  }

  private void \u0023\u003DzoEcAzFBHboD2PhE9QAhTock\u003D(
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    if (this.\u0023\u003DznaRoot_wQHeu != null)
      throw new Exception("You already have a buffer set. You need to set dimmensoins before the buffer.  You may need to clear the buffer first.");
    this.\u0023\u003Dz9LgI12vZMy\u0024F = _param1;
    this.\u0023\u003Dzh_FblSMKXulX = _param2;
    this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D = _param3;
    this.\u0023\u003DzppSlQB5wmDk_ = _param4;
    if (_param5 > 4)
      throw new Exception("It looks like you are passing bits per pixel rather than distance in Floats.");
    this.\u0023\u003Dz\u0024wxbMCh_3XSWn2xljEt8YDpBlP5T = _param5 >= _param4 / 32 /*0x20*/ ? _param5 : throw new Exception("You do not have enough room between pixels to support your bit depth.");
    if (_param3 < _param5 * _param1)
      throw new Exception("You do not have enough strideInFloats to hold the width and pixel distance you have described.");
  }

  public void \u0023\u003Dz6dSZRTGrExKsSL6\u0024lg\u003D\u003D()
  {
    this.\u0023\u003DznaRoot_wQHeu = (float[]) null;
    this.\u0023\u003Dz9LgI12vZMy\u0024F = this.\u0023\u003Dzh_FblSMKXulX = this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D = this.\u0023\u003Dz\u0024wxbMCh_3XSWn2xljEt8YDpBlP5T = 0;
  }

  public float[] \u0023\u003Dz9b1_JhA\u003D() => this.\u0023\u003DznaRoot_wQHeu;

  public float[] \u0023\u003Dz9b1_JhA\u003D(out int _param1)
  {
    _param1 = this.\u0023\u003DzSNURIo8cO\u0024iR;
    return this.\u0023\u003DznaRoot_wQHeu;
  }

  public float[] \u0023\u003DzVQUKaPRRo4mI(int _param1, out int _param2)
  {
    _param2 = this.\u0023\u003Dz4ohp_xe1a9WAapdZZA\u003D\u003D + this.\u0023\u003Dz7Y6yp9wrhlh7[_param1];
    return this.\u0023\u003DznaRoot_wQHeu;
  }

  public float[] \u0023\u003DznPLKTp_rfCU9(int _param1, int _param2, out int _param3)
  {
    _param3 = this.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    return this.\u0023\u003DznaRoot_wQHeu;
  }

  public \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D \u0023\u003DzFiIk5SM\u003D(
    int _param1,
    int _param2)
  {
    return this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003DzpXQh1U84EY2nUbOFY6f_7N0\u003D(this.\u0023\u003DznaRoot_wQHeu, this.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2));
  }

  public virtual void \u0023\u003DzR2zHA_0\u003D(
    int _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param3)
  {
    _param1 -= (int) this.\u0023\u003DzGBMU9r9dm\u0024nB.dje_z3GBAX47U_ejd;
    _param2 -= (int) this.\u0023\u003DzGBMU9r9dm\u0024nB.dje_zLPL6EZPA_ejd;
    this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003DzDjUCkxvYu2E5(this.\u0023\u003Dz9b1_JhA\u003D(), this.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2), _param3, 1);
  }

  public int \u0023\u003DzZZG5mbQp1REt(int _param1)
  {
    return this.\u0023\u003Dz4ohp_xe1a9WAapdZZA\u003D\u003D + this.\u0023\u003Dz7Y6yp9wrhlh7[_param1];
  }

  public int \u0023\u003DzHlHGfKJZNJsq(int _param1, int _param2)
  {
    return this.\u0023\u003Dz4ohp_xe1a9WAapdZZA\u003D\u003D + this.\u0023\u003Dz7Y6yp9wrhlh7[_param2] + this.\u0023\u003DzKei7JjOVfWOI[_param1];
  }

  public void \u0023\u003DzLWpPHs7BoU1N(int _param1, int _param2, float[] _param3, int _param4)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz1sAbEWOIYGyA(
    int _param1,
    int _param2,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param3,
    byte _param4)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DznxX6Ui5rZOBf(
    float[] _param1,
    IColorType _param2)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzIw2\u00246wVb_WCdm8lNiA\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4)
  {
    int num;
    this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003DzDjUCkxvYu2E5(this.\u0023\u003DznPLKTp_rfCU9(_param1, _param2, out num), num, _param4, _param3);
  }

  public void \u0023\u003DzWDirdRobfratFs26wg\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzoE1u2venaydfoEqpjg\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4,
    byte _param5)
  {
    if ((double) _param4.\u0023\u003DzKCqGEcs\u003D == 0.0)
      return;
    int num1 = _param3 - _param1 + 1;
    int num2;
    float[] numArray = this.\u0023\u003DznPLKTp_rfCU9(_param1, _param2, out num2);
    float num3 = _param4.\u0023\u003DzKCqGEcs\u003D * ((float) _param5 * 0.003921569f);
    if ((double) num3 == 1.0)
    {
      this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003DzDjUCkxvYu2E5(numArray, num2, _param4, num1);
    }
    else
    {
      do
      {
        this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003Dz1sAbEWOIYGyA(numArray, num2, new \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D(_param4.\u0023\u003Dz4WHdt9g\u003D, _param4.\u0023\u003DzoRsAtmfOFDZe, _param4.\u0023\u003DzcdKuX48ZXN_S, num3));
        num2 += this.\u0023\u003Dz\u0024wxbMCh_3XSWn2xljEt8YDpBlP5T;
      }
      while (--num1 != 0);
    }
  }

  public void \u0023\u003Dzz4pZLJmkX8\u0024Augw7tA\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4,
    byte _param5)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz\u00244g26jU6qlj5jJbMN_d_Aru2N2xf(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4,
    byte[] _param5,
    int _param6)
  {
    float zKcqGecs = _param4.\u0023\u003DzKCqGEcs\u003D;
    if ((double) zKcqGecs == 0.0)
      return;
    int num1;
    float[] numArray = this.\u0023\u003DznPLKTp_rfCU9(_param1, _param2, out num1);
    do
    {
      float num2 = zKcqGecs * ((float) _param5[_param6] * 0.003921569f);
      if ((double) num2 == 1.0)
        this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003DzDjUCkxvYu2E5(numArray, num1, _param4, 1);
      else
        this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003Dz1sAbEWOIYGyA(numArray, num1, new \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D(_param4.\u0023\u003Dz4WHdt9g\u003D, _param4.\u0023\u003DzoRsAtmfOFDZe, _param4.\u0023\u003DzcdKuX48ZXN_S, num2));
      num1 += this.\u0023\u003Dz\u0024wxbMCh_3XSWn2xljEt8YDpBlP5T;
      ++_param6;
    }
    while (--_param3 != 0);
  }

  public void \u0023\u003Dz5yPQgEdavuyT81_RwrDy\u002430AFlUD(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D _param4,
    byte[] _param5,
    int _param6)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzWPXGdZTW9yddqSlTow\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param4,
    int _param5)
  {
    int num = this.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    do
    {
      this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003DzDjUCkxvYu2E5(this.\u0023\u003DznaRoot_wQHeu, num, _param4[_param5], 1);
      ++_param5;
      num += this.\u0023\u003Dz\u0024wxbMCh_3XSWn2xljEt8YDpBlP5T;
    }
    while (--_param3 != 0);
  }

  public void \u0023\u003Dz1iCm\u00249JJu\u0024SR_MfckQ\u003D\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param4,
    int _param5)
  {
    int num = this.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    do
    {
      this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003DzDjUCkxvYu2E5(this.\u0023\u003DznaRoot_wQHeu, num, _param4[_param5], 1);
      ++_param5;
      num += this.\u0023\u003DzZv9c5g_3yAgF3TYxUw\u003D\u003D;
    }
    while (--_param3 != 0);
  }

  public void \u0023\u003Dz_Cr7XQfsG3x4FPQiULaFBxE\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param4,
    int _param5,
    byte[] _param6,
    int _param7,
    bool _param8)
  {
    this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D.\u0023\u003Dz_A3pQLKo8i_c(this.\u0023\u003DznaRoot_wQHeu, this.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2), _param4, _param5, _param6, _param7, _param8, _param3);
  }

  public void \u0023\u003DzCHZdvpXxHmsxsEnO0xInJE8\u003D(
    int _param1,
    int _param2,
    int _param3,
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D[] _param4,
    int _param5,
    byte[] _param6,
    int _param7,
    bool _param8)
  {
    int num1 = this.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    int num2 = this.\u0023\u003DzY_LHm_Vxz84xXBUPBw\u003D\u003D();
    if (!_param8)
    {
      do
      {
        \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkxn9Kxm85A1kScXGmPg6TsgT4HDPm6TQ_ZxBpZGUeMaTr4BZmbQ\u003D.\u0023\u003DzaJC7XkKnnvEJF1Swqw\u003D\u003D(this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D, this.\u0023\u003DznaRoot_wQHeu, num1, _param4[_param5], (int) _param6[_param7++]);
        num1 += num2;
        ++_param5;
      }
      while (--_param3 != 0);
    }
    else if (_param6[_param7] == (byte) 1)
    {
      do
      {
        \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkxn9Kxm85A1kScXGmPg6TsgT4HDPm6TQ_ZxBpZGUeMaTr4BZmbQ\u003D.\u0023\u003DzqpZRTPYdqNQ9(this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D, this.\u0023\u003DznaRoot_wQHeu, num1, _param4[_param5]);
        num1 += num2;
        ++_param5;
      }
      while (--_param3 != 0);
    }
    else
    {
      do
      {
        \u0023\u003Dz4lH8q7tXMt_gtLJO2itFkxn9Kxm85A1kScXGmPg6TsgT4HDPm6TQ_ZxBpZGUeMaTr4BZmbQ\u003D.\u0023\u003DzaJC7XkKnnvEJF1Swqw\u003D\u003D(this.\u0023\u003Dz\u0024HQb_d5E1viweQCSKg\u003D\u003D, this.\u0023\u003DznaRoot_wQHeu, num1, _param4[_param5], (int) _param6[_param7]);
        num1 += num2;
        ++_param5;
      }
      while (--_param3 != 0);
    }
  }

  public void \u0023\u003DzyiTnxh6_JqTYVxGjPQsx0Ug\u003D(
    \u0023\u003DzAfUZ1hld3Aj4_oK9JVqPo0l783DjW2h74dYy0k094dr0Mor7TsgpRyiVdmhwT3f\u0024leflCXc\u003D _param1)
  {
    throw new NotImplementedException();
  }

  private bool \u0023\u003DzZdqAWX9RRNB\u0024(int _param1, int _param2)
  {
    \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D af0lRdvTglyLevjU0 = this.\u0023\u003DztvwmoyKu8oUlLP4_gA\u003D\u003D().\u0023\u003DzpXQh1U84EY2nUbOFY6f_7N0\u003D(this.\u0023\u003DznaRoot_wQHeu, this.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2));
    return af0lRdvTglyLevjU0.\u0023\u003DzaFSg4agx5zA7ee4hwQ\u003D\u003D() != 0 || af0lRdvTglyLevjU0.\u0023\u003DzV4RXfxw92_Lg32L76Q\u003D\u003D() != 0 || af0lRdvTglyLevjU0.\u0023\u003DzawOz8j0iM2Uvf\u0024fqsg\u003D\u003D() != 0 || af0lRdvTglyLevjU0.\u0023\u003Dz5hw2frZMqmtuBiH06w\u003D\u003D() != 0;
  }

  public void \u0023\u003DzvswwX8WAwCrn(
    out RectangleInt _param1)
  {
    _param1 = new RectangleInt(0, 0, this.Width, this.Height);
    bool flag = false;
    for (int index1 = 0; index1 < this.\u0023\u003Dzh_FblSMKXulX; ++index1)
    {
      for (int index2 = 0; index2 < this.\u0023\u003Dz9LgI12vZMy\u0024F; ++index2)
      {
        if (this.\u0023\u003DzZdqAWX9RRNB\u0024(index2, index1))
        {
          _param1.\u0023\u003DzRNV_Dpk\u003D = index1;
          index1 = this.\u0023\u003Dzh_FblSMKXulX;
          index2 = this.\u0023\u003Dz9LgI12vZMy\u0024F;
          flag = true;
        }
      }
    }
    if (!flag)
    {
      _param1.\u0023\u003DzkTP\u0024FzM\u003D(0, 0, 0, 0);
    }
    else
    {
      for (int index3 = this.\u0023\u003Dzh_FblSMKXulX - 1; index3 >= 0; --index3)
      {
        for (int index4 = 0; index4 < this.\u0023\u003Dz9LgI12vZMy\u0024F; ++index4)
        {
          if (this.\u0023\u003DzZdqAWX9RRNB\u0024(index4, index3))
          {
            _param1.\u0023\u003DzSzOWcj8\u003D = index3 + 1;
            index3 = -1;
            index4 = this.\u0023\u003Dz9LgI12vZMy\u0024F;
          }
        }
      }
      for (int index5 = 0; index5 < this.\u0023\u003Dz9LgI12vZMy\u0024F; ++index5)
      {
        for (int index6 = 0; index6 < this.\u0023\u003Dzh_FblSMKXulX; ++index6)
        {
          if (this.\u0023\u003DzZdqAWX9RRNB\u0024(index5, index6))
          {
            _param1.\u0023\u003DzP4R7yU0\u003D = index5;
            index6 = this.\u0023\u003Dzh_FblSMKXulX;
            index5 = this.\u0023\u003Dz9LgI12vZMy\u0024F;
          }
        }
      }
      for (int index7 = this.\u0023\u003Dz9LgI12vZMy\u0024F - 1; index7 >= 0; --index7)
      {
        for (int index8 = 0; index8 < this.\u0023\u003Dzh_FblSMKXulX; ++index8)
        {
          if (this.\u0023\u003DzZdqAWX9RRNB\u0024(index7, index8))
          {
            _param1.\u0023\u003Dzp55dtus\u003D = index7 + 1;
            index8 = this.\u0023\u003Dzh_FblSMKXulX;
            index7 = -1;
          }
        }
      }
    }
  }

  public void \u0023\u003DzpAcjMXrfqlL_()
  {
    Vector2 jnelpsqX4Q78W2Ejd = this.\u0023\u003DzSm8CGbTqdI1U();
    this.\u0023\u003DzEN24ToKSPvcM(new Vector2(0.0, 0.0));
    RectangleInt xvhBayrK2CmzoKas;
    this.\u0023\u003DzvswwX8WAwCrn(out xvhBayrK2CmzoKas);
    if (xvhBayrK2CmzoKas.Width == this.Width && xvhBayrK2CmzoKas.Height == this.Height)
      this.\u0023\u003DzEN24ToKSPvcM(jnelpsqX4Q78W2Ejd);
    else if (xvhBayrK2CmzoKas.Width > 0)
    {
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D hjj4mAclInlkopAshpw = new \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D();
      hjj4mAclInlkopAshpw.\u0023\u003DzFeNr2Uw\u003D(this, xvhBayrK2CmzoKas);
      this.\u0023\u003DzFeNr2Uw\u003D(hjj4mAclInlkopAshpw);
      this.\u0023\u003DzEN24ToKSPvcM(new Vector2((double) -xvhBayrK2CmzoKas.\u0023\u003DzP4R7yU0\u003D + jnelpsqX4Q78W2Ejd.dje_z3GBAX47U_ejd, (double) -xvhBayrK2CmzoKas.\u0023\u003DzRNV_Dpk\u003D + jnelpsqX4Q78W2Ejd.dje_zLPL6EZPA_ejd));
    }
    else
      this.\u0023\u003Dz05SWyVw\u003D();
  }

  public RectangleInt \u0023\u003DzOvmtoW_Py5e_()
  {
    RectangleInt xvhBayrK2CmzoKas = new RectangleInt(0, 0, this.Width, this.Height);
    xvhBayrK2CmzoKas.\u0023\u003DznR9\u00242Eg\u003D((int) this.\u0023\u003DzSm8CGbTqdI1U().dje_z3GBAX47U_ejd, (int) this.\u0023\u003DzSm8CGbTqdI1U().dje_zLPL6EZPA_ejd);
    return xvhBayrK2CmzoKas;
  }

  private void \u0023\u003DzFeNr2Uw\u003D(
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D _param1)
  {
    RectangleInt xvhBayrK2CmzoKas = _param1.\u0023\u003DzOvmtoW_Py5e_();
    this.\u0023\u003DzFeNr2Uw\u003D(_param1, xvhBayrK2CmzoKas);
    this.\u0023\u003DzEN24ToKSPvcM(_param1.\u0023\u003DzSm8CGbTqdI1U());
  }

  private void \u0023\u003DzFeNr2Uw\u003D(
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D _param1,
    RectangleInt _param2)
  {
    if (_param1 == this)
      throw new Exception("We do not create a temp buffer for this to work.  You must have a source distinct from the dest.");
    this.\u0023\u003Dz05SWyVw\u003D();
    this.\u0023\u003DzWIEZ7Zw\u003D(_param2.Width, _param2.Height, _param2.Width * _param1.\u0023\u003DzHstjD51XfGa0() / 8, _param1.\u0023\u003DzHstjD51XfGa0());
    this.\u0023\u003DzgDbPoFoJnrrrHveVDA\u003D\u003D(_param1.\u0023\u003DztvwmoyKu8oUlLP4_gA\u003D\u003D());
    if (this.\u0023\u003Dz9LgI12vZMy\u0024F == 0 || this.\u0023\u003Dzh_FblSMKXulX == 0)
      return;
    RectangleInt xvhBayrK2CmzoKas = new RectangleInt(0, 0, _param2.Width, _param2.Height);
    Graphics2D yrnYmqhSbkIjQ8lsq = this.\u0023\u003Dz9Yt\u0024vKcgxNiu();
    yrnYmqhSbkIjQ8lsq.Clear((IColorType) new \u0023\u003DzDNNaZJ5EGyaw0AhPEVAPg7Z8mbS\u0024KpwTZWt25viJ7i7WYL1vIcbzTgAF0lRdvTGLYLevjU0\u003D(0.0f, 0.0f, 0.0f, 0.0f));
    int num1 = -_param2.\u0023\u003DzP4R7yU0\u003D - (int) _param1.\u0023\u003DzSm8CGbTqdI1U().dje_z3GBAX47U_ejd;
    int num2 = -_param2.\u0023\u003DzRNV_Dpk\u003D - (int) _param1.\u0023\u003DzSm8CGbTqdI1U().dje_zLPL6EZPA_ejd;
    yrnYmqhSbkIjQ8lsq.\u0023\u003DzLDS6T7I\u003D((IImageFloat) _param1, (double) num1, (double) num2, 0.0, 1.0, 1.0);
  }

  public sealed class \u0023\u003Dzo5XZ2jPFuNIh5iLb1w\u003D\u003D : 
    \u0023\u003Dzio\u0024B9RjpWPC7_mh7fpi_3smKkT1b5smOw_lMHMtPtnGTbJIqHYmQyGOu0d1gzQea9g\u003D\u003D
  {
    private \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D \u0023\u003Dz97TAMb15TLEN;

    public \u0023\u003Dzo5XZ2jPFuNIh5iLb1w\u003D\u003D(
      \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSeBW2Qyo4X1JlYe0Jj9qgYOQm6qHjj4mAclInlkopAShpw\u003D\u003D _param1)
    {
      this.\u0023\u003Dz97TAMb15TLEN = _param1;
      ScanlineRasterizer zoxZ8e2vJ5pzF77A = new ScanlineRasterizer();
      this.\u0023\u003DzFeNr2Uw\u003D((IImageFloat) new \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3zBj0gwfrZ83Dds5jpCOZ4BhB_9FqI1TxBQx0ZXbRK0ob9fwKig\u003D((IImageFloat) _param1), zoxZ8e2vJ5pzF77A);
      this.\u0023\u003Dz_y5lbRn8Mf7TIRyIeAh8YTM\u003D((IScanlineCache) new \u0023\u003Dzm9W_6u1Hb\u0024Y4gq7yl8Gm\u00242KpZczbbMbMasxecTGKxnt5bT2l7s_zjbEU3l3ty8PLFOQY2AYvJuIy2kooCOncjwSFLeruKsraew\u003D\u003D());
    }
  }
}
