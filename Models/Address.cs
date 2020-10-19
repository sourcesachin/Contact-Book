
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UserApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string Line1 { get; set; }
        [RegularExpression(@"^[\w ]+$")]
        public string Line2 { get; set; }
        public string Country { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Postcode { get; set; }

        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}
