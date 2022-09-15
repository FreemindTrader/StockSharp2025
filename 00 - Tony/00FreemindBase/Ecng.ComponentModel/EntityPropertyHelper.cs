// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.EntityPropertyHelper
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using Ecng.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ecng.ComponentModel
{
    public static class EntityPropertyHelper
    {
        public static List<EntityProperty> GetEntityProperties(
          this Type type,
          Func<PropertyInfo, bool> filter = null )
        {
            return type.GetEntityProperties( ( EntityProperty )null, filter );
        }

        public static List<EntityProperty> GetEntityProperties(
          this Type type,
          EntityProperty parent,
          Func<PropertyInfo, bool> filter = null )
        {
            return type.GetEntityProperties( parent, new HashSet<Type>(), filter ?? ( Func<PropertyInfo, bool> )( p => true ) );
        }

        private static List<EntityProperty> GetEntityProperties(
          this Type type,
          EntityProperty parent,
          HashSet<Type> processed,
          Func<PropertyInfo, bool> filter )
        {
            List<EntityProperty> entityPropertyList = new List<EntityProperty>();
            if ( processed.Contains( type ) )
                return entityPropertyList;
            Type type1 = type.GetUnderlyingType();
            if ( ( object )type1 == null )
                type1 = type;
            type = type1;
            IEnumerable<PropertyInfo> propertyInfos = ( ( IEnumerable<PropertyInfo> )type.GetMembers<PropertyInfo>( BindingFlags.Instance | BindingFlags.Public ) ).Where<PropertyInfo>( filter );
            HashSet<string> stringSet = new HashSet<string>();
            processed.Add( type );
            foreach ( PropertyInfo provider in propertyInfos )
            {
                string str = ( parent != null ? parent.Name + "." : string.Empty ) + provider.Name;
                if ( stringSet.Add( str ) )
                {
                    EntityProperty parent1 = new EntityProperty()
                    {
                        Name = str,
                        Parent = parent,
                        DisplayName = provider.GetDisplayName( ( string )null ),
                        Description = provider.GetDescription( ( string )null )
                    };
                    Type propertyType = provider.PropertyType;
                    if ( !propertyType.IsPrimitive() )
                        parent1.Properties = ( IEnumerable<EntityProperty> )propertyType.GetEntityProperties( parent1, processed, filter );
                    entityPropertyList.Add( parent1 );
                }
            }
            processed.Remove( type );
            return entityPropertyList;
        }

        public static Type GetPropType( this Type type, string name )
        {
            if ( ( object )type == null )
                throw new ArgumentNullException( nameof( type ) );
            if ( name == null )
                throw new ArgumentNullException( nameof( name ) );
            Type type1 = type.GetUnderlyingType();
            if ( ( object )type1 == null )
                type1 = type;
            type = type1;
            string str = name;
            char[ ] chArray = new char[1] { '.' };
            foreach ( string name1 in str.Split( chArray ) )
            {
                PropertyInfo property = type.GetProperty( name1 );
                if ( ( object )property == null )
                    return ( Type )null;
                Type type2 = property.PropertyType.GetUnderlyingType();
                if ( ( object )type2 == null )
                    type2 = property.PropertyType;
                type = type2;
            }
            return type;
        }

        public static object GetPropValue( this object entity, string name )
        {
            object obj = entity;
            string str = name;
            char[ ] chArray = new char[1] { '.' };
            foreach ( string name1 in str.Split( chArray ) )
            {
                PropertyInfo property = obj?.GetType().GetProperty( name1 );
                if ( ( object )property == null || property.PropertyType.IsNullable() && property.GetValue( obj, ( object[ ] )null ) == null )
                    return ( object )null;
                obj = property.GetValue( obj, ( object[ ] )null );
            }
            return obj;
        }
    }
}
