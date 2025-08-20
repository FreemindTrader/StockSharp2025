//// Decompiled with JetBrains decompiler
//// Type: #=zNpTQ6VGNYT7plNgM4mFVSrejKcp$LekFDw1PpSGX__GL
//// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
//// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Security;
//using System.Threading;

//namespace StockSharp.Xaml.Charting;

//public sealed class AbstractList<T> : IUltraList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IList, IEnumerable, ICollection
//{
//    private static readonly T[] _emptyArray = Array.Empty<T>();

//    private T[] _items;

//    private int _index;

//    private object myLock;

//    private int _count;

//    public AbstractList() : this(128/*0x80*/)
//    {
//    }

//    public AbstractList(int index)
//    {
//        this._items  = index >= 0 ? new T[index] : throw new ArgumentOutOfRangeException("capacity");
//    }

//    public AbstractList(IEnumerable<T> myEnumerable)
//    {
//        if ( myEnumerable == null )
//            throw new ArgumentNullException("collection");

//        if ( myEnumerable is ICollection<T> collection )
//        {
//            int count = collection.Count;
//            this._items  = new T[count];
//            collection.CopyTo(this._items, 0);
//            this._index  = count;
//        }
//        else
//        {
//            this._index  = 0;
//            this._items  = new T[128 /*0x80*/];

//            foreach ( T item in myEnumerable )
//                this.Add(item);
//        }
//    }

//    public static AbstractList<T> NewList(
//      T[] _param0)
//    {
//        return new AbstractList<T>()
//        {
//            _items = _param0,
//            _index = _param0.Length
//        }

//    ;
//    }

//    [SpecialName]
//    public T[] ItemsArray
//    {
//        get
//        {
//            return this._items;
//        }
//    }

//    public int Count => this._items.Length;

//    public void Resize(int size)
//    {
//        if ( size < this._index )
//            throw new ArgumentOutOfRangeException("value");
//        if ( size == this._items.Length )
//            return;
//        if ( size > 0 )
//        {
//            T[] destinationArray = new T[size];
//            if ( this._index  > 0 )
//                Array.Copy((Array)this._items, 0, (Array)destinationArray, 0, this._index);

//            this._items  = destinationArray;
//        }
//        else
//            this._items  = AbstractList<T>._emptyArray;
//    }

//    bool IList.IsFixedSize => false;

//    bool IList.IsReadOnly => false;

//    bool ICollection.IsSynchronized => false;

//    object ICollection.SyncRoot
//    {
//        get
//        {
//            if ( this.myLock == null ) Interlocked.CompareExchange<object>(ref this.myLock, new object(), (object)null);
//            return this.myLock;
//        }
//    }

//    object IList.this[int index]
//    {
//        get
//        {
//            return (object)this[index];
//        }
//        set
//        {
//            try
//            {
//                this[index] = (T)value;
//            }
//            catch ( InvalidCastException ex )
//            {
//                throw new ArgumentException(
//                    string.Format("Value type {0} was of the wrong type", (object)value.GetType().Name));
//            }
//        }
//    }

//    int IList.Add(object _param1)

//    {
//        if ( _param1 == null )
//            throw new ArgumentNullException("item");
//        try
//        {
//            this.Add((T)_param1);
//        }
//        catch ( InvalidCastException ex )
//        {
//            throw new ArgumentException($"Value type {_param1.GetType().Name} was of the wrong type");
//        }
//        return this.Count - 1;
//    }

//    [SecuritySafeCritical]
//    bool IList.Contains(object _param1)

//    {
//        return AbstractList<T>.\u0023\u003DzHPu9XX3E93EL(_param1) && this.Contains((T)_param1);
//    }

//    void ICollection.CopyTo(Array _param1, int _param2)

//    {
//        if ( _param1 != null && _param1.Rank != 1 )
//            throw new ArgumentException("Array rank not supported", "array.Rank");
//        try
//        {
//            Array.Copy((Array)this._items, 0, _param1, _param2, this._index);
//        }
//        catch ( ArrayTypeMismatchException ex )
//        {
//            throw new ArgumentException("Invalid array type");
//        }
//    }

//    int IList.IndexOf(object _param1)

//    {
//        return AbstractList<T>.\u0023\u003DzHPu9XX3E93EL(_param1) ? this.IndexOf((T)_param1) : -1;
//    }

//    void IList.Insert(int _param1, object _param2)

//    {
//        try
//        {
//            this.Insert(_param1, (T)_param2);
//        }
//        catch ( InvalidCastException ex )
//        {
//            throw new ArgumentException($"Value type {_param2.GetType().Name} was of the wrong type");
//        }
//    }

//    [SecuritySafeCritical]
//    void IList.Remove(object _param1)
//    {
//        if ( !AbstractList<T>.\u0023\u003DzHPu9XX3E93EL(_param1))
//            return;
//        this.Remove((T)_param1);
//    }


//    public int Count => this._index;

//    public void \u0023\u003DzpFWgSog\u003D(int _param1)
//  {
//    this._index = _param1;
//}

//  bool ICollection<T>.IsReadOnly

//    {
//        get { return false; }

//    }

//    public T this[int _param1]
//    {
//        get
//        {
//            return _param1 >= this._count ? default(T) : this._items[_param1];
//        }
//        set
//        {
//            if ( _param1 >= this._count )
//                throw new ArgumentOutOfRangeException("index");
//            this._items[_param1] = value;
//            ++this._count;
//        }
//    }

//    public void Add(T _param1)
//    {
//        if ( this._index == this._items.Length )
//            this.\u0023\u003DzflETr9POPm\u0024J(this._index + 1);
//        this._items[this._index++] = _param1;
//        ++this._count;
//    }

//    public void Clear()
//    {
//        if ( this._index > 0 )
//        {
//            Array.Clear((Array)this._items, 0, this._index);
//            this._index = 0;
//        }
//        ++this._count;
//    }

//    public bool Contains(T _param1)
//    {
//        if ( (object)_param1 == null )
//        {
//            for ( int index = 0; index < this._index; ++index )
//            {
//                if ( (object)this._items[index] == null )
//                    return true;
//            }
//            return false;
//        }
//        EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
//        for ( int index = 0; index < this._index; ++index )
//        {
//            if ( equalityComparer.Equals(this._items[index], _param1) )
//                return true;
//        }
//        return false;
//    }

//    public void Clone(T[] _param1, int _param2)
//    {
//        Array.Copy((Array)this._items, 0, (Array)_param1, _param2, this._index);
//    }

//    IEnumerator<T> IEnumerable<T>.\u0023\u003DzeZA2Ff9OcH6BD7PGVCI6viR78aQH()
//      {
//        return (IEnumerator<T>)new AbstractList<T>.\u0023\u003DzdFhhG7w\u003D( this );
//    }

//    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
//      {
//        return (IEnumerator)new AbstractList<T>.\u0023\u003DzdFhhG7w\u003D( this );
//    }

//    public int IndexOf(T _param1)
//    {
//        return Array.IndexOf<T>(this._items, _param1, 0, this._index);
//    }

//    public void Insert(int _param1, T _param2)
//    {
//        if ( _param1 > this._index )
//            throw new ArgumentOutOfRangeException("index");
//        if ( this._index == this._items.Length )
//            this.\u0023\u003DzflETr9POPm\u0024J(this._index + 1);
//        if ( _param1 < this._index )
//            Array.Copy((Array)this._items, _param1, (Array)this._items, _param1 + 1, this._index - _param1);
//        this._items[_param1] = _param2;
//        ++this._index;
//        ++this._count;
//    }

//    public bool Remove(T _param1)
//    {
//        int num = this.IndexOf(_param1);
//        if ( num < 0 )
//            return false;
//        this.RemoveAt(num);
//        return true;
//    }

//    public void RemoveAt(int _param1)
//    {
//        if ( _param1 >= this._index )
//            throw new ArgumentOutOfRangeException("index");
//        --this._index;
//        if ( _param1 < this._index )
//            Array.Copy((Array)this._items, _param1 + 1, (Array)this._items, _param1, this._index - _param1);
//        this._items[this._index] = default(T);
//        ++this._count;
//    }

//private static bool \u0023\u003DzHPu9XX3E93EL(object _param0)
//{
//    if ( _param0 is T )
//        return true;
//    return _param0 == null && (object)default(T) == null;
//}

//public T GetMaximum()
//{
//    return \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dzj_Gd1fY\u003D<T>(this._listArray, 0, this.Count);
//}

//public T GetMinimum()
//{
//    return \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz2wWd_ME\u003D<T>(this._listArray, 0, this.Count);
//}

//public void AddRange(IEnumerable<T> _param1)
//{
//    this.InsertRange(this._index, _param1);
//}

//public IUltraReadOnlyList<T> AsReadOnly()
//{
//    return (IUltraReadOnlyList<T>)new UltraReadOnlyList<T>( this );
//}

//public void Clone(T[] _param1)
//{
//    this.Clone(_param1, 0);
//}

//public void Clone(
//  int _param1,
//  T[] _param2,
//  int _param3,
//  int _param4)
//{
//    if ( this._index - _param1 < _param4 )
//        throw new ArgumentException("Invalid Offset or Length");
//    Array.Copy((Array)this._listArray, _param1, (Array)_param2, _param3, _param4);
//}

//public bool \u0023\u003Dz8_bgWJ3JomKk(int _param1)
//{
//    if ( this.Count >= _param1 )
//        return false;
//    this.\u0023\u003DzflETr9POPm\u0024J(_param1);
//    this.\u0023\u003DzpFWgSog\u003D( _param1 );
//    return true;
//}

//private void \u0023\u003DzflETr9POPm\u0024J(int _param1)
//{
//    if ( this._listArray.Length >= _param1 )
//        return;
//    int num = this._listArray.Length == 0 ? 4 : this._listArray.Length * 2;
//    if ( num < _param1 )
//        num = _param1;
//    this.Resize(num);
//}

//public AbstractList<T>.\u0023\u003DzdFhhG7w\u003D \u0023\u003DzRPOJ5g0\u003D()
//  {
//    return new AbstractList<T>.\u0023\u003DzdFhhG7w\u003D(this);
//}

//public int \u0023\u003DzttdLFAU\u003D(T _param1, int _param2)
//  {
//    if (_param2 > this._index)
//      throw new ArgumentOutOfRangeException("index");
//return Array.IndexOf<T>(this._listArray, _param1, _param2, this._index - _param2);
//}

//public int \u0023\u003DzttdLFAU\u003D(
//  T _param1,
//  int _param2,
//  int _param3)
//  {
//    if (_param2 > this._index)
//      throw new ArgumentOutOfRangeException("index");
//if (_param3< 0 || _param2> this._index - _param3 )
//    throw new ArgumentOutOfRangeException("count");
//return Array.IndexOf<T>(this._listArray, _param1, _param2, _param3);
//}

//public void InsertRange(
//  int _param1,
//  IEnumerable<T> _param2)
//{
//    if ( _param2 == null )
//        throw new ArgumentOutOfRangeException("collection");
//    if ( _param1 > this._index )
//        throw new ArgumentOutOfRangeException("index");
//    switch ( _param2 )
//    {
//        case Array sourceArray2:
//            int length1 = sourceArray2.Length;
//            this.\u0023\u003DzflETr9POPm\u0024J(this._index + length1);
//            Array.Copy((Array)this._listArray, _param1, (Array)this._listArray, _param1 + length1, this._index - _param1);
//            Array.Copy(sourceArray2, 0, (Array)this._listArray, _param1, length1);
//            this._index += length1;
//            break;
//        case IList<T> zH9HnkngList:
//            int count = zH9HnkngList.Count;
//            T[] sourceArray1 = zH9HnkngList.\u0023\u003Dz1bvQV4SZTWpA<T>();
//            this.\u0023\u003DzflETr9POPm\u0024J(this._index + count);
//            Array.Copy((Array)this._listArray, _param1, (Array)this._listArray, _param1 + count, this._index - _param1);
//            T[] zg0gWx4E = this._listArray;
//            int destinationIndex = _param1;
//            int length2 = count;
//            Array.Copy((Array)sourceArray1, 0, (Array)zg0gWx4E, destinationIndex, length2);
//            this._index += count;
//            ++this._count;
//            break;
//        default:
//            using ( IEnumerator<T> enumerator = _param2.GetEnumerator() )
//            {
//                int length3 = this._index - _param1;
//                T[] zH9HnkngArray = new T[length3];
//                Array.Copy((Array)this._listArray, _param1, (Array)zH9HnkngArray, 0, length3);
//                while ( enumerator.MoveNext() )
//                {
//                    this.\u0023\u003DzflETr9POPm\u0024J(this._index + 1);
//                    this._listArray[_param1] = enumerator.Current;
//                    ++_param1;
//                    ++this._index;
//                }
//                Array.Copy((Array)zH9HnkngArray, 0, (Array)this._listArray, _param1, length3);
//            }
//            ++this._count;
//            ++this._count;
//            break;
//    }
//}

//public int \u0023\u003DzLi0LBQXEUVc9(T _param1)
//{
//    return this._index == 0 ? -1 : this.\u0023\u003DzLi0LBQXEUVc9(_param1, this._index - 1, this._index);
//}

//public int \u0023\u003DzLi0LBQXEUVc9(T _param1, int _param2)
//{
//    return _param2 < this._index ? this.\u0023\u003DzLi0LBQXEUVc9(_param1, _param2, _param2 + 1) : throw new ArgumentOutOfRangeException("index");
//}

//public int \u0023\u003DzLi0LBQXEUVc9(
//  T _param1,
//  int _param2,
//  int _param3)
//{
//    if ( this.Count != 0 && _param2 < 0 )
//        throw new ArgumentOutOfRangeException("index");
//    if ( this.Count != 0 && _param3 < 0 )
//        throw new ArgumentOutOfRangeException("Count");
//    if ( this._index == 0 )
//        return -1;
//    if ( _param2 >= this._index )
//        throw new ArgumentOutOfRangeException("index");
//    if ( _param3 > _param2 + 1 )
//        throw new ArgumentOutOfRangeException("Count");
//    return Array.LastIndexOf<T>(this._listArray, _param1, _param2, _param3);
//}

//public void RemoveRange(int _param1, int _param2)
//{
//    if ( _param1 < 0 )
//        throw new ArgumentOutOfRangeException("index");
//    if ( _param2 < 0 )
//        throw new ArgumentOutOfRangeException("count");
//    if ( this._index - _param1 < _param2 )
//        throw new ArgumentOutOfRangeException("count");
//    if ( _param2 <= 0 )
//        return;
//    this._index -= _param2;
//    if ( _param1 < this._index )
//        Array.Copy((Array)this._listArray, _param1 + _param2, (Array)this._listArray, _param1, this._index - _param1);
//    Array.Clear((Array)this._listArray, this._index, _param2);
//    ++this._count;
//}

//public T[] ToArray()
//  {
//    T[] destinationArray = new T[this._index];
//Array.Copy((Array)this._listArray, 0, (Array) destinationArray, 0, this._index);
//return destinationArray;
//}

//public void \u0023\u003DzFqwmAtQ6h18qSpWcIw\u003D\u003D()
//  {
//    if (this._index >= (int) ((double) this._listArray.Length* 0.9))
//      return;
//    this.Resize(this._index);
//}

//public static IList<T> \u0023\u003Dz\u00247WdPho\u003D(
//  List<T> _param0)
//  {
//    return (IList<T>) new AbstractList<T>.\u0023\u003DzcHSqfbQ\u003D(_param0);
//  }

//  public void SetCount(int _param1)
//{
//    this._index = _param1;
//}

//public sealed class \u0023\u003DzcHSqfbQ\u003D : 
//    IList<T>,
//    ICollection<T>,
//    IEnumerable<T>,
//    IEnumerable
//  {


//    private readonly List<T> \u0023\u003Dz5IntIgc\u003D;
    
//    private readonly object \u0023\u003Dz6j0cxXE\u003D;

//    public \u0023\u003DzcHSqfbQ\u003D(List<T> _param1)
//    {
//      this.\u0023\u003Dz5IntIgc\u003D = _param1;
//      this.\u0023\u003Dz6j0cxXE\u003D = ((ICollection) _param1).SyncRoot;
//    }

//    public int Count
//    {
//      get
//      {
//        lock (this.\u0023\u003Dz6j0cxXE\u003D)
//          return this.\u0023\u003Dz5IntIgc\u003D.Count;
//      }
//    }

//    public bool IsReadOnly
//    {
//      get => ((ICollection<T>) this.\u0023\u003Dz5IntIgc\u003D).IsReadOnly;
//    }

//    public T this[int _param1]
//    {
//      get
//      {
//        lock (this.\u0023\u003Dz6j0cxXE\u003D)
//          return this.\u0023\u003Dz5IntIgc\u003D[_param1];
//      }
//      set
//      {
//        lock (this.\u0023\u003Dz6j0cxXE\u003D)
//          this.\u0023\u003Dz5IntIgc\u003D[_param1] = value;
//      }
//    }

//    public void Add(T _param1)
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        this.\u0023\u003Dz5IntIgc\u003D.Add(_param1);
//    }

//    public void Clear()
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        this.\u0023\u003Dz5IntIgc\u003D.Clear();
//    }

//    public bool Contains(T _param1)
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        return this.\u0023\u003Dz5IntIgc\u003D.Contains(_param1);
//    }

//    public void Clone(T[] _param1, int _param2)
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        this.\u0023\u003Dz5IntIgc\u003D.Clone(_param1, _param2);
//    }

//    public bool Remove(T _param1)
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        return this.\u0023\u003Dz5IntIgc\u003D.Remove(_param1);
//    }

//    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        return (IEnumerator) this.\u0023\u003Dz5IntIgc\u003D.GetEnumerator();
//    }

//    IEnumerator<T> IEnumerable<T>.\u0023\u003DzeZA2Ff9OcH6BD7PGVCI6viR78aQH()
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        return ((IEnumerable<T>) this.\u0023\u003Dz5IntIgc\u003D).GetEnumerator();
//    }

//    public int IndexOf(T _param1)
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        return this.\u0023\u003Dz5IntIgc\u003D.IndexOf(_param1);
//    }

//    public void Insert(int _param1, T _param2)
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        this.\u0023\u003Dz5IntIgc\u003D.Insert(_param1, _param2);
//    }

//    public void RemoveAt(int _param1)
//    {
//      lock (this.\u0023\u003Dz6j0cxXE\u003D)
//        this.\u0023\u003Dz5IntIgc\u003D.RemoveAt(_param1);
//    }
//  }

//  public struct \u0023\u003DzdFhhG7w\u003D : 
//    IEnumerator<T>,
//    IDisposable,
//    IEnumerator
//  {
    
//    private readonly AbstractList<T> \u0023\u003Dz5IntIgc\u003D;
    
//    private readonly int _count;
    
//    private T \u0023\u003DzCUQ2vA0\u003D;
    
//    private int _count;

//    public \u0023\u003DzdFhhG7w\u003D(
//      AbstractList<T> _param1)
//    {
//      this.\u0023\u003Dz5IntIgc\u003D = _param1;
//      this._count = 0;
//      this._count = _param1._count;
//      this.\u0023\u003DzCUQ2vA0\u003D = default (T);
//    }

//    public T Current => this.\u0023\u003DzCUQ2vA0\u003D;

//    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
//    {
//      if (this._count == 0 || this._count == this.\u0023\u003Dz5IntIgc\u003D._index + 1)
//        throw new InvalidOperationException("Enumerator Index out of range");
//      return (object) this.Current;
//    }

//    public void Dispose()
//    {
//    }

//    public bool MoveNext()
//    {
//      AbstractList<T> z5IntIgc = this.\u0023\u003Dz5IntIgc\u003D;
//      if (this._count != z5IntIgc._count || this._count >= z5IntIgc._index)
//        return this.\u0023\u003Dz6UGGlohE7E5N();
//      this.\u0023\u003DzCUQ2vA0\u003D = z5IntIgc._listArray[this._count];
//      ++this._count;
//      return true;
//    }

//    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
//    {
//      if (this._count != this.\u0023\u003Dz5IntIgc\u003D._count)
//        throw new InvalidOperationException("Enumerator version is invalid");
//      this._count = 0;
//      this.\u0023\u003DzCUQ2vA0\u003D = default (T);
//    }

//    private bool \u0023\u003Dz6UGGlohE7E5N()
//    {
//      if (this._count != this.\u0023\u003Dz5IntIgc\u003D._count)
//        throw new InvalidOperationException("Enumerator version is invalid");
//      this._count = this.\u0023\u003Dz5IntIgc\u003D._index + 1;
//      this.\u0023\u003DzCUQ2vA0\u003D = default (T);
//      return false;
//    }
//  }
//}
