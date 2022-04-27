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
Added a Code Quality build action, to automate the performance of standard code quality checks.  There are a few issues raised by this, and are have been left in to demonstrate it's functionality.

## Upgrade .NET to LTS
The solution is based on .NET 5.0, which has an end-of-life support date of May 8th 2022 See [Microsoft .NET and .NET Core - Microsoft Lifecycle](https://docs.microsoft.com/en-us/lifecycle/products/microsoft-net-and-net-core) for details.


## Global Usings
Added in C# 10 Implicit using directives.  The intention is to simplify and lighten code, by declaring only once a using keyword on a specific namespace on a project.  See [Implicit using directives](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview#implicit-using-directives) for details.

## Editor Config
Added to the solution is an EditorConfig file, used to enforce consistent coding styles and standards for everyone that works in the code base. See [Create portable, custom editor settings with EditorConfig](https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options?view=vs-2022) for details.

## Minimal API
Upgraded project to use the minimal API framework. See [Minimal APIs overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0) for details.

## Entity Framework
### Code First
Taking the '[code first](https://entityframework.net/ef-code-first)' principal of Entity Framework, the column 'Note' was added to the 'Ticket' POCO with appropriate attributes.  Then using the Package Manager Console, a new migration was added and then the database was upgraded to include the changes.
```cmd
Add-Migration AddNoteToTicket
Update-Database
```
I took the decision to make the column nullable and to have a maximum length.

### Query the database
```PowerShell
Import-Module PSSQLite
$Database = ".\Ticketing.db"
$Query = "SELECT * FROM sqlite_master WHERE type='table'"
Invoke-SqliteQuery -DataSource $Database -Query $Query
```

## PostRejection branch
A new branch which implements the 'domain/module-driven' approach.  It moves from the traditional folder structure where the application is grouped by its domain.  The different domains of the application are organised in module (or feature) folders.

### The structure of a module
The benefits of this approach makes that every module becomes self-contained.  Simple modules can have a simple setup, while a module has the flexibility to deviate from the "default" setup for more complex modules.  A domain-based structure groups files and folders by their (sub)domain, this gives us a better understanding of the application and makes it easier to navigate through the application.

## Testing
We will be using XUnit for integration testing.
### InternalsVisibleTo
To make the API project visible to internal testing, we need to add the following to the API project:
```xml
<ItemGroup>
	<InternalsVisibleTo Include="AareonTechnicalTest.Tests" />
</ItemGroup>
```
Add to the API Program.cs the following to make the implicit Program class public so test projects can access it.
```csharp
public partial class Program { }
```

### Using HTTPREPL
 1. Run the following .NET Core CLI command in the command shell:
```bash
dotnet run
```
The preceding command locates the project at the current directory, retrieves and installs any required project dependencies, compliles the project code, and hosts the web API with the ASP.NET Code Kestrel web server at both an HTTP and HTTPS endpoint.

Ports as defined in the project will be selected for HTTP, port 5000, and port 5001 for HTTPS. Ports used during development can be easily changed by editing the project's launchSettings.json file.

A variation of the following output appears to indicate that the app is running:

```cmd
Building...
info: AareonTechnicalTest[0]
	  Starting AareonTechnicalTest 04/27/2022 11:40:19
info: Microsoft.Hosting.Lifetime[14]
	  Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
	  Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
	  Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
	  Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
	  Content root path: ...\AareonTechnicalTest\AareonTechnicalTest\
```
2. Ope a new integrated terminal, then run the following command to install the .NET HTTP REPL command-line tool, to use to make HTTP requests to the web API:
```cmd
dotnet tool install -g Microsoft.dotnet-httprepl
```
3. Connect to the web API by running the following command:
```cmd
httprepl https://localhost:5001
```
4. Explore the available endpoints by running the following command:
```cmd
ls
```
The preceding command detects all APIs available on the connected endpoint.  A variation of the following output appears:
```cmd
https://localhost:5001/> ls
.        []
person   [GET|POST]
ticket   [GET|POST]
```
5. Goto the **person** endpoint by running the following command:
```cmd
cd person
```
6. make a **GET** request in **HttpRepl** by using the following command:
```cmd
get
```
The preceeding command makes a **GET** request:
```json
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Date: Wed, 27 Apr 2022 13:51:07 GMT
Server: Kestrel
Transfer-Encoding: chunked

[
  {
	"forename": "Paris",
	"id": 1,
	"isAdmin": true,
	"surname": "Hilton",
	"ticketNotes": [
	],
	"tickets": [
	]
  },
  {
	"forename": "Jack",
	"id": 2,
	"isAdmin": false,
	"surname": "Griffiths",
	"ticketNotes": [
	],
	"tickets": [
	]
  },
]
```