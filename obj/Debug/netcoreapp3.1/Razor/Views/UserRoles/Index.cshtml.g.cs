#pragma checksum "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5b92a1de2bf855146228c2fe587788be715c9b79"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserRoles_Index), @"mvc.1.0.view", @"/Views/UserRoles/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\_ViewImports.cshtml"
using BPUIO_OneForEachOther;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\_ViewImports.cshtml"
using BPUIO_OneForEachOther.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5b92a1de2bf855146228c2fe587788be715c9b79", @"/Views/UserRoles/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2c13fea2aff528e21ddf84d16efdf6589f9f829e", @"/Views/_ViewImports.cshtml")]
    public class Views_UserRoles_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BPUIO_OneForEachOther.Models.UserRole>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>User roles</h1>\r\n\r\n<p>\r\n    <!--a class=\"btn btn-primary\" asp-action=\"Create\">Create</a-->\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Role));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.User));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <!--th>Actions</th-->\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 28 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 31 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Role.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 34 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.User.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 34 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
                                                              Write(Html.DisplayFor(modelItem => item.User.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <!--td>\r\n                <a class=\"btn btn-outline-primary\" asp-action=\"Edit\" asp-route-id=\"");
#nullable restore
#line 40 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
                                                                              Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Edit</a>\r\n                <a class=\"btn btn-outline-info\" asp-action=\"Details\" asp-route-id=\"");
#nullable restore
#line 41 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
                                                                              Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Details</a>\r\n                <a class=\"btn btn-outline-danger\" asp-action=\"Delete\" asp-route-id=\"");
#nullable restore
#line 42 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
                                                                               Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">Delete</a>\r\n            </td-->\r\n        </tr>\r\n");
#nullable restore
#line 45 "C:\Users\goranz\source\repos\BPUIO-OneForEachOther\BPUIO-OneForEachOther\Views\UserRoles\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BPUIO_OneForEachOther.Models.UserRole>> Html { get; private set; }
    }
}
#pragma warning restore 1591
