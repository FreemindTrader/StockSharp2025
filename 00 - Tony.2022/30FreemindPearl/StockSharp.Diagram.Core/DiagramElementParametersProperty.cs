using StockSharp.Diagram;
using System;
using System.ComponentModel;

internal sealed class DiagramElementParametersProperty : PropertyDescriptor
{

    private readonly DiagramSocket _scoket;

    private readonly string _category;

    public DiagramElementParametersProperty( string category, DiagramSocket socket )
      : base( string.Concat( socket.Id, ( ( object )socket ).GetHashCode().ToString() ), Array.Empty<Attribute>() )
    {        
        if ( category == null )
            throw new ArgumentNullException( "category == null" );
        _category = category;
        _scoket = socket;
    }

    public override bool CanResetValue( object _param1 )
    {
        return false;
    }

    public override object GetValue( object _param1 )
    {
        return _scoket.Value;
    }

    public override void ResetValue( object _param1 )
    {
    }

    public override void SetValue( object _param1, object _param2 )
    {
    }

    public override bool ShouldSerializeValue( object _param1 )
    {
        return false;
    }

    public override Type ComponentType
    {
        get
        {
            return _scoket.Type.Type;
        }
    }

    public override bool IsReadOnly
    {
        get
        {
            return true;
        }
    }

    public override Type PropertyType
    {
        get
        {
            return _scoket.Type.Type;
        }
    }

    public override string Category
    {
        get
        {
            return _category;
        }
    }

    public override string DisplayName
    {
        get
        {
            return _scoket.Name;
        }
    }

    public override string Description
    {
        get
        {
            return _scoket.Name;
        }
    }
}
