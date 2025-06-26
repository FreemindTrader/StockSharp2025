using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions.UndoRedo
{
    public interface IUndoRedoMember
    {
        void OnCommit( object change );
        void OnUndo( object change );
        void OnRedo( object change );

        object Owner { get; set; }
        string Name { get; set; }
        event EventHandler<MemberChangedEventArgs> Changed;
    }
    internal interface IChangedNotification
    {
        void OnChanged( CommandDoneType type, IChange change );
    }
}
