# Orhedge

Application is meant to be place where student can work together, sharing study materials
and communication on different topics. Questions can be asked, comments can be liked,
users can be rewarded or punished with excluding from system. Materials are filtered and sorted, 
and student can upload/download material from a page.

## Getting started

You can **clone** the repo on your local machine using https://Orhege@dev.azure.com/Orhege/Orhege/_git/Orhege , and
open it in Visual Studio. **You need to have Microsoft Account**. 
If you don't have one, you can open it for free - [(Sign in/Create)Microsoft Account](https://account.microsoft.com/account?lang=en-us);

### System architecture

![Architecture](/ArchitectureExample.png) <br/>
Three-layered architecture is used:
- **Orhedge** project represents presentation layer. It is ASP.NET Core Web Application.
It uses [Autofac](https://autofaccn.readthedocs.io/en/latest/getting-started/) as Dependency Injector and [Automapper](https://automapper.org/) for automatic mapping between models.
- **ServiceLayer** project represents Business Logic Layer and contains main logic for 
application. It is .NET Core Library project with dependencies: Automapper and [NLog](https://github.com/nlog/nlog/wiki/Configuration-file). Nlog
is modern library used for logging purposes. It uses simple [Repository Pattern](https://deviq.com/repository-pattern/).
- **DatabaseLayer** project represents Data Layer and it wraps around [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/).
Code First Approach is used, as well as [FluentAPI](https://www.entityframeworktutorial.net/code-first/fluent-api-in-code-first.aspx).
It contains database entities and main context which offers database access.
 

### Prerequisites

Project is **not dependent** on type of a platform, you just need to have [.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) 
installed on your system.
You need to install:
- [Visual Studio](https://visualstudio.microsoft.com/downloads/),
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads),
- [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15).

### Installing

## Running the tests

You can run all tests using [Visual Studio Test Explorer](https://docs.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2019).
There you have hierarchical structure showing you test classes and test methods. You run tests by clicking on button Run.
Project uses [MSTest Framework](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest) for testing.

###  What is being tested?

Unit test are written for service classess from ServiceLayer.dll project.

### And coding style tests

Each test class is named by class that is being tested. Each test method is named using this convention:
public void MethodName*DataValidity*Returns*Result*();

## Deployment

Application is supposed to use remote file server, so it needs to be configured. Using Publish
option in Visual Studio, ASP.NET Web Application can be published.


## Built With


## Authors

- Marija Novakovic
- Bojan Malinic
- Milica Vasic
- Aleksandar Zrnic
- Goran Majstorovic