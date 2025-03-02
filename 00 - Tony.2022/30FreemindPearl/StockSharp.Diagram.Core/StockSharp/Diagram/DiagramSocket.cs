
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StockSharp.Diagram
{
    /// <summary>Connection.</summary>
    public class DiagramSocket : Disposable, INotifyPropertyChanged
    {

        private DiagramSocketType _type = DiagramSocketType.Bool;

        private readonly INotifyPropertyChanged _propertyChangedEvent;

        private string _id;

        private string _name;

        private readonly DiagramSocketDirection _direction;

        private bool _isDynamic;

        private int _linkableMaximum;

        private object _value;

        private DiagramElement _parent;

        private bool _isSelected;

        private bool _diagramSocketFunction;

        private bool _isBreakActive;

        private IList<DiagramSocketType> _availableTypes;

        private System.Action<DiagramSocketValue> _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.DiagramSocket" />.
        /// </summary>
        public DiagramSocket( DiagramSocketDirection dir, string socketId = null )
        {
            if ( socketId != null && socketId.IsEmptyOrWhiteSpace() )
                throw new ArgumentException( "socketId" );
            _propertyChangedEvent = new DispatcherNotifiableObject<DiagramSocket>( ConfigManager.GetService<IDispatcher>(), this );

            _id = socketId ?? Guid.NewGuid().ToString();
            _direction = dir;
            AvailableTypes = new List<DiagramSocketType>();
            this.Reset();
        }

        /// <summary>Gui wrapper for property binding.</summary>
        public INotifyPropertyChanged GuiWrapper
        {
            get
            {
                return _propertyChangedEvent;
            }
        }

        /// <summary>The connection identifier.</summary>
        public string Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                if ( _id.EqualsIgnoreCase( value ) )
                    return;
                _id = value;
                OnPropertyChanged( nameof( Id ) );
            }
        }

        /// <summary>The connection name.</summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged( nameof( Name ) );
            }
        }

        /// <summary>Connection type.</summary>
        public DiagramSocketType Type
        {
            get
            {
                return _type;
            }
            set
            {
                DiagramSocketType diagramSocketType = value;
                if ( diagramSocketType == null )
                    throw new ArgumentNullException( "diagramSocketType == null" );
                _type = diagramSocketType;
                OnPropertyChanged( nameof( Type ) );
            }
        }

        /// <summary>The connection direction.</summary>
        public DiagramSocketDirection Directon
        {
            get
            {
                return _direction;
            }
        }

        /// <summary>Dynamic sockets are removed during Load().</summary>
        public bool IsDynamic
        {
            get
            {
                return _isDynamic;
            }
            set
            {
                _isDynamic = value;
            }
        }

        /// <summary>The maximum number of connections.</summary>
        public int LinkableMaximum
        {
            get
            {
                return _linkableMaximum;
            }
            set
            {
                _linkableMaximum = value;
                OnPropertyChanged( nameof( LinkableMaximum ) );
            }
        }

        /// <summary>The current value.</summary>
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged( nameof( Value ) );
            }
        }

        /// <summary>The socket parent element.</summary>
        public DiagramElement Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if ( _parent == value )
                    return;
                _parent = value;
                OnPropertyChanged( nameof( Parent ) );
            }
        }

        /// <summary>Is socket selected.</summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if ( _isSelected == value )
                    return;
                _isSelected = value;
                OnPropertyChanged( nameof( IsSelected ) );
            }
        }

        /// <summary>Is socket has break.</summary>
        public bool IsBreak
        {
            get
            {
                return _diagramSocketFunction;
            }
            set
            {
                if ( _diagramSocketFunction == value )
                    return;
                _diagramSocketFunction = value;
                OnPropertyChanged( nameof( IsBreak ) );
            }
        }

        /// <summary>Is socket break active.</summary>
        public bool IsBreakActive
        {
            get
            {
                return _isBreakActive;
            }
            set
            {
                if ( _isBreakActive == value )
                    return;
                _isBreakActive = value;
                OnPropertyChanged( nameof( IsBreakActive ) );
            }
        }

        /// <summary>Available input data types.</summary>
        public IList<DiagramSocketType> AvailableTypes
        {
            get
            {
                return _availableTypes;
            }

            set
            {
                _availableTypes = value;
            }
        }



        /// <summary>Socket action.</summary>
        public System.Action<DiagramSocketValue> Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }

        /// <summary>The event of the socket connection with another one.</summary>
        public event System.Action<DiagramSocket, DiagramSocket> Connected;

        /// <summary>The socket disconnection event.</summary>
        public event System.Action<DiagramSocket, DiagramSocket> Disconnected;

        /// <inheritdoc />
        protected override void DisposeManaged()
        {
            GuiWrapper.DoDispose<INotifyPropertyChanged>();
            base.DisposeManaged();
        }

        /// <summary>
        /// </summary>
        /// <param name="original">
        ///   <see cref="T:StockSharp.Diagram.DiagramSocket" />
        /// </param>
        public virtual void InitializeCopy( DiagramSocket original )
        {
        }

        /// <summary>To check the ability to make a connection.</summary>
        /// <param name="to">Connection.</param>
        /// <returns>The test result.</returns>
        public bool CanConnect( DiagramSocket to )
        {
            if ( to == null )
                throw new ArgumentNullException( "to == null" );
            if ( to.Type.Type.IsAssignableFrom( Type.Type ) && to.Type != DiagramSocketType.Any )
                return true;
            if ( to.AvailableTypes.Contains( DiagramSocketType.Any ) && to.Type == DiagramSocketType.Any )
                return to.CanConnectFrom( this );
            return to.AvailableTypes.Contains( Type );
        }

        /// <summary>To check the ability to make a connection.</summary>
        /// <param name="from">Connection.</param>
        /// <returns>The test result.</returns>
        public virtual bool CanConnectFrom( DiagramSocket from )
        {
            return true;
        }

        /// <summary>Is input.</summary>
        public bool IsInput
        {
            get
            {
                return Directon == DiagramSocketDirection.In;
            }
        }

        /// <summary>Is output.</summary>
        public bool IsOutput
        {
            get
            {
                return Directon == DiagramSocketDirection.Out;
            }
        }

        /// <summary>
        /// Invoke <see cref="E:StockSharp.Diagram.DiagramSocket.Connected" /> event.
        /// </summary>
        /// <param name="other">
        ///   <see cref="T:StockSharp.Diagram.DiagramSocket" />
        /// </param>
        public void Connect( DiagramSocket other )
        {
            if ( other.Directon == Directon )
                throw new InvalidOperationException( "other.Directon == this.Directon" );

            System.Action<DiagramSocket, DiagramSocket> cnt = Connected;
            if ( cnt == null )
                return;
            cnt( this, other );
        }

        /// <summary>
        /// Invoke <see cref="E:StockSharp.Diagram.DiagramSocket.Connected" /> event.
        /// </summary>
        /// <param name="other">
        ///   <see cref="T:StockSharp.Diagram.DiagramSocket" />
        /// </param>
        public void Disconnect( DiagramSocket other )
        {
            System.Action<DiagramSocket, DiagramSocket> dis = Disconnected;
            if ( dis == null )
                return;
            dis( this, other );
        }

        /// <summary>The connection properties value change event.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>To call the connection property value change event.</summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged( string propertyName )
        {
            PropertyChangedEventHandler pc = PropertyChanged;
            if ( pc == null )
                return;
            pc.Invoke( this, propertyName );
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if ( Parent == null )
                return string.Concat( Name, "(", Id, ")" );

            return string.Concat( Parent.Name, "(", Name, ")" );
        }
    }
}
