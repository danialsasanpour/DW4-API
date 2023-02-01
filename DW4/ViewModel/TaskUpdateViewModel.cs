using System.ComponentModel.DataAnnotations;

namespace DW4.Model
{
    public class TaskUpdateViewModel
    {
        

        [Required]
        public string Token { get; set; }

        [Required]
        public Boolean Done { get; set; }
    }
}
