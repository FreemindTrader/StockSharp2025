using System.Windows.Input;

#nullable disable
public interface IDrawingSurfaceViewModel
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
