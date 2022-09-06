
using System;
using System.Drawing;

namespace StockSharp.Diagram
{
    /// <summary>Node.</summary>
    public interface ICompositionModelNode : ICloneable
    {
        /// <summary>Key.</summary>
        string Key { get; set; }

        /// <summary>
        ///   <see cref="T:StockSharp.Diagram.DiagramElement" />
        /// </summary>
        DiagramElement Element { get; set; }

        /// <summary>Location.</summary>
        PointF Location { get; set; }

        /// <summary>Type id.</summary>
        Guid TypeId { get; set; }

        /// <summary>Figure id.</summary>
        string Figure { get; set; }

        /// <summary>Custom text.</summary>
        string Text { get; set; }
    }
}
