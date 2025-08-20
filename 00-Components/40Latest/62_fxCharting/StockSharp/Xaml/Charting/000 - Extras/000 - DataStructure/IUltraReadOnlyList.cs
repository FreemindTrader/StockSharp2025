using SciChart.Data.Model;
using System.Collections;
using System.Collections.Generic;


namespace StockSharp.Xaml.Charting;

/// <summary>
/// An interface for a read-only list that extends <see cref="IUltraList{T}"/> and other collection interfaces.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IUltraReadOnlyList<T> : ISciList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
}
