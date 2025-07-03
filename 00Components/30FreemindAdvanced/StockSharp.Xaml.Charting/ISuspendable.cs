namespace SciChart.Core.Framework;

//
// Summary:
//     Types which implement ISuspendable can have updates suspended/resumed. Useful
//     for batch operations
public interface ISuspendable
{
    //
    // Summary:
    //     Gets a value indicating whether updates for the target are currently suspended
    bool IsSuspended
    {
        get;
    }

    //
    // Summary:
    //     Suspends drawing updates on the target until the returned object is disposed,
    //     when a final draw call will be issued
    //
    // Returns:
    //     The disposable Update Suspender
    IUpdateSuspender SuspendUpdates();

    //
    // Summary:
    //     Resumes updates on the target, intended to be called by IUpdateSuspender
    void ResumeUpdates( IUpdateSuspender suspender );

    //
    // Summary:
    //     Called by IUpdateSuspender each time a target suspender is disposed. When the
    //     final target suspender has been disposed, ResumeUpdates is called
    void DecrementSuspend();
}
