using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAppWebApi.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public required string UserName { get; set; }

        public required string UserEmail { get; set; }

        public required byte[] Password { get; set; }

        public required byte[] Salt { get; set; }
    }
}
