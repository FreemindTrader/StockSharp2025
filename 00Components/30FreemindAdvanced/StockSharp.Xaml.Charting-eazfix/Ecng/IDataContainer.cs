namespace MatterHackers.Agg
{
    internal interface IDataContainer<T>
    {
        T[ ] Array
        {
            get;
        }

        void RemoveLast();
    }
}
