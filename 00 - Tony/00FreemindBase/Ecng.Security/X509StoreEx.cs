using Ecng.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Ecng.Security
{
    public class X509StoreEx : Disposable
    {
        private readonly X509Store _store;
        private X509StoreEx.X509Certificate2CollectionEx _certificates;

        public X509StoreEx( OpenFlags flags )
        {
            this._store = new X509Store();
            this.Open( flags );
        }

        public X509StoreEx( StoreLocation location, OpenFlags flags )
        {
            this._store = new X509Store( location );
            this.Open( flags );
        }

        public X509StoreEx( StoreName name, OpenFlags flags )
        {
            this._store = new X509Store( name );
            this.Open( flags );
        }

        public X509StoreEx( string name, OpenFlags flags )
        {
            this._store = new X509Store( name );
            this.Open( flags );
        }

        public X509StoreEx( StoreName name, StoreLocation location, OpenFlags flags )
        {
            this._store = new X509Store( name, location );
            this.Open( flags );
        }

        public X509StoreEx( string name, StoreLocation location, OpenFlags flags )
        {
            this._store = new X509Store( name, location );
            this.Open( flags );
        }

        public string Name
        {
            get
            {
                return this._store.Name;
            }
        }

        public StoreLocation Location
        {
            get
            {
                return this._store.Location;
            }
        }

        public X509Certificate2Collection Certificates
        {
            get
            {
                return ( X509Certificate2Collection )this._certificates;
            }
        }

        private void Open( OpenFlags flags )
        {
            this._store.Open( flags );
            this._certificates = new X509StoreEx.X509Certificate2CollectionEx( this._store );
        }

        protected override void DisposeManaged()
        {
            this._store.Close();
            base.DisposeManaged();
        }

        private sealed class X509Certificate2CollectionEx : X509Certificate2Collection
        {
            private readonly X509Store _store;

            public X509Certificate2CollectionEx( X509Store store )
              : base( store.Certificates )
            {
                this._store = store;
            }

            protected override void OnInsertComplete( int index, object value )
            {
                if ( this._store != null )
                    this._store.Add( ( X509Certificate2 )value );
                base.OnInsertComplete( index, value );
            }

            protected override void OnSetComplete( int index, object oldValue, object newValue )
            {
                if ( this._store != null )
                {
                    if ( oldValue != null )
                        this._store.Remove( ( X509Certificate2 )oldValue );
                    this._store.Add( ( X509Certificate2 )newValue );
                }
                base.OnSetComplete( index, oldValue, newValue );
            }

            protected override void OnRemoveComplete( int index, object value )
            {
                if ( this._store != null )
                    this._store.Remove( ( X509Certificate2 )value );
                base.OnRemoveComplete( index, value );
            }

            protected override void OnClearComplete()
            {
                if ( this._store != null )
                    this._store.RemoveRange( new X509Certificate2Collection( this.InnerList.Cast<X509Certificate2>().ToArray<X509Certificate2>() ) );
                base.OnClearComplete();
            }
        }
    }
}
