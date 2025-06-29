// Decompiled with JetBrains decompiler
// Type: #=zdPAQRlt3VWWvvKbSPLZ0IdT9dyz7oMs24P2Fx4JrIgV$y9xNKaNytRdYOY23
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable enable
internal sealed class \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdT9dyz7oMs24P2Fx4JrIgV\u0024y9xNKaNytRdYOY23 : 
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024,
  IDisposable
{
  
  private 
  #nullable disable
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzXZq\u0024gjyIo\u00244n;
  
  private readonly \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX \u0023\u003DzEcmsYfw\u003D;
  
  private readonly \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D \u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J;
  
  private Point \u0023\u003DzeDqneUWYjgVB;

  public \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdT9dyz7oMs24P2Fx4JrIgV\u0024y9xNKaNytRdYOY23(
    \u0023\u003DzpKvy0OA0_My0Sg27HiUJaX\u0024AyxSGkqEcPv0Ah3hMaVEX _param1,
    \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D _param2)
  {
    this.\u0023\u003DzEcmsYfw\u003D = _param1;
    this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J = _param2;
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003Dz7ZSU06M\u003D(
    \u0023\u003Dzo13esGCwfQJn\u0024h2kOXY\u0024_bD_Pyrb\u0024d0P2noEI5c\u003D _param1,
    double _param2,
    double _param3)
  {
    this.\u0023\u003DzeDqneUWYjgVB = new Point(_param2, _param3);
    Point point = this.\u0023\u003Dzop6vn0GowyiR(this.\u0023\u003DzeDqneUWYjgVB);
    this.\u0023\u003DzXZq\u0024gjyIo\u00244n = this.\u0023\u003DzEcmsYfw\u003D.\u0023\u003Dz7ZSU06M\u003D(_param1, point.X, point.Y);
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  private Point \u0023\u003Dzop6vn0GowyiR(Point _param1)
  {
    return this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J.\u0023\u003DzsTReN_n58EEf(_param1);
  }

  public \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024 \u0023\u003DzfRDRUq8\u003D(
    double _param1,
    double _param2)
  {
    this.\u0023\u003Dzmfkjk40xo1ck(new Point(_param1, _param2));
    return (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) this;
  }

  private void \u0023\u003Dzmfkjk40xo1ck(Point _param1)
  {
    this.\u0023\u003DzSrYqDAdK62dH(this.\u0023\u003DzeDqneUWYjgVB, _param1);
    this.\u0023\u003DzNq_YOflx6uAn(_param1);
    this.\u0023\u003DzeDqneUWYjgVB = _param1;
  }

  private void \u0023\u003DzSrYqDAdK62dH(Point _param1, Point _param2)
  {
    foreach (Point point in \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdT9dyz7oMs24P2Fx4JrIgV\u0024y9xNKaNytRdYOY23.\u0023\u003Dzu8WSnHW5eYXx_MVLzQ\u003D\u003D(_param1, _param2))
      this.\u0023\u003DzNq_YOflx6uAn(point);
  }

  private static IEnumerable<Point> \u0023\u003Dzu8WSnHW5eYXx_MVLzQ\u003D\u003D(
    Point _param0,
    Point _param1)
  {
    return (IEnumerable<Point>) new \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdT9dyz7oMs24P2Fx4JrIgV\u0024y9xNKaNytRdYOY23.\u0023\u003DzPNUNMhERRYPwzKF_aNgUbuU\u003D(-2)
    {
      \u0023\u003DzoXFq90nEuXYCONAPdA\u003D\u003D = _param0,
      \u0023\u003DzTyFgSrmTCJ3_JX2iEQ\u003D\u003D = _param1
    };
  }

  private static int \u0023\u003DzOE26zfa0ygdXJEa3zQ\u003D\u003D(Point _param0, Point _param1)
  {
    double num = Math.Abs(_param0.X - _param1.X);
    return (int) (Math.PI * ((Math.Abs(_param0.Y) + Math.Abs(_param1.Y)) / 2.0) * (num / 180.0) / 4.0);
  }

  private void \u0023\u003DzNq_YOflx6uAn(Point _param1)
  {
    Point point = this.\u0023\u003Dz3JhL3ghZJXhh2PqEiwlNXv1dPT_J.\u0023\u003DzsTReN_n58EEf(_param1);
    this.\u0023\u003DzXZq\u0024gjyIo\u00244n.\u0023\u003DzfRDRUq8\u003D(point.X, point.Y);
  }

  public void \u0023\u003DzBNsE20w\u003D()
  {
    if (this.\u0023\u003DzXZq\u0024gjyIo\u00244n == null)
      return;
    this.\u0023\u003DzXZq\u0024gjyIo\u00244n.\u0023\u003DzBNsE20w\u003D();
    this.\u0023\u003DzXZq\u0024gjyIo\u00244n = (\u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe_IXeV1qkdyQymVhxLr4oDQ\u0024) null;
  }

  public void Dispose() => this.\u0023\u003DzBNsE20w\u003D();

  private sealed class \u0023\u003DzPNUNMhERRYPwzKF_aNgUbuU\u003D : 
    IEnumerable<Point>,
    IEnumerable,
    IEnumerator<Point>,
    IEnumerator,
    IDisposable
  {
    
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    
    private Point \u0023\u003Dzaev1bhaFFIDX;
    
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    
    private Point \u0023\u003Dzul3UIvg\u003D;
    
    public Point \u0023\u003DzoXFq90nEuXYCONAPdA\u003D\u003D;
    
    private Point \u0023\u003Dz1fZcCSo\u003D;
    
    public Point \u0023\u003DzTyFgSrmTCJ3_JX2iEQ\u003D\u003D;
    
    private int \u0023\u003Dz\u0024Jq4fjsW911sBo9e1Q\u003D\u003D;
    
    private double \u0023\u003DzjyrSHEwsQBDE1gpEMQ\u003D\u003D;
    
    private double \u0023\u003DzQOIJ1TmGwKzjJrbq6w\u003D\u003D;
    
    private double \u0023\u003DzTRfI0oIcNk2wEFLNvA\u003D\u003D;
    
    private double \u0023\u003Dz49G2_dDCjGlwZUZ7pg\u003D\u003D;
    
    private int \u0023\u003Dz85Hyxf6MHzOS;

    [DebuggerHidden]
    public \u0023\u003DzPNUNMhERRYPwzKF_aNgUbuU\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.\u0023\u003DzyDgD8d_Zy8d21234Xw\u003D\u003D()
    {
    }

    bool IEnumerator.MoveNext()
    {
      switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
      {
        case 0:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          this.\u0023\u003Dz\u0024Jq4fjsW911sBo9e1Q\u003D\u003D = \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdT9dyz7oMs24P2Fx4JrIgV\u0024y9xNKaNytRdYOY23.\u0023\u003DzOE26zfa0ygdXJEa3zQ\u003D\u003D(this.\u0023\u003Dzul3UIvg\u003D, this.\u0023\u003Dz1fZcCSo\u003D);
          if (this.\u0023\u003Dz\u0024Jq4fjsW911sBo9e1Q\u003D\u003D > 0)
          {
            this.\u0023\u003DzjyrSHEwsQBDE1gpEMQ\u003D\u003D = this.\u0023\u003Dzul3UIvg\u003D.X;
            double x = this.\u0023\u003Dz1fZcCSo\u003D.X;
            this.\u0023\u003DzQOIJ1TmGwKzjJrbq6w\u003D\u003D = this.\u0023\u003Dzul3UIvg\u003D.Y;
            double y = this.\u0023\u003Dz1fZcCSo\u003D.Y;
            int num = this.\u0023\u003Dz\u0024Jq4fjsW911sBo9e1Q\u003D\u003D + 1;
            this.\u0023\u003DzTRfI0oIcNk2wEFLNvA\u003D\u003D = (x - this.\u0023\u003DzjyrSHEwsQBDE1gpEMQ\u003D\u003D) / (double) num;
            this.\u0023\u003Dz49G2_dDCjGlwZUZ7pg\u003D\u003D = (y - this.\u0023\u003DzQOIJ1TmGwKzjJrbq6w\u003D\u003D) / (double) num;
            this.\u0023\u003Dz85Hyxf6MHzOS = 0;
            break;
          }
          goto label_7;
        case 1:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          ++this.\u0023\u003Dz85Hyxf6MHzOS;
          break;
        default:
          return false;
      }
      if (this.\u0023\u003Dz85Hyxf6MHzOS < this.\u0023\u003Dz\u0024Jq4fjsW911sBo9e1Q\u003D\u003D)
      {
        this.\u0023\u003DzjyrSHEwsQBDE1gpEMQ\u003D\u003D += this.\u0023\u003DzTRfI0oIcNk2wEFLNvA\u003D\u003D;
        this.\u0023\u003DzQOIJ1TmGwKzjJrbq6w\u003D\u003D += this.\u0023\u003Dz49G2_dDCjGlwZUZ7pg\u003D\u003D;
        this.\u0023\u003Dzaev1bhaFFIDX = new Point(this.\u0023\u003DzjyrSHEwsQBDE1gpEMQ\u003D\u003D, this.\u0023\u003DzQOIJ1TmGwKzjJrbq6w\u003D\u003D);
        this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
        return true;
      }
label_7:
      return false;
    }

    [DebuggerHidden]
    Point IEnumerator<Point>.\u0023\u003DzdQvlJwBrQOkhF8rejS5KemZ9qdsUIguNvslfeT0\u003D()
    {
      return this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    [DebuggerHidden]
    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator<Point> IEnumerable<Point>.\u0023\u003Dzz0QiOq5G7BemC3GqHNUcyAqBFdcssAbLh0fYuZs\u003D()
    {
      \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdT9dyz7oMs24P2Fx4JrIgV\u0024y9xNKaNytRdYOY23.\u0023\u003DzPNUNMhERRYPwzKF_aNgUbuU\u003D erryPwzKfANgUbuU;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        erryPwzKfANgUbuU = this;
      }
      else
        erryPwzKfANgUbuU = new \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IdT9dyz7oMs24P2Fx4JrIgV\u0024y9xNKaNytRdYOY23.\u0023\u003DzPNUNMhERRYPwzKF_aNgUbuU\u003D(0);
      erryPwzKfANgUbuU.\u0023\u003Dzul3UIvg\u003D = this.\u0023\u003DzoXFq90nEuXYCONAPdA\u003D\u003D;
      erryPwzKfANgUbuU.\u0023\u003Dz1fZcCSo\u003D = this.\u0023\u003DzTyFgSrmTCJ3_JX2iEQ\u003D\u003D;
      return (IEnumerator<Point>) erryPwzKfANgUbuU;
    }

    [DebuggerHidden]
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003Dzz0QiOq5G7BemC3GqHNUcyAqBFdcssAbLh0fYuZs\u003D();
    }
  }
}
