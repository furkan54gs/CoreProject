#pragma checksum "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Product\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef404a8513e5d1932bb4966df6ad1b8cad9d3c20"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Details), @"mvc.1.0.view", @"/Views/Product/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef404a8513e5d1932bb4966df6ad1b8cad9d3c20", @"/Views/Product/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae08336554d044db44289d48573bc572ba753d78", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Product>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            DefineSection("Categories", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Product\Details.cshtml"
Write(await Component.InvokeAsync("Categories"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n<div class=\"card mb-3\">\r\n    <div class=\"card-body\">\r\n        <div class=\"row no-gutters\">\r\n            <div class=\"col-md-3\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ef404a8513e5d1932bb4966df6ad1b8cad9d3c204705", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 245, "~/img/", 245, 6, true);
#nullable restore
#line 12 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Product\Details.cshtml"
AddHtmlAttributeValue("", 251, Model.ImageUrl, 251, 15, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n            <div class=\"col-md-9\">\r\n\r\n                <h5 class=\"card-title\">");
#nullable restore
#line 16 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Product\Details.cshtml"
                                  Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <p class=\"card-text\">");
#nullable restore
#line 17 "C:\Users\mfurk\Code\CoreProject\core.webui\Views\Product\Details.cshtml"
                                Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p class=\"card-text\">\r\n                    <small class=\"text-muted\">3 days ago</small>\r\n                </p>\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Product> Html { get; private set; }
    }
}
#pragma warning restore 1591
