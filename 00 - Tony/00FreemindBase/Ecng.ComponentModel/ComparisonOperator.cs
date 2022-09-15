using System.ComponentModel.DataAnnotations;

namespace Ecng.ComponentModel
{
    public enum ComparisonOperator
    {
        [Display( Name = "=" )] Equal,
        [Display( Name = "!=" )] NotEqual,
        [Display( Name = ">" )] Greater,
        [Display( Name = ">=" )] GreaterOrEqual,
        [Display( Name = "<" )] Less,
        [Display( Name = "<=" )] LessOrEqual,
        [Display( Name = "Any" )] Any,
    }
}
