// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.UrlExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Net;
using Ecng.Reflection;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public static class UrlExtensions
{
    private static readonly SynchronizedPairSet<string, Type> _newIdentifiers = new SynchronizedPairSet<string, Type>((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase);
    private static readonly SynchronizedPairSet<string, Type> _oldIdentifiers = new SynchronizedPairSet<string, Type>((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase);
    public const int MaxWidth = 200;
    public const int MaxHeight = 200;
    private static readonly PairSet<string, string> _tags;
    private const string _fieldPrefix = "___";
    private static readonly Regex _re;
    public const string SizeKey = "size";

    static UrlExtensions()
    {
        PairSet<string, string> pairSet = new PairSet<string, string>();
        ((KeyedCollection<string, string>)pairSet).Add("c#", "csharp");
        ((KeyedCollection<string, string>)pairSet).Add("s#", "stocksharp");
        ((KeyedCollection<string, string>)pairSet).Add(".net", "dotnet");
        ((KeyedCollection<string, string>)pairSet).Add("c++", "cpp");
        ((KeyedCollection<string, string>)pairSet).Add("s#.api", "api");
        ((KeyedCollection<string, string>)pairSet).Add("s#.studio", "studio");
        ((KeyedCollection<string, string>)pairSet).Add("s#.data", "hydra");
        UrlExtensions._tags = pairSet;
        UrlExtensions._re = new Regex("___\\w+___", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        foreach (Type implementation in ReflectionHelper.FindImplementations<BaseEntity>(typeof(BaseEntity).Assembly, false, false, false, (Func<Type, bool>)null))
        {
            ((KeyedCollection<string, Type>)UrlExtensions._newIdentifiers).Add($"{StringHelper.ToLower(implementation.Name[0], true).ToString()}{implementation.Name.Substring(1, implementation.Name.Length - 1)}Id", implementation);
            string str = new string(((IEnumerable<char>)implementation.Name.ToCharArray()).Where<char>(new Func<char, bool>(char.IsUpper)).ToArray<char>()).ToLowerInvariant();
            if (implementation == typeof(Favorite))
                str = "fv";
            else if (implementation == typeof(Email))
                str = "em";
            else if (implementation == typeof(Strategy))
                str = "sr";
            else if (implementation == typeof(StrategyPositionChange))
                str = "srpc";
            else if (implementation == typeof(StrategyEquityChange))
                str = "srec";
            else if (implementation == typeof(Domain))
                str = "dom";
            else if (implementation == typeof(Poll))
                str = "pl";
            else if (implementation == typeof(PollVote))
                str = "plv";
            else if (implementation == typeof(Payment))
                str = "pay";
            else if (implementation == typeof(PayGateway))
                str = "paygw";
            else if (implementation == typeof(PayGatewayDomain))
                str = "paygwd";
            else if (implementation == typeof(Subscription))
                str = "sub";
            else if (implementation == typeof(Suspicious))
                str = "sus";
            else if (implementation == typeof(Social))
                str = "soc";
            else if (implementation == typeof(StrategyType))
                str = "stype";
            else if (implementation == typeof(StrategyParam))
                str = "sparam";
            else if (implementation == typeof(DataPermission))
                str = "dper";
            else if (implementation == typeof(SocialToken))
                str = "stkn";
            ((KeyedCollection<string, Type>)UrlExtensions._oldIdentifiers).Add(str + "id", implementation);
        }
    }

    public static string GetGravatarUrl(this string gravatarToken, string avatarSize)
    {
        if (StringHelper.IsEmpty(gravatarToken))
            throw new ArgumentNullException(nameof(gravatarToken));
        int num = StringHelper.IsEmpty(avatarSize) ? 200 : Converter.To<int>((object)avatarSize.Substring(0, avatarSize.IndexOf('x')));
        return NetworkHelper.GetGravatarUrl(gravatarToken, num);
    }

    public static string GetTagUrlName(this string tagName)
    {
        return CollectionHelper.TryGetValue<string, string>((IDictionary<string, string>)UrlExtensions._tags, tagName.ToLowerInvariant()) ?? tagName;
    }

    public static string GetTagName(this string tagUrl)
    {
        return CollectionHelper.TryGetKey<string, string>(UrlExtensions._tags, tagUrl.ToLowerInvariant()) ?? tagUrl;
    }

    public static string ToFullAbsolute(this string virtualPath, Domain domain, bool urlEscape = true)
    {
        return virtualPath.ToFullAbsolute(TypeHelper.CheckOnNull<Domain>(domain, nameof(domain)).Code, urlEscape);
    }

    public static Type GetEntityType(string queryKey, bool newFormat)
    {
        Type type;
        if (!UrlExtensions.TryGetEntityType(queryKey, newFormat, out type))
            throw new ArgumentException($"For key {queryKey} no any type associated.", nameof(queryKey));
        return type;
    }

    public static bool TryGetEntityType(string queryKey, bool newFormat, out Type type)
    {
        if (StringHelper.EqualsIgnoreCase(queryKey, "profileId"))
            queryKey = "clientId";
        return (newFormat ? (KeyedCollection<string, Type>)UrlExtensions._newIdentifiers : (KeyedCollection<string, Type>)UrlExtensions._oldIdentifiers).TryGetValue(queryKey, out type);
    }

    public static string GetIdentity(Type entityType, bool newFormat)
    {
        return (newFormat ? UrlExtensions._newIdentifiers : UrlExtensions._oldIdentifiers)[entityType];
    }

    public static string GetIdentity<T>(bool newFormat)
    {
        return UrlExtensions.GetIdentity(typeof(T), newFormat);
    }

    public static IEnumerable<string> SplitTags(this string groups)
    {
        if (StringHelper.IsEmpty(groups))
            return (IEnumerable<string>)Array.Empty<string>();
        string str = groups.IndexOf(',') != -1 ? "," : " ";
        return ((IEnumerable<string>)StringHelper.SplitBySep(groups, str, true)).Select<string, string>((Func<string, string>)(g => g.Trim().Replace("?", string.Empty).Replace("$", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty))).Where<string>((Func<string, bool>)(s => !StringHelper.IsEmpty(s))).Distinct<string>((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase);
    }

    public static string GetIdNameUrlPart(this long id, string name = null)
    {
        string idNameUrlPart = Converter.To<string>((object)id);
        if (!StringHelper.IsEmpty(name))
        {
            name = NetworkHelper.CheckUrl(name.Replace('.', '_').Replace(' ', '-'), true, true, true);
            if (!StringHelper.IsEmpty(name))
                idNameUrlPart = $"{idNameUrlPart}/{name}";
        }
        return idNameUrlPart;
    }

    public static string GetProductUrlPart(this long id, string urlAlias, string packageId)
    {
        return StringHelper.IsEmpty(urlAlias, StringHelper.IsEmpty(packageId, Converter.To<string>((object)id))).ToLowerInvariant();
    }

    public static bool IsArg(this string key) => key.StartsWith("___") && key.EndsWith("___");

    public static IEnumerable<(string name, string value)> SplitArgs(this string args)
    {
        return _(args).Reverse<(string, string)>();

        static IEnumerable<(string name, string value)> _(string args)
        {
            IOrderedEnumerable<Match> source = UrlExtensions._re.Matches(args).Cast<Match>().OrderByDescending<Match, int>((Func<Match, int>)(m => m.Index));
            int prevIdx = args.Length;
            Func<Match, (string, string)> selector = (Func<Match, (string, string)>)(m =>
            {
                string str1 = m.Value;
                int startIndex = m.Index + str1.Length + 1;
                string str2 = startIndex >= args.Length ? string.Empty : args.Substring(startIndex, MathHelper.Max(prevIdx - startIndex, 0));
                if (str2.EndsWith("\r\n"))
                    str2 = str2.Substring(0, str2.Length - "\r\n".Length);
                prevIdx = m.Index;
                return (str1, str2);
            });
            return source.Select<Match, (string, string)>(selector);
        }
    }    

    public static string JoinArgs(this IEnumerable<(string name, string value)> args)
    {
        return StringHelper.JoinRN(args.Select<ValueTuple<string, string>, string>((Func<ValueTuple<string, string>, string>)(a => a.Item1 + "=" + a.Item2)));
    }

    public static string GetIdNameUrlPart<TEntity>(this TEntity entity) where TEntity : BaseEntity, INameEntity
    {
        if ((object)entity == null)
            throw new ArgumentNullException(nameof(entity));
        return entity.Id.GetIdNameUrlPart(entity.Name);
    }

    public static string ToRelative(this string absoluteUrl)
    {
        return StringHelper.Remove(StringHelper.Remove(StringHelper.Remove(StringHelper.Remove(StringHelper.Remove(absoluteUrl, "http://localhost/stocksharp/", true), "http://stocksharp.com/", true), "http://stocksharp.ru/", true), "https://stocksharp.com/", true), "https://stocksharp.ru/", true);
    }
}
