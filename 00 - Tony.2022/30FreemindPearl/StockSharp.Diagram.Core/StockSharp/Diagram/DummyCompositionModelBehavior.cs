using Ecng.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StockSharp.Diagram
{
    /// <summary>
    /// Dummy implementation of <see cref="T:StockSharp.Diagram.ICompositionModelBehavior`2" />.
    /// </summary>
    public class DummyCompositionModelBehavior : ICompositionModelBehavior<DummyCompositionModelNode, DummyCompositionModelLink>, ICloneable
    {

        private IEnumerable<DummyCompositionModelNode> _nodes = new ObservableCollection<DummyCompositionModelNode>();

        private IEnumerable<DummyCompositionModelLink> _links = new ObservableCollection<DummyCompositionModelLink>();

        private ICompositionModel _parent;

        private IUndoManager _undoManager;

        private bool _isUndoManagerSuspended;

        private bool _isModifiable;

        /// <inheritdoc />
        public ICompositionModel Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        /// <inheritdoc />
        public IUndoManager UndoManager
        {
            get
            {
                return _undoManager;
            }
            set
            {
                _undoManager = value;
            }
        }

        /// <inheritdoc />
        public bool IsUndoManagerSuspended
        {
            get
            {
                return _isUndoManagerSuspended;
            }
            set
            {
                _isUndoManagerSuspended = value;
            }
        }

        /// <inheritdoc />
        public bool Modifiable
        {
            get
            {
                return _isModifiable;
            }
            set
            {
                if ( _isModifiable == value )
                    return;
                _isModifiable = value;

                var bc = BehaviorChanged;
                if ( bc == null )
                    return;
                bc( (ModelChange.Property, nameof( Modifiable ), null, !value, null, value, null) );
            }
        }

        /// <inheritdoc />
        public IEnumerable<DummyCompositionModelNode> Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                IEnumerable<DummyCompositionModelNode> oldNodes = _nodes;
                _nodes = value;
                var bc = BehaviorChanged;
                if ( bc == null )
                    return;
                bc( (ModelChange.ChangedNodesSource, null, null, oldNodes, null, value, null) );
            }
        }

        /// <inheritdoc />
        public IEnumerable<DummyCompositionModelLink> Links
        {
            get
            {
                return _links;
            }
            set
            {
                IEnumerable<DummyCompositionModelLink> oldLinks = _links;
                _links = value;
                var bc = BehaviorChanged;
                if ( bc == null )
                    return;

                bc( (ModelChange.ChangedLinksSource, null, null, oldLinks, null, value, null) );
            }
        }

        /// <inheritdoc />

        public event Action<(ModelChange change, object data, string propName, object oldValue, object oldParam, object newValue, object newParam)> BehaviorChanged;

        /// <inheritdoc />
        public DummyCompositionModelNode FindNodeByKey( string key )
        {
            return Nodes.FirstOrDefault<DummyCompositionModelNode>( x => x.Key == key );
        }



        /// <inheritdoc />
        public IEnumerable<DummyCompositionModelLink> GetLinksForNode( DummyCompositionModelNode node )
        {
            return Links.Where<DummyCompositionModelLink>( x =>
            {
                if ( x.From != node.Key )
                    return x.To == node.Key;

                return true;
            } );
        }

        /// <inheritdoc />
        public void RaiseCommited( string name, DummyCompositionModelNode node, IUndoableEdit op )
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public void RaiseLinksRemoved( DummyCompositionModelNode node )
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public void RaiseSocketAdded( DummyCompositionModelNode node )
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public void AddLink( DummyCompositionModelLink link )
        {
            ( ( ICollection<DummyCompositionModelLink> )Links ).Add( link );
            var bc = BehaviorChanged;
            if ( bc == null )
                return;
            bc( (ModelChange.AddedLink, link, null, null, null, null, null) );
        }

        /// <inheritdoc />
        public void RemoveLink( DummyCompositionModelLink link )
        {
            ( ( ICollection<DummyCompositionModelLink> )Links ).Remove( link );
            var bc = BehaviorChanged;
            if ( bc == null )
                return;
            bc( (ModelChange.RemovedLink, link, null, null, null, null, null) );
        }

        /// <inheritdoc />
        public DummyCompositionModelLink AddLink( DummyCompositionModelNode from, string fromPort, DummyCompositionModelNode to, string toPort )
        {
            DummyCompositionModelLink link = new DummyCompositionModelLink() { From = from.Key, FromPort = fromPort, To = to.Key, ToPort = toPort };
            AddLink( link );
            return link;
        }





        /// <inheritdoc />
        public void RemoveLink( DummyCompositionModelNode from, string fromPort, DummyCompositionModelNode to, string toPort )
        {
            var links = ( ( ICollection<DummyCompositionModelLink> )Links )
                        .RemoveWhere( x =>
                        {
                            if ( x.From == from.Key && x.FromPort == fromPort && x.To == to.Key )
                                return x.ToPort == toPort;

                            return false;
                        }
                    );

            foreach ( DummyCompositionModelLink compositionModelLink in links )
            {
                var bc = BehaviorChanged;
                if ( bc != null )
                    bc( (ModelChange.RemovedLink, ( object )compositionModelLink, null, null, null, null, null) );
            }
        }

        /// <inheritdoc />
        public void AddNode( DummyCompositionModelNode node )
        {
            ( ( ICollection<DummyCompositionModelNode> )Nodes ).Add( node );
            var bc = BehaviorChanged;
            if ( bc == null )
                return;
            bc( (ModelChange.AddedNode, node, null, null, null, null, null) );
        }

        /// <inheritdoc />
        public void RemoveNode( DummyCompositionModelNode node )
        {
            ( ( ICollection<DummyCompositionModelNode> )Nodes ).Remove( node );
            var bc = BehaviorChanged;
            if ( bc == null )
                return;
            bc( (ModelChange.RemovedNode, node, null, null, null, null, null) );
        }

        /// <inheritdoc />
        public bool StartTransaction( string name )
        {
            var bc = BehaviorChanged;
            if ( bc != null )
                bc( (ModelChange.StartedTransaction, name, null, null, null, null, null) );
            return true;
        }

        /// <inheritdoc />
        public bool RollbackTransaction()
        {
            var bc = BehaviorChanged;
            if ( bc != null )
                bc( (ModelChange.RolledBackTransaction, null, null, null, null, null, null) );
            return true;
        }

        /// <inheritdoc />
        public bool CommitTransaction( string name )
        {
            var bc = BehaviorChanged;
            if ( bc != null )
                bc( (ModelChange.CommittedTransaction, name, null, null, null, null, null) );
            return true;
        }

        object ICloneable.Clone()
        {
            return new DummyCompositionModelBehavior();
        }
    }


}
