# Aareon Technical Test - Ticketing System
## Overview
We need you to extend the current ticketing system that will allow additional resources to be included.

- [x] Implement RESTful endpoint(s) that will allow all Create, Read, Update, and Delete (CRUD) actions on a Ticket.
- [x] Now amend the solution to allow the addition of Notes to a Ticket.
- [ ] Amend the solution to track any data manipulation and actions. Auditing could happen out of process.
- [x] Create a Pull Request on github.

### Requirements
- A note is created by a Person to log additional information against a ticket.
- A note can be created, updated, or removed by anyone, but only an Administrator may delete an existing note.
- Any actions that are taken against records in the ticketing system are subject to monitoring and auditing.
- This application will be deployed automatically using a CI/CD pipeline.

These tasks should take no longer than 4 hours

## Reviewer Notes
### Before looking at the technical test and requirements
I had the following thoughts and questions before seeing the actual techical test and requirements:

- What is the expected experience level of the Software Engineer(s) that will be responsible for support and maintenance going forward?
  - I personally feel that this has a direct impact of the up-front efforts (and thus costs) that should be undertaken with regards to code complexity and internal documentation.  This also has a bearing on how complicated and abstracted the solution needs to be.
- What is the expected lifespan of this solution?  Technology moved forward at an ever increasing pace, and technologies such as .NET Core 3.1 has an end-of-life support on 03/12/2022.  As such what is the plans going forward for any such solution in terms of support and maintenance when such end-of-life is approaching?
  - I personally feel that without forward planning for such lifespan events can result in a degradation of service, and higher initial resource costs for initial implementation.
- What levels of performance are required for the service?  In respects of average response time to the end user, but also in terms of concurrency of users?
  - I personally feel that this is often either missing or over stated and that a reasonable performance metric should be in place.  If this is missing or over stated, the implementation costs can often be much higher than required.  This also has a bearing on how complicated the solution needs to be.
  - In my experience the handling of concurrent users is often overlooked.
- Would the service be required to scale up to meet needs?
  - Automatic up and down scaling of services  by using services such as Azure API management instance to add and remove units can benefit the service during peak times.
- Would the service be required to scale out to meet localised needs?
  - Multi-region deployment across a number of Azure regions would be of benefit if the service is used in such regions, from reading the ‘Welcome’ section of the Recruitment Pack, I can see that the parent company “Aereal Bank Group” operates in a total of 28 countries.
- Is the service required to support languages other than en-GB?
  - I feel that whilst API end-points do not ‘necessarily’ need to change to provide multi-language, they can be of benefit.  Multi-language if required would need to be handled thought the entirety of the solution, from front-end, user language detection, to data storage and information retrieval (if the user is en-GB, should any data returned include other languages such as uk-UA?
- Has the client considered implementing the service in a manner that provides a cross-platform solution, such as both Web and Mobile?
  - I personally feel that the use and acceptance of mobile applications is starting to out-weigh traditional web based applications.  Users are most likely to have available a mobile device, rather than a traditional desktop/laptop.  Implementing a solution such as .NET 6 with MAUI could provide a multi-platform app UI across iOS, Android, macOS and Windows by possibly following the Progressive Web Application route.
- Logging is indicated as being used, but there is no definition as to what has to be logged and why, what are the logging requirements?
  - Logging is only as good as the data logged and the tools already in place to turn the data into meaningful and timely information.  I personally feel that often logging is over-done and this tends to cause so much noisy data, that the true data that is lost.  Worse, when there is insufficient logging in place, the truly important logging data is missed.
- There is a ‘hint’ towards the service requires authentication and authorisation.
  - I think that this could determine the kind of authentication and authorisation that could be implemented.  Such as Azure AD, Azure AD B2C (External Identities) or a custom JWT based authentication.
- Security is often only tagged onto a project as a side-note or by-product.  If required, are there details of what authorisation each service end-point should have in place?
  - In my experience, security is often just ‘assumed’ and never truly defined.  As such, it is often the diligent Software Engineer that takes authentication and authorisation into account, and not always to a successful end.

# Reviewer Updates
## GitHub Repository
The main branch for this GitHub repository can be found here: [Main Branch](https://github.com/GeorgeLeithead/AareonTechnicalTest).

As the test was supplied in a ZIP format, the ZIP was extracted localy and after adding a few basic Git files, was PUSHed to GitHub.  See the [repository history](https://github.com/GeorgeLeithead/AareonTechnicalTest/tree/252229e20484372179cbdc380621cb33536a6d2d) for the initial files from the ZIP.

Being the only person contributing to the repository, I took the decision to work directly on the MAIN branch.  Normally when working with others, a branch would be created for the amendments and mreged with main when appropriate.
### GitHub Actions
#### Build
Added a dotnet.yml to build and run any tests when the code is pulled to GitHub.
#### Code Quality
Added a Code Quality build action, to automate the performance of standard code quality checks.

## Upgrade .NET to LTS
The solution is based on .NET 5.0, which has an end-of-life support date of May 8th 2022 See [Microsoft .NET and .NET Core - Microsoft Lifecycle](https://docs.microsoft.com/en-us/lifecycle/products/microsoft-net-and-net-core) for details.


## Global Usings
Added in C# 10 Implicit using directives.  The intention is to simplify and lighten code, by declaring only once a using keyword on a specific namespace on a project.  See [Implicit using directives](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview#implicit-using-directives) for details.

## Editor Config
Added to the solution is an EditorConfig file, used to enforce consistent coding styles and standards for everyone that works in the code base. See [Create portable, custom editor settings with EditorConfig](https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options?view=vs-2022) for details.

## Minimal API
Upgraded project to use the minimal API framework. See [Minimal APIs overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0) for details.

## Entity Framework - Code First
Taking the '[code first](https://entityframework.net/ef-code-first)' principal of Entity Framework, the column 'Note' was added to the 'Ticket' POCO with appropriate attributes.  Then using the Package Manager Console, a new migration was added and then the database was upgraded to include the changes.
```cmd
Add-Migration AddNoteToTicket
Update-Database
```
I took the decision to make the column nullable and to have a maximum length.

## JsonIgnore
After adding a one-to-many reference for ticket to notes, there is a JSON referece loop created.  To avoid this, we ignore the reference loop properties by using ```[JsonIgnore]```.  See [How to ignore properties with System.Text.Json](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-ignore-properties?pivots=dotnet-6-0) for details.

## View-Models
In order to return and accept records it 'may' be necessary to provide view-models for end-points where necessary.  This would allow the JSON to be richer, without compromising the base data models.

For example, for the 'GetTicketNoteById', ideally the JSON would return the Id, Note and also the Person object.