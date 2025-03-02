using DotNetDBF;
using Ecng.Collections;
using Ecng.Interop;
using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

internal static class Class25
{
    public static int smethod_0( this DBFReader dbfreader_0, string string_0 )
    {
        return ( ( IEnumerable< DBFField > ) dbfreader_0.Fields ).IndexOf< DBFField >( new Func< DBFField, bool >( new Class25.Class26( )
    {
      string_0 = string_0
    }.method_0 ) );
    }

    public static Stream smethod_1(
    this System.Net.FtpClient.FtpClient ftpClient_0,
    string string_0,
    string string_1,
    string string_2 )
    {
        return ftpClient_0.smethod_2( Path.Combine( string_0, string_1 ) ).Save( string_2 );
    }

    public static Stream smethod_2( this System.Net.FtpClient.FtpClient ftpClient_0, string string_0 )
    {
        if( ftpClient_0 == null )
        {
            throw new ArgumentNullException( "client" );
        }

        MemoryStream memoryStream = new MemoryStream( );
        using( Stream stream = ftpClient_0.OpenRead( string_0 ) )
        {
            stream.CopyTo( ( Stream )memoryStream );
        }

        memoryStream.Position = 0L;
        return ( Stream ) memoryStream;
    }

    public static void smethod_3( this string string_0, string string_1 )
    {
        using( FileStream stream_0 = File.OpenRead( string_0 ) )
        {
            stream_0.smethod_4( string_1 );
        }
    }

    public static void smethod_4( this Stream stream_0, string string_0 )
    {
        if( stream_0 == null )
        {
            throw new ArgumentNullException( "stream" );
        }

        stream_0.Position = 0L;
        if( Directory.Exists( string_0 ) )
        {
            InteropHelper.BlockDeleteDir( string_0, true, 1000, 0 );
        }
        else
        {
            Directory.CreateDirectory( string_0 );
        }

        using( ZipArchive source = new ZipArchive( stream_0, ZipArchiveMode.Read, true ) )
        {
            source.ExtractToDirectory( string_0 );
        }
    }

    private sealed class Class26
    {
        public string string_0;

        internal bool method_0( DBFField dbffield_0 )
        {
            return dbffield_0.Name == this.string_0;
        }
    }
}
