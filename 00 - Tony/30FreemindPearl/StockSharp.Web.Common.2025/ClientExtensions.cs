// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.ClientExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.Common
{
    public static class ClientExtensions
    {
        public const string HashTokenKey = "clientHash";
        public const int OnlineMinutesWindow = 5;

        public static bool IsDeleted( this Client client )
        {
            if ( !client.Deleted )
                return client.SelfDeleted.GetValueOrDefault();
            return true;
        }

        public static bool IsLocked( this Client client, bool checkApproved = false )
        {
            if ( client.IsDeleted() || client.IsLockedOut.GetValueOrDefault() )
                return true;
            if ( checkApproved )
                return !client.IsApproved.GetValueOrDefault();
            return false;
        }

        public static bool IsAnonymous( this Client client )
        {
            return ( ( BaseEntity ) TypeHelper.CheckOnNull<Client>(  client, nameof( client ) ) ).Id == 145183L;
        }

        public static bool IsOnline( this Client client )
        {
            DateTime? lastActivityDate = client.LastActivityDate;
            ref DateTime? local = ref lastActivityDate;
            if ( !local.HasValue )
                return false;
            return local.GetValueOrDefault().IsOnline();
        }

        public static bool IsOnline( this DateTime lastActivityDate )
        {
            return ( DateTime.UtcNow - lastActivityDate ).TotalMinutes < 5.0;
        }

        public static string GetToken( this Client client )
        {
            return ( ( BaseEntity ) TypeHelper.CheckOnNull<Client>(  client, nameof( client ) ) ).Id.GetToken();
        }

        public static string GetToken( this long clientId )
        {
            return ( string ) Converter.To<string>( ( object ) clientId );
        }

        public static bool VerifySupport( this Client client )
        {
            DateTime? supportTill = (DateTime?) client?.SupportTill;
            DateTime utcNow = DateTime.UtcNow;
            if ( !supportTill.HasValue )
                return false;
            return supportTill.GetValueOrDefault() > utcNow;
        }

        public static int GetFilledPercentage( this Client client )
        {
            int num1 = 0;
            if ( !StringHelper.IsEmpty( client.VKontakte ) || !StringHelper.IsEmpty( client.Facebook ) )
                num1 += 10;
            if ( !StringHelper.IsEmpty( client.Skype ) )
                num1 += 10;
            if ( !StringHelper.IsEmpty( client.City ) )
                num1 += 10;
            if ( client.Birthday.HasValue )
                num1 += 10;
            File picture = client.Picture;
            if ( ( picture != null ? ( !picture.Deleted ? 1 : 0 ) : 0 ) != 0 )
                num1 += 20;
            if ( client.PublicDescription != null )
            {
                if ( !StringHelper.IsEmpty( client.PublicDescription.Text ) )
                    num1 += 10;
                Topic topic = client.PublicDescription.Topic;
                int num2;
                if ( topic == null )
                {
                    num2 = 0;
                }
                else
                {
                    int? length = topic.Tags?.Items?.Length;
                    int num3 = 0;
                    num2 = length.GetValueOrDefault() > num3 & length.HasValue ? 1 : 0;
                }
                if ( num2 != 0 )
                    num1 += 20;
            }
            return num1;
        }

        public static BaseEntitySet<Client> ToClientsSet( this string str )
        {
            return ( ( IEnumerable<string> ) StringHelper.SplitByComma( str, false ) ).Select<string, long?>( ( Func<string, long?> ) ( s => ( long? ) Converter.To<long?>( ( object ) s ) ) ).Where<long?>( ( Func<long?, bool> ) ( i => i.HasValue ) ).Select<long?, Client>( ( Func<long?, Client> ) ( i =>
            {
                return new Client() { Id = i.Value };
            } ) ).ToEntitySet<Client>( 0L );
        }

        public static string ToClientsString( this BaseEntitySet<Client> clients )
        {
            if ( clients?.Items != null )
                return StringHelper.JoinComma( ( ( IEnumerable<Client> ) clients.Items ).Select<Client, string>( ( Func<Client, string> ) ( c => ( string ) Converter.To<string>( ( object ) c.Id ) ) ) );
            return string.Empty;
        }

        public static string GetAccessToken( this Client client )
        {
            BaseEntitySet<AccessToken> accessTokens = ((Client) TypeHelper.CheckOnNull<Client>( client, nameof (client))).AccessTokens;
            string str;
            if ( accessTokens == null )
            {
                str = ( string ) null;
            }
            else
            {
                AccessToken[] items = accessTokens.Items;
                str = items != null ? ( ( IEnumerable<AccessToken> ) items ).FirstOrDefault<AccessToken>()?.Text : ( string ) null;
            }
            return str ?? client.AuthToken;
        }

        public static File GetAvatarFile( this Client client )
        {
            File file1 = ((Client) TypeHelper.CheckOnNull<Client>( client, nameof (client))).Picture;
            if ( file1 == null )
            {
                File file2 = new File();
                file2.Id = client.Gender == 2 ? 101449L : 101448L;
                file1 = file2;
            }
            return file1;
        }
    }
}
