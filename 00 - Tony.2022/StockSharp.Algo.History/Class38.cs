using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

[DebuggerNonUserCode]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
internal sealed class Class38
{
    private static ResourceManager resourceManager_0;
    private static CultureInfo cultureInfo_0;

    internal Class38( )
    {
    }

    internal static ResourceManager smethod_0( )
    {
        if( Class38.resourceManager_0 == null )
        {
            Class38.resourceManager_0 = new ResourceManager( "StockSharp.Algo.History.Properties.Resources", typeof( Class38 ).Assembly );
        }

        return Class38.resourceManager_0;
    }

    internal static CultureInfo smethod_1( )
    {
        return Class38.cultureInfo_0;
    }

    internal static void smethod_2( CultureInfo cultureInfo_1 )
    {
        Class38.cultureInfo_0 = cultureInfo_1;
    }

    internal static string smethod_3( )
    {
        return Class38.smethod_0( ).GetString( "DukasCopySecurities", Class38.cultureInfo_0 );
    }

    internal static string smethod_4( )
    {
        return Class38.smethod_0( ).GetString( "IgnoreCodes", Class38.cultureInfo_0 );
    }

    internal static string smethod_5( )
    {
        return Class38.smethod_0( ).GetString( "lci_2012_names", Class38.cultureInfo_0 );
    }

    internal static byte[] smethod_6( )
    {
        return ( byte[] ) Class38.smethod_0( ).GetObject( "Securities", Class38.cultureInfo_0 );
    }
}
