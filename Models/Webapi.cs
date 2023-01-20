using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Webapi
    {
        [Required]
        public int Id { get; set; }
        public string? name { get; set; }
    }
}
