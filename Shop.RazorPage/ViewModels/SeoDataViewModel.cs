using System.ComponentModel.DataAnnotations;
using Shop.RazorPage.Models;

namespace Shop.RazorPage.ViewModels;

public class SeoDataViewModel
{
    [Display(Name = "عنوان صفحه")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string MetaTitle { get; set; }


    [DataType(DataType.MultilineText)]
    public string? MetaDescription { get; set; }


    public string? MetaKeyWords { get; set; }


    public bool IndexPage { get; set; }


    [DataType(DataType.Url)]
    public string? Canonical { get; set; }


    [DataType(DataType.MultilineText)]
    public string? Schema { get; set; }


    public SeoData MapSeoData()
    {
        return new SeoData()
        {
            MetaTitle = MetaTitle,
            MetaDescription = MetaDescription,
            MetaKeyWords = MetaKeyWords,
            IndexPage = IndexPage,
            Canonical = Canonical,
            Schema = Schema,
        };
    }

    public static SeoDataViewModel MapSeoDataViewModel(SeoData seoData)
    {
        return new SeoDataViewModel()
        {
            MetaTitle = seoData.MetaTitle,
            MetaDescription = seoData.MetaDescription,
            MetaKeyWords = seoData.MetaKeyWords,
            IndexPage = seoData.IndexPage,
            Canonical = seoData.Canonical,
            Schema = seoData.Schema,
        };
    }
}