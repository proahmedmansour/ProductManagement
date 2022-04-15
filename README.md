# Product Management
This is a starter template, a well-architected, production-ready solution based on ABP framwork.

# Installation
1. Clone or download the code from this repository.
2. Open the solution with visual studio.
3. Change the connection string. The connection string is defined in the appsettings.json file 
in the ProductManagement.Web and ProductManagement.DbMigrator projects and If you change it, change it in both places

# Creating a database
ProductManagement.DbMigrator is a console application that simplifies creating and migrating a database in development
and production. It also seeds the initial data, creating an admin role and user to log in to the application.

Right-click the ProductManagement.DbMigrator project and select the 
Set as Startup Project command. Then, run the project using Ctrl + F5 to run it 
without debugging.

# Running the web application
Set ProductManagement.Web as the startup project and run it using Ctrl + F5 (start 
without debugging).

If you got this error "AbpException: Could not find the bundle file '/libs/abp/core/abp.css' for the bundle 'Basic.Global'!"
The solution is: open your project folder and run this command "abp install-libs" and of course you need to install abp cli first. 

If it works properly you will see a main page opened in your browser, navigate to login page and insert admin and 1q2w3E* as your username and password.

# Exploring the pre-built modules
ABP is a modular framework, and this solution has installed the fundamental modules, Let's explore it.

The pre-installed modules are Account, Identity, and Tenant Management. These modules source code not included in this solution They are used as NuGet packages and easily upgraded 
when a new ABP version is published. They are designed as highly customizable, without touching their code. However if you need, you can include their source code in your 
solution to freely change them based on your unique requirements.

# Solution Structure
Projects are organized in src and test folders. src folder contains the actual application which is layered based on DDD principles.

Each section below will explain the related project & its dependencies.
1. Domain.Shared Project:
This project contains constants, enums and other objects these are actually a part of the domain layer, but needed to be used by all layers/projects in the solution.
This project has no dependency to other projects in the solution. All other projects depend on this directly or indirectly.

2. Domain Project:
This is the domain layer of the solution. It mainly contains entities, aggregate roots, domain services, value objects, repository interfaces and other domain objects. Depends on the .Domain.Shared because it uses constants, enums and other objects defined in that project.

3. Application.Contracts Project: 
This project mainly contains application service interfaces and Data Transfer Objects (DTO) of the application layer. It does exists to separate interface & implementation of the application layer. In this way, the interface project can be shared to the clients as a contract package. Depends on the .Domain.Shared because it may use constants, enums and other shared objects of this project in the application service interfaces and DTOs.

4. Application Project: 
This project contains the application service implementations of the interfaces defined in the .Application.Contracts project.
Depends on the .Application.Contracts project to be able to implement the interfaces and use the DTOs.
Depends on the .Domain project to be able to use domain objects (entities, repository interfaces... etc.) to perform the application logic.

5. EntityFrameworkCore Project: 
This is the integration project for the EF Core. It defines the DbContext and implements repository interfaces defined in the .Domain project.Depends on the .Domain project to be able to reference to entities and repository interfaces.

6. DbMigrator Project:
This is a console application which simplifies to execute database migrations on development and production environments. When you run this application, it;
Creates the database if necessary.
Applies the pending database migrations.
Seeds initial data if needed.
Depends on the .EntityFrameworkCore project (for EF Core) since it needs to access to the migrations.
Depends on the .Application.Contracts project to be able to access permission definitions, because initial data seeder grants all permissions for the admin role by default.

7. HttpApi Project: This project is used to define your API Controllers. Most of time you don't need to manually define API Controllers since ABP's Auto API Controllers feature creates them automagically based on your application layer. However, in case of you need to write API controllers, this is the best place to do it. Depends on the .Application.Contracts project to be able to inject the application service interfaces.

8. HttpApi.Client Project: This is a project that defines C# client proxies to use the HTTP APIs of the solution. You can share this library to 3rd-party clients, so they can easily consume your HTTP APIs in their Dotnet applications (For other type of applications, they can still use your APIs, either manually or using a tool in their own platform) Most of time you don't need to manually create C# client proxies, thanks to ABP's Dynamic C# API Clients feature. .HttpApi.Client.ConsoleTestApp project is a console application created to demonstrate the usage of the client proxies. Depends on the .Application.Contracts project to be able to share the same application service interfaces and DTOs with the remote service.

9. Web Project:
This project contains the User Interface (UI) of the application if you are using ASP.NET Core MVC UI. It contains Razor pages, JavaScript files, CSS files, images and so on... This project contains the main appsettings.json file that contains the connection string and other configuration of the application. Depends on the .HttpApi since UI layer needs to use APIs and application service interfaces of the solution.

### Test Projects
.Domain.Tests is used to test the domain layer.
.Application.Tests is used to test the application layer.
.EntityFrameworkCore.Tests is used to test EF Core configuration and custom repositories.
.Web.Tests is used to test the UI (if you are using ASP.NET Core MVC UI).
.TestBase is a base (shared) project for all tests.

This template comes with the test infrastructure properly configured using the 
xUnit, Shouldly, and NSubstitute libraries. It uses the SQLite in-memory database to 
mock the database. A separate database is created for each test. It is seeded and destroyed 
at the end of the test. In this way, tests do not affect each other, and your real database 
remains untouched.
