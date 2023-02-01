using System.ComponentModel.DataAnnotations;

namespace DW4.Model
{
    public class Task : BaseModel
    {
        [Required]
        public string CreateByUid { get; set; }

        [Required]
        public string CreatedByName { get; set; }

        [Required]
        public string AssignedToUid { get; set; }

        [Required]
        public string AssignedToName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool Done { get; set; }


        public Task() : base()
        {
            Done = false;

        }
    }
}
