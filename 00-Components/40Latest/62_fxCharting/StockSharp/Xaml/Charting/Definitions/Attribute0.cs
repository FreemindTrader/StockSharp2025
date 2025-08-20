using System;

internal sealed class Attribute0 : Attribute
{
    private readonly bool bool_0;

    public Attribute0( bool bool_1 = true )
    {
        bool_0 = bool_1;
    }

    public bool GetAttributeValue( )
    {
        return bool_0;
    }
}
