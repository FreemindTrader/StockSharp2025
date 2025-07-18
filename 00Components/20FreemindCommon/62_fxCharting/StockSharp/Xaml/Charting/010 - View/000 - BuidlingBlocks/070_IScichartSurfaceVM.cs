using System.Windows.Input;

#nullable disable
public interface IScichartSurfaceVM
{
    string Title
    {
        get; set;
    }

    void ZoomExtents();

    ICommand ClosePaneCommand
    {
        get; set;
    }
}
