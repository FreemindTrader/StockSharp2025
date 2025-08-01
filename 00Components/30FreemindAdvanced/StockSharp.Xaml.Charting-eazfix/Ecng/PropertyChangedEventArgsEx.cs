using System.ComponentModel;
using System.Diagnostics;

#nullable disable
public sealed class PropertyChangedEventArgsEx : PropertyChangedEventArgs
{

    private object _objectOne;

    private object _objectTwo;

    public PropertyChangedEventArgsEx(
      string _param1,
      object _param2,
      object _param3)
      : base(_param1)
    {
        this.SetObjectOne(_param2);
        this.SetObjectTwo(_param3);
    }

    public object GetObjectOne() => this._objectOne;

    protected void SetObjectOne(object _param1)
    {
        this._objectOne = _param1;
    }

    public object GetObjectTwo()
    {
        return this._objectTwo;
    }

    protected void SetObjectTwo(object _param1)
    {
        this._objectTwo = _param1;
    }
}
