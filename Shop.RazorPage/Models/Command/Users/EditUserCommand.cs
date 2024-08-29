using Shop.RazorPage.Models.Response.Users;

namespace Shop.RazorPage.Models.Command.Users;

public class EditUserCommand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public IFormFile? Avatar { get; set; }
    public Gender Gender { get; set; }
}