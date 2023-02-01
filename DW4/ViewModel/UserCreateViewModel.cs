using System.ComponentModel.DataAnnotations;

namespace DW4.ViewModel;

public class UserCreateViewModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Password { get; set; }
}