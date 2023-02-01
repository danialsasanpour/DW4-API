using System.ComponentModel.DataAnnotations;

namespace DW4.Model
{
    public class Session
    {
        [Key]
        public string Token { get; set; }

        [Required]
        public string Email { get; set; }

        public Session()
        {
            Token = Guid.NewGuid().ToString();
        }

    }
}
