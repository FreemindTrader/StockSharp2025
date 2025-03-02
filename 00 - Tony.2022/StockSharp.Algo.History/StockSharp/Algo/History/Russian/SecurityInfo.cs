using Ecng.Common;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;

namespace StockSharp.Algo.History.Russian
{
    public class SecurityInfo : Cloneable< SecurityInfo >
    {
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;
        private Decimal? nullable_0;
        private DateTime? nullable_1;
        private DateTime? nullable_2;
        private int? nullable_3;
        private Decimal? nullable_4;
        private Decimal? nullable_5;
        private string string_7;
        private DateTime? nullable_6;

        public string Name
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string ShortName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string Code
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string Board
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public string Isin
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public string Asset
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }

        public string Type
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public Decimal? IssueSize
        {
            get
            {
                return this.nullable_0;
            }
            set
            {
                this.nullable_0 = value;
            }
        }

        public DateTime? IssueDate
        {
            get
            {
                return this.nullable_1;
            }
            set
            {
                this.nullable_1 = value;
            }
        }

        public DateTime? LastDate
        {
            get
            {
                return this.nullable_2;
            }
            set
            {
                this.nullable_2 = value;
            }
        }

        public int? Decimals
        {
            get
            {
                return this.nullable_3;
            }
            set
            {
                this.nullable_3 = value;
            }
        }

        public Decimal? Multiplier
        {
            get
            {
                return this.nullable_4;
            }
            set
            {
                this.nullable_4 = value;
            }
        }

        public Decimal? PriceStep
        {
            get
            {
                return this.nullable_5;
            }
            set
            {
                this.nullable_5 = value;
            }
        }

        public string Currency
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = value;
            }
        }

        public DateTime? SettleDate
        {
            get
            {
                return this.nullable_6;
            }
            set
            {
                this.nullable_6 = value;
            }
        }

        public SecurityTypes? GetSecurityType( )
        {
            string type = this.Type;

            if( type == "options" )
            {
                return new SecurityTypes?( SecurityTypes.Option );
            }

            if( type == "depositary_receipt" )
            {
                return new SecurityTypes?( SecurityTypes.Adr );
            }

            if( type == "preferred_share" )
            {
                return new SecurityTypes?( SecurityTypes.Stock );
            }

            if( type == "commodity_futures" || type == "spread" )
            {
                return new SecurityTypes?( SecurityTypes.Future );
            }

            //switch ( Class24.smethod_0( type ) )
            //{
            //    case 1001941631:
            //        if ( !( type == "futures" ) )
            //            goto default;
            //        else
            //            break;
            //    case 1229586638:
            //        if ( type == "spread" )
            //            break;
            //        goto default;
            //    case 1627200841:
            //        if ( type == "commodity_futures" )
            //            break;
            //        goto default;
            //    case 2316512852:
            //        if ( !( type == "common_share" ) )
            //            goto default;
            //        else
            //            goto label_7;
            //    case 2653909194:
            //        if ( type == "preferred_share" )
            //            goto label_7;
            //        else
            //            goto default;
            //    case 2796265354:
            //        if ( type == "depositary_receipt" )
            //            return new SecurityTypes?( SecurityTypes.Adr );
            //        goto default;
            //    case 4012403877:
            //        
            //        goto default;
            //    default:
            //        return new SecurityTypes?( );
            //}
            //return new SecurityTypes?( SecurityTypes.Future );
            //label_7:
            //return new SecurityTypes?( SecurityTypes.Stock );

            return new SecurityTypes?( );
        }

        public void FillTo( Security security, IExchangeInfoProvider exchangeInfoProvider )
        {
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( exchangeInfoProvider == null )
            {
                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
            }

            if( security.ShortName.IsEmpty( ) )
            {
                security.ShortName = this.ShortName;
            }

            security.Name = this.Name;
            security.Code = this.Code;
            string str = this.Board;
            if( str.CompareIgnoreCase( "RFUD" ) )
            {
                str = "FORTS";
            }

            security.Board = exchangeInfoProvider.GetOrCreateBoard( str, null );
            Decimal? nullable1 = security.Multiplier;
            if( !nullable1.HasValue )
            {
                security.Multiplier = this.Multiplier;
            }

            if( security.Decimals.HasValue )
            {
                security.Decimals = this.Decimals;
            }

            nullable1 = security.PriceStep;
            if( !nullable1.HasValue )
            {
                security.PriceStep = this.PriceStep;
                if( !security.Decimals.HasValue )
                {
                    nullable1 = this.PriceStep;
                    if( nullable1.HasValue )
                    {
                        Security security1 = security;
                        nullable1 = this.PriceStep;
                        int? nullable2 = new int?( nullable1.Value.GetCachedDecimals( ) );
                        security1.Decimals = nullable2;
                    }
                }
            }
            if( !security.Currency.HasValue )
            {
                security.Currency = this.Currency.FromMicexCurrencyName( null );
            }

            if( security.ExternalId.Isin.IsEmpty( ) )
            {
                SecurityExternalId externalId = security.ExternalId;
                externalId.Isin = this.Isin;
                security.ExternalId = externalId;
            }
            DateTime? nullable3;
            if( this.IssueDate.HasValue )
            {
                Security security1 = security;
                nullable3 = this.IssueDate;
                DateTimeOffset? nullable2 = new DateTimeOffset?( nullable3.Value.ApplyTimeZone( TimeHelper.Moscow ) );
                security1.IssueDate = nullable2;
            }
            nullable1 = this.IssueSize;
            if( nullable1.HasValue )
            {
                security.IssueSize = this.IssueSize;
            }

            nullable3 = this.LastDate;
            if( nullable3.HasValue )
            {
                Security security1 = security;
                nullable3 = this.LastDate;
                DateTimeOffset? nullable2 = new DateTimeOffset?( nullable3.Value.ApplyTimeZone( TimeHelper.Moscow ) );
                security1.ExpiryDate = nullable2;
            }
            if( !this.Asset.IsEmpty( ) )
            {
                security.UnderlyingSecurityId = this.Asset + "@" + security.Board.Code;
            }

            SecurityTypes? securityType = this.GetSecurityType( );
            if( !securityType.HasValue )
            {
                return;
            }

            security.Type = securityType;
        }

        public override SecurityInfo Clone( )
        {
            return new SecurityInfo( )
            {
                Name = this.Name,
                ShortName = this.ShortName,
                Code = this.Code,
                Board = this.Board,
                Isin = this.Isin,
                Asset = this.Asset,
                Type = this.Type,
                Decimals = this.Decimals,
                Multiplier = this.Multiplier,
                PriceStep = this.PriceStep,
                Currency = this.Currency,
                IssueSize = this.IssueSize,
                IssueDate = this.IssueDate,
                LastDate = this.LastDate
            };
        }
    }
}
