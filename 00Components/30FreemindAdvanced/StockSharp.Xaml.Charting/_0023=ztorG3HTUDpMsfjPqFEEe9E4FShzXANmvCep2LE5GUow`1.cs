// Decompiled with JetBrains decompiler
// Type: #=ztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal class \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<T> : 
  IDisposable
  where T : new()
{
  
  private readonly object \u0023\u003Dzw9f4RaU\u003D;
  
  private readonly Queue<T> \u0023\u003Dz4FCMJWQ\u003D;
  
  private int \u0023\u003DztUQ677I\u003D;

  public \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown()
  {
    this.\u0023\u003Dzw9f4RaU\u003D = new object();
    this.\u0023\u003Dz4FCMJWQ\u003D = new Queue<T>();
  }

  public \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown(
    int _param1,
    Func<T, T> _param2)
    : this()
  {
    for (int index = 0; index < _param1; ++index)
    {
      T zH9Hnkng = _param2(new T());
      ++this.\u0023\u003DztUQ677I\u003D;
      this.\u0023\u003DzhggR\u00247o\u003D(zH9Hnkng);
    }
  }

  [SpecialName]
  public int \u0023\u003DzlpVGw6E\u003D() => this.\u0023\u003DztUQ677I\u003D;

  [SpecialName]
  public int \u0023\u003Dz8o3wsq506dwJ() => this.\u0023\u003Dz4FCMJWQ\u003D.Count;

  [SpecialName]
  public bool \u0023\u003DzE9zGTWQAFP9k() => this.\u0023\u003Dz4FCMJWQ\u003D.Count == 0;

  public T \u0023\u003Dza7jLYgw\u003D()
  {
    return this.\u0023\u003Dza7jLYgw\u003D(\u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<T>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D ?? (\u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<T>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D = new Func<T, T>(\u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<T>.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzl\u0024kq8n59APwXalx4TQ\u003D\u003D)));
  }

  public T \u0023\u003Dza7jLYgw\u003D(
    Func<T, T> _param1)
  {
    return this.\u0023\u003Dza7jLYgw\u003D(new Func<T>(new \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<T>.\u0023\u003Dz\u0024BXIxgXe6cQmBJ9OJYFFE7I\u003D()
    {
      \u0023\u003DzTeHLJLex4n7B = _param1
    }.\u0023\u003DzI0QPeZE4_5s\u0024));
  }

  public T \u0023\u003Dza7jLYgw\u003D(
    Func<T> _param1)
  {
    lock (this.\u0023\u003Dzw9f4RaU\u003D)
    {
      if (this.\u0023\u003Dz4FCMJWQ\u003D.Count > 0)
        return this.\u0023\u003Dz4FCMJWQ\u003D.Dequeue();
      ++this.\u0023\u003DztUQ677I\u003D;
      return _param1();
    }
  }

  public void \u0023\u003DzhggR\u00247o\u003D(T _param1)
  {
    lock (this.\u0023\u003Dzw9f4RaU\u003D)
      this.\u0023\u003Dz4FCMJWQ\u003D.Enqueue(_param1);
  }

  public void Dispose()
  {
    lock (this.\u0023\u003Dzw9f4RaU\u003D)
    {
      this.\u0023\u003DztUQ677I\u003D = 0;
      while (this.\u0023\u003Dz4FCMJWQ\u003D.Count > 0)
        this.\u0023\u003Dz4FCMJWQ\u003D.Dequeue();
    }
  }

  private sealed class \u0023\u003Dz\u0024BXIxgXe6cQmBJ9OJYFFE7I\u003D
  {
    public Func<T, T> \u0023\u003DzTeHLJLex4n7B;

    internal T \u0023\u003DzI0QPeZE4_5s\u0024()
    {
      return this.\u0023\u003DzTeHLJLex4n7B(new T());
    }
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<T>.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new \u0023\u003DztorG3HTUDpMsfjPqFEEe9E4FShzXANmvCep2LE5GUown<T>.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<T, T> \u0023\u003Dz9026KON3YHnz1feVRw\u003D\u003D;

    internal T \u0023\u003Dzl\u0024kq8n59APwXalx4TQ\u003D\u003D(
      T _param1)
    {
      return _param1;
    }
  }
}
