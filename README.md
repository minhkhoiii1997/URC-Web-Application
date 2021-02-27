# Undergraduate Research Coordinator (URC)

```
Team Name:     [URCC] - Undergraduate Research Coordinator Coordinators
Team members:  Calvin Tu, Khoi Tran, Ping Cheng Chung
Date:          November, 2020
Course:        CS 4540, University of Utah, School of Computing
Copyright:     CS 4540 and Calvin Tu, Khoi Minh Tran, Ping Cheng Chung - This work may not be copied for use in Academic Coursework.

Deployed URL:  Calvin: http://ec2-75-101-242-168.compute-1.amazonaws.com/
               Khoi: http://ec2-52-200-31-207.compute-1.amazonaws.com/
               Ping: http://ec2-34-204-51-81.compute-1.amazonaws.com/
Github Page:   https://github.com/uofu-cs4540-fall2020/final-project-ktucalvin
```

## Comments to Evaluators:

Please see final_report.pdf

### PS6
* Resume upload works correctly. Resume is viewable online if pdf. Uploads are restricted to only .pdf, .doc, and .docx
file types.
* File upload names are random GUIDs instead of trusting the user
* Many-to-Many tables are implemented for courses, interests, and skills
* Student search has search-as-you-type functionality (may be slow on ec2)
* Student search results have random profile pictures, but the data is still the same
* Spinners on image and resume upload

### Older assignments
* Search, sort, and filter on Opportunities page are currently non-functional placeholders
* Image upload is under Opportunities/Edit

## Assignment Specific Write-up:

### (PS6) Data Validation
1. GPA must be 0 - 4.0
2. Degree plan can be up to 10 characters
3. uID must match regex
4. Availability must be 0 - 168 hours (because there are 168 hours in a week, even though ~80 would be normal max, 168
gives more flexibility in assignments)
5. Personal statement must be 10 - 1000 characters

### (PS5) Authentication/Authorization Observations
Authentication is ensuring users only see what they are allowed to whereas authorization is verifying who they claim to
be. We made the "concise opportunities" view show professors only the opportunities which they own and so they do not
see the create or delete options. Admins can bypass this restriction. Perhaps our best feature is that when creating a
new opportunity, a dropdown menu shows all available professors and allows an administrator to create opportunities only
for existing URC professors.

### (PS5) Above and Beyond
When creating an opportunity, there is a dropdown to show administrators all registered URC professors to make assigning
a research opportunity to a professor much easier. The admin role management dashboard uses Sweet Alerts to improve the
UI design.

### (PS5) Improvements
We cleaned up various text and styling issues and other miscellaneous technical debt. We created a separate HTML layout
for the home page since the styles on that page are full viewport-width whereas every other page is centered within a
div.

### PS4 Writeup
We did implement skills and tags as a many-to-many relations. Skills is implemented specially, it has *two* many-to-many
relationships between the Skills and Opportunities tables. This allows opportunities to distinguish between required and
preferred skills without requiring two separate skills tables that would need to be synced.

Image upload was implemented, it can be found under Opportunities/Edit but not Opportunities/Create.

### (PS2) Bootstrap Discussion
Bootstrap has helped us implement some basic interactive elements easily, and without JavaScript. Most prominently are
the various dropdowns and the "Featured Research" image carousel on the main page. Its formatting and normalization CSS
rules have made positioning things more consistent, including across browsers. Since we had prior experience with
flexbox and grid however, custom CSS was as effective for layout and styles. Bootstrap helped the most for interactive
elements that would otherwise require JavaScript.

*Components used*
* Carousel
* Dropdown
* .container, .container-fluid
* .d-flex, .align-items-\*, .justify-content-\*
* Various form elements (.form-group)
* Navbar
* .btn, .btn-primary, .btn-outline-primary

Our user interface makes the site more usable by considering user flows; what a user would do when the land on the site. First they are greeted by an attention-grabbing hero and are provided only two choices: log in if they know that they are going to look for research, or jump to featured research projects and testimonials if they are still unsure of the site. The opportunities and details page are designed to make information easily skimmable (e.g. the tags on each opportunity) to help users quickly find an opportunity they are interested in.

### (PS1) UI/UX Choices
We have decided to create separate views of the site for students, professors, and admins since they have different
concerns which, when mixed on a single page, would create friction for each user class. As such, the Opportunities page
does not have any area to create a listing; instead we have (currently nonfunctioning) sort and filter controls that
students can use to quickly find positions that they want. We plan to eventually have a login page that will redirect a
user to the appropriate page. We modeled the site after Handshake and LinkedIn, since those sites serve a similar
purpose of connecting people with employers.

## Peers Helped
N/A

## Peers Consulted
N/A

## Acknowledgements
* Bootstrap
* ASP.NET Core 3.1
* JSFiddle
* Stylelint

## References
1. System Font Stack - Geoff Graham - https://css-tricks.com/snippets/css/system-font-stack/
2. LinkedIn - https://www.linkedin.com/
3. Handshake - https://utah.joinhandshake.com/
4. "Stop chrome from auto styling input type=search" - https://stackoverflow.com/questions/11538192/stop-chrome-from-auto-styling-input-type-search
5. "How to Write a Perfect Website Hero Message" - https://www.trajectorywebdesign.com/blog/website-hero-message/
6. btwnexhibits - https://www.btwnexhibits.com/
7. background - https://developer.mozilla.org/en-US/docs/Web/CSS/background#%3Cbg-size%3E
8. filter - https://developer.mozilla.org/en-US/docs/Web/CSS/filter
9. Unicode Character 'RIGHT-POINTING DOUBLE ANGLE QUOTATION MARK' (U+00BB) - http://www.fileformat.info/info/unicode/char/bb/index.htm
10. Bootstrap Documentation (various pages) - https://getbootstrap.com/docs/4.0/getting-started/introduction/
11. select - https://developer.mozilla.org/en-US/docs/Web/HTML/Element/select
12. Forms - https://getbootstrap.com/docs/4.5/components/forms/
13. How TO - Animated Search Form - https://www.w3schools.com/howto/howto_css_animated_search.asp
14. textarea - https://developer.mozilla.org/en-US/docs/Web/HTML/Element/textarea
15. Creating a help icon using CSS - https://stackoverflow.com/questions/29102646/creating-a-help-icon-using-css/29104375
16. Tooltips - https://getbootstrap.com/docs/4.5/components/tooltips/
17. What is the purpose of the `name` attribute in a checkbox input element? - https://stackoverflow.com/questions/3626883/what-is-the-purpose-of-the-name-attribute-in-a-checkbox-input-element
18. Razor Pages with Entity Framework Core in ASP.NET Core - Tutorial 1 of 8 - https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?tabs=visual-studio&view=aspnetcore-3.1#startupcs-1
19. Relationships (Many-to-Many) - https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#other-relationship-patterns
20. How to specify a composite primary key using EFCore Code First Migrations - https://stackoverflow.com/questions/51993903/how-to-specify-a-composite-primary-key-using-efcore-code-first-migrations
21. Entity Properties - https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt
22. System.ComponentModel.DataAnnotations Namespace - https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1
23. Example of array.map() in C#? - https://stackoverflow.com/questions/32959468/example-of-array-map-in-c
24. String.Join Method - https://docs.microsoft.com/en-us/dotnet/api/system.string.join?view=netcore-3.1
25. [SOLVED] - Display only Date in @Html.DisplayFor() in MVC - https://entityframework.net/knowledge-base/26464319/display-only-date-in--html-displayfor---in-mvc
26. GUID - how to generate GUID in C# - https://stackoverflow.com/questions/8477664/how-can-i-generate-uuid-in-c-sharp
27. How to use data attributes - https://developer.mozilla.org/en-US/docs/Learn/HTML/Howto/Use_data_attributes
28. Configure ASP.NET Core Identity - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-3.1
29. Seed Users & Roles in ASP.NET Core with Entity Framework (EF) Core - https://deanilvincent.github.io/2019/11/22/seed-users-and-roles-in-asp-net-core-with-entity-framework-core/
30. ASP.NET Core 3.x Authorize attribute not working - https://forums.asp.net/t/2162033.aspx?ASP+NET+Core+3+x+Authorize+attribute+not+working
31. How do I specify different Layouts in the ASP.NET MVC 3 razor ViewStart file? - https://stackoverflow.com/questions/5161380/how-do-i-specify-different-layouts-in-the-asp-net-mvc-3-razor-viewstart-file/5161384
32. Data Tables Full Getting Started Guide - https://datatables.net/manual/installation
33. How to redraw DataTable with new data - https://stackoverflow.com/questions/25929347/how-to-redraw-datatable-with-new-data/25929434
34. Get current user - https://stackoverflow.com/questions/38751616/asp-net-core-identity-get-current-user
35. Escaping Razor View @ Symbol in MVC project - http://www.technicaloverload.com/escaping-razor-view-symbol-in-mvc-project/
36. Sweet Alerts - https://sweetalert.js.org/guides/
37. How generate a unique file name at upload files in webserver (MVC) - https://stackoverflow.com/questions/24625078/how-generate-a-unique-file-name-at-upload-files-in-webserver-mvc
38. Redirect to Action in Another Controller - https://stackoverflow.com/questions/10785245/redirect-to-action-in-another-controller
39. Relationships - https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#required-and-optional-relationships
40. Cards - https://getbootstrap.com/docs/4.0/components/card/
41. Configuring One To Many Relationships in Entity Framework Core - https://www.learnentityframeworkcore.com/configuration/one-to-many-relationship-configuration
42. How to properly exit a C# application - https://stackoverflow.com/questions/12977924/how-to-properly-exit-a-c-sharp-application
43. Send __RequestVerificationToken with Fetch API and recieve with an [ValidateAntiForgeryToken] action - https://stackoverflow.com/questions/57727595/send-requestverificationtoken-with-fetch-api-and-recieve-with-an-validateanti
44. jQuery.ajax() - https://api.jquery.com/jQuery.ajax/
45. Open API v3 Specification - http://spec.openapis.org/oas/v3.0.3
46. How to return a specific status code and no contents from Controller? - https://stackoverflow.com/questions/37690114/how-to-return-a-specific-status-code-and-no-contents-from-controller
47. How do you detect the clearing of a search html5 input - https://stackoverflow.com/questions/2977023/how-do-you-detect-the-clearing-of-a-search-html5-input
48. Spinners - https://getbootstrap.com/docs/4.4/components/spinners/
49. Add custom user data to the Identity DB - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/add-user-data?view=aspnetcore-3.1&tabs=visual-studio#add-custom-user-data-to-the-identity-db
50. Customize the model - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-3.1#customize-the-model
51. How to enumerate an enum - https://stackoverflow.com/questions/105372/how-to-enumerate-an-enum
52. Route name - https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#route-name
53. Asp.net Identity 2.0 - Unique Email - https://stackoverflow.com/questions/27515561/asp-net-identity-2-0-unique-email
54. How can I force entity framework to insert identity columns? - https://stackoverflow.com/questions/13086006/how-can-i-force-entity-framework-to-insert-identity-columns
55. Long string interpolation lines in C#6 - https://stackoverflow.com/questions/31764898/long-string-interpolation-lines-in-c6
56. Entity Framework code-first date field creation - https://stackoverflow.com/questions/5658216/entity-framework-code-first-date-field-creation
57. How to read values from the querystring with ASP.NET Core? - https://stackoverflow.com/questions/41577376/how-to-read-values-from-the-querystring-with-asp-net-core
58. datetime2 (Transact-SQL) - https://docs.microsoft.com/en-us/sql/t-sql/data-types/datetime2-transact-sql?view=sql-server-ver15
59. Custom date and time format strings - https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings

## Project Description
A web application built to connect students to faculty to make finding research work frictionless. Professors can post
available research opportunities along with any required skills, pictures, related tags, etc. Students can quickly find
and sort through these opportunities and find the best one they can apply their skills to. There will also be an admin
section to allow admins to visualize URC usage as well as other monitoring/maintenance tasks.

Built on an ASP.NET Core 3.1 (MVC) backend and a vanilla frontend, styled with Bootstrap, and deployed on AWS.
