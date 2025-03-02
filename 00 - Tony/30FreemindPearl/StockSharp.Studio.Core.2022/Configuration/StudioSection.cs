// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.StudioSection
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Configuration;
using System.Configuration;

namespace StockSharp.Studio.Core.Configuration
{
    public class StudioSection : StockSharpSection
    {
        private const string _toolControlsKey = "toolControls";
        private const string _strategyControlsKey = "strategyControls";
        private const string _fixServerAddressKey = "fixServerAddress";

        protected StudioSection()
        {
        }

        [ConfigurationProperty( "toolControls", IsDefaultCollection = true )]
        [ConfigurationCollection( typeof( ControlElementCollection ), AddItemName = "control", ClearItemsName = "clear", RemoveItemName = "remove" )]
        public ControlElementCollection ToolControls
        {
            get
            {
                return ( ControlElementCollection )this["toolControls"];
            }
        }

        [ConfigurationProperty( "strategyControls", IsDefaultCollection = true )]
        [ConfigurationCollection( typeof( ControlElementCollection ), AddItemName = "control", ClearItemsName = "clear", RemoveItemName = "remove" )]
        public ControlElementCollection StrategyControls
        {
            get
            {
                return ( ControlElementCollection )this["strategyControls"];
            }
        }

        [ConfigurationProperty( "fixServerAddress", DefaultValue = "stocksharp.com:5001" )]
        public string FixServerAddress
        {
            get
            {
                return ( string )this["fixServerAddress"];
            }
        }
    }
}
