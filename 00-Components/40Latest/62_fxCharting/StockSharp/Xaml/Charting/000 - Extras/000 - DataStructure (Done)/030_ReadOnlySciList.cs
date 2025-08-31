using SciChart.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace StockSharp.Xaml.Charting;

internal class ReadOnlySciList<T> : IReadOnlySciList<T>, ISciList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection 
{
    private readonly SciList<T> _innerList;

    public ReadOnlySciList(SciList<T> anotherList)
    {
        _innerList = anotherList;
    }

    public ReadOnlySciList(T[ ] arr)
    {
        _innerList = SciList<T>.ForArray(arr);
    }

    private void ThrowReadOnly()
    {
        throw new InvalidOperationException("this list is read-only");
    }

    public T[ ] ItemsArray
    {
        get
        {
            return _innerList.ItemsArray;
        }
    }

    internal int Capacity
    {
        get
        {
            return _innerList.Capacity;
        }
        set
        {
            ThrowReadOnly();
        }
    }

    bool IList.IsFixedSize
    {
        get
        {
            return false;
        }
    }

    bool IList.IsReadOnly
    {
        get
        {
            return true;
        }
    }

    bool ICollection.IsSynchronized
    {
        get
        {
            return false;
        }
    }

    object ICollection.SyncRoot
    {
        get
        {
            return ((ICollection) _innerList).SyncRoot;
        }
    }

    object IList.this[int index]
    {
        get
        {
            return (object) _innerList[index];
        }
        set
        {
            ThrowReadOnly();
        }
    }

    int IList.Add(object item)
    {
        ThrowReadOnly();
        return 0;
    }

    bool IList.Contains(object item)
    {
        return ((IList) _innerList).Contains(item);
    }

    void ICollection.CopyTo(Array array, int arrayIndex)
    {
        ((ICollection) _innerList).CopyTo(array, arrayIndex);
    }

    int IList.IndexOf(object item)
    {
        return ((IList) _innerList).IndexOf(item);
    }

    void IList.Insert(int index, object item)
    {
        ThrowReadOnly();
    }

    void IList.Remove(object item)
    {
        ThrowReadOnly();
    }

    public int Count
    {
        get
        {
            return _innerList.Count;
        }
        internal set
        {
            ThrowReadOnly();
        }
    }

    bool ICollection<T>.IsReadOnly
    {
        get
        {
            return true;
        }
    }

    public bool HasValues => _innerList.Count > 0;

    public T this[int index]
    {
        get
        {
            return _innerList[index];
        }
        set
        {
            ThrowReadOnly();
        }
    }

    public void Add(T item)
    {
        ThrowReadOnly();
    }

    public void Clear()
    {
        ThrowReadOnly();
    }

    public bool Contains(T item)
    {
        return _innerList.Contains(item);
    }

    public void CopyTo(T[ ] array, int arrayIndex)
    {
        _innerList.CopyTo(array, arrayIndex);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return (IEnumerator<T>) _innerList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator) _innerList.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return _innerList.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        ThrowReadOnly();
    }

    public bool Remove(T item)
    {
        ThrowReadOnly();
        return false;
    }

    public void RemoveAt(int index)
    {
        ThrowReadOnly();
    }

    public T GetMaximum()
    {
        return _innerList.GetMaximum();
    }

    public T GetMinimum()
    {
        return _innerList.GetMinimum();
    }

    public void AddRange(IEnumerable<T> collection)
    {
        ThrowReadOnly();
    }

    public void CopyTo(T[ ] array)
    {
        _innerList.CopyTo(array);
    }

    public void CopyTo(int index, T[ ] array, int arrayIndex, int count)
    {
        _innerList.CopyTo(index, array, arrayIndex, count);
    }

    public bool EnsureMinSize(int minSize)
    {
        ThrowReadOnly();
        return false;
    }

    public int IndexOf(T item, int index)
    {
        return _innerList.IndexOf(item, index);
    }

    public int IndexOf(T item, int index, int count)
    {
        return _innerList.IndexOf(item, index, count);
    }

    public void InsertRange(int index, IEnumerable<T> collection)
    {
        ThrowReadOnly();
    }

    public int LastIndexOf(T item)
    {
        return _innerList.LastIndexOf(item);
    }

    public int LastIndexOf(T item, int index)
    {
        return _innerList.LastIndexOf(item, index);
    }

    public int LastIndexOf(T item, int index, int count)
    {
        return _innerList.LastIndexOf(item, index, count);
    }

    public void RemoveRange(int index, int count)
    {
        ThrowReadOnly();
    }

    public T[ ] ToArray()
    {
        return _innerList.ToArray();
    }

    public void TrimExcess()
    {
        ThrowReadOnly();
    }

    public void SetCount(int setLength)
    {
        ThrowReadOnly();
    }

    public void GetMinMax(out T min, out T max)
    {
        min = GetMinimum();
        max = GetMaximum();
    }

    public IList AsList()
    {
        return this;
    }

    public bool ContainsNaN(int startIndex, int count)
    {
        throw new NotImplementedException();
    }

    public bool IsSortedAscending(int startIndex, int count)
    {
        throw new NotImplementedException();
    }

    public bool IsEvenlySpaced(int startIndex, int count, double epsilon, out double spacing)
    {
        throw new NotImplementedException();
    }
}
