#pragma checksum "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Shared\_navbar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4f0b266e00cdd17e67fa4c801aeae5b4223658c5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__navbar), @"mvc.1.0.view", @"/Views/Shared/_navbar.cshtml")]
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
#line 2 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\_ViewImports.cshtml"
using core.entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\_ViewImports.cshtml"
using core.webui.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\_ViewImports.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\_ViewImports.cshtml"
using core.webui.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\_ViewImports.cshtml"
using core.webui.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f0b266e00cdd17e67fa4c801aeae5b4223658c5", @"/Views/Shared/_navbar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae08336554d044db44289d48573bc572ba753d78", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__navbar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"  <div class=""navbar bg-danger navbar-dark navbar-expand-sm"">
        <div class=""container"">
            <a href=""/"" class=""navbar-brand"">CoreApp</a>
            <ul class=""navbar-nav mr-auto"">
                <li class=""nav-item"">
                    <a href=""/products"" class=""nav-link"">Products</a>
                </li>

");
#nullable restore
#line 9 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Shared\_navbar.cshtml"
                 if(User.Identity.IsAuthenticated)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <li class=""nav-item"">
                        <a href=""/cart"" class=""nav-link"">Cart</a>
                    </li>
                    <li class=""nav-item"">
                        <a href=""/orders"" class=""nav-link"">Orders</a>
                    </li>
");
#nullable restore
#line 17 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Shared\_navbar.cshtml"

                    if(User.IsInRole("admin"))
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                         <li class=""nav-item"">
                        <a href=""/admin/products"" class=""nav-link"">Admin Products</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""/admin/categories"" class=""nav-link"">Admin Categories</a>
                        </li>
                        <li class=""nav-item"">
                            <a href=""/admin/role/list"" class=""nav-link"">Roles</a>
                        </li>
                         <li class=""nav-item"">
                            <a href=""/admin/user/list"" class=""nav-link"">Users</a>
                        </li>
");
#nullable restore
#line 32 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Shared\_navbar.cshtml"
                    }
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </ul>\r\n\r\n             <ul class=\"navbar-nav ml-auto\">\r\n\r\n");
#nullable restore
#line 38 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Shared\_navbar.cshtml"
                 if(User.Identity.IsAuthenticated)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <li class=\"nav-item\">\r\n                        <a href=\"/account/manage\" class=\"nav-link\">\r\n                            ");
#nullable restore
#line 42 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Shared\_navbar.cshtml"
                       Write(User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </a>\r\n                    </li>\r\n                    <li class=\"nav-item\">\r\n                        <a href=\"/account/logout\" class=\"nav-link\">Logout</a>\r\n                    </li>\r\n");
#nullable restore
#line 48 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Shared\_navbar.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <li class=""nav-item"">
                        <a href=""/account/login"" class=""nav-link"">Login</a>
                    </li>
                    <li class=""nav-item"">
                        <a href=""/account/register"" class=""nav-link"">Register</a>
                    </li>
");
#nullable restore
#line 57 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Shared\_navbar.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </ul>\r\n\r\n\r\n        </div>\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
