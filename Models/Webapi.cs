using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Webapi
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string Details { get; set; }
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
