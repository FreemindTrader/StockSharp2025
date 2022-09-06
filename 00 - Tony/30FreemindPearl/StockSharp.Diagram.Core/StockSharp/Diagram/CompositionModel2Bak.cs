using Ecng.Common;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Diagram
{
    /// <summary>
    /// Default implementation of <see cref="T:StockSharp.Diagram.ICompositionModel" />.
    /// </summary>
    /// <typeparam name="TNode">Node type.</typeparam>
    /// <typeparam name="TLink">Link type.</typeparam>
    public class CompositionModel<TNode, TLink> : ICompositionModel, ICloneable<ICompositionModel>, ICloneable
      where TNode : ICompositionModelNode, new()
      where TLink : ICompositionModelLink, new()
    {

        private readonly ICompositionModelBehavior<TNode, TLink> _compositionModelBehaviors;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.CompositionModel`2" />.
        /// </summary>
        /// <param name="behavior">
        ///   <see cref="T:StockSharp.Diagram.ICompositionModelBehavior`2" />
        /// </param>
        public CompositionModel( ICompositionModelBehavior<TNode, TLink> behavior )
        {
            ICompositionModelBehavior<TNode, TLink> compositionModelBehavior = behavior;
            if ( compositionModelBehavior == null )
                throw new ArgumentNullException( "compositionModelBehavior == null" );

            _compositionModelBehaviors            = compositionModelBehavior;
            _compositionModelBehaviors.Modifiable = true;
            _compositionModelBehaviors.Parent     = this;
            _compositionModelBehaviors.BehaviorChanged += OnBehaviorChanged;
        }

        private void AddedNode( TNode node )
        {
            if ( node.Element == null )
                return;
            DiagramElement element = node.Element;
            element.StartedUndoableOperation += new Action( OnStartedUndoableOperation );
            element.CommittedUndoableOperation += new Action<DiagramElement, IUndoableEdit>( OnCommittedUndoableOperation );
            element.SocketAdded += new Action<DiagramSocket>( this.OnSocketAdded );
            element.SocketRemoved += new Action<DiagramSocket>( this.OnSocketRemoved );
            element.SocketChanged += new Action<DiagramSocket>( this.\u0023\u003DzFZcx6XM24oQh );
            Action<DiagramElement> zwF1Azeo = this.\u0023\u003DzwF1Azeo\u003D;
            if ( zwF1Azeo == null )
                return;
            zwF1Azeo( element );
        }

        private void OnBehaviorChanged( ValueTuple<ModelChange, object, string, object, object, object, object> _param1 )
        {
            bool found = false;
            switch ( _param1.Item1 )
            {
                case ModelChange.FinishedRedo:
                case ModelChange.FinishedUndo:
                    found = true;
                    break;
                case ModelChange.Property:
                    if ( !_param1.Item3.IsEmpty() && !_param1.Item3.EqualsIgnoreCase( nameof( -1260198911 ) ) )
                    {
                        found = true;
                        break;
                    }
                    break;
                case ModelChange.ChangedNodesSource:
                    IEnumerable<TNode> source1 = ( IEnumerable<TNode> )_param1.Item4;
                    if ( source1 != null )
                        source1.ForEach<TNode>( new Action<TNode>( x => this.AddedNode( x ) );
                    IEnumerable<TNode> source2 = ( IEnumerable<TNode> )_param1.Item6;
                    if ( source2 != null )
                        source2.ForEach<TNode>( new Action<TNode>( this.OnAddedNode) );
                    found = true;
                    break;
                case ModelChange.ChangedLinksSource:
                    IEnumerable<TLink> source3 = ( IEnumerable<TLink> )_param1.Item4;
                    if ( source3 != null )
                        source3.ForEach<TLink>( new Action<TLink>( this.\u0023\u003DzahLiBXfSLT_wm3kMUs6TvgQ\u003D) );
                    IEnumerable<TLink> source4 = ( IEnumerable<TLink> )_param1.Item6;
                    if ( source4 != null )
                        source4.ForEach<TLink>( new Action<TLink>( this.\u0023\u003DzJHPfZrpNBUfg8B0YV4PU51g\u003D) );
                    found = true;
                    break;
                case ModelChange.AddedNode:
                    AddedNode( ( TNode )_param1.Item2 );
                    found = true;
                    break;
                case ModelChange.RemovedNode:
                    this.OnEachElementUninitialied( ( TNode )_param1.Item2 );
                    found = true;
                    break;
                case ModelChange.AddedLink:
                    this.\u0023\u003DqT\u00249jHbRht09ER1ypMqneXYfUFfAgjMDbFALCDwjz3J2pctpCo8EYjXJUzrBGCe25( ( TLink )_param1.Item2, false );
                    found = true;
                    break;
                case ModelChange.RemovedLink:
                    this.\u0023\u003DqkdTH1D88ZJ6gytAvJZRNrGEYSE96znMyrcbcm73pAe1omAOHRvBGzD31XhvsjHCO( ( TLink )_param1.Item2 );
                    found = true;
                    break;
                case ModelChange.ChangedLinkFromPort:
                    TLink link1 = ( TLink )_param1.Item2;
                    TLink link2 = link1.TypedClone<TLink>();
                    link2.From = ( string )_param1.Item4;
                    link2.FromPort = ( string )_param1.Item5;
                    this.\u0023\u003DqkdTH1D88ZJ6gytAvJZRNrGEYSE96znMyrcbcm73pAe1omAOHRvBGzD31XhvsjHCO( link2 );
                    this.\u0023\u003DqT\u00249jHbRht09ER1ypMqneXYfUFfAgjMDbFALCDwjz3J2pctpCo8EYjXJUzrBGCe25( link1, false );
                    found = true;
                    break;
                case ModelChange.ChangedLinkToPort:
                    TLink link3 = ( TLink )_param1.Item2;
                    TLink link4 = link3.TypedClone<TLink>();
                    link4.To = ( string )_param1.Item4;
                    link4.ToPort = ( string )_param1.Item5;
                    this.\u0023\u003DqkdTH1D88ZJ6gytAvJZRNrGEYSE96znMyrcbcm73pAe1omAOHRvBGzD31XhvsjHCO( link4 );
                    this.\u0023\u003DqT\u00249jHbRht09ER1ypMqneXYfUFfAgjMDbFALCDwjz3J2pctpCo8EYjXJUzrBGCe25( link3, false );
                    found = true;
                    break;
            }
            if ( !found )
                return;
            Action mc = this.OnModelChanged;
            if ( mc == null )
                return;
            mc();
        }

        

        private void OnSocketAdded( DiagramSocket socket )
        {            
            TNode node = this.Nodes.FirstOrDefault<TNode>( new Func<TNode, bool>( x => x.Element == socket.Parent ) );
            if ( ( object )node == null )
                return;

            foreach ( TLink link in this.GetLinks( node, socket ) )
            {
                if ( !this.GetSocket( ( ICompositionModelLink )link ) )
                    this._compositionModelBehaviors.RemoveLink( link );
            }
            this._compositionModelBehaviors.RaiseSocketAdded( node );
        }

        private bool GetSocket( ICompositionModelLink link )
        {
            if ( link.GetFromSocket<TNode, TLink>( this._compositionModelBehaviors ) != null )
                return link.GetToSocket<TNode, TLink>( this._compositionModelBehaviors ) != null;
            return false;
        }

        

        private IEnumerable<TLink> GetLinks( TNode node, DiagramSocket socket )
        {            
            return ( IEnumerable<TLink> )this._compositionModelBehaviors.GetLinksForNode( node ).Where<TLink>( new Func<TLink, bool>( x => {
                if ( !socket.IsInput )
                {
                    if ( x.From == node.Key )
                        return x.FromPort.EqualsIgnoreCase( socket.Id );
                    return false;
                }
                if ( x.To == node.Key )
                    return x.ToPort.EqualsIgnoreCase( socket.Id );
                return false;
            } ) ).ToArray<TLink>();
        }

        private IEnumerable<TLink> SomeFunctions( DiagramElement dElement, DiagramSocket socket )
        {
            return this.GetLinks( this.SomeNodeFunctions( dElement ), socket );
        }

        private TNode SomeNodeFunctions( DiagramElement dElement )
        {
            return this.Nodes.FirstOrDefault<TNode>( new Func<TNode, bool>( x => x.Element == dElement ) );
        }

        //private sealed class SomeNodeFunctions325
        //{
        //    public DiagramElement _diagram;

        //    internal bool Method3051( TX x )
        //    {
        //        return x.Element == this._diagram;
        //    }
        //}

        //private sealed class SomeHolder065332
        //{
        //    public DiagramSocket _socket01;

        //    internal bool Method363( TX x )
        //    {
        //        return x.Element == this._socket01.Parent;
        //    }
        //}

        private bool IsUndoManagerValid()
        {
            return UndoManager != null;
        }

        private void OnStartedUndoableOperation()
        {
            if ( !IsUndoManagerValid() )
                return;

            _compositionModelBehaviors.StartTransaction( nameof( -1260198674 ) );
        }



        private void OnCommittedUndoableOperation( DiagramElement diagram, IUndoableEdit uEdit )
        {
            if ( !IsUndoManagerValid() )
                return;

            TNode node = Nodes.FirstOrDefault<TNode>( new Func<TNode, bool>( x => x.Element == diagram ) );

            if ( node != null )
                _compositionModelBehaviors.RaiseCommited( nameof( -1260198674 ), node, uEdit );
            _compositionModelBehaviors.CommitTransaction( nameof( -1260198674 ) );
        }

        object ICompositionModel.Behavior
        {
            get
            {
                return _compositionModelBehaviors;
            }
        }

        bool ICompositionModel.HasErrors
        {
            get
            {
                return Elements.Any<DiagramElement>( x => x == null );
            }

        }

        /// <inheritdoc />
        public bool Modifiable
        {
            get
            {
                return _compositionModelBehaviors.Modifiable;
            }
            set
            {
                _compositionModelBehaviors.Modifiable = value;
            }
        }

        /// <inheritdoc />
        public IUndoManager UndoManager
        {
            get
            {
                return _compositionModelBehaviors.UndoManager;
            }
            set
            {
                _compositionModelBehaviors.UndoManager = value;
            }
        }

        /// <inheritdoc />
        public bool IsUndoManagerSuspended
        {
            get
            {
                return _compositionModelBehaviors.IsUndoManagerSuspended;
            }
            set
            {
                _compositionModelBehaviors.IsUndoManagerSuspended = value;
            }
        }

        /// <inheritdoc />
        public event Action ModelChanged;

        /// <inheritdoc />
        public event Action<DiagramElement> ElementAdded;

        /// <inheritdoc />
        public event Action<DiagramElement> ElementRemoved;

        /// <summary>Nodes.</summary>
        public IEnumerable<TNode> Nodes
        {
            get
            {
                return _compositionModelBehaviors.Nodes;
            }
            set
            {
                _compositionModelBehaviors.Nodes = value;
            }
        }

        /// <summary>Links.</summary>
        public IEnumerable<TLink> Links
        {
            get
            {
                return _compositionModelBehaviors.Links;
            }
            set
            {
                _compositionModelBehaviors.Links = value;
            }
        }

        /// <inheritdoc />
        public IEnumerable<DiagramElement> Elements
        {
            get
            {
                return Nodes.Select<TNode, DiagramElement>( CompositionModel<TNode, TLink>.LamdaShit.\u0023\u003DzBcvPZFLzwvfkKkZ2SA\u003D\u003D ?? ( CompositionModel<TNode, TLink>.LamdaShit.\u0023\u003DzBcvPZFLzwvfkKkZ2SA\u003D\u003D = new Func<TNode, DiagramElement>( CompositionModel<TNode, TLink>.LamdaShit._lamdaShit.\u0023\u003DzLRWoSB\u0024vSSSX5hsyr663iY0\u003D) ));
            }
        }

        DiagramElement ICompositionModel.\u0023\u003DzRORvwKViX3agMV7IEPyXkhcpwnFJ(
          string _param1)
        {
            TNode nodeByKey = _compositionModelBehaviors.FindNodeByKey( _param1 );
            ref TNode local = ref nodeByKey;
            if ( local == null )
                return null;
            return local.Element;
        }

        string ICompositionModel.\u0023\u003Dz9bxPNFWpy4\u0024rVHC0tbre0xx1EhHr(
          DiagramElement _param1)
        {
            TNode node = this.SomeNodeFunctions( _param1 );
            ref TNode local = ref node;
            if ( local == null )
                return null;
            return local.Key;
        }







        private void OnEachElementUninitialied( TNode _param1 )
        {
            if ( _param1.Element == null )
                return;
            DiagramElement element = _param1.Element;
            element.StartedUndoableOperation -= new Action( this.OnStartedUndoableOperation );
            element.CommittedUndoableOperation -= new Action<DiagramElement, IUndoableEdit>( this.OnCommittedUndoableOperation );
            element.SocketAdded -= new Action<DiagramSocket>( this.OnSocketAdded );
            element.SocketRemoved -= new Action<DiagramSocket>( this.OnSocketRemoved );
            element.SocketChanged -= new Action<DiagramSocket>( this.\u0023\u003DzFZcx6XM24oQh );
            Action<DiagramElement> zpUvmMdo = this.\u0023\u003DzpUVMMdo\u003D;
            if ( zpUvmMdo == null )
                return;
            zpUvmMdo( element );
        }



        private void OnSocketRemoved( DiagramSocket socket )
        {
            this.RemoveSocket( socket, false );
        }

        private void RemoveSocket( DiagramSocket _param1, bool _param2 )
        {
            CompositionModel<TNode, TLink>.SomeHolder1234 mx1ovnfnxgCdr6JlhaiI9oE = new CompositionModel<TNode, TLink>.SomeHolder1234();
            mx1ovnfnxgCdr6JlhaiI9oE._socket01 = _param1;
            mx1ovnfnxgCdr6JlhaiI9oE.\u0023\u003Dz6dLRbYgaOhi_ = _param2;
            mx1ovnfnxgCdr6JlhaiI9oE._diagramElement = this;
            TNode node = this.Nodes.FirstOrDefault<TNode>( new Func<TNode, bool>( mx1ovnfnxgCdr6JlhaiI9oE.\u0023\u003Dzp_Zr2mFXNmiLha99wQ\u003D\u003D) );
            if ( ( object )node == null )
                return;
            this.SomeFunctions( node, mx1ovnfnxgCdr6JlhaiI9oE._socket01 ).Where<TLink>( new Func<TLink, bool>( mx1ovnfnxgCdr6JlhaiI9oE.\u0023\u003DzCHUrTK3I\u0024fqIeeFEqw\u003D\u003D) ).ForEach<TLink>( new Action<TLink>( this._compositionModelBehaviors.RemoveLink ) );
            this._compositionModelBehaviors.RaiseLinksRemoved( node );
        }

        private void \u0023\u003DzFZcx6XM24oQh( DiagramSocket _param1 )
        {
            this.RemoveSocket( _param1, true );
        }

        private sealed class SomeHolder1234
    {
      public DiagramSocket _socket01;
        public bool \u0023\u003Dz6dLRbYgaOhi_;
      public CompositionModel<TX, TY> _diagramElement;

        internal bool \u0023\u003Dzp_Zr2mFXNmiLha99wQ\u003D\u003D(TX _param1)
      {
        return _param1.Element == this._socket01.Parent;
      }

    internal bool \u0023\u003DzCHUrTK3I\u0024fqIeeFEqw\u003D\u003D(
      TY _param1)
      {
        if (this.\u0023\u003Dz6dLRbYgaOhi_ && this._diagramElement.SomeLink((ICompositionModelLink) _param1))
          return !this._diagramElement.\u0023\u003DzihlTvy0imJXr((ICompositionModelLink) _param1);
        return true;
    }
}



private bool \u0023\u003DzihlTvy0imJXr( ICompositionModelLink _param1 )
        {
            DiagramSocket fromSocket = _param1.GetFromSocket<TNode, TLink>( this._compositionModelBehaviors );
            DiagramSocket toSocket = _param1.GetToSocket<TNode, TLink>( this._compositionModelBehaviors );
            DiagramSocket diagramSocket = fromSocket;
            DiagramSocket to = toSocket;
            if ( diagramSocket != null && to != null )
                return diagramSocket.CanConnect( to );
            return false;
        }



        private bool IsUndoManagerValid()
        {
            return this.UndoManager != null;
        }

        private void OnStartedUndoableOperation()
        {
            if ( !this.IsUndoManagerValid() )
                return;
            this._compositionModelBehaviors.StartTransaction( nameof( -1260198674 ) );
        }

        private void OnCommittedUndoableOperation( DiagramElement _param1, IUndoableEdit _param2 )
        {
            CompositionModel<TNode, TLink>.SomeHolder0653 lbouEtKdC9poOv1kgPw = new CompositionModel<TNode, TLink>.SomeHolder0653();
            lbouEtKdC9poOv1kgPw._diagram = _param1;
            if ( !this.IsUndoManagerValid() )
                return;
            TNode node = this.Nodes.FirstOrDefault<TNode>( new Func<TNode, bool>( lbouEtKdC9poOv1kgPw.Method360 ) );
            if ( ( object )node != null )
                this._compositionModelBehaviors.RaiseCommited( nameof( -1260198674 ), node, _param2 );
            this._compositionModelBehaviors.CommitTransaction( nameof( -1260198674 ) );
        }



        IEnumerable<DiagramSocket> ICompositionModel.\u0023\u003Dz5_C_kSxqbOzo4hiyYnkdLJO_Nmr1QsYqGEF_N_s\u003D(
          DiagramElement _param1,
          DiagramSocket _param2)
        {
            IEnumerable<TLink> source = this.SomeFunctions( _param1, _param2 );
            if ( !_param2.IsInput )
                return source.Select<TLink, DiagramSocket>( new Func<TLink, DiagramSocket>( this.\u0023\u003DzWB65jvo2jRYlchj4uHuJaXhFcXsergMm2BHlTHPQZaz\u0024jUNsBA\u003D\u003D) );
            return source.Select<TLink, DiagramSocket>( new Func<TLink, DiagramSocket>( this.\u0023\u003Dz_3rBmqHDk8xk8zFY0D4TM2\u0024HrL1G60e9LaAcTWz0jOrcjWDToQ\u003D\u003D) );
        }

        void ICompositionModel.\u0023\u003Dz3\u00241RqFPruZDmohRwvQRiXt0\u003D(
          DiagramElement _param1,
          PointF _param2)
        {
            CompositionModel<TNode, TLink>.\u0023\u003Dzk_UQUHQ_B6RAzwbB5oucS8U\u003D uquhqB6RazwbB5oucS8U = new CompositionModel<TNode, TLink>.\u0023\u003Dzk_UQUHQ_B6RAzwbB5oucS8U\u003D();
            uquhqB6RazwbB5oucS8U._diagramElement = this;
            uquhqB6RazwbB5oucS8U._diagram = _param1;
            uquhqB6RazwbB5oucS8U.\u0023\u003DzkEU6r4g\u003D = _param2;
            if ( uquhqB6RazwbB5oucS8U._diagram == null )
                throw new ArgumentNullException( nameof( -1260198665 ) );
            this.ExecuteTransaction( nameof( -1260198710 ), new Action<CompositionModel<TNode, TLink>>( uquhqB6RazwbB5oucS8U.\u0023\u003DzUrPm83BvrtIc_BEsJoE3WcV1FcGwzy_aVQ\u003D\u003D) );
        }

        /// <summary>Add node.</summary>
        /// <param name="node">
        ///   <typeparamref name="TNode" />
        /// </param>
        public void AddNode( TNode node )
        {
            this._compositionModelBehaviors.AddNode( node );
        }

        /// <summary>Remove node.</summary>
        /// <param name="node">
        ///   <typeparamref name="TNode" />
        /// </param>
        public void RemoveNode( TNode node )
        {
            this._compositionModelBehaviors.RemoveNode( node );
        }

        /// <summary>Remove link.</summary>
        /// <param name="from">From node.</param>
        /// <param name="fromPort">
        ///   <see cref="P:StockSharp.Diagram.ICompositionModelLink.FromPort" />
        /// </param>
        /// <param name="to">To node.</param>
        /// <param name="toPort">
        ///   <see cref="P:StockSharp.Diagram.ICompositionModelLink.ToPort" />
        /// </param>
        public void RemoveLink( TNode from, string fromPort, TNode to, string toPort )
        {
            this._compositionModelBehaviors.RemoveLink( from, fromPort, to, toPort );
        }

        [return: TupleElementNames( new string[ ] { "nodeKey", "socket" } )]
        IEnumerable<ValueTuple<string, DiagramSocket>> ICompositionModel.\u0023\u003DzAf_OhKZp\u0024f6NivpV_UFaC7ZIhEzeO_UDKg\u003D\u003D()
        {
            List<ValueTuple<string, DiagramSocket>> valueTupleList = new List<ValueTuple<string, DiagramSocket>>();
            foreach ( TNode node in this.Nodes )
            {
                CompositionModel<TNode, TLink>.\u0023\u003DzGK0Qrg98Jkr10hUqO6xrzXQ\u003D qrg98Jkr10hUqO6xrzXq = new CompositionModel<TNode, TLink>.\u0023\u003DzGK0Qrg98Jkr10hUqO6xrzXQ\u003D();
                qrg98Jkr10hUqO6xrzXq._templateXY = node;
                if ( qrg98Jkr10hUqO6xrzXq._templateXY.Element != null && qrg98Jkr10hUqO6xrzXq._templateXY.Element.ShowSockets )
                {
                    qrg98Jkr10hUqO6xrzXq.\u0023\u003DzqE1dqIfcQ90J = this._compositionModelBehaviors.GetLinksForNode( qrg98Jkr10hUqO6xrzXq._templateXY ).Select<TLink, string>( new Func<TLink, string>( qrg98Jkr10hUqO6xrzXq.\u0023\u003DzLT5K9kifajdKddRH3SyXHUI6Q6\u0024qlpZuqDdC48r138A9 ) ).ToArray<string>();
                    qrg98Jkr10hUqO6xrzXq._templateXY.Element.InputSockets.Concat<DiagramSocket>( ( IEnumerable<DiagramSocket> )qrg98Jkr10hUqO6xrzXq._templateXY.Element.OutputSockets ).Where<DiagramSocket>( new Func<DiagramSocket, bool>( qrg98Jkr10hUqO6xrzXq.\u0023\u003DzAE4bJE5FgwlT65U6pZWOhETDxE86Kx0zePvdjvEcog26 ) ).Select<DiagramSocket, ValueTuple<string, DiagramSocket>>( new Func<DiagramSocket, ValueTuple<string, DiagramSocket>>( qrg98Jkr10hUqO6xrzXq.\u0023\u003Dzawibpr_MV3Ad_0OSYsS5DUF7Rkjeizqs6mRmdu046CUo ) ).ForEach<ValueTuple<string, DiagramSocket>>( new Action<ValueTuple<string, DiagramSocket>>( valueTupleList.Add ) );
                }
            }
            return ( IEnumerable<ValueTuple<string, DiagramSocket>> )valueTupleList;
        }

        /// <summary>Execute the specified action in transaction scope.</summary>
        /// <param name="name">Transaction name.</param>
        /// <param name="action">Action.</param>
        public void ExecuteTransaction( string name, Action<CompositionModel<TNode, TLink>> action )
        {
            if ( name.IsEmpty() )
                throw new ArgumentNullException( nameof( -1260198703 ) );
            if ( action == null )
                throw new ArgumentNullException( nameof( -1260198748 ) );
            this._compositionModelBehaviors.StartTransaction( name );
            try
            {
                action( this );
                this._compositionModelBehaviors.CommitTransaction( name );
            }
            catch ( Exception ex )
            {
                this._compositionModelBehaviors.RollbackTransaction();
                throw;
            }
        }

        private CompositionModel<TNode, TLink> \u0023\u003Dzrjw0qgw\u003D()
    {
      CompositionModel<TNode, TLink> compositionModel = new CompositionModel<TNode, TLink>( ( ICompositionModelBehavior<TNode, TLink> )this._compositionModelBehaviors.Clone() );
        compositionModel.ExecuteTransaction(nameof(-1260198725), new Action<CompositionModel<TNode, TLink>>(this.\u0023\u003DzBeOP7ZObITbVhoU8Mw\u003D\u003D));
      return compositionModel;
        }

        ICompositionModel ICloneable<ICompositionModel>.\u0023\u003DzvAatKiHZz5l3FAlrENHHWwBpM1TE\u0024y1TgOoqSew3RQOh()
        {
            return ( ICompositionModel )this.\u0023\u003Dzrjw0qgw\u003D();
        }

        object ICloneable.Clone()
        {
            return ( object )this.\u0023\u003Dzrjw0qgw\u003D();
    }

    private void \u0023\u003DqkdTH1D88ZJ6gytAvJZRNrGEYSE96znMyrcbcm73pAe1omAOHRvBGzD31XhvsjHCO(
      TLink _param1)
    {
      if (_param1.IsReconnecting)
        return;
      DiagramSocket fromSocket = _param1.GetFromSocket<TNode, TLink>(this._compositionModelBehaviors);
      DiagramSocket toSocket = _param1.GetToSocket<TNode, TLink>(this._compositionModelBehaviors);
      DiagramSocket other1 = fromSocket;
      DiagramSocket other2 = toSocket;
      other1?.Disconnect(other2);
      other2?.Disconnect(other1);
      _param1.IsConnected = false;
    }

    private bool \u0023\u003DqT\u00249jHbRht09ER1ypMqneXYfUFfAgjMDbFALCDwjz3J2pctpCo8EYjXJUzrBGCe25(
      TLink _param1,
      bool _param2)
    {
      if (_param1.IsReconnecting)
        return true;
      try
      {
        _param1.IsReconnecting = true;
        _param1.IsConnected = true;
        DiagramSocket fromSocket = _param1.GetFromSocket<TNode, TLink>(this._compositionModelBehaviors);
        DiagramSocket toSocket = _param1.GetToSocket<TNode, TLink>(this._compositionModelBehaviors);
        DiagramSocket other = fromSocket;
        DiagramSocket diagramSocket = toSocket;
        if (other == null || diagramSocket == null || _param2 && !other.CanConnect(diagramSocket))
          return false;
        other.Connect(diagramSocket);
        diagramSocket.Connect(other);
        _param1.FromPort = other.Id;
        _param1.ToPort = diagramSocket.Id;
      }
      finally
      {
        _param1.IsReconnecting = false;
      }
      return true;
    }

    

    private void OnAddedNode(TNode _param1)
    {
      this.AddedNode(_param1);
    }

    private void \u0023\u003DzahLiBXfSLT_wm3kMUs6TvgQ\u003D(TLink _param1)
    {
      this.\u0023\u003DqkdTH1D88ZJ6gytAvJZRNrGEYSE96znMyrcbcm73pAe1omAOHRvBGzD31XhvsjHCO(_param1);
    }

    private void \u0023\u003DzJHPfZrpNBUfg8B0YV4PU51g\u003D(TLink _param1)
    {
      this.\u0023\u003DqT\u00249jHbRht09ER1ypMqneXYfUFfAgjMDbFALCDwjz3J2pctpCo8EYjXJUzrBGCe25(_param1, false);
    }

    private DiagramSocket \u0023\u003Dz_3rBmqHDk8xk8zFY0D4TM2\u0024HrL1G60e9LaAcTWz0jOrcjWDToQ\u003D\u003D(
      TLink _param1)
    {
      return _param1.GetFromSocket<TNode, TLink>(this._compositionModelBehaviors);
    }

    private DiagramSocket \u0023\u003DzWB65jvo2jRYlchj4uHuJaXhFcXsergMm2BHlTHPQZaz\u0024jUNsBA\u003D\u003D(
      TLink _param1)
    {
      return _param1.GetToSocket<TNode, TLink>(this._compositionModelBehaviors);
    }

    private void \u0023\u003DzBeOP7ZObITbVhoU8Mw\u003D\u003D(CompositionModel<TNode, TLink> _param1)
    {
      _param1.Nodes = (IEnumerable<TNode>) new ObservableCollection<TNode>(this.Nodes.Select<TNode, TNode>(CompositionModel<TNode, TLink>.LamdaShit.\u0023\u003DzuuHc4C_fT8St3xo1gA\u003D\u003D ?? (CompositionModel<TNode, TLink>.LamdaShit.\u0023\u003DzuuHc4C_fT8St3xo1gA\u003D\u003D = new Func<TNode, TNode>(CompositionModel<TNode, TLink>.LamdaShit._lamdaShit.\u0023\u003DzWAO9PGi2rDDcyk0A\u0024g\u003D\u003D))));
      _param1.Links = (IEnumerable<TLink>) new ObservableCollection<TLink>(this.Links.Select<TLink, TLink>(CompositionModel<TNode, TLink>.LamdaShit.\u0023\u003DzBP0GTrGaRr9VzQgGow\u003D\u003D ?? (CompositionModel<TNode, TLink>.LamdaShit.\u0023\u003DzBP0GTrGaRr9VzQgGow\u003D\u003D = new Func<TLink, TLink>(CompositionModel<TNode, TLink>.LamdaShit._lamdaShit.\u0023\u003DzrOarSOQNGR788vUYlg\u003D\u003D))));
    }

   

    private sealed class \u0023\u003DzGK0Qrg98Jkr10hUqO6xrzXQ\u003D
    {
      public TX _templateXY;
      public string[] \u0023\u003DzqE1dqIfcQ90J;

      internal string \u0023\u003DzLT5K9kifajdKddRH3SyXHUI6Q6\u0024qlpZuqDdC48r138A9(
        TY _param1)
      {
        if (!(_param1.From == this._templateXY.Key))
          return _param1.ToPort;
        return _param1.FromPort;
      }

      internal bool \u0023\u003DzAE4bJE5FgwlT65U6pZWOhETDxE86Kx0zePvdjvEcog26(DiagramSocket _param1)
      {
        return ((IEnumerable<string>) this.\u0023\u003DzqE1dqIfcQ90J).All<string>(new Func<string, bool>(new CompositionModel<TX, TY>.\u0023\u003DzJYKAoGholwHE_fINj0a236M\u003D() { \u0023\u003DzzMalryc\u003D = _param1 }.\u0023\u003Dz6Bf9PG4Dg0Whm0263M286uSt_qoPD8k5a_88JgI\u0024HURN));
      }

      internal ValueTuple<string, DiagramSocket> \u0023\u003Dzawibpr_MV3Ad_0OSYsS5DUF7Rkjeizqs6mRmdu046CUo(
        DiagramSocket _param1)
      {
        return new ValueTuple<string, DiagramSocket>(this._templateXY.Key, new DiagramSocket(_param1.Directon, _param1.Id) { Name = string.Concat(_param1.Name, nameof(-1260198860), this._templateXY.Element.Name), Type = _param1.Type, LinkableMaximum = _param1.LinkableMaximum });
      }
    }

    private sealed class \u0023\u003DzJYKAoGholwHE_fINj0a236M\u003D
    {
      public DiagramSocket \u0023\u003DzzMalryc\u003D;

      internal bool \u0023\u003Dz6Bf9PG4Dg0Whm0263M286uSt_qoPD8k5a_88JgI\u0024HURN(string _param1)
      {
        return !_param1.EqualsIgnoreCase(this.\u0023\u003DzzMalryc\u003D.Id);
      }
    }

    [Serializable]
    private sealed class LamdaShit
    {
      public static readonly CompositionModel<TX, TY>.LamdaShit _lamdaShit = new CompositionModel<TX, TY>.LamdaShit();
      public static Func<DiagramElement, bool> Method360;
      public static Func<TX, DiagramElement> \u0023\u003DzBcvPZFLzwvfkKkZ2SA\u003D\u003D;
      public static Func<TX, TX> \u0023\u003DzuuHc4C_fT8St3xo1gA\u003D\u003D;
      public static Func<TY, TY> \u0023\u003DzBP0GTrGaRr9VzQgGow\u003D\u003D;

      internal bool Method363(
        DiagramElement _param1)
      {
        return _param1 == null;
      }

      internal DiagramElement \u0023\u003DzLRWoSB\u0024vSSSX5hsyr663iY0\u003D(
        TX _param1)
      {
        return _param1.Element;
      }

      internal TX \u0023\u003DzWAO9PGi2rDDcyk0A\u0024g\u003D\u003D(
        TX _param1)
      {
        return (TX) _param1.Clone();
      }

      internal TY \u0023\u003DzrOarSOQNGR788vUYlg\u003D\u003D(
        TY _param1)
      {
        return _param1.TypedClone<TY>();
      }
    }

    

    

    private sealed class \u0023\u003Dzk_UQUHQ_B6RAzwbB5oucS8U\u003D
    {
      public CompositionModel<TX, TY> _diagramElement;
      public DiagramElement _diagram;
      public PointF \u0023\u003DzkEU6r4g\u003D;

      internal void \u0023\u003DzUrPm83BvrtIc_BEsJoE3WcV1FcGwzy_aVQ\u003D\u003D(
        CompositionModel<TX, TY> _param1)
      {
        this._diagramElement._compositionModelBehaviors.AddNode(new TX()
        {
          Element = this._diagram,
          Location = this.\u0023\u003DzkEU6r4g\u003D
        });
      }
    }

    

    
  }
}
