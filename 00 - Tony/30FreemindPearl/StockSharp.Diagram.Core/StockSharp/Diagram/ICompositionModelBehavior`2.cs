
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace StockSharp.Diagram
{
    /// <summary>
    /// <see cref="T:StockSharp.Diagram.ICompositionModel" /> behavior.
    ///     </summary>
    /// <typeparam name="TNode">Node type.</typeparam>
    /// <typeparam name="TLink">Link type.</typeparam>
    public interface ICompositionModelBehavior<TNode, TLink> : ICloneable
      where TNode : ICompositionModelNode
      where TLink : ICompositionModelLink
    {
        /// <summary>Parent.</summary>
        ICompositionModel Parent { get; set; }

        /// <summary>
        ///   <see cref="T:StockSharp.Diagram.IUndoManager" />
        /// </summary>
        IUndoManager UndoManager { get; set; }

        /// <summary>
        /// Undo manager is suspended if this property is set to true.
        /// </summary>
        bool IsUndoManagerSuspended { get; set; }

        /// <summary>Is it possible to edit a composite element diagram.</summary>
        bool Modifiable { get; set; }

        /// <summary>Nodes.</summary>
        IEnumerable<TNode> Nodes { get; set; }

        /// <summary>Links.</summary>
        IEnumerable<TLink> Links { get; set; }

        /// <summary>Find node by key.</summary>
        /// <param name="key">Key.</param>
        /// <returns>
        ///   <typeparamref name="TNode" />
        /// </returns>
        TNode FindNodeByKey( string key );

        /// <summary>Add node.</summary>
        /// <param name="node">
        ///   <typeparamref name="TNode" />
        /// </param>
        void AddNode( TNode node );

        /// <summary>Remove node.</summary>
        /// <param name="node">
        ///   <typeparamref name="TNode" />
        /// </param>
        void RemoveNode( TNode node );

        /// <summary>Add link.</summary>
        /// <param name="link">
        ///   <typeparamref name="TLink" />
        /// </param>
        void AddLink( TLink link );

        /// <summary>Remove link.</summary>
        /// <param name="link">
        ///   <typeparamref name="TLink" />
        /// </param>
        void RemoveLink( TLink link );

        /// <summary>Add link.</summary>
        /// <param name="from">From node.</param>
        /// <param name="fromPort">
        ///   <see cref="P:StockSharp.Diagram.ICompositionModelLink.FromPort" />
        /// </param>
        /// <param name="to">To node.</param>
        /// <param name="toPort">
        ///   <see cref="P:StockSharp.Diagram.ICompositionModelLink.ToPort" />
        /// </param>
        /// <returns>
        ///   <typeparamref name="TLink" />
        /// </returns>
        TLink AddLink( TNode from, string fromPort, TNode to, string toPort );

        /// <summary>Remove link.</summary>
        /// <param name="from">From node.</param>
        /// <param name="fromPort">
        ///   <see cref="P:StockSharp.Diagram.ICompositionModelLink.FromPort" />
        /// </param>
        /// <param name="to">To node.</param>
        /// <param name="toPort">
        ///   <see cref="P:StockSharp.Diagram.ICompositionModelLink.ToPort" />
        /// </param>
        void RemoveLink( TNode from, string fromPort, TNode to, string toPort );

        /// <summary>Get all links for the specified node.</summary>
        /// <param name="node">
        ///   <typeparamref name="TNode" />
        /// </param>
        /// <returns>Links.</returns>
        IEnumerable<TLink> GetLinksForNode( TNode node );

        /// <summary>Start transaction.</summary>
        /// <param name="name">Operation name.</param>
        /// <returns>Operation result.</returns>
        bool StartTransaction( string name );

        /// <summary>Commit transaction.</summary>
        /// <param name="name">Operation name.</param>
        /// <returns>Operation result.</returns>
        bool CommitTransaction( string name );

        /// <summary>Rollback transaction.</summary>
        /// <returns>Operation result.</returns>
        bool RollbackTransaction();

        /// <summary>Changed event.</summary>        
        event Action<(ModelChange change, object data, string propName, object oldValue, object oldParam, object newValue, object newParam)> BehaviorChanged;

        /// <summary>Raise socket added event.</summary>
        /// <param name="node">
        ///   <typeparamref name="TNode" />
        /// </param>
        void RaiseSocketAdded( TNode node );

        /// <summary>Raise links removed event.</summary>
        /// <param name="node">
        ///   <typeparamref name="TNode" />
        /// </param>
        void RaiseLinksRemoved( TNode node );

        /// <summary>Raise commited event.</summary>
        /// <param name="name">Opeation name.</param>
        /// <param name="node">
        ///   <typeparamref name="TNode" />
        /// </param>
        /// <param name="op">
        ///   <see cref="T:StockSharp.Diagram.IUndoableEdit" />
        /// </param>
        void RaiseCommited( string name, TNode node, IUndoableEdit op );
    }
}
