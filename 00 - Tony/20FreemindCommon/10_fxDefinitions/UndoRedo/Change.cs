using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions.UndoRedo
{
    interface IChange
    {
        object NewObject { get; set; }
        object OldObject { get; set; }
    }
    class Change<TState> : IChange
    {
        public TState OldState;
        public TState NewState;

        #region IChange Members

        object IChange.NewObject
        {
            get
            {
                return NewState;
            }
            set
            {
                NewState = ( TState ) value;
            }
        }

        object IChange.OldObject
        {
            get
            {
                return OldState;
            }
            set
            {
                OldState = ( TState ) value;
            }
        }

        #endregion
    }
}
