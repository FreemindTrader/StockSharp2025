using StockSharp.Diagram;
using System;
using System.ComponentModel;
using System.Linq;

internal sealed class DiagramElementProperties : PropertyDescriptor
{

    private readonly IDiagramElementParam _component;

    private readonly string _category;

    public DiagramElementProperties( IDiagramElementParam _param1 )
      : this( _param1.Category, _param1 )
    {
        _component = _param1;
    }

    public DiagramElementProperties( string _param1, IDiagramElementParam _param2 )
      : base( string.Concat( _param2.Name, ( ( object )_param2 ).GetHashCode().ToString() ), Array.Empty<Attribute>() )
    {
        _category = _param1;
        _component = _param2;
    }

    public override bool CanResetValue( object _param1 )
    {
        return false;
    }

    public override object GetValue( object _param1 )
    {
        return _component.Value;
    }

    public override void ResetValue( object _param1 )
    {
    }

    public override void SetValue( object _param1, object _param2 )
    {
        _component.Value = _param2;
    }

    public override bool ShouldSerializeValue( object _param1 )
    {
        return false;
    }

    public override Type ComponentType
    {
        get
        {
            return _component.Type;
        }
    }

    public override bool IsReadOnly
    {
        get
        {
            return _component.Attributes.Any<Attribute>( x => {
                var ro = x as ReadOnlyAttribute;
                if ( ro != null )
                    return ro.IsReadOnly;
                return false;
            } );
        }
    }

    public override Type PropertyType
    {
        get
        {
            return _component.Type;
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
            return _component.DisplayName;
        }
    }

    public override string Description
    {
        get
        {
            return _component.Description;
        }
    }

    public override AttributeCollection Attributes
    {
        get
        {
            return new AttributeCollection( _component.Attributes.ToArray<Attribute>() );
        }
    }
}
