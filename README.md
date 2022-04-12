# ProductManagement
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
