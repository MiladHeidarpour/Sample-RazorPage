namespace Shop.RazorPage.Models.Response.Categories;

public class CategoryDto:BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public List<ChildCategoryDto> Childs { get; set; }
}
