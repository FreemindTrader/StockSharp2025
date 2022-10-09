using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fx.Definitions
{
    public class fxFibLevels
    {                
        /// <summary>
        /// This call is used to store all the properties for every single fib levels
        /// </summary>
        /// <param name="percentage"></param>
        /// <param name="value"></param>
        /// <param name="fibTargetType"></param>
        /// <param name="strength"></param>
        public fxFibLevels( FibPercentage percentage, double value, FibonacciTargetType targetType, int fibStrength )
        {
            Value = value;
            Percentage = percentage;
            TargetType = targetType;
            FibStrength = fibStrength;
        }

        public double Value { get; set; }


        int _fibStrength;
        FibonacciTargetType _targetType;
        FibPercentage _percentage;

        public FibPercentage Percentage
        {
            get => _percentage;
            set => _percentage = value;
        }


        public FibonacciTargetType TargetType
        {
            get => _targetType;
            set => _targetType = value;
        }

        
        public int FibStrength
        {
            get => _fibStrength;
            set => _fibStrength = value;
        }
        

    }


}
