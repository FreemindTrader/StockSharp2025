
//namespace SciChart.Data.Numerics.GenericMath;

////
//// Summary:
////     Defines the interface to a generic math helper
////
//// Type parameters:
////   T:
//public interface IMath<T>
//{
//    //
//    // Summary:
//    //     Gets the MinValue for T. for DateTime it returns DateTime.MinValue (it has .Ticks
//    //     = 0)
//    T MinValue
//    {
//        get;
//    }

//    //
//    // Summary:
//    //     Gets the MaxValue for T.
//    T MaxValue
//    {
//        get;
//    }

//    //
//    // Summary:
//    //     Gets the ZeroValue for T. for DateTime it returns DateTime.MinValue (it has .Ticks
//    //     = 0)
//    T ZeroValue
//    {
//        get;
//    }

//    //
//    // Summary:
//    //     Returns the Max of A and B
//    T Max( T a, T b );

//    //
//    // Summary:
//    //     Returns the Min of A and B
//    T Min( T a, T b );

//    //
//    // Summary:
//    //     Returns the Min of A and B greater than a Floor
//    T MinGreaterThan( T floor, T a, T b );

//    //
//    // Summary:
//    //     Returns if T is NaN. Only valid for Float, Double types. For all other types,
//    //     always returns false
//    bool IsNaN( T value );

//    //
//    // Summary:
//    //     Subtracts a - b. For DateTime it returns a new DateTime with .Ticks = a.Ticks
//    //     - b.Ticks
//    T Subtract( T a, T b );

//    //
//    // Summary:
//    //     Get the Absolute value of (a)
//    T Abs( T a );

//    //
//    // Summary:
//    //     Converts to the equivalent value as a double
//    double ToDouble( T value );

//    //
//    // Summary:
//    //     Multiplies lhs * rhs
//    T Mult( T lhs, T rhs );

//    //
//    // Summary:
//    //     Multiplies lhs * rhs
//    T Mult( T lhs, double rhs );    

//    //
//    // Summary:
//    //     Adds lhs + rhs. for DateTime it returns a new DateTime with .Ticks = lhs.Ticks
//    //     + rhs.Ticks
//    T Add( T lhs, T rhs );

//    //
//    // Summary:
//    //     Returns T++ for DateTime it increments .Ticks
//    T Inc( ref T value );

//    //
//    // Summary:
//    //     Returns T-- for DateTime it decrements .Ticks
//    T Dec( ref T value );        
//}
