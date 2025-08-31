//using SciChart.Data.Model;
//using StockSharp.Xaml.Charting.ATony;
//using System;
//using System.Collections;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;

//namespace StockSharp.Xaml.Charting.ATony
//{
//    internal class ReadOnlySciList<T> : ISciReadOnlyList<T>, ISciList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
//    {
//        private readonly SciList<T> _parent;

//        public ReadOnlySciList( SciList<T> parent )
//        {
//            _parent = parent;
//        }

//        public ReadOnlySciList( T[ ] arr )
//        {
//            var myList = arr.ToList();
//            _parent = new SciList( myList.To );
//        }

//        private void ThrowReadOnly( )
//        {
//            throw new InvalidOperationException( "this list is read-only" );
//        }

//        public T[ ] ItemsArray
//        {
//            get
//            {
//                return _parent.ItemsArray;
//            }
//        }

//        internal int Capacity
//        {
//            get
//            {
//                return _parent.Capacity;
//            }
//            set
//            {
//                ThrowReadOnly( );
//            }
//        }

//        bool IList.IsFixedSize
//        {
//            get
//            {
//                return false;
//            }
//        }

//        bool IList.IsReadOnly
//        {
//            get
//            {
//                return true;
//            }
//        }

//        bool ICollection.IsSynchronized
//        {
//            get
//            {
//                return false;
//            }
//        }

//        object ICollection.SyncRoot
//        {
//            get
//            {
//                return ( ( ICollection )_parent ).SyncRoot;
//            }
//        }

//        object IList.this[ int index ]
//        {
//            get
//            {
//                return ( object )_parent[ index ];
//            }
//            set
//            {
//                ThrowReadOnly( );
//            }
//        }

//        int IList.Add( object item )
//        {
//            ThrowReadOnly( );
//            return 0;
//        }

//        bool IList.Contains( object item )
//        {
//            return ( ( IList )_parent ).Contains( item );
//        }

//        void ICollection.CopyTo( Array array, int arrayIndex )
//        {
//            ( ( ICollection )_parent ).CopyTo( array, arrayIndex );
//        }

//        int IList.IndexOf( object item )
//        {
//            return ( ( IList )_parent ).IndexOf( item );
//        }

//        void IList.Insert( int index, object item )
//        {
//            ThrowReadOnly( );
//        }

//        void IList.Remove( object item )
//        {
//            ThrowReadOnly( );
//        }

//        public int Count
//        {
//            get
//            {
//                return _parent.Count;
//            }
//            internal set
//            {
//                ThrowReadOnly( );
//            }
//        }

//        bool ICollection<T>.IsReadOnly
//        {
//            get
//            {
//                return true;
//            }
//        }

//        public bool HasValues => throw new NotImplementedException( );

//        public T this[ int index ]
//        {
//            get
//            {
//                return _parent[ index ];
//            }
//            set
//            {
//                ThrowReadOnly( );
//            }
//        }

//        public void Add( T item )
//        {
//            ThrowReadOnly( );
//        }

//        public void Clear( )
//        {
//            ThrowReadOnly( );
//        }

//        public bool Contains( T item )
//        {
//            return _parent.Contains( item );
//        }

//        public void CopyTo( T[ ] array, int arrayIndex )
//        {
//            _parent.CopyTo( array, arrayIndex );
//        }

//        IEnumerator<T> IEnumerable<T>.GetEnumerator( )
//        {
//            return ( IEnumerator<T> )_parent.GetEnumerator( );
//        }

//        IEnumerator IEnumerable.GetEnumerator( )
//        {
//            return ( IEnumerator )_parent.GetEnumerator( );
//        }

//        public int IndexOf( T item )
//        {
//            return _parent.IndexOf( item );
//        }

//        public void Insert( int index, T item )
//        {
//            ThrowReadOnly( );
//        }

//        public bool Remove( T item )
//        {
//            ThrowReadOnly( );
//            return false;
//        }

//        public void RemoveAt( int index )
//        {
//            ThrowReadOnly( );
//        }

//        public T GetMaximum( )
//        {
//            return _parent.GetMaximum( );
//        }

//        public T GetMinimum( )
//        {
//            return _parent.GetMinimum( );
//        }

//        public void AddRange( IEnumerable<T> collection )
//        {
//            ThrowReadOnly( );
//        }

//        public void CopyTo( T[ ] array )
//        {
//            _parent.CopyTo( array, 0  );
//        }

//        public void CopyTo( int index, T[ ] array, int arrayIndex, int count )
//        {
//            _parent.CopyTo( index, array, arrayIndex, count );
//        }

//        public bool EnsureMinSize( int minSize )
//        {
//            ThrowReadOnly( );
//            return false;
//        }

//        public int IndexOf( T item, int index )
//        {
//            return _parent.IndexOf( item, index );
//        }

//        public int IndexOf( T item, int index, int count )
//        {
//            return _parent.IndexOf( item, index, count );
//        }

//        public void InsertRange( int index, IEnumerable<T> collection )
//        {
//            ThrowReadOnly( );
//        }

//        public int LastIndexOf( T item )
//        {
//            return _parent.LastIndexOf( item );
//        }

//        public int LastIndexOf( T item, int index )
//        {
//            return _parent.LastIndexOf( item, index );
//        }

//        public int LastIndexOf( T item, int index, int count )
//        {
//            return _parent.LastIndexOf( item, index, count );
//        }

//        public void RemoveRange( int index, int count )
//        {
//            ThrowReadOnly( );
//        }

//        public T[ ] ToArray( )
//        {
//            return _parent.ToArray( );
//        }

//        public void TrimExcess( )
//        {
//            ThrowReadOnly( );
//        }

//        public void SetCount( int setLength )
//        {
//            ThrowReadOnly( );
//        }

//        public void GetMinMax( out T min, out T max )
//        {
//            throw new NotImplementedException( );
//        }

//        public IList AsList( )
//        {
//            throw new NotImplementedException( );
//        }

//        public bool ContainsNaN( int startIndex, int count )
//        {
//            throw new NotImplementedException( );
//        }

//        public bool IsSortedAscending( int startIndex, int count )
//        {
//            throw new NotImplementedException( );
//        }

//        public bool IsEvenlySpaced( int startIndex, int count, double epsilon, out double spacing )
//        {
//            throw new NotImplementedException( );
//        }
//    }
//}
