using MSSQLDataGenerator.BDLoader.Attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSSQLDataGenerator.Models
{
    [Index(nameof(Employee.Email), IsUnique = true)]
    [Index(nameof(Employee.Phone_number), IsUnique = true)]
    [BulkDataLoaderXRowsOfData(1000000)]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }


        [BulkDataLoader(
            "Emma",
            "Noah",
            "Olivia",
            "Liam",
            "Ava",
            "Ethan",
            "Sophia",
            "Mason",
            "Isabella",
            "Logan",
            "Mia",
            "Lucas",
            "Charlotte",
            "Jackson",
            "Amelia",
            "Aiden",
            "Harper",
            "Caden",
            "Abigail",
            "Grayson",
            "Emily",
            "Elijah",
            "Elizabeth",
            "Oliver",
            "Sofia",
            "Carter",
            "Avery",
            "Sebastian",
            "Scarlett",
            "William")]
        public string First_Name { get; set; }

        [BulkDataLoader("Smith",
            "Johnson",
            "Brown",
            "Davis",
            "Garcia",
            "Rodriguez",
            "Martinez",
            "Hernandez",
            "Lopez",
            "Perez",
            "Gonzalez",
            "Wilson",
            "Anderson",
            "Thomas",
            "Jackson",
            "White",
            "Harris",
            "Martin",
            "Thompson",
            "Moore",
            "Young",
            "Allen",
            "King",
            "Wright",
            "Lee",
            "Walker",
            "Hall",
            "Lewis",
            "Robinson",
            "Perez")]
        public string Last_Name { get; set; }

        [BulkDataLoader(DataFormatType.AlfaNumeric, "{XXXXXXXXXXXXXXXX}@gmail.com")]
        public string Email { get; set; }


        [BulkDataLoader(DataFormatType.Numeric, "+91 {9XXXXXXXXX}")]
        public string Phone_number { get; set; }


        [BulkDataLoader("24-04-1993","27-04-2023")]
        public DateTime Date_of_Joining { get; set; }


        [BulkDataLoader(false, 1, 9)]
        public Int32 Pay_Scale_Fk { get; set; }

        [ForeignKey("Pay_Scale_Fk")]
        public PayScale PayScale { get; set; }


        [BulkDataLoader(false, 1, 10)]
        public Int32 Department_id_Fk { get; set; }

        [ForeignKey("Department_id_Fk")]
        public Department Department { get; set; }

        [BulkDataLoader(false, 1, 20)]
        public Int32 Location_id_Fk { get; set; }

        [ForeignKey("Location_id_Fk")]
        public Location Location { get; set; }


    }
}
