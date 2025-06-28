using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public abstract class SelectCommand(object instance, bool canEdit) : BaseStudioCommand
{
    public override StudioCommandDirections PossibleDirection => StudioCommandDirections.Top;

    public virtual Type InstanceType
    {
        get
        {
            Type type = this.Instance?.GetType();
            return (object)type != null ? type : typeof(object);
        }
    }

    public object Instance { get; } = instance;

    public bool CanEdit { get; } = canEdit;
}
