using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace StockSharp.Xaml
{
    public class LogSourceNode : Disposable
    {
        internal static readonly Guid CoreRootGuid = Guid.NewGuid();
        internal static readonly Guid StrategyRootGuid = Guid.NewGuid();
        private readonly ObservableCollection<LogSourceNode> _internalChildNodes;
        private readonly Guid _keyGuid;
        private readonly string _name;
        private readonly LogSourceNode _parentNode;
        private readonly IEnumerable<LogSourceNode> _childNodes;

        public LogSourceNode( Guid key, string name, LogSourceNode parentNode )
        {
            if ( StringHelper.IsEmpty( name ) )
            {
                name = "-";
            }

            _keyGuid = key;
            _name = name;
            _parentNode = parentNode;
            _internalChildNodes = new ObservableCollection<LogSourceNode>( );
            _childNodes = ( IEnumerable<LogSourceNode> ) _internalChildNodes;
        }

        public Guid Key
        {
            get
            {
                return _keyGuid;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ImageSource Icon
        {
            get
            {
                return ( ImageSource ) IconsExtension.GetImage( GetIconName( ) );
            }
        }

        public LogSourceNode ParentNode
        {
            get
            {
                return _parentNode;
            }
        }

        public IEnumerable<LogSourceNode> ChildNodes
        {
            get
            {
                return _childNodes;
            }
        }

        public void Add( LogSourceNode node )
        {
            if ( node == null )
            {
                throw new ArgumentNullException( nameof( node ) );
            }

            _internalChildNodes.Add( node );
        }

        public void Remove( LogSourceNode node )
        {
            if ( node == null )
            {
                throw new ArgumentNullException( nameof( node ) );
            }

            _internalChildNodes.Remove( node );
        }

        public override string ToString( )
        {
            return Name;
        }

        private string GetIconName( )
        {
            if ( Key == LogSourceNode.CoreRootGuid )
            {
                return "Screen";
            }

            int num = Key == LogSourceNode.StrategyRootGuid ? 1 : 0;
            return "OpenFolder";
        }
    }
}
