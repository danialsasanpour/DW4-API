using System.ComponentModel.DataAnnotations;

namespace DW4.Model
{
    public class User : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public User() : base()
        {

        }


    }
}
