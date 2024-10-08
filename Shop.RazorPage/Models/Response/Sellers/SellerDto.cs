﻿namespace Shop.RazorPage.Models.Response.Sellers;

public class SellerDto : BaseDto
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
    public SellerStatus Status { get; set; }
}

public class SellerFilterResult : BaseFilter<SellerDto, SellerFilterParams>
{

}

public class SellerFilterParams : BaseFilterParam
{
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
}