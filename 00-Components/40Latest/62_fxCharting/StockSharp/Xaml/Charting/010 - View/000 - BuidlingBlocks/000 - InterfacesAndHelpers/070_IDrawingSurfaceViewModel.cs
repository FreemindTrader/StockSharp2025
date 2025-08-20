using System.Windows.Input;

#nullable disable
public interface IScichartSurfaceMVVM
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
