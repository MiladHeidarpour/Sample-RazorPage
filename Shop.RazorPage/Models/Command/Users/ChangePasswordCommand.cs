
namespace Shop.RazorPage.Models.Command.Users;

public class ChangePasswordCommand
{
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}