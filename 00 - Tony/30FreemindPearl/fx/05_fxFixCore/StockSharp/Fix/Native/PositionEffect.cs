namespace StockSharp.Fix.Native
{
    /// <summary>
    /// Indicates whether the resulting position after a trade should be an opening position or closing position.
    /// </summary>
    public static class PositionEffect
    {
        /// <summary>Close.</summary>
        public const char Close = 'C';
        /// <summary>Default.</summary>
        public const char Default = 'D';
        /// <summary>FIFO.</summary>
        public const char FIFO = 'F';
        /// <summary>Close but notify on open.</summary>
        public const char CloseNotify = 'N';
        /// <summary>Open.</summary>
        public const char Open = 'O';
        /// <summary>Rolled.</summary>
        public const char Rolled = 'R';
    }
}
