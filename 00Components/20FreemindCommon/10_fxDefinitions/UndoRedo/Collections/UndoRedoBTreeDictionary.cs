using System;
using System.Collections.Generic;
using fx.Definitions.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using fx.Collections;

namespace fx.Definitions.UndoRedo
{
    public class UndoRedoBTreeDictionary<TKey, TValue> : BTreeDictionary<TKey, TValue>, IUndoRedoMember, IChangedNotification
    {
        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.PooledDictionary<TKey,TValue>
        /// class that is empty, has the default initial capacity, and uses the default
        /// equality comparer for the key type.
        /// </summary>
        public UndoRedoBTreeDictionary( )
        {
        }

        /// <summary>
        //     Initializes a new instance of the System.Collections.Generic.PooledDictionary<TKey,TValue>
        //     class that contains elements copied from the specified System.Collections.Generic.IDictionary<TKey,TValue>
        //     and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">
        /// The System.Collections.Generic.IDictionary<TKey,TValue> whose elements are
        /// copied to the new System.Collections.Generic.PooledDictionary<TKey,TValue>.
        /// </param>
        public UndoRedoBTreeDictionary( IDictionary<TKey, TValue> dictionary )
        {
            foreach ( var keyValuePairTKey in dictionary )
            {
                Add( keyValuePairTKey.Key, keyValuePairTKey.Value );
            }
        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.PooledDictionary<TKey,TValue>
        ///     class that is empty, has the default initial capacity, and uses the specified
        ///     System.Collections.Generic.IEqualityComparer<T>.
        /// </summary>
        /// <param name="comparer">
        /// The System.Collections.Generic.IEqualityComparer<T> implementation to use
        /// when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer<T>
        /// for the type of the key.
        /// </param>
        public UndoRedoBTreeDictionary( IComparer<TKey> comparer ) : base( comparer, 128 )
        { }
        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.PooledDictionary<TKey,TValue>
        /// class that is empty, has the specified initial capacity, and uses the default
        /// equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the System.Collections.Generic.PooledDictionary<TKey,TValue> can contain.</param>
        public UndoRedoBTreeDictionary( int capacity ) : base( capacity )
        { }
        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.PooledDictionary<TKey,TValue>
        /// class that contains elements copied from the specified System.Collections.Generic.IDictionary<TKey,TValue>
        /// and uses the specified System.Collections.Generic.IEqualityComparer<T>.
        /// </summary>
        /// <param name="dictionary">The System.Collections.Generic.IDictionary<TKey,TValue> whose elements are copied to the new System.Collections.Generic.PooledDictionary<TKey,TValue>.</param>
        /// <param name="comparer">The System.Collections.Generic.IEqualityComparer<T> implementation to use when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer<T> for the type of the key.</param>
        public UndoRedoBTreeDictionary( IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer ) : base( comparer, 128 )
        {
            foreach ( var keyValuePairTKey in dictionary )
            {
                Add( keyValuePairTKey.Key, keyValuePairTKey.Value );
            }
        }
        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.PooledDictionary<TKey,TValue>
        /// class that is empty, has the specified initial capacity, and uses the specified
        /// System.Collections.Generic.IEqualityComparer<T>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the System.Collections.Generic.PooledDictionary<TKey,TValue> can contain.</param>
        /// <param name="comparer">The System.Collections.Generic.IEqualityComparer<T> implementation to use when comparing keys, or null to use the default System.Collections.Generic.EqualityComparer<T> for the type of the key.</param>
        public UndoRedoBTreeDictionary( int capacity, IComparer<TKey> comparer ) : base( comparer, capacity  )
        { }
        

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a System.Collections.Generic.KeyNotFoundException, and a set operation creates a new element with the specified key.</returns>
        public new TValue this [ TKey key ]
        {
            get { return base [ key ]; }
            set
            {
                if ( key != null )
                {
                    ChangesList changes = Enlist( );

                    if ( changes != null )
                    {
                        if ( !ContainsKey( key ) )
                        {
                            changes.Add(
                                            delegate
                                            {
                                                base [ key ] = value;
                                            },

                                            delegate
                                            {
                                                base.Remove( key );
                                            }
                                       );
                        }
                        else
                        {
                            TValue oldValue = base [ key ];

                            changes.Add(
                                            delegate
                                            {
                                                base [ key ] = value;
                                            },
                                            delegate
                                            {
                                                base [ key ] = oldValue;
                                            }
                                      );
                        }
                    }
                }

                base [ key ] = value;
            }
        }

        /// <summary>Adds the specified key and value to the dictionary.</summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
        public new void Add( TKey key, TValue value )
        {
            if ( key != null && !ContainsKey( key ) )
            {
                ChangesList changes = Enlist( );

                if ( changes != null )
                {
                    changes.Add(
                                    delegate
                                    {
                                        base.Add( key, value );
                                    },

                                    delegate
                                    {
                                        base.Remove( key );
                                    }
                              );
                }

            }
            base.Add( key, value );
        }
        /// <summary>
        /// Removes all keys and values from the System.Collections.Generic.PooledDictionary<TKey,TValue>.
        /// </summary>
        public new void Clear( )
        {
            ChangesList changes = Enlist( );

            if ( changes != null )
            {
                PooledDictionary<TKey, TValue> copy = new PooledDictionary<TKey, TValue>( this );

                changes.Add(
                                delegate
                                {
                                    base.Clear( );
                                },
                                delegate
                                {
                                    foreach ( TKey key in copy.Keys )
                                    {
                                        base.Add( key, copy [ key ] );
                                    }
                                }
                          );
            }
            base.Clear( );
        }

        /// <summary>
        /// Removes the value with the specified key from the System.Collections.Generic.PooledDictionary<TKey,TValue>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if key is not found in the System.Collections.Generic.PooledDictionary<TKey,TValue>.</returns>
        public new bool Remove( TKey key )
        {
            TValue value;

            if ( TryGetValue( key, out value ) )
            {
                ChangesList changes = Enlist( );
                if ( changes != null )
                {
                    changes.Add(
                                    delegate
                                    {
                                        base.Remove( key );
                                    },
                                    delegate
                                    {
                                        base.Add( key, value );
                                    }
                              );
                }


                return base.Remove( key );
            }
            else
                return false;
        }

        delegate void OperationInvoker( );

        class ChangesList : Change<PooledList<OperationInvoker>>
        {
            public ChangesList( )
            {
                NewState = new PooledList<OperationInvoker>( );
                OldState = new PooledList<OperationInvoker>( );
            }
            public void Add( OperationInvoker doChange, OperationInvoker undoChange )
            {
                OldState.Add( undoChange );
                NewState.Add( doChange );
            }
        }

        ChangesList Enlist( )
        {
            UndoRedoArea.AssertCommand( );
            Command command = UndoRedoArea.CurrentArea.CurrentCommand;

            if ( !command.IsEnlisted( this ) )
            {
                ChangesList changes = new ChangesList( );
                command [ this ] = changes;

                return changes;
            }
            else
            {
                return ( ChangesList ) command [ this ];
            }

        }

        #region IUndoRedoMember Members

        void IUndoRedoMember.OnCommit( object change )
        { }

        void IUndoRedoMember.OnUndo( object change )
        {
            ChangesList changesList = ( ChangesList ) change;

            for ( int i = changesList.OldState.Count - 1 ; i >= 0 ; i-- )
            {
                changesList.OldState [ i ]( );
            }

        }

        void IUndoRedoMember.OnRedo( object change )
        {
            ChangesList changesList = ( ChangesList ) change;
            for ( int i = 0 ; i <= changesList.NewState.Count - 1 ; i++ )
                changesList.NewState [ i ]( );
        }

        public object Owner
        {
            get
            {
                return UndoRedoMemberExtender.GetOwner( this );
            }
            set
            {
                UndoRedoMemberExtender.SetOwner( this, value );
            }
        }

        public string Name
        {
            get
            {
                return UndoRedoMemberExtender.GetName( this );
            }
            set
            {
                UndoRedoMemberExtender.SetName( this, value );
            }
        }
        /// <summary>
        /// This event is fired if the dictionary was changed during a command. 
        /// MemberChangedEventArgs.NewValue contains int count of how many times dictionary was changed.
        /// MemberChangedEventArgs.OldValue contains the same value.
        /// </summary>
        public event EventHandler<MemberChangedEventArgs> Changed
        {
            add { UndoRedoMemberExtender.SubscribeChanges( this, value ); }
            remove { UndoRedoMemberExtender.UnsubscribeChanges( this, value ); }
        }

        #endregion

        #region IChangedNotification Members

        void IChangedNotification.OnChanged( CommandDoneType type, IChange change )
        {
            UndoRedoMemberExtender.OnChanged( this, type, ( ( ChangesList ) change ).NewState.Count, ( ( ChangesList ) change ).OldState.Count );
        }

        #endregion
    }
}

