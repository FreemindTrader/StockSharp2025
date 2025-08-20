using System.Collections;
using System.Collections.Generic;


namespace StockSharp.Xaml.Charting;

/// <summary>
/// An interface for a list that provides additional functionalities such as getting maximum and minimum items, adding and inserting ranges, and managing item count.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IUltraList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
    T GetMaximum();

    T GetMinimum();

    void AddRange(IEnumerable<T> _param1);

    void InsertRange(int _param1, IEnumerable<T> _param2);

    void RemoveRange(int _param1, int _param2);

    T[] ItemsArray
    {
        get;
    }

    void SetCount(int _param1);
}
