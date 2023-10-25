using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingAppWebApi.Models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "varchar(7)")]
        public required string PlateNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeleteDate { get; set; }

        [Column(TypeName ="text")]
        public string? Description { get; set; }
    }
}
