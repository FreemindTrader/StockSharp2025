using Ecng.Common;
using Ecng.Serialization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;

namespace fx.Charting
{
    public abstract class ChartComponentBase< T > : Equatable< T >, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable where T : ChartComponentBase< T >
    {
        private Guid _guid;

        protected ChartComponentBase( )
        {
            Id = Guid.NewGuid( );
        }

        [Browsable( false )]
        public Guid Id
        {
            get
            {
                return _guid;
            }
            internal set
            {
                _guid = value;
            }
        }

        public virtual void Load( SettingsStorage storage )
        {
            Id = storage.GetValue( "Id", new Guid( ) );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.SetValue( "Id", Id );
        }

        protected override bool OnEquals( T other )
        {
            return other.Id == Id;
        }

        public override int GetHashCode( )
        {
            return Id.GetHashCode( );
        }

        internal virtual T Clone( T other )
        {
            if( other == default( T ) )
            {
                throw new ArgumentNullException( "elem" );
            }
            other.Id = Id;
            return other;
        }

        public event PropertyChangingEventHandler PropertyChanging;

        protected void RaisePropertyChanging( string name )
        {
            PropertyChanging?.Invoke( this, name );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged( string name )
        {
            PropertyChanged?.Invoke( this, name );
        }

        public event Action< object, string, object > PropertyValueChanging;

        protected void RaisePropertyValueChanging( string name, object value )
        {
            PropertyValueChanging?.Invoke( this, name, value );
        }

        protected bool SetField< TField >( ref TField field, TField value, string name )
        {
            if( EqualityComparer< TField >.Default.Equals( field, value ) )
            {
                return false;
            }
            RaisePropertyChanging( name );
            field = value;
            RaisePropertyChanged( name );
            return true;
        }
    }
}
