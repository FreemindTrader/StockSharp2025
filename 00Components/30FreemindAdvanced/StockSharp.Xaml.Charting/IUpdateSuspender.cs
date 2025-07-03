using System;

namespace SciChart.Core.Framework;

//
// Summary:
//     Defines the interface to an SciChart.Core.Framework.UpdateSuspender, a disposable
//     class which allows nested suspend/resume operations on an SciChart.Core.Framework.ISuspendable
//     target
public interface IUpdateSuspender : IDisposable
{
    //
    // Summary:
    //     Gets a value indicating whether updates for this instance are currently suspended
    bool IsSuspended
    {
        get;
    }

    //
    // Summary:
    //     Gets or sets a value indicating whether the target will resume when the IUpdateSuspender
    //     is disposed. Default is True
    bool ResumeTargetOnDispose
    {
        get; set;
    }

    //
    // Summary:
    //     Gets or sets an associated Tab for this SciChart.Core.Framework.IUpdateSuspender
    //     instance
    object Tag
    {
        get;
    }
}