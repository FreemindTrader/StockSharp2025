using System;
using System.Collections.Generic;
using System.Collections;


namespace fx.Definitions
{

    public enum OverLeverageStatus
    {
        INVALID = 0,
        PROCESSING = 1,
        PROCESSED = 2,
        WARNING = 80,
        DANGEROUS = 70,
        CRITICAL = 60,
        IMMEDIATE = 50
    }


    public class OverLeverageMsg
    {
        // Fields...
        string _accountName;
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
            }
        }

        double _usableMarginPercentage;
        public double UsableMarginPercentage
        {
            get { return _usableMarginPercentage; }
            set
            {
                _usableMarginPercentage = value;
            }
        }



        public OverLeverageMsg( string accountName, double usableMarginPercentage )
        {
            _accountName = accountName;
            _usableMarginPercentage = usableMarginPercentage;
        }
    }
}
