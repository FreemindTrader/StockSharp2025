﻿//
// THIS FILE IS AUTOGENERATED - DO NOT EDIT
// In order to make changes make sure to edit the t4 template file (*.tt)
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace fx.Indicators
{
    public static partial class EnumerableStats
    {

        /// <summary>
        /// Computes the MinMax of a sequence of nullable int values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<int>? MinMax(this IEnumerable<int?> source)
        {
            IEnumerable<int> values = source.AllValues();
            if (values.Any())
                return values.MinMax();

            return null;
        }

        /// <summary>
        /// Computes the MinMax of a sequence of int values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<int> MinMax(this IEnumerable<int> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // initialize minimum to max possible value and maximum to minimum possible value
            // so that the first comparisons in the aggregate function work as expected
            var minMax = new Range<int>(int.MaxValue, int.MinValue, true);

            bool any = false;
            var result = source.Aggregate( minMax, (accumulator, value) =>
            {
                var min = Math.Min(accumulator.Min, value);
                var max = Math.Max(accumulator.Max, value);
                any = true;

                return new Range<int>(min, max);
            });

            if (any)
                return result;

            throw new InvalidOperationException("source sequence contains no elements");
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of nullable int values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<int>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of int values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<int> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        /// Computes the MinMax of a sequence of nullable long values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<long>? MinMax(this IEnumerable<long?> source)
        {
            IEnumerable<long> values = source.AllValues();
            if (values.Any())
                return values.MinMax();

            return null;
        }

        /// <summary>
        /// Computes the MinMax of a sequence of long values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<long> MinMax(this IEnumerable<long> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // initialize minimum to max possible value and maximum to minimum possible value
            // so that the first comparisons in the aggregate function work as expected
            var minMax = new Range<long>(long.MaxValue, long.MinValue, true);

            bool any = false;
            var result = source.Aggregate( minMax, (accumulator, value) =>
            {
                var min = Math.Min(accumulator.Min, value);
                var max = Math.Max(accumulator.Max, value);
                any = true;

                return new Range<long>(min, max);
            });

            if (any)
                return result;

            throw new InvalidOperationException("source sequence contains no elements");
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of nullable long values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<long>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of long values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<long> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        /// Computes the MinMax of a sequence of nullable float values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<float>? MinMax(this IEnumerable<float?> source)
        {
            IEnumerable<float> values = source.AllValues();
            if (values.Any())
                return values.MinMax();

            return null;
        }

        /// <summary>
        /// Computes the MinMax of a sequence of float values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<float> MinMax(this IEnumerable<float> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // initialize minimum to max possible value and maximum to minimum possible value
            // so that the first comparisons in the aggregate function work as expected
            var minMax = new Range<float>(float.MaxValue, float.MinValue, true);

            bool any = false;
            var result = source.Aggregate( minMax, (accumulator, value) =>
            {
                var min = Math.Min(accumulator.Min, value);
                var max = Math.Max(accumulator.Max, value);
                any = true;

                return new Range<float>(min, max);
            });

            if (any)
                return result;

            throw new InvalidOperationException("source sequence contains no elements");
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of nullable float values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<float>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of float values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<float> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        /// Computes the MinMax of a sequence of nullable double values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<double>? MinMax(this IEnumerable<double?> source)
        {
            IEnumerable<double> values = source.AllValues();
            if (values.Any())
                return values.MinMax();

            return null;
        }

        /// <summary>
        /// Computes the MinMax of a sequence of double values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<double> MinMax(this IEnumerable<double> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // initialize minimum to max possible value and maximum to minimum possible value
            // so that the first comparisons in the aggregate function work as expected
            var minMax = new Range<double>(double.MaxValue, double.MinValue, true);

            bool any = false;
            var result = source.Aggregate( minMax, (accumulator, value) =>
            {
                var min = Math.Min(accumulator.Min, value);
                var max = Math.Max(accumulator.Max, value);
                any = true;

                return new Range<double>(min, max);
            });

            if (any)
                return result;

            throw new InvalidOperationException("source sequence contains no elements");
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of nullable double values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<double>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of double values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<double> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        /// Computes the MinMax of a sequence of nullable decimal values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<decimal>? MinMax(this IEnumerable<decimal?> source)
        {
            IEnumerable<decimal> values = source.AllValues();
            if (values.Any())
                return values.MinMax();

            return null;
        }

        /// <summary>
        /// Computes the MinMax of a sequence of decimal values.
        /// </summary>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The MinMax.</returns>
        public static Range<decimal> MinMax(this IEnumerable<decimal> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // initialize minimum to max possible value and maximum to minimum possible value
            // so that the first comparisons in the aggregate function work as expected
            var minMax = new Range<decimal>(decimal.MaxValue, decimal.MinValue, true);

            bool any = false;
            var result = source.Aggregate( minMax, (accumulator, value) =>
            {
                var min = Math.Min(accumulator.Min, value);
                var max = Math.Max(accumulator.Max, value);
                any = true;

                return new Range<decimal>(min, max);
            });

            if (any)
                return result;

            throw new InvalidOperationException("source sequence contains no elements");
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of nullable decimal values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<decimal>? MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }

        /// <summary>
        ///     Computes the MinMax of a sequence of decimal values that are obtained
        ///     by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The MinMax.</returns>
        public static Range<decimal> MinMax<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.Select(selector).MinMax();
        }
    }
}