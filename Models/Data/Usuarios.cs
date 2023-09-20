using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovilAPI.Models.Data
{
    public class Usuarios
    {
        [Key]
        public int id { get; set; }
        public string? email { get; set; }
        public string? nombre { get; set; }

        [Required]
        public string? password { get; set; }

        [Required]
        public string? usuario { get; set; }

    }
}
