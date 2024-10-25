using CookieManager;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Response.Orders;
using Shop.RazorPage.Services.Products;
using Shop.RazorPage.Services.Sellers;
using StackExchange.Redis;

namespace Shop.RazorPage.Infrastructure.CookieUtils;

public class ShopCardCookieManger
{
    private readonly ICookieManager _cookieManager;
    private readonly ISellerService _sellerService;
    private readonly IProductService _productService;

    private const string CookieShopCardName = "shop-card";
    public ShopCardCookieManger(ICookieManager cookieManager, ISellerService sellerService, IProductService productService)
    {
        _cookieManager = cookieManager;
        _sellerService = sellerService;
        _productService = productService;
    }

    public OrderDto? GetShopCard()
    {
        return _cookieManager.Get<OrderDto>(CookieShopCardName);
    }

    public async Task<ApiResult> AddItem(long inventoryId, int count)
    {
        var shopcard = GetShopCard();
        var inventory = await _sellerService.GetInventoryById(inventoryId);
        if (inventory == null)
        {
            return ApiResult.Error();
        }

        var product = await _productService.GetProductById(inventory.ProductId);
        if (shopcard == null)
        {
            var order = new OrderDto()
            {
                Address = null,
                CreationDate = DateTime.Now,
                Discount = null,
                Id = 1,
                UserId = 1,
                UserFullName = "",
                Status = OrderStatus.Finally,
                Items = new List<OrderItemDto>()
                {
                    new OrderItemDto()
                    {
                        Price = inventory.Price,
                        Count = count,
                        ProductImageName = inventory.ProductImage,
                        ShopName = inventory.ShopName,
                        CreationDate = DateTime.Now,
                        ProductTitle = inventory.ProductTitle,
                        InventoryId = inventoryId,
                        OrderId = 1,
                        Id = GenerateId(),
                        ProductSlug = product!.Slug
                    }
                }
            };
            SetCookie(order);
            return ApiResult.Success();
        }
        else
        {
            if (shopcard.Items.Any(f => f.InventoryId == inventoryId))
            {
                var item = shopcard.Items.First(f => f.InventoryId == inventoryId);
                if (inventory.Count >= item.Count + count)
                {
                    item.Count += count;
                }
                else
                {
                    return ApiResult.Error("تعداد موجودی فروشنده کمتر از تعداد درخواستی است");
                }
            }
            else
            {
                var newItem = new OrderItemDto()
                {
                    Price = inventory.Price,
                    Count = count,
                    ProductImageName = inventory.ProductImage,
                    ShopName = inventory.ShopName,
                    CreationDate = DateTime.Now,
                    ProductTitle = inventory.ProductTitle,
                    InventoryId = inventoryId,
                    OrderId = 1,
                    Id = GenerateId(),
                    ProductSlug = product!.Slug
                };
                shopcard.Items.Add(newItem);
            }
            SetCookie(shopcard);
            return ApiResult.Success();
        }
    }

    public void DeleteOrderItem(long itemId)
    {
        var shopCard = GetShopCard();
        if (shopCard == null)
        {
            return;
        }

        var item = shopCard.Items.FirstOrDefault(f => f.Id == itemId);
        if (item == null)
        {
            return;
        }

        shopCard.Items.Remove(item);
        SetCookie(shopCard);
    }

    public void Increase(long itemId)
    {
        var shopCard = GetShopCard();
        if (shopCard == null)
        {
            return;
        }

        var item = shopCard.Items.FirstOrDefault(f => f.Id == itemId);
        if (item == null)
        {
            return;
        }

        item.Count += 1;
        SetCookie(shopCard);
    }

    public void Decrease(long itemId)
    {
        var shopCard = GetShopCard();
        if (shopCard == null)
        {
            return;
        }

        var item = shopCard.Items.FirstOrDefault(f => f.Id == itemId);
        if (item == null || item.Count == 1)
        {
            return;
        }

        item.Count -= 1;
        SetCookie(shopCard);

    }

    public void DeleteShopCard()
    {
        _cookieManager.Remove(CookieShopCardName);
    }
    private void SetCookie(OrderDto order)
    {
        _cookieManager.Set(CookieShopCardName, order, new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(7),
            Secure = true,
        });
    }
    private long GenerateId()
    {
        var random = new Random();
        var number = random.Next(0, 10000) * 6 ^ 2 + random.Next(6, 1000000);
        return number;
    }
}