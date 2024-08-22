namespace Shop.RazorPage.Models.Command.Categories;

public class AddChildCategoryCommand
{
    public long ParentId { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public SeoData SeoData { get; set; }
}