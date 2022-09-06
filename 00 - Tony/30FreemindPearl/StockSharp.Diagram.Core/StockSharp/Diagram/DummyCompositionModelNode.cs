
using Ecng.Common;
using StockSharp.Localization;
using System;
using System.Diagnostics;
using System.Drawing;

namespace StockSharp.Diagram
{
    /// <summary>
    /// Dummy implementation of <see cref="T:StockSharp.Diagram.ICompositionModelNode" />.
    /// </summary>
    public class DummyCompositionModelNode : ICompositionModelNode, ICloneable
    {
        
        private string _key;
        
        private DiagramElement _element;
        
        private PointF _location;
        
        private Guid _typeId;
        
        private string _figure;
        
        private string _text;

        /// <inheritdoc />
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        /// <inheritdoc />
        public DiagramElement Element
        {
            get
            {
                return _element;
            }
            set
            {
                _element = value;
            }
        }

        /// <inheritdoc />
        public PointF Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        /// <inheritdoc />
        public Guid TypeId
        {
            get
            {
                return _typeId;
            }
            set
            {
                _typeId = value;
            }
        }

        /// <inheritdoc />
        public string Figure
        {
            get
            {
                return _figure;
            }
            set
            {
                _figure = value;
            }
        }

        /// <inheritdoc />
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        object ICloneable.Clone()
        {
            if ( Element == null )
            {
                throw new InvalidOperationException( LocalizedStrings.ElementNotLoaded.Put( Text ) );
            }
                
            return new DummyCompositionModelNode() { Key = Key, Element = Element.Clone( ), Location = Location, TypeId = TypeId, Figure = Figure, Text = Text };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ( ( object )Element )?.ToString() ?? base.ToString();
        }
    }
}
