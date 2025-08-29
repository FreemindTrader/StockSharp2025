using System.Collections;
using System.Collections.Generic;


namespace StockSharp.Xaml.Charting;

/// <summary>
/// An interface for a list that provides additional functionalities such as getting maximum and minimum items, adding and inserting ranges, and managing item count.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IUltraList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
    //
    // Summary:
    //     Gets the internal ItemsArray that this list wraps for direct unchecked access
    //     NOTE: The count of the ItemsArray may differ from the count of the List. Use
    //     the List.Count when iterating
    T[] ItemsArray { get; }

    //
    // Summary:
    //     Gets the maximum in the list
    T GetMaximum();

    //
    // Summary:
    //     Gets the minimum in the list
    T GetMinimum();

    //
    // Summary:
    //     Gets the minimum and maximum in the list
    //
    // Parameters:
    //   min:
    //     The minimum value in the list
    //
    //   max:
    //     The maximum value in the list
    // void GetMinMax(out T min, out T max);

    //
    // Summary:
    //     Adds a range of items to the list
    void AddRange(IEnumerable<T> items);

    //
    // Summary:
    //     Inserts a range of items to the list
    void InsertRange(int index, IEnumerable<T> items);

    //
    // Summary:
    //     Removes a range of items from the list
    void RemoveRange(int index, int count);

    //
    // Summary:
    //     Forces the count of the list, in operations where we know the capacity in advance
    //
    //
    // Parameters:
    //   setLength:
    void SetCount(int setLength);

    //
    // Summary:
    //     Gets a value indicating whether this list has any values.
    // bool HasValues { get; }

    //
    // Summary:
    //     Gets this instance as System.Collections.IList instance
    // IList AsList();

    //
    // Summary:
    //     Gets a value indicating whether this list has NaN values in specified range
    //
    // Parameters:
    //   startIndex:
    //
    //   count:
    // bool ContainsNaN(int startIndex, int count);

    //
    // Summary:
    //     Gets a value indicating whether this list has sorted ascending values in specified
    //     range
    //
    // Parameters:
    //   startIndex:
    //
    //   count:
    // bool IsSortedAscending(int startIndex, int count);

    //
    // Summary:
    //     Gets a value indicating whether this list has evenly spaced values in specified
    //     range
    //
    // Parameters:
    //   startIndex:
    //
    //   count:
    //
    //   epsilon:
    //
    //   spacing:
    // bool IsEvenlySpaced(int startIndex, int count, double epsilon, out double spacing);
}
