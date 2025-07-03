namespace SciChart.Core.Framework;

//
// Summary:
//     Types which implement IInvalidatableElement can be invalidated (redrawn)
public interface IInvalidatableElement
{
    //
    // Summary:
    //     Asynchronously requests that the element redraws itself plus children. Will be
    //     ignored if the element is ISuspendable and currently IsSuspended (within a SuspendUpdates/ResumeUpdates
    //     call)
    void InvalidateElement();
}
