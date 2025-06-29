// Decompiled with JetBrains decompiler
// Type: #=zKX_o18CSOBV8bEiC9p$hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Xaml.Charting.Rendering.HighSpeedRasterizer.WBX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#nullable disable
internal static class \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D
{
  private static readonly int[] \u0023\u003Dz_FUsyObExE8N = new int[8192 /*0x2000*/];
  private static readonly int[] \u0023\u003DzLmHTXF__p5pA = new int[8192 /*0x2000*/];
  internal static int[,] \u0023\u003DzysGNeXLGQrTFa4zixuE1sQiZOAav = new int[5, 5]
  {
    {
      1,
      4,
      7,
      4,
      1
    },
    {
      4,
      16 /*0x10*/,
      26,
      16 /*0x10*/,
      4
    },
    {
      7,
      26,
      41,
      26,
      7
    },
    {
      4,
      16 /*0x10*/,
      26,
      16 /*0x10*/,
      4
    },
    {
      1,
      4,
      7,
      4,
      1
    }
  };
  internal static int[,] \u0023\u003Dzo8w5Q2KsNK7CZqCtd5fWpQSr7pnh = new int[3, 3]
  {
    {
      16 /*0x10*/,
      26,
      16 /*0x10*/
    },
    {
      26,
      41,
      26
    },
    {
      16 /*0x10*/,
      26,
      16 /*0x10*/
    }
  };
  internal static int[,] \u0023\u003DzpCqOIxgfWQQUkjhy62T6b1Q\u003D = new int[3, 3]
  {
    {
      0,
      -2,
      0
    },
    {
      -2,
      11,
      -2
    },
    {
      0,
      -2,
      0
    }
  };

  private static void \u0023\u003Dzs8rejDM\u003D<T>(ref T _param0, ref T _param1)
  {
    T obj = _param0;
    _param0 = _param1;
    _param1 = obj;
  }

  private static unsafe void \u0023\u003DzHwiDZJ6yzPxC(
    int _param0,
    int _param1,
    BitmapContext _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    bool _param8,
    bool _param9)
  {
    byte num1 = 0;
    if (_param8)
      num1 = byte.MaxValue;
    if (_param3 == _param5 || _param4 == _param6)
      return;
    int* numPtr = _param2.\u0023\u003DzSKG\u0024_qBsOJZc();
    if (_param4 > _param6)
    {
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzs8rejDM\u003D<int>(ref _param3, ref _param5);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzs8rejDM\u003D<int>(ref _param4, ref _param6);
    }
    int num2 = _param5 - _param3;
    int num3 = _param6 - _param4;
    if (_param3 > _param5)
      num2 = _param3 - _param5;
    int num4 = _param3;
    int index = _param4;
    ushort num5 = num2 <= num3 ? (ushort) ((num2 << 16 /*0x10*/) / num3) : (ushort) ((num3 << 16 /*0x10*/) / num2);
    byte num6 = (byte) (((long) _param7 & 4278190080L /*0xFF000000*/) >> 24);
    byte num7 = (byte) ((_param7 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
    byte num8 = (byte) ((_param7 & 65280) >> 8);
    byte num9 = (byte) _param7;
    ushort num10 = 0;
    if (num2 >= num3)
    {
      while (num2-- != 0)
      {
        if ((int) (ushort) ((uint) num10 + (uint) num5) <= (int) num10)
          ++index;
        num10 += num5;
        if (_param3 < _param5)
          ++num4;
        else
          --num4;
        if (index >= 0 && index < _param1)
        {
          if (_param9)
            \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[index] = Math.Max(num4 + 1, \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[index]);
          else
            \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[index] = Math.Min(num4 - 1, \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[index]);
          if (num4 >= 0 && num4 < _param0)
          {
            byte num11 = (byte) ((int) num6 * (int) (ushort) ((uint) (ushort) ((uint) num10 >> 8) ^ (uint) num1) >> 8);
            int num12 = (int) num7;
            byte num13 = num8;
            byte num14 = num9;
            int num15 = numPtr[index * _param0 + num4];
            byte num16 = (byte) ((num15 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
            byte num17 = (byte) ((num15 & 65280) >> 8);
            byte num18 = (byte) num15;
            int num19 = (int) num11;
            byte num20 = (byte) (num12 * num19 + (int) num16 * ((int) byte.MaxValue - (int) num11) >> 8);
            byte num21 = (byte) ((int) num13 * (int) num11 + (int) num17 * ((int) byte.MaxValue - (int) num11) >> 8);
            byte num22 = (byte) ((int) num14 * (int) num11 + (int) num18 * ((int) byte.MaxValue - (int) num11) >> 8);
            numPtr[index * _param0 + num4] = -16777216 /*0xFF000000*/ | (int) num20 << 16 /*0x10*/ | (int) num21 << 8 | (int) num22;
          }
        }
      }
    }
    else
    {
      byte num23 = (byte) ((uint) num1 ^ (uint) byte.MaxValue);
      while (--num3 != 0)
      {
        if ((int) (ushort) ((uint) num10 + (uint) num5) <= (int) num10)
        {
          if (_param3 < _param5)
            ++num4;
          else
            --num4;
        }
        num10 += num5;
        ++index;
        if (num4 >= 0 && num4 < _param0 && index >= 0 && index < _param1)
        {
          byte num24 = (byte) ((int) num6 * (int) (ushort) ((uint) (ushort) ((uint) num10 >> 8) ^ (uint) num23) >> 8);
          int num25 = (int) num7;
          byte num26 = num8;
          byte num27 = num9;
          int num28 = numPtr[index * _param0 + num4];
          byte num29 = (byte) ((num28 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
          byte num30 = (byte) ((num28 & 65280) >> 8);
          byte num31 = (byte) num28;
          int num32 = (int) num24;
          byte num33 = (byte) (num25 * num32 + (int) num29 * ((int) byte.MaxValue - (int) num24) >> 8);
          byte num34 = (byte) ((int) num26 * (int) num24 + (int) num30 * ((int) byte.MaxValue - (int) num24) >> 8);
          byte num35 = (byte) ((int) num27 * (int) num24 + (int) num31 * ((int) byte.MaxValue - (int) num24) >> 8);
          numPtr[index * _param0 + num4] = -16777216 /*0xFF000000*/ | (int) num33 << 16 /*0x10*/ | (int) num34 << 8 | (int) num35;
          if (_param9)
            \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[index] = num4 + 1;
          else
            \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[index] = num4 - 1;
        }
      }
    }
  }

  private static unsafe void \u0023\u003DzFun0vYqvUTLh(
    int _param0,
    int _param1,
    BitmapContext _param2,
    float _param3,
    float _param4,
    float _param5,
    float _param6,
    float _param7,
    int _param8)
  {
    if ((double) _param7 <= 0.0)
      return;
    int* numPtr = _param2.\u0023\u003DzSKG\u0024_qBsOJZc();
    if ((double) _param4 > (double) _param6)
    {
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzs8rejDM\u003D<float>(ref _param3, ref _param5);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzs8rejDM\u003D<float>(ref _param4, ref _param6);
    }
    if ((double) _param3 == (double) _param5)
    {
      _param3 -= (float) ((int) _param7 / 2);
      _param5 += (float) ((int) _param7 / 2);
      if ((double) _param3 < 0.0)
        _param3 = 0.0f;
      if ((double) _param5 < 0.0 || (double) _param3 >= (double) _param0)
        return;
      if ((double) _param5 >= (double) _param0)
        _param5 = (float) (_param0 - 1);
      if ((double) _param4 >= (double) _param1 || (double) _param6 < 0.0)
        return;
      if ((double) _param4 < 0.0)
        _param4 = 0.0f;
      if ((double) _param6 >= (double) _param1)
        _param6 = (float) (_param1 - 1);
      for (int index1 = (int) _param3; (double) index1 <= (double) _param5; ++index1)
      {
        for (int index2 = (int) _param4; (double) index2 <= (double) _param6; ++index2)
        {
          byte num1 = (byte) (((long) _param8 & 4278190080L /*0xFF000000*/) >> 24);
          byte num2 = (byte) ((_param8 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
          byte num3 = (byte) ((_param8 & 65280) >> 8);
          int num4 = (int) (byte) _param8;
          byte num5 = num2;
          byte num6 = num3;
          int num7 = numPtr[index2 * _param0 + index1];
          byte num8 = (byte) ((num7 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
          byte num9 = (byte) ((num7 & 65280) >> 8);
          byte num10 = (byte) num7;
          byte num11 = (byte) ((int) num5 * (int) num1 + (int) num8 * ((int) byte.MaxValue - (int) num1) >> 8);
          byte num12 = (byte) ((int) num6 * (int) num1 + (int) num9 * ((int) byte.MaxValue - (int) num1) >> 8);
          int num13 = (int) num1;
          byte num14 = (byte) (num4 * num13 + (int) num10 * ((int) byte.MaxValue - (int) num1) >> 8);
          numPtr[index2 * _param0 + index1] = -16777216 /*0xFF000000*/ | (int) num11 << 16 /*0x10*/ | (int) num12 << 8 | (int) num14;
        }
      }
    }
    else if ((double) _param4 == (double) _param6)
    {
      if ((double) _param3 > (double) _param5)
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzs8rejDM\u003D<float>(ref _param3, ref _param5);
      _param4 -= (float) ((int) _param7 / 2);
      _param6 += (float) ((int) _param7 / 2);
      if ((double) _param4 < 0.0)
        _param4 = 0.0f;
      if ((double) _param6 < 0.0 || (double) _param4 >= (double) _param1)
        return;
      if ((double) _param6 >= (double) _param1)
        _param5 = (float) (_param1 - 1);
      if ((double) _param3 >= (double) _param0 || (double) _param6 < 0.0)
        return;
      if ((double) _param3 < 0.0)
        _param3 = 0.0f;
      if ((double) _param5 >= (double) _param0)
        _param5 = (float) (_param0 - 1);
      for (int index3 = (int) _param3; (double) index3 <= (double) _param5; ++index3)
      {
        for (int index4 = (int) _param4; (double) index4 <= (double) _param6; ++index4)
        {
          byte num15 = (byte) (((long) _param8 & 4278190080L /*0xFF000000*/) >> 24);
          byte num16 = (byte) ((_param8 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
          byte num17 = (byte) ((_param8 & 65280) >> 8);
          int num18 = (int) (byte) _param8;
          byte num19 = num16;
          byte num20 = num17;
          int num21 = numPtr[index4 * _param0 + index3];
          byte num22 = (byte) ((num21 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
          byte num23 = (byte) ((num21 & 65280) >> 8);
          byte num24 = (byte) num21;
          byte num25 = (byte) ((int) num19 * (int) num15 + (int) num22 * ((int) byte.MaxValue - (int) num15) >> 8);
          byte num26 = (byte) ((int) num20 * (int) num15 + (int) num23 * ((int) byte.MaxValue - (int) num15) >> 8);
          int num27 = (int) num15;
          byte num28 = (byte) (num18 * num27 + (int) num24 * ((int) byte.MaxValue - (int) num15) >> 8);
          numPtr[index4 * _param0 + index3] = -16777216 /*0xFF000000*/ | (int) num25 << 16 /*0x10*/ | (int) num26 << 8 | (int) num28;
        }
      }
    }
    else
    {
      ++_param4;
      ++_param6;
      double num29 = ((double) _param6 - (double) _param4) / ((double) _param5 - (double) _param3);
      double num30 = ((double) _param5 - (double) _param3) / ((double) _param6 - (double) _param4);
      double num31 = (double) _param7;
      float num32 = _param5 - _param3;
      float num33 = _param6 - _param4;
      float num34 = (float) (num31 * (double) num33 / Math.Sqrt((double) num32 * (double) num32 + (double) num33 * (double) num33));
      float num35 = (float) (num31 * (double) num32 / Math.Sqrt((double) num32 * (double) num32 + (double) num33 * (double) num33));
      double num36 = (double) num32 * (double) num33 / ((double) num32 * (double) num32 + (double) num33 * (double) num33);
      _param3 += num34 / 2f;
      _param4 -= num35 / 2f;
      _param5 += num34 / 2f;
      _param6 -= num35 / 2f;
      float num37 = -num34;
      float num38 = num35;
      int val1_1 = (int) _param3;
      int val1_2 = (int) _param4;
      int val1_3 = (int) _param5;
      int val2_1 = (int) _param6;
      int val1_4 = (int) ((double) _param3 + (double) num37);
      int val1_5 = (int) ((double) _param4 + (double) num38);
      int val1_6 = (int) ((double) _param5 + (double) num37);
      int val2_2 = (int) ((double) _param6 + (double) num38);
      if ((double) _param7 == 2.0)
      {
        if ((double) Math.Abs(num33) < (double) Math.Abs(num32))
        {
          if ((double) _param3 < (double) _param5)
          {
            val1_5 = val1_2 + 2;
            val2_2 = val2_1 + 2;
          }
          else
          {
            val1_2 = val1_5 + 2;
            val2_1 = val2_2 + 2;
          }
        }
        else
        {
          val1_1 = val1_4 + 2;
          val1_3 = val1_6 + 2;
        }
      }
      int num39 = Math.Min(Math.Min(val1_2, val2_1), Math.Min(val1_5, val2_2));
      int num40 = Math.Max(Math.Max(val1_2, val2_1), Math.Max(val1_5, val2_2));
      if (num39 < 0)
        num39 = -1;
      if (num40 >= _param1)
        num40 = _param1 + 1;
      for (int index = num39 + 1; index < num40 - 1; ++index)
      {
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[index] = -65536;
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[index] = 32768 /*0x8000*/;
      }
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzHwiDZJ6yzPxC(_param0, _param1, _param2, val1_1, val1_2, val1_3, val2_1, _param8, (double) num38 > 0.0, false);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzHwiDZJ6yzPxC(_param0, _param1, _param2, val1_4, val1_5, val1_6, val2_2, _param8, (double) num38 < 0.0, true);
      if ((double) _param7 > 1.0)
      {
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzHwiDZJ6yzPxC(_param0, _param1, _param2, val1_1, val1_2, val1_4, val1_5, _param8, true, (double) num38 > 0.0);
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzHwiDZJ6yzPxC(_param0, _param1, _param2, val1_3, val2_1, val1_6, val2_2, _param8, false, (double) num38 < 0.0);
      }
      if ((double) _param3 < (double) _param5)
      {
        if (val2_1 >= 0 && val2_1 < _param1)
          \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[val2_1] = Math.Min(val1_3, \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[val2_1]);
        if (val1_5 >= 0 && val1_5 < _param1)
          \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[val1_5] = Math.Max(val1_4, \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[val1_5]);
      }
      else
      {
        if (val1_2 >= 0 && val1_2 < _param1)
          \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[val1_2] = Math.Min(val1_1, \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[val1_2]);
        if (val2_2 >= 0 && val2_2 < _param1)
          \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[val2_2] = Math.Max(val1_6, \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[val2_2]);
      }
      for (int index5 = num39 + 1; index5 < num40 - 1; ++index5)
      {
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[index5] = Math.Max(\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[index5], 0);
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[index5] = Math.Min(\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[index5], _param0 - 1);
        for (int index6 = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_FUsyObExE8N[index5]; index6 <= \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzLmHTXF__p5pA[index5]; ++index6)
        {
          byte num41 = (byte) (((long) _param8 & 4278190080L /*0xFF000000*/) >> 24);
          byte num42 = (byte) ((_param8 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
          byte num43 = (byte) ((_param8 & 65280) >> 8);
          int num44 = (int) (byte) _param8;
          byte num45 = num42;
          byte num46 = num43;
          int num47 = numPtr[index5 * _param0 + index6];
          byte num48 = (byte) ((num47 & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
          byte num49 = (byte) ((num47 & 65280) >> 8);
          byte num50 = (byte) num47;
          byte num51 = (byte) ((int) num45 * (int) num41 + (int) num48 * ((int) byte.MaxValue - (int) num41) >> 8);
          byte num52 = (byte) ((int) num46 * (int) num41 + (int) num49 * ((int) byte.MaxValue - (int) num41) >> 8);
          int num53 = (int) num41;
          byte num54 = (byte) (num44 * num53 + (int) num50 * ((int) byte.MaxValue - (int) num41) >> 8);
          numPtr[index5 * _param0 + index6] = -16777216 /*0xFF000000*/ | (int) num51 << 16 /*0x10*/ | (int) num52 << 8 | (int) num54;
        }
      }
    }
  }

  internal static void \u0023\u003DzbuqTyk9tDHwh(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8)
  {
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzFun0vYqvUTLh(_param1, _param2, _param0, (float) _param3, (float) _param4, (float) _param5, (float) _param6, (float) _param8, _param7);
  }

  internal static void \u0023\u003DzbuqTyk9tDHwh(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    Color _param7,
    int _param8)
  {
    int num1 = (int) _param7.A + 1;
    int num2 = (int) _param7.A << 24 | (int) (byte) ((int) _param7.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param7.G * num1 >> 8) << 8 | (int) (byte) ((int) _param7.B * num1 >> 8);
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzFun0vYqvUTLh(_param1, _param2, _param0, (float) _param3, (float) _param4, (float) _param5, (float) _param6, (float) _param8, num2);
  }

  internal static void \u0023\u003DzbuqTyk9tDHwh(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5,
    int _param6)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzFun0vYqvUTLh(_param0.PixelWidth, _param0.PixelHeight, bitmapContext, (float) _param1, (float) _param2, (float) _param3, (float) _param4, (float) _param6, num2);
  }

  internal static int \u0023\u003Dz_DB895w\u003D(double _param0, Color _param1)
  {
    if (_param0 < 0.0 || _param0 > 1.0)
      throw new ArgumentOutOfRangeException("", "");
    _param1.A = (byte) ((double) _param1.A * _param0);
    return \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz_DB895w\u003D(_param1);
  }

  internal static int \u0023\u003Dz_DB895w\u003D(Color _param0)
  {
    if (_param0.A == (byte) 0)
      return 0;
    int num = (int) _param0.A + 1;
    return (int) _param0.A << 24 | (int) (byte) ((int) _param0.R * num >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param0.G * num >> 8) << 8 | (int) (byte) ((int) _param0.B * num >> 8);
  }

  internal static unsafe void \u0023\u003DzUf222sU\u003D(
    this WriteableBitmap _param0,
    Color _param1)
  {
    int num1 = (int) _param1.A + 1;
    int num2 = (int) _param1.A << 24 | (int) (byte) ((int) _param1.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param1.G * num1 >> 8) << 8 | (int) (byte) ((int) _param1.B * num1 >> 8);
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      int pixelWidth = _param0.PixelWidth;
      int pixelHeight = _param0.PixelHeight;
      int num3 = pixelWidth * 4;
      for (int index = 0; index < pixelWidth; ++index)
        numPtr[index] = num2;
      int num4 = 1;
      int num5 = 1;
      while (num5 < pixelHeight)
      {
        BitmapContext.\u0023\u003Dzk\u0024wemaJeIY7r(bitmapContext, 0, bitmapContext, num5 * num3, num4 * num3);
        num5 += num4;
        num4 = Math.Min(2 * num4, pixelHeight - num5);
      }
    }
  }

  internal static void \u0023\u003DzUf222sU\u003D(this WriteableBitmap _param0)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      bitmapContext.\u0023\u003DzUf222sU\u003D();
  }

  internal static WriteableBitmap \u0023\u003DzQ8SgRgQ\u003D(this WriteableBitmap _param0)
  {
    WriteableBitmap writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(_param0.PixelWidth, _param0.PixelHeight);
    using (BitmapContext bitmapContext1 = _param0.\u0023\u003DzjnjmjBtrwZM5((\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 0))
    {
      using (BitmapContext bitmapContext2 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
        BitmapContext.\u0023\u003Dzk\u0024wemaJeIY7r(bitmapContext1, 0, bitmapContext2, 0, bitmapContext1.\u0023\u003DzxhbmvAVxpXvh() * 4);
    }
    return writeableBitmap;
  }

  internal static unsafe void \u0023\u003Dzq7Bcrww\u003D(
    this WriteableBitmap _param0,
    Func<int, int, Color> _param1)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      int pixelWidth = _param0.PixelWidth;
      int pixelHeight = _param0.PixelHeight;
      int num1 = 0;
      for (int index1 = 0; index1 < pixelHeight; ++index1)
      {
        for (int index2 = 0; index2 < pixelWidth; ++index2)
        {
          Color color = _param1(index2, index1);
          int num2 = (int) color.A + 1;
          numPtr[num1++] = (int) color.A << 24 | (int) (byte) ((int) color.R * num2 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) color.G * num2 >> 8) << 8 | (int) (byte) ((int) color.B * num2 >> 8);
        }
      }
    }
  }

  internal static unsafe void \u0023\u003Dzq7Bcrww\u003D(
    this WriteableBitmap _param0,
    Func<int, int, Color, Color> _param1)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      int pixelWidth = _param0.PixelWidth;
      int pixelHeight = _param0.PixelHeight;
      int index1 = 0;
      for (int index2 = 0; index2 < pixelHeight; ++index2)
      {
        for (int index3 = 0; index3 < pixelWidth; ++index3)
        {
          int b = numPtr[index1];
          Color color = _param1(index3, index2, Color.FromArgb((byte) (b >> 24), (byte) (b >> 16 /*0x10*/), (byte) (b >> 8), (byte) b));
          int num = (int) color.A + 1;
          numPtr[index1++] = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 /*0x10*/ | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
        }
      }
    }
  }

  internal static unsafe int \u0023\u003DzRjQ8n0MoBfAr(
    this WriteableBitmap _param0,
    int _param1,
    int _param2)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      return bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param2 * _param0.PixelWidth + _param1];
  }

  internal static unsafe Color \u0023\u003DzFiIk5SM\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int num1 = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param2 * _param0.PixelWidth + _param1];
      int a;
      int num2 = a = (int) (byte) (num1 >> 24);
      if (num2 == 0)
        num2 = 1;
      int num3 = 65280 / num2;
      int r = (int) (byte) ((num1 >> 16 /*0x10*/ & (int) byte.MaxValue) * num3 >> 8);
      int g = (int) (byte) ((num1 >> 8 & (int) byte.MaxValue) * num3 >> 8);
      int b = (int) (byte) ((num1 & (int) byte.MaxValue) * num3 >> 8);
      return Color.FromArgb((byte) a, (byte) r, (byte) g, (byte) b);
    }
  }

  internal static unsafe byte \u0023\u003Dz38DsZAXIb6Woq\u0024n_xQ\u003D\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5((\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 0))
    {
      int num = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param2 * _param0.PixelWidth + _param1];
      return (byte) ((int) (byte) (num >> 16 /*0x10*/) * 6966 + (int) (byte) (num >> 8) * 23436 + (int) (byte) num * 2366 >> 15);
    }
  }

  internal static unsafe void \u0023\u003Dzwu8zArYH4KUt(
    this WriteableBitmap _param0,
    int _param1,
    byte _param2,
    byte _param3,
    byte _param4)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param1] = -16777216 /*0xFF000000*/ | (int) _param2 << 16 /*0x10*/ | (int) _param3 << 8 | (int) _param4;
  }

  internal static unsafe void \u0023\u003DzR2zHA_0\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    byte _param3,
    byte _param4,
    byte _param5)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param2 * _param0.PixelWidth + _param1] = -16777216 /*0xFF000000*/ | (int) _param3 << 16 /*0x10*/ | (int) _param4 << 8 | (int) _param5;
  }

  internal static unsafe void \u0023\u003Dzwu8zArYH4KUt(
    this WriteableBitmap _param0,
    int _param1,
    byte _param2,
    byte _param3,
    byte _param4,
    byte _param5)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param1] = (int) _param2 << 24 | (int) _param3 << 16 /*0x10*/ | (int) _param4 << 8 | (int) _param5;
  }

  internal static unsafe void \u0023\u003DzR2zHA_0\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    byte _param3,
    byte _param4,
    byte _param5,
    byte _param6)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param2 * _param0.PixelWidth + _param1] = (int) _param3 << 24 | (int) _param4 << 16 /*0x10*/ | (int) _param5 << 8 | (int) _param6;
  }

  internal static unsafe void \u0023\u003Dzwu8zArYH4KUt(
    this WriteableBitmap _param0,
    int _param1,
    Color _param2)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int num = (int) _param2.A + 1;
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param1] = (int) _param2.A << 24 | (int) (byte) ((int) _param2.R * num >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param2.G * num >> 8) << 8 | (int) (byte) ((int) _param2.B * num >> 8);
    }
  }

  internal static unsafe void \u0023\u003DzR2zHA_0\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    Color _param3)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int num = (int) _param3.A + 1;
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param2 * _param0.PixelWidth + _param1] = (int) _param3.A << 24 | (int) (byte) ((int) _param3.R * num >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param3.G * num >> 8) << 8 | (int) (byte) ((int) _param3.B * num >> 8);
    }
  }

  internal static unsafe void \u0023\u003Dzwu8zArYH4KUt(
    this WriteableBitmap _param0,
    int _param1,
    byte _param2,
    Color _param3)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int num = (int) _param2 + 1;
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param1] = (int) _param2 << 24 | (int) (byte) ((int) _param3.R * num >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param3.G * num >> 8) << 8 | (int) (byte) ((int) _param3.B * num >> 8);
    }
  }

  internal static unsafe void \u0023\u003DzR2zHA_0\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    byte _param3,
    Color _param4)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int num = (int) _param3 + 1;
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param2 * _param0.PixelWidth + _param1] = (int) _param3 << 24 | (int) (byte) ((int) _param4.R * num >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param4.G * num >> 8) << 8 | (int) (byte) ((int) _param4.B * num >> 8);
    }
  }

  internal static unsafe void \u0023\u003Dzwu8zArYH4KUt(
    this WriteableBitmap _param0,
    int _param1,
    int _param2)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param1] = _param2;
  }

  internal static unsafe void \u0023\u003DzR2zHA_0\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[_param2 * _param0.PixelWidth + _param1] = _param3;
  }

  internal static unsafe void \u0023\u003DztJb5\u0024zF1SLeC(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    IList<int> _param4,
    double _param5,
    bool _param6)
  {
    int num1 = Math.Max(_param2, _param3);
    _param3 = Math.Min(_param2, _param3);
    _param2 = num1;
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    if (_param2 == _param3)
      return;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      int num2 = Math.Min(_param2, pixelHeight);
      int index1 = _param1 + num2 * pixelWidth;
      int num3 = num2;
      while (num3 >= _param3 && num3 >= 0)
      {
        if (num3 >= 0 && num3 < pixelHeight)
        {
          int index2 = (_param2 - num3) * _param4.Count / (_param2 - _param3);
          if (_param6)
            index2 = _param4.Count - 1 - index2;
          if (index2 >= 0 && index2 < _param4.Count)
          {
            int num4 = _param4[index2];
            int num5 = (int) ((double) (num4 >> 24 & (int) byte.MaxValue) * _param5);
            if (num5 == (int) byte.MaxValue)
              numPtr[index1] = num4;
            else if (num5 > 0)
            {
              int num6 = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[index1];
              int num7 = num6 >> 24 & (int) byte.MaxValue;
              int num8 = num6 >> 16 /*0x10*/ & (int) byte.MaxValue;
              int num9 = num6 >> 8 & (int) byte.MaxValue;
              int num10 = num6 & (int) byte.MaxValue;
              int num11 = num4 >> 16 /*0x10*/ & (int) byte.MaxValue;
              int num12 = num4 >> 8 & (int) byte.MaxValue;
              int num13 = num4 & (int) byte.MaxValue;
              int num14 = num5;
              int num15 = num11 * num14 / (int) byte.MaxValue + num8 * num7 * ((int) byte.MaxValue - num5) / 65025;
              int num16 = num12 * num5 / (int) byte.MaxValue + num9 * num7 * ((int) byte.MaxValue - num5) / 65025;
              int num17 = num13 * num5 / (int) byte.MaxValue + num10 * num7 * ((int) byte.MaxValue - num5) / 65025;
              int num18 = num5 + num7 * ((int) byte.MaxValue - num5) / (int) byte.MaxValue;
              bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc()[index1] = (num18 << 24) + (num15 << 16 /*0x10*/) + (num16 << 8) + num17;
            }
          }
        }
        --num3;
        index1 -= pixelWidth;
      }
    }
  }

  internal static void \u0023\u003DzSyuZtKbRCpVU(
    this WriteableBitmap _param0,
    Rect _param1,
    WriteableBitmap _param2,
    Rect _param3,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 _param4)
  {
    _param0.\u0023\u003DzSyuZtKbRCpVU(_param1, _param2, _param3, Colors.White, _param4);
  }

  internal static void \u0023\u003DzSyuZtKbRCpVU(
    this WriteableBitmap _param0,
    Rect _param1,
    WriteableBitmap _param2,
    Rect _param3)
  {
    _param0.\u0023\u003DzSyuZtKbRCpVU(_param1, _param2, _param3, Colors.White, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
  }

  internal static void \u0023\u003DzSyuZtKbRCpVU(
    this WriteableBitmap _param0,
    Point _param1,
    WriteableBitmap _param2,
    Rect _param3,
    Color _param4,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 _param5)
  {
    Rect rect = new Rect(_param1, new Size(_param3.Width, _param3.Height));
    _param0.\u0023\u003DzSyuZtKbRCpVU(rect, _param2, _param3, _param4, _param5);
  }

  internal static unsafe void \u0023\u003DzSyuZtKbRCpVU(
    this WriteableBitmap _param0,
    Rect _param1,
    WriteableBitmap _param2,
    Rect _param3,
    Color _param4,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 _param5)
  {
    if (_param4.A == (byte) 0)
      return;
    int width1 = (int) _param1.Width;
    int height = (int) _param1.Height;
    int pixelWidth1 = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    Rect rect = new Rect(0.0, 0.0, (double) pixelWidth1, (double) pixelHeight);
    rect.Intersect(_param1);
    if (rect.IsEmpty)
      return;
    int pixelWidth2 = _param2.PixelWidth;
    using (BitmapContext bitmapContext1 = _param2.\u0023\u003DzjnjmjBtrwZM5())
    {
      using (BitmapContext bitmapContext2 = _param0.\u0023\u003DzjnjmjBtrwZM5())
      {
        int* numPtr1 = bitmapContext1.\u0023\u003DzSKG\u0024_qBsOJZc();
        int* numPtr2 = bitmapContext2.\u0023\u003DzSKG\u0024_qBsOJZc();
        int num1 = bitmapContext1.\u0023\u003DzxhbmvAVxpXvh();
        bitmapContext2.\u0023\u003DzxhbmvAVxpXvh();
        int x1 = (int) _param1.X;
        int y1 = (int) _param1.Y;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int a = (int) _param4.A;
        int r = (int) _param4.R;
        int g = (int) _param4.G;
        int b = (int) _param4.B;
        bool flag = _param4 != Colors.White;
        int width2 = (int) _param3.Width;
        double num6 = _param3.Width / _param1.Width;
        double num7 = _param3.Height / _param1.Height;
        int x2 = (int) _param3.X;
        int y2 = (int) _param3.Y;
        int num8 = -1;
        int num9 = -1;
        double num10 = (double) y2;
        int num11 = y1;
        for (int index1 = 0; index1 < height; ++index1)
        {
          if (num11 >= 0 && num11 < pixelHeight)
          {
            double num12 = (double) x2;
            int index2 = x1 + num11 * pixelWidth1;
            int num13 = x1;
            int num14 = *numPtr1;
            if (_param5 == (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6 && !flag)
            {
              int num15 = (int) num12 + (int) num10 * pixelWidth2;
              int num16 = num13 < 0 ? -num13 : 0;
              int num17 = num13 + num16;
              int num18 = pixelWidth2 - num16;
              int num19 = num17 + num18 < pixelWidth1 ? num18 : pixelWidth1 - num17;
              if (num19 > width2)
                num19 = width2;
              if (num19 > width1)
                num19 = width1;
              BitmapContext.\u0023\u003Dzk\u0024wemaJeIY7r(bitmapContext1, (num15 + num16) * 4, bitmapContext2, (index2 + num16) * 4, num19 * 4);
            }
            else
            {
              for (int index3 = 0; index3 < width1; ++index3)
              {
                if (num13 >= 0 && num13 < pixelWidth1)
                {
                  if ((int) num12 != num8 || (int) num10 != num9)
                  {
                    int index4 = (int) num12 + (int) num10 * pixelWidth2;
                    if (index4 >= 0 && index4 < num1)
                    {
                      num14 = numPtr1[index4];
                      num5 = num14 >> 24 & (int) byte.MaxValue;
                      num2 = num14 >> 16 /*0x10*/ & (int) byte.MaxValue;
                      num3 = num14 >> 8 & (int) byte.MaxValue;
                      num4 = num14 & (int) byte.MaxValue;
                      if (flag && num5 != 0)
                      {
                        num5 = num5 * a * 32897 >> 23;
                        num2 = (num2 * r * 32897 >> 23) * a * 32897 >> 23;
                        num3 = (num3 * g * 32897 >> 23) * a * 32897 >> 23;
                        num4 = (num4 * b * 32897 >> 23) * a * 32897 >> 23;
                        num14 = num5 << 24 | num2 << 16 /*0x10*/ | num3 << 8 | num4;
                      }
                    }
                    else
                      num5 = 0;
                  }
                  switch (_param5)
                  {
                    case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 3:
                      int num20 = numPtr2[index2];
                      int num21 = num20 >> 24 & (int) byte.MaxValue;
                      int num22 = num20 >> 16 /*0x10*/ & (int) byte.MaxValue;
                      int num23 = num20 >> 8 & (int) byte.MaxValue;
                      int num24 = num20 & (int) byte.MaxValue;
                      int num25 = num21 * num5 * 32897 >> 23 << 24 | num22 * num5 * 32897 >> 23 << 16 /*0x10*/ | num23 * num5 * 32897 >> 23 << 8 | num24 * num5 * 32897 >> 23;
                      numPtr2[index2] = num25;
                      break;
                    case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 5:
                      num2 = num14 >> 16 /*0x10*/ & (int) byte.MaxValue;
                      num3 = num14 >> 8 & (int) byte.MaxValue;
                      num4 = num14 & (int) byte.MaxValue;
                      if (num2 != (int) _param4.R || num3 != (int) _param4.G || num4 != (int) _param4.B)
                      {
                        numPtr2[index2] = num14;
                        break;
                      }
                      break;
                    case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6:
                      numPtr2[index2] = num14;
                      break;
                    default:
                      if (num5 > 0)
                      {
                        int num26 = numPtr2[index2];
                        int num27 = num26 >> 24 & (int) byte.MaxValue;
                        if ((num5 == (int) byte.MaxValue || num27 == 0) && _param5 != (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 1 && _param5 != (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 2 && _param5 != (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 4)
                        {
                          numPtr2[index2] = num14;
                          break;
                        }
                        int num28 = num26 >> 16 /*0x10*/ & (int) byte.MaxValue;
                        int num29 = num26 >> 8 & (int) byte.MaxValue;
                        int num30 = num26 & (int) byte.MaxValue;
                        switch (_param5)
                        {
                          case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0:
                            num26 = num5 + (num27 * ((int) byte.MaxValue - num5) * 32897 >> 23) << 24 | num2 + (num28 * ((int) byte.MaxValue - num5) * 32897 >> 23) << 16 /*0x10*/ | num3 + (num29 * ((int) byte.MaxValue - num5) * 32897 >> 23) << 8 | num4 + (num30 * ((int) byte.MaxValue - num5) * 32897 >> 23);
                            break;
                          case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 1:
                            int num31 = (int) byte.MaxValue <= num5 + num27 ? (int) byte.MaxValue : num5 + num27;
                            num26 = num31 << 24 | (num31 <= num2 + num28 ? num31 : num2 + num28) << 16 /*0x10*/ | (num31 <= num3 + num29 ? num31 : num3 + num29) << 8 | (num31 <= num4 + num30 ? num31 : num4 + num30);
                            break;
                          case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 2:
                            num26 = num27 << 24 | (num2 >= num28 ? 0 : num2 - num28) << 16 /*0x10*/ | (num3 >= num29 ? 0 : num3 - num29) << 8 | (num4 >= num30 ? 0 : num4 - num30);
                            break;
                          case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 4:
                            int num32 = num5 * num27 + 128 /*0x80*/;
                            int num33 = num2 * num28 + 128 /*0x80*/;
                            int num34 = num3 * num29 + 128 /*0x80*/;
                            int num35 = num4 * num30 + 128 /*0x80*/;
                            int num36 = (num32 >> 8) + num32 >> 8;
                            int num37 = (num33 >> 8) + num33 >> 8;
                            int num38 = (num34 >> 8) + num34 >> 8;
                            int num39 = (num35 >> 8) + num35 >> 8;
                            num26 = num36 << 24 | (num36 <= num37 ? num36 : num37) << 16 /*0x10*/ | (num36 <= num38 ? num36 : num38) << 8 | (num36 <= num39 ? num36 : num39);
                            break;
                        }
                        numPtr2[index2] = num26;
                        break;
                      }
                      break;
                  }
                }
                ++num13;
                ++index2;
                num12 += num6;
              }
            }
          }
          num10 += num7;
          ++num11;
        }
      }
    }
  }

  internal static unsafe void \u0023\u003DzSyuZtKbRCpVU(
    BitmapContext _param0,
    int _param1,
    int _param2,
    Rect _param3,
    BitmapContext _param4,
    Rect _param5,
    int _param6)
  {
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 zEzdVavx1gj20 = (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0;
    int width1 = (int) _param3.Width;
    int height = (int) _param3.Height;
    Rect rect = new Rect(0.0, 0.0, (double) _param1, (double) _param2);
    rect.Intersect(_param3);
    if (rect.IsEmpty)
      return;
    int* numPtr1 = _param4.\u0023\u003DzSKG\u0024_qBsOJZc();
    int* numPtr2 = _param0.\u0023\u003DzSKG\u0024_qBsOJZc();
    int num1 = _param4.\u0023\u003DzxhbmvAVxpXvh();
    int x1 = (int) _param3.X;
    int y1 = (int) _param3.Y;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int width2 = (int) _param5.Width;
    double num6 = _param5.Width / _param3.Width;
    double num7 = _param5.Height / _param3.Height;
    int x2 = (int) _param5.X;
    int y2 = (int) _param5.Y;
    int num8 = -1;
    int num9 = -1;
    double num10 = (double) y2;
    int num11 = y1;
    for (int index1 = 0; index1 < height; ++index1)
    {
      if (num11 >= 0 && num11 < _param2)
      {
        double num12 = (double) x2;
        int index2 = x1 + num11 * _param1;
        int num13 = x1;
        int num14 = *numPtr1;
        if (zEzdVavx1gj20 == (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6)
        {
          int num15 = (int) num12 + (int) num10 * _param6;
          int num16 = num13 < 0 ? -num13 : 0;
          int num17 = num13 + num16;
          int num18 = _param6 - num16;
          int num19 = num17 + num18 < _param1 ? num18 : _param1 - num17;
          if (num19 > width2)
            num19 = width2;
          if (num19 > width1)
            num19 = width1;
          BitmapContext.\u0023\u003Dzk\u0024wemaJeIY7r(_param4, (num15 + num16) * 4, _param0, (index2 + num16) * 4, num19 * 4);
        }
        else
        {
          for (int index3 = 0; index3 < width1; ++index3)
          {
            if (num13 >= 0 && num13 < _param1)
            {
              if ((int) num12 != num8 || (int) num10 != num9)
              {
                int index4 = (int) num12 + (int) num10 * _param6;
                if (index4 >= 0 && index4 < num1)
                {
                  num14 = numPtr1[index4];
                  num5 = num14 >> 24 & (int) byte.MaxValue;
                  num2 = num14 >> 16 /*0x10*/ & (int) byte.MaxValue;
                  num3 = num14 >> 8 & (int) byte.MaxValue;
                  num4 = num14 & (int) byte.MaxValue;
                }
                else
                  num5 = 0;
              }
              switch (zEzdVavx1gj20)
              {
                case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 3:
                  int num20 = numPtr2[index2];
                  int num21 = num20 >> 24 & (int) byte.MaxValue;
                  int num22 = num20 >> 16 /*0x10*/ & (int) byte.MaxValue;
                  int num23 = num20 >> 8 & (int) byte.MaxValue;
                  int num24 = num20 & (int) byte.MaxValue;
                  int num25 = num21 * num5 * 32897 >> 23 << 24 | num22 * num5 * 32897 >> 23 << 16 /*0x10*/ | num23 * num5 * 32897 >> 23 << 8 | num24 * num5 * 32897 >> 23;
                  numPtr2[index2] = num25;
                  break;
                case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 5:
                  num2 = num14 >> 16 /*0x10*/ & (int) byte.MaxValue;
                  num3 = num14 >> 8 & (int) byte.MaxValue;
                  num4 = num14 & (int) byte.MaxValue;
                  if (num2 != (int) byte.MaxValue || num3 != (int) byte.MaxValue || num4 != (int) byte.MaxValue)
                  {
                    numPtr2[index2] = num14;
                    break;
                  }
                  break;
                case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6:
                  numPtr2[index2] = num14;
                  break;
                default:
                  if (num5 > 0)
                  {
                    int num26 = numPtr2[index2];
                    int num27 = num26 >> 24 & (int) byte.MaxValue;
                    if ((num5 == (int) byte.MaxValue || num27 == 0) && zEzdVavx1gj20 != (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 1 && zEzdVavx1gj20 != (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 2 && zEzdVavx1gj20 != (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 4)
                    {
                      numPtr2[index2] = num14;
                      break;
                    }
                    int num28 = num26 >> 16 /*0x10*/ & (int) byte.MaxValue;
                    int num29 = num26 >> 8 & (int) byte.MaxValue;
                    int num30 = num26 & (int) byte.MaxValue;
                    switch (zEzdVavx1gj20)
                    {
                      case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0:
                        num26 = num5 + (num27 * ((int) byte.MaxValue - num5) * 32897 >> 23) << 24 | num2 + (num28 * ((int) byte.MaxValue - num5) * 32897 >> 23) << 16 /*0x10*/ | num3 + (num29 * ((int) byte.MaxValue - num5) * 32897 >> 23) << 8 | num4 + (num30 * ((int) byte.MaxValue - num5) * 32897 >> 23);
                        break;
                      case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 1:
                        int num31 = (int) byte.MaxValue <= num5 + num27 ? (int) byte.MaxValue : num5 + num27;
                        num26 = num31 << 24 | (num31 <= num2 + num28 ? num31 : num2 + num28) << 16 /*0x10*/ | (num31 <= num3 + num29 ? num31 : num3 + num29) << 8 | (num31 <= num4 + num30 ? num31 : num4 + num30);
                        break;
                      case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 2:
                        num26 = num27 << 24 | (num2 >= num28 ? 0 : num2 - num28) << 16 /*0x10*/ | (num3 >= num29 ? 0 : num3 - num29) << 8 | (num4 >= num30 ? 0 : num4 - num30);
                        break;
                      case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 4:
                        int num32 = num5 * num27 + 128 /*0x80*/;
                        int num33 = num2 * num28 + 128 /*0x80*/;
                        int num34 = num3 * num29 + 128 /*0x80*/;
                        int num35 = num4 * num30 + 128 /*0x80*/;
                        int num36 = (num32 >> 8) + num32 >> 8;
                        int num37 = (num33 >> 8) + num33 >> 8;
                        int num38 = (num34 >> 8) + num34 >> 8;
                        int num39 = (num35 >> 8) + num35 >> 8;
                        num26 = num36 << 24 | (num36 <= num37 ? num36 : num37) << 16 /*0x10*/ | (num36 <= num38 ? num36 : num38) << 8 | (num36 <= num39 ? num36 : num39);
                        break;
                    }
                    numPtr2[index2] = num26;
                    break;
                  }
                  break;
              }
            }
            ++num13;
            ++index2;
            num12 += num6;
          }
        }
      }
      num10 += num7;
      ++num11;
    }
  }

  internal static byte[] \u0023\u003DzRSAv_naMs3YT(
    this WriteableBitmap _param0,
    int _param1,
    int _param2)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      if (_param2 == -1)
        _param2 = bitmapContext.\u0023\u003DzxhbmvAVxpXvh();
      int length = _param2 * 4;
      byte[] numArray = new byte[length];
      BitmapContext.\u0023\u003Dzk\u0024wemaJeIY7r(bitmapContext, _param1, numArray, 0, length);
      return numArray;
    }
  }

  internal static byte[] \u0023\u003DzRSAv_naMs3YT(this WriteableBitmap _param0, int _param1)
  {
    return _param0.\u0023\u003DzRSAv_naMs3YT(0, _param1);
  }

  internal static byte[] \u0023\u003DzRSAv_naMs3YT(this WriteableBitmap _param0)
  {
    return _param0.\u0023\u003DzRSAv_naMs3YT(0, -1);
  }

  internal static WriteableBitmap \u0023\u003DztnwaPREJn5PW(
    this WriteableBitmap _param0,
    byte[] _param1,
    int _param2,
    int _param3)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      BitmapContext.\u0023\u003Dzk\u0024wemaJeIY7r(_param1, _param2, bitmapContext, 0, _param3);
      return _param0;
    }
  }

  internal static WriteableBitmap \u0023\u003DztnwaPREJn5PW(
    this WriteableBitmap _param0,
    byte[] _param1,
    int _param2)
  {
    return _param0.\u0023\u003DztnwaPREJn5PW(_param1, 0, _param2);
  }

  internal static WriteableBitmap \u0023\u003DztnwaPREJn5PW(
    this WriteableBitmap _param0,
    byte[] _param1)
  {
    return _param0.\u0023\u003DztnwaPREJn5PW(_param1, 0, _param1.Length);
  }

  internal static unsafe void \u0023\u003Dz7wT8YaG6TYLJ(
    this WriteableBitmap _param0,
    Stream _param1)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      byte[] buffer1 = new byte[bitmapContext.\u0023\u003DzxhbmvAVxpXvh() * 4];
      int index1 = 0;
      int num1 = pixelWidth << 2;
      int num2 = pixelWidth << 3;
      int index2 = (pixelHeight - 1) * num1;
      for (int index3 = 0; index3 < pixelHeight; ++index3)
      {
        for (int index4 = 0; index4 < pixelWidth; ++index4)
        {
          int num3 = numPtr[index1];
          buffer1[index2] = (byte) num3;
          buffer1[index2 + 1] = (byte) (num3 >> 8);
          buffer1[index2 + 2] = (byte) (num3 >> 16 /*0x10*/);
          buffer1[index2 + 3] = (byte) (num3 >> 24);
          ++index1;
          index2 += 4;
        }
        index2 -= num2;
      }
      byte[] numArray = new byte[18];
      numArray[2] = (byte) 2;
      numArray[12] = (byte) pixelWidth;
      numArray[13] = (byte) ((pixelWidth & 65280) >> 8);
      numArray[14] = (byte) pixelHeight;
      numArray[15] = (byte) ((pixelHeight & 65280) >> 8);
      numArray[16 /*0x10*/] = (byte) 32 /*0x20*/;
      byte[] buffer2 = numArray;
      using (BinaryWriter binaryWriter = new BinaryWriter(_param1))
      {
        binaryWriter.Write(buffer2);
        binaryWriter.Write(buffer1);
      }
    }
  }

  internal static WriteableBitmap \u0023\u003Dz50Ym0LI\u003D(
    this WriteableBitmap _param0,
    string _param1)
  {
    string name = new AssemblyName(Assembly.GetCallingAssembly().FullName).Name;
    return _param0.\u0023\u003Dz_YJ1GTI\u003D(name + "" + _param1);
  }

  internal static WriteableBitmap \u0023\u003Dz_YJ1GTI\u003D(
    this WriteableBitmap _param0,
    string _param1)
  {
    using (Stream stream = Application.GetResourceStream(new Uri(_param1, UriKind.Relative)).Stream)
    {
      BitmapImage source = new BitmapImage();
      source.StreamSource = stream;
      _param0 = new WriteableBitmap((BitmapSource) source);
      source.UriSource = (Uri) null;
      return _param0;
    }
  }

  internal static void \u0023\u003DzVRUUvzhAr5SR(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003DzVRUUvzhAr5SR(_param1, _param2, _param3, _param4, num2, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
  }

  internal static unsafe void \u0023\u003DzVRUUvzhAr5SR(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 _param6)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    int num1 = _param5 >> 24 & (int) byte.MaxValue;
    int num2 = _param5 >> 16 /*0x10*/ & (int) byte.MaxValue;
    int num3 = _param5 >> 8 & (int) byte.MaxValue;
    int num4 = _param5 & (int) byte.MaxValue;
    bool flag = _param6 == (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6 || num1 == (int) byte.MaxValue;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      if (_param1 < 0 && _param3 < 0 || _param2 < 0 && _param4 < 0 || _param1 >= pixelWidth && _param3 >= pixelWidth || _param2 >= pixelHeight && _param4 >= pixelHeight)
        return;
      if (_param1 < 0)
        _param1 = 0;
      if (_param1 >= pixelWidth)
        _param1 = pixelWidth - 1;
      if (_param2 < 0)
        _param2 = 0;
      if (_param2 >= pixelHeight)
        _param2 = pixelHeight - 1;
      if (_param3 < 0)
        _param3 = 0;
      if (_param3 >= pixelWidth)
        _param3 = pixelWidth - 1;
      if (_param4 < 0)
        _param4 = 0;
      if (_param4 >= pixelHeight)
        _param4 = pixelHeight - 1;
      if (_param2 > _param4)
      {
        _param4 -= _param2;
        _param2 += _param4;
        _param4 = _param2 - _param4;
      }
      int num5 = _param2 * pixelWidth;
      int num6 = num5 + _param1;
      int num7 = num5 + _param3;
      for (int index = num6; index <= num7; ++index)
        numPtr[index] = flag ? _param5 : \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index], num1, num2, num3, num4);
      int num8 = _param3 - _param1 + 1;
      int num9 = num6 * 4;
      int num10 = _param4 * pixelWidth + _param1;
      for (int index1 = num6 + pixelWidth; index1 <= num10; index1 += pixelWidth)
      {
        if (flag)
        {
          BitmapContext.\u0023\u003Dzk\u0024wemaJeIY7r(bitmapContext, num9, bitmapContext, index1 * 4, num8 * 4);
        }
        else
        {
          for (int index2 = 0; index2 < num8; ++index2)
          {
            int index3 = index1 + index2;
            numPtr[index3] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index3], num1, num2, num3, num4);
          }
        }
      }
    }
  }

  private static int \u0023\u003Dz0bTqvegIuCkN(
    int _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    int num1 = _param0;
    int num2 = num1 >> 24 & (int) byte.MaxValue;
    int num3 = num1 >> 16 /*0x10*/ & (int) byte.MaxValue;
    int num4 = num1 >> 8 & (int) byte.MaxValue;
    int num5 = num1 & (int) byte.MaxValue;
    return _param1 + (num2 * ((int) byte.MaxValue - _param1) * 32897 >> 23) << 24 | _param2 + (num3 * ((int) byte.MaxValue - _param1) * 32897 >> 23) << 16 /*0x10*/ | _param3 + (num4 * ((int) byte.MaxValue - _param1) * 32897 >> 23) << 8 | _param4 + (num5 * ((int) byte.MaxValue - _param1) * 32897 >> 23);
  }

  internal static unsafe void \u0023\u003DzVRUUvzhAr5SR(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Func<int, int, int> _param5,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 _param6)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      if (_param1 < 0 && _param3 < 0 || _param2 < 0 && _param4 < 0 || _param1 >= pixelWidth && _param3 >= pixelWidth || _param2 >= pixelHeight && _param4 >= pixelHeight)
        return;
      if (_param1 < 0)
        _param1 = 0;
      if (_param2 < 0)
        _param2 = 0;
      if (_param3 < 0)
        _param3 = 0;
      if (_param4 < 0)
        _param4 = 0;
      if (_param1 > pixelWidth)
        _param1 = pixelWidth;
      if (_param2 > pixelHeight)
        _param2 = pixelHeight;
      if (_param3 > pixelWidth)
        _param3 = pixelWidth;
      if (_param4 > pixelHeight)
        _param4 = pixelHeight;
      if (_param2 > _param4)
      {
        _param4 -= _param2;
        _param2 += _param4;
        _param4 = _param2 - _param4;
      }
      int num1 = _param3 - _param1 + 1;
      int num2 = _param2 * pixelWidth + _param1;
      int num3 = _param4 * pixelWidth + _param1;
      int num4 = _param2;
      int num5 = num2;
      while (num5 < num3)
      {
        int num6 = _param1;
        int num7 = 0;
        while (num7 < num1)
        {
          int index = num5 + num7;
          int num8 = _param5(num6, num4);
          int num9 = num8 >> 24 & (int) byte.MaxValue;
          int num10 = num8 >> 16 /*0x10*/ & (int) byte.MaxValue;
          int num11 = num8 >> 8 & (int) byte.MaxValue;
          int num12 = num8 & (int) byte.MaxValue;
          bool flag = _param6 == (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6 || num9 == (int) byte.MaxValue;
          numPtr[index] = flag ? num8 : \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index], num9, num10, num11, num12);
          ++num7;
          ++num6;
        }
        num5 += pixelWidth;
        ++num4;
      }
    }
  }

  internal static void \u0023\u003Dz\u0024cz\u0024mtm8DTUg(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003Dz\u0024cz\u0024mtm8DTUg(_param1, _param2, _param3, _param4, num2);
  }

  internal static void \u0023\u003Dz\u0024cz\u0024mtm8DTUg(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    int num1 = _param3 - _param1 >> 1;
    int num2 = _param4 - _param2 >> 1;
    int num3 = _param1 + num1;
    int num4 = _param2 + num2;
    _param0.\u0023\u003DzFUd3PqlNUK45_oOAYlinHKM\u003D(num3, num4, num1, num2, _param5);
  }

  internal static void \u0023\u003DzFUd3PqlNUK45_oOAYlinHKM\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003DzFUd3PqlNUK45_oOAYlinHKM\u003D(_param1, _param2, _param3, _param4, num2);
  }

  internal static void \u0023\u003DzFUd3PqlNUK45_oOAYlinHKM\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzFUd3PqlNUK45_oOAYlinHKM\u003D(bitmapContext, _param1, _param2, _param3, _param4, _param5, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
  }

  internal static unsafe void \u0023\u003DzFUd3PqlNUK45_oOAYlinHKM\u003D(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 _param6)
  {
    int* numPtr = _param0.\u0023\u003DzSKG\u0024_qBsOJZc();
    int num1 = _param0.\u0023\u003DzOc4Ixb6AQPL8();
    int num2 = _param0.\u0023\u003DzeaNYBEDp1wgD();
    if (_param3 < 1 || _param4 < 1)
      return;
    int num3 = _param3;
    int num4 = 0;
    int num5 = _param3 * _param3 << 1;
    int num6 = _param4 * _param4 << 1;
    int num7 = _param4 * _param4 * (1 - (_param3 << 1));
    int num8 = _param3 * _param3;
    int num9 = 0;
    int num10 = num6 * _param3;
    int num11 = 0;
    int num12 = _param5 >> 24 & (int) byte.MaxValue;
    int num13 = _param5 >> 16 /*0x10*/ & (int) byte.MaxValue;
    int num14 = _param5 >> 8 & (int) byte.MaxValue;
    int num15 = _param5 & (int) byte.MaxValue;
    bool flag = _param6 == (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6 || num12 == (int) byte.MaxValue;
    while (num10 >= num11)
    {
      int num16 = _param2 + num4;
      int num17 = _param2 - num4;
      if (num16 < 0)
        num16 = 0;
      if (num16 >= num2)
        num16 = num2 - 1;
      if (num17 < 0)
        num17 = 0;
      if (num17 >= num2)
        num17 = num2 - 1;
      int num18 = num16 * num1;
      int num19 = num17 * num1;
      int num20 = _param1 + num3;
      int num21 = _param1 - num3;
      if (num20 < 0)
        num20 = 0;
      if (num20 >= num1)
        num20 = num1 - 1;
      if (num21 < 0)
        num21 = 0;
      if (num21 >= num1)
        num21 = num1 - 1;
      for (int index = num21; index <= num20; ++index)
      {
        numPtr[index + num18] = flag ? _param5 : \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index + num18], num12, num13, num14, num15);
        numPtr[index + num19] = flag ? _param5 : \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index + num19], num12, num13, num14, num15);
      }
      ++num4;
      num11 += num5;
      num9 += num8;
      num8 += num5;
      if (num7 + (num9 << 1) > 0)
      {
        --num3;
        num10 -= num6;
        num9 += num7;
        num7 += num6;
      }
    }
    int num22 = 0;
    int num23 = _param4;
    int num24 = _param2 + num23;
    int num25 = _param2 - num23;
    if (num24 < 0)
      num24 = 0;
    if (num24 >= num2)
      num24 = num2 - 1;
    if (num25 < 0)
      num25 = 0;
    if (num25 >= num2)
      num25 = num2 - 1;
    int num26 = num24 * num1;
    int num27 = num25 * num1;
    int num28 = _param4 * _param4;
    int num29 = _param3 * _param3 * (1 - (_param4 << 1));
    int num30 = 0;
    int num31 = 0;
    int num32 = num5 * _param4;
    while (num31 <= num32)
    {
      int num33 = _param1 + num22;
      int num34 = _param1 - num22;
      if (num33 < 0)
        num33 = 0;
      if (num33 >= num1)
        num33 = num1 - 1;
      if (num34 < 0)
        num34 = 0;
      if (num34 >= num1)
        num34 = num1 - 1;
      for (int index = num34; index <= num33; ++index)
      {
        numPtr[index + num26] = flag ? _param5 : \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index + num26], num12, num13, num14, num15);
        numPtr[index + num27] = flag ? _param5 : \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index + num27], num12, num13, num14, num15);
      }
      ++num22;
      num31 += num6;
      num30 += num28;
      num28 += num6;
      if (num29 + (num30 << 1) > 0)
      {
        --num23;
        int num35 = _param2 + num23;
        int num36 = _param2 - num23;
        if (num35 < 0)
          num35 = 0;
        if (num35 >= num2)
          num35 = num2 - 1;
        if (num36 < 0)
          num36 = 0;
        if (num36 >= num2)
          num36 = num2 - 1;
        num26 = num35 * num1;
        num27 = num36 * num1;
        num32 -= num5;
        num30 += num29;
        num29 += num5;
      }
    }
  }

  internal static void \u0023\u003Dz_I15ZX7u91\u0024T(
    this WriteableBitmap _param0,
    int[] _param1,
    Color _param2)
  {
    int num1 = (int) _param2.A + 1;
    int num2 = (int) _param2.A << 24 | (int) (byte) ((int) _param2.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param2.G * num1 >> 8) << 8 | (int) (byte) ((int) _param2.B * num1 >> 8);
    _param0.\u0023\u003Dz_I15ZX7u91\u0024T(_param1, num2, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
  }

  internal static unsafe void \u0023\u003Dz_I15ZX7u91\u0024T(
    this WriteableBitmap _param0,
    int[] _param1,
    int _param2,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 _param3)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    int num1 = _param2 >> 24 & (int) byte.MaxValue;
    int num2 = _param2 >> 16 /*0x10*/ & (int) byte.MaxValue;
    int num3 = _param2 >> 8 & (int) byte.MaxValue;
    int num4 = _param2 & (int) byte.MaxValue;
    bool flag = _param3 == (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6 || num1 == (int) byte.MaxValue;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      int length = _param1.Length;
      int[] numArray = new int[_param1.Length >> 1];
      int num5 = pixelHeight;
      int num6 = 0;
      for (int index = 1; index < length; index += 2)
      {
        int num7 = _param1[index];
        if (num7 < num5)
          num5 = num7;
        if (num7 > num6)
          num6 = num7;
      }
      if (num5 < 0)
        num5 = 0;
      if (num6 >= pixelHeight)
        num6 = pixelHeight - 1;
      for (int index1 = num5; index1 <= num6; ++index1)
      {
        float num8 = (float) _param1[0];
        float num9 = (float) _param1[1];
        int num10 = 0;
        for (int index2 = 2; index2 < length; index2 += 2)
        {
          float num11 = (float) _param1[index2];
          float num12 = (float) _param1[index2 + 1];
          if ((double) num9 < (double) index1 && (double) num12 >= (double) index1 || (double) num12 < (double) index1 && (double) num9 >= (double) index1)
            numArray[num10++] = (int) ((double) num8 + ((double) index1 - (double) num9) / ((double) num12 - (double) num9) * ((double) num11 - (double) num8));
          num8 = num11;
          num9 = num12;
        }
        for (int index3 = 1; index3 < num10; ++index3)
        {
          int num13 = numArray[index3];
          int index4;
          for (index4 = index3; index4 > 0 && numArray[index4 - 1] > num13; --index4)
            numArray[index4] = numArray[index4 - 1];
          numArray[index4] = num13;
        }
        for (int index5 = 0; index5 < num10 - 1; index5 += 2)
        {
          int num14 = numArray[index5];
          int num15 = numArray[index5 + 1];
          if (num15 > 0 && num14 < pixelWidth)
          {
            if (num14 < 0)
              num14 = 0;
            if (num15 >= pixelWidth)
              num15 = pixelWidth - 1;
            for (int index6 = num14; index6 <= num15; ++index6)
            {
              int index7 = index1 * pixelWidth + index6;
              numPtr[index7] = flag ? _param2 : \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index7], num1, num2, num3, num4);
            }
          }
        }
      }
    }
  }

  internal static unsafe void \u0023\u003Dz_I15ZX7u91\u0024T(
    this WriteableBitmap _param0,
    int[] _param1,
    Func<int, int, int> _param2,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20 _param3)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      int length = _param1.Length;
      int[] numArray = new int[_param1.Length >> 1];
      int num1 = pixelHeight;
      int num2 = 0;
      for (int index = 1; index < length; index += 2)
      {
        int num3 = _param1[index];
        if (num3 < num1)
          num1 = num3;
        if (num3 > num2)
          num2 = num3;
      }
      if (num1 < 0)
        num1 = 0;
      if (num2 >= pixelHeight)
        num2 = pixelHeight - 1;
      for (int index1 = num1; index1 <= num2; ++index1)
      {
        float num4 = (float) _param1[0];
        float num5 = (float) _param1[1];
        int num6 = 0;
        for (int index2 = 2; index2 < length; index2 += 2)
        {
          float num7 = (float) _param1[index2];
          float num8 = (float) _param1[index2 + 1];
          if ((double) num5 < (double) index1 && (double) num8 >= (double) index1 || (double) num8 < (double) index1 && (double) num5 >= (double) index1)
            numArray[num6++] = (int) ((double) num4 + ((double) index1 - (double) num5) / ((double) num8 - (double) num5) * ((double) num7 - (double) num4));
          num4 = num7;
          num5 = num8;
        }
        for (int index3 = 1; index3 < num6; ++index3)
        {
          int num9 = numArray[index3];
          int index4;
          for (index4 = index3; index4 > 0 && numArray[index4 - 1] > num9; --index4)
            numArray[index4] = numArray[index4 - 1];
          numArray[index4] = num9;
        }
        for (int index5 = 0; index5 < num6 - 1; index5 += 2)
        {
          int num10 = numArray[index5];
          int num11 = numArray[index5 + 1];
          if (num11 > 0 && num10 < pixelWidth)
          {
            if (num10 < 0)
              num10 = 0;
            if (num11 >= pixelWidth)
              num11 = pixelWidth - 1;
            for (int index6 = num10; index6 <= num11; ++index6)
            {
              int index7 = index1 * pixelWidth + index6;
              int num12 = _param2(index6, index1);
              int num13 = num12 >> 24 & (int) byte.MaxValue;
              int num14 = num12 >> 16 /*0x10*/ & (int) byte.MaxValue;
              int num15 = num12 >> 8 & (int) byte.MaxValue;
              int num16 = num12 & (int) byte.MaxValue;
              bool flag = _param3 == (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 6 || num13 == (int) byte.MaxValue;
              numPtr[index7] = flag ? num12 : \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz0bTqvegIuCkN(numPtr[index7], num13, num14, num15, num16);
            }
          }
        }
      }
    }
  }

  internal static void \u0023\u003DzbULVDJzEYzHX(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8,
    Color _param9)
  {
    int num1 = (int) _param9.A + 1;
    int num2 = (int) _param9.A << 24 | (int) (byte) ((int) _param9.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param9.G * num1 >> 8) << 8 | (int) (byte) ((int) _param9.B * num1 >> 8);
    _param0.\u0023\u003DzbULVDJzEYzHX(_param1, _param2, _param3, _param4, _param5, _param6, _param7, _param8, num2);
  }

  internal static void \u0023\u003DzbULVDJzEYzHX(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8,
    int _param9)
  {
    _param0.\u0023\u003Dz_I15ZX7u91\u0024T(new int[10]
    {
      _param1,
      _param2,
      _param3,
      _param4,
      _param5,
      _param6,
      _param7,
      _param8,
      _param1,
      _param2
    }, _param9, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
  }

  internal static void \u0023\u003DzcwowdvolC4hP(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    Color _param7)
  {
    int num1 = (int) _param7.A + 1;
    int num2 = (int) _param7.A << 24 | (int) (byte) ((int) _param7.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param7.G * num1 >> 8) << 8 | (int) (byte) ((int) _param7.B * num1 >> 8);
    _param0.\u0023\u003DzcwowdvolC4hP(_param1, _param2, _param3, _param4, _param5, _param6, num2);
  }

  internal static void \u0023\u003DzcwowdvolC4hP(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7)
  {
    _param0.\u0023\u003Dz_I15ZX7u91\u0024T(new int[8]
    {
      _param1,
      _param2,
      _param3,
      _param4,
      _param5,
      _param6,
      _param1,
      _param2
    }, _param7, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
  }

  private static unsafe List<int> \u0023\u003Dzsc3kcYymJ8aO(
    int _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8,
    BitmapContext _param9,
    int _param10,
    int _param11)
  {
    _param9.\u0023\u003DzSKG\u0024_qBsOJZc();
    int num1 = Math.Min(_param0, Math.Min(_param2, Math.Min(_param4, _param6)));
    int num2 = Math.Min(_param1, Math.Min(_param3, Math.Min(_param5, _param7)));
    int num3 = Math.Max(_param0, Math.Max(_param2, Math.Max(_param4, _param6)));
    int num4 = Math.Max(_param1, Math.Max(_param3, Math.Max(_param5, _param7)));
    int num5 = num3 - num1;
    int num6 = num2;
    int num7 = num4 - num6;
    if (num5 > num7)
      num7 = num5;
    List<int> intList = new List<int>();
    if (num7 != 0)
    {
      float num8 = 2f / (float) num7;
      for (float num9 = 0.0f; (double) num9 <= 1.0; num9 += num8)
      {
        float num10 = num9 * num9;
        float num11 = 1f - num9;
        float num12 = num11 * num11;
        int num13 = (int) ((double) num11 * (double) num12 * (double) _param0 + 3.0 * (double) num9 * (double) num12 * (double) _param2 + 3.0 * (double) num11 * (double) num10 * (double) _param4 + (double) num9 * (double) num10 * (double) _param6);
        int num14 = (int) ((double) num11 * (double) num12 * (double) _param1 + 3.0 * (double) num9 * (double) num12 * (double) _param3 + 3.0 * (double) num11 * (double) num10 * (double) _param5 + (double) num9 * (double) num10 * (double) _param7);
        intList.Add(num13);
        intList.Add(num14);
      }
      intList.Add(_param6);
      intList.Add(_param7);
    }
    return intList;
  }

  internal static void \u0023\u003DzpdAyoZT4N1vyMUFYKg\u003D\u003D(
    this WriteableBitmap _param0,
    int[] _param1,
    Color _param2)
  {
    int num1 = (int) _param2.A + 1;
    int num2 = (int) _param2.A << 24 | (int) (byte) ((int) _param2.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param2.G * num1 >> 8) << 8 | (int) (byte) ((int) _param2.B * num1 >> 8);
    _param0.\u0023\u003DzpdAyoZT4N1vyMUFYKg\u003D\u003D(_param1, num2);
  }

  internal static void \u0023\u003DzpdAyoZT4N1vyMUFYKg\u003D\u003D(
    this WriteableBitmap _param0,
    int[] _param1,
    int _param2)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int num1 = _param1[0];
      int num2 = _param1[1];
      List<int> intList = new List<int>();
      for (int index = 2; index + 5 < _param1.Length; index += 6)
      {
        int num3 = _param1[index + 4];
        int num4 = _param1[index + 5];
        intList.AddRange((IEnumerable<int>) \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzsc3kcYymJ8aO(num1, num2, _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], num3, num4, _param2, bitmapContext, pixelWidth, pixelHeight));
        num1 = num3;
        num2 = num4;
      }
      _param0.\u0023\u003Dz_I15ZX7u91\u0024T(intList.ToArray(), _param2, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
    }
  }

  private static unsafe List<int> \u0023\u003Dz8Oty5u\u0024Dp\u0024PY(
    int _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    float _param8,
    int _param9,
    BitmapContext _param10,
    int _param11,
    int _param12)
  {
    _param10.\u0023\u003DzSKG\u0024_qBsOJZc();
    int num1 = Math.Min(_param0, Math.Min(_param2, Math.Min(_param4, _param6)));
    int num2 = Math.Min(_param1, Math.Min(_param3, Math.Min(_param5, _param7)));
    int num3 = Math.Max(_param0, Math.Max(_param2, Math.Max(_param4, _param6)));
    int num4 = Math.Max(_param1, Math.Max(_param3, Math.Max(_param5, _param7)));
    int num5 = num3 - num1;
    int num6 = num2;
    int num7 = num4 - num6;
    if (num5 > num7)
      num7 = num5;
    List<int> intList = new List<int>();
    if (num7 != 0)
    {
      float num8 = 2f / (float) num7;
      float num9 = _param8 * (float) (_param4 - _param0);
      float num10 = _param8 * (float) (_param5 - _param1);
      float num11 = _param8 * (float) (_param6 - _param2);
      float num12 = _param8 * (float) (_param7 - _param3);
      float num13 = num9 + num11 + (float) (2 * _param2) - (float) (2 * _param4);
      float num14 = num10 + num12 + (float) (2 * _param3) - (float) (2 * _param5);
      float num15 = -2f * num9 - num11 - (float) (3 * _param2) + (float) (3 * _param4);
      float num16 = -2f * num10 - num12 - (float) (3 * _param3) + (float) (3 * _param5);
      for (float num17 = 0.0f; (double) num17 <= 1.0; num17 += num8)
      {
        float num18 = num17 * num17;
        int num19 = (int) ((double) num13 * (double) num18 * (double) num17 + (double) num15 * (double) num18 + (double) num9 * (double) num17 + (double) _param2);
        int num20 = (int) ((double) num14 * (double) num18 * (double) num17 + (double) num16 * (double) num18 + (double) num10 * (double) num17 + (double) _param3);
        intList.Add(num19);
        intList.Add(num20);
      }
      intList.Add(_param4);
      intList.Add(_param5);
    }
    return intList;
  }

  internal static void \u0023\u003DzQsuJMLPbAICl(
    this WriteableBitmap _param0,
    int[] _param1,
    float _param2,
    Color _param3)
  {
    int num1 = (int) _param3.A + 1;
    int num2 = (int) _param3.A << 24 | (int) (byte) ((int) _param3.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param3.G * num1 >> 8) << 8 | (int) (byte) ((int) _param3.B * num1 >> 8);
    _param0.\u0023\u003DzQsuJMLPbAICl(_param1, _param2, num2);
  }

  internal static void \u0023\u003DzQsuJMLPbAICl(
    this WriteableBitmap _param0,
    int[] _param1,
    float _param2,
    int _param3)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      List<int> intList = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz8Oty5u\u0024Dp\u0024PY(_param1[0], _param1[1], _param1[0], _param1[1], _param1[2], _param1[3], _param1[4], _param1[5], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
      int index;
      for (index = 2; index < _param1.Length - 4; index += 2)
        intList.AddRange((IEnumerable<int>) \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz8Oty5u\u0024Dp\u0024PY(_param1[index - 2], _param1[index - 1], _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[index + 4], _param1[index + 5], _param2, _param3, bitmapContext, pixelWidth, pixelHeight));
      intList.AddRange((IEnumerable<int>) \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz8Oty5u\u0024Dp\u0024PY(_param1[index - 2], _param1[index - 1], _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[index + 2], _param1[index + 3], _param2, _param3, bitmapContext, pixelWidth, pixelHeight));
      _param0.\u0023\u003Dz_I15ZX7u91\u0024T(intList.ToArray(), _param3, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
    }
  }

  internal static void \u0023\u003DzRbwdQ0iQbY1k(
    this WriteableBitmap _param0,
    int[] _param1,
    float _param2,
    Color _param3)
  {
    int num1 = (int) _param3.A + 1;
    int num2 = (int) _param3.A << 24 | (int) (byte) ((int) _param3.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param3.G * num1 >> 8) << 8 | (int) (byte) ((int) _param3.B * num1 >> 8);
    _param0.\u0023\u003DzRbwdQ0iQbY1k(_param1, _param2, num2);
  }

  internal static void \u0023\u003DzRbwdQ0iQbY1k(
    this WriteableBitmap _param0,
    int[] _param1,
    float _param2,
    int _param3)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int length = _param1.Length;
      List<int> intList = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz8Oty5u\u0024Dp\u0024PY(_param1[length - 2], _param1[length - 1], _param1[0], _param1[1], _param1[2], _param1[3], _param1[4], _param1[5], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
      int index;
      for (index = 2; index < length - 4; index += 2)
        intList.AddRange((IEnumerable<int>) \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz8Oty5u\u0024Dp\u0024PY(_param1[index - 2], _param1[index - 1], _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[index + 4], _param1[index + 5], _param2, _param3, bitmapContext, pixelWidth, pixelHeight));
      intList.AddRange((IEnumerable<int>) \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz8Oty5u\u0024Dp\u0024PY(_param1[index - 2], _param1[index - 1], _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[0], _param1[1], _param2, _param3, bitmapContext, pixelWidth, pixelHeight));
      intList.AddRange((IEnumerable<int>) \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz8Oty5u\u0024Dp\u0024PY(_param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[0], _param1[1], _param1[2], _param1[3], _param2, _param3, bitmapContext, pixelWidth, pixelHeight));
      _param0.\u0023\u003Dz_I15ZX7u91\u0024T(intList.ToArray(), _param3, (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzEzdVAvx1gj20) 0);
    }
  }

  internal static WriteableBitmap \u0023\u003DzMxv7fx65tV1yUOcJ1g\u003D\u003D(
    this WriteableBitmap _param0,
    int[,] _param1)
  {
    int num1 = 0;
    int[,] numArray = _param1;
    int upperBound1 = numArray.GetUpperBound(0);
    int upperBound2 = numArray.GetUpperBound(1);
    for (int lowerBound1 = numArray.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
    {
      for (int lowerBound2 = numArray.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
      {
        int num2 = numArray[lowerBound1, lowerBound2];
        num1 += num2;
      }
    }
    return _param0.\u0023\u003DzMxv7fx65tV1yUOcJ1g\u003D\u003D(_param1, num1, 0);
  }

  internal static unsafe WriteableBitmap \u0023\u003DzMxv7fx65tV1yUOcJ1g\u003D\u003D(
    this WriteableBitmap _param0,
    int[,] _param1,
    int _param2,
    int _param3)
  {
    int num1 = _param1.GetUpperBound(0) + 1;
    int num2 = _param1.GetUpperBound(1) + 1;
    if ((num2 & 1) == 0)
      throw new InvalidOperationException("");
    if ((num1 & 1) == 0)
      throw new InvalidOperationException("");
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    WriteableBitmap writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(pixelWidth, pixelHeight);
    using (BitmapContext bitmapContext1 = _param0.\u0023\u003DzjnjmjBtrwZM5((\u0023\u003DzJZzdBuNdGPIV6c3AUcyRfoP2Syvy\u0024BgFeX2EQXJ3KFLxo_iOGyvT1XIHK5BB) 0))
    {
      using (BitmapContext bitmapContext2 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
      {
        int* numPtr1 = bitmapContext1.\u0023\u003DzSKG\u0024_qBsOJZc();
        int* numPtr2 = bitmapContext2.\u0023\u003DzSKG\u0024_qBsOJZc();
        int num3 = 0;
        int num4 = num2 >> 1;
        int num5 = num1 >> 1;
        for (int index1 = 0; index1 < pixelHeight; ++index1)
        {
          for (int index2 = 0; index2 < pixelWidth; ++index2)
          {
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            for (int index3 = -num4; index3 <= num4; ++index3)
            {
              int num10 = index3 + index2;
              if (num10 < 0)
                num10 = 0;
              else if (num10 >= pixelWidth)
                num10 = pixelWidth - 1;
              for (int index4 = -num5; index4 <= num5; ++index4)
              {
                int num11 = index4 + index1;
                if (num11 < 0)
                  num11 = 0;
                else if (num11 >= pixelHeight)
                  num11 = pixelHeight - 1;
                int num12 = numPtr1[num11 * pixelWidth + num10];
                int num13 = _param1[index4 + num4, index3 + num5];
                num6 += (num12 >> 24 & (int) byte.MaxValue) * num13;
                num7 += (num12 >> 16 /*0x10*/ & (int) byte.MaxValue) * num13;
                num8 += (num12 >> 8 & (int) byte.MaxValue) * num13;
                num9 += (num12 & (int) byte.MaxValue) * num13;
              }
            }
            int num14 = num6 / _param2 + _param3;
            int num15 = num7 / _param2 + _param3;
            int num16 = num8 / _param2 + _param3;
            int num17 = num9 / _param2 + _param3;
            byte num18 = num14 > (int) byte.MaxValue ? byte.MaxValue : (num14 < 0 ? (byte) 0 : (byte) num14);
            byte num19 = num15 > (int) byte.MaxValue ? byte.MaxValue : (num15 < 0 ? (byte) 0 : (byte) num15);
            byte num20 = num16 > (int) byte.MaxValue ? byte.MaxValue : (num16 < 0 ? (byte) 0 : (byte) num16);
            byte num21 = num17 > (int) byte.MaxValue ? byte.MaxValue : (num17 < 0 ? (byte) 0 : (byte) num17);
            numPtr2[num3++] = (int) num18 << 24 | (int) num19 << 16 /*0x10*/ | (int) num20 << 8 | (int) num21;
          }
        }
        return writeableBitmap;
      }
    }
  }

  internal static unsafe WriteableBitmap \u0023\u003DzkRsyJTI\u003D(this WriteableBitmap _param0)
  {
    WriteableBitmap writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(_param0.PixelWidth, _param0.PixelHeight);
    using (BitmapContext bitmapContext1 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
    {
      using (BitmapContext bitmapContext2 = _param0.\u0023\u003DzjnjmjBtrwZM5())
      {
        int* numPtr1 = bitmapContext1.\u0023\u003DzSKG\u0024_qBsOJZc();
        int* numPtr2 = bitmapContext2.\u0023\u003DzSKG\u0024_qBsOJZc();
        int num1 = bitmapContext2.\u0023\u003DzxhbmvAVxpXvh();
        for (int index = 0; index < num1; ++index)
        {
          int num2 = numPtr2[index];
          int num3 = num2 >> 24 & (int) byte.MaxValue;
          int num4 = num2 >> 16 /*0x10*/ & (int) byte.MaxValue;
          int num5 = num2 >> 8 & (int) byte.MaxValue;
          int num6 = num2 & (int) byte.MaxValue;
          int num7 = (int) byte.MaxValue - num4;
          int num8 = (int) byte.MaxValue - num5;
          int num9 = (int) byte.MaxValue - num6;
          numPtr1[index] = num3 << 24 | num7 << 16 /*0x10*/ | num8 << 8 | num9;
        }
        return writeableBitmap;
      }
    }
  }

  internal static void \u0023\u003DzzWEiifkWFV4ENgL9xQ\u003D\u003D(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    BitmapContext _param7)
  {
    if (_param4 < 0 && _param6 < 0 || _param4 > _param2 && _param6 > _param2 || _param3 == _param5 && _param4 == _param6)
      return;
    int pixelWidth = _param7.\u0023\u003DzZin35e8ltnFe().PixelWidth;
    int num1 = pixelWidth / 2;
    Rect rect1 = new Rect(0.0, 0.0, (double) pixelWidth, (double) pixelWidth);
    int num2 = _param5 - _param3;
    int num3 = _param6 - _param4;
    int num4 = 0;
    if (num2 < 0)
    {
      num2 = -num2;
      num4 = -1;
    }
    else if (num2 > 0)
      num4 = 1;
    int num5 = 0;
    if (num3 < 0)
    {
      num3 = -num3;
      num5 = -1;
    }
    else if (num3 > 0)
      num5 = 1;
    int num6;
    int num7;
    int num8;
    int num9;
    int num10;
    int num11;
    if (num2 > num3)
    {
      num6 = num4;
      num7 = 0;
      num8 = num4;
      num9 = num5;
      num10 = num3;
      num11 = num2;
    }
    else
    {
      num6 = 0;
      num7 = num5;
      num8 = num4;
      num9 = num5;
      num10 = num2;
      num11 = num3;
    }
    int num12 = _param3;
    int num13 = _param4;
    int num14 = num11 >> 1;
    Rect rect2 = new Rect((double) (num12 - num1), (double) (num13 - num1), (double) pixelWidth, (double) pixelWidth);
    if (num13 < _param2 && num13 >= 0 && num12 < _param1 && num12 >= 0)
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzSyuZtKbRCpVU(_param0, _param1, _param2, rect2, _param7, rect1, pixelWidth);
    for (int index = 0; index < num11; ++index)
    {
      num14 -= num10;
      if (num14 < 0)
      {
        num14 += num11;
        num12 += num8;
        num13 += num9;
      }
      else
      {
        num12 += num6;
        num13 += num7;
      }
      if (num13 < _param2 && num13 >= 0 && num12 < _param1 && num12 >= 0)
      {
        rect2.X = (double) (num12 - num1);
        rect2.Y = (double) (num13 - num1);
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzSyuZtKbRCpVU(_param0, _param1, _param2, rect2, _param7, rect1, pixelWidth);
      }
    }
  }

  private static byte \u0023\u003DztJfm\u0024Zotcpox(Rect _param0, double _param1, double _param2)
  {
    byte num = 0;
    if (_param1 < _param0.Left)
      num |= (byte) 1;
    else if (_param1 > _param0.Right)
      num |= (byte) 2;
    if (_param2 > _param0.Bottom)
      num |= (byte) 4;
    else if (_param2 < _param0.Top)
      num |= (byte) 8;
    return num;
  }

  internal static bool \u0023\u003DzQp4pqbouH0zcIVdt44meiLSWy5T3TxEdb52woaw\u003D(
    Rect _param0,
    ref float _param1,
    ref float _param2,
    ref float _param3,
    ref float _param4,
    int _param5)
  {
    return \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(new Rect(_param0.X - (double) _param5, _param0.Y - (double) _param5, _param0.Width + (double) (2 * _param5), _param0.Height + (double) (2 * _param5)), ref _param1, ref _param2, ref _param3, ref _param4);
  }

  internal static bool \u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(
    Rect _param0,
    ref float _param1,
    ref float _param2,
    ref float _param3,
    ref float _param4)
  {
    double num1 = (double) _param1.\u0023\u003DzcYUW_6FX9t5L();
    double num2 = (double) _param2.\u0023\u003DzcYUW_6FX9t5L();
    double num3 = (double) _param3.\u0023\u003DzcYUW_6FX9t5L();
    double num4 = (double) _param4.\u0023\u003DzcYUW_6FX9t5L();
    int num5 = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(_param0, ref num1, ref num2, ref num3, ref num4) ? 1 : 0;
    _param1 = (float) num1;
    _param2 = (float) num2;
    _param3 = (float) num3;
    _param4 = (float) num4;
    return num5 != 0;
  }

  internal static bool \u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(
    Rect _param0,
    ref int _param1,
    ref int _param2,
    ref int _param3,
    ref int _param4)
  {
    double num1 = (double) _param1;
    double num2 = (double) _param2;
    double num3 = (double) _param3;
    double num4 = (double) _param4;
    int num5 = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(_param0, ref num1, ref num2, ref num3, ref num4) ? 1 : 0;
    _param1 = (int) num1;
    _param2 = (int) num2;
    _param3 = (int) num3;
    _param4 = (int) num4;
    return num5 != 0;
  }

  internal static bool \u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(
    Rect _param0,
    ref double _param1,
    ref double _param2,
    ref double _param3,
    ref double _param4)
  {
    byte num1 = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DztJfm\u0024Zotcpox(_param0, _param1, _param2);
    byte num2 = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DztJfm\u0024Zotcpox(_param0, _param3, _param4);
    if (num1 == (byte) 0 && num2 == (byte) 0)
      return true;
    bool flag = false;
    while (((int) num1 | (int) num2) != 0)
    {
      if (((int) num1 & (int) num2) == 0)
      {
        double num3 = _param3;
        double num4 = _param4;
        byte num5 = num1 != (byte) 0 ? num1 : num2;
        if (((int) num5 & 8) != 0)
        {
          if (!double.IsInfinity(_param2))
            num3 = _param1 + (_param3 - _param1) * (_param0.Top - _param2) / (_param4 - _param2);
          num4 = _param0.Top;
        }
        else if (((int) num5 & 4) != 0)
        {
          if (!double.IsInfinity(_param2))
            num3 = _param1 + (_param3 - _param1) * (_param0.Bottom - _param2) / (_param4 - _param2);
          num4 = _param0.Bottom;
        }
        else if (((int) num5 & 2) != 0)
        {
          if (!double.IsInfinity(_param1))
            num4 = _param2 + (_param4 - _param2) * (_param0.Right - _param1) / (_param3 - _param1);
          num3 = _param0.Right;
        }
        else if (((int) num5 & 1) != 0)
        {
          if (!double.IsInfinity(_param1))
            num4 = _param2 + (_param4 - _param2) * (_param0.Left - _param1) / (_param3 - _param1);
          num3 = _param0.Left;
        }
        else
        {
          num3 = double.NaN;
          num4 = double.NaN;
        }
        if ((int) num5 == (int) num1)
        {
          _param1 = num3;
          _param2 = num4;
          num1 = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DztJfm\u0024Zotcpox(_param0, _param1, _param2);
        }
        else
        {
          _param3 = num3;
          _param4 = num4;
          num2 = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DztJfm\u0024Zotcpox(_param0, _param3, _param4);
        }
      }
      else
        goto label_26;
    }
    flag = true;
label_26:
    return flag;
  }

  public static int \u0023\u003Dz11eJ_TEyXgoH(
    int _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    int num1 = _param4 >> 24 & (int) byte.MaxValue;
    int num2 = _param4 >> 16 /*0x10*/ & (int) byte.MaxValue;
    int num3 = _param4 >> 8 & (int) byte.MaxValue;
    int num4 = _param4 & (int) byte.MaxValue;
    _param4 = _param0 + (num1 * ((int) byte.MaxValue - _param0) * 32897 >> 23) << 24 | _param1 + (num2 * ((int) byte.MaxValue - _param0) * 32897 >> 23) << 16 /*0x10*/ | _param2 + (num3 * ((int) byte.MaxValue - _param0) * 32897 >> 23) << 8 | _param3 + (num4 * ((int) byte.MaxValue - _param0) * 32897 >> 23);
    return _param4;
  }

  public static unsafe void \u0023\u003Dz69V8iivg9Z1c(
    BitmapContext _param0,
    int _param1,
    int _param2,
    short _param3,
    short _param4,
    short _param5,
    short _param6,
    int _param7,
    int _param8,
    int _param9,
    int _param10)
  {
    int* numPtr = _param0.\u0023\u003DzSKG\u0024_qBsOJZc();
    if ((int) _param4 > (int) _param6)
    {
      int num1 = (int) _param4;
      _param4 = _param6;
      _param6 = (short) num1;
      int num2 = (int) _param3;
      _param3 = _param5;
      _param5 = (short) num2;
    }
    numPtr[(int) _param4 * _param1 + (int) _param3] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8, _param9, _param10, numPtr[(int) _param4 * _param1 + (int) _param3]);
    short num3 = (short) ((int) _param5 - (int) _param3);
    short num4;
    if (num3 >= (short) 0)
    {
      num4 = (short) 1;
    }
    else
    {
      num4 = (short) -1;
      num3 = -num3;
    }
    short num5 = (short) ((int) _param6 - (int) _param4);
    if (num5 == (short) 0)
    {
      while (num3-- != (short) 0)
      {
        _param3 += num4;
        numPtr[(int) _param4 * _param1 + (int) _param3] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8, _param9, _param10, numPtr[(int) _param4 * _param1 + (int) _param3]);
      }
    }
    else if (num3 == (short) 0)
    {
      do
      {
        ++_param4;
        numPtr[(int) _param4 * _param1 + (int) _param3] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8, _param9, _param10, numPtr[(int) _param4 * _param1 + (int) _param3]);
      }
      while (--num5 != (short) 0);
    }
    else if ((int) num3 == (int) num5)
    {
      do
      {
        _param3 += num4;
        ++_param4;
        numPtr[(int) _param4 * _param1 + (int) _param3] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8, _param9, _param10, numPtr[(int) _param4 * _param1 + (int) _param3]);
      }
      while (--num5 != (short) 0);
    }
    else
    {
      ushort num6 = 0;
      if ((int) num5 > (int) num3)
      {
        ushort num7 = (ushort) (((ulong) num3 << 16 /*0x10*/) / (ulong) num5);
        while (--num5 != (short) 0)
        {
          ushort num8 = num6;
          num6 += num7;
          if ((int) num6 <= (int) num8)
            _param3 += num4;
          ++_param4;
          int num9 = (int) (ushort) ((uint) num6 >> 8);
          int num10 = num9 ^ (int) byte.MaxValue;
          numPtr[(int) _param4 * _param1 + (int) _param3] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8 * num10 >> 8, _param9 * num10 >> 8, _param10 * num10 >> 8, numPtr[(int) _param4 * _param1 + (int) _param3]);
          int num11 = num9;
          numPtr[(int) _param4 * _param1 + (int) _param3 + (int) num4] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8 * num11 >> 8, _param9 * num11 >> 8, _param10 * num11 >> 8, numPtr[(int) _param4 * _param1 + (int) _param3 + (int) num4]);
        }
        numPtr[(int) _param6 * _param1 + (int) _param5] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8, _param9, _param10, numPtr[(int) _param6 * _param1 + (int) _param5]);
      }
      else
      {
        ushort num12 = (ushort) (((ulong) num5 << 16 /*0x10*/) / (ulong) num3);
        while (--num3 != (short) 0)
        {
          ushort num13 = num6;
          num6 += num12;
          if ((int) num6 <= (int) num13)
            ++_param4;
          _param3 += num4;
          int num14 = (int) (ushort) ((uint) num6 >> 8);
          int num15 = num14 ^ (int) byte.MaxValue;
          numPtr[(int) _param4 * _param1 + (int) _param3] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8 * num15 >> 8, _param9 * num15 >> 8, _param10 * num15 >> 8, numPtr[(int) _param4 * _param1 + (int) _param3]);
          int num16 = num14;
          numPtr[((int) _param4 + 1) * _param1 + (int) _param3] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8 * num16 >> 8, _param9 * num16 >> 8, _param10 * num16 >> 8, numPtr[((int) _param4 + 1) * _param1 + (int) _param3]);
        }
        numPtr[(int) _param6 * _param1 + (int) _param5] = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz11eJ_TEyXgoH(_param7, _param8, _param9, _param10, numPtr[(int) _param6 * _param1 + (int) _param5]);
      }
    }
  }

  internal static void \u0023\u003Dz5EjP9MXm9qR7g3KHo8lWFwg\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003Dz5EjP9MXm9qR7g3KHo8lWFwg\u003D(_param1, _param2, _param3, _param4, num2);
  }

  internal static void \u0023\u003Dz5EjP9MXm9qR7g3KHo8lWFwg\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int pixelWidth = _param0.PixelWidth;
      int pixelHeight = _param0.PixelHeight;
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz5EjP9MXm9qR7g3KHo8lWFwg\u003D(bitmapContext, pixelWidth, pixelHeight, _param1, _param2, _param3, _param4, _param5);
    }
  }

  internal static unsafe void \u0023\u003Dz5EjP9MXm9qR7g3KHo8lWFwg\u003D(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7)
  {
    if (_param4 < 0 && _param6 < 0 || _param4 > _param2 && _param6 > _param2)
      return;
    if (_param3 == _param5 && _param4 == _param6)
    {
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzVUQzlzp_K4Dd(_param0, _param1, _param2, _param3, _param4, _param7);
    }
    else
    {
      int* numPtr = _param0.\u0023\u003DzSKG\u0024_qBsOJZc();
      int num1 = _param5 - _param3;
      int num2 = _param6 - _param4;
      int num3 = 0;
      if (num1 < 0)
      {
        num1 = -num1;
        num3 = -1;
      }
      else if (num1 > 0)
        num3 = 1;
      int num4 = 0;
      if (num2 < 0)
      {
        num2 = -num2;
        num4 = -1;
      }
      else if (num2 > 0)
        num4 = 1;
      int num5;
      int num6;
      int num7;
      int num8;
      int num9;
      int num10;
      if (num1 > num2)
      {
        num5 = num3;
        num6 = 0;
        num7 = num3;
        num8 = num4;
        num9 = num2;
        num10 = num1;
      }
      else
      {
        num5 = 0;
        num6 = num4;
        num7 = num3;
        num8 = num4;
        num9 = num1;
        num10 = num2;
      }
      int num11 = _param3;
      int num12 = _param4;
      int num13 = num10 >> 1;
      if (num12 < _param2 && num12 >= 0 && num11 < _param1 && num11 >= 0)
        numPtr[num12 * _param1 + num11] = _param7;
      for (int index = 0; index < num10; ++index)
      {
        num13 -= num9;
        if (num13 < 0)
        {
          num13 += num10;
          num11 += num7;
          num12 += num8;
        }
        else
        {
          num11 += num5;
          num12 += num6;
        }
        if (num12 < _param2 && num12 >= 0 && num11 < _param1 && num11 >= 0)
          numPtr[num12 * _param1 + num11] = _param7;
      }
    }
  }

  internal static unsafe void \u0023\u003DzVUQzlzp_K4Dd(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    if (_param4 >= _param2 || _param4 < 0 || _param3 >= _param1 || _param3 < 0)
      return;
    _param0.\u0023\u003DzSKG\u0024_qBsOJZc()[_param4 * _param1 + _param3] = _param5;
  }

  internal static void \u0023\u003Dz802y6ee\u00246psQ(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003Dz802y6ee\u00246psQ(_param1, _param2, _param3, _param4, num2);
  }

  internal static unsafe void \u0023\u003Dz802y6ee\u00246psQ(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      int num1 = _param3 - _param1;
      int num2 = _param4 - _param2;
      int num3 = num2 >= 0 ? num2 : -num2;
      int num4 = num1 >= 0 ? num1 : -num1;
      if (num4 > num3)
        num3 = num4;
      if (num3 == 0)
        return;
      float num5 = (float) num1 / (float) num3;
      float num6 = (float) num2 / (float) num3;
      float num7 = (float) _param1;
      float num8 = (float) _param2;
      for (int index = 0; index < num3; ++index)
      {
        if ((double) num8 < (double) pixelHeight && (double) num8 >= 0.0 && (double) num7 < (double) pixelWidth && (double) num7 >= 0.0)
          numPtr[(int) num8 * pixelWidth + (int) num7] = _param5;
        num7 += num5;
        num8 += num6;
      }
    }
  }

  internal static void \u0023\u003Dzk8_eoWQ\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003Dzk8_eoWQ\u003D(_param1, _param2, _param3, _param4, num2);
  }

  internal static void \u0023\u003Dzk8_eoWQ\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, _param0.PixelWidth, _param0.PixelHeight, _param1, _param2, _param3, _param4, _param5);
  }

  internal static unsafe void \u0023\u003Dzk8_eoWQ\u003D(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7)
  {
    int* numPtr = _param0.\u0023\u003DzSKG\u0024_qBsOJZc();
    int num1 = _param5 - _param3;
    int num2 = _param6 - _param4;
    int num3 = num2 < 0 ? -num2 : num2;
    if ((num1 < 0 ? -num1 : num1) > num3)
    {
      if (num1 < 0)
      {
        int num4 = _param3;
        _param3 = _param5;
        _param5 = num4;
        int num5 = _param4;
        _param4 = _param6;
        _param6 = num5;
      }
      int num6 = (num2 << 8) / num1;
      int num7 = _param4 << 8;
      int num8 = _param6 << 8;
      int num9 = _param2 << 8;
      if (_param4 < _param6)
      {
        if (_param4 >= _param2 || _param6 < 0)
          return;
        if (num7 < 0)
        {
          if (num6 == 0)
            return;
          int num10 = num7;
          num7 = num6 - 1 + (num7 + 1) % num6;
          _param3 += (num7 - num10) / num6;
        }
        if (num8 >= num9 && num6 != 0)
        {
          int num11 = num9 - 1 - (num9 - 1 - num7) % num6;
          _param5 = _param3 + (num11 - num7) / num6;
        }
      }
      else
      {
        if (_param6 >= _param2 || _param4 < 0)
          return;
        if (num7 >= num9)
        {
          if (num6 == 0)
            return;
          int num12 = num7;
          num7 = num9 - 1 + (num6 - (num9 - 1 - num12) % num6);
          _param3 += (num7 - num12) / num6;
        }
        if (num8 < 0 && num6 != 0)
        {
          int num13 = num7 % num6;
          _param5 = _param3 + (num13 - num7) / num6;
        }
      }
      if (_param3 < 0)
      {
        num7 -= num6 * _param3;
        _param3 = 0;
      }
      if (_param5 >= _param1)
        _param5 = _param1 - 1;
      int num14 = num7;
      int num15 = num14 >> 8;
      int num16 = num15;
      int index1 = _param3 + num15 * _param1;
      int num17 = num6 < 0 ? 1 - _param1 : 1 + _param1;
      for (int index2 = _param3; index2 <= _param5; ++index2)
      {
        numPtr[index1] = _param7;
        num14 += num6;
        int num18 = num14 >> 8;
        if (num18 != num16)
        {
          num16 = num18;
          index1 += num17;
        }
        else
          ++index1;
      }
    }
    else
    {
      if (num3 == 0)
        return;
      if (num2 < 0)
      {
        int num19 = _param3;
        _param3 = _param5;
        _param5 = num19;
        int num20 = _param4;
        _param4 = _param6;
        _param6 = num20;
      }
      int num21 = _param3 << 8;
      int num22 = _param5 << 8;
      int num23 = _param1 << 8;
      int num24 = (num1 << 8) / num2;
      if (_param3 < _param5)
      {
        if (_param3 >= _param1 || _param5 < 0)
          return;
        if (num21 < 0)
        {
          if (num24 == 0)
            return;
          int num25 = num21;
          num21 = num24 - 1 + (num21 + 1) % num24;
          _param4 += (num21 - num25) / num24;
        }
        if (num22 >= num23 && num24 != 0)
        {
          int num26 = num23 - 1 - (num23 - 1 - num21) % num24;
          _param6 = _param4 + (num26 - num21) / num24;
        }
      }
      else
      {
        if (_param5 >= _param1 || _param3 < 0)
          return;
        if (num21 >= num23)
        {
          if (num24 == 0)
            return;
          int num27 = num21;
          num21 = num23 - 1 + (num24 - (num23 - 1 - num27) % num24);
          _param4 += (num21 - num27) / num24;
        }
        if (num22 < 0 && num24 != 0)
        {
          int num28 = num21 % num24;
          _param6 = _param4 + (num28 - num21) / num24;
        }
      }
      if (_param4 < 0)
      {
        num21 -= num24 * _param4;
        _param4 = 0;
      }
      if (_param6 >= _param2)
        _param6 = _param2 - 1;
      int num29 = num21 + (_param4 * _param1 << 8);
      int num30 = (_param1 << 8) + num24;
      for (int index = _param4; index <= _param6; ++index)
      {
        numPtr[num29 >> 8] = _param7;
        num29 += num30;
      }
    }
  }

  internal static void \u0023\u003DztpcxZUcHpycq(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003DztpcxZUcHpycq(_param1, _param2, _param3, _param4, num2);
  }

  internal static void \u0023\u003DztpcxZUcHpycq(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DztpcxZUcHpycq(bitmapContext, _param0.PixelWidth, _param0.PixelHeight, _param1, _param2, _param3, _param4, _param5, false);
  }

  internal static unsafe void \u0023\u003DztpcxZUcHpycq(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    bool _param8)
  {
    if (!\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzNk8cBLuXAKp15HT5R2aU3iOBWzr8(new Rect(0.0, 0.0, (double) _param1, (double) _param2), ref _param3, ref _param4, ref _param5, ref _param6))
      return;
    int num1 = _param1 - 1;
    int num2 = _param2 - 1;
    if (_param3 < 0)
      _param3 = 0;
    else if (_param3 > num1)
      _param3 = num1;
    if (_param4 < 0)
      _param4 = 0;
    else if (_param4 > num2)
      _param4 = num2;
    if (_param5 < 0)
      _param5 = 0;
    if (_param5 > num1)
      _param5 = num1;
    if (_param6 < 0)
      _param6 = 0;
    if (_param6 > num2)
      _param6 = num2;
    int num3 = _param1 * _param2;
    int num4 = _param4 * _param1 + _param3;
    int num5 = _param5 - _param3;
    int num6 = _param6 - _param4;
    int num7 = _param7 >> 24 & (int) byte.MaxValue;
    uint num8 = (uint) (_param7 & 16711935);
    uint num9 = (uint) (_param7 >> 8 & (int) byte.MaxValue);
    int num10 = num5;
    int num11 = num6;
    if (num5 < 0)
      num10 = -num5;
    if (num6 < 0)
      num11 = -num6;
    int num12;
    int num13;
    int num14;
    int num15;
    int num16;
    int num17;
    int num18;
    if (num10 > num11)
    {
      num12 = num10;
      num13 = num11;
      num14 = _param5;
      num15 = _param6;
      num16 = 1;
      num18 = num17 = _param1;
      if (num5 < 0)
        num16 = -num16;
      if (num6 < 0)
        num18 = -num18;
    }
    else
    {
      num12 = num11;
      num13 = num10;
      num14 = _param6;
      num15 = _param5;
      num16 = num17 = _param1;
      num18 = 1;
      if (num6 < 0)
        num16 = -num16;
      if (num5 < 0)
        num18 = -num18;
    }
    int num19 = num14 + num12;
    int num20 = (num13 << 1) - num12;
    int num21 = num13 << 1;
    int num22 = num13 - num12 << 1;
    double num23 = 1.0 / (4.0 * Math.Sqrt((double) (num12 * num12 + num13 * num13)));
    double num24 = 0.75 - 2.0 * ((double) num12 * num23);
    int num25 = (int) (num23 * 1024.0);
    int num26 = (int) (num24 * 1024.0 * (double) num7);
    int num27 = (int) (768.0 * (double) num7);
    int num28 = num25 * num7;
    int num29 = num12 * num28;
    int num30 = num20 * num28;
    int num31 = 0;
    int num32 = num21 * num28;
    int num33 = num22 * num28;
    int* numPtr = _param0.\u0023\u003DzSKG\u0024_qBsOJZc();
    bool flag = true;
    do
    {
      if (!flag || !_param8)
      {
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzqa48lAsFPlt487ecE9ARzPzzxdrORx\u0024VZA\u003D\u003D(numPtr, num4, num27 - num31 >> 10, num8, num9);
        int num34 = num4 + num18;
        if (num34 < num3 && (flag && num17 == num18 || num34 % num17 > 0))
          \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzqa48lAsFPlt487ecE9ARzPzzxdrORx\u0024VZA\u003D\u003D(numPtr, num34, num26 + num31 >> 10, num8, num9);
        int num35 = num4 - num18;
        if (num35 >= 0 && num35 < num3 && (flag && num17 == num18 || num4 % num17 > 0))
          \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzqa48lAsFPlt487ecE9ARzPzzxdrORx\u0024VZA\u003D\u003D(numPtr, num35, num26 - num31 >> 10, num8, num9);
      }
      if (num20 < 0)
      {
        num31 = num30 + num29;
        num20 += num21;
        num30 += num32;
      }
      else
      {
        num31 = num30 - num29;
        num20 += num22;
        num30 += num33;
        ++num15;
        num4 += num18;
      }
      ++num14;
      num4 += num16;
      flag = false;
    }
    while (num14 <= num19);
  }

  private static unsafe void \u0023\u003Dzqa48lAsFPlt487ecE9ARzPzzxdrORx\u0024VZA\u003D\u003D(
    int* _param0,
    int _param1,
    int _param2,
    uint _param3,
    uint _param4)
  {
    int num1 = _param0[_param1];
    uint num2 = (uint) (num1 >>> 24);
    uint num3 = (uint) (num1 >>> 8 & (int) byte.MaxValue);
    uint num4 = (uint) (num1 & 16711935);
    _param0[_param1] = (int) ((long) _param2 + ((long) num2 * (long) ((int) byte.MaxValue - _param2) * 32897L >> 23) << 24 | (long) (_param4 - num3) * (long) _param2 + (long) (num3 << 8) & 4294967040L | ((long) (_param3 - num4) * (long) _param2 >> 8) + (long) num4 & 16711935L);
  }

  internal static void \u0023\u003DztpSn45GESJ8upYzdDQ\u003D\u003D(
    this WriteableBitmap _param0,
    int[] _param1,
    Color _param2)
  {
    int num1 = (int) _param2.A + 1;
    int num2 = (int) _param2.A << 24 | (int) (byte) ((int) _param2.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param2.G * num1 >> 8) << 8 | (int) (byte) ((int) _param2.B * num1 >> 8);
    _param0.\u0023\u003DztpSn45GESJ8upYzdDQ\u003D\u003D(_param1, num2);
  }

  internal static void \u0023\u003DztpSn45GESJ8upYzdDQ\u003D\u003D(
    this WriteableBitmap _param0,
    int[] _param1,
    int _param2)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int num1 = _param1[0];
      int num2 = _param1[1];
      if (num1 < 0)
        num1 = 0;
      if (num2 < 0)
        num2 = 0;
      if (num1 > pixelWidth)
        num1 = pixelWidth;
      if (num2 > pixelHeight)
        num2 = pixelHeight;
      for (int index = 2; index < _param1.Length; index += 2)
      {
        int num3 = _param1[index];
        int num4 = _param1[index + 1];
        if (num3 < 0)
          num3 = 0;
        if (num4 < 0)
          num4 = 0;
        if (num3 > pixelWidth)
          num3 = pixelWidth;
        if (num4 > pixelHeight)
          num4 = pixelHeight;
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, num1, num2, num3, num4, _param2);
        num1 = num3;
        num2 = num4;
      }
    }
  }

  internal static void \u0023\u003DzTNCC1vWt\u0024o04(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    Color _param7)
  {
    int num1 = (int) _param7.A + 1;
    int num2 = (int) _param7.A << 24 | (int) (byte) ((int) _param7.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param7.G * num1 >> 8) << 8 | (int) (byte) ((int) _param7.B * num1 >> 8);
    _param0.\u0023\u003DzTNCC1vWt\u0024o04(_param1, _param2, _param3, _param4, _param5, _param6, num2);
  }

  internal static void \u0023\u003DzTNCC1vWt\u0024o04(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, _param1, _param2, _param3, _param4, _param7);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, _param3, _param4, _param5, _param6, _param7);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, _param5, _param6, _param1, _param2, _param7);
    }
  }

  internal static void \u0023\u003Dz7zUbWtTKc3tA(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8,
    Color _param9)
  {
    int num1 = (int) _param9.A + 1;
    int num2 = (int) _param9.A << 24 | (int) (byte) ((int) _param9.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param9.G * num1 >> 8) << 8 | (int) (byte) ((int) _param9.B * num1 >> 8);
    _param0.\u0023\u003Dz7zUbWtTKc3tA(_param1, _param2, _param3, _param4, _param5, _param6, _param7, _param8, num2);
  }

  internal static void \u0023\u003Dz7zUbWtTKc3tA(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8,
    int _param9)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, _param1, _param2, _param3, _param4, _param9);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, _param3, _param4, _param5, _param6, _param9);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, _param5, _param6, _param7, _param8, _param9);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, _param7, _param8, _param1, _param2, _param9);
    }
  }

  internal static void \u0023\u003DzoetbnPCAjix1(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003DzoetbnPCAjix1(_param1, _param2, _param3, _param4, num2);
  }

  internal static unsafe void \u0023\u003DzoetbnPCAjix1(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      if (_param1 < 0 && _param3 < 0 || _param2 < 0 && _param4 < 0 || _param1 >= pixelWidth && _param3 >= pixelWidth || _param2 >= pixelHeight && _param4 >= pixelHeight)
        return;
      if (_param1 < 0)
        _param1 = 0;
      if (_param2 < 0)
        _param2 = 0;
      if (_param3 < 0)
        _param3 = 0;
      if (_param4 < 0)
        _param4 = 0;
      if (_param1 > pixelWidth)
        _param1 = pixelWidth;
      if (_param2 > pixelHeight)
        _param2 = pixelHeight;
      if (_param3 > pixelWidth)
        _param3 = pixelWidth;
      if (_param4 > pixelHeight)
        _param4 = pixelHeight;
      int num1 = _param2 * pixelWidth;
      int index1 = _param4 * pixelWidth - pixelWidth + _param1;
      int num2 = num1 + _param3;
      int num3 = num1 + _param1;
      for (int index2 = num3; index2 < num2; ++index2)
      {
        numPtr[index2] = _param5;
        numPtr[index1] = _param5;
        ++index1;
      }
      int index3 = num3 + pixelWidth;
      int num4 = index1 - pixelWidth;
      for (int index4 = num1 + _param3 - 1 + pixelWidth; index4 < num4; index4 += pixelWidth)
      {
        numPtr[index4] = _param5;
        numPtr[index3] = _param5;
        index3 += pixelWidth;
      }
    }
  }

  internal static void \u0023\u003DzIZCdW2WR6Rxw(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6)
  {
    int num1 = _param3 - _param1 >> 1;
    int num2 = _param4 - _param2 >> 1;
    int num3 = _param1 + num1;
    int num4 = _param2 + num2;
    _param0.\u0023\u003Dzi0gT5tAUKCgY7FXpsS29Z3U\u003D(num3, num4, num1, num2, _param5, _param6);
  }

  internal static void \u0023\u003Dzi0gT5tAUKCgY7FXpsS29Z3U\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    Color _param5)
  {
    int num1 = (int) _param5.A + 1;
    int num2 = (int) _param5.A << 24 | (int) (byte) ((int) _param5.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param5.G * num1 >> 8) << 8 | (int) (byte) ((int) _param5.B * num1 >> 8);
    _param0.\u0023\u003Dzi0gT5tAUKCgY7FXpsS29Z3U\u003D(_param1, _param2, _param3, _param4, num2);
  }

  internal static unsafe void \u0023\u003Dzi0gT5tAUKCgY7FXpsS29Z3U\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr = bitmapContext.\u0023\u003DzSKG\u0024_qBsOJZc();
      int pixelWidth = _param0.PixelWidth;
      int pixelHeight = _param0.PixelHeight;
      if (_param3 < 1 || _param4 < 1)
        return;
      int num1 = _param3;
      int num2 = 0;
      int num3 = _param3 * _param3 << 1;
      int num4 = _param4 * _param4 << 1;
      int num5 = _param4 * _param4 * (1 - (_param3 << 1));
      int num6 = _param3 * _param3;
      int num7 = 0;
      int num8 = num4 * _param3;
      int num9 = 0;
      while (num8 >= num9)
      {
        int num10 = _param2 + num2;
        int num11 = _param2 - num2;
        if (num10 < 0)
          num10 = 0;
        if (num10 >= pixelHeight)
          num10 = pixelHeight - 1;
        if (num11 < 0)
          num11 = 0;
        if (num11 >= pixelHeight)
          num11 = pixelHeight - 1;
        int num12 = num10 * pixelWidth;
        int num13 = num11 * pixelWidth;
        int num14 = _param1 + num1;
        int num15 = _param1 - num1;
        if (num14 < 0)
          num14 = 0;
        if (num14 >= pixelWidth)
          num14 = pixelWidth - 1;
        if (num15 < 0)
          num15 = 0;
        if (num15 >= pixelWidth)
          num15 = pixelWidth - 1;
        numPtr[num14 + num12] = _param5;
        numPtr[num15 + num12] = _param5;
        numPtr[num15 + num13] = _param5;
        numPtr[num14 + num13] = _param5;
        ++num2;
        num9 += num3;
        num7 += num6;
        num6 += num3;
        if (num5 + (num7 << 1) > 0)
        {
          --num1;
          num8 -= num4;
          num7 += num5;
          num5 += num4;
        }
      }
      int num16 = 0;
      int num17 = _param4;
      int num18 = _param2 + num17;
      int num19 = _param2 - num17;
      if (num18 < 0)
        num18 = 0;
      if (num18 >= pixelHeight)
        num18 = pixelHeight - 1;
      if (num19 < 0)
        num19 = 0;
      if (num19 >= pixelHeight)
        num19 = pixelHeight - 1;
      int num20 = num18 * pixelWidth;
      int num21 = num19 * pixelWidth;
      int num22 = _param4 * _param4;
      int num23 = _param3 * _param3 * (1 - (_param4 << 1));
      int num24 = 0;
      int num25 = 0;
      int num26 = num3 * _param4;
      while (num25 <= num26)
      {
        int num27 = _param1 + num16;
        int num28 = _param1 - num16;
        if (num27 < 0)
          num27 = 0;
        if (num27 >= pixelWidth)
          num27 = pixelWidth - 1;
        if (num28 < 0)
          num28 = 0;
        if (num28 >= pixelWidth)
          num28 = pixelWidth - 1;
        numPtr[num27 + num20] = _param5;
        numPtr[num28 + num20] = _param5;
        numPtr[num28 + num21] = _param5;
        numPtr[num27 + num21] = _param5;
        ++num16;
        num25 += num4;
        num24 += num22;
        num22 += num4;
        if (num23 + (num24 << 1) > 0)
        {
          --num17;
          int num29 = _param2 + num17;
          int num30 = _param2 - num17;
          if (num29 < 0)
            num29 = 0;
          if (num29 >= pixelHeight)
            num29 = pixelHeight - 1;
          if (num30 < 0)
            num30 = 0;
          if (num30 >= pixelHeight)
            num30 = pixelHeight - 1;
          num20 = num29 * pixelWidth;
          num21 = num30 * pixelWidth;
          num26 -= num3;
          num24 += num23;
          num23 += num3;
        }
      }
    }
  }

  internal static void \u0023\u003Dzi0gT5tAUKCgY7FXpsS29Z3U\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzi0gT5tAUKCgY7FXpsS29Z3U\u003D(bitmapContext, _param1, _param2, _param3, _param4, _param5, _param6);
  }

  internal static unsafe void \u0023\u003Dzi0gT5tAUKCgY7FXpsS29Z3U\u003D(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6)
  {
    int num1 = _param6 >> 1;
    int num2 = _param6 - num1 - 1;
    int* numPtr = _param0.\u0023\u003DzSKG\u0024_qBsOJZc();
    int num3 = _param0.\u0023\u003DzOc4Ixb6AQPL8();
    int num4 = _param0.\u0023\u003DzeaNYBEDp1wgD();
    if (_param3 < 1 || _param4 < 1)
      return;
    int num5 = _param3;
    int num6 = 0;
    int num7 = _param3 * _param3 << 1;
    int num8 = _param4 * _param4 << 1;
    int num9 = _param4 * _param4 * (1 - (_param3 << 1));
    int num10 = _param3 * _param3;
    int num11 = 0;
    int num12 = num8 * _param3;
    int num13 = 0;
    int num14 = 0;
    while (num12 >= num13)
    {
      int num15 = _param2 + num6;
      int num16 = _param2 - num6;
      if (num15 < 0)
        num15 = 0;
      if (num15 >= num4)
        num15 = num4 - 1;
      if (num16 < 0)
        num16 = 0;
      if (num16 >= num4)
        num16 = num4 - 1;
      int num17 = num15 * num3;
      int num18 = num16 * num3;
      int num19 = _param1 + num5;
      int num20 = _param1 - num5;
      if (num19 < 0)
        num19 = 0;
      if (num19 >= num3)
        num19 = num3 - 1;
      if (num20 < 0)
        num20 = 0;
      if (num20 >= num3)
        num20 = num3 - 1;
      numPtr[num19 + num17] = _param5;
      numPtr[num20 + num17] = _param5;
      numPtr[num20 + num18] = _param5;
      numPtr[num19 + num18] = _param5;
      for (int index = 1; index <= num1; ++index)
      {
        if (num19 + index < num3)
        {
          numPtr[num19 + index + num17] = _param5;
          numPtr[num19 + index + num18] = _param5;
        }
        if (num20 - index >= 0)
        {
          numPtr[num20 - index + num17] = _param5;
          numPtr[num20 - index + num18] = _param5;
        }
      }
      for (int index = 1; index <= num2; ++index)
      {
        if (num19 - index < num3)
        {
          numPtr[num19 - index + num17] = _param5;
          numPtr[num19 - index + num18] = _param5;
        }
        if (num20 + index >= 0)
        {
          numPtr[num20 + index + num17] = _param5;
          numPtr[num20 + index + num18] = _param5;
        }
      }
      num14 = num19 - _param1;
      ++num6;
      num13 += num7;
      num11 += num10;
      num10 += num7;
      if (num9 + (num11 << 1) > 0)
      {
        --num5;
        num12 -= num8;
        num11 += num9;
        num9 += num8;
      }
    }
    int num21 = 0;
    int num22 = _param4;
    int num23 = _param2 + num22;
    int num24 = _param2 - num22;
    if (num23 < 0)
      num23 = 0;
    if (num23 >= num4)
      num23 = num4 - 1;
    if (num24 < 0)
      num24 = 0;
    if (num24 >= num4)
      num24 = num4 - 1;
    int num25 = num23 * num3;
    int num26 = num24 * num3;
    int num27 = _param4 * _param4;
    int num28 = _param3 * _param3 * (1 - (_param4 << 1));
    int num29 = 0;
    int num30 = 0;
    int num31 = num7 * _param4;
    while (num30 <= num31)
    {
      int num32 = _param1 + num21;
      int num33 = _param1 - num21;
      if (num32 < 0)
        num32 = 0;
      if (num32 >= num3)
        num32 = num3 - 1;
      if (num33 < 0)
        num33 = 0;
      if (num33 >= num3)
        num33 = num3 - 1;
      numPtr[num32 + num25] = _param5;
      numPtr[num33 + num25] = _param5;
      numPtr[num33 + num26] = _param5;
      numPtr[num32 + num26] = _param5;
      for (int index = 1; index <= num1; ++index)
      {
        if (num23 + index < num4)
        {
          numPtr[num32 + num25 + index * num3] = _param5;
          numPtr[num33 + num25 + index * num3] = _param5;
        }
        if (num24 - index >= 0)
        {
          numPtr[num33 + num26 - index * num3] = _param5;
          numPtr[num32 + num26 - index * num3] = _param5;
        }
      }
      for (int index = 1; index <= num2; ++index)
      {
        if (num23 - index >= 0)
        {
          numPtr[num32 + num25 - index * num3] = _param5;
          numPtr[num33 + num25 - index * num3] = _param5;
        }
        if (num24 + index < num4)
        {
          numPtr[num33 + num26 + index * num3] = _param5;
          numPtr[num32 + num26 + index * num3] = _param5;
        }
      }
      ++num21;
      num30 += num8;
      num29 += num27;
      num27 += num8;
      if (num28 + (num29 << 1) > 0)
      {
        --num22;
        num23 = _param2 + num22;
        num24 = _param2 - num22;
        if (num23 < 0)
          num23 = 0;
        if (num23 >= num4)
          num23 = num4 - 1;
        if (num24 < 0)
          num24 = 0;
        if (num24 >= num4)
          num24 = num4 - 1;
        num25 = num23 * num3;
        num26 = num24 * num3;
        num31 -= num7;
        num29 += num28;
        num28 += num7;
      }
    }
    for (int index1 = 1; index1 <= num1; ++index1)
    {
      for (int index2 = 1; index2 <= num1 - index1; ++index2)
      {
        int num34 = index1 + _param1 + num14;
        int num35 = _param2 - index2 - num14;
        if (num34 >= 0 && num34 < num3 && num35 >= 0 && num35 < num4)
          numPtr[num34 + num35 * num3] = _param5;
        int num36 = -index1 + _param1 - num14;
        int num37 = num35;
        if (num36 >= 0 && num36 < num3 && num37 >= 0 && num37 < num4)
          numPtr[num36 + num37 * num3] = _param5;
        int num38 = num36;
        int num39 = _param2 + index2 + num14;
        if (num38 >= 0 && num38 < num3 && num39 >= 0 && num39 < num4)
          numPtr[num38 + num39 * num3] = _param5;
        int num40 = num34;
        int num41 = num39;
        if (num40 >= 0 && num40 < num3 && num41 >= 0 && num41 < num4)
          numPtr[num40 + num41 * num3] = _param5;
      }
    }
  }

  internal static void \u0023\u003DzFtF5uXJldIW8(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8,
    Color _param9)
  {
    int num1 = (int) _param9.A + 1;
    int num2 = (int) _param9.A << 24 | (int) (byte) ((int) _param9.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param9.G * num1 >> 8) << 8 | (int) (byte) ((int) _param9.B * num1 >> 8);
    _param0.\u0023\u003DzFtF5uXJldIW8(_param1, _param2, _param3, _param4, _param5, _param6, _param7, _param8, num2);
  }

  internal static void \u0023\u003DzFtF5uXJldIW8(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8,
    int _param9)
  {
    int num1 = Math.Min(_param1, Math.Min(_param3, Math.Min(_param5, _param7)));
    int num2 = Math.Min(_param2, Math.Min(_param4, Math.Min(_param6, _param8)));
    int num3 = Math.Max(_param1, Math.Max(_param3, Math.Max(_param5, _param7)));
    int num4 = Math.Max(_param2, Math.Max(_param4, Math.Max(_param6, _param8)));
    int num5 = num3 - num1;
    int num6 = num2;
    int num7 = num4 - num6;
    if (num5 > num7)
      num7 = num5;
    if (num7 == 0)
      return;
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      float num8 = 2f / (float) num7;
      int num9 = _param1;
      int num10 = _param2;
      for (float num11 = num8; (double) num11 <= 1.0; num11 += num8)
      {
        float num12 = num11 * num11;
        float num13 = 1f - num11;
        float num14 = num13 * num13;
        int num15 = (int) ((double) num13 * (double) num14 * (double) _param1 + 3.0 * (double) num11 * (double) num14 * (double) _param3 + 3.0 * (double) num13 * (double) num12 * (double) _param5 + (double) num11 * (double) num12 * (double) _param7);
        int num16 = (int) ((double) num13 * (double) num14 * (double) _param2 + 3.0 * (double) num11 * (double) num14 * (double) _param4 + 3.0 * (double) num13 * (double) num12 * (double) _param6 + (double) num11 * (double) num12 * (double) _param8);
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, num9, num10, num15, num16, _param9);
        num9 = num15;
        num10 = num16;
      }
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(bitmapContext, pixelWidth, pixelHeight, num9, num10, _param7, _param8, _param9);
    }
  }

  internal static void \u0023\u003DzSlXpINdDaUagL2yPOQ\u003D\u003D(
    this WriteableBitmap _param0,
    int[] _param1,
    Color _param2)
  {
    int num1 = (int) _param2.A + 1;
    int num2 = (int) _param2.A << 24 | (int) (byte) ((int) _param2.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param2.G * num1 >> 8) << 8 | (int) (byte) ((int) _param2.B * num1 >> 8);
    _param0.\u0023\u003DzSlXpINdDaUagL2yPOQ\u003D\u003D(_param1, num2);
  }

  internal static void \u0023\u003DzSlXpINdDaUagL2yPOQ\u003D\u003D(
    this WriteableBitmap _param0,
    int[] _param1,
    int _param2)
  {
    int num1 = _param1[0];
    int num2 = _param1[1];
    for (int index = 2; index + 5 < _param1.Length; index += 6)
    {
      int num3 = _param1[index + 4];
      int num4 = _param1[index + 5];
      _param0.\u0023\u003DzFtF5uXJldIW8(num1, num2, _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], num3, num4, _param2);
      num1 = num3;
      num2 = num4;
    }
  }

  private static void \u0023\u003Dzocnp0n1g0lrV(
    int _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    float _param8,
    int _param9,
    BitmapContext _param10,
    int _param11,
    int _param12)
  {
    int num1 = Math.Min(_param0, Math.Min(_param2, Math.Min(_param4, _param6)));
    int num2 = Math.Min(_param1, Math.Min(_param3, Math.Min(_param5, _param7)));
    int num3 = Math.Max(_param0, Math.Max(_param2, Math.Max(_param4, _param6)));
    int num4 = Math.Max(_param1, Math.Max(_param3, Math.Max(_param5, _param7)));
    int num5 = num3 - num1;
    int num6 = num2;
    int num7 = num4 - num6;
    if (num5 > num7)
      num7 = num5;
    if (num7 == 0)
      return;
    float num8 = 2f / (float) num7;
    int num9 = _param2;
    int num10 = _param3;
    float num11 = _param8 * (float) (_param4 - _param0);
    float num12 = _param8 * (float) (_param5 - _param1);
    float num13 = _param8 * (float) (_param6 - _param2);
    float num14 = _param8 * (float) (_param7 - _param3);
    float num15 = num11 + num13 + (float) (2 * _param2) - (float) (2 * _param4);
    float num16 = num12 + num14 + (float) (2 * _param3) - (float) (2 * _param5);
    float num17 = -2f * num11 - num13 - (float) (3 * _param2) + (float) (3 * _param4);
    float num18 = -2f * num12 - num14 - (float) (3 * _param3) + (float) (3 * _param5);
    for (float num19 = num8; (double) num19 <= 1.0; num19 += num8)
    {
      float num20 = num19 * num19;
      int num21 = (int) ((double) num15 * (double) num20 * (double) num19 + (double) num17 * (double) num20 + (double) num11 * (double) num19 + (double) _param2);
      int num22 = (int) ((double) num16 * (double) num20 * (double) num19 + (double) num18 * (double) num20 + (double) num12 * (double) num19 + (double) _param3);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(_param10, _param11, _param12, num9, num10, num21, num22, _param9);
      num9 = num21;
      num10 = num22;
    }
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzk8_eoWQ\u003D(_param10, _param11, _param12, num9, num10, _param4, _param5, _param9);
  }

  internal static void \u0023\u003DzJwP1eeijr15N(
    this WriteableBitmap _param0,
    int[] _param1,
    float _param2,
    Color _param3)
  {
    int num1 = (int) _param3.A + 1;
    int num2 = (int) _param3.A << 24 | (int) (byte) ((int) _param3.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param3.G * num1 >> 8) << 8 | (int) (byte) ((int) _param3.B * num1 >> 8);
    _param0.\u0023\u003DzJwP1eeijr15N(_param1, _param2, num2);
  }

  internal static void \u0023\u003DzJwP1eeijr15N(
    this WriteableBitmap _param0,
    int[] _param1,
    float _param2,
    int _param3)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzocnp0n1g0lrV(_param1[0], _param1[1], _param1[0], _param1[1], _param1[2], _param1[3], _param1[4], _param1[5], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
      int index;
      for (index = 2; index < _param1.Length - 4; index += 2)
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzocnp0n1g0lrV(_param1[index - 2], _param1[index - 1], _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[index + 4], _param1[index + 5], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzocnp0n1g0lrV(_param1[index - 2], _param1[index - 1], _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[index + 2], _param1[index + 3], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
    }
  }

  internal static void \u0023\u003Dz_JvuZMkAPzoM(
    this WriteableBitmap _param0,
    int[] _param1,
    float _param2,
    Color _param3)
  {
    int num1 = (int) _param3.A + 1;
    int num2 = (int) _param3.A << 24 | (int) (byte) ((int) _param3.R * num1 >> 8) << 16 /*0x10*/ | (int) (byte) ((int) _param3.G * num1 >> 8) << 8 | (int) (byte) ((int) _param3.B * num1 >> 8);
    _param0.\u0023\u003Dz_JvuZMkAPzoM(_param1, _param2, num2);
  }

  internal static void \u0023\u003Dz_JvuZMkAPzoM(
    this WriteableBitmap _param0,
    int[] _param1,
    float _param2,
    int _param3)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int length = _param1.Length;
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzocnp0n1g0lrV(_param1[length - 2], _param1[length - 1], _param1[0], _param1[1], _param1[2], _param1[3], _param1[4], _param1[5], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
      int index;
      for (index = 2; index < length - 4; index += 2)
        \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzocnp0n1g0lrV(_param1[index - 2], _param1[index - 1], _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[index + 4], _param1[index + 5], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzocnp0n1g0lrV(_param1[index - 2], _param1[index - 1], _param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[0], _param1[1], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
      \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dzocnp0n1g0lrV(_param1[index], _param1[index + 1], _param1[index + 2], _param1[index + 3], _param1[0], _param1[1], _param1[2], _param1[3], _param2, _param3, bitmapContext, pixelWidth, pixelHeight);
    }
  }

  internal static WriteableBitmap \u0023\u003Dz7FKHKl8\u003D(
    this WriteableBitmap _param0,
    int _param1,
    int _param2,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzeruYkaM\u003D _param3)
  {
    using (BitmapContext bitmapContext = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int[] numArray = \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003Dz7FKHKl8\u003D(bitmapContext, _param0.PixelWidth, _param0.PixelHeight, _param1, _param2, _param3);
      WriteableBitmap writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(_param1, _param2);
      BitmapContext.\u0023\u003Dzk\u0024wemaJeIY7r(numArray, 0, bitmapContext, 0, 4 * numArray.Length);
      return writeableBitmap;
    }
  }

  internal static unsafe int[] \u0023\u003Dz7FKHKl8\u003D(
    BitmapContext _param0,
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzeruYkaM\u003D _param5)
  {
    int* numPtr = _param0.\u0023\u003DzSKG\u0024_qBsOJZc();
    int[] numArray = new int[_param3 * _param4];
    float num1 = (float) _param1 / (float) _param3;
    float num2 = (float) _param2 / (float) _param4;
    switch (_param5)
    {
      case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzeruYkaM\u003D) 0:
        int num3 = 0;
        for (int index1 = 0; index1 < _param4; ++index1)
        {
          for (int index2 = 0; index2 < _param3; ++index2)
          {
            float num4 = (float) index2 * num1;
            double num5 = (double) index1 * (double) num2;
            int num6 = (int) num4;
            int num7 = (int) num5;
            numArray[num3++] = numPtr[num7 * _param1 + num6];
          }
        }
        break;
      case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzeruYkaM\u003D) 1:
        int num8 = 0;
        for (int index3 = 0; index3 < _param4; ++index3)
        {
          for (int index4 = 0; index4 < _param3; ++index4)
          {
            float num9 = (float) index4 * num1;
            double num10 = (double) index3 * (double) num2;
            int num11 = (int) num9;
            int num12 = (int) num10;
            float num13 = num9 - (float) num11;
            float num14 = (float) num10 - (float) num12;
            double num15 = 1.0 - (double) num13;
            float num16 = 1f - num14;
            int num17 = num11 + 1;
            if (num17 >= _param1)
              num17 = num11;
            int num18 = num12 + 1;
            if (num18 >= _param2)
              num18 = num12;
            int num19 = numPtr[num12 * _param1 + num11];
            byte num20 = (byte) (num19 >> 24);
            byte num21 = (byte) (num19 >> 16 /*0x10*/);
            byte num22 = (byte) (num19 >> 8);
            byte num23 = (byte) num19;
            int num24 = numPtr[num12 * _param1 + num17];
            byte num25 = (byte) (num24 >> 24);
            byte num26 = (byte) (num24 >> 16 /*0x10*/);
            byte num27 = (byte) (num24 >> 8);
            byte num28 = (byte) num24;
            int num29 = numPtr[num18 * _param1 + num11];
            byte num30 = (byte) (num29 >> 24);
            byte num31 = (byte) (num29 >> 16 /*0x10*/);
            byte num32 = (byte) (num29 >> 8);
            byte num33 = (byte) num29;
            int num34 = numPtr[num18 * _param1 + num17];
            byte num35 = (byte) (num34 >> 24);
            byte num36 = (byte) (num34 >> 16 /*0x10*/);
            byte num37 = (byte) (num34 >> 8);
            byte num38 = (byte) num34;
            float num39 = (float) (num15 * (double) num20 + (double) num13 * (double) num25);
            float num40 = (float) (num15 * (double) num30 + (double) num13 * (double) num35);
            byte num41 = (byte) ((double) num16 * (double) num39 + (double) num14 * (double) num40);
            float num42 = (float) (num15 * (double) num21 * (double) num20 + (double) num13 * (double) num26 * (double) num25);
            float num43 = (float) (num15 * (double) num31 * (double) num30 + (double) num13 * (double) num36 * (double) num35);
            float num44 = (float) ((double) num16 * (double) num42 + (double) num14 * (double) num43);
            float num45 = (float) (num15 * (double) num22 * (double) num20 + (double) num13 * (double) num27 * (double) num25);
            float num46 = (float) (num15 * (double) num32 * (double) num30 + (double) num13 * (double) num37 * (double) num35);
            float num47 = (float) ((double) num16 * (double) num45 + (double) num14 * (double) num46);
            float num48 = (float) (num15 * (double) num23 * (double) num20 + (double) num13 * (double) num28 * (double) num25);
            float num49 = (float) (num15 * (double) num33 * (double) num30 + (double) num13 * (double) num38 * (double) num35);
            float num50 = (float) ((double) num16 * (double) num48 + (double) num14 * (double) num49);
            if (num41 > (byte) 0)
            {
              num44 /= (float) num41;
              num47 /= (float) num41;
              num50 /= (float) num41;
            }
            byte num51 = (byte) num44;
            byte num52 = (byte) num47;
            byte num53 = (byte) num50;
            numArray[num8++] = (int) num41 << 24 | (int) num51 << 16 /*0x10*/ | (int) num52 << 8 | (int) num53;
          }
        }
        break;
    }
    return numArray;
  }

  internal static unsafe WriteableBitmap \u0023\u003Dz1zxzaac\u003D(
    this WriteableBitmap _param0,
    int _param1)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext1 = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr1 = bitmapContext1.\u0023\u003DzSKG\u0024_qBsOJZc();
      int index1 = 0;
      _param1 %= 360;
      WriteableBitmap writeableBitmap;
      if (_param1 > 0 && _param1 <= 90)
      {
        writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(pixelHeight, pixelWidth);
        using (BitmapContext bitmapContext2 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
        {
          int* numPtr2 = bitmapContext2.\u0023\u003DzSKG\u0024_qBsOJZc();
          for (int index2 = 0; index2 < pixelWidth; ++index2)
          {
            for (int index3 = pixelHeight - 1; index3 >= 0; --index3)
            {
              int index4 = index3 * pixelWidth + index2;
              numPtr2[index1] = numPtr1[index4];
              ++index1;
            }
          }
        }
      }
      else if (_param1 > 90 && _param1 <= 180)
      {
        writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(pixelWidth, pixelHeight);
        using (BitmapContext bitmapContext3 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
        {
          int* numPtr3 = bitmapContext3.\u0023\u003DzSKG\u0024_qBsOJZc();
          for (int index5 = pixelHeight - 1; index5 >= 0; --index5)
          {
            for (int index6 = pixelWidth - 1; index6 >= 0; --index6)
            {
              int index7 = index5 * pixelWidth + index6;
              numPtr3[index1] = numPtr1[index7];
              ++index1;
            }
          }
        }
      }
      else if (_param1 > 180 && _param1 <= 270)
      {
        writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(pixelHeight, pixelWidth);
        using (BitmapContext bitmapContext4 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
        {
          int* numPtr4 = bitmapContext4.\u0023\u003DzSKG\u0024_qBsOJZc();
          for (int index8 = pixelWidth - 1; index8 >= 0; --index8)
          {
            for (int index9 = 0; index9 < pixelHeight; ++index9)
            {
              int index10 = index9 * pixelWidth + index8;
              numPtr4[index1] = numPtr1[index10];
              ++index1;
            }
          }
        }
      }
      else
        writeableBitmap = _param0.Clone();
      return writeableBitmap;
    }
  }

  internal static unsafe WriteableBitmap \u0023\u003Dz9CcvHZvl4d5\u0024(
    this WriteableBitmap _param0,
    double _param1,
    bool _param2)
  {
    double num1 = -1.0 * Math.PI / 180.0 * _param1;
    int pixelWidth1 = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    int num2;
    int num3;
    if (_param2)
    {
      num2 = pixelWidth1;
      num3 = pixelHeight;
    }
    else
    {
      double num4 = _param1 / (180.0 / Math.PI);
      num2 = (int) Math.Ceiling(Math.Abs(Math.Sin(num4) * (double) pixelHeight) + Math.Abs(Math.Cos(num4) * (double) pixelWidth1));
      num3 = (int) Math.Ceiling(Math.Abs(Math.Sin(num4) * (double) pixelWidth1) + Math.Abs(Math.Cos(num4) * (double) pixelHeight));
    }
    int num5 = pixelWidth1 / 2;
    int num6 = pixelHeight / 2;
    int num7 = num2 / 2;
    int num8 = num3 / 2;
    WriteableBitmap writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(num2, num3);
    using (BitmapContext bitmapContext1 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
    {
      using (BitmapContext bitmapContext2 = _param0.\u0023\u003DzjnjmjBtrwZM5())
      {
        int* numPtr1 = bitmapContext1.\u0023\u003DzSKG\u0024_qBsOJZc();
        int* numPtr2 = bitmapContext2.\u0023\u003DzSKG\u0024_qBsOJZc();
        int pixelWidth2 = _param0.PixelWidth;
        for (int index1 = 0; index1 < num3; ++index1)
        {
          for (int index2 = 0; index2 < num2; ++index2)
          {
            int x = index2 - num7;
            int y = num8 - index1;
            double num9 = Math.Sqrt((double) (x * x + y * y));
            double num10;
            if (x == 0)
            {
              if (y == 0)
              {
                numPtr1[index1 * num2 + index2] = numPtr2[num6 * pixelWidth2 + num5];
                continue;
              }
              num10 = y >= 0 ? Math.PI / 2.0 : 3.0 * Math.PI / 2.0;
            }
            else
              num10 = Math.Atan2((double) y, (double) x);
            double num11 = num10 - num1;
            double num12 = num9 * Math.Cos(num11);
            double num13 = num9 * Math.Sin(num11);
            double num14 = num12 + (double) num5;
            double num15 = (double) num6 - num13;
            int num16 = (int) Math.Floor(num14);
            int num17 = (int) Math.Floor(num15);
            int num18 = (int) Math.Ceiling(num14);
            int num19 = (int) Math.Ceiling(num15);
            if (num16 >= 0 && num18 >= 0 && num16 < pixelWidth1 && num18 < pixelWidth1 && num17 >= 0 && num19 >= 0 && num17 < pixelHeight && num19 < pixelHeight)
            {
              double num20 = num14 - (double) num16;
              double num21 = num15 - (double) num17;
              Color color1 = _param0.\u0023\u003DzFiIk5SM\u003D(num16, num17);
              Color color2 = _param0.\u0023\u003DzFiIk5SM\u003D(num18, num17);
              Color color3 = _param0.\u0023\u003DzFiIk5SM\u003D(num16, num19);
              Color color4 = _param0.\u0023\u003DzFiIk5SM\u003D(num18, num19);
              double num22 = (1.0 - num20) * (double) color1.R + num20 * (double) color2.R;
              double num23 = (1.0 - num20) * (double) color1.G + num20 * (double) color2.G;
              double num24 = (1.0 - num20) * (double) color1.B + num20 * (double) color2.B;
              double num25 = (1.0 - num20) * (double) color1.A + num20 * (double) color2.A;
              double num26 = (1.0 - num20) * (double) color3.R + num20 * (double) color4.R;
              double num27 = (1.0 - num20) * (double) color3.G + num20 * (double) color4.G;
              double num28 = (1.0 - num20) * (double) color3.B + num20 * (double) color4.B;
              double num29 = (1.0 - num20) * (double) color3.A + num20 * (double) color4.A;
              int num30 = (int) Math.Round((1.0 - num21) * num22 + num21 * num26);
              int num31 = (int) Math.Round((1.0 - num21) * num23 + num21 * num27);
              int num32 = (int) Math.Round((1.0 - num21) * num24 + num21 * num28);
              int num33 = (int) Math.Round((1.0 - num21) * num25 + num21 * num29);
              if (num30 < 0)
                num30 = 0;
              if (num30 > (int) byte.MaxValue)
                num30 = (int) byte.MaxValue;
              if (num31 < 0)
                num31 = 0;
              if (num31 > (int) byte.MaxValue)
                num31 = (int) byte.MaxValue;
              if (num32 < 0)
                num32 = 0;
              if (num32 > (int) byte.MaxValue)
                num32 = (int) byte.MaxValue;
              if (num33 < 0)
                num33 = 0;
              if (num33 > (int) byte.MaxValue)
                num33 = (int) byte.MaxValue;
              int num34 = num33 + 1;
              numPtr1[index1 * num2 + index2] = num33 << 24 | (int) (byte) (num30 * num34 >> 8) << 16 /*0x10*/ | (int) (byte) (num31 * num34 >> 8) << 8 | (int) (byte) (num32 * num34 >> 8);
            }
          }
        }
        return writeableBitmap;
      }
    }
  }

  internal static unsafe WriteableBitmap \u0023\u003Dzq1wgKfc\u003D(
    this WriteableBitmap _param0,
    \u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzZSpFwmoJ\u0024y0y _param1)
  {
    int pixelWidth = _param0.PixelWidth;
    int pixelHeight = _param0.PixelHeight;
    using (BitmapContext bitmapContext1 = _param0.\u0023\u003DzjnjmjBtrwZM5())
    {
      int* numPtr1 = bitmapContext1.\u0023\u003DzSKG\u0024_qBsOJZc();
      int index1 = 0;
      WriteableBitmap writeableBitmap = (WriteableBitmap) null;
      switch (_param1)
      {
        case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzZSpFwmoJ\u0024y0y) 0:
          writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(pixelWidth, pixelHeight);
          using (BitmapContext bitmapContext2 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
          {
            int* numPtr2 = bitmapContext2.\u0023\u003DzSKG\u0024_qBsOJZc();
            for (int index2 = 0; index2 < pixelHeight; ++index2)
            {
              for (int index3 = pixelWidth - 1; index3 >= 0; --index3)
              {
                int index4 = index2 * pixelWidth + index3;
                numPtr2[index1] = numPtr1[index4];
                ++index1;
              }
            }
            break;
          }
        case (\u0023\u003DzKX_o18CSOBV8bEiC9p\u0024hcRqT_aFFGC8SXHRDFRNLhT09ZbT5e0zs1o6P3wDxp8X89w\u003D\u003D.\u0023\u003DzZSpFwmoJ\u0024y0y) 1:
          writeableBitmap = \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H4mTMLasK3mtdLa7x5wzHVAkgC1CG\u0024N92YKzAECB.\u0023\u003DzfScL5aE\u003D(pixelWidth, pixelHeight);
          using (BitmapContext bitmapContext3 = writeableBitmap.\u0023\u003DzjnjmjBtrwZM5())
          {
            int* numPtr3 = bitmapContext3.\u0023\u003DzSKG\u0024_qBsOJZc();
            for (int index5 = pixelHeight - 1; index5 >= 0; --index5)
            {
              for (int index6 = 0; index6 < pixelWidth; ++index6)
              {
                int index7 = index5 * pixelWidth + index6;
                numPtr3[index1] = numPtr1[index7];
                ++index1;
              }
            }
            break;
          }
      }
      return writeableBitmap;
    }
  }

  internal enum \u0023\u003DzEzdVAvx1gj20
  {
  }

  internal enum \u0023\u003DzZSpFwmoJ\u0024y0y
  {
  }

  internal enum \u0023\u003DzeruYkaM\u003D
  {
  }
}
