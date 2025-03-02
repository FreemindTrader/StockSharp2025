// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ExpiredException
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

using System;

namespace StockSharp.Web.Api.Interfaces
{
    public class ExpiredException : InvalidOperationException
    {
        public ExpiredException(string message)
          : base(message)
        {
        }
    }
}
