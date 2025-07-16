using System.ComponentModel;

#nullable disable
internal interface IChartModifierBase :
  INotifyPropertyChanged,
  IReceiveMouseEvents
{
    IServiceContainer Services
    {
        get; set;
    }

    IChartModifierSurface ModifierSurface
    {
        get;
    }

    string ModifierName
    {
        get;
    }

    bool IsAttached
    {
        get; set;
    }

    object DataContext
    {
        get; set;
    }

    bool ReceiveHandledEvents
    {
        get;
    }

    void OnAttached();

    void OnDetached();
}
