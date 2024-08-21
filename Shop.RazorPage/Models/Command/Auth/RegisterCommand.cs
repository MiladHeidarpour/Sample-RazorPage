namespace Shop.RazorPage.Models.Command.Auth;

public class RegisterCommand
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
