namespace StockSharp.Algo.History.Russian.Finam
{
    public class FinamSecurityInfo
    {
        private long long_0;
        private long long_1;
        private string string_0;
        private string string_1;
        private int int_0;

        public long FinamSecurityId
        {
            get
            {
                return this.long_0;
            }
            set
            {
                this.long_0 = value;
            }
        }

        public long FinamMarketId
        {
            get
            {
                return this.long_1;
            }
            set
            {
                this.long_1 = value;
            }
        }

        public string Code
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string Name
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public int Decimals
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }
    }
}
