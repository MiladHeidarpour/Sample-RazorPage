using Shop.RazorPage.Models.Response.Users;

namespace Shop.RazorPage.Models.Command.Users;

public class CreateUserCommand
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
}