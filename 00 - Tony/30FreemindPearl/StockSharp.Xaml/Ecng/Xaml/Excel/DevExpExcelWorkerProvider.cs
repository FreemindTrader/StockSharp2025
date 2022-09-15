// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Excel.DevExpExcelWorkerProvider
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using DevExpress.Export.Xl;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ecng.Xaml.Excel
{
  /// <summary>
  /// Implementation of the <see cref="T:Ecng.Interop.IExcelWorkerProvider" /> works with DevExpress excel processors.
  /// </summary>
  public class DevExpExcelWorkerProvider : IExcelWorkerProvider
  {
    IExcelWorker IExcelWorkerProvider.\u0023\u003DzFYlaNFIaTgemX_LcxALq8nGD_BIf(
      Stream _param1,
      bool _param2)
    {
      return (IExcelWorker) new DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D(_param1);
    }

    IExcelWorker IExcelWorkerProvider.\u0023\u003Dz7bTeWhSsJh8g6vl0fvRy9YVLXK3B(
      Stream _param1)
    {
      return (IExcelWorker) new DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D(_param1);
    }

    private sealed class \u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D : IExcelWorker, IDisposable
    {
      
      private readonly IXlExporter \u0023\u003Dz1WwIblxXCqnrPHj1Iw\u003D\u003D = XlExport.CreateExporter((XlDocumentFormat) 0);
      
      private readonly List<DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D> \u0023\u003DzrhbAgHjqHSGc = new List<DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D>();
      
      private readonly IXlDocument \u0023\u003Dz6HLvFfw\u003D;
      
      private DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D \u0023\u003Dzie\u0024jNgygCzNb;

      public \u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D(Stream _param1)
      {
        this.\u0023\u003Dz6HLvFfw\u003D = this.\u0023\u003Dz1WwIblxXCqnrPHj1Iw\u003D\u003D.CreateDocument(_param1);
      }

      void IDisposable.\u0023\u003DzkQukXWEgNG41rF31Rw\u003D\u003D()
      {
        this.\u0023\u003DzrhbAgHjqHSGc.ForEach(DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.SomeShit.\u0023\u003DzJfrymowbHyB4_NGSgQ\u003D\u003D ?? (DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.SomeShit.\u0023\u003DzJfrymowbHyB4_NGSgQ\u003D\u003D = new Action<DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D>(DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.SomeShit.ShitMethod02.\u0023\u003Dz\u0024ytQdxGeB7UvrEfemX8dNvle\u0024qg4)));
        this.\u0023\u003DzrhbAgHjqHSGc.Clear();
        ((IDisposable) this.\u0023\u003Dz6HLvFfw\u003D).Dispose();
      }

      IExcelWorker IExcelWorker.\u0023\u003DzMLgEgNaYjmmjKKfkOK7jjBeCPiI4<T>(
        int _param1,
        int _param2,
        T _param3)
      {
        this.\u0023\u003Dzie\u0024jNgygCzNb.\u0023\u003DzYGa\u0024kes\u003D<T>(_param1, _param2, _param3);
        return (IExcelWorker) this;
      }

      T IExcelWorker.\u0023\u003DzUVn_EpKyKpjOXYrkUmdlZX_TdFH3<T>(
        int _param1,
        int _param2)
      {
        return this.\u0023\u003Dzie\u0024jNgygCzNb.\u0023\u003DzA2Z2cNo\u003D<T>(_param1, _param2);
      }

      IExcelWorker IExcelWorker.\u0023\u003DzgRmkXpw0asJ7jA7NTzvuTCmc3_xU(
        int _param1,
        Type _param2)
      {
        this.\u0023\u003Dzie\u0024jNgygCzNb.\u0023\u003Dzisvvlt0\u003D[_param1] = new RefPair<Type, string>(_param2, (string) null);
        return (IExcelWorker) this;
      }

      IExcelWorker IExcelWorker.\u0023\u003DzgRmkXpw0asJ7jA7NTzvuTCmc3_xU(
        int _param1,
        string _param2)
      {
        this.\u0023\u003Dzie\u0024jNgygCzNb.\u0023\u003Dzisvvlt0\u003D[_param1] = new RefPair<Type, string>((Type) null, _param2);
        return (IExcelWorker) this;
      }

      IExcelWorker IExcelWorker.\u0023\u003Dz2\u0024eh0Y2qcX\u002465_8AMvTjgrWTJD9f(
        int _param1,
        ComparisonOperator _param2,
        string _param3,
        string _param4,
        string _param5)
      {
        return (IExcelWorker) this;
      }

      IExcelWorker IExcelWorker.\u0023\u003DzRTZlAefZ0wqVHLJZug3zy87NDqfj(
        string _param1)
      {
        this.\u0023\u003Dzie\u0024jNgygCzNb.Name = _param1;
        return (IExcelWorker) this;
      }

      IExcelWorker IExcelWorker.\u0023\u003DzQSFla8i_gHi5CUspcHc5Pfv_zCsg()
      {
        this.\u0023\u003Dzie\u0024jNgygCzNb = new DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D(this);
        this.\u0023\u003DzrhbAgHjqHSGc.Add(this.\u0023\u003Dzie\u0024jNgygCzNb);
        return (IExcelWorker) this;
      }

      bool IExcelWorker.\u0023\u003Dzuodkx0xPT\u0024kjFvMZhsjjqkMSvZ9c(string _param1)
      {
        return this.\u0023\u003DzrhbAgHjqHSGc.Any<DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D>(new Func<DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D, bool>(new DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.LambdaClass023() { \u0023\u003DzgLcry1E\u003D = _param1 }.\u0023\u003Dzx7wQfT42t3WQYZ2Nb\u00248bS8uwlySzsI5sjGiW0RM\u003D));
      }

      IExcelWorker IExcelWorker.\u0023\u003Dz0NaEmTdK\u0024htytn0fyfZEmK_kUAvp(
        string _param1)
      {
        this.\u0023\u003Dzie\u0024jNgygCzNb = this.\u0023\u003DzrhbAgHjqHSGc.First<DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D>(new Func<DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D, bool>(new DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzwGb5y_xuB4EBPkZkFkyxh1E\u003D()
        {
          \u0023\u003DzgLcry1E\u003D = _param1
        }.\u0023\u003DzPsExUas9DHm7k5jjwX_oTZklc_OeJqdmy9\u0024h9Qg\u003D));
        return (IExcelWorker) this;
      }

      int IExcelWorker.\u0023\u003DzcT1DraHbas6MoxD6dnKi7YFlT06j()
      {
        return this.\u0023\u003Dzie\u0024jNgygCzNb.\u0023\u003Dzisvvlt0\u003D.Count;
      }

      int IExcelWorker.\u0023\u003DzAkdfkrkNCNbVO_JK4FWhTPG0F\u0024Qx()
      {
        return this.\u0023\u003Dzie\u0024jNgygCzNb.\u0023\u003DzXbrWjKc\u003D.Count;
      }

      [Serializable]
      private sealed class SomeShit
      {
        public static readonly DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.SomeShit ShitMethod02 = new DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.SomeShit();
        public static Action<DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D> \u0023\u003DzJfrymowbHyB4_NGSgQ\u003D\u003D;

        internal void \u0023\u003Dz\u0024ytQdxGeB7UvrEfemX8dNvle\u0024qg4(
          DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D _param1)
        {
          _param1.Dispose();
        }
      }

      private sealed class LambdaClass023
      {
        public string \u0023\u003DzgLcry1E\u003D;

        internal bool \u0023\u003Dzx7wQfT42t3WQYZ2Nb\u00248bS8uwlySzsI5sjGiW0RM\u003D(
          DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D _param1)
        {
          return _param1.Name.EqualsIgnoreCase(this.\u0023\u003DzgLcry1E\u003D);
        }
      }

      private sealed class \u0023\u003DzWc1rPWc\u003D : IDisposable
      {
        
        private readonly Dictionary<int, SortedDictionary<int, object>> \u0023\u003DzpRl36n8\u003D = new Dictionary<int, SortedDictionary<int, object>>();
        
        public readonly SortedDictionary<int, RefPair<Type, string>> \u0023\u003Dzisvvlt0\u003D = new SortedDictionary<int, RefPair<Type, string>>();
        
        public readonly HashSet<int> \u0023\u003DzXbrWjKc\u003D = new HashSet<int>();
        
        private readonly DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D \u0023\u003Dzb_lIXZc\u003D;
        
        private string \u0023\u003Dzmvkmo2GrGaQ5OAnRLQ\u003D\u003D;

        public \u0023\u003DzWc1rPWc\u003D(
          DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D _param1)
        {
          DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D gjVLertSt5mRecdGw = _param1;
          if (gjVLertSt5mRecdGw == null)
            throw new ArgumentNullException(nameof(2127280352));
          this.\u0023\u003Dzb_lIXZc\u003D = gjVLertSt5mRecdGw;
        }

        public string Name
        {
          get
          {
            return this.\u0023\u003Dzmvkmo2GrGaQ5OAnRLQ\u003D\u003D;
          }
          set
          {
            this.\u0023\u003Dzmvkmo2GrGaQ5OAnRLQ\u003D\u003D = value;
          }
        }

        public void \u0023\u003DzYGa\u0024kes\u003D<T>(int _param1, int _param2, T _param3)
        {
          this.\u0023\u003Dzisvvlt0\u003D.TryAdd<int, RefPair<Type, string>>(_param1, new RefPair<Type, string>());
          this.\u0023\u003DzXbrWjKc\u003D.Add(_param2);
          this.\u0023\u003DzpRl36n8\u003D.SafeAdd<int, SortedDictionary<int, object>>(_param2, DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzSmC2xAGrkNC5WQqVwg\u003D\u003D<T>.\u0023\u003Dz5pHPlllntxm7YXEetA\u003D\u003D ?? (DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzSmC2xAGrkNC5WQqVwg\u003D\u003D<T>.\u0023\u003Dz5pHPlllntxm7YXEetA\u003D\u003D = new Func<int, SortedDictionary<int, object>>(DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzSmC2xAGrkNC5WQqVwg\u003D\u003D<T>.ShitMethod02.\u0023\u003Dz89fezg0edT4nu6Uxtg\u003D\u003D)))[_param1] = (object) _param3;
        }

        public T \u0023\u003DzA2Z2cNo\u003D<T>(int _param1, int _param2)
        {
          return (T) this.\u0023\u003DzpRl36n8\u003D.SafeAdd<int, SortedDictionary<int, object>>(_param2, DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzBZV_jiYSirKxcLy5jg\u003D\u003D<T>.\u0023\u003Dzn_lFV\u0024ekElYR23K9XA\u003D\u003D ?? (DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzBZV_jiYSirKxcLy5jg\u003D\u003D<T>.\u0023\u003Dzn_lFV\u0024ekElYR23K9XA\u003D\u003D = new Func<int, SortedDictionary<int, object>>(DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzBZV_jiYSirKxcLy5jg\u003D\u003D<T>.ShitMethod02.\u0023\u003DzQ8FleEzeMeBqSkJe0fhwHiE\u003D))).TryGetValue<int, object>(_param1);
        }

        public void Dispose()
        {
          using (IXlSheet sheet = this.\u0023\u003Dzb_lIXZc\u003D.\u0023\u003Dz6HLvFfw\u003D.CreateSheet())
          {
            if (!this.Name.IsEmpty())
              sheet.set_Name(this.Name);
            foreach (KeyValuePair<int, RefPair<Type, string>> keyValuePair in this.\u0023\u003Dzisvvlt0\u003D)
            {
              using (IXlColumn column = sheet.CreateColumn(keyValuePair.Key))
              {
                if (!(keyValuePair.Value.First != (Type) null))
                {
                  if (!keyValuePair.Value.Second.IsEmpty())
                  {
                    IXlColumn ixlColumn = column;
                    XlCellFormatting xlCellFormatting = new XlCellFormatting();
                    ((XlFormatting) xlCellFormatting).set_IsDateTimeFormatString(true);
                    ((XlFormatting) xlCellFormatting).set_NetFormatString(keyValuePair.Value.Second);
                    ixlColumn.set_Formatting(xlCellFormatting);
                  }
                }
              }
            }
            foreach (int key in this.\u0023\u003DzXbrWjKc\u003D.OrderBy<int>())
            {
              using (IXlRow row = sheet.CreateRow(key))
              {
                SortedDictionary<int, object> sortedDictionary;
                if (this.\u0023\u003DzpRl36n8\u003D.TryGetValue(key, out sortedDictionary))
                {
                  foreach (KeyValuePair<int, object> keyValuePair in sortedDictionary)
                  {
                    using (IXlCell cell = row.CreateCell(keyValuePair.Key))
                    {
                      if (keyValuePair.Value != null)
                      {
                        object obj1 = keyValuePair.Value;
                        XlVariantValue xlVariantValue1;
                        if (obj1 is bool)
                        {
                          bool flag = (bool) obj1;
                          XlVariantValue xlVariantValue2 = (XlVariantValue) null;
                          ((XlVariantValue) ref xlVariantValue2).set_BooleanValue(flag);
                          xlVariantValue1 = xlVariantValue2;
                        }
                        else
                        {
                          object obj2 = keyValuePair.Value;
                          if (obj2 is DateTime)
                          {
                            DateTime dateTime = (DateTime) obj2;
                            XlVariantValue xlVariantValue2 = (XlVariantValue) null;
                            ((XlVariantValue) ref xlVariantValue2).set_DateTimeValue(dateTime);
                            xlVariantValue1 = xlVariantValue2;
                          }
                          else
                          {
                            object obj3 = keyValuePair.Value;
                            if (obj3 is DateTimeOffset)
                            {
                              DateTimeOffset dateTimeOffset = (DateTimeOffset) obj3;
                              XlVariantValue xlVariantValue2 = (XlVariantValue) null;
                              ((XlVariantValue) ref xlVariantValue2).set_DateTimeValue(dateTimeOffset.DateTime);
                              xlVariantValue1 = xlVariantValue2;
                            }
                            else
                            {
                              string str = keyValuePair.Value as string;
                              if (str != null)
                              {
                                XlVariantValue xlVariantValue2 = (XlVariantValue) null;
                                ((XlVariantValue) ref xlVariantValue2).set_TextValue(str);
                                xlVariantValue1 = xlVariantValue2;
                              }
                              else
                              {
                                if (!keyValuePair.Value.GetType().IsNumeric())
                                  throw new ArgumentOutOfRangeException(keyValuePair.Value?.ToString());
                                XlVariantValue xlVariantValue2 = (XlVariantValue) null;
                                ((XlVariantValue) ref xlVariantValue2).set_NumericValue(keyValuePair.Value.To<double>());
                                xlVariantValue1 = xlVariantValue2;
                              }
                            }
                          }
                        }
                        cell.set_Value(xlVariantValue1);
                      }
                    }
                  }
                }
              }
            }
          }
          this.\u0023\u003Dzisvvlt0\u003D.Clear();
          this.\u0023\u003DzXbrWjKc\u003D.Clear();
          this.\u0023\u003DzpRl36n8\u003D.Clear();
        }

        [Serializable]
        private sealed class \u0023\u003DzBZV_jiYSirKxcLy5jg\u003D\u003D<\u0023\u003DznSahTwA\u003D>
        {
          public static readonly DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzBZV_jiYSirKxcLy5jg\u003D\u003D<\u0023\u003DznSahTwA\u003D> ShitMethod02 = new DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzBZV_jiYSirKxcLy5jg\u003D\u003D<\u0023\u003DznSahTwA\u003D>();
          public static Func<int, SortedDictionary<int, object>> \u0023\u003Dzn_lFV\u0024ekElYR23K9XA\u003D\u003D;

          internal SortedDictionary<int, object> \u0023\u003DzQ8FleEzeMeBqSkJe0fhwHiE\u003D(
            int _param1)
          {
            return new SortedDictionary<int, object>();
          }
        }

        [Serializable]
        private sealed class \u0023\u003DzSmC2xAGrkNC5WQqVwg\u003D\u003D<\u0023\u003DznSahTwA\u003D>
        {
          public static readonly DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzSmC2xAGrkNC5WQqVwg\u003D\u003D<\u0023\u003DznSahTwA\u003D> ShitMethod02 = new DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D.\u0023\u003DzSmC2xAGrkNC5WQqVwg\u003D\u003D<\u0023\u003DznSahTwA\u003D>();
          public static Func<int, SortedDictionary<int, object>> \u0023\u003Dz5pHPlllntxm7YXEetA\u003D\u003D;

          internal SortedDictionary<int, object> \u0023\u003Dz89fezg0edT4nu6Uxtg\u003D\u003D(
            int _param1)
          {
            return new SortedDictionary<int, object>();
          }
        }
      }

      private sealed class \u0023\u003DzwGb5y_xuB4EBPkZkFkyxh1E\u003D
      {
        public string \u0023\u003DzgLcry1E\u003D;

        internal bool \u0023\u003DzPsExUas9DHm7k5jjwX_oTZklc_OeJqdmy9\u0024h9Qg\u003D(
          DevExpExcelWorkerProvider.\u0023\u003DzGJ\u0024vLErtSt5mRECDGw\u003D\u003D.\u0023\u003DzWc1rPWc\u003D _param1)
        {
          return _param1.Name.EqualsIgnoreCase(this.\u0023\u003DzgLcry1E\u003D);
        }
      }
    }
  }
}
