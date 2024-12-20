﻿using Shop.RazorPage.Models.Command.Products;
using Shop.RazorPage.Models.Response.Products;
using Shop.RazorPage.Models;
using System.Text;
using Newtonsoft.Json;

namespace Shop.RazorPage.Services.Products;

public class ProductService : IProductService
{
    private readonly HttpClient _client;
    private const string ModuleName = "Product";
    public ProductService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> CreateProduct(CreateProductCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Slug), "Slug");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.Description), "Description");
        formData.Add(new StringContent(command.CategoryId.ToString()), "CategoryId");
        formData.Add(new StringContent(command.SubCategoryId.ToString()), "SubCategoryId");
        if (command.SecondarySubCategoryId != null)
            formData.Add(new StringContent(command.SecondarySubCategoryId.ToString() ?? string.Empty), "SecondarySubCategoryId");
        formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
        formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");
        formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
        formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription");
        formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");
        formData.Add(new StringContent(command.SeoData.Schema), "SeoData.Schema");

        var specifications = JsonConvert.SerializeObject(command.Specifications);
        formData.Add(new StringContent(specifications, Encoding.UTF8, "application/json"), "Specifications");


        var result = await _client.PostAsync(ModuleName, formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditProduct(EditProductCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Slug), "Slug");
        formData.Add(new StringContent(command.ProductId.ToString()), "ProductId");
        if (command.ImageFile != null)
            formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.Description), "Description");
        formData.Add(new StringContent(command.CategoryId.ToString()), "CategoryId");
        formData.Add(new StringContent(command.SubCategoryId.ToString()), "SubCategoryId");
        formData.Add(new StringContent(command.SecondarySubCategoryId.ToString() ?? string.Empty), "SecondarySubCategoryId");
        formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
        formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");
        formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
        formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription");
        formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");
        formData.Add(new StringContent(command.SeoData.Schema), "SeoData.Schema");

        var specifications = JsonConvert.SerializeObject(command.Specifications);
        formData.Add(new StringContent(specifications, Encoding.UTF8, "application/json"), "Specifications");

        var result = await _client.PutAsync(ModuleName, formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> AddImage(AddProductImageCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.Sequence.ToString()), "Sequence");
        formData.Add(new StringContent(command.ProductId.ToString()), "ProductId");

        var result = await _client.PostAsync($"{ModuleName}/Images/Add", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteProductImage(DeleteProductImageCommand command)
    {
        var json = JsonConvert.SerializeObject(command);
        var message = new HttpRequestMessage(HttpMethod.Delete, $"{ModuleName}/images/Remove")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var result = await _client.SendAsync(message);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ProductDto?> GetProductById(long productId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<ProductDto?>>($"{ModuleName}/{productId}");
        return result?.Data;
    }

    public async Task<ProductDto?> GetProductBySlug(string slug)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<ProductDto?>>($"{ModuleName}/bySlug/{slug}");
        return result?.Data;
    }

    public async Task<SingleProductDto?> GetSingleProductBySlug(string slug)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<SingleProductDto?>>($"{ModuleName}/single/{slug}");
        return result?.Data;
    }

    public async Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams)
    {
        var url = $"{ModuleName}?pageId={filterParams.PageId}&take={filterParams.Take}" +
            $"&slug={filterParams.Slug}&title={filterParams.Title}";
        if (filterParams.Id != null)
            url += $"&Id={filterParams.Id}";
        var result = await _client.GetFromJsonAsync<ApiResult<ProductFilterResult>>(url);
        return result?.Data;
    }

    public async Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams)
    {
        var url = $"{ModuleName}/shop?pageId={filterParams.PageId}&take={filterParams.Take}" +
                  $"&categorySlug={filterParams.CategorySlug}&onlyAvailableProducts={filterParams.OnlyAvailableProducts}" +
                  $"&search={filterParams.Search}&SearchOrderBy={filterParams.SearchOrderBy}";
        if (filterParams.JustHasDiscount!=null)
        {
            url += $"&JustHasDiscount={filterParams.JustHasDiscount}";
        }

        var result = await _client.GetFromJsonAsync<ApiResult<ProductShopResult>>(url);
        return result?.Data;
    }
}
