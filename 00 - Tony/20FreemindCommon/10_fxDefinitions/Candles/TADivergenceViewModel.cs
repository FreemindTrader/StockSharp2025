using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class TADivergenceViewModel
    {
        public TADivergenceViewModel( TADivergence taDivergence )
        {
            TaDivergence = taDivergence;
        }

        public TADivergence TaDivergence { get; set; }

        public string ImageUrl
        {
            get
            {
                switch ( TaDivergence )
                {
                    case TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW:
                    case TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM:
                        return "../Images/GreenStar16x16.png";

                    case TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH:
                    case TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_DOUBLE_TOP:
                        return "../Images/RedStar16x16.png";

                    case TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW:
                    case TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP:
                        return "../Images/HiddenNegative16x16.png";

                    case TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH:
                    case TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM:
                        return "../Images/HiddenPositive16x16.png";

                    case TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH:
                    case TADivergence.NEGATIVE_DIVERGENCE_DOUBLE_TOP:
                        return "../Images/00Negative_16x16.png";

                    case TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW:
                    case TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP:
                        return "../Images/HiddenNegative16x16.png";

                    case TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM:
                    case TADivergence.HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH:
                        return "../Images/HiddenPositive16x16.png";


                    case TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM:
                    case TADivergence.POSITIVE_DIVERGENCE_LOWER_LOW:
                        return "../Images/00Positive_16x16.png";                    
                }


                throw new Exception( "Should not be possible" );
            }
            set
            {
                throw new Exception( "Should not be possible" );
            }
        }
    }
}

