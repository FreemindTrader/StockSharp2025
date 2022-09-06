using System;
using System.Linq;

namespace FreemindAITrade.Helpers
{
    public static class QuerableExtensions
    {
        public static void Load<T>( this IQueryable<T> q ) { }
    }
}
