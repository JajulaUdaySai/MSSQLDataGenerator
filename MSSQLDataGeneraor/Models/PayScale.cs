using MSSQLDataGenerator.BDLoader.Attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSSQLDataGenerator.Models
{
    [BulkDataLoaderExactTable]
    [Index(nameof(PayScale.Code), IsUnique = true)]
    public class PayScale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [BulkDataLoader("A",
            "B1","B2","B3",
            "C1","C2",
            "D1","D2",
            "E"
            )]
        public string Code { get; set; }

        [BulkDataLoader((Int64)180000,
            300000,600000,800000,
            1000000,1500000,
            2000000,3000000,
            5000000
            )]
        public Int64 MinSalary { get; set; }

        [BulkDataLoader((Int64)290000,
           590000, 790000, 990000,
           1490000, 1990000,
           2990000, 4900000,
           8000000
           )]
        public Int64 MaxSalary { get; set; }

    }
}
