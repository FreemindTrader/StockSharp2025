// Decompiled with JetBrains decompiler
// Type: #=z6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

#nullable enable
internal abstract class \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>
  where T : 
  #nullable disable
  IXmlSerializable
{
  protected string[] \u0023\u003Dz6DunSwc\u003D = Array.Empty<string>();
  protected Dictionary<Type, string[]> \u0023\u003DzU1bOEY1Ldlir6qBx51Rdl_k\u003D;

  public virtual void \u0023\u003Dz7SZ\u0024Lrw\u003D(
    T _param1,
    XmlWriter _param2)
  {
    \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass1234 _someMemebers1234 = new \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass1234();
    _someMemebers1234.\u0023\u003Dz_i6sZDg\u003D = _param1;
    foreach (string str in this.\u0023\u003Dz6DunSwc\u003D)
      _param2.\u0023\u003DzVjDFK7Q\u003D((object) _someMemebers1234.\u0023\u003Dz_i6sZDg\u003D, str);
    if (this.\u0023\u003DzU1bOEY1Ldlir6qBx51Rdl_k\u003D == null)
      return;
    foreach (string str in this.\u0023\u003DzU1bOEY1Ldlir6qBx51Rdl_k\u003D.Where<KeyValuePair<Type, string[]>>(new Func<KeyValuePair<Type, string[]>, bool>(_someMemebers1234.\u0023\u003DztbYFMhd9VetwIFGIeQ\u003D\u003D)).SelectMany<KeyValuePair<Type, string[]>, string>(\u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass34343383.\u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D ?? (\u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass34343383.\u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D = new Func<KeyValuePair<Type, string[]>, IEnumerable<string>>(\u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass34343383.SomeMethond0343.\u0023\u003DzllFLfGFO1_8H9XmntQ\u003D\u003D))))
      _param2.\u0023\u003DzVjDFK7Q\u003D((object) _someMemebers1234.\u0023\u003Dz_i6sZDg\u003D, str);
  }

  public virtual void \u0023\u003Dz4EJs3pc\u003D(
    T _param1,
    XmlReader _param2)
  {
    \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.\u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D u5Svx6MhYdSkOpoa = new \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.\u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D();
    u5Svx6MhYdSkOpoa.\u0023\u003Dz_i6sZDg\u003D = _param1;
    foreach (string str in this.\u0023\u003Dz6DunSwc\u003D)
      _param2.\u0023\u003DzpJLX1I844aD6zcceiA\u003D\u003D((object) u5Svx6MhYdSkOpoa.\u0023\u003Dz_i6sZDg\u003D, str);
    if (this.\u0023\u003DzU1bOEY1Ldlir6qBx51Rdl_k\u003D == null)
      return;
    foreach (string str in this.\u0023\u003DzU1bOEY1Ldlir6qBx51Rdl_k\u003D.Where<KeyValuePair<Type, string[]>>(new Func<KeyValuePair<Type, string[]>, bool>(u5Svx6MhYdSkOpoa.\u0023\u003Dz6QPdyiLfsgITxtjnxQ\u003D\u003D)).SelectMany<KeyValuePair<Type, string[]>, string>(\u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass34343383.\u0023\u003Dz01OeJ1\u0024vz10AkFmm0w\u003D\u003D ?? (\u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass34343383.\u0023\u003Dz01OeJ1\u0024vz10AkFmm0w\u003D\u003D = new Func<KeyValuePair<Type, string[]>, IEnumerable<string>>(\u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass34343383.SomeMethond0343.\u0023\u003Dze\u002495iAM12dgsBeqnXg\u003D\u003D))))
      _param2.\u0023\u003DzpJLX1I844aD6zcceiA\u003D\u003D((object) u5Svx6MhYdSkOpoa.\u0023\u003Dz_i6sZDg\u003D, str);
  }

  public void \u0023\u003DzT642HR8\u003D(
    IEnumerable<T> _param1,
    XmlWriter _param2)
  {
    foreach (T zH9Hnkng in _param1)
    {
      Type type = zH9Hnkng.GetType();
      _param2.WriteStartElement(type.Name);
      _param2.WriteAttributeString("Type", type.\u0023\u003Dzb_Ih6a0\u003D());
      zH9Hnkng.WriteXml(_param2);
      _param2.WriteEndElement();
    }
  }

  public IEnumerable<T> \u0023\u003DztbbHmR4\u003D(XmlReader _param1)
  {
    return (IEnumerable<T>) new \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.\u0023\u003DzETj7TM98ehFq016Wrg\u003D\u003D(-2)
    {
      \u0023\u003DzBqvDnRM8YYte = _param1
    };
  }

  private sealed class \u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D
  {
    public T \u0023\u003Dz_i6sZDg\u003D;

    internal bool \u0023\u003Dz6QPdyiLfsgITxtjnxQ\u003D\u003D(KeyValuePair<Type, string[]> _param1)
    {
      return _param1.Key.IsInstanceOfType((object) this.\u0023\u003Dz_i6sZDg\u003D);
    }
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass34343383 SomeMethond0343 = new \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.SomeClass34343383();
    public static Func<KeyValuePair<Type, string[]>, 
    #nullable enable
    IEnumerable<
    #nullable disable
    string>> \u0023\u003Dz4cfR9WtX1fuK0AziEg\u003D\u003D;
    public static Func<KeyValuePair<Type, string[]>, 
    #nullable enable
    IEnumerable<
    #nullable disable
    string>> \u0023\u003Dz01OeJ1\u0024vz10AkFmm0w\u003D\u003D;

    internal 
    #nullable enable
    IEnumerable<
    #nullable disable
    string> \u0023\u003DzllFLfGFO1_8H9XmntQ\u003D\u003D(KeyValuePair<Type, string[]> _param1)
    {
      return (IEnumerable<string>) _param1.Value;
    }

    internal 
    #nullable enable
    IEnumerable<
    #nullable disable
    string> \u0023\u003Dze\u002495iAM12dgsBeqnXg\u003D\u003D(KeyValuePair<Type, string[]> _param1)
    {
      return (IEnumerable<string>) _param1.Value;
    }
  }

  private sealed class SomeClass1234
  {
    public T \u0023\u003Dz_i6sZDg\u003D;

    internal bool \u0023\u003DztbYFMhd9VetwIFGIeQ\u003D\u003D(KeyValuePair<Type, string[]> _param1)
    {
      return _param1.Key.IsInstanceOfType((object) this.\u0023\u003Dz_i6sZDg\u003D);
    }
  }

  private sealed class \u0023\u003DzETj7TM98ehFq016Wrg\u003D\u003D : 
    IEnumerable<T>,
    IEnumerator<T>,
    IDisposable,
    IEnumerable,
    IEnumerator
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private T \u0023\u003Dzaev1bhaFFIDX;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private XmlReader \u0023\u003DzYKG62\u00244\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public XmlReader \u0023\u003DzBqvDnRM8YYte;

    [DebuggerHidden]
    public \u0023\u003DzETj7TM98ehFq016Wrg\u003D\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
      this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D = Environment.CurrentManagedThreadId;
    }

    [DebuggerHidden]
    void IDisposable.Dispose()
    {
    }

    bool IEnumerator.MoveNext()
    {
      switch (this.\u0023\u003Dz4fzyEZ1SsHYa)
      {
        case 0:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          if (this.\u0023\u003DzYKG62\u00244\u003D.MoveToContent() != XmlNodeType.Element || this.\u0023\u003DzYKG62\u00244\u003D.IsEmptyElement || !this.\u0023\u003DzYKG62\u00244\u003D.Read())
            goto label_6;
          break;
        case 1:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          this.\u0023\u003DzYKG62\u00244\u003D.Read();
          break;
        default:
          return false;
      }
      if (this.\u0023\u003DzYKG62\u00244\u003D.MoveToContent() == XmlNodeType.Element)
      {
        T instance = (T) Activator.CreateInstance(Type.GetType(this.\u0023\u003DzYKG62\u00244\u003D["Type"]));
        instance.ReadXml(this.\u0023\u003DzYKG62\u00244\u003D);
        this.\u0023\u003Dzaev1bhaFFIDX = instance;
        this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
        return true;
      }
label_6:
      return false;
    }

    [DebuggerHidden]
    T IEnumerator<T>.\u0023\u003DzFPFGeOP\u0024gB895G_WoTnUqerbuUD3()
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
    IEnumerator<
    #nullable disable
    T> IEnumerable<T>.\u0023\u003DzeZA2Ff9OcH6BD7PGVCI6viR78aQH()
    {
      \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.\u0023\u003DzETj7TM98ehFq016Wrg\u003D\u003D etj7Tm98ehFq016Wrg;
      if (this.\u0023\u003Dz4fzyEZ1SsHYa == -2 && this.\u0023\u003DzFd7NMxipJEwB36N0OA\u003D\u003D == Environment.CurrentManagedThreadId)
      {
        this.\u0023\u003Dz4fzyEZ1SsHYa = 0;
        etj7Tm98ehFq016Wrg = this;
      }
      else
        etj7Tm98ehFq016Wrg = new \u0023\u003Dz6RuI7w1XIc38iQTeDB5TvWfY_h5S2Y_Vli8ECWL1cMw8VzeOSw\u003D\u003D<T>.\u0023\u003DzETj7TM98ehFq016Wrg\u003D\u003D(0);
      etj7Tm98ehFq016Wrg.\u0023\u003DzYKG62\u00244\u003D = this.\u0023\u003DzBqvDnRM8YYte;
      return (IEnumerator<T>) etj7Tm98ehFq016Wrg;
    }

    [DebuggerHidden]
    #nullable enable
    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      return (IEnumerator) this.\u0023\u003DzeZA2Ff9OcH6BD7PGVCI6viR78aQH();
    }
  }
}
