// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.Extensions
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ecng.ComponentModel
{
  public static class Extensions
  {
    public static string GetDisplayName(this ICustomAttributeProvider provider, string defaultValue = null)
    {
      Assembly provider1 = provider as Assembly;
      if ((object) provider1 != null)
        return provider1.GetAttribute<AssemblyTitleAttribute>(true)?.Title;
      DisplayAttribute attribute1 = provider.GetAttribute<DisplayAttribute>(true);
      switch (attribute1?.Name)
      {
        case null:
          DisplayNameAttribute attribute2 = provider.GetAttribute<DisplayNameAttribute>(true);
          if (attribute2 != null)
            return attribute2.DisplayName;
          return defaultValue ?? provider.GetTypeName();
        default:
          return attribute1.GetName();
      }
    }

    public static string GetDisplayName(this PropertyDescriptor pd, string defaultValue = null)
    {
      foreach (object attribute in pd.Attributes)
      {
        DisplayAttribute displayAttribute = attribute as DisplayAttribute;
        if (displayAttribute != null)
          return displayAttribute.GetName();
        DisplayNameAttribute displayNameAttribute = attribute as DisplayNameAttribute;
        if (displayNameAttribute != null)
          return displayNameAttribute.DisplayName;
      }
      return defaultValue ?? pd.PropertyType.Name;
    }

    public static string GetDescription(this ICustomAttributeProvider provider, string defaultValue = null)
    {
      Assembly provider1 = provider as Assembly;
      if ((object) provider1 != null)
        return provider1.GetAttribute<AssemblyDescriptionAttribute>(true)?.Description;
      DisplayAttribute attribute1 = provider.GetAttribute<DisplayAttribute>(true);
      switch (attribute1?.Description)
      {
        case null:
          DescriptionAttribute attribute2 = provider.GetAttribute<DescriptionAttribute>(true);
          if (attribute2 != null)
            return attribute2.Description;
          return defaultValue ?? provider.GetTypeName();
        default:
          return attribute1.GetDescription();
      }
    }

    public static string GetCategory(this ICustomAttributeProvider provider, string defaultValue = null)
    {
      DisplayAttribute attribute1 = provider.GetAttribute<DisplayAttribute>(true);
      switch (attribute1?.GroupName)
      {
        case null:
          CategoryAttribute attribute2 = provider.GetAttribute<CategoryAttribute>(true);
          if (attribute2 != null)
            return attribute2.Category;
          return defaultValue ?? provider.GetTypeName();
        default:
          return attribute1.GetGroupName();
      }
    }

    private static string GetTypeName(this ICustomAttributeProvider provider)
    {
      return ((MemberInfo) provider).Name;
    }

    public static string GetDisplayName(this object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      string str = value.ToString();
      Type type = value.GetType();
      if (!(value is Enum))
      {
        ICustomAttributeProvider provider = value as ICustomAttributeProvider;
        if (provider != null)
          return provider.GetDisplayName((string) null);
        return type.GetDisplayName(str);
      }
      FieldInfo field = type.GetField(str);
      if ((object) field == null)
        return str;
      return field.GetDisplayName((string) null);
    }

    public static string GetFieldDisplayName<TField>(this TField field)
    {
      return field.GetType().GetField(field.ToString()).GetDisplayName((string) null);
    }

    public static string GetFieldDescription<TField>(this TField field)
    {
      return field.GetType().GetField(field.ToString()).GetAttribute<DisplayAttribute>(true)?.GetDescription();
    }

    public static Uri GetFieldIcon<TField>(this TField field)
    {
      Type type = field.GetType();
      IconAttribute attribute = type.GetField(field.ToString()).GetAttribute<IconAttribute>(true);
      if (attribute == null)
        return (Uri) null;
      if (!attribute.IsFullPath)
        return attribute.Icon.GetResourceUrl(type);
      return new Uri(attribute.Icon, UriKind.Relative);
    }

    public static string GetDocUrl(this Type type)
    {
      return type.GetAttribute<DocAttribute>(true)?.DocUrl;
    }

    public static Uri GetIconUrl(this Type type)
    {
      IconAttribute attribute = type.GetAttribute<IconAttribute>(true);
      if (attribute == null)
        return (Uri) null;
      if (!attribute.IsFullPath)
        return attribute.Icon.GetResourceUrl(type);
      return new Uri(attribute.Icon, UriKind.Relative);
    }

    public static Uri GetResourceUrl(this string resName)
    {
      return Assembly.GetEntryAssembly().GetResourceUrl(resName);
    }

    public static Uri GetResourceUrl(this string resName, Type type)
    {
      if ((object) type == null)
        throw new ArgumentNullException(nameof (type));
      return type.Assembly.GetResourceUrl(resName);
    }

    private static Uri GetResourceUrl(this Assembly assembly, string resName)
    {
      if ((object) assembly == null)
        throw new ArgumentNullException(nameof (assembly));
      if (resName.IsEmpty())
        throw new ArgumentNullException(nameof (resName));
      string fullName = assembly.FullName;
      return new Uri("/" + fullName.Substring(0, fullName.IndexOf(',')) + ";component/" + resName, UriKind.Relative);
    }

    public static IEnumerable<IItemsSourceItem> GetValues(
      this ItemsSourceAttribute attr)
    {
      if (attr == null)
        throw new ArgumentNullException(nameof (attr));
      return attr.Type.CreateInstance<IItemsSource>().Values;
    }
  }
}
