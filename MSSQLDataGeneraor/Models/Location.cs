using MSSQLDataGenerator.BDLoader.Attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSSQLDataGenerator.Models
{
    [Index(nameof(Location.City), IsUnique = true)]
    [BulkDataLoaderMasterTable]
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        
        [BulkDataLoader(
            "Mumbai", 
            "Delhi",
            "Bengaluru(Bangalore)",
            "Kolkata(Calcutta)", 
            "Chennai(Madras)",
            "Hyderabad",
            "Ahmedabad",
            "Pune",
            "Surat",
            "Jaipur",
            "Lucknow",
            "Kanpur",
            "Nagpur",
            "Visakhapatnam(Vizag)",
            "Bhopal",
            "Patna",
            "Ludhiana",
            "Agra",
            "Nashik",
            "Vadodara(Baroda)")]
        public string City { get; set; }

    }
}
