using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;

namespace StockSharp.Algo.History
{
    public abstract class BaseHistorySource
    {
        private readonly SecurityIdGenerator securityIdGenerator_0;

        protected BaseHistorySource( )
        {
            this.securityIdGenerator_0 = new SecurityIdGenerator( );
        }

        public SecurityIdGenerator SecurityIdGenerator
        {
            get
            {
                return this.securityIdGenerator_0;
            }
        }

        protected string GetSecurityCode( Security security )
        {
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( !security.Id.IsEmpty( ) )
            {
                return this.SecurityIdGenerator.Split( security.Id, false ).SecurityCode;
            }

            if( security.Code.IsEmpty( ) )
            {
                throw new ArgumentException( LocalizedStrings.Str1025, nameof( security ) );
            }

            return security.Code;
        }
    }
}
