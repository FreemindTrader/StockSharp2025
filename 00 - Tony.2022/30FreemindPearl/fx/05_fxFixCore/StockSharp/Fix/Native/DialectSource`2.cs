using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic;

namespace StockSharp.Fix.Native
{
    /// <summary>Dialect source.</summary>
    /// <typeparam name="TInterfaceType">
    /// <see cref="T:StockSharp.Fix.Dialects.IFixDialect" /> or <see cref="T:StockSharp.Fix.Dialects.IFastDialect" />.</typeparam>
    /// <typeparam name="TAsmHolder">Assembly holder type.</typeparam>
    public class DialectSource<TInterfaceType, TAsmHolder> : ItemsSourceBase<Type>
    {
        private static readonly CachedSynchronizedSet<Type> _dialectSourceSet = new CachedSynchronizedSet<Type>();

        static DialectSource()
        {
            foreach (Type type in typeof(TAsmHolder).Assembly.GetTypes())
            {
                if (typeof(TInterfaceType).IsAssignableFrom(type) && !type.IsAbstract && type.IsBrowsable())
                    Add(type);
            }
        }

        /// <summary>Add new dialect.</summary>
        /// <param name="type">Type.</param>
        public static void Add(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("Type is null");
            _dialectSourceSet.Add(type);
        }

        /// <summary>Get values.</summary>
        /// <returns>Values.</returns>
        protected override IEnumerable<Type> GetValues() => _dialectSourceSet.Cache;

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        protected override string GetName(Type value) => value.GetDisplayName(null);

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        protected override string GetDescription(Type value) => value.GetDescription();

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        protected override Uri GetIcon(Type value) => value.GetIconUrl();
    }
}
