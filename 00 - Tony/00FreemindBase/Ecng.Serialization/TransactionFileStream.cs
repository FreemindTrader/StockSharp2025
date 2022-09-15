using Ecng.Common;
using System;
using System.Diagnostics;
using System.IO;

namespace Ecng.Serialization
{
    public class TransactionFileStream : Stream
    {
        private readonly string _name;
        private readonly string _nameTemp;
        private FileStream _temp;

        public TransactionFileStream( string name, FileMode mode )
        {
            if ( name.IsEmpty() )
                throw new ArgumentNullException( nameof( name ) );
            this._name = name;
            this._nameTemp = this._name + ".tmp";
            switch ( mode )
            {
                case FileMode.CreateNew:
                {
                    if ( File.Exists( this._name ) )
                        throw new IOException();
                    goto case FileMode.Create;
                }
                
                case FileMode.Create:
                case FileMode.Truncate:
                {
                    this._temp = new FileStream( this._nameTemp, mode, FileAccess.Write );
                }                
                break;

                case FileMode.Open:
                {
                    File.Copy( this._name, this._nameTemp, true );
                    goto case FileMode.Create;
                }
                
                case FileMode.OpenOrCreate:
                {
                    if ( File.Exists( this._name ) )
                    {
                        File.Copy( this._name, this._nameTemp, true );
                        goto case FileMode.Create;
                    }
                    else
                        goto case FileMode.Create;
                }
                
                case FileMode.Append:
                {
                    if ( File.Exists( this._name ) )
                    {
                        File.Copy( this._name, this._nameTemp, true );
                        goto case FileMode.Create;
                    }
                    else
                        goto case FileMode.Create;
                }
                
                default:
                throw new ArgumentOutOfRangeException( nameof( mode ), ( object )mode, ( string )null );
            }
        }

        protected override void Dispose( bool disposing )
        {
            if ( this._temp == null )
                return;
            this._temp.Dispose();
            this._temp = ( FileStream )null;
            base.Dispose( disposing );
            File.Copy( this._nameTemp, this._name, true );
            try
            {
                File.Delete( this._nameTemp );
            }
            catch ( Exception ex )
            {
                Trace.WriteLine( ( object )ex );
            }
        }

        public override void Flush()
        {
            this._temp.Flush();
        }

        public override long Seek( long offset, SeekOrigin origin )
        {
            return this._temp.Seek( offset, origin );
        }

        public override void SetLength( long value )
        {
            this._temp.SetLength( value );
        }

        public override int Read( byte[ ] buffer, int offset, int count )
        {
            throw new NotSupportedException();
        }

        public override void Write( byte[ ] buffer, int offset, int count )
        {
            this._temp.Write( buffer, offset, count );
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return this._temp.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return this._temp.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                return this._temp.Length;
            }
        }

        public override long Position
        {
            get
            {
                return this._temp.Position;
            }
            set
            {
                this._temp.Position = value;
            }
        }
    }
}
