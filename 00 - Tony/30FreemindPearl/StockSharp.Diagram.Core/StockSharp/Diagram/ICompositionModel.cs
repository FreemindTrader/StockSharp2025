
using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace StockSharp.Diagram
{
    /// <summary>
    /// <see cref="T:StockSharp.Diagram.CompositionDiagramElement" /> model.
    ///     </summary>
    public interface ICompositionModel : ICloneable<ICompositionModel>, ICloneable
    {
        /// <summary>
        ///   <see cref="T:StockSharp.Diagram.ICompositionModelBehavior`2" />
        /// </summary>
        object Behavior { get; }

        /// <summary>To check the composite element for errors in diagram.</summary>
        bool HasErrors { get; }

        /// <summary>Is it possible to edit a composite element diagram.</summary>
        bool Modifiable { get; set; }

        /// <summary>
        ///   <see cref="T:StockSharp.Diagram.IUndoManager" />
        /// </summary>
        IUndoManager UndoManager { get; set; }

        /// <summary>
        /// Undo manager is suspended if this property is set to true.
        /// </summary>
        bool IsUndoManagerSuspended { get; set; }

        /// <summary>Child elements.</summary>
        IEnumerable<DiagramElement> Elements { get; }

        /// <summary>Add element.</summary>
        /// <param name="element">The diagram element.</param>
        /// <param name="location">Element position.</param>
        void AddElement( DiagramElement element, PointF location = default( PointF ) );

        /// <summary>Get connected sockets.</summary>
        /// <param name="element">
        ///   <see cref="T:StockSharp.Diagram.DiagramElement" />
        /// </param>
        /// <param name="socket">
        ///   <see cref="T:StockSharp.Diagram.DiagramSocket" />
        /// </param>
        /// <returns>Connected sockets.</returns>
        IEnumerable<DiagramSocket> GetConnectedSocketsFor( DiagramElement element, DiagramSocket socket );

        /// <summary>Get disconnected sockets.</summary>
        /// <returns>Disconnected sockets.</returns>        
        IEnumerable<(string nodeKey, DiagramSocket socket)> GetDisconnectedSockets();

        /// <summary>Get element unique key.</summary>
        /// <param name="element">
        ///   <see cref="T:StockSharp.Diagram.DiagramElement" />
        /// </param>
        /// <returns>Key.</returns>
        string GetElementKey( DiagramElement element );

        /// <summary>Find element by unique key.</summary>
        /// <param name="key">Key.</param>
        /// <returns>
        ///   <see cref="T:StockSharp.Diagram.DiagramElement" />
        /// </returns>
        DiagramElement FindElementByKey( string key );

        /// <summary>Changed event.</summary>
        event Action ModelChanged;

        /// <summary>Child element added event.</summary>
        event Action<DiagramElement> ElementAdded;

        /// <summary>Child element removed event.</summary>
        event Action<DiagramElement> ElementRemoved;
    }
}
