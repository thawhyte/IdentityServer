#pragma checksum "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Administration\StartAndEndDay.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "de2e2a6a42ceb92b9a39969c5b44de5597b811e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Administration_StartAndEndDay), @"mvc.1.0.view", @"/Views/Administration/StartAndEndDay.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"de2e2a6a42ceb92b9a39969c5b44de5597b811e9", @"/Views/Administration/StartAndEndDay.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f9401418b945d245799e2cf4f749cd2ff6d35f3", @"/Views/_ViewImports.cshtml")]
    public class Views_Administration_StartAndEndDay : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route", "ManageSOD", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmAssignSOD"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route", "ManageEOD", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmAssignEOD"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route", "ManageEOY", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmAssignEOY"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/StartAndEndDay.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Administration\StartAndEndDay.cshtml"
  
    ViewData["Title"] = "Start And End Of Day";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""card  card-subcategories"">

    <div class=""card-header "">
        <h4 class=""card-title text-center""> Manage Start And End Of Day</h4>
        <br />
    </div>

    <div class=""card-body "">

        <!--
           color-classes: ""nav-pills-primary"", ""nav-pills-info"", ""nav-pills-success"", ""nav-pills-warning"",""nav-pills-danger""
        -->
        <ul class=""nav nav-pills nav-pills-danger nav-pills-icons justify-content-center"" role=""tablist"">
            <li class=""nav-item"">
                <a class=""nav-link active"" data-toggle=""tab"" href=""#AssignSOD"" role=""tablist"">
                    <i class=""now-ui-icons objects_umbrella-13""></i>
                    Start Day
                </a>
            </li>
            <li class=""nav-item"">
                <a class=""nav-link"" data-toggle=""tab"" href=""#AssignEOD"" role=""tablist"">
                    <i class=""fa fa-address-card""></i>
                    End Day
                </a>
            </li>
            <li class=""nav-item"">
                <");
            WriteLiteral(@"a class=""nav-link"" data-toggle=""tab"" href=""#AssignEOY"" role=""tablist"">
                    <i class=""fa fa-address-card""></i>
                    End Year
                </a>
            </li>
        </ul>
        <div class=""tab-content tab-space tab-subcategories"">
            <div class=""tab-pane active"" id=""AssignSOD"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "de2e2a6a42ceb92b9a39969c5b44de5597b811e97731", async() => {
                WriteLiteral("\n                    ");
#nullable restore
#line 41 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Administration\StartAndEndDay.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    <div class=\"form-group\">\n                        <div class=\"col-md-12\">\n                            ");
#nullable restore
#line 44 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Administration\StartAndEndDay.cshtml"
                       Write(ViewBag.CurrentDate);

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                            <button class=\"btn btn-info col-md-12\" id=\"btnAssignSOD\" type=\"button\" value=\"Start the Day\">\n");
                WriteLiteral(" <i class=\"fas fa-plus\"></i>\n                                Start the Day\n");
                WriteLiteral(" <i class=\"fas fa-plus\"></i>\n                            </button>\n                        </div>\n                    </div>\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Route = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </div>\n            <div class=\"tab-pane\" id=\"AssignEOD\">\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "de2e2a6a42ceb92b9a39969c5b44de5597b811e910533", async() => {
                WriteLiteral("\n                    ");
#nullable restore
#line 56 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Administration\StartAndEndDay.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    <div class=\"form-group\">\n                        <div class=\"col-md-12\">\n                            ");
#nullable restore
#line 59 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Administration\StartAndEndDay.cshtml"
                       Write(ViewBag.CurrentDate);

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                            <button class=\"btn btn-info col-md-12\" id=\"btnAssignEOD\" type=\"button\" value=\"End the Day\">\n");
                WriteLiteral(" <i class=\"fas fa-plus\"></i>\n                                End the Day\n");
                WriteLiteral(" <i class=\"fas fa-plus\"></i>\n                            </button>\n                        </div>\n");
                WriteLiteral("                    </div>\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Route = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </div>\n            <div class=\"tab-pane\" id=\"AssignEOY\">\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "de2e2a6a42ceb92b9a39969c5b44de5597b811e913367", async() => {
                WriteLiteral("\n                    ");
#nullable restore
#line 72 "C:\Workspace\ERP\ERP NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\Admin_NIBSS\TheCoreBanking.Authenticate\Views\Administration\StartAndEndDay.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                    <div class=\"form-group\">\n                        <div class=\"col-md-12\">\n\n                            <button class=\"btn btn-info col-md-12\" id=\"btnAssignEOY\" type=\"button\" value=\"End the Year\">\n");
                WriteLiteral(" <i class=\"fas fa-plus\"></i>\n                                End the Year\n");
                WriteLiteral(@" <i class=""fas fa-plus""></i>
                            </button>

                        </div>

                    </div>
                    <div class=""form-group"">
                        <div class=""col-md-offset-2 col-md-8"">
                            <input class=""form-control datetimepicker"" id=""EndYearDate"" />
                        </div>

                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Route = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n            </div>\n        </div>\n    </div>\n</div>\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "de2e2a6a42ceb92b9a39969c5b44de5597b811e916154", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591