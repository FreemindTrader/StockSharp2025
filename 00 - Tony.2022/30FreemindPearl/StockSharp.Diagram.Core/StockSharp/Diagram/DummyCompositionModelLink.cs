using System;

namespace StockSharp.Diagram
{
    /// <summary>
    /// Dummy implementation of <see cref="T:StockSharp.Diagram.ICompositionModelLink" />.
    /// </summary>
    public class DummyCompositionModelLink : ICompositionModelLink, ICloneable
    {
        private string _from;

        private string _to;

        private string _toPort;

        private string _fromPort;

        /// <summary>Is reconnecting.</summary>
        public bool IsReconnecting { get; set; }

        /// <summary>Is connected.</summary>
        public bool IsConnected { get; set; }

        /// <inheritdoc />
        public string From
        {
            get
            {
                return this._from;
            }
            set
            {
                this._from = value;
            }
        }

        /// <inheritdoc />
        public string To
        {
            get
            {
                return this._to;
            }
            set
            {
                this._to = value;
            }
        }

        /// <inheritdoc />
        public string ToPort
        {
            get
            {
                return this._toPort;
            }
            set
            {
                this._toPort = value;
            }
        }

        /// <inheritdoc />
        public string FromPort
        {
            get
            {
                return this._fromPort;
            }
            set
            {
                this._fromPort = value;
            }
        }

        object ICloneable.Clone()
        {
            return ( object )new DummyCompositionModelLink() { From = this.From, To = this.To, FromPort = this.FromPort, ToPort = this.ToPort };
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Concat( new string[8] { this.From, nameof( From ), this.FromPort, nameof( FromPort ), this.To, nameof( To ), this.ToPort, nameof( ToPort ) } );
        }
    }
}
