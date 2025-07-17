// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.MarkupExtensions.Static
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;

namespace StockSharp.Xaml.Charting.Common.MarkupExtensions
{
    public class Static : MarkupExtension
    {
        public static readonly object UnsetArgument = new object();
        public static readonly DependencyProperty DefaultMemberTypeProperty = DependencyProperty.RegisterAttached("DefaultMemberType", typeof (Type), typeof (Static), new PropertyMetadata((PropertyChangedCallback) null));
        private static readonly object NoCachedValue = new object();
        private object m_arg1 = Static.UnsetArgument;
        private object m_arg2 = Static.UnsetArgument;
        private object m_arg3 = Static.UnsetArgument;
        private object m_cachedValue = Static.NoCachedValue;
        private Type m_memberType;
        private string m_member;

        public Static()
        {
        }

        public Static( string member )
        {
            this.Member = member;
        }

        public Static( Type memberType, string member )
        {
            this.MemberType = memberType;
            this.Member = member;
        }

        public Type MemberType
        {
            get
            {
                return this.m_memberType;
            }
            set
            {
                this.m_memberType = value;
            }
        }

        public string Member
        {
            get
            {
                return this.m_member;
            }
            set
            {
                this.m_member = value;
            }
        }

        public object Arg1
        {
            get
            {
                return this.m_arg1;
            }
            set
            {
                this.m_arg1 = value;
            }
        }

        public object Arg2
        {
            get
            {
                return this.m_arg2;
            }
            set
            {
                this.m_arg2 = value;
            }
        }

        public object Arg3
        {
            get
            {
                return this.m_arg3;
            }
            set
            {
                this.m_arg3 = value;
            }
        }

        public static Type GetDefaultMemberType( DependencyObject obj )
        {
            return ( Type ) obj.GetValue( Static.DefaultMemberTypeProperty );
        }

        public static void SetDefaultMemberType( DependencyObject obj, Type value )
        {
            obj.SetValue( Static.DefaultMemberTypeProperty, ( object ) value );
        }

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            if ( this.m_cachedValue != Static.NoCachedValue )
                return this.m_cachedValue;
            if ( string.IsNullOrWhiteSpace( this.m_member ) )
                throw new InvalidOperationException( "Member property must be set to a non-empty value on Static markup extension." );
            string member = this.m_member;
            Type type = this.m_memberType;
            List<object> objectList = (List<object>) null;
            int length1 = member.IndexOf('(');
            if ( length1 >= 0 )
            {
                objectList = Static.ParseArguments( member, length1 + 1 );
                member = member.Substring( 0, length1 ).Trim();
            }
            if ( this.Arg1 != Static.UnsetArgument )
            {
                if ( objectList != null )
                    throw new InvalidOperationException( "Arg1, Arg2, etc. cannot be used when arguments are specified in member" );
                objectList = new List<object>();
                objectList.Add( this.Arg1 );
            }
            if ( this.Arg2 != Static.UnsetArgument )
            {
                if ( objectList == null || objectList.Count != 1 || length1 >= 0 )
                    throw new InvalidOperationException( "Arg1, Arg2, etc. cannot be used when arguments are specified in member and must be specified in consecutive order" );
                objectList.Add( this.Arg2 );
            }
            if ( this.Arg3 != Static.UnsetArgument )
            {
                if ( objectList == null || objectList.Count != 2 || length1 >= 0 )
                    throw new InvalidOperationException( "Arg1, Arg2, etc. cannot be used when arguments are specified in member and must be specified in consecutive order" );
                objectList.Add( this.Arg3 );
            }
            int length2 = member.LastIndexOf('.');
            if ( length2 >= 0 )
            {
                if ( serviceProvider == null )
                    return DependencyProperty.UnsetValue;
                IXamlTypeResolver service = serviceProvider.GetService(typeof (IXamlTypeResolver)) as IXamlTypeResolver;
                if ( service == null )
                    return DependencyProperty.UnsetValue;
                string qualifiedTypeName = this.m_member.Substring(0, length2);
                type = service.Resolve( qualifiedTypeName );
                member = member.Substring( length2 + 1 );
            }
            else if ( this.m_memberType != ( Type ) null )
            {
                type = this.m_memberType;
            }
            else
            {
                if ( serviceProvider != null )
                {
                    IRootObjectProvider service = serviceProvider.GetService(typeof (IRootObjectProvider)) as IRootObjectProvider;
                    if ( service != null )
                    {
                        DependencyObject rootObject = service.RootObject as DependencyObject;
                        if ( rootObject != null )
                            type = Static.GetDefaultMemberType( rootObject );
                    }
                }
                if ( type == ( Type ) null )
                    return DependencyProperty.UnsetValue;
            }
            object obj;
            if ( objectList == null )
            {
                PropertyInfo property = type.GetProperty(member, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                if ( property != ( PropertyInfo ) null )
                {
                    if ( !property.CanRead )
                        throw new InvalidOperationException( "Property " + member + " is not readable." );
                    obj = property.GetValue( ( object ) null, ( object[ ] ) null );
                }
                else
                {
                    FieldInfo field = type.GetField(member, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                    if ( !( field != ( FieldInfo ) null ) )
                        throw new InvalidOperationException( string.Format( "No public static property or field '{0}' found in type '{1}'", ( object ) member, ( object ) type.FullName ) );
                    obj = field.GetValue( ( object ) null );
                }
            }
            else
            {
                int argCount = objectList.Count;
                MethodInfo methodInfo = ((IEnumerable<MethodInfo>) type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>) (m =>
        {
            if (string.Equals(m.Name, member, StringComparison.InvariantCultureIgnoreCase) && !m.IsGenericMethod)
                return m.GetParameters().Length == argCount;
            return false;
        }));
                if ( methodInfo == ( MethodInfo ) null )
                    throw new InvalidOperationException( string.Format( "No public static non-generic method '{0}' with {1} parameter(s) found in type '{2}'", ( object ) member, ( object ) argCount, ( object ) type ) );
                ParameterInfo[] parameters = methodInfo.GetParameters();
                object[] objArray = new object[argCount];
                for ( int index = 0 ; index < argCount ; ++index )
                    objArray[ index ] = Static.ConvertToType( parameters[ index ].ParameterType, objectList[ index ], CultureInfo.InvariantCulture );
                obj = methodInfo.Invoke( ( object ) null, argCount == 0 ? ( object[ ] ) null : objArray );
            }
            if ( serviceProvider != null )
            {
                IProvideValueTarget service = serviceProvider.GetService(typeof (IProvideValueTarget)) as IProvideValueTarget;
                if ( service != null )
                {
                    PropertyInfo targetProperty1 = service.TargetProperty as PropertyInfo;
                    if ( targetProperty1 != ( PropertyInfo ) null )
                    {
                        obj = Static.ConvertToType( targetProperty1.PropertyType, obj, Thread.CurrentThread.CurrentUICulture );
                    }
                    else
                    {
                        DependencyProperty targetProperty2 = service.TargetProperty as DependencyProperty;
                        if ( targetProperty2 != null )
                            obj = Static.ConvertToType( targetProperty2.PropertyType, obj, Thread.CurrentThread.CurrentUICulture );
                    }
                }
            }
            this.m_cachedValue = obj;
            return obj;
        }

        private static object ConvertToType( Type targetType, object value, CultureInfo culture )
        {
            if ( value == null )
                return ( object ) null;
            if ( targetType.IsAssignableFrom( value.GetType() ) )
                return value;
            if ( targetType != typeof( string ) && string.IsNullOrWhiteSpace( value.ToString() ) )
                return ( object ) null;
            Type type = Nullable.GetUnderlyingType(targetType);
            if ( ( object ) type == null )
                type = targetType;
            targetType = type;
            return Convert.ChangeType( value, targetType, ( IFormatProvider ) culture );
        }

        private static List<object> ParseArguments( string member, int pos )
        {
            List<object> objectList = new List<object>();
        label_2:
            while ( true )
            {
                while ( pos >= member.Length || !char.IsWhiteSpace( member[ pos ] ) )
                {
                    if ( pos >= member.Length )
                        throw new InvalidOperationException( "Ending ')' is missing from member specification" );
                    if ( member[ pos ] == ')' )
                        return objectList;
                    if ( member[ pos ] == '\'' )
                    {
                        ++pos;
                        int startIndex = pos;
                        while ( pos < member.Length && ( member[ pos ] != '\'' || member[ pos - 1 ] == '\\' ) )
                            ++pos;
                        if ( pos >= member.Length )
                            throw new InvalidOperationException( "Ending ' is missing from member specification" );
                        objectList.Add( ( object ) member.Substring( startIndex, pos - startIndex ) );
                    }
                    else
                    {
                        int startIndex = pos;
                        while ( pos < member.Length && member[ pos ] != ')' && ( member[ pos ] != ',' && member[ pos ] != ')' ) )
                            ++pos;
                        string str = member.Substring(startIndex, pos - startIndex).Trim();
                        objectList.Add( ( object ) str );
                    }
                    while ( true )
                    {
                        if ( pos < member.Length && member[ pos ] != ')' )
                        {
                            if ( member[ pos ] != ',' )
                            {
                                if ( char.IsWhiteSpace( member, pos ) )
                                    ++pos;
                                else
                                    goto label_21;
                            }
                            else
                                break;
                        }
                        else
                            goto label_2;
                    }
                    ++pos;
                    continue;
                label_21:
                    throw new InvalidOperationException( "Separating comma (',') or ending paranthesis (')') is expected at position " + ( object ) pos );
                }
                ++pos;
            }
        }

        public override string ToString()
        {
            return ( string ) null;
        }
    }
}
