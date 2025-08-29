using System.ComponentModel;
using System.Diagnostics;

#nullable disable
public sealed class PropertyChangedEventArgsEx :  PropertyChangedEventArgs
{
  
  private object _objectOne;
  
  private object _objectTwo;

  public PropertyChangedEventArgsEx( string propertyName, object _param2, object _param3 )
    : base(propertyName)
  {
    SetObjectOne(_param2);
    SetObjectTwo(_param3);
  }

  public object GetObjectOne() => _objectOne;

  protected void SetObjectOne(object _param1)
  {
    _objectOne = _param1;
  }

  public object GetObjectTwo()
  {
    return _objectTwo;
  }

  protected void SetObjectTwo(object _param1)
  {
    _objectTwo = _param1;
  }
}
