// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartBaseViewModel
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>Base class for chart related view models.</summary>
public class ChartBaseViewModel : NotifiableObject
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>> \u0023\u003Dz3B6MdxES0dCL = new SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>();

  /// <summary>Raised before property value is changed.</summary>
  public event Action<object, string, object> PropertyValueChanging;

  private void \u0023\u003DzzFkPYBhFpV7B(string _param1, object _param2)
  {
    Action<object, string, object> zewvKoksnfftN = this.\u0023\u003DzewvKoksnfftN;
    if (zewvKoksnfftN == null)
      return;
    zewvKoksnfftN((object) this, _param1, _param2);
  }

  /// <summary>Set property value and raise events.</summary>
  /// <typeparam name="T">Value type.</typeparam>
  /// <param name="field">Property backing field.</param>
  /// <param name="value">New value.</param>
  /// <param name="propertyName">Name of the property.</param>
  protected bool SetField<T>(ref T field, T value, string propertyName)
  {
    if (EqualityComparer<T>.Default.Equals(field, value))
      return false;
    this.\u0023\u003DzzFkPYBhFpV7B(propertyName, (object) value);
    field = value;
    this.NotifyChanged(propertyName);
    return true;
  }

  /// <summary>
  /// Helper method to raise property change notifications on this object if the event was raised on another object <paramref name="source" />.
  /// </summary>
  /// <param name="source">
  /// </param>
  /// <param name="nameFrom">
  /// </param>
  /// <param name="namesTo">
  /// </param>
  protected void MapPropertyChangeNotification(
    INotifyPropertyChanged source,
    string nameFrom,
    params string[] namesTo)
  {
    ChartBaseViewModel.\u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D skXz9pb7XdulaJda = new ChartBaseViewModel.\u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D();
    skXz9pb7XdulaJda.\u0023\u003DzL2OrHlw\u003D = source;
    skXz9pb7XdulaJda.\u0023\u003DzRRvwDu67s9Rm = this;
    skXz9pb7XdulaJda.\u0023\u003DzBkVhXhc\u003D = nameFrom;
    skXz9pb7XdulaJda.\u0023\u003Dzb5ffYz0\u003D = namesTo;
    string[] zb5ffYz0 = skXz9pb7XdulaJda.\u0023\u003Dzb5ffYz0\u003D;
    if ((zb5ffYz0 != null ? (zb5ffYz0.Length != 0 ? 1 : 0) : 0) == 0)
      skXz9pb7XdulaJda.\u0023\u003Dzb5ffYz0\u003D = new string[1]
      {
        skXz9pb7XdulaJda.\u0023\u003DzBkVhXhc\u003D
      };
    CollectionHelper.SyncDo<SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>>(this.\u0023\u003Dz3B6MdxES0dCL, new Action<SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>>(skXz9pb7XdulaJda.\u0023\u003DzEvCBk4t2uj_E0g7AMwV\u0024814\u003D));
  }

  private void \u0023\u003DzlNMg8s2tEtQY(object _param1, PropertyChangedEventArgs _param2)
  {
    CollectionHelper.SyncDo<SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>>(this.\u0023\u003Dz3B6MdxES0dCL, new Action<SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>>(new ChartBaseViewModel.\u0023\u003DzP\u0024K\u0024cmcad6NXc5A8\u0024A\u003D\u003D()
    {
      \u0023\u003DzwM8aRUE\u003D = _param1,
      \u0023\u003Dz1BK01YA\u003D = _param2,
      \u0023\u003DzRRvwDu67s9Rm = this
    }.\u0023\u003Dz18I1PzXKKBBgiBkdltgDycU\u003D));
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly ChartBaseViewModel.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new ChartBaseViewModel.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<string, HashSet<string>> \u0023\u003Dzz3SuYdPV_Nb4Pu6bTw\u003D\u003D;

    internal HashSet<string> \u0023\u003DzAUCm_y_Pfc5wY\u0024\u0024V8dNAZoc\u003D(string _param1)
    {
      return new HashSet<string>();
    }
  }

  private sealed class \u0023\u003DzP\u0024K\u0024cmcad6NXc5A8\u0024A\u003D\u003D
  {
    public object \u0023\u003DzwM8aRUE\u003D;
    public PropertyChangedEventArgs \u0023\u003Dz1BK01YA\u003D;
    public ChartBaseViewModel \u0023\u003DzRRvwDu67s9Rm;

    internal void \u0023\u003Dz18I1PzXKKBBgiBkdltgDycU\u003D(
      SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>> _param1)
    {
      Dictionary<string, HashSet<string>> dictionary;
      HashSet<string> stringSet;
      if (!_param1.TryGetValue((INotifyPropertyChanged) this.\u0023\u003DzwM8aRUE\u003D, ref dictionary) || !dictionary.TryGetValue(this.\u0023\u003Dz1BK01YA\u003D.PropertyName, out stringSet))
        return;
      foreach (string str in stringSet)
        this.\u0023\u003DzRRvwDu67s9Rm.NotifyChanged(str);
    }
  }

  private sealed class \u0023\u003DzxSk_xz9pb7XDulaJDA\u003D\u003D
  {
    public INotifyPropertyChanged \u0023\u003DzL2OrHlw\u003D;
    public ChartBaseViewModel \u0023\u003DzRRvwDu67s9Rm;
    public string \u0023\u003DzBkVhXhc\u003D;
    public string[] \u0023\u003Dzb5ffYz0\u003D;
    public Func<INotifyPropertyChanged, Dictionary<string, HashSet<string>>> \u0023\u003DzuAeZVTPDgzYE;

    internal void \u0023\u003DzEvCBk4t2uj_E0g7AMwV\u0024814\u003D(
      SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>> _param1)
    {
      CollectionHelper.AddRange<string>((ICollection<string>) CollectionHelper.SafeAdd<string, HashSet<string>>((IDictionary<string, HashSet<string>>) CollectionHelper.SafeAdd<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>((IDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>) _param1, this.\u0023\u003DzL2OrHlw\u003D, this.\u0023\u003DzuAeZVTPDgzYE ?? (this.\u0023\u003DzuAeZVTPDgzYE = new Func<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>(this.\u0023\u003DzBERiR5If4xgsFAdbsBWfr6M\u003D))), this.\u0023\u003DzBkVhXhc\u003D, ChartBaseViewModel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzz3SuYdPV_Nb4Pu6bTw\u003D\u003D ?? (ChartBaseViewModel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dzz3SuYdPV_Nb4Pu6bTw\u003D\u003D = new Func<string, HashSet<string>>(ChartBaseViewModel.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzAUCm_y_Pfc5wY\u0024\u0024V8dNAZoc\u003D))), (IEnumerable<string>) this.\u0023\u003Dzb5ffYz0\u003D);
    }

    internal Dictionary<string, HashSet<string>> \u0023\u003DzBERiR5If4xgsFAdbsBWfr6M\u003D(
      INotifyPropertyChanged _param1)
    {
      _param1.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzlNMg8s2tEtQY);
      return new Dictionary<string, HashSet<string>>();
    }
  }
}
