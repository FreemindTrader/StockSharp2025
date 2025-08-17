// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.ClientExtensions
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

public static class ClientExtensions
{
    public const string HashTokenKey = "clientHash";
    public const int OnlineMinutesWindow = 5;

    public static bool IsDeleted(this Client client)
    {
        return client.Deleted || client.SelfDeleted.GetValueOrDefault();
    }

    public static bool IsLocked(this Client client, bool checkApproved = false)
    {
        if (client.IsDeleted() || client.IsLockedOut.GetValueOrDefault())
            return true;
        return checkApproved && !client.IsApproved.GetValueOrDefault();
    }

    public static bool IsAnonymous(this Client client)
    {
        return TypeHelper.CheckOnNull<Client>(client, nameof(client)).Id == 145183L;
    }

    public static bool IsOnline(this Client client)
    {
        DateTime? lastActivityDate = client.LastActivityDate;
        ref DateTime? local = ref lastActivityDate;
        return local.HasValue && local.GetValueOrDefault().IsOnline();
    }

    public static bool IsOnline(this DateTime lastActivityDate)
    {
        return (DateTime.UtcNow - lastActivityDate).TotalMinutes < 5.0;
    }

    public static string GetToken(this Client client)
    {
        return TypeHelper.CheckOnNull<Client>(client, nameof(client)).Id.GetToken();
    }

    public static string GetToken(this long clientId) => Converter.To<string>((object)clientId);

    public static bool VerifySupport(this Client client)
    {
        DateTime? supportTill = (DateTime?)client?.SupportTill;
        DateTime utcNow = DateTime.UtcNow;
        return supportTill.HasValue && supportTill.GetValueOrDefault() > utcNow;
    }

    public static int GetFilledPercentage(this Client client)
    {
        int filledPercentage = 0;
        if (!StringHelper.IsEmpty(client.VKontakte) || !StringHelper.IsEmpty(client.Facebook))
            filledPercentage += 10;
        if (!StringHelper.IsEmpty(client.Skype))
            filledPercentage += 10;
        if (!StringHelper.IsEmpty(client.City))
            filledPercentage += 10;
        if (client.Birthday.HasValue)
            filledPercentage += 10;
        File picture = client.Picture;
        if ((picture != null ? (!picture.Deleted ? 1 : 0) : 0) != 0)
            filledPercentage += 20;
        if (client.PublicDescription != null)
        {
            if (!StringHelper.IsEmpty(client.PublicDescription.Text))
                filledPercentage += 10;
            Topic topic = client.PublicDescription.Topic;
            int num1;
            if (topic == null)
            {
                num1 = 0;
            }
            else
            {
                int? length = topic.Tags?.Items?.Length;
                int num2 = 0;
                num1 = length.GetValueOrDefault() > num2 & length.HasValue ? 1 : 0;
            }
            if (num1 != 0)
                filledPercentage += 20;
        }
        return filledPercentage;
    }

    public static BaseEntitySet<Client> ToClientsSet(this string str)
    {
        return ((IEnumerable<string>)StringHelper.SplitByComma(str, false)).Select<string, long?>((Func<string, long?>)(s => Converter.To<long?>((object)s))).Where<long?>((Func<long?, bool>)(i => i.HasValue)).Select<long?, Client>((Func<long?, Client>)(i =>
        {
            return new Client() { Id = i.Value };
        })).ToEntitySet<Client>();
    }

    public static string ToClientsString(this BaseEntitySet<Client> clients)
    {
        return clients?.Items != null ? StringHelper.JoinComma(((IEnumerable<Client>)clients.Items).Select<Client, string>((Func<Client, string>)(c => Converter.To<string>((object)c.Id)))) : string.Empty;
    }

    public static string GetAccessToken(this Client client)
    {
        BaseEntitySet<AccessToken> accessTokens = TypeHelper.CheckOnNull<Client>(client, nameof(client)).AccessTokens;
        string str;
        if (accessTokens == null)
        {
            str = (string)null;
        }
        else
        {
            AccessToken[] items = accessTokens.Items;
            str = items != null ? ((IEnumerable<AccessToken>)items).FirstOrDefault<AccessToken>()?.Text : (string)null;
        }
        return str ?? client.AuthToken;
    }

    public static File GetAvatarFile(this Client client)
    {
        File avatarFile = TypeHelper.CheckOnNull<Client>(client, nameof(client)).Picture;
        if (avatarFile == null)
        {
            File file = new File();
            file.Id = client.Gender == 2 ? 101449L : 101448L;
            avatarFile = file;
        }
        return avatarFile;
    }
}
