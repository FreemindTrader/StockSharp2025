using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml.Charting;

internal sealed class UncheckedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
    private T[] array;
    private int baseIndex;
    private int count;

    internal UncheckedList(T[] array, int baseIndex, int count)
    {
        this.array = array;
        this.baseIndex = baseIndex;
        this.count = count;
    }

    internal UncheckedList(T[] array)
    {
        this.array = array;
        this.count = array.Length;
    }

    internal T[] Array
    {
        get
        {
            return this.array;
        }
    }

    internal int BaseIndex
    {
        get
        {
            return this.baseIndex;
        }
    }

    public int Count
    {
        get
        {
            return this.count;
        }
    }

    public int IndexOf(T item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, T item)
    {
        throw new NotSupportedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotSupportedException();
    }

    public T this[int index]
    {
        get
        {
            return this.Array[index + this.BaseIndex];
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public void Add(T item)
    {
        throw new NotSupportedException();
    }

    public void Clear()
    {
        throw new NotSupportedException();
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool IsReadOnly
    {
        get
        {
            return true;
        }
    }

    public bool Remove(T item)
    {
        throw new NotSupportedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
