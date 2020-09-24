Harbor Control Systems
============================
The Durban harbor control requires a concurrency safe application that will help the staff control the flow of boats 
and ships into the harbor.

Tools: Visual Studio 2019 Professional, Visual Studio code.

Softwares: .NET Core 3.1 SDK, Node.js

Language & Technology: Microsoft .NET Core, C#.NET,Web Api, Dependency Injection, Repository Patterns, Angular 8

Web Technology: Html, Bootstrap 3, Typescript,CSS

Project Architecture

1. Presentation Layer - Angular 8 for UI layer
2. Bussiness Layer -  HarborControlDemo.Api, HarborControl.Repository
3. Data Access Layer- HarborControlDemo.DataModels
4. Shared Library - HarborControlDemo.Utility

Back-end required Nuget Package install
1. NLog.Web.AspWebCore - for global error exception handling to store log in file.
2. Microsoft.Extensions.Logging - To log any run time error
3. Microsoft.Extensions.Http: To call third party API 

Front-end required package
1. npm install bootstrap@3.4.1 --save
2. npm ngx-toastr 9.1.0 --save
3. npm install @angular/animations --save

How to Run?
- First open back-end project solutions and build all project solutions and install all dependency packages, required packages and run the project in debug.
- For front-end application first open "HarborControlDemo" folder in visual studio code and from the top menu terminal select new terminal install all node package using "npm install" 
command.
- To run front-end application use command "ng serve --o" which open default port "http://localhost:4200"
 