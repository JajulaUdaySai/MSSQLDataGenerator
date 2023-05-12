using MSSQLDataGenerator.BDLoader.Attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSSQLDataGenerator.Models
{
    [BulkDataLoaderMasterTable]
    [Index(nameof(Department.Name), IsUnique = true)]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [BulkDataLoader(
            "Marketing",
            "Finance",
            "Human Resources",
            "IT",
            "Sales",
            "Customer Service",
            "Operations",
            "Research and Development",
            "Legal",
            "Public Relations")]
        public string Name { get; set; }

    }
}
