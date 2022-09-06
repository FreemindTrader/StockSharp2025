using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using StockSharp.Algo.Storages;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    [Guid( "2110DD49-58CD-4F8A-A8EE-1779471FC71E" )]
    [DisplayNameLoc( "SecuritiesAndConnections" )]
    [VectorIcon( "Sort" )]
    public partial class SecurityMappingPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {
        private readonly ISecurityMappingStorage _storage;

        public SecurityMappingPane()
        {
            InitializeComponent();
            _storage = ConfigManager.GetService<ISecurityMappingStorage>( "server_mapping" );
            MappingPanel.ConnectorsInfo.Add( new ConnectorInfo( typeof( ServerAdapter ) ) );
            MappingPanel.Storage = _storage;
        }

        bool IPane.IsValid
        {
            get
            {
                return true;
            }
        }

        
        [DisplayNameLoc( "DataServer" )]
        private class ServerAdapter : MessageAdapter
        {
            public ServerAdapter( IdGenerator transactionIdGenerator )
              : base( transactionIdGenerator )
            {
            }

            protected override bool OnSendInMessage( Message message )
            {
                throw new NotSupportedException();
            }
        }
    }
}
