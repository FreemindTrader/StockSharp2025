// Decompiled with JetBrains decompiler
// Type: #=ztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
internal sealed class \u0023\u003DztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg\u003D : 
  \u0023\u003DzlalC_BLW58oQFzS2Y8CMpwbBRmxTjoI81dC7J9YT\u0024RWJeZXysfONBiA\u003D<Decimal>
{
  private double \u0023\u003DzT\u0024kqKKwnFTS7 = 1.0;

  public \u0023\u003DztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg\u003D()
  {
    this.\u0023\u003DzWGTdqe9TpK9M(true);
    this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(true);
  }

  public bool \u0023\u003DzZNXhT1ubdKMn() => this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D();

  public void \u0023\u003DzWGTdqe9TpK9M(bool _param1)
  {
    this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(_param1);
  }

  public bool \u0023\u003DzYod2AP\u0024vFlZp9roA9AjCses\u003D()
  {
    return this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl();
  }

  public void \u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(bool _param1)
  {
    this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(_param1);
  }

  internal double \u0023\u003Dz\u0024rRwq4PtSMIR() => this.\u0023\u003DzT\u0024kqKKwnFTS7;

  public override void \u0023\u003DzFIf7JZ5S\u0024Wr_(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    Decimal _param2,
    bool _param3)
  {
    this.\u0023\u003Dzhe7qd\u0024I\u003D((IList<Decimal>) _param1, 1);
    if (!_param3 && !this.\u0023\u003DzZNXhT1ubdKMn())
      throw new InvalidOperationException("");
  }

  public override void \u0023\u003DzeU6gWqHRfREz(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    int _param2,
    IEnumerable<Decimal> _param3,
    bool _param4)
  {
    this.\u0023\u003Dzhe7qd\u0024I\u003D((IList<Decimal>) _param1, ((\u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D) _param1).get_Count() - _param2);
    if (!_param4 && !this.\u0023\u003DzZNXhT1ubdKMn())
      throw new InvalidOperationException("");
  }

  public override void \u0023\u003Dzs9WSchJIpnF0(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    int _param2,
    Decimal _param3,
    bool _param4)
  {
    this.\u0023\u003DzJ\u0024jJ8KQ\u003D((IList<Decimal>) _param1, _param2, 1);
    if (!_param4 && !this.\u0023\u003DzZNXhT1ubdKMn())
      throw new InvalidOperationException("");
  }

  public override void \u0023\u003DzPY2yStN8KbO\u0024(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    int _param2,
    int _param3,
    IEnumerable<Decimal> _param4,
    bool _param5)
  {
    this.\u0023\u003DzJ\u0024jJ8KQ\u003D((IList<Decimal>) _param1, _param2, _param4.Count<Decimal>());
    if (!_param5 && !this.\u0023\u003DzZNXhT1ubdKMn())
      throw new InvalidOperationException("");
  }

  public void \u0023\u003Dzhe7qd\u0024I\u003D(IList<Decimal> _param1, int _param2)
  {
    if (!this.\u0023\u003DzZNXhT1ubdKMn())
      return;
    int count = _param1.Count;
    int index = count - _param2;
    this.\u0023\u003DzWGTdqe9TpK9M((_param2 == 1 || \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003DzNe9B4H63T3gE<Decimal>(_param1, index, _param2)) & (count <= 1 || index <= 0 || _param1[index] >= _param1[index - 1]));
    if (!this.\u0023\u003DzYod2AP\u0024vFlZp9roA9AjCses\u003D())
      return;
    double num1 = 1.0;
    double num2 = this.\u0023\u003DzT\u0024kqKKwnFTS7 * 0.000125;
    if ((_param2 == 1 ? 1 : (\u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003DzScZFqV8CJEMRehn4FqDXCME\u003D<Decimal>(_param1, index, _param2, num2, out num1) ? 1 : 0)) == 0)
      this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
    else if (count > _param2 && _param2 >= 2 && Math.Abs(this.\u0023\u003DzT\u0024kqKKwnFTS7 - num1) > num2)
      this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
    else if (index > 0)
    {
      double num3 = \u0023\u003DztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param1[index - 1]);
      double num4 = \u0023\u003DztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param1[index]) - num3;
      if (_param1.Count > 2)
      {
        if (Math.Abs(num4 - this.\u0023\u003DzT\u0024kqKKwnFTS7) > num2)
        {
          this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
          return;
        }
      }
      else if (num4 == 0.0)
      {
        this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
        return;
      }
      this.\u0023\u003DzT\u0024kqKKwnFTS7 = num4;
    }
    else
      this.\u0023\u003DzT\u0024kqKKwnFTS7 = num1;
  }

  public void \u0023\u003DzJ\u0024jJ8KQ\u003D(IList<Decimal> _param1, int _param2, int _param3)
  {
    if (!this.\u0023\u003DzZNXhT1ubdKMn())
      return;
    bool flag = _param3 == 1 || \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003DzNe9B4H63T3gE<Decimal>(_param1, _param2, _param3);
    int index = _param2 + _param3 - 1;
    this.\u0023\u003DzWGTdqe9TpK9M(flag && (_param2 == 0 || _param1[_param2] >= _param1[_param2 - 1]) && (index >= _param1.Count - 1 || _param1[index] <= _param1[index + 1]));
    if (!this.\u0023\u003DzYod2AP\u0024vFlZp9roA9AjCses\u003D())
      return;
    double num1 = 1.0;
    double num2 = this.\u0023\u003DzT\u0024kqKKwnFTS7 * 0.000125;
    if ((_param3 == 1 ? 1 : (\u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003DzScZFqV8CJEMRehn4FqDXCME\u003D<Decimal>(_param1, _param2, _param3, num2, out num1) ? 1 : 0)) == 0)
      this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
    else if (_param1.Count > _param3 && _param3 >= 2 && Math.Abs(this.\u0023\u003DzT\u0024kqKKwnFTS7 - num1) > num2)
    {
      this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
    }
    else
    {
      if (_param2 > 0)
      {
        double num3 = \u0023\u003DztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param1[_param2 - 1]);
        double num4 = \u0023\u003DztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param1[_param2]) - num3;
        if (_param1.Count > 2)
        {
          if (Math.Abs(num4 - this.\u0023\u003DzT\u0024kqKKwnFTS7) > num2)
          {
            this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
            return;
          }
        }
        else if (num4 == 0.0)
        {
          this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
          return;
        }
        this.\u0023\u003DzT\u0024kqKKwnFTS7 = num4;
      }
      if (index < _param1.Count - 1)
      {
        double num5 = \u0023\u003DztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param1[index]);
        double num6 = \u0023\u003DztyAKlj3UbIrpcOb4hAbyLoyC1uHFTnz2mGzeexL5RRRMqcjTZqBB6tg\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param1[index + 1]) - num5;
        if (_param1.Count > 2)
        {
          if (Math.Abs(num6 - this.\u0023\u003DzT\u0024kqKKwnFTS7) > num2)
          {
            this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
            return;
          }
        }
        else if (num6 == 0.0)
        {
          this.\u0023\u003DztzlkMHp1fyBYyPnbdOgIwYc\u003D(false);
          return;
        }
        this.\u0023\u003DzT\u0024kqKKwnFTS7 = num6;
      }
      else
        this.\u0023\u003DzT\u0024kqKKwnFTS7 = num1;
    }
  }

  public override void \u0023\u003DzUf222sU\u003D()
  {
    base.\u0023\u003DzUf222sU\u003D();
    this.\u0023\u003DzT\u0024kqKKwnFTS7 = 1.0;
  }

  private static double \u0023\u003DzkUwF72z8\u0024rM9(Decimal _param0) => (double) _param0;
}
