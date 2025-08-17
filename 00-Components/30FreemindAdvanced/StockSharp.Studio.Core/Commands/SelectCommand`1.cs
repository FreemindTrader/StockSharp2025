using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class SelectCommand<T>(T instance, bool canEdit) : SelectCommand((object)instance, canEdit)
{
    public override Type InstanceType => typeof(T);

    public T TypedInstance => (T)this.Instance;
}
