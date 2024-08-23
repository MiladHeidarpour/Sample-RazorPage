namespace Shop.RazorPage.Models.Command.Sliders;

public class CreateSliderCommand
{
    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Title { get; set; }
}