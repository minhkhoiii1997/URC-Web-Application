#pragma checksum "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c97ff9643e4c8a45caa3020671236754d8649846"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Students_Delete), @"mvc.1.0.view", @"/Views/Students/Delete.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c97ff9643e4c8a45caa3020671236754d8649846", @"/Views/Students/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8d7cfd8f071e8df9f09366146bd9f323258bc8a", @"/Views/_ViewImports.cshtml")]
    public class Views_Students_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<URC.Models.StudentApplication>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            WriteLiteral("\n");
#nullable restore
#line 20 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
  
    ViewData["Title"] = "Delete Application";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>Delete Your Application</h1>\n\n<h3>Are you sure you want to delete your application? This action is irreversible.</h3>\n<div>\n    <dl class=\"row\">\n        <dt class=\"col-sm-4\">\n            Resume\n        </dt>\n        <dd class=\"col-sm-10\">\n");
#nullable restore
#line 33 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
             if (Model.ResumeFilename == "" || Model.ResumeFilename == null)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
           Write(Html.Raw("--"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
                               
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c97ff9643e4c8a45caa3020671236754d86498465806", async() => {
                WriteLiteral("Resume");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1165, "~/uploads/resumes/", 1165, 18, true);
#nullable restore
#line 39 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
AddHtmlAttributeValue("", 1183, Model.ResumeFilename, 1183, 21, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 40 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </dd>\n        <dt class=\"col-sm-4\">\n            Application To\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 46 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
       Write(Model.OpportunityId);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 46 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
                              Write(Model.Opportunity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            Expected Graduation\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 52 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
       Write(Html.DisplayFor(model => model.ExpectedGraduation));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            Degree Plan\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 58 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
       Write(Html.DisplayFor(model => model.DegreePlan));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            GPA\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 64 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
       Write(Html.DisplayFor(model => model.GPA));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            uID\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 70 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
       Write(Html.DisplayFor(model => model.UId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            Availability\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 76 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Availability));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            Personal Statement\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 82 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
       Write(Html.DisplayFor(model => model.PersonalStatement));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            Created (UTC)\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 88 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
        Write(Model.ApplicationDate.ToUniversalTime());

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            Modified (UTC)\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 94 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
        Write(Model.TimeModified.ToUniversalTime());

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-4\">\n            Looking for position?\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 100 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
        Write(Model.IsLookingForPosition ? "Yes" : "No");

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n    </dl>\n\n    <p>\n        <b>Courses: </b>\n");
#nullable restore
#line 106 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
          
            string courses = "";
            foreach (var c in Model.Student.Courses)
            {
                courses += c.Course.Name + ", ";
            }
            if (Model.Student.Courses.Count > 0)
                courses = courses.Substring(0, courses.Length - 2);
        

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
#nullable restore
#line 115 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
    Write(courses);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </p>\n    <p>\n        <b>Interests: </b>\n");
#nullable restore
#line 119 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
          
            string interests = "";
            foreach (var i in Model.Student.Interests)
            {
                interests += i.Interest.Name + ", ";
            }
            if (Model.Student.Interests.Count > 0)
                interests = interests.Substring(0, interests.Length - 2);
        

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
#nullable restore
#line 128 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
    Write(interests);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </p>\n    <p>\n        <b>Skills: </b>\n");
#nullable restore
#line 132 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
          
            string skills = "";

            foreach (var s in Model.Student.Skills)
            {
                skills += s.Skill.Name + ", ";
            }
            if (Model.Student.Skills.Count > 0)
                skills = skills.Substring(0, skills.Length - 2);
        

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
#nullable restore
#line 142 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
    Write(skills);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </p>\n\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c97ff9643e4c8a45caa3020671236754d864984613932", async() => {
                WriteLiteral("\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c97ff9643e4c8a45caa3020671236754d864984614197", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 146 "C:\Users\minhk\Dev\final-project-ktucalvin\URC\Views\Students\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.StudentApplicationId);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" /> |\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c97ff9643e4c8a45caa3020671236754d864984615995", async() => {
                    WriteLiteral("Back to List");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<URC.Models.StudentApplication> Html { get; private set; }
    }
}
#pragma warning restore 1591