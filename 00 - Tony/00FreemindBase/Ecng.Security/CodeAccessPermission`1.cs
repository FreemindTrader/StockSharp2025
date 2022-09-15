// Decompiled with JetBrains decompiler
// Type: Ecng.Security.CodeAccessPermission`1
// Assembly: Ecng.Security, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FC731BA6-0108-4E2D-8E5E-F8573AC505F7
// Assembly location: T:\00-FreemindTrader\packages\ecng.security\1.0.118\lib\netstandard2.0\Ecng.Security.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Security;

namespace Ecng.Security
{
    public abstract class CodeAccessPermission<T> : System.Security.CodeAccessPermission where T : IPermission
    {
        public override IPermission Copy()
        {
            return ( IPermission )this.OnCopy();
        }

        public override void FromXml( SecurityElement elem )
        {
            if ( elem == null )
                throw new ArgumentNullException( nameof( elem ) );
            if ( !elem.Tag.Equals( "IPermission" ) )
                throw new ArgumentException( nameof( elem ) );
            string str1 = elem.Attribute( "class" );
            if ( str1.IsEmpty() )
                throw new ArgumentException( nameof( elem ) );
            if ( str1.IndexOfIgnoreCase( ( ( object )this ).GetType().FullName, -1 ) < 0 )
                throw new ArgumentException( nameof( elem ) );
            string str2 = elem.Attribute( "Unrestricted" );
            if ( !str2.IsEmpty() && string.Compare( str2, "true", true, CultureInfo.InvariantCulture ) == 0 )
                this.OnFromXml( elem, true );
            else
                this.OnFromXml( elem, false );
        }

        public override IPermission Intersect( IPermission target )
        {
            if ( target == null )
                throw new ArgumentNullException( nameof( target ) );
            return ( IPermission )this.OnIntersect( ( T )target );
        }

        public override bool IsSubsetOf( IPermission target )
        {
            if ( target == null )
                throw new ArgumentNullException( nameof( target ) );
            return this.OnIsSubsetOf( ( T )target );
        }

        public override SecurityElement ToXml()
        {
            SecurityElement elem = new SecurityElement( "IPermission" );
            SecurityElement securityElement = elem;
            Type type = ( ( object )this ).GetType();
            string str = ( ( object )type != null ? type.ToString() : ( string )null ) + ", " + ( ( object )this ).GetType().Module.Assembly.FullName.Replace( '"', '\'' );
            securityElement.AddAttribute( "class", str );
            elem.AddAttribute( "version", "1" );
            if ( this.OnToXml( elem ) )
                elem.AddAttribute( "Unrestricted", "true" );
            return elem;
        }

        public virtual IPermission Union( IPermission other )
        {
            if ( other == null )
                throw new ArgumentNullException( nameof( other ) );
            return ( IPermission )this.OnUnion( ( T )other );
        }

        protected abstract void OnFromXml( SecurityElement elem, bool unrestricted );

        protected abstract bool OnToXml( SecurityElement elem );

        protected abstract T OnCopy();

        protected abstract T OnIntersect( T target );

        protected abstract bool OnIsSubsetOf( T target );

        protected abstract T OnUnion( T other );
    }
}
