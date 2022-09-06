
using System;
using System.Diagnostics;

namespace StockSharp.Diagram
{
    /// <summary>Strategy type info.</summary>
    public class StrategyType
    {
        
        private string _name;
        
        private string _description;
        
        private Type _type;
        
        private string _typeName;

        /// <summary>Name.</summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>Description.</summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        /// <summary>Type.</summary>
        public Type Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <summary>Type name.</summary>
        public string TypeName
        {
            get
            {
                return _typeName;
            }
            set
            {
                _typeName = value;
            }
        }
    }
}
