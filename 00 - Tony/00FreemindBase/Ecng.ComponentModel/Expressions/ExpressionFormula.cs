// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.Expressions.ExpressionFormula
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Common;
using System;
using System.Collections.Generic;

namespace Ecng.ComponentModel.Expressions
{
  public abstract class ExpressionFormula
  {
    public abstract Decimal Calculate(Decimal[] values);

    protected ExpressionFormula(string expression, IEnumerable<string> identifiers)
    {
      if (expression.IsEmpty())
        throw new ArgumentNullException(nameof (expression));
      this.Expression = expression;
      IEnumerable<string> strings = identifiers;
      if (strings == null)
        throw new ArgumentNullException(nameof (identifiers));
      this.Identifiers = strings;
    }

    internal ExpressionFormula(string error)
    {
      if (error.IsEmpty())
        throw new ArgumentNullException(nameof (error));
      this.Error = error;
    }

    public string Expression { get; }

    public string Error { get; }

    public IEnumerable<string> Identifiers { get; }

    public static IEnumerable<string> Functions
    {
      get
      {
        return ExpressionHelper.Functions;
      }
    }

    public override string ToString()
    {
      return this.Error ?? this.Expression;
    }
  }
}
