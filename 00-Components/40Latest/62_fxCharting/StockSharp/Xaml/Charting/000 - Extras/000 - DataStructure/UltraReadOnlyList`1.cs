using System;
using System.Collections;
using System.Collections.Generic;

namespace StockSharp.Xaml.Charting;

internal class UltraReadOnlyList<T> : IUltraReadOnlyList<T>, IUltraList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
{
    private readonly UltraList<T> _parent;

    public UltraReadOnlyList(UltraList<T> parent)
    {
        this._parent = parent;
    }

    public UltraReadOnlyList(T[ ] arr)
    {
        this._parent = UltraList<T>.ForArray(arr);
    }

    private void ThrowReadOnly()
    {
        throw new InvalidOperationException("this list is read-only");
    }

    public T[ ] ItemsArray
    {
        get
        {
            return this._parent.ItemsArray;
        }
    }

    internal int Capacity
    {
        get
        {
            return this._parent.Capacity;
        }
        set
        {
            this.ThrowReadOnly();
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
            return ((ICollection) this._parent).SyncRoot;
        }
    }

    object IList.this[int index]
    {
        get
        {
            return (object) this._parent[index];
        }
        set
        {
            this.ThrowReadOnly();
        }
    }

    int IList.Add(object item)
    {
        this.ThrowReadOnly();
        return 0;
    }

    bool IList.Contains(object item)
    {
        return ((IList) this._parent).Contains(item);
    }

    void ICollection.CopyTo(Array array, int arrayIndex)
    {
        ((ICollection) this._parent).CopyTo(array, arrayIndex);
    }

    int IList.IndexOf(object item)
    {
        return ((IList) this._parent).IndexOf(item);
    }

    void IList.Insert(int index, object item)
    {
        this.ThrowReadOnly();
    }

    void IList.Remove(object item)
    {
        this.ThrowReadOnly();
    }

    public int Count
    {
        get
        {
            return this._parent.Count;
        }
        internal set
        {
            this.ThrowReadOnly();
        }
    }

    bool ICollection<T>.IsReadOnly
    {
        get
        {
            return true;
        }
    }

    public bool HasValues => throw new NotImplementedException();

    public T this[int index]
    {
        get
        {
            return this._parent[index];
        }
        set
        {
            this.ThrowReadOnly();
        }
    }

    public void Add(T item)
    {
        this.ThrowReadOnly();
    }

    public void Clear()
    {
        this.ThrowReadOnly();
    }

    public bool Contains(T item)
    {
        return this._parent.Contains(item);
    }

    public void CopyTo(T[ ] array, int arrayIndex)
    {
        this._parent.CopyTo(array, arrayIndex);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return (IEnumerator<T>) this._parent.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator) this._parent.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return this._parent.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        this.ThrowReadOnly();
    }

    public bool Remove(T item)
    {
        this.ThrowReadOnly();
        return false;
    }

    public void RemoveAt(int index)
    {
        this.ThrowReadOnly();
    }

    public T GetMaximum()
    {
        return this._parent.GetMaximum();
    }

    public T GetMinimum()
    {
        return this._parent.GetMinimum();
    }

    public void AddRange(IEnumerable<T> collection)
    {
        this.ThrowReadOnly();
    }

    public void CopyTo(T[ ] array)
    {
        this._parent.CopyTo(array);
    }

    public void CopyTo(int index, T[ ] array, int arrayIndex, int count)
    {
        this._parent.CopyTo(index, array, arrayIndex, count);
    }

    public bool EnsureMinSize(int minSize)
    {
        this.ThrowReadOnly();
        return false;
    }

    public int IndexOf(T item, int index)
    {
        return this._parent.IndexOf(item, index);
    }

    public int IndexOf(T item, int index, int count)
    {
        return this._parent.IndexOf(item, index, count);
    }

    public void InsertRange(int index, IEnumerable<T> collection)
    {
        this.ThrowReadOnly();
    }

    public int LastIndexOf(T item)
    {
        return this._parent.LastIndexOf(item);
    }

    public int LastIndexOf(T item, int index)
    {
        return this._parent.LastIndexOf(item, index);
    }

    public int LastIndexOf(T item, int index, int count)
    {
        return this._parent.LastIndexOf(item, index, count);
    }

    public void RemoveRange(int index, int count)
    {
        this.ThrowReadOnly();
    }

    public T[ ] ToArray()
    {
        return this._parent.ToArray();
    }

    public void TrimExcess()
    {
        this.ThrowReadOnly();
    }

    public void SetCount(int setLength)
    {
        this.ThrowReadOnly();
    }

    public void GetMinMax(out T min, out T max)
    {
        throw new NotImplementedException();
    }

    public IList AsList()
    {
        throw new NotImplementedException();
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
