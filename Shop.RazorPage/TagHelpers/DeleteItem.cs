﻿using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shop.RazorPage.TagHelpers;

public class DeleteItem : TagHelper
{
    public string Url { get; set; }
    public string Description { get; set; }
    public string Class { get; set; } = "btn btn-danger btn-sm";
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("onClick", $"DeleteItem('{Url}','{Description}')");


        //output.AddClass("btn",HtmlEncoder.Default);
        //output.AddClass("btn-danger",HtmlEncoder.Default);
        //output.AddClass("btn-danger",HtmlEncoder.Default);

        //output.Attributes.Add("Class", "btn btn-danger btn-sm");

        output.Attributes.Add("class", Class);

        base.Process(context, output);
    }
}