// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.TopicExtensions
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
    public static class TopicExtensions
    {
        public const int MaxDaysToEditMessage = 2;

        public static bool IsPublic( this TopicTypes type )
        {
            if ( type != TopicTypes.Forum && type != TopicTypes.Article )
                return type == TopicTypes.News;
            return true;
        }

        public static string FormatToString( this IEnumerable<TopicTag> tags, string separator = ", " )
        {
            return StringHelper.Join( tags.Select<TopicTag, string>( ( Func<TopicTag, string> ) ( t => t.Name ) ), separator );
        }

        public static Topic GetContent( Topic englishTopic, Topic russianTopic, bool isEnglish )
        {
            return ( !isEnglish ? russianTopic ?? englishTopic : englishTopic ?? russianTopic ) ?? ( isEnglish ? russianTopic : englishTopic );
        }

        public static IEnumerable<SearchPhrase> ToSearchPhrases(
          this string expression )
        {
            if ( expression == null )
                throw new ArgumentNullException( nameof( expression ) );
            List<SearchPhrase> searchPhraseList = new List<SearchPhrase>();
            bool flag = false;
            AllocationArray<char> allocationArray = new AllocationArray<char>(1);
            int num = 0;
            foreach ( char ch in expression )
            {
                switch ( ch )
                {
                    case ' ':
                    if ( flag )
                    {
                        allocationArray.Add( ch );
                        break;
                    }
                    if ( allocationArray.Count > 0 )
                    {
                        string lowerInvariant = new string(allocationArray.Buffer, 0, allocationArray.Count).ToLowerInvariant();
                        bool isSpecial = lowerInvariant == "or" || lowerInvariant == "and";
                        if ( isSpecial || lowerInvariant.Length > 2 )
                            searchPhraseList.Add( new SearchPhrase( lowerInvariant, num - lowerInvariant.Length, false, isSpecial ) );
                        allocationArray.Count = 0;
                        break;
                    }
                    break;
                    case '"':
                    if ( flag )
                    {
                        flag = false;
                        string str = new string(allocationArray.Buffer, 0, allocationArray.Count);
                        searchPhraseList.Add( new SearchPhrase( str, num - str.Length - 1, true, false ) );
                        allocationArray.Count = 0;
                        break;
                    }
                    flag = true;
                    break;
                    default:
                    allocationArray.Add( ch );
                    break;
                }
                ++num;
            }
            if ( allocationArray.Count > 0 )
            {
                string str = new string(allocationArray.Buffer, 0, allocationArray.Count);
                bool isSpecial = str == "or" || str == "and";
                if ( isSpecial || str.Length > 2 )
                    searchPhraseList.Add( new SearchPhrase( str, num - str.Length, false, isSpecial ) );
            }
            while ( searchPhraseList.Count > 0 && searchPhraseList [0].IsSpecial )
                searchPhraseList.RemoveAt( 0 );
            while ( searchPhraseList.Count > 0 && searchPhraseList [searchPhraseList.Count - 1].IsSpecial )
                searchPhraseList.RemoveAt( searchPhraseList.Count - 1 );
            return ( IEnumerable<SearchPhrase> ) searchPhraseList;
        }

        public static string [ ] GetForms( this string expression )
        {
            return new string [1] { expression };
        }

        public static Client GetSecondClient( this Message message )
        {
            return ( ( Message ) TypeHelper.CheckOnNull<Message>(  message, nameof( message ) ) ).Topic.GetSecondClient( message.Client );
        }

        public static Client GetSecondClient( this Topic topic, Client firstClient )
        {
            return topic.GetSecondClient( ( ( BaseEntity ) TypeHelper.CheckOnNull<Client>(  firstClient, nameof( firstClient ) ) ).Id );
        }

        public static Client GetSecondClient( this Topic topic, long firstClientId )
        {
            if ( topic == null )
                throw new ArgumentNullException( nameof( topic ) );
            if ( firstClientId != topic.SecondClient.Id )
                return topic.SecondClient;
            return topic.Client;
        }

        public static bool IsPinned( this Topic topic )
        {
            TopicGroup group = ((Topic) TypeHelper.CheckOnNull<Topic>( topic, nameof (topic))).Group;
            if ( group == null )
                return false;
            
            return  group.Id  == 5L;
        }

        public static void SetPinned( this Topic topic, bool value )
        {
            if ( topic == null )
                throw new ArgumentNullException( nameof( topic ) );
            if ( topic.Group != null && topic.Group.Id != 5L )
                throw new InvalidOperationException( string.Format( "Group {0} is not expected.", ( object ) topic.Group.Id ) );
            Topic topic1 = topic;
            TopicGroup topicGroup;
            if ( !value )
            {
                topicGroup = ( TopicGroup ) null;
            }
            else
            {
                topicGroup = new TopicGroup();
                topicGroup.Id = 5L;
            }
            topic1.Group = topicGroup;
        }
    }
}
