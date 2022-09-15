// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.LazyHelper
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Reflection;
using System;

namespace Ecng.ComponentModel
{
  public static class LazyHelper
  {
    private static readonly SynchronizedDictionary<object, Delegate> _factories = new SynchronizedDictionary<object, Delegate>();
    private static readonly SynchronizedDictionary<object, object> _states = new SynchronizedDictionary<object, object>();

    public static Lazy<T> Track<T>(this Lazy<T> lazy)
    {
      if (lazy == null)
        throw new ArgumentNullException(nameof (lazy));
      if (lazy.IsValueCreated)
        throw new ArgumentException(nameof (lazy));
      if (OperatingSystemEx.IsFramework)
      {
        LazyHelper._factories.Add((object) lazy, (Delegate) lazy.GetValue<Lazy<T>, VoidType, Func<T>>("m_valueFactory", (VoidType) null));
      }
      else
      {
        LazyHelper._states.Add((object) lazy, lazy.GetValue<Lazy<T>, VoidType, object>("_state", (VoidType) null));
        LazyHelper._factories.Add((object) lazy, (Delegate) lazy.GetValue<Lazy<T>, VoidType, Func<T>>("_factory", (VoidType) null));
      }
      return lazy;
    }

    public static void Reset<T>(this Lazy<T> lazy)
    {
      if (lazy == null)
        throw new ArgumentNullException(nameof (lazy));
      Delegate factory = LazyHelper._factories[(object) lazy];
      if (OperatingSystemEx.IsFramework)
      {
        lazy.SetValue<Lazy<T>, object>("m_boxed", (object) null);
        lazy.SetValue<Lazy<T>, Delegate>("m_valueFactory", factory);
      }
      else
      {
        lazy.SetValue<Lazy<T>, object>("_state", LazyHelper._states[(object) lazy]);
        lazy.SetValue<Lazy<T>, Delegate>("_factory", factory);
      }
    }
  }
}
