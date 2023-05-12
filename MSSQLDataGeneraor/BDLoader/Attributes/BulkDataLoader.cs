namespace MSSQLDataGenerator.BDLoader.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class BulkDataLoader : Attribute
    {
        public string Format { get; set; }
        public DataFormatType FormatType { get; set; }
        public bool HasFormat { get; set; }
        public bool IsUnique { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public List<string> Values { get; set; }
        public List<Int16> Int16Values { get; set; }
        public List<Int32> Int32Values { get; set; }
        public List<Int64> Int64Values { get; set; }


        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }


        //For String
        public BulkDataLoader( params string[] values)
        {
            this.HasFormat = false;
            Values = values.ToList();
        }

        public BulkDataLoader( DataFormatType formatType,string Format)
        {
            this.HasFormat = true;
            this.Format = Format;
            FormatType = formatType;
        }

        //For Intigers
        public BulkDataLoader(params Int64[] values)
        {
            Int64Values = values.ToList();
        }
        public BulkDataLoader(params Int32[] values)
        {
            Int32Values = values.ToList();
        }
        public BulkDataLoader(params Int16[] values)
        {
            Int16Values = values.ToList();
        }

        //for Intigers
        public BulkDataLoader(bool isUnique, int minValue, int maxValue, int minLength = 1 , int maxLength = 1)
        {
            IsUnique = isUnique;
            MinLength = minLength;
            MaxLength = maxLength;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public BulkDataLoader(string FromDateTime, string ToDateTime)
        {
            FromDate =  DateTime.Parse(FromDateTime);
            ToDate = DateTime.Parse(ToDateTime);
        }
    }


    public enum DataFormatType
    {
        AlfaNumeric = 0,
        Numeric = 1,
        Alfabetic = 2
    }
}
