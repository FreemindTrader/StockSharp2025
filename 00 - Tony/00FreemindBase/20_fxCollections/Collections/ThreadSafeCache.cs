using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

#pragma warning disable CS0414

namespace fx.Collections
{
    public static class DictionaryHelper
    {
        /// <summary>
        /// This method is used by the dictionary implementations to adapt a given size to a prime number (or at least
        /// to some number that's not easily divided).
        /// </summary>
        public static int AdaptSize( int value )
        {
            if( value <= 31 )
            {
                return 31;
            }

            if( ( value % 2 ) == 0 )
            {
                value--;
            }

            checked
            {
                while( true )
                {
                    value += 2;

                    if( value % 3 == 0 )
                    {
                        continue;
                    }

                    if( value % 5 == 0 )
                    {
                        continue;
                    }

                    if( value % 7 == 0 )
                    {
                        continue;
                    }

                    if( value % 11 == 0 )
                    {
                        continue;
                    }

                    if( value % 13 == 0 )
                    {
                        continue;
                    }

                    if( value % 17 == 0 )
                    {
                        continue;
                    }

                    if( value % 19 == 0 )
                    {
                        continue;
                    }

                    if( value % 23 == 0 )
                    {
                        continue;
                    }

                    if( value % 29 == 0 )
                    {
                        continue;
                    }

                    if( value % 31 == 0 )
                    {
                        continue;
                    }

                    return value;
                }
            }
        }
    }

    public sealed class ThreadSafeCache< TKey, TValue >
    {
        private sealed class DatabarNode
        {
            internal DatabarNode( int hashCode, DatabarNode nextNode, TKey key, TValue value )
            {
                _hashCode = hashCode;
                _nextNode = nextNode;
                _key = key;
                _value = value;
            }

            internal readonly int _hashCode;

            internal DatabarNode _nextNode;

            internal readonly TKey _key;

            internal readonly TValue _value;
        }

        private readonly object _lock = new object( );

        private readonly Func< TKey, TValue > _creator = null!;

        private readonly IEqualityComparer< TKey > _comparer;

        private DatabarNode[ ] _baseNodes;

        private int _count;

        public ThreadSafeCache( ) : this( 31, null ! )
        {
        }

        public ThreadSafeCache( int capacity, IEqualityComparer< TKey > comparer )
        {            

            capacity = DictionaryHelper.AdaptSize( capacity );
            if( comparer == null )
            {
                comparer = EqualityComparer< TKey >.Default;
            }
            
            _comparer = comparer;
            _baseNodes = new DatabarNode[ capacity ];
        }

        public void Clear( )
        {
            if( _count == 0 )
            {
                return;
            }

            lock( _lock )
            {
                if( _count == 0 )
                {
                    return;
                }

                for( int i = 0; i < _baseNodes.Length; i++ )
                {
                    _baseNodes[ i ] = null!;
                }

                _count = 0;
            }
        }

        public TValue GetValue( TKey key )
        {
            if( key == null )
            {
                throw new ArgumentNullException( "key" );
            }

            int hashCode = _comparer.GetHashCode( key ) & int.MaxValue;
            var baseNodes = _baseNodes;
            int bucketIndex = hashCode % baseNodes.Length;

            var node = baseNodes[ bucketIndex ];
            while( node != null )
            {
                if( hashCode == node._hashCode )
                {
                    if( _comparer.Equals( key, node._key ) )
                    {
                        return node._value;
                    }
                }

                node = node._nextNode;
            }

            return default!;
        }

        public void Set( TKey key, TValue value )
        {
            if( key == null )
            {
                throw new ArgumentNullException( "key" );
            }

            int hashCode = _comparer.GetHashCode( key ) & int.MaxValue;
            var newNode = new DatabarNode( hashCode, null!, key, value );

            lock( _lock )
            {
                int bucketIndex = hashCode % _baseNodes.Length;

                DatabarNode node = _baseNodes[ bucketIndex ];
                DatabarNode previousNode = null!;
                while( node != null )
                {
                    if( hashCode == node._hashCode )
                    {
                        if( _comparer.Equals( key, node._key ) )
                        {
                            newNode._nextNode = node._nextNode;

                            if( previousNode != null )
                            {
                                previousNode._nextNode = newNode;
                            }
                            else
                            {
                                _baseNodes[ bucketIndex ] = newNode;
                            }

                            return;
                        }
                    }

                    previousNode = node;
                    node = node._nextNode;
                }

                if( _count >= _baseNodes.Length )
                {
                    _Resize( );
                    bucketIndex = hashCode % _baseNodes.Length;
                }

                newNode._nextNode = _baseNodes[ bucketIndex ];
                _baseNodes[ bucketIndex ] = newNode;
                _count++;
            }
        }

        public bool Remove( TKey key )
        {
            if( key == null )
            {
                throw new ArgumentNullException( "key" );
            }

            int hashCode = _comparer.GetHashCode( key ) & int.MaxValue;
            // We consider that Removes are uncommon and that they will
            // usually find an item to remove, so we do a lock directly avoiding
            // the double search done in the GetOrCreateValue (in that case it is
            // useful because it guarantees the lock-free read when the items are
            // there).
            lock( _lock )
            {
                int bucketIndex = hashCode % _baseNodes.Length;

                DatabarNode previousNode = null!;
                DatabarNode node = _baseNodes[ bucketIndex ];
                while( node != null )
                {
                    if( hashCode == node._hashCode )
                    {
                        if( _comparer.Equals( key, node._key ) )
                        {
                            if( previousNode != null )
                            {
                                previousNode._nextNode = node._nextNode;
                            }
                            else
                            {
                                _baseNodes[ bucketIndex ] = node._nextNode;
                            }

                            _count--;
                            return true;
                        }
                    }

                    previousNode = node;
                    node = node._nextNode;
                }
            }

            return false;
        }

        private void _Resize( )
        {
            int newSize;
            checked
            {
                newSize = DictionaryHelper.AdaptSize( _baseNodes.Length * 2 );
            }

            var newNodes = new DatabarNode[ newSize ];

            foreach( var baseNode in _baseNodes )
            {
                var oldNode = baseNode;
                while( oldNode != null )
                {
                    int hashCode = oldNode._hashCode;
                    int bucketIndex = hashCode % newSize;
                    var newNode = new DatabarNode( hashCode, newNodes[ bucketIndex ], oldNode._key, oldNode._value );
                    newNodes[ bucketIndex ] = newNode;

                    oldNode = oldNode._nextNode;
                }
            }

            _baseNodes = newNodes;
        }
    }

    public sealed class WeakGetOrCreateValueDictionary< TKey, TValue >
        where
        TValue : class
    {
        private sealed class DatabarNode
        {
            internal DatabarNode( int hashCode, DatabarNode nextNode, TKey key, WeakReference weakReference )
            {
                _hashCode = hashCode;
                _nextNode = nextNode;
                _key = key;
                _weakReference = weakReference;
            }

            internal readonly int _hashCode;

            internal DatabarNode _nextNode;

            internal readonly TKey _key;

            internal WeakReference _weakReference;
        }

        private readonly object _lock = new object( );

        private readonly Func< TKey, TValue > _creator;

        private readonly IEqualityComparer< TKey > _comparer;

        private DatabarNode[ ] _baseNodes;

        private int _count;

        public WeakGetOrCreateValueDictionary( Func< TKey, TValue > creator ) : this( creator, 31, null! )
        {
        }

        public WeakGetOrCreateValueDictionary( Func< TKey, TValue > creator, int capacity, IEqualityComparer< TKey > comparer )
        {
            if( creator == null )
            {
                throw new ArgumentNullException( "creator" );
            }

            capacity = DictionaryHelper.AdaptSize( capacity );
            if( comparer == null )
            {
                comparer = EqualityComparer< TKey >.Default;
            }

            _creator = creator;
            _comparer = comparer;
            _baseNodes = new DatabarNode[ capacity ];
        }

        public void Clear( )
        {
            if( _count == 0 )
            {
                return;
            }

            lock( _lock )
            {
                if( _count == 0 )
                {
                    return;
                }

                for( int i = 0; i < _baseNodes.Length; i++ )
                {
                    _baseNodes[ i ] = null!;
                }

                _count = 0;
            }
        }

        public TValue GetOrCreateValue( TKey key )
        {
            if( key == null )
            {
                throw new ArgumentNullException( "key" );
            }

            int hashCode = _comparer.GetHashCode( key ) & int.MaxValue;
            var baseNodes = _baseNodes;
            int bucketIndex = hashCode % baseNodes.Length;

            WeakReference weakReference = null!;
            var node = baseNodes[ bucketIndex ];
            while( node != null )
            {
                if( hashCode == node._hashCode )
                {
                    if( _comparer.Equals( key, node._key ) )
                    {
                        weakReference = node._weakReference;

                        if( weakReference == null )
                        {
                            return null!;
                        }

                        object target = weakReference.Target;
                        if( target == null )
                        {
                            break;
                        }

                        return ( TValue )target;
                    }
                }

                node = node._nextNode;
            }

            lock( _lock )
            {
                bucketIndex = hashCode % _baseNodes.Length;
                node = _baseNodes[ bucketIndex ];
                while( node != null )
                {
                    if( hashCode == node._hashCode )
                    {
                        if( _comparer.Equals( key, node._key ) )
                        {
                            weakReference = node._weakReference;

                            if( weakReference == null )
                            {
                                return null!;
                            }

                            object target = weakReference.Target;
                            if( target == null )
                            {
                                break;
                            }

                            return ( TValue )target;
                        }
                    }

                    node = node._nextNode;
                }

                if( node != null )
                {
                    var result = _creator( key );

                    if( result != null )
                    {
                        weakReference.Target = result;
                    }
                    else
                    {
                        node._weakReference = null!;
                    }

                    return result!;
                }

                if( _count >= _baseNodes.Length )
                {
                    _Resize( );
                    bucketIndex = hashCode % _baseNodes.Length;
                }

                TValue value = _creator( key );
                if( value == null )
                {
                    weakReference = null!;
                }
                else
                {
                    weakReference = new WeakReference( value );
                }

                var newNode = new DatabarNode( hashCode, _baseNodes[ bucketIndex ], key, weakReference );
                _baseNodes[ bucketIndex ] = newNode;
                _count++;
                return value!;
            }
        }

        public void Set( TKey key, TValue value )
        {
            if( key == null )
            {
                throw new ArgumentNullException( "key" );
            }

            int hashCode = _comparer.GetHashCode( key ) & int.MaxValue;
            lock( _lock )
            {
                int bucketIndex = hashCode % _baseNodes.Length;
                var node = _baseNodes[ bucketIndex ];

                while( node != null )
                {
                    if( hashCode == node._hashCode )
                    {
                        if( _comparer.Equals( key, node._key ) )
                        {
                            if( value == null )
                            {
                                node._weakReference = null!;
                                return;
                            }

                            var weakReference = node._weakReference;
                            if( weakReference != null )
                            {
                                weakReference.Target = value;
                            }
                            else
                            {
                                node._weakReference = new WeakReference( value );
                            }

                            return;
                        }
                    }

                    node = node._nextNode;
                }

                if( _count >= _baseNodes.Length )
                {
                    _Resize( );
                    bucketIndex = hashCode % _baseNodes.Length;
                }

                WeakReference newWeakReference = null!;
                if( value != null )
                {
                    newWeakReference = new WeakReference( value );
                }

                var newNode = new DatabarNode( hashCode, _baseNodes[ bucketIndex ], key, newWeakReference );
                _baseNodes[ bucketIndex ] = newNode;
                _count++;
            }
        }

        public bool Remove( TKey key )
        {
            if( key == null )
            {
                throw new ArgumentNullException( "key" );
            }

            int hashCode = _comparer.GetHashCode( key ) & int.MaxValue;
            lock( _lock )
            {
                int bucketIndex = hashCode % _baseNodes.Length;

                DatabarNode previousNode = null!;
                DatabarNode node = _baseNodes[ bucketIndex ];
                while( node != null )
                {
                    if( hashCode == node._hashCode )
                    {
                        if( _comparer.Equals( key, node._key ) )
                        {
                            // As an optimization if an item is needed for
                            // this key again we check if we have a weakReference.
                            // If we have it is enough to set its target to null.
                            // But, if the weakReference is null (meaning a null
                            // value was set) we must opt to remove the node
                            // as creating an weak-reference will be bad.

                            var weakReference = node._weakReference;
                            if( weakReference != null )
                            {
                                // we don't do a _count-- in this case as the
                                // node will continue to be here and the count
                                // is related to the number of nodes.

                                weakReference.Target = null;
                            }
                            else
                            {
                                // In this case we are really removing a node.
                                _count--;

                                if( previousNode != null )
                                {
                                    previousNode._nextNode = node._nextNode;
                                }
                                else
                                {
                                    _baseNodes[ bucketIndex ] = node._nextNode;
                                }
                            }

                            return true;
                        }
                    }

                    previousNode = node;
                    node = node._nextNode;
                }
            }

            return false;
        }

        private void _Resize( )
        {
            int count = 0;
            foreach( var baseNode in _baseNodes )
            {
                var node = baseNode;
                while( node != null )
                {
                    var weakReference = node._weakReference;

                    if( weakReference == null || weakReference.IsAlive )
                    {
                        count++;
                    }

                    node = node._nextNode;
                }
            }

            _count = count;

            int oldSize = _baseNodes.Length;
            int newSize;
            checked
            {
                newSize = DictionaryHelper.AdaptSize( count * 2 );
            }

            if( newSize > oldSize )
            {
                var newNodes = new DatabarNode[ newSize ];
                foreach( var baseNode in _baseNodes )
                {
                    var oldNode = baseNode;
                    while( oldNode != null )
                    {
                        var weakReference = oldNode._weakReference;
                        if( weakReference == null || weakReference.IsAlive )
                        {
                            int hashCode = oldNode._hashCode;
                            int bucketIndex = hashCode % newSize;
                            var newNode = new DatabarNode( hashCode, newNodes[ bucketIndex ], oldNode._key, oldNode._weakReference );
                            newNodes[ bucketIndex ] = newNode;
                        }

                        oldNode = oldNode._nextNode;
                    }
                }

                _baseNodes = newNodes;
            }
            else
            {
                for( int nodeIndex = 0; nodeIndex < oldSize; nodeIndex++ )
                {
                    DatabarNode previousNode = null!;
                    var node = _baseNodes[ nodeIndex ];
                    while( node != null )
                    {
                        var weakReference = node._weakReference;
                        if( weakReference == null || weakReference.IsAlive )
                        {
                            previousNode = node;
                        }
                        else
                        {
                            if( previousNode == null )
                            {
                                _baseNodes[ nodeIndex ] = node._nextNode;
                            }
                            else
                            {
                                previousNode._nextNode = node._nextNode;
                            }
                        }

                        node = node._nextNode;
                    }
                }
            }
        }
    }

    public static class GCKeepAliver
    {
        private static readonly object _lock = new object( );

        private static int _count;

        private static bool _unloading;

        private sealed class DieAlways
        {
            ~DieAlways( )
            {
                if( _unloading )
                {
                    return;
                }

                GC.ReRegisterForFinalize( this );

                bool lockTaken = false;
                try
                {
                    Monitor.TryEnter( _lock, 100, ref lockTaken );

                    if( lockTaken )
                    {
                        _count = 0;
                        try
                        {
                            int oldSize = _nodes.Length;
                            checked
                            {
                                int newSize = DictionaryHelper.AdaptSize( _count * 2 );

                                if( newSize < oldSize )
                                {
                                    _nodes = new DatabarNode[ newSize ];
                                    return;
                                }
                            }
                        }
                        catch
                        {
                        }

                        for( int i = 0; i < _nodes.Length; i++ )
                        {
                            _nodes[ i ] = null!;
                        }
                    }
                }
                finally
                {
                    if( lockTaken )
                    {
                        Monitor.Exit( _lock );
                    }
                }
            }
        }

        static GCKeepAliver( )
        {
            AppDomain.CurrentDomain.ProcessExit += ( a, b ) => _unloading = true;
            AppDomain.CurrentDomain.DomainUnload += ( a, b ) => _unloading = true;

            new DieAlways( );
        }

        private sealed class DatabarNode
        {
            internal DatabarNode( int hashCode, DatabarNode nextNode, object item )
            {
                _hashCode = hashCode;
                _nextNode = nextNode;
                _item = item;
            }

            internal readonly int _hashCode;

            internal readonly DatabarNode _nextNode;

            internal readonly object _item;
        }

        private static DatabarNode[ ] _nodes = new DatabarNode[ 31 ];

        public static void KeepAlive( object item )
        {
            if( item == null )
            {
                return;
            }

            int hashCode = RuntimeHelpers.GetHashCode( item ) & int.MaxValue;
            var nodes = _nodes;
            int bucketIndex = hashCode % nodes.Length;
            var node = nodes[ bucketIndex ];

            while( node != null )
            {
                // There's no need to check the hashcode first, as
                // this is only comparing references.
                if( node._item == item )
                {
                    return;
                }

                node = node._nextNode;
            }

            lock( _lock )
            {
                bucketIndex = hashCode % _nodes.Length;
                node = _nodes[ bucketIndex ];

                while( node != null )
                {
                    if( node._item == item )
                    {
                        return;
                    }

                    node = node._nextNode;
                }

                if( _count >= _nodes.Length )
                {
                    _Resize( );
                    bucketIndex = hashCode % _nodes.Length;
                }

                var newNode = new DatabarNode( hashCode, _nodes[ bucketIndex ], item );
                _nodes[ bucketIndex ] = newNode;
                _count++;
            }
        }

        private static void _Resize( )
        {
            int newSize;
            checked
            {
                newSize = DictionaryHelper.AdaptSize( _nodes.Length * 2 );
            }

            var newNodes = new DatabarNode[ newSize ];
            foreach( var baseNode in _nodes )
            {
                var oldNode = baseNode;
                while( oldNode != null )
                {
                    int hashCode = oldNode._hashCode;
                    int newBucketIndex = hashCode % newSize;
                    var newNode = new DatabarNode( hashCode, newNodes[ newBucketIndex ], oldNode._item );
                    newNodes[ newBucketIndex ] = newNode;

                    oldNode = oldNode._nextNode;
                }
            }
        }
    }
}
