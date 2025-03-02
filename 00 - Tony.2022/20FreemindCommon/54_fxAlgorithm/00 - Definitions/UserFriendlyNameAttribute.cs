using fx.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public class UserFriendlyNameAttribute : Attribute
    {
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public UserFriendlyNameAttribute( string name )
        {
            _name = name;
        }

        /// <summary>
        ///
        /// </summary>
        public static string GetTypeAttributeName( Type classType )
        {
            string name = GeneralHelper.SeparateCapitalLetters( classType.Name );
            GetTypeAttributeValue( classType, ref name );
            return name;
        }

        /// <summary>
        ///
        /// </summary>
        public static bool GetTypeAttributeValue( Type classType, ref string name )
        {
            object[ ] attributes = classType.GetCustomAttributes( typeof( UserFriendlyNameAttribute ), false );

            if( attributes != null &&
                 attributes.Length > 0 )
            {
                name = ( ( UserFriendlyNameAttribute )attributes[ 0 ] ).Name;
                return true;
            }
            return false;
        }
    }
}
