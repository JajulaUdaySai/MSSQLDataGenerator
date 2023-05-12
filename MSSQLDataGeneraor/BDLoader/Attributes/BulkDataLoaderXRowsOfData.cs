namespace MSSQLDataGenerator.BDLoader.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BulkDataLoaderXRowsOfData : Attribute
    {
        public Int64 NumberOfRows { get; set; }

        public BulkDataLoaderXRowsOfData(Int64 NumberOfRows)
        {
            this.NumberOfRows = NumberOfRows;
        }

    }
}
