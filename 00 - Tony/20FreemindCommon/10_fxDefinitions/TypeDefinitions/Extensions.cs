using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography;
using System.Collections;
using fx.Collections;

namespace fx.Definitions
{
    public static class IsNullOrEmptyExtension
    {
        public static bool IsNullOrEmpty( this IEnumerable source )
        {
            if ( source != null )
            {
                foreach ( object obj in source )
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsNullOrEmpty<T>( this IEnumerable<T> source )
        {
            if ( source != null )
            {
                foreach ( T obj in source )
                {
                    return false;
                }
            }
            return true;
        }
    }

    public static class Strings
    {
        private static readonly PooledDictionary< int, string > _entityTable = new PooledDictionary< int, string >();

        private static readonly PooledDictionary< string, string > _USStateTable = new PooledDictionary< string, string >();

        /// <summary>
        /// Initializes the <see cref="Strings"/> class.
        /// </summary>
        static Strings( )
        {
            FillEntities( );
            FillUSStates( );
        }

        public static bool Matches( this string source, string compare )
        {
            return String.Equals( source, compare, StringComparison.InvariantCultureIgnoreCase );
        }

        public static bool MatchesTrimmed( this string source, string compare )
        {
            return String.Equals( source.Trim( ), compare.Trim( ), StringComparison.InvariantCultureIgnoreCase );
        }

        public static bool MatchesRegex( this string inputString, string matchPattern )
        {
            return Regex.IsMatch( inputString, matchPattern,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
        }

        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromEnd">The remove from end.</param>
        /// <returns></returns>
        public static string Chop( this string sourceString, int removeFromEnd )
        {
            string result = sourceString;
            if ( ( removeFromEnd > 0 ) && ( sourceString.Length > removeFromEnd - 1 ) )
                result = result.Remove( sourceString.Length - removeFromEnd, removeFromEnd );
            return result;
        }

        /// <summary>
        /// Strips the last specified chars from a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="backDownTo">The back down to.</param>
        /// <returns></returns>
        public static string Chop( this string sourceString, string backDownTo )
        {
            int removeDownTo = sourceString.LastIndexOf( backDownTo );
            int removeFromEnd = 0;
            if ( removeDownTo > 0 )
                removeFromEnd = sourceString.Length - removeDownTo;

            string result = sourceString;

            if ( sourceString.Length > removeFromEnd - 1 )
                result = result.Remove( removeDownTo, removeFromEnd );

            return result;
        }

        /// <summary>
        /// Plurals to singular.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string PluralToSingular( this string sourceString )
        {
            return sourceString.MakeSingular( );
        }

        /// <summary>
        /// Singulars to plural.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string SingularToPlural( this string sourceString )
        {
            return sourceString.MakePlural( );
        }

        /// <summary>
        /// Make plural when count is not one
        /// </summary>
        /// <param name="number">The number of things</param>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Pluralize( this int number, string sourceString )
        {
            if ( number == 1 )
                return String.Concat( number, " ", sourceString.MakeSingular( ) );
            return String.Concat( number, " ", sourceString.MakePlural( ) );
        }

        /// <summary>
        /// Removes the specified chars from the beginning of a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeFromBeginning">The remove from beginning.</param>
        /// <returns></returns>
        public static string Clip( this string sourceString, int removeFromBeginning )
        {
            string result = sourceString;
            if ( sourceString.Length > removeFromBeginning )
                result = result.Remove( 0, removeFromBeginning );
            return result;
        }

        /// <summary>
        /// Removes chars from the beginning of a string, up to the specified string
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removeUpTo">The remove up to.</param>
        /// <returns></returns>
        public static string Clip( this string sourceString, string removeUpTo )
        {
            int removeFromBeginning = sourceString.IndexOf( removeUpTo );
            string result = sourceString;

            if ( sourceString.Length > removeFromBeginning && removeFromBeginning > 0 )
                result = result.Remove( 0, removeFromBeginning );

            return result;
        }

        /// <summary>
        /// Strips the last char from a a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Chop( this string sourceString )
        {
            return Chop( sourceString, 1 );
        }

        /// <summary>
        /// Strips the last char from a a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Clip( this string sourceString )
        {
            return Clip( sourceString, 1 );
        }

        /// <summary>
        /// Fasts the replace.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns></returns>
        public static string FastReplace( this string original, string pattern, string replacement )
        {
            return FastReplace( original, pattern, replacement, StringComparison.InvariantCultureIgnoreCase );
        }

        /// <summary>
        /// Fasts the replace.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns></returns>
        public static string FastReplace( this string original, string pattern, string replacement,
                                         StringComparison comparisonType )
        {
            if ( original == null )
                return null;

            if ( String.IsNullOrEmpty( pattern ) )
                return original;

            int lenPattern = pattern.Length;
            int idxPattern = -1;
            int idxLast = 0;

            StringBuilder result = new StringBuilder();

            while ( true )
            {
                idxPattern = original.IndexOf( pattern, idxPattern + 1, comparisonType );

                if ( idxPattern < 0 )
                {
                    result.Append( original, idxLast, original.Length - idxLast );
                    break;
                }

                result.Append( original, idxLast, idxPattern - idxLast );
                result.Append( replacement );

                idxLast = idxPattern + lenPattern;
            }

            return result.ToString( );
        }

        /// <summary>
        /// Returns text that is located between the startText and endText tags.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="startText">The text from which to start the crop</param>
        /// <param name="endText">The endpoint of the crop</param>
        /// <returns></returns>
        public static string Crop( this string sourceString, string startText, string endText )
        {
            int startIndex = sourceString.IndexOf( startText, StringComparison.CurrentCultureIgnoreCase );
            if ( startIndex == -1 )
                return String.Empty;

            startIndex += startText.Length;
            int endIndex = sourceString.IndexOf( endText, startIndex, StringComparison.CurrentCultureIgnoreCase );
            if ( endIndex == -1 )
                return String.Empty;

            return sourceString.Substring( startIndex, endIndex - startIndex );
        }

        /// <summary>
        /// Removes excess white space in a string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string Squeeze( this string sourceString )
        {
            char[ ] delim =
            { ' '
            };
            string[ ] lines = sourceString.Split( delim, StringSplitOptions.RemoveEmptyEntries );
            StringBuilder sb = new StringBuilder();
            foreach ( string s in lines )
            {
                if ( !String.IsNullOrEmpty( s.Trim( ) ) )
                    sb.Append( s + " " );
            }
            //remove the last pipe
            string result = Chop( sb.ToString( ) );
            return result.Trim( );
        }

        /// <summary>
        /// Removes all non-alpha numeric characters in a string
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string ToAlphaNumericOnly( this string sourceString )
        {
            return Regex.Replace( sourceString, @"\W*", "" );
        }

        /// <summary>
        /// Creates a string array based on the words in a sentence
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string [ ] ToWords( this string sourceString )
        {
            string result = sourceString.Trim( );
            return result.Split( new [ ]
            { ' '
            }, StringSplitOptions.RemoveEmptyEntries );
        }

        /// <summary>
        /// Strips all HTML tags from a string
        /// </summary>
        /// <param name="htmlString">The HTML string.</param>
        /// <returns></returns>
        public static string StripHTML( this string htmlString )
        {
            return StripHTML( htmlString, String.Empty );
        }

        /// <summary>
        /// Strips all HTML tags from a string and replaces the tags with the specified replacement
        /// </summary>
        /// <param name="htmlString">The HTML string.</param>
        /// <param name="htmlPlaceHolder">The HTML place holder.</param>
        /// <returns></returns>
        public static string StripHTML( this string htmlString, string htmlPlaceHolder )
        {
            const string pattern = @"<(.|\n)*?>";
            string sOut = Regex.Replace( htmlString, pattern, htmlPlaceHolder );
            sOut = sOut.Replace( "&nbsp;", String.Empty );
            sOut = sOut.Replace( "&amp;", "&" );
            sOut = sOut.Replace( "&gt;", ">" );
            sOut = sOut.Replace( "&lt;", "<" );
            return sOut;
        }

        public static PooledList<string> FindMatches( this string source, string find )
        {
            Regex reg = new Regex(find, RegexOptions.IgnoreCase);

            PooledList< string > result = new PooledList< string >();
            foreach ( Match m in reg.Matches( source ) )
                result.Add( m.Value );
            return result;
        }

        /// <summary>
        /// Converts a generic PooledList collection to a single comma-delimitted string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public static string ToDelimitedList( this IEnumerable<string> list )
        {
            return ToDelimitedList( list, "," );
        }

        /// <summary>
        /// Converts a generic PooledList collection to a single string using the specified delimitter.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string ToDelimitedList( this IEnumerable<string> list, string delimiter )
        {
            StringBuilder sb = new StringBuilder();
            foreach ( string s in list )
                sb.Append( String.Concat( s, delimiter ) );
            string result = sb.ToString( );
            result = Chop( result );
            return result;
        }

        /// <summary>
        /// Strips the specified input.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="stripValue">The strip value.</param>
        /// <returns></returns>
        public static string Strip( this string sourceString, string stripValue )
        {
            if ( !String.IsNullOrEmpty( stripValue ) )
            {
                string[ ] replace = stripValue.Split( new[ ]
                { ','
                } );
                for ( int i = 0 ; i < replace.Length ; i++ )
                {
                    if ( !String.IsNullOrEmpty( sourceString ) )
                        sourceString = Regex.Replace( sourceString, replace [ i ], String.Empty );
                }
            }
            return sourceString;
        }

        /// <summary>
        /// Converts ASCII encoding to Unicode
        /// </summary>
        /// <param name="asciiCode">The ASCII code.</param>
        /// <returns></returns>
        public static string AsciiToUnicode( this int asciiCode )
        {
            Encoding ascii = Encoding.UTF32;
            char c = ( char ) asciiCode;
            Byte[ ] b = ascii.GetBytes( c.ToString( ) );
            return ascii.GetString( ( b ) );
        }

        /// <summary>
        /// Converts Text to HTML-encoded string
        /// </summary>
        /// <param name="textString">The text string.</param>
        /// <returns></returns>
        public static string TextToEntity( this string textString )
        {
            foreach ( KeyValuePair<int, string> key in _entityTable )
                textString = textString.Replace( AsciiToUnicode( key.Key ), key.Value );
            return textString.Replace( AsciiToUnicode( 38 ), "&amp;" );
        }

        /// <summary>
        /// Converts HTML-encoded bits to Text
        /// </summary>
        /// <param name="entityText">The entity text.</param>
        /// <returns></returns>
        public static string EntityToText( this string entityText )
        {
            entityText = entityText.Replace( "&amp;", "&" );
            foreach ( KeyValuePair<int, string> key in _entityTable )
                entityText = entityText.Replace( key.Value, AsciiToUnicode( key.Key ) );
            return entityText;
        }

        /// <summary>
        /// Formats the args using String.Format with the target string as a format string.
        /// </summary>
        /// <param name="fmt">The format string passed to String.Format</param>
        /// <param name="args">The args passed to String.Format</param>
        /// <returns></returns>
        public static string ToFormattedString( this string fmt, params object [ ] args )
        {
            return String.Format( fmt, args );
        }

        /// <summary>
        /// Strings to enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        public static T ToEnum<T>( this string Value )
        {
            T oOut = default( T );
            Type t = typeof( T );
            foreach ( FieldInfo fi in t.GetFields( ) )
            {
                if ( fi.Name.Matches( Value ) )
                    oOut = ( T ) fi.GetValue( null );
            }

            return oOut;
        }

        /// <summary>
        /// Fills the entities.
        /// </summary>
        private static void FillEntities( )
        {
            _entityTable.Add( 160, "&nbsp;" );
            _entityTable.Add( 161, "&iexcl;" );
            _entityTable.Add( 162, "&cent;" );
            _entityTable.Add( 163, "&pound;" );
            _entityTable.Add( 164, "&curren;" );
            _entityTable.Add( 165, "&yen;" );
            _entityTable.Add( 166, "&brvbar;" );
            _entityTable.Add( 167, "&sect;" );
            _entityTable.Add( 168, "&uml;" );
            _entityTable.Add( 169, "&copy;" );
            _entityTable.Add( 170, "&ordf;" );
            _entityTable.Add( 171, "&laquo;" );
            _entityTable.Add( 172, "&not;" );
            _entityTable.Add( 173, "&shy;" );
            _entityTable.Add( 174, "&reg;" );
            _entityTable.Add( 175, "&macr;" );
            _entityTable.Add( 176, "&deg;" );
            _entityTable.Add( 177, "&plusmn;" );
            _entityTable.Add( 178, "&sup2;" );
            _entityTable.Add( 179, "&sup3;" );
            _entityTable.Add( 180, "&acute;" );
            _entityTable.Add( 181, "&micro;" );
            _entityTable.Add( 182, "&para;" );
            _entityTable.Add( 183, "&middot;" );
            _entityTable.Add( 184, "&cedil;" );
            _entityTable.Add( 185, "&sup1;" );
            _entityTable.Add( 186, "&ordm;" );
            _entityTable.Add( 187, "&raquo;" );
            _entityTable.Add( 188, "&frac14;" );
            _entityTable.Add( 189, "&frac12;" );
            _entityTable.Add( 190, "&frac34;" );
            _entityTable.Add( 191, "&iquest;" );
            _entityTable.Add( 192, "&Agrave;" );
            _entityTable.Add( 193, "&Aacute;" );
            _entityTable.Add( 194, "&Acirc;" );
            _entityTable.Add( 195, "&Atilde;" );
            _entityTable.Add( 196, "&Auml;" );
            _entityTable.Add( 197, "&Aring;" );
            _entityTable.Add( 198, "&AElig;" );
            _entityTable.Add( 199, "&Ccedil;" );
            _entityTable.Add( 200, "&Egrave;" );
            _entityTable.Add( 201, "&Eacute;" );
            _entityTable.Add( 202, "&Ecirc;" );
            _entityTable.Add( 203, "&Euml;" );
            _entityTable.Add( 204, "&Igrave;" );
            _entityTable.Add( 205, "&Iacute;" );
            _entityTable.Add( 206, "&Icirc;" );
            _entityTable.Add( 207, "&Iuml;" );
            _entityTable.Add( 208, "&ETH;" );
            _entityTable.Add( 209, "&Ntilde;" );
            _entityTable.Add( 210, "&Ograve;" );
            _entityTable.Add( 211, "&Oacute;" );
            _entityTable.Add( 212, "&Ocirc;" );
            _entityTable.Add( 213, "&Otilde;" );
            _entityTable.Add( 214, "&Ouml;" );
            _entityTable.Add( 215, "&times;" );
            _entityTable.Add( 216, "&Oslash;" );
            _entityTable.Add( 217, "&Ugrave;" );
            _entityTable.Add( 218, "&Uacute;" );
            _entityTable.Add( 219, "&Ucirc;" );
            _entityTable.Add( 220, "&Uuml;" );
            _entityTable.Add( 221, "&Yacute;" );
            _entityTable.Add( 222, "&THORN;" );
            _entityTable.Add( 223, "&szlig;" );
            _entityTable.Add( 224, "&agrave;" );
            _entityTable.Add( 225, "&aacute;" );
            _entityTable.Add( 226, "&acirc;" );
            _entityTable.Add( 227, "&atilde;" );
            _entityTable.Add( 228, "&auml;" );
            _entityTable.Add( 229, "&aring;" );
            _entityTable.Add( 230, "&aelig;" );
            _entityTable.Add( 231, "&ccedil;" );
            _entityTable.Add( 232, "&egrave;" );
            _entityTable.Add( 233, "&eacute;" );
            _entityTable.Add( 234, "&ecirc;" );
            _entityTable.Add( 235, "&euml;" );
            _entityTable.Add( 236, "&igrave;" );
            _entityTable.Add( 237, "&iacute;" );
            _entityTable.Add( 238, "&icirc;" );
            _entityTable.Add( 239, "&iuml;" );
            _entityTable.Add( 240, "&eth;" );
            _entityTable.Add( 241, "&ntilde;" );
            _entityTable.Add( 242, "&ograve;" );
            _entityTable.Add( 243, "&oacute;" );
            _entityTable.Add( 244, "&ocirc;" );
            _entityTable.Add( 245, "&otilde;" );
            _entityTable.Add( 246, "&ouml;" );
            _entityTable.Add( 247, "&divide;" );
            _entityTable.Add( 248, "&oslash;" );
            _entityTable.Add( 249, "&ugrave;" );
            _entityTable.Add( 250, "&uacute;" );
            _entityTable.Add( 251, "&ucirc;" );
            _entityTable.Add( 252, "&uuml;" );
            _entityTable.Add( 253, "&yacute;" );
            _entityTable.Add( 254, "&thorn;" );
            _entityTable.Add( 255, "&yuml;" );
            _entityTable.Add( 402, "&fnof;" );
            _entityTable.Add( 913, "&Alpha;" );
            _entityTable.Add( 914, "&Beta;" );
            _entityTable.Add( 915, "&Gamma;" );
            _entityTable.Add( 916, "&Delta;" );
            _entityTable.Add( 917, "&Epsilon;" );
            _entityTable.Add( 918, "&Zeta;" );
            _entityTable.Add( 919, "&Eta;" );
            _entityTable.Add( 920, "&Theta;" );
            _entityTable.Add( 921, "&Iota;" );
            _entityTable.Add( 922, "&Kappa;" );
            _entityTable.Add( 923, "&Lambda;" );
            _entityTable.Add( 924, "&Mu;" );
            _entityTable.Add( 925, "&Nu;" );
            _entityTable.Add( 926, "&Xi;" );
            _entityTable.Add( 927, "&Omicron;" );
            _entityTable.Add( 928, "&Pi;" );
            _entityTable.Add( 929, "&Rho;" );
            _entityTable.Add( 931, "&Sigma;" );
            _entityTable.Add( 932, "&Tau;" );
            _entityTable.Add( 933, "&Upsilon;" );
            _entityTable.Add( 934, "&Phi;" );
            _entityTable.Add( 935, "&Chi;" );
            _entityTable.Add( 936, "&Psi;" );
            _entityTable.Add( 937, "&Omega;" );
            _entityTable.Add( 945, "&alpha;" );
            _entityTable.Add( 946, "&beta;" );
            _entityTable.Add( 947, "&gamma;" );
            _entityTable.Add( 948, "&delta;" );
            _entityTable.Add( 949, "&epsilon;" );
            _entityTable.Add( 950, "&zeta;" );
            _entityTable.Add( 951, "&eta;" );
            _entityTable.Add( 952, "&theta;" );
            _entityTable.Add( 953, "&iota;" );
            _entityTable.Add( 954, "&kappa;" );
            _entityTable.Add( 955, "&lambda;" );
            _entityTable.Add( 956, "&mu;" );
            _entityTable.Add( 957, "&nu;" );
            _entityTable.Add( 958, "&xi;" );
            _entityTable.Add( 959, "&omicron;" );
            _entityTable.Add( 960, "&pi;" );
            _entityTable.Add( 961, "&rho;" );
            _entityTable.Add( 962, "&sigmaf;" );
            _entityTable.Add( 963, "&sigma;" );
            _entityTable.Add( 964, "&tau;" );
            _entityTable.Add( 965, "&upsilon;" );
            _entityTable.Add( 966, "&phi;" );
            _entityTable.Add( 967, "&chi;" );
            _entityTable.Add( 968, "&psi;" );
            _entityTable.Add( 969, "&omega;" );
            _entityTable.Add( 977, "&thetasym;" );
            _entityTable.Add( 978, "&upsih;" );
            _entityTable.Add( 982, "&piv;" );
            _entityTable.Add( 8226, "&bull;" );
            _entityTable.Add( 8230, "&hellip;" );
            _entityTable.Add( 8242, "&prime;" );
            _entityTable.Add( 8243, "&Prime;" );
            _entityTable.Add( 8254, "&oline;" );
            _entityTable.Add( 8260, "&frasl;" );
            _entityTable.Add( 8472, "&weierp;" );
            _entityTable.Add( 8465, "&image;" );
            _entityTable.Add( 8476, "&real;" );
            _entityTable.Add( 8482, "&trade;" );
            _entityTable.Add( 8501, "&alefsym;" );
            _entityTable.Add( 8592, "&larr;" );
            _entityTable.Add( 8593, "&uarr;" );
            _entityTable.Add( 8594, "&rarr;" );
            _entityTable.Add( 8595, "&darr;" );
            _entityTable.Add( 8596, "&harr;" );
            _entityTable.Add( 8629, "&crarr;" );
            _entityTable.Add( 8656, "&lArr;" );
            _entityTable.Add( 8657, "&uArr;" );
            _entityTable.Add( 8658, "&rArr;" );
            _entityTable.Add( 8659, "&dArr;" );
            _entityTable.Add( 8660, "&hArr;" );
            _entityTable.Add( 8704, "&forall;" );
            _entityTable.Add( 8706, "&part;" );
            _entityTable.Add( 8707, "&exist;" );
            _entityTable.Add( 8709, "&empty;" );
            _entityTable.Add( 8711, "&nabla;" );
            _entityTable.Add( 8712, "&isin;" );
            _entityTable.Add( 8713, "&notin;" );
            _entityTable.Add( 8715, "&ni;" );
            _entityTable.Add( 8719, "&prod;" );
            _entityTable.Add( 8721, "&sum;" );
            _entityTable.Add( 8722, "&minus;" );
            _entityTable.Add( 8727, "&lowast;" );
            _entityTable.Add( 8730, "&radic;" );
            _entityTable.Add( 8733, "&prop;" );
            _entityTable.Add( 8734, "&infin;" );
            _entityTable.Add( 8736, "&ang;" );
            _entityTable.Add( 8743, "&and;" );
            _entityTable.Add( 8744, "&or;" );
            _entityTable.Add( 8745, "&cap;" );
            _entityTable.Add( 8746, "&cup;" );
            _entityTable.Add( 8747, "&int;" );
            _entityTable.Add( 8756, "&there4;" );
            _entityTable.Add( 8764, "&sim;" );
            _entityTable.Add( 8773, "&cong;" );
            _entityTable.Add( 8776, "&asymp;" );
            _entityTable.Add( 8800, "&ne;" );
            _entityTable.Add( 8801, "&equiv;" );
            _entityTable.Add( 8804, "&le;" );
            _entityTable.Add( 8805, "&ge;" );
            _entityTable.Add( 8834, "&sub;" );
            _entityTable.Add( 8835, "&sup;" );
            _entityTable.Add( 8836, "&nsub;" );
            _entityTable.Add( 8838, "&sube;" );
            _entityTable.Add( 8839, "&supe;" );
            _entityTable.Add( 8853, "&oplus;" );
            _entityTable.Add( 8855, "&otimes;" );
            _entityTable.Add( 8869, "&perp;" );
            _entityTable.Add( 8901, "&sdot;" );
            _entityTable.Add( 8968, "&lceil;" );
            _entityTable.Add( 8969, "&rceil;" );
            _entityTable.Add( 8970, "&lfloor;" );
            _entityTable.Add( 8971, "&rfloor;" );
            _entityTable.Add( 9001, "&lang;" );
            _entityTable.Add( 9002, "&rang;" );
            _entityTable.Add( 9674, "&loz;" );
            _entityTable.Add( 9824, "&spades;" );
            _entityTable.Add( 9827, "&clubs;" );
            _entityTable.Add( 9829, "&hearts;" );
            _entityTable.Add( 9830, "&diams;" );
            _entityTable.Add( 34, "&quot;" );
            //_entityTable.Add(38, "&amp;");
            _entityTable.Add( 60, "&lt;" );
            _entityTable.Add( 62, "&gt;" );
            _entityTable.Add( 338, "&OElig;" );
            _entityTable.Add( 339, "&oelig;" );
            _entityTable.Add( 352, "&Scaron;" );
            _entityTable.Add( 353, "&scaron;" );
            _entityTable.Add( 376, "&Yuml;" );
            _entityTable.Add( 710, "&circ;" );
            _entityTable.Add( 732, "&tilde;" );
            _entityTable.Add( 8194, "&ensp;" );
            _entityTable.Add( 8195, "&emsp;" );
            _entityTable.Add( 8201, "&thinsp;" );
            _entityTable.Add( 8204, "&zwnj;" );
            _entityTable.Add( 8205, "&zwj;" );
            _entityTable.Add( 8206, "&lrm;" );
            _entityTable.Add( 8207, "&rlm;" );
            _entityTable.Add( 8211, "&ndash;" );
            _entityTable.Add( 8212, "&mdash;" );
            _entityTable.Add( 8216, "&lsquo;" );
            _entityTable.Add( 8217, "&rsquo;" );
            _entityTable.Add( 8218, "&sbquo;" );
            _entityTable.Add( 8220, "&ldquo;" );
            _entityTable.Add( 8221, "&rdquo;" );
            _entityTable.Add( 8222, "&bdquo;" );
            _entityTable.Add( 8224, "&dagger;" );
            _entityTable.Add( 8225, "&Dagger;" );
            _entityTable.Add( 8240, "&permil;" );
            _entityTable.Add( 8249, "&lsaquo;" );
            _entityTable.Add( 8250, "&rsaquo;" );
            _entityTable.Add( 8364, "&euro;" );
        }

        /// <summary>
        /// Converts US State Name to it's two-character abbreviation. Returns null if the state name was not found.
        /// </summary>
        /// <param name="stateName">US State Name (ie Texas)</param>
        /// <returns></returns>
        public static string USStateNameToAbbrev( string stateName )
        {
            stateName = stateName.ToUpper( );
            foreach ( KeyValuePair<string, string> key in _USStateTable )
            {
                if ( stateName == key.Key )
                    return key.Value;
            }
            return null;
        }

        /// <summary>
        /// Converts a two-character US State Abbreviation to it's official Name Returns null if the abbreviation was not found.
        /// </summary>
        /// <param name="stateAbbrev">US State Name (ie Texas)</param>
        /// <returns></returns>
        public static string USStateAbbrevToName( string stateAbbrev )
        {
            stateAbbrev = stateAbbrev.ToUpper( );
            foreach ( KeyValuePair<string, string> key in _USStateTable )
            {
                if ( stateAbbrev == key.Value )
                    return key.Key;
            }
            return null;
        }

        /// <summary>
        /// Fills the US States.
        /// </summary>
        private static void FillUSStates( )
        {
            _USStateTable.Add( "ALABAMA", "AL" );
            _USStateTable.Add( "ALASKA", "AK" );
            _USStateTable.Add( "AMERICAN SAMOA", "AS" );
            _USStateTable.Add( "ARIZONA ", "AZ" );
            _USStateTable.Add( "ARKANSAS", "AR" );
            _USStateTable.Add( "CALIFORNIA ", "CA" );
            _USStateTable.Add( "COLORADO ", "CO" );
            _USStateTable.Add( "CONNECTICUT", "CT" );
            _USStateTable.Add( "DELAWARE", "DE" );
            _USStateTable.Add( "DISTRICT OF COLUMBIA", "DC" );
            _USStateTable.Add( "FEDERATED STATES OF MICRONESIA", "FM" );
            _USStateTable.Add( "FLORIDA", "FL" );
            _USStateTable.Add( "GEORGIA", "GA" );
            _USStateTable.Add( "GUAM ", "GU" );
            _USStateTable.Add( "HAWAII", "HI" );
            _USStateTable.Add( "IDAHO", "ID" );
            _USStateTable.Add( "ILLINOIS", "IL" );
            _USStateTable.Add( "INDIANA", "IN" );
            _USStateTable.Add( "IOWA", "IA" );
            _USStateTable.Add( "KANSAS", "KS" );
            _USStateTable.Add( "KENTUCKY", "KY" );
            _USStateTable.Add( "LOUISIANA", "LA" );
            _USStateTable.Add( "MAINE", "ME" );
            _USStateTable.Add( "MARSHALL ISLANDS", "MH" );
            _USStateTable.Add( "MARYLAND", "MD" );
            _USStateTable.Add( "MASSACHUSETTS", "MA" );
            _USStateTable.Add( "MICHIGAN", "MI" );
            _USStateTable.Add( "MINNESOTA", "MN" );
            _USStateTable.Add( "MISSISSIPPI", "MS" );
            _USStateTable.Add( "MISSOURI", "MO" );
            _USStateTable.Add( "MONTANA", "MT" );
            _USStateTable.Add( "NEBRASKA", "NE" );
            _USStateTable.Add( "NEVADA", "NV" );
            _USStateTable.Add( "NEW HAMPSHIRE", "NH" );
            _USStateTable.Add( "NEW JERSEY", "NJ" );
            _USStateTable.Add( "NEW MEXICO", "NM" );
            _USStateTable.Add( "NEW YORK", "NY" );
            _USStateTable.Add( "NORTH CAROLINA", "NC" );
            _USStateTable.Add( "NORTH DAKOTA", "ND" );
            _USStateTable.Add( "NORTHERN MARIANA ISLANDS", "MP" );
            _USStateTable.Add( "OHIO", "OH" );
            _USStateTable.Add( "OKLAHOMA", "OK" );
            _USStateTable.Add( "OREGON", "OR" );
            _USStateTable.Add( "PALAU", "PW" );
            _USStateTable.Add( "PENNSYLVANIA", "PA" );
            _USStateTable.Add( "PUERTO RICO", "PR" );
            _USStateTable.Add( "RHODE ISLAND", "RI" );
            _USStateTable.Add( "SOUTH CAROLINA", "SC" );
            _USStateTable.Add( "SOUTH DAKOTA", "SD" );
            _USStateTable.Add( "TENNESSEE", "TN" );
            _USStateTable.Add( "TEXAS", "TX" );
            _USStateTable.Add( "UTAH", "UT" );
            _USStateTable.Add( "VERMONT", "VT" );
            _USStateTable.Add( "VIRGIN ISLANDS", "VI" );
            _USStateTable.Add( "VIRGINIA ", "VA" );
            _USStateTable.Add( "WASHINGTON", "WA" );
            _USStateTable.Add( "WEST VIRGINIA", "WV" );
            _USStateTable.Add( "WISCONSIN", "WI" );
            _USStateTable.Add( "WYOMING", "WY" );
        }
    }

    public static class Inflector
    {
        private static readonly PooledList<InflectorRule> _plurals = new PooledList<InflectorRule>();
        private static readonly PooledList<InflectorRule> _singulars = new PooledList<InflectorRule>();
        private static readonly PooledList<string> _uncountables = new PooledList<string>();

        /// <summary>
        /// Initializes the <see cref="Inflector"/> class.
        /// </summary>
        static Inflector( )
        {
            AddPluralRule( "$", "s" );
            AddPluralRule( "s$", "s" );
            AddPluralRule( "(ax|test)is$", "$1es" );
            AddPluralRule( "(octop|vir)us$", "$1i" );
            AddPluralRule( "(alias|status)$", "$1es" );
            AddPluralRule( "(bu)s$", "$1ses" );
            AddPluralRule( "(buffal|tomat)o$", "$1oes" );
            AddPluralRule( "([ti])um$", "$1a" );
            AddPluralRule( "sis$", "ses" );
            AddPluralRule( "(?:([^f])fe|([lr])f)$", "$1$2ves" );
            AddPluralRule( "(hive)$", "$1s" );
            AddPluralRule( "([^aeiouy]|qu)y$", "$1ies" );
            AddPluralRule( "(x|ch|ss|sh)$", "$1es" );
            AddPluralRule( "(matr|vert|ind)ix|ex$", "$1ices" );
            AddPluralRule( "([m|l])ouse$", "$1ice" );
            AddPluralRule( "^(ox)$", "$1en" );
            AddPluralRule( "(quiz)$", "$1zes" );

            AddSingularRule( "s$", String.Empty );
            AddSingularRule( "ss$", "ss" );
            AddSingularRule( "(n)ews$", "$1ews" );
            AddSingularRule( "([ti])a$", "$1um" );
            AddSingularRule( "((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis" );
            AddSingularRule( "(^analy)ses$", "$1sis" );
            AddSingularRule( "([^f])ves$", "$1fe" );
            AddSingularRule( "(hive)s$", "$1" );
            AddSingularRule( "(tive)s$", "$1" );
            AddSingularRule( "([lr])ves$", "$1f" );
            AddSingularRule( "([^aeiouy]|qu)ies$", "$1y" );
            AddSingularRule( "(s)eries$", "$1eries" );
            AddSingularRule( "(m)ovies$", "$1ovie" );
            AddSingularRule( "(x|ch|ss|sh)es$", "$1" );
            AddSingularRule( "([m|l])ice$", "$1ouse" );
            AddSingularRule( "(bus)es$", "$1" );
            AddSingularRule( "(o)es$", "$1" );
            AddSingularRule( "(shoe)s$", "$1" );
            AddSingularRule( "(cris|ax|test)es$", "$1is" );
            AddSingularRule( "(octop|vir)i$", "$1us" );
            AddSingularRule( "(alias|status)$", "$1" );
            AddSingularRule( "(alias|status)es$", "$1" );
            AddSingularRule( "^(ox)en", "$1" );
            AddSingularRule( "(vert|ind)ices$", "$1ex" );
            AddSingularRule( "(matr)ices$", "$1ix" );
            AddSingularRule( "(quiz)zes$", "$1" );

            AddIrregularRule( "person", "people" );
            AddIrregularRule( "man", "men" );
            AddIrregularRule( "child", "children" );
            AddIrregularRule( "sex", "sexes" );
            AddIrregularRule( "tax", "taxes" );
            AddIrregularRule( "move", "moves" );

            AddUnknownCountRule( "equipment" );
            AddUnknownCountRule( "information" );
            AddUnknownCountRule( "rice" );
            AddUnknownCountRule( "money" );
            AddUnknownCountRule( "species" );
            AddUnknownCountRule( "series" );
            AddUnknownCountRule( "fish" );
            AddUnknownCountRule( "sheep" );
        }

        /// <summary>
        /// Adds the irregular rule.
        /// </summary>
        /// <param name="singular">The singular.</param>
        /// <param name="plural">The plural.</param>
        private static void AddIrregularRule( string singular, string plural )
        {
            AddPluralRule( String.Concat( "(", singular [ 0 ], ")", singular.Substring( 1 ), "$" ),
                String.Concat( "$1", plural.Substring( 1 ) ) );
            AddSingularRule( String.Concat( "(", plural [ 0 ], ")", plural.Substring( 1 ), "$" ),
                String.Concat( "$1", singular.Substring( 1 ) ) );
        }

        /// <summary>
        /// Adds the unknown count rule.
        /// </summary>
        /// <param name="word">The word.</param>
        private static void AddUnknownCountRule( string word )
        {
            _uncountables.Add( word.ToLower( ) );
        }

        /// <summary>
        /// Adds the plural rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="replacement">The replacement.</param>
        private static void AddPluralRule( string rule, string replacement )
        {
            _plurals.Add( new InflectorRule( rule, replacement ) );
        }

        /// <summary>
        /// Adds the singular rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="replacement">The replacement.</param>
        private static void AddSingularRule( string rule, string replacement )
        {
            _singulars.Add( new InflectorRule( rule, replacement ) );
        }

        /// <summary>
        /// Makes the plural.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string MakePlural( this string word )
        {
            return ApplyRules( _plurals, word );
        }

        /// <summary>
        /// Makes the singular.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string MakeSingular( this string word )
        {
            return ApplyRules( _singulars, word );
        }

        /// <summary>
        /// Applies the rules.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        private static string ApplyRules( IList<InflectorRule> rules, string word )
        {
            string result = word;
            if ( !_uncountables.Contains( word.ToLower( ) ) )
            {
                for ( int i = rules.Count - 1 ; i >= 0 ; i-- )
                {
                    string currentPass = rules[i].Apply(word);
                    if ( currentPass != null )
                    {
                        result = currentPass;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Converts the string to title case.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string ToTitleCase( this string word )
        {
            return Regex.Replace( ToHumanCase( AddUnderscores( word ) ), @"\b([a-z])",
                match => match.Captures [ 0 ].Value.ToUpper( ) );
        }

        /// <summary>
        /// Converts the string to human case.
        /// </summary>
        /// <param name="lowercaseAndUnderscoredWord">The lowercase and underscored word.</param>
        /// <returns></returns>
        public static string ToHumanCase( this string lowercaseAndUnderscoredWord )
        {
            return MakeInitialCaps( Regex.Replace( lowercaseAndUnderscoredWord, @"_", " " ) );
        }

        /// <summary>
        /// Convert string to proper case
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string ToProper( this string sourceString )
        {
            string propertyName = sourceString.ToPascalCase();
            return propertyName;
        }

        /// <summary>
        /// Converts the string to pascal case.
        /// </summary>
        /// <param name="lowercaseAndUnderscoredWord">The lowercase and underscored word.</param>
        /// <returns></returns>
        public static string ToPascalCase( this string lowercaseAndUnderscoredWord )
        {
            return ToPascalCase( lowercaseAndUnderscoredWord, true );
        }

        /// <summary>
        /// Converts text to pascal case...
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="removeUnderscores">if set to <c>true</c> [remove underscores].</param>
        /// <returns></returns>
        public static string ToPascalCase( this string text, bool removeUnderscores )
        {
            if ( String.IsNullOrEmpty( text ) )
                return text;

            text = text.Replace( "_", " " );
            string joinString = removeUnderscores ? String.Empty : "_";
            string[] words = text.Split(' ');
            if ( words.Length > 1 || words [ 0 ].IsUpperCase( ) )
            {
                for ( int i = 0 ; i < words.Length ; i++ )
                {
                    if ( words [ i ].Length > 0 )
                    {
                        string word = words[i];
                        string restOfWord = word.Substring(1);

                        if ( restOfWord.IsUpperCase( ) )
                            restOfWord = restOfWord.ToLower( CultureInfo.CurrentUICulture );

                        char firstChar = char.ToUpper(word[0], CultureInfo.CurrentUICulture);
                        words [ i ] = String.Concat( firstChar, restOfWord );
                    }
                }
                return String.Join( joinString, words );
            }
            return String.Concat( words [ 0 ].Substring( 0, 1 ).ToUpper( CultureInfo.CurrentUICulture ), words [ 0 ].Substring( 1 ) );
        }

        /// <summary>
        /// Converts the string to camel case.
        /// </summary>
        /// <param name="lowercaseAndUnderscoredWord">The lowercase and underscored word.</param>
        /// <returns></returns>
        public static string ToCamelCase( this string lowercaseAndUnderscoredWord )
        {
            return MakeInitialLowerCase( ToPascalCase( lowercaseAndUnderscoredWord ) );
        }

        /// <summary>
        /// Adds the underscores.
        /// </summary>
        /// <param name="pascalCasedWord">The pascal cased word.</param>
        /// <returns></returns>
        public static string AddUnderscores( this string pascalCasedWord )
        {
            return
                Regex.Replace(
                    Regex.Replace( Regex.Replace( pascalCasedWord, @"([A-Z]+)([A-Z][a-z])", "$1_$2" ), @"([a-z\d])([A-Z])",
                        "$1_$2" ), @"[-\s]", "_" ).ToLower( );
        }

        /// <summary>
        /// Makes the initial caps.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string MakeInitialCaps( this string word )
        {
            return String.Concat( word.Substring( 0, 1 ).ToUpper( ), word.Substring( 1 ).ToLower( ) );
        }

        /// <summary>
        /// Makes the initial lower case.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static string MakeInitialLowerCase( this string word )
        {
            return String.Concat( word.Substring( 0, 1 ).ToLower( ), word.Substring( 1 ) );
        }

        /// <summary>
        /// Adds the ordinal suffix.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string AddOrdinalSuffix( this string number )
        {
            if ( number.IsStringNumeric( ) )
            {
                int n = int.Parse(number);
                int nMod100 = n % 100;

                if ( nMod100 >= 11 && nMod100 <= 13 )
                    return String.Concat( number, "th" );

                switch ( n % 10 )
                {
                    case 1:
                    return String.Concat( number, "st" );
                    case 2:
                    return String.Concat( number, "nd" );
                    case 3:
                    return String.Concat( number, "rd" );
                    default:
                    return String.Concat( number, "th" );
                }
            }
            return number;
        }

        /// <summary>
        /// Converts the underscores to dashes.
        /// </summary>
        /// <param name="underscoredWord">The underscored word.</param>
        /// <returns></returns>
        public static string ConvertUnderscoresToDashes( this string underscoredWord )
        {
            return underscoredWord.Replace( '_', '-' );
        }


        #region Nested type: InflectorRule

        /// <summary>
        /// Summary for the InflectorRule class
        /// </summary>
        private class InflectorRule
        {
            /// <summary>
            /// 
            /// </summary>
            public readonly Regex regex;

            /// <summary>
            /// 
            /// </summary>
            public readonly string replacement;

            /// <summary>
            /// Initializes a new instance of the <see cref="InflectorRule"/> class.
            /// </summary>
            /// <param name="regexPattern">The regex pattern.</param>
            /// <param name="replacementText">The replacement text.</param>
            public InflectorRule( string regexPattern, string replacementText )
            {
                regex = new Regex( regexPattern, RegexOptions.IgnoreCase );
                replacement = replacementText;
            }

            /// <summary>
            /// Applies the specified word.
            /// </summary>
            /// <param name="word">The word.</param>
            /// <returns></returns>
            public string Apply( string word )
            {
                if ( !regex.IsMatch( word ) )
                    return null;

                string replace = regex.Replace(word, replacement);
                if ( word == word.ToUpper( ) )
                    replace = replace.ToUpper( );

                return replace;
            }
        }



        #endregion
    }


    public static class Validation
    {
        /// <summary>
        /// Determines whether the specified eval string contains only alpha characters.
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified eval string is alpha; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlpha( this string evalString )
        {
            return !Regex.IsMatch( evalString, RegexPattern.ALPHA );
        }

        /// <summary>
        /// Determines whether the specified eval string contains only alphanumeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric( this string evalString )
        {
            return !Regex.IsMatch( evalString, RegexPattern.ALPHA_NUMERIC );
        }

        /// <summary>
        /// Determines whether the specified eval string contains only alphanumeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <param name="allowSpaces">if set to <c>true</c> [allow spaces].</param>
        /// <returns>
        /// 	<c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric( this string evalString, bool allowSpaces )
        {
            if ( allowSpaces )
                return !Regex.IsMatch( evalString, RegexPattern.ALPHA_NUMERIC_SPACE );
            return IsAlphaNumeric( evalString );
        }

        /// <summary>
        /// Determines whether the specified eval string contains only numeric characters
        /// </summary>
        /// <param name="evalString">The eval string.</param>
        /// <returns>
        /// 	<c>true</c> if the string is numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumeric( this string evalString )
        {
            return !Regex.IsMatch( evalString, RegexPattern.NUMERIC );
        }

        /// <summary>
        /// Determines whether the specified email address string is valid based on regular expression evaluation.
        /// </summary>
        /// <param name="emailAddressString">The email address string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified email address is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmail( this string emailAddressString )
        {
            return Regex.IsMatch( emailAddressString, RegexPattern.EMAIL );
        }

        /// <summary>
        /// Determines whether the specified string is lower case.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified string is lower case; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLowerCase( this string inputString )
        {
            return Regex.IsMatch( inputString, RegexPattern.LOWER_CASE );
        }

        /// <summary>
        /// Determines whether the specified string is upper case.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>
        /// 	<c>true</c> if the specified string is upper case; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUpperCase( this string inputString )
        {
            return Regex.IsMatch( inputString, RegexPattern.UPPER_CASE );
        }

        /// <summary>
        /// Determines whether the specified string is a valid GUID.
        /// </summary>
        /// <param name="guid">The GUID.</param>
        /// <returns>
        /// 	<c>true</c> if the specified string is a valid GUID; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGuid( this string guid )
        {
            return Regex.IsMatch( guid, RegexPattern.GUID );
        }

        /// <summary>
        /// Determines whether the specified string is a valid US Zip Code, using either 5 or 5+4 format.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>
        /// 	<c>true</c> if it is a valid zip code; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZIPCodeAny( this string zipCode )
        {
            return Regex.IsMatch( zipCode, RegexPattern.US_ZIPCODE_PLUS_FOUR_OPTIONAL );
        }

        /// <summary>
        /// Determines whether the specified string is a valid US Zip Code, using the 5 digit format.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>
        /// 	<c>true</c> if it is a valid zip code; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZIPCodeFive( this string zipCode )
        {
            return Regex.IsMatch( zipCode, RegexPattern.US_ZIPCODE );
        }

        /// <summary>
        /// Determines whether the specified string is a valid US Zip Code, using the 5+4 format.
        /// </summary>
        /// <param name="zipCode">The zip code.</param>
        /// <returns>
        /// 	<c>true</c> if it is a valid zip code; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsZIPCodeFivePlusFour( this string zipCode )
        {
            return Regex.IsMatch( zipCode, RegexPattern.US_ZIPCODE_PLUS_FOUR );
        }

        /// <summary>
        /// Determines whether the specified string is a valid Social Security number. Dashes are optional.
        /// </summary>
        /// <param name="socialSecurityNumber">The Social Security Number</param>
        /// <returns>
        /// 	<c>true</c> if it is a valid Social Security number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSocialSecurityNumber( this string socialSecurityNumber )
        {
            return Regex.IsMatch( socialSecurityNumber, RegexPattern.SOCIAL_SECURITY );
        }

        /// <summary>
        /// Determines whether the specified string is a valid IP address.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIPAddress( this string ipAddress )
        {
            return Regex.IsMatch( ipAddress, RegexPattern.IP_ADDRESS );
        }

        /// <summary>
        /// Determines whether the specified string is a valid US phone number using the referenced regex string.
        /// </summary>
        /// <param name="telephoneNumber">The telephone number.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUSTelephoneNumber( this string telephoneNumber )
        {
            return Regex.IsMatch( telephoneNumber, RegexPattern.US_TELEPHONE );
        }

        /// <summary>
        /// Determines whether the specified string is a valid currency string using the referenced regex string.
        /// </summary>
        /// <param name="currency">The currency string.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUSCurrency( this string currency )
        {
            return Regex.IsMatch( currency, RegexPattern.US_CURRENCY );
        }

        /// <summary>
        /// Determines whether the specified string is a valid URL string using the referenced regex string.
        /// </summary>
        /// <param name="url">The URL string.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsURL( this string url )
        {
            return Regex.IsMatch( url, RegexPattern.URL );
        }

        /// <summary>
        /// Determines whether the specified string is consider a strong password based on the supplied string.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>
        /// 	<c>true</c> if strong; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStrongPassword( this string password )
        {
            return Regex.IsMatch( password, RegexPattern.STRONG_PASSWORD );
        }


        #region Credit Cards

        /// <summary>
        /// Determines whether the specified string is a valid credit, based on matching any one of the eight credit card strings
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardAny( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_AMERICAN_EXPRESS ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_CARTE_BLANCHE ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_DINERS_CLUB ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_DISCOVER ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_EN_ROUTE ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_JCB ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_MASTER_CARD ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_VISA );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is an American Express, Discover, MasterCard, or Visa
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardBigFour( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_AMERICAN_EXPRESS ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_DISCOVER ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_MASTER_CARD ) ||
                       Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_VISA );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is an American Express card
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardAmericanExpress( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_AMERICAN_EXPRESS );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is an Carte Blanche card
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardCarteBlanche( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_CARTE_BLANCHE );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is an Diner's Club card
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardDinersClub( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_DINERS_CLUB );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is a Discover card
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardDiscover( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_DISCOVER );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is an En Route card
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardEnRoute( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_EN_ROUTE );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is an JCB card
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardJCB( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_JCB );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is a Master Card credit card
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardMasterCard( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_MASTER_CARD );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified string is Visa card.
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns>
        /// 	<c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCreditCardVisa( this string creditCard )
        {
            if ( CreditPassesFormatCheck( creditCard ) )
            {
                creditCard = CleanCreditCardNumber( creditCard );
                return Regex.IsMatch( creditCard, RegexPattern.CREDIT_CARD_VISA );
            }
            return false;
        }

        /// <summary>
        /// Cleans the credit card number, returning just the numeric values.
        /// </summary>
        /// <param name="creditCard">The credit card.</param>
        /// <returns></returns>
        public static string CleanCreditCardNumber( this string creditCard )
        {
            Regex regex = new Regex(RegexPattern.CREDIT_CARD_STRIP_NON_NUMERIC, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return regex.Replace( creditCard, String.Empty );
        }

        /// <summary>
        /// Determines whether the credit card number, once cleaned, passes the Luhn algorith.
        /// See: http://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="creditCardNumber">The credit card number.</param>
        /// <returns></returns>
        private static bool CreditPassesFormatCheck( this string creditCardNumber )
        {
            creditCardNumber = CleanCreditCardNumber( creditCardNumber );
            if ( creditCardNumber.IsInteger( ) )
            {
                int[] numArray = new int[creditCardNumber.Length];
                for ( int i = 0 ; i < numArray.Length ; i++ )
                    numArray [ i ] = Convert.ToInt16( creditCardNumber [ i ].ToString( ) );

                return IsValidLuhn( numArray );
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified int array passes the Luhn algorith
        /// </summary>
        /// <param name="digits">The int array to evaluate</param>
        /// <returns>
        /// 	<c>true</c> if it validates; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLuhn( this int [ ] digits )
        {
            int sum = 0;
            bool alt = false;
            for ( int i = digits.Length - 1 ; i >= 0 ; i-- )
            {
                if ( alt )
                {
                    digits [ i ] *= 2;
                    if ( digits [ i ] > 9 )
                        digits [ i ] -= 9; // equivalent to adding the value of digits
                }
                sum += digits [ i ];
                alt = !alt;
            }
            return sum % 10 == 0;
        }

        /// <summary>
        /// Determine whether the passed string is numeric, by attempting to parse it to a double
        /// </summary>
        /// <param name="str">The string to evaluated for numeric conversion</param>
        /// <returns>
        /// 	<c>true</c> if the string can be converted to a number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStringNumeric( this string str )
        {
            double result;
            return ( double.TryParse( str, NumberStyles.Float, NumberFormatInfo.CurrentInfo, out result ) );
        }

        #endregion
    }

    public class RegexPattern
    {
        public const string ALPHA = "[^a-zA-Z]";
        public const string ALPHA_NUMERIC = "[^a-zA-Z0-9]";
        public const string ALPHA_NUMERIC_SPACE = @"[^a-zA-Z0-9\s]";
        public const string CREDIT_CARD_AMERICAN_EXPRESS = @"^(?:(?:[3][4|7])(?:\d{13}))$";
        public const string CREDIT_CARD_CARTE_BLANCHE = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";
        public const string CREDIT_CARD_DINERS_CLUB = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";
        public const string CREDIT_CARD_DISCOVER = @"^(?:(?:6011)(?:\d{12}))$";
        public const string CREDIT_CARD_EN_ROUTE = @"^(?:(?:[2](?:014|149))(?:\d{11}))$";
        public const string CREDIT_CARD_JCB = @"^(?:(?:(?:2131|1800)(?:\d{11}))$|^(?:(?:3)(?:\d{15})))$";
        public const string CREDIT_CARD_MASTER_CARD = @"^(?:(?:[5][1-5])(?:\d{14}))$";
        public const string CREDIT_CARD_STRIP_NON_NUMERIC = @"(\-|\s|\D)*";
        public const string CREDIT_CARD_VISA = @"^(?:(?:[4])(?:\d{12}|\d{15}))$";
        public const string EMAIL = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
        public const string EMBEDDED_CLASS_NAME_MATCH = "(?<=^_).*?(?=_)";
        public const string EMBEDDED_CLASS_NAME_REPLACE = "^_.*?_";
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_MATCH = "(?<=^UNDERSCORE).*?(?=UNDERSCORE)";
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_REPLACE = "^UNDERSCORE.*?UNDERSCORE";
        public const string GUID = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
        public const string IP_ADDRESS = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        public const string LOWER_CASE = @"^[a-z]+$";
        public const string NUMERIC = "[^0-9]";
        public const string SOCIAL_SECURITY = @"^\d{3}[-]?\d{2}[-]?\d{4}$";
        public const string SQL_EQUAL = @"\=";
        public const string SQL_GREATER = @"\>";
        public const string SQL_GREATER_OR_EQUAL = @"\>.*\=";
        public const string SQL_IS = @"\x20is\x20";
        public const string SQL_IS_NOT = @"\x20is\x20not\x20";
        public const string SQL_LESS = @"\<";
        public const string SQL_LESS_OR_EQUAL = @"\<.*\=";
        public const string SQL_LIKE = @"\x20like\x20";
        public const string SQL_NOT_EQUAL = @"\<.*\>";
        public const string SQL_NOT_LIKE = @"\x20not\x20like\x20";

        public const string STRONG_PASSWORD =
            @"(?=^.{8,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*";

        public const string UPPER_CASE = @"^[A-Z]+$";
        public const string URL = @"^^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";
        public const string US_CURRENCY = @"^\$(([1-9]\d*|([1-9]\d{0,2}(\,\d{3})*))(\.\d{1,2})?|(\.\d{1,2}))$|^\$[0](.00)?$";
        public const string US_TELEPHONE = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
        public const string US_ZIPCODE = @"^\d{5}$";
        public const string US_ZIPCODE_PLUS_FOUR = @"^\d{5}((-|\s)?\d{4})$";
        public const string US_ZIPCODE_PLUS_FOUR_OPTIONAL = @"^\d{5}((-|\s)?\d{4})?$";
    }

    public static class Numeric
    {
        /// <summary>
        /// Determines whether a number is a natural number (positive, non-decimal)
        /// </summary>
        /// <param name="sItem">The s item.</param>
        /// <returns>
        /// 	<c>true</c> if [is natural number] [the specified s item]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNaturalNumber( this string sItem )
        {
            Regex notNaturalPattern = new Regex("[^0-9]");
            Regex naturalPattern = new Regex("0*[1-9][0-9]*");

            return !notNaturalPattern.IsMatch( sItem ) && naturalPattern.IsMatch( sItem );
        }

        /// <summary>
        /// Determines whether [is whole number] [the specified s item].
        /// </summary>
        /// <param name="sItem">The s item.</param>
        /// <returns>
        /// 	<c>true</c> if [is whole number] [the specified s item]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWholeNumber( this string sItem )
        {
            Regex notWholePattern = new Regex("[^0-9]");
            return !notWholePattern.IsMatch( sItem );
        }

        /// <summary>
        /// Determines whether the specified s item is integer.
        /// </summary>
        /// <param name="sItem">The s item.</param>
        /// <returns>
        /// 	<c>true</c> if the specified s item is integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInteger( this string sItem )
        {
            Regex notIntPattern = new Regex("[^0-9-]");
            Regex intPattern = new Regex("^-[0-9]+$|^[0-9]+$");

            return !notIntPattern.IsMatch( sItem ) && intPattern.IsMatch( sItem );
        }

        /// <summary>
        /// Determines whether the specified s item is number.
        /// </summary>
        /// <param name="sItem">The s item.</param>
        /// <returns>
        /// 	<c>true</c> if the specified s item is number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumber( this string sItem )
        {
            double result;
            return ( double.TryParse( sItem, NumberStyles.Float, NumberFormatInfo.CurrentInfo, out result ) );
        }

        /// <summary>
        /// Determines whether the specified value is an even number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is even; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEven( this int value )
        {
            return ( ( value & 1 ) == 0 );
        }

        /// <summary>
        /// Determines whether the specified value is an odd number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is odd; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOdd( this int value )
        {
            return ( ( value & 1 ) == 1 );
        }

        /// <summary>
        /// Generates a random number with an upper bound
        /// </summary>
        /// <param name="high">The high.</param>
        /// <returns></returns>
        public static int Random( int high )
        {
            byte[] random = new Byte[4];
            new RNGCryptoServiceProvider( ).GetBytes( random );
            int randomNumber = BitConverter.ToInt32(random, 0);

            return Math.Abs( randomNumber % high );
        }

        /// <summary>
        /// Generates a random number between the specified bounds
        /// </summary>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <returns></returns>
        public static int Random( int low, int high )
        {
            return new Random( ).Next( low, high );
        }

        /// <summary>
        /// Generates a random double
        /// </summary>
        /// <returns></returns>
        public static double Random( )
        {
            return new Random( ).NextDouble( );
        }
    }

    public static class Dates
    {

        public const string AGO = "ago";
        public const string DAY = "day";
        public const string HOUR = "hour";
        public const string MINUTE = "minute";
        public const string MONTH = "month";
        public const string SECOND = "second";
        public const string SPACE = " ";
        public const string YEAR = "year";

        #region Date Math

        /// <summary>
        /// Returns a date in the past by days.
        /// </summary>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        public static DateTime DaysAgo( this int days )
        {
            TimeSpan t = new TimeSpan(days, 0, 0, 0);
            return DateTime.Now.Subtract( t );
        }

        /// <summary>
        ///  Returns a date in the future by days.
        /// </summary>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        public static DateTime DaysFromNow( this int days )
        {
            TimeSpan t = new TimeSpan(days, 0, 0, 0);
            return DateTime.Now.Add( t );
        }

        /// <summary>
        /// Returns a date in the past by hours.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <returns></returns>
        public static DateTime HoursAgo( this int hours )
        {
            TimeSpan t = new TimeSpan(hours, 0, 0);
            return DateTime.Now.Subtract( t );
        }

        /// <summary>
        /// Returns a date in the future by hours.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <returns></returns>
        public static DateTime HoursFromNow( this int hours )
        {
            TimeSpan t = new TimeSpan(hours, 0, 0);
            return DateTime.Now.Add( t );
        }

        /// <summary>
        /// Returns a date in the past by minutes
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public static DateTime MinutesAgo( this int minutes )
        {
            TimeSpan t = new TimeSpan(0, minutes, 0);
            return DateTime.Now.Subtract( t );
        }

        /// <summary>
        /// Returns a date in the future by minutes.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        public static DateTime MinutesFromNow( this int minutes )
        {
            TimeSpan t = new TimeSpan(0, minutes, 0);
            return DateTime.Now.Add( t );
        }

        /// <summary>
        /// Gets a date in the past according to seconds
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static DateTime SecondsAgo( this int seconds )
        {
            TimeSpan t = new TimeSpan(0, 0, seconds);
            return DateTime.Now.Subtract( t );
        }

        /// <summary>
        /// Gets a date in the future by seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static DateTime SecondsFromNow( this int seconds )
        {
            TimeSpan t = new TimeSpan(0, 0, seconds);
            return DateTime.Now.Add( t );
        }

        #endregion


        #region Diffs

        /// <summary>
        /// Diffs the specified date.
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static TimeSpan Diff( this DateTime dateOne, DateTime dateTwo )
        {
            TimeSpan t = dateOne.Subtract(dateTwo);
            return t;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffDays( this string dateOne, string dateTwo )
        {
            DateTime dtOne;
            DateTime dtTwo;
            if ( DateTime.TryParse( dateOne, out dtOne ) && DateTime.TryParse( dateTwo, out dtTwo ) )
                return Diff( dtOne, dtTwo ).TotalDays;
            return 0;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffDays( this DateTime dateOne, DateTime dateTwo )
        {
            return Diff( dateOne, dateTwo ).TotalDays;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffHours( this string dateOne, string dateTwo )
        {
            DateTime dtOne;
            DateTime dtTwo;
            if ( DateTime.TryParse( dateOne, out dtOne ) && DateTime.TryParse( dateTwo, out dtTwo ) )
                return Diff( dtOne, dtTwo ).TotalHours;
            return 0;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffHours( this DateTime dateOne, DateTime dateTwo )
        {
            return Diff( dateOne, dateTwo ).TotalHours;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffMinutes( this string dateOne, string dateTwo )
        {
            DateTime dtOne;
            DateTime dtTwo;
            if ( DateTime.TryParse( dateOne, out dtOne ) && DateTime.TryParse( dateTwo, out dtTwo ) )
                return Diff( dtOne, dtTwo ).TotalMinutes;
            return 0;
        }

        /// <summary>
        /// Returns a double indicating the number of days between two dates (past is negative)
        /// </summary>
        /// <param name="dateOne">The date one.</param>
        /// <param name="dateTwo">The date two.</param>
        /// <returns></returns>
        public static double DiffMinutes( this DateTime dateOne, DateTime dateTwo )
        {
            return Diff( dateOne, dateTwo ).TotalMinutes;
        }

        /// <summary>
        /// Displays the difference in time between the two dates. Return example is "12 years 4 months 24 days 8 hours 33 minutes 5 seconds"
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static string ReadableDiff( this DateTime startTime, DateTime endTime )
        {
            string result;

            int seconds = endTime.Second - startTime.Second;
            int minutes = endTime.Minute - startTime.Minute;
            int hours = endTime.Hour - startTime.Hour;
            int days = endTime.Day - startTime.Day;
            int months = endTime.Month - startTime.Month;
            int years = endTime.Year - startTime.Year;

            if ( seconds < 0 )
            {
                minutes--;
                seconds += 60;
            }
            if ( minutes < 0 )
            {
                hours--;
                minutes += 60;
            }
            if ( hours < 0 )
            {
                days--;
                hours += 24;
            }

            if ( days < 0 )
            {
                months--;
                int previousMonth = (endTime.Month == 1) ? 12 : endTime.Month - 1;
                int year = (previousMonth == 12) ? endTime.Year - 1 : endTime.Year;
                days += DateTime.DaysInMonth( year, previousMonth );
            }
            if ( months < 0 )
            {
                years--;
                months += 12;
            }

            //put this in a readable format
            if ( years > 0 )
            {
                result = years.Pluralize( YEAR );
                if ( months != 0 )
                    result += ", " + months.Pluralize( MONTH );
                result += " ago";
            }
            else if ( months > 0 )
            {
                result = months.Pluralize( MONTH );
                if ( days != 0 )
                    result += ", " + days.Pluralize( DAY );
                result += " ago";
            }
            else if ( days > 0 )
            {
                result = days.Pluralize( DAY );
                if ( hours != 0 )
                    result += ", " + hours.Pluralize( HOUR );
                result += " ago";
            }
            else if ( hours > 0 )
            {
                result = hours.Pluralize( HOUR );
                if ( minutes != 0 )
                    result += ", " + minutes.Pluralize( MINUTE );
                result += " ago";
            }
            else if ( minutes > 0 )
                result = minutes.Pluralize( MINUTE ) + " ago";
            else
                result = seconds.Pluralize( SECOND ) + " ago";

            return result;
        }

        #endregion


        // many thanks to ASP Alliance for the code below
        // http://authors.aspalliance.com/olson/methods/

        /// <summary>
        /// Counts the number of weekdays between two dates.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static int CountWeekdays( this DateTime startTime, DateTime endTime )
        {
            TimeSpan ts = endTime - startTime;
            Console.WriteLine( ts.Days );
            int cnt = 0;
            for ( int i = 0 ; i < ts.Days ; i++ )
            {
                DateTime dt = startTime.AddDays(i);
                if ( IsWeekDay( dt ) )
                    cnt++;
            }
            return cnt;
        }

        /// <summary>
        /// Counts the number of weekends between two dates.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static int CountWeekends( this DateTime startTime, DateTime endTime )
        {
            TimeSpan ts = endTime - startTime;
            Console.WriteLine( ts.Days );
            int cnt = 0;
            for ( int i = 0 ; i < ts.Days ; i++ )
            {
                DateTime dt = startTime.AddDays(i);
                if ( IsWeekEnd( dt ) )
                    cnt++;
            }
            return cnt;
        }

        /// <summary>
        /// Verifies if the object is a date
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>
        /// 	<c>true</c> if the specified dt is date; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDate( this object dt )
        {
            DateTime newDate;
            return DateTime.TryParse( dt.ToString( ), out newDate );
        }

        /// <summary>
        /// Checks to see if the date is a week day (Mon - Fri)
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>
        /// 	<c>true</c> if [is week day] [the specified dt]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWeekDay( this DateTime dt )
        {
            return ( dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday );
        }

        /// <summary>
        /// Checks to see if the date is Saturday or Sunday
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns>
        /// 	<c>true</c> if [is week end] [the specified dt]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWeekEnd( this DateTime dt )
        {
            return ( dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday );
        }

        /// <summary>
        /// Displays the difference in time between the two dates. Return example is "12 years 4 months 24 days 8 hours 33 minutes 5 seconds"
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public static string TimeDiff( this DateTime startTime, DateTime endTime )
        {
            int seconds = endTime.Second - startTime.Second;
            int minutes = endTime.Minute - startTime.Minute;
            int hours = endTime.Hour - startTime.Hour;
            int days = endTime.Day - startTime.Day;
            int months = endTime.Month - startTime.Month;
            int years = endTime.Year - startTime.Year;
            if ( seconds < 0 )
            {
                minutes--;
                seconds += 60;
            }
            if ( minutes < 0 )
            {
                hours--;
                minutes += 60;
            }
            if ( hours < 0 )
            {
                days--;
                hours += 24;
            }
            if ( days < 0 )
            {
                months--;
                int previousMonth = (endTime.Month == 1) ? 12 : endTime.Month - 1;
                int year = (previousMonth == 12) ? endTime.Year - 1 : endTime.Year;
                days += DateTime.DaysInMonth( year, previousMonth );
            }
            if ( months < 0 )
            {
                years--;
                months += 12;
            }

            string sYears = FormatString(YEAR, String.Empty, years);
            string sMonths = FormatString(MONTH, sYears, months);
            string sDays = FormatString(DAY, sMonths, days);
            string sHours = FormatString(HOUR, sDays, hours);
            string sMinutes = FormatString(MINUTE, sHours, minutes);
            string sSeconds = FormatString(SECOND, sMinutes, seconds);

            return String.Concat( sYears, sMonths, sDays, sHours, sMinutes, sSeconds );
        }

        /// <summary>
        /// Given a datetime object, returns the formatted month and day, i.e. "April 15th"
        /// </summary>
        /// <param name="date">The date to extract the string from</param>
        /// <returns></returns>
        public static string GetFormattedMonthAndDay( this DateTime date )
        {
            return String.Concat( String.Format( "{0:MMMM}", date ), " ", GetDateDayWithSuffix( date ) );
        }

        /// <summary>
        /// Given a datetime object, returns the formatted day, "15th"
        /// </summary>
        /// <param name="date">The date to extract the string from</param>
        /// <returns></returns>
        public static string GetDateDayWithSuffix( this DateTime date )
        {
            int dayNumber = date.Day;
            string suffix = "th";

            if ( dayNumber == 1 || dayNumber == 21 || dayNumber == 31 )
                suffix = "st";
            else if ( dayNumber == 2 || dayNumber == 22 )
                suffix = "nd";
            else if ( dayNumber == 3 || dayNumber == 23 )
                suffix = "rd";

            return String.Concat( dayNumber, suffix );
        }

        /// <summary>
        /// Remove leading strings with zeros and adjust for singular/plural
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="previousStr">The previous STR.</param>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static string FormatString( this string str, string previousStr, int t )
        {
            if ( ( t == 0 ) && ( previousStr.Length == 0 ) )
                return String.Empty;

            string suffix = (t == 1) ? String.Empty : "s";
            return String.Concat( t, SPACE, str, suffix, SPACE );
        }
    }
}