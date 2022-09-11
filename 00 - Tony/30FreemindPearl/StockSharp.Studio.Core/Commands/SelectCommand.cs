// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.SelectCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public abstract class SelectCommand : BaseStudioCommand
    {
        private readonly bool _canEdit;
        private readonly Func<bool> _canEditFunc;

        protected SelectCommand( Type instanceType, object instance, bool canEdit )
        {
            _canEdit = canEdit;
            InstanceType = instanceType;
            Instance = instance;
        }

        protected SelectCommand( Type instanceType, object instance, Func<bool> canEdit )
        {
            Func<bool> func = canEdit;
            if ( func == null )
                throw new ArgumentNullException( nameof( canEdit ) );
            _canEditFunc = func;
            InstanceType = instanceType;
            Instance = instance;
        }

        public override StudioCommandDirections PossibleDirection
        {
            get
            {
                return StudioCommandDirections.Top;
            }
        }

        public Type InstanceType { get; }

        public object Instance { get; }

        public bool CanEdit
        {
            get
            {
                Func<bool> canEditFunc = _canEditFunc;
                if ( canEditFunc == null )
                    return _canEdit;
                return canEditFunc();
            }
        }
    }
}
