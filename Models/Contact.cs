using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required,RegularExpression(@"^[\w ]+$")]
        public string FirstName { get; set; }
        [Required, RegularExpression(@"^[\w ]+$")]
        public string LastName { get; set; }
        [Required]
        public string DOB { get; set; }
        [RegularExpression(@"^[\w ]+$")]
        public string NickName { get; set; }
        public virtual IEnumerable<Address> Addresses { get; set; }
        
    }
}
