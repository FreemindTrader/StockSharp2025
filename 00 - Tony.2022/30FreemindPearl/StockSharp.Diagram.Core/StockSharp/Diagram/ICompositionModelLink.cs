
using System;

namespace StockSharp.Diagram
{
    /// <summary>Link.</summary>
    public interface ICompositionModelLink : ICloneable
    {
        /// <summary>Is reconnecting.</summary>
        bool IsReconnecting { get; set; }

        /// <summary>Is connected.</summary>
        bool IsConnected { get; set; }

        /// <summary>From node key.</summary>
        string From { get; set; }

        /// <summary>To node key.</summary>
        string To { get; set; }

        /// <summary>To socket key.</summary>
        string ToPort { get; set; }

        /// <summary>From socket key.</summary>
        string FromPort { get; set; }
    }
}
