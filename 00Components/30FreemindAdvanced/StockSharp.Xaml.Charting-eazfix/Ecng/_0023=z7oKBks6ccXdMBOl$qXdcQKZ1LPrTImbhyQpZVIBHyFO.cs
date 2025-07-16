// Decompiled with JetBrains decompiler
// Type: #=z7oKBks6ccXdMBOl$qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;

#nullable disable
public sealed class \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D : 
  \u0023\u003DzlalC_BLW58oQFzS2Y8CMpwbBRmxTjoI81dC7J9YT\u0024RWJeZXysfONBiA\u003D<Decimal>
{
  private double \u0023\u003DzECK7c5Gz4mSc;
  private double \u0023\u003DzUvUfmcjO8Kin;
  private bool \u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D;
  private double \u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D;

  public override void \u0023\u003DzFIf7JZ5S\u0024Wr_(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    Decimal _param2,
    bool _param3)
  {
    this.\u0023\u003DzUIhkiEELxlDXkW60HA\u003D\u003D(_param1, _param2, ((ICollection<Decimal>) _param1).Count - 1, _param3);
  }

  public override void \u0023\u003DzeU6gWqHRfREz(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    int _param2,
    IEnumerable<Decimal> _param3,
    bool _param4)
  {
    switch (_param3)
    {
      case Decimal[] numArray2:
        this.\u0023\u003DzhyFsF2I8e3f6_Y5THg\u003D\u003D((IList<Decimal>) _param1, _param2, numArray2, numArray2.Length, _param4);
        break;
      case IList<Decimal> numList:
        int count = numList.Count;
        Decimal[] numArray1 = numList.\u0023\u003Dz1bvQV4SZTWpA<Decimal>();
        this.\u0023\u003DzhyFsF2I8e3f6_Y5THg\u003D\u003D((IList<Decimal>) _param1, _param2, numArray1, count, _param4);
        break;
      default:
        IEnumerable<Decimal> nums = _param3;
        int num = _param2;
        using (IEnumerator<Decimal> enumerator = nums.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Decimal current = enumerator.Current;
            this.\u0023\u003DzUIhkiEELxlDXkW60HA\u003D\u003D(_param1, current, num, _param4);
            ++num;
          }
          break;
        }
    }
  }

  public override void \u0023\u003Dzs9WSchJIpnF0(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    int _param2,
    Decimal _param3,
    bool _param4)
  {
    if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && !this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl())
      return;
    IList<Decimal> numList = (IList<Decimal>) _param1;
    int count = numList.Count;
    if (_param2 == 0)
    {
      if (count <= 1)
        return;
      Decimal num1 = numList[1];
      double num2 = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(num1) - \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param3);
      this.\u0023\u003DzUvUfmcjO8Kin = num2;
      this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D = true;
      if (num2 < 0.0)
      {
        this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(false);
        this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
        if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && !_param4)
          throw new InvalidOperationException("Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks");
      }
      else
      {
        if (!this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl() || !this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D)
          return;
        double num3 = num2 - (\u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(numList[2]) - \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(num1));
        if (num3 < 0.0)
          num3 = -num3;
        if (num3 <= this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D)
          return;
        this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
      }
    }
    else if (_param2 == count)
    {
      this.\u0023\u003DzFIf7JZ5S\u0024Wr_(_param1, _param3, _param4);
    }
    else
    {
      this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
      if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D())
        return;
      if (_param2 > 0 && numList[_param2 - 1] > _param3)
      {
        this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(false);
        if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && !_param4)
          throw new InvalidOperationException("Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks");
      }
      if (_param2 >= count - 1 || !(numList[_param2 + 1] < _param3))
        return;
      this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(false);
      if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && !_param4)
        throw new InvalidOperationException("Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks");
    }
  }

  public override void \u0023\u003DzPY2yStN8KbO\u0024(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    int _param2,
    int _param3,
    IEnumerable<Decimal> _param4,
    bool _param5)
  {
    IList<Decimal> numList = (IList<Decimal>) _param1;
    int count = numList.Count;
    if (_param2 + _param3 == count)
      this.\u0023\u003DzeU6gWqHRfREz(_param1, count - _param3, _param4, _param5);
    else if (_param2 == 0)
    {
      if (count <= 2)
        return;
      double num1 = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(numList[1]) - \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(numList[0]);
      this.\u0023\u003DzUvUfmcjO8Kin = num1;
      this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D = true;
      this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D = this.\u0023\u003DzUvUfmcjO8Kin * 0.000125;
      double znxnJcVnyOdRqEfYqdQ = this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D;
      for (int index = 2; index < _param3; ++index)
      {
        double num2 = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(numList[index]);
        double num3 = num2 - this.\u0023\u003DzECK7c5Gz4mSc;
        this.\u0023\u003DzECK7c5Gz4mSc = num2;
        if (num3 < 0.0)
        {
          this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(false);
          this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
          if (this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() || _param5)
            break;
          throw new InvalidOperationException("Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks");
        }
        if (this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl())
        {
          double num4 = num3 - num1;
          if (num4 < 0.0)
            num4 = -num4;
          if (num4 > znxnJcVnyOdRqEfYqdQ)
          {
            this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
            break;
          }
        }
      }
    }
    else
    {
      this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
      if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D())
        return;
      Decimal num5 = numList[_param2 - 1];
      int num6 = _param2 + _param3 + 1;
      for (int index = _param2; index < num6; ++index)
      {
        Decimal num7 = numList[index];
        if (num7 < num5)
        {
          this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(false);
          if (this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() || _param5)
            break;
          throw new InvalidOperationException("Data has been Inserted to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks");
        }
        num5 = num7;
      }
    }
  }

  private void \u0023\u003DzUIhkiEELxlDXkW60HA\u003D\u003D(
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<Decimal> _param1,
    Decimal _param2,
    int _param3,
    bool _param4)
  {
    if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && !this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl())
      return;
    if (_param3 > 0)
    {
      double num1 = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param2) - this.\u0023\u003DzECK7c5Gz4mSc;
      if (num1 < 0.0)
      {
        this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(false);
        this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
        if (this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() || _param4)
          return;
        throw new InvalidOperationException("Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks");
      }
      if (this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl())
      {
        if (this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D)
        {
          double num2 = num1 - this.\u0023\u003DzUvUfmcjO8Kin;
          if (num2 < 0.0)
            num2 = -num2;
          if (num2 > this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D)
            this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
        }
        else
        {
          this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D = true;
          this.\u0023\u003DzUvUfmcjO8Kin = num1;
          this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D = num1 * 0.000125;
        }
      }
    }
    this.\u0023\u003DzECK7c5Gz4mSc = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param2);
  }

  private void \u0023\u003DzhyFsF2I8e3f6_Y5THg\u003D\u003D(
    IList<Decimal> _param1,
    int _param2,
    Decimal[] _param3,
    int _param4,
    bool _param5)
  {
    if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && !this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl())
      return;
    int num1 = _param4;
    double znxnJcVnyOdRqEfYqdQ = this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D;
    if (_param2 > 0 && _param4 > 0)
    {
      double num2 = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param3[0]) - this.\u0023\u003DzECK7c5Gz4mSc;
      if (this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && this.\u0023\u003DzECK7c5Gz4mSc > \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param3[0]))
      {
        this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(false);
        if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && !_param5)
          throw new InvalidOperationException("Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks");
      }
      if (this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl())
      {
        if (this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D && _param4 > 0)
        {
          double num3 = num2 - this.\u0023\u003DzUvUfmcjO8Kin;
          if (num3 < 0.0)
            num3 = -num3;
          if (num3 > this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D)
            this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
        }
        else
        {
          this.\u0023\u003DzUvUfmcjO8Kin = num2;
          this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D = true;
          this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D = num2 * 0.000125;
        }
      }
    }
    double num4 = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param3[0]);
    double num5 = this.\u0023\u003DzUvUfmcjO8Kin;
    bool flag = this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D;
    for (int index = 1; index < num1; ++index)
    {
      double num6 = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param3[index]);
      double num7 = num6 - num4;
      if (num7 < 0.0)
      {
        this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(false);
        this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
        if (!this.\u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D() && !_param5)
          throw new InvalidOperationException("Data has been Appended to a DataSeries which is unsorted in the X-Direction. Unsorted data can have severe performance implications in Ultrachart.\r\nFor maximum performance, please double-check that you are only inserting sorted data to Ultrachart. Alternatively, to disable this warning and allow unsorted data, please set DataSeries.AcceptsUnsortedData = true. For more info see Performance Tips and Tricks at http://support.ultrachart.com/index.php?/Knowledgebase/Article/View/17227/36/performance-tips-and-tricks");
        break;
      }
      num4 = num6;
      if (this.\u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl())
      {
        if (flag)
        {
          double num8 = num7 - num5;
          if (num8 < 0.0)
            num8 = -num8;
          if (num8 > znxnJcVnyOdRqEfYqdQ)
            this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
        }
        else
        {
          num5 = num7;
          flag = true;
        }
      }
    }
    this.\u0023\u003DzECK7c5Gz4mSc = num4;
    this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D = flag;
    this.\u0023\u003DzUvUfmcjO8Kin = num5;
    this.\u0023\u003DzECK7c5Gz4mSc = \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param3[num1 - 1]);
    if (this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D || num1 <= 1)
      return;
    this.\u0023\u003DzUvUfmcjO8Kin = this.\u0023\u003DzECK7c5Gz4mSc - \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D.\u0023\u003DzkUwF72z8\u0024rM9(_param3[num1 - 2]);
    this.\u0023\u003DzJEpy_QVydelYNN6GnA\u003D\u003D = true;
    this.\u0023\u003DznxnJcVNYOdRqEfYQdQ\u003D\u003D = this.\u0023\u003DzUvUfmcjO8Kin * 0.000125;
  }

  private static double \u0023\u003DzkUwF72z8\u0024rM9(Decimal _param0) => (double) _param0;
}
