#pragma checksum "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Home\Handmade_Opportunities.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "656af4b51b044cf6cc921aee75e435ddf51f3abe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Handmade_Opportunities), @"mvc.1.0.view", @"/Views/Home/Handmade_Opportunities.cshtml")]
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
#line 1 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\_ViewImports.cshtml"
using URC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\_ViewImports.cshtml"
using URC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"656af4b51b044cf6cc921aee75e435ddf51f3abe", @"/Views/Home/Handmade_Opportunities.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8d7cfd8f071e8df9f09366146bd9f323258bc8a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Handmade_Opportunities : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/opportunities.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<!--
  Author:    Kevin Tran
  Partner:   Calvin Tu
  Date:      08/31/2020
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Kevin Tran and Calvin Tu - This work may not be copied for use in Academic Coursework.

  We, Kevin Tran, Calvin Tu, certify that we wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents
  HTML for student's view of the listing of opportunities
-->

");
#nullable restore
#line 15 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Home\Handmade_Opportunities.cshtml"
  
    ViewData["Title"] = "Handmade Opportunities";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            DefineSection("Styles", async() => {
                WriteLiteral("\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "656af4b51b044cf6cc921aee75e435ddf51f3abe5092", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
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
            WriteLiteral(@"
<br />

<div id=""opportunities"" class=""container"">
    <div id=""opportunity-filter"">
        <input type=""search"" placeholder=""Search for opportunities..."" />
        <span class=""nav-item dropdown"">
            <a class=""nav-link dropdown-toggle"" href=""#"" id=""navbarDepartment"" role=""button"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                Departments
            </a>
            <div class=""dropdown-menu"" aria-labelledby=""navbarDropdown"">
                <a class=""dropdown-item"" href=""#"">Software Development</a>
                <a class=""dropdown-item"" href=""#"">Machine Learning</a>
                <a class=""dropdown-item"" href=""#"">AI</a>
            </div>
        </span>
        <span class=""nav-item dropdown"">
            <a class=""nav-link dropdown-toggle"" href=""#"" id=""navbarSortBy"" role=""button"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                Sort by...
            </a>
            <div class=""dropdown-menu"" aria-labelledby=""navbarDrop");
            WriteLiteral(@"down"">
                <a class=""dropdown-item"" href=""#"">A-Z</a>
                <a class=""dropdown-item"" href=""#"">Project ID</a>
                <a class=""dropdown-item"" href=""#"">Professor's Researches</a>
            </div>
        </span>
        <span>Ascending</span>
    </div>
    <hr />

    <div class=""opportunity"">
        <div class=""opportunity-header"">
            <img src=""https://static.hudl.com/users/prod/Hudl/5852fa1e9845ca0c70c59d3b/dd0b95debf4047739dfd61ec80797a33.jpg"" alt=""URC project logo"" />
            <div>
                <h2>
                    <a href=""Details"">Undergraduate Research Coordinator (URC)</a>
                </h2>
                <!-- Idea for at a glance comes from Handshake https://utah.joinhandshake.com/postings -->
                <div class=""at-a-glance"">
                    <div>
                        <h3>
                            Application deadline
                        </h3>
                        <p>
                            September, 2020
       ");
            WriteLiteral(@"                 </p>
                    </div>
                    <div>
                        <h3>
                            Posted date
                        </h3>
                        <p>
                            August, 2020
                        </p>
                    </div>
                    <div>
                        <h3>
                            Skills required
                        </h3>
                        <p>
                            CS3505
                        </p>
                    </div>
                    <div>
                        <h3>
                            Professor
                        </h3>
                        <p>
                            H. James de St. Germain
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <p>
            A web application built to connect students to faculty to make finding research work frictionless. Professors can post available research");
            WriteLiteral(@" opportunities along with any required skills, pictures, related tags, etc. Students can quickly find and sort through these opportunities and find the best one they can apply their skills to. There will also be an admin section to allow admins to visualize URC usage as well as other monitoring/maintenance tasks.
        </p>
        <ul class=""opportunity-tags"">
            <li>C#</li>
            <li>ASP.NET Core 3.1</li>
            <li>JavaScript</li>
            <li>School of Computing</li>
        </ul>
    </div>

    <div class=""opportunity"">
        <div class=""opportunity-header"">
            <img src=""https://static.hudl.com/users/prod/Hudl/5852fa1e9845ca0c70c59d3b/dd0b95debf4047739dfd61ec80797a33.jpg"" alt=""URC project logo"" />
            <div>
                <h2>
                    <a href=""Details"">Undergraduate Research Coordinator (URC)</a>
                </h2>
                <!-- Idea for at a glance comes from Handshake https://utah.joinhandshake.com/postings -->
                <div cla");
            WriteLiteral(@"ss=""at-a-glance"">
                    <div>
                        <h3>
                            Application deadline
                        </h3>
                        <p>
                            September, 2020
                        </p>
                    </div>
                    <div>
                        <h3>
                            Posted date
                        </h3>
                        <p>
                            August, 2020
                        </p>
                    </div>
                    <div>
                        <h3>
                            Skills required
                        </h3>
                        <p>
                            CS3505
                        </p>
                    </div>
                    <div>
                        <h3>
                            Professor
                        </h3>
                        <p>
                            H. James de St. Germain
                        </p>
              ");
            WriteLiteral(@"      </div>
                </div>
            </div>
        </div>
        <p>
            A web application built to connect students to faculty to make finding research work frictionless. Professors can post available research opportunities along with any required skills, pictures, related tags, etc. Students can quickly find and sort through these opportunities and find the best one they can apply their skills to. There will also be an admin section to allow admins to visualize URC usage as well as other monitoring/maintenance tasks.
        </p>
        <ul class=""opportunity-tags"">
            <li>C#</li>
            <li>ASP.NET Core 3.1</li>
            <li>JavaScript</li>
            <li>School of Computing</li>
        </ul>
    </div>
</div>




");
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