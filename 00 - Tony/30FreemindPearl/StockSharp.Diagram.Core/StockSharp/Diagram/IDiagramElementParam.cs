using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StockSharp.Diagram
{
    /// <summary>The diagram element parameter.</summary>
    public interface IDiagramElementParam : IPersistable, INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>Parameter name.</summary>
        string Name { get; set; }

        /// <summary>The displayed name.</summary>
        string DisplayName { get; set; }

        /// <summary>The parameter description.</summary>
        string Description { get; set; }

        /// <summary>Category.</summary>
        string Category { get; set; }

        /// <summary>Parameter type.</summary>
        Type Type { get; }

        /// <summary>Attributes.</summary>
        IList<Attribute> Attributes { get; }

        /// <summary>The parameter value.</summary>
        object Value { get; set; }

        /// <summary>The default value is specified.</summary>
        bool IsDefault { get; }

        /// <summary>The changeable parameter.</summary>
        bool IsParam { get; set; }

        /// <summary>To ignore when saving.</summary>
        bool IgnoreOnSave { get; set; }

        /// <summary>Set value and ignore it on save settings.</summary>
        /// <param name="value">Value.</param>
        void SetValueWithIgnoreOnSave( object value );

        /// <summary>Raise changed event when property is changed.</summary>
        bool NotifyOnChanged { get; set; }
    }
}
