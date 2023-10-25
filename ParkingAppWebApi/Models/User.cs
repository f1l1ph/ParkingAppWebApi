using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAppWebApi.Models
{
    class User
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string UserName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Password { get; set; }

        [Column(TypeName = "varbinary(16)")]
        public string? Salt { get; set; }
    }
}
