//using DotNetDBF;
//using Ecng.Common;
//using StockSharp.Localization;
//using System;
//using System.IO;

//internal sealed class Class16 : Disposable
//{
//    private readonly bool bool_0;
//    private readonly string string_0;
//    private readonly DBFReader dbfreader_0;
//    private readonly StreamReader streamReader_0;
//    private string[] string_1;
//    private int int_0;

//    public Class16( bool bool_1, string string_2 )
//    {
//        this.bool_0 = bool_1;
//        this.string_0 = string_2;
//        if( bool_1 )
//        {
//            this.streamReader_0 = new StreamReader( string_2 );
//        }
//        else
//        {
//            this.dbfreader_0 = new DBFReader( string_2 );
//        }
//    }

//    protected override void DisposeManaged( )
//    {
//        if( this.bool_0 )
//        {
//            this.streamReader_0?.Dispose( );
//        }

//        if( !this.bool_0 )
//        {
//            this.dbfreader_0?.Dispose( );
//        }

//        base.DisposeManaged( );
//    }

//    private string[] method_0( )
//    {
//        string str = this.streamReader_0.ReadLine( );
//        if( str == null )
//        {
//            throw new InvalidOperationException( LocalizedStrings.Str2107Params.Put( ( object )this.string_0 ) );
//        }

//        return str.Split( ';' );
//    }

//    public int method_1( string string_2 )
//    {
//        if( !this.bool_0 )
//        {
//            return this.dbfreader_0.smethod_0( string_2 );
//        }

//        if( this.string_1 == null )
//        {
//            this.string_1 = this.method_0( );
//        }

//        return this.string_1.IndexOf< string >( string_2 );
//    }

//    public object[] method_2( )
//    {
//        if( !this.bool_0 )
//        {
//            if( this.int_0 >= this.dbfreader_0.RecordCount )
//            {
//                return ( object[ ] )null;
//            }

//            ++this.int_0;
//            return this.dbfreader_0.NextRecord( );
//        }
//        string str = this.streamReader_0.ReadLine( );
//        string[] strArray;
//        if( str == null )
//        {
//            strArray = ( string[ ] )null;
//        }
//        else
//        {
//            strArray = str.Split( ';' );
//        }

//        return ( object[] ) strArray;
//    }
//}
