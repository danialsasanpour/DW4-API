using System.ComponentModel.DataAnnotations;

namespace DW4.Model
{
    public class BaseModel
    {
        [Key]
        public string Id { get; set; }
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
