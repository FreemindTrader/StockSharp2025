using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public static class PagedDataExtensions
    {
        public static async Task<IEnumerable<T>> GetPaginatedDataAsync<TService, T>( this TService service,
                                                                                        Func<TService, int, int, CancellationToken, Task<BaseEntitySet<T>>> getPage,
                                                                                        int pageSize = 20,
                                                                                        int count = 2147483647,
                                                                                        int maxIter = 10,
                                                                                        CancellationToken cancellationToken = default( CancellationToken )
                                                                                   )
        {
            List<T> list;
            IEnumerable< T > allData = new List< T >( );

            if ( service == null )
            {
                throw new ArgumentNullException( nameof( service ) );
            }

            if ( getPage == null )
            {
                throw new ArgumentNullException( nameof( getPage ) );
            }

            if ( pageSize <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( pageSize ) );
            }

            if ( count <= 0 )
            {
                throw new ArgumentOutOfRangeException( nameof( count ) );
            }


            list = new List<T>();
            int skip = 0;
            int iter = 0;

            do
            {
                int required = pageSize.Min( count - skip );

                var pageResult = await getPage( service, skip, required, cancellationToken );

                list.AddRange( pageResult.Items );

                if ( pageResult.Items.Length >= required )
                {
                    skip += required;
                    if ( iter++ > maxIter )
                        throw new InvalidOperationException( "iter > maxIter" );
                }
                else
                    break;

                var pagingData = await getPage( service, skip, required, cancellationToken );

                list.AddRange( pagingData.Items );

                if ( pagingData.Items.Length >= required )
                {
                    skip += required;
                    if ( iter++ > maxIter )
                        throw new InvalidOperationException( "iter > maxIter" );
                }
                else
                    break;
            }
            while ( skip < count );
            allData = list;

            return allData;
        }
    }
}

