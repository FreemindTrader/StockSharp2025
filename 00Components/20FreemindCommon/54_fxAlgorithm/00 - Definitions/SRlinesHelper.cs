using fx.Bars;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Algorithm
{
    public static class SRlinesHelper
    {
        public static MatchedSRinfo GetUpperShadowSRInfo(this SBar bar, TimeSpan period, string name, float srLine)
        {
            MatchedSRinfo output = null;
            var candleRange = bar.WholeCandle;

            if (candleRange.Contains(srLine) || candleRange.WithUpperRange(srLine))
            {
                var upperShadow = bar.UpperShadow;

                if (upperShadow.ExactTouch(srLine))
                {
                    output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.ExactTouch);
                }
                else if (upperShadow.AlmostTouch(srLine))
                {
                    output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.AlmostTouch);
                }
                else if (upperShadow.Contains(srLine))
                {
                    output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.PenetratedAndClosedBelow);
                }
                else
                {
                    var realBody = bar.RealBody;

                    if (realBody.HighestPointExactTouch(srLine))
                    {
                        output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.ExactTouch);
                    }
                    else if (realBody.HighestPointAlmostTouch(srLine))
                    {
                        output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.AlmostTouch);
                    }
                    else if ((bar.Open < srLine) && (bar.Close > srLine))
                    {
                        output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.BrokenAndClosedAbove);
                    }
                    else if ((bar.Open > srLine) && (bar.Close < srLine))
                    {
                        output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.TestedAndFailedBelow);
                    }
                }
            }

            return output;
        }

        public static MatchedSRinfo GetLowerShadowSRInfo(this SBar bar, TimeSpan period, string name, float srLine)
        {
            MatchedSRinfo output = null;
            var candleRange = bar.WholeCandle;

            if (candleRange.Contains(srLine) || candleRange.WithUpperRange(srLine))
            {
                var lowerShadow = bar.LowerShadow;

                if (lowerShadow.ExactTouch(srLine))
                {
                    output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.ExactTouch);
                }
                else if (lowerShadow.AlmostTouch(srLine))
                {
                    output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.AlmostTouch);
                }
                else if (lowerShadow.Contains(srLine))
                {
                    output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.PenetratedAndClosedBelow);
                }
                else
                {
                    var realBody = bar.RealBody;

                    if (realBody.LowestPointExactTouch(srLine))
                    {
                        output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.ExactTouch);
                    }
                    else if (realBody.LowestPointAlmostTouch(srLine))
                    {
                        output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.AlmostTouch);
                    }
                    else if ((bar.Open > srLine) && (bar.Close < srLine))
                    {
                        output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.BrokenAndClosedBelow);
                    }
                    else if ((bar.Open < srLine) && (bar.Close > srLine))
                    {
                        output = new MatchedSRinfo(bar, period, name, srLine, SRLineResponseType.BrokenAndRecoverAbove);
                    }
                }
            }

            return output;
        }
    }
}
