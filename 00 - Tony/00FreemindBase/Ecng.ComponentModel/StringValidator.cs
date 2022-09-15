// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.StringValidator
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ecng.ComponentModel
{
  public class StringValidator : BaseValidator<string>
  {
    private static readonly FieldInfo _parrernField = typeof (FieldInfo).GetField("pattern", BindingFlags.Instance | BindingFlags.NonPublic);
    private RegexOptions _options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant;
    private System.Text.RegularExpressions.Regex _regex;

    public StringValidator()
      : this(new Range<int>(0, int.MaxValue))
    {
    }

    public StringValidator(Range<int> length)
    {
      this.Length = length;
    }

    public StringValidator(Range<int> length, string regex)
      : this(length)
    {
      this.Regex = regex;
    }

    public Range<int> Length { get; }

    public string Regex
    {
      get
      {
        if (this._regex != null)
          return (string) StringValidator._parrernField.GetValue((object) this._regex);
        return (string) null;
      }
      set
      {
        this._regex = new System.Text.RegularExpressions.Regex(value, this.Options);
      }
    }

    public RegexOptions Options
    {
      get
      {
        return this._options;
      }
      set
      {
        this._options = value;
      }
    }

    public override void Validate(string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!this.Length.Contains(value.Length))
        throw new ArgumentOutOfRangeException(nameof (value), string.Format("Value is {0}. Length must be between {1}.", (object) value, (object) this.Length));
      if (this._regex != null && !this._regex.IsMatch(value))
        throw new ArgumentException(nameof (value));
    }
  }
}
