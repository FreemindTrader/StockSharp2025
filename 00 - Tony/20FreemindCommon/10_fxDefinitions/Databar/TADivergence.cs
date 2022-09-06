using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    [Flags]
    public enum TADivergence
    {
        NoDivergence                                       = 0,
        IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW            = 1 << 1,
        IMPORTANT_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM        = 1 << 2,

        IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH          = 1 << 3,
        IMPORTANT_NEGATIVE_DIVERGENCE_DOUBLE_TOP           = 1 << 4,

        IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW     = 1 << 5,
        IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP    = 1 << 6,
        IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH   = 1 << 7,
        IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM = 1 << 8,

        NEGATIVE_DIVERGENCE_HIGHER_HIGH                    = 1 << 9,
        NEGATIVE_DIVERGENCE_DOUBLE_TOP                     = 1 << 10,

        HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW               = 1 << 11,
        HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP              = 1 << 12,

        HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM           = 1 << 13,
        HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH             = 1 << 14,
        POSITIVE_DIVERGENCE_DOUBLE_BOTTOM                  = 1 << 15,
        POSITIVE_DIVERGENCE_LOWER_LOW                      = 1 << 16,
    }
}
