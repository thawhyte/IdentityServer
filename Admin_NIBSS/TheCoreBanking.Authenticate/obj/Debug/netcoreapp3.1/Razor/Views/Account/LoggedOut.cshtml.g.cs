#pragma checksum "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2d5b5b7e685ea78cffb914afdd0c2f950f689838"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_LoggedOut), @"mvc.1.0.view", @"/Views/Account/LoggedOut.cshtml")]
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
#line 1 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\_ViewImports.cshtml"
using TheCoreBanking.Authenticate;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\_ViewImports.cshtml"
using TheCoreBanking.Authenticate.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d5b5b7e685ea78cffb914afdd0c2f950f689838", @"/Views/Account/LoggedOut.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f9401418b945d245799e2cf4f749cd2ff6d35f3", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_LoggedOut : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LoggedOutViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/fintraklogo.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/signout-redirect.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 2 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
  
    // set this so the layout rendering sees an anonymous user
    ViewData["signed-out"] = true;

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"col-md-4 ml-auto mr-auto\">\n\n    <div class=\"card card-login\">\n\n        <div class=\"card-header \">\n            <div class=\"logo-container\">\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2d5b5b7e685ea78cffb914afdd0c2f950f6898384529", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </div>\n        </div>\n\n        <div class=\"card-body text-center\">\n            <h4 class=\"card-title\">\n                Logout <br />\n                <small>You are now logged out</small>\n            </h4>\n");
#nullable restore
#line 22 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
             if (Model.PostLogoutRedirectUri != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div>\n                    Click the button below to return to <span>");
#nullable restore
#line 25 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
                                                         Write(Model.ClientName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>  the\n                    application.\n                </div>\n");
#nullable restore
#line 28 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
             if (Model.SignOutIframeUrl != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <iframe width=\"0\" height=\"0\" class=\"signout\"");
            BeginWriteAttribute("src", " src=\"", 925, "\"", 954, 1);
#nullable restore
#line 31 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
WriteAttributeValue("", 931, Model.SignOutIframeUrl, 931, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></iframe>\n");
#nullable restore
#line 32 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\n        <div class=\"card-footer \">\n");
#nullable restore
#line 35 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
             if (Model.PostLogoutRedirectUri != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a class=\"PostLogoutRedirectUri btn btn-primary  btn-lg btn-block\"");
            BeginWriteAttribute("href", " href=\"", 1180, "\"", 1215, 1);
#nullable restore
#line 37 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
WriteAttributeValue("", 1187, Model.PostLogoutRedirectUri, 1187, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Return</a>\n");
#nullable restore
#line 38 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\n\n    </div>\n\n</div>\n\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\n");
#nullable restore
#line 47 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
     if (Model.AutomaticRedirectAfterSignOut)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2d5b5b7e685ea78cffb914afdd0c2f950f6898389498", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
#nullable restore
#line 50 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Account\LoggedOut.cshtml"
    }

#line default
#line hidden
#nullable disable
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LoggedOutViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
