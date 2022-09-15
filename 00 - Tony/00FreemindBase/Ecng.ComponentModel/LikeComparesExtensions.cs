// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.LikeComparesExtensions
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;

namespace Ecng.ComponentModel
{
  public static class LikeComparesExtensions
  {
    public static bool Like(this string value, string like, LikeCompares? likeCompare)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (like.IsEmpty())
        return true;
      if (likeCompare.HasValue)
      {
        switch (likeCompare.GetValueOrDefault())
        {
          case LikeCompares.Contains:
            break;
          case LikeCompares.StartWith:
            return value.StartsWithIgnoreCase(like);
          case LikeCompares.EndWith:
            return value.EndsWithIgnoreCase(like);
          case LikeCompares.Equals:
            return value.EqualsIgnoreCase(like);
          case LikeCompares.NotContains:
            return !value.ContainsIgnoreCase(like);
          default:
            throw new ArgumentOutOfRangeException(nameof (likeCompare));
        }
      }
      return value.ContainsIgnoreCase(like);
    }

    public static string ToExpression(this string like, LikeCompares? likeCompare = null)
    {
      if (like.IsEmpty())
        throw new ArgumentNullException(nameof (like));
      if (likeCompare.HasValue)
      {
        switch (likeCompare.GetValueOrDefault())
        {
          case LikeCompares.Contains:
            break;
          case LikeCompares.StartWith:
            return like + "%";
          case LikeCompares.EndWith:
            return "%" + like;
          case LikeCompares.Equals:
            return like;
          case LikeCompares.NotContains:
            throw new NotSupportedException();
          default:
            throw new ArgumentOutOfRangeException(nameof (likeCompare));
        }
      }
      return "%" + like + "%";
    }
  }
}
