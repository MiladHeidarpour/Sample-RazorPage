namespace Shop.RazorPage.Models.Command.Sliders;

public class EditSliderCommand
{
    public long Id { get; set; }
    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Title { get; set; }
}