using Ecng.Collections;
using Ecng.Common;
using Ecng.Compilation;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ecng.ComponentModel.Expressions
{
    [CLSCompliant( false )]
    public static class ExpressionHelper
    {
        private static readonly Regex _idRegex = new Regex( "(?<id>(#*)(@*)(#*)(\\w*\\.*)(\\**)(\\w+(\\/*)\\w+)@\\w+)" );
        private static readonly Regex _nameRegex = new Regex( "(?<name>(\\w+))" );
        private static readonly Regex _bracketsVarRegex = new Regex( "\\[(?<name>[^\\]]*)\\]" );
        private const string IdPattern = "(#*)(@*)(#*)(\\w*\\.*)(\\**)(\\w+(\\/*)\\w+)@\\w+";
        private const string _prefix = "MathHelper.";
        private static readonly CachedSynchronizedDictionary<string, string> _funcReplaces;
        private const string _template = "using System;\r\nusing System.Collections.Generic;\r\n\r\nusing Ecng.Common;\r\n\r\nusing Ecng.ComponentModel.Expressions;\r\n\r\nclass TempExpressionFormula : ExpressionFormula\r\n{\r\n\tpublic TempExpressionFormula(string expression, IEnumerable<string> identifiers)\r\n\t\t: base(expression, identifiers)\r\n\t{\r\n\t}\r\n\r\n\tpublic override decimal Calculate(decimal[] values)\r\n\t{\r\n\t\treturn __insert_code;\r\n\t}\r\n}";

        public static IEnumerable<string> GetIds( string expression )
        {
            return ExpressionHelper._idRegex.Matches( expression ).Cast<Match>().Where<Match>( ( Func<Match, bool> )( match => match.Success ) ).Select<Match, string>( ( Func<Match, string> )( match => match.Groups["id"].Value ) );
        }

        private static IEnumerable<Group> GetVariableNames( string expression )
        {
            return ExpressionHelper._nameRegex.Matches( expression ).Cast<Match>().Where<Match>( ( Func<Match, bool> )( match => match.Success ) ).Select<Match, Group>( ( Func<Match, Group> )( match => match.Groups["name"] ) );
        }

        public static string Encode( string expression )
        {
            foreach ( string oldValue in ExpressionHelper.GetIds( expression ).Distinct<string>( ( IEqualityComparer<string> )StringComparer.InvariantCultureIgnoreCase ) )
                expression = expression.Replace( oldValue, "[{" + oldValue + "}]" );
            return expression;
        }

        public static string Decode( string expression, out IDictionary<string, string> replaces )
        {
            replaces = ( IDictionary<string, string> )new Dictionary<string, string>( ( IEqualityComparer<string> )StringComparer.InvariantCultureIgnoreCase );
            foreach ( Match match in ( IEnumerable<Match> )ExpressionHelper._bracketsVarRegex.Matches( expression ).Cast<Match>().OrderByDescending<Match, int>( ( Func<Match, int> )( m => m.Index ) ) )
            {
                string key = match.Groups["name"].Value;
                if ( !replaces.ContainsKey( key ) )
                    replaces.Add( key, string.Format( "VAR{0}@VAR", ( object )replaces.Count ) );
            }
            return replaces.Aggregate<KeyValuePair<string, string>, string>( expression, ( Func<string, KeyValuePair<string, string>, string> )( ( current, pair ) => current.ReplaceIgnoreCase( "[" + pair.Key + "]", pair.Value ).ReplaceIgnoreCase( pair.Key, pair.Value ) ) );
        }

        public static IEnumerable<string> Functions
        {
            get
            {
                return ( IEnumerable<string> )ExpressionHelper._funcReplaces.CachedKeys;
            }
        }

        public static ExpressionFormula CreateError( string errorText )
        {
            return ( ExpressionFormula )new ExpressionHelper.ErrorExpressionFormula( errorText );
        }

        private static string ReplaceFuncs( string text )
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach ( KeyValuePair<string, string> cachedPair in ExpressionHelper._funcReplaces.CachedPairs )
            {
                string str1 = cachedPair.Key + "(";
                if ( text.ContainsIgnoreCase( str1 ) )
                {
                    string str2 = TypeHelper.GenerateSalt( 16 ).Base64();
                    dictionary.Add( str2, cachedPair.Value + "(" );
                    text = text.ReplaceIgnoreCase( str1, str2 );
                }
            }
            foreach ( KeyValuePair<string, string> keyValuePair in dictionary )
                text = text.ReplaceIgnoreCase( keyValuePair.Key, keyValuePair.Value );
            return text;
        }

        private static string Escape( string text, bool useIds, out IEnumerable<string> identifiers )
        {
            if ( text.IsEmptyOrWhiteSpace() )
                throw new ArgumentNullException( nameof( text ) );
            if ( useIds )
            {
                IDictionary<string, string> replaces;
                text = ExpressionHelper.Decode( text.ToUpperInvariant(), out replaces );
                identifiers = ( IEnumerable<string> )ExpressionHelper.GetIds( text ).Distinct<string>().ToArray<string>();
                int num = 0;
                foreach ( string oldValue in identifiers )
                {
                    text = text.ReplaceIgnoreCase( oldValue, string.Format( "values[{0}]", ( object )num ) );
                    ++num;
                }
                if ( num == 0 )
                    throw new InvalidOperationException( "Expression '" + text + "' do not contains any identifiers." );
                return ExpressionHelper.ReplaceFuncs( text );
            }
            text = text.Replace( ".", "__DOT__" );
            Group[ ] array = ExpressionHelper.GetVariableNames( text ).Where<Group>( ( Func<Group, bool> )( g =>
            {
                long result;
                if ( !g.Value.ContainsIgnoreCase( "__DOT__" ) && !long.TryParse( g.Value, out result ) )
                    return !ExpressionHelper._funcReplaces.ContainsKey( g.Value );
                return false;
            } ) ).ToArray<Group>();
            Dictionary<string, int> dictionary = new Dictionary<string, int>( ( IEqualityComparer<string> )StringComparer.InvariantCultureIgnoreCase );
            foreach ( Group group in ( IEnumerable<Group> )( ( IEnumerable<Group> )array ).OrderByDescending<Group, int>( ( Func<Group, int> )( g => g.Index ) ) )
            {
                int count;
                if ( !dictionary.TryGetValue( group.Value, out count ) )
                {
                    count = dictionary.Count;
                    dictionary.Add( group.Value, count );
                }
                text = text.Remove( group.Index, group.Length ).Insert( group.Index, string.Format( "values[{0}]", ( object )count ) );
            }
            identifiers = ( IEnumerable<string> )dictionary.Keys.ToArray<string>();
            text = text.Replace( "__DOT__", "." );
            return ExpressionHelper.ReplaceFuncs( text );
        }

        public static ExpressionFormula Compile(
          this ICompilerService service,
          string expression,
          bool useIds )
        {
            try
            {
                List<string> refs = new List<string>( ( IEnumerable<string> )new string[3] { typeof( object ).Assembly.Location, typeof( ExpressionFormula ).Assembly.Location, typeof( MathHelper ).Assembly.Location } );
                string[ ] strArray = new string[2] { "System.Runtime", "netstandard" };
                Assembly[ ] allAssemblies = ( ( IEnumerable<Assembly> )AppDomain.CurrentDomain.GetAssemblies() ).ToArray<Assembly>();
                ( ( IEnumerable<string> )strArray ).ForEach<string>( ( Action<string> )( name =>
                {
                    Assembly assembly = ( ( IEnumerable<Assembly> )allAssemblies ).FirstOrDefault<Assembly>( ( Func<Assembly, bool> )( a => a.GetName().Name.EqualsIgnoreCase( name ) ) );
                    if ( !( assembly != ( Assembly )null ) )
                        return;
                    refs.Add( assembly.Location );
                } ) );
                IEnumerable<string> identifiers;
                string newValue = ExpressionHelper.Escape( expression, useIds, out identifiers );
                CompilationResult compilationResult = service.GetCompiler( CompilationLanguages.CSharp ).Compile( "IndexExpression", "using System;\r\nusing System.Collections.Generic;\r\n\r\nusing Ecng.Common;\r\n\r\nusing Ecng.ComponentModel.Expressions;\r\n\r\nclass TempExpressionFormula : ExpressionFormula\r\n{\r\n\tpublic TempExpressionFormula(string expression, IEnumerable<string> identifiers)\r\n\t\t: base(expression, identifiers)\r\n\t{\r\n\t}\r\n\r\n\tpublic override decimal Calculate(decimal[] values)\r\n\t{\r\n\t\treturn __insert_code;\r\n\t}\r\n}".Replace( "__insert_code", newValue ), ( IEnumerable<string> )refs );
                ExpressionFormula expressionFormula;
                if ( ( object )compilationResult.Assembly != null )
                    expressionFormula = compilationResult.Assembly.GetType( "TempExpressionFormula" ).CreateInstance<ExpressionFormula>( ( object )expression, ( object )identifiers );
                else
                    expressionFormula = ( ExpressionFormula )new ExpressionHelper.ErrorExpressionFormula( compilationResult.Errors.Where<CompilationError>( ( Func<CompilationError, bool> )( e => e.Type == CompilationErrorTypes.Error ) ).Select<CompilationError, string>( ( Func<CompilationError, string> )( e => e.Message ) ).Join( Environment.NewLine ) );
                return expressionFormula;
            }
            catch ( Exception ex )
            {
                return ( ExpressionFormula )new ExpressionHelper.ErrorExpressionFormula( ex.ToString() );
            }
        }

        static ExpressionHelper()
        {
            CachedSynchronizedDictionary<string, string> synchronizedDictionary = new CachedSynchronizedDictionary<string, string>( ( IEqualityComparer<string> )StringComparer.InvariantCultureIgnoreCase );
            synchronizedDictionary.Add( "abs", "MathHelper.Abs" );
            synchronizedDictionary.Add( "acos", "MathHelper.Acos" );
            synchronizedDictionary.Add( "asin", "MathHelper.Asin" );
            synchronizedDictionary.Add( "atan", "MathHelper.Atan" );
            synchronizedDictionary.Add( "ceiling", "MathHelper.Ceiling" );
            synchronizedDictionary.Add( "cos", "MathHelper.Cos" );
            synchronizedDictionary.Add( "exp", "MathHelper.Exp" );
            synchronizedDictionary.Add( "floor", "MathHelper.Floor" );
            synchronizedDictionary.Add( "log", "MathHelper.Log" );
            synchronizedDictionary.Add( "log10", "MathHelper.Log10" );
            synchronizedDictionary.Add( "max", "MathHelper.Max" );
            synchronizedDictionary.Add( "min", "MathHelper.Min" );
            synchronizedDictionary.Add( "pow", "MathHelper.Pow" );
            synchronizedDictionary.Add( "round", "MathHelper.Round" );
            synchronizedDictionary.Add( "sign", "MathHelper.Sign" );
            synchronizedDictionary.Add( "sin", "MathHelper.Sin" );
            synchronizedDictionary.Add( "sqrt", "MathHelper.Sqrt" );
            synchronizedDictionary.Add( "tan", "MathHelper.Tan" );
            synchronizedDictionary.Add( "truncate", "MathHelper.Truncate" );
            ExpressionHelper._funcReplaces = synchronizedDictionary;
        }

        private class ErrorExpressionFormula : ExpressionFormula
        {
            public ErrorExpressionFormula( string error )
              : base( error )
            {
            }

            public override Decimal Calculate( Decimal[ ] prices )
            {
                throw new NotSupportedException( this.Error );
            }
        }
    }
}
