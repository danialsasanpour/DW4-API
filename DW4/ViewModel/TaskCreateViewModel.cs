using System.ComponentModel.DataAnnotations;

namespace DW4.Model
{
    public class TaskCreateViewModel 
    {
        [Required]
        public string AssignedToUid { get; set; }

        [Required]
        public string Description { get; set; }


        [Required]
        public string Token { get; set; }
    }
}
