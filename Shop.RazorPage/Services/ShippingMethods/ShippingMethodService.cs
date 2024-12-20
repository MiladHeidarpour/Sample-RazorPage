﻿using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Response.ShippingMethods;

namespace Shop.RazorPage.Services.ShippingMethods;

public class ShippingMethodService : IShippingMethodService
{
    private readonly HttpClient _client;
    private const string ModuleName = "ShippingMethod";
    public ShippingMethodService(HttpClient client)
    {
        _client = client;
    }
    public async Task<List<ShippingMethodDto>> GetShippingMethods()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<ShippingMethodDto>>>($"{ModuleName}");
        return result?.Data ?? new();
    }
}