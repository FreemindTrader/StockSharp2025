// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.TopicExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public static class TopicExtensions
{
    public const int MaxDaysToEditMessage = 2;

    public static bool IsPublic(this TopicTypes type)
    {
        return type == TopicTypes.Forum || type == TopicTypes.Article || type == TopicTypes.News;
    }

    public static string FormatToString(this IEnumerable<TopicTag> tags, string separator = ", ")
    {
        return StringHelper.Join(tags.Select<TopicTag, string>((Func<TopicTag, string>)(t => t.Name)), separator);
    }

    public static Topic GetContent(Topic englishTopic, Topic russianTopic, bool isEnglish)
    {
        return (!isEnglish ? russianTopic ?? englishTopic : englishTopic ?? russianTopic) ?? (isEnglish ? russianTopic : englishTopic);
    }

    public static IEnumerable<SearchPhrase> ToSearchPhrases(this string expression)
    {
        if (expression == null)
            throw new ArgumentNullException(nameof(expression));
        List<SearchPhrase> searchPhrases = new List<SearchPhrase>();
        bool flag = false;
        AllocationArray<char> allocationArray = new AllocationArray<char>(1);
        int num = 0;
        foreach (char ch in expression)
        {
            switch (ch)
            {
                case ' ':
                    if (flag)
                    {
                        allocationArray.Add(ch);
                        break;
                    }
                    if (allocationArray.Count > 0)
                    {
                        string lowerInvariant = new string(allocationArray.Buffer, 0, allocationArray.Count).ToLowerInvariant();
                        bool isSpecial = lowerInvariant == "or" || lowerInvariant == "and";
                        if (isSpecial || lowerInvariant.Length > 2)
                            searchPhrases.Add(new SearchPhrase(lowerInvariant, num - lowerInvariant.Length, false, isSpecial));
                        allocationArray.Count = 0;
                        break;
                    }
                    break;
                case '"':
                    if (flag)
                    {
                        flag = false;
                        string str = new string(allocationArray.Buffer, 0, allocationArray.Count);
                        searchPhrases.Add(new SearchPhrase(str, num - str.Length - 1, true, false));
                        allocationArray.Count = 0;
                        break;
                    }
                    flag = true;
                    break;
                default:
                    allocationArray.Add(ch);
                    break;
            }
            ++num;
        }
        if (allocationArray.Count > 0)
        {
            string str = new string(allocationArray.Buffer, 0, allocationArray.Count);
            bool isSpecial = str == "or" || str == "and";
            if (isSpecial || str.Length > 2)
                searchPhrases.Add(new SearchPhrase(str, num - str.Length, false, isSpecial));
        }
        while (searchPhrases.Count > 0 && searchPhrases[0].IsSpecial)
            searchPhrases.RemoveAt(0);
        while (searchPhrases.Count > 0 && searchPhrases[searchPhrases.Count - 1].IsSpecial)
            searchPhrases.RemoveAt(searchPhrases.Count - 1);
        return (IEnumerable<SearchPhrase>)searchPhrases;
    }

    public static string[] GetForms(this string expression)
    {
        return new string[1] { expression };
    }

    public static Client GetSecondClient(this Message message)
    {
        return TypeHelper.CheckOnNull<Message>(message, nameof(message)).Topic.GetSecondClient(message.Client);
    }

    public static Client GetSecondClient(this Topic topic, Client firstClient)
    {
        return topic.GetSecondClient(TypeHelper.CheckOnNull<Client>(firstClient, nameof(firstClient)).Id);
    }

    public static Client GetSecondClient(this Topic topic, long firstClientId)
    {
        if (topic == null)
            throw new ArgumentNullException(nameof(topic));
        return firstClientId != topic.SecondClient.Id ? topic.SecondClient : topic.Client;
    }

    public static bool IsPinned(this Topic topic)
    {
        TopicGroup group = ((Topic)TypeHelper.CheckOnNull<Topic>(topic, nameof(topic))).Group;
        if (group == null)
            return false;

        return group.Id == 5L;
    }

    public static void SetPinned(this Topic topic, bool value)
    {
        if (topic == null)
            throw new ArgumentNullException(nameof(topic));
        if (topic.Group != null && topic.Group.Id != 5L)
            throw new InvalidOperationException($"Group {topic.Group.Id} is not expected.");
        Topic topic1 = topic;
        TopicGroup topicGroup;
        if (!value)
        {
            topicGroup = (TopicGroup)null;
        }
        else
        {
            topicGroup = new TopicGroup();
            topicGroup.Id = 5L;
        }
        topic1.Group = topicGroup;
    }
}
