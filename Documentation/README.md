# Poker API Documentation #
[Back to __Getting Started__ and __Source Code Home Page__](../../../)

* [Number of Players Supported](#number-of-players-supported) 
* [Noteworthy NuGet Packages](#noteworthy-nuget-packages)
* [High Level Design](#high-level-design)
* [EFCore Migration](#efcore-migration)
* [Sample Request Models](#sample-request-models)

## Number of Players Supported ##
Maximum number of players i.e. 9

## Noteworthy NuGet Packages ##
* ```Autofac```: Replaces built-in ASP.NET Core IoC container
* ```AutoMapper```: Used for object-to-object mapping between Domain and Data layers  
* ```FluentValidation```: Used for validating Domain request models
* ```Microsoft.EntityFrameworkCore.*```: Used for object-to-relational mapping and data migration
* ```Serilog```: Used for structured logging to console as well as to SEQ dashboard app
* ```Swashbuckle.*```: Used for generating Swagger/OpenAPI documentation for the API as well as for Swagger UI

## High Level Design ##
* Clean code with separation of concerns, especially
   * __API/Controllers project__ only references __Services project__ and __Models project (DTO)__.
   * __Services project__ references __Repositories project__ which references __Mappers project__.
* Main logic that assigns Poker Ranks to submitted Poker Hands is in a library project ```RankAssignment.Simple``` which represents an included implementation for ```IRankAssigner``` interface. A separate helper class library ```Common``` contains card sorter, card converter, Poker Hand recognizer and an enum that models all 52 cards.
* __EfCore__ context and entity classes are only found in __Repositories project__, not in __Services project__. A layer of abstraction exists between them using __Unit of Work Pattern__ and __Repository Pattern__. Swappping out ```SQLite``` EFCore provider with ```MSSQL``` or ```PostgreSQL``` or ```Oracle``` provider should be a fairly simple task without requiring code changes in the API, Services, Assigner, Common or Model project.

## EFCore Migration ##
The solution is checked in with __initial EFCore Migration__ already done. The database file ```PokerDB.db``` should be in root solution directory and the migration files are in ```Migrations``` subfolder of __Repositories project__.

However, both the migration files and the database files can be deleted and a fresh new migration can be run as follows.

*This assumes ```dotnet-ef``` tool has already been installed globally as outlined on Getting Started page*

* Using PowerShell Package Manager Console in Visual Studio, ```cd``` to solution directory, then run this command
   ```dotnet
   dotnet ef migrations add InitialCreate --project PokerApi.Repositories --startup-project EfcMigrations.ConsoleApp
   ```
   This generates the database scripts to create tables and relationships based on your entity models. Make sure to see success response before proceeding to the next step
* Then run this command
   ```dotnet
   dotnet ef database update --project PokerApi.Repositories --startup-project EfcMigrations.ConsoleApp -- --environment Development
   ```
   This executes the generated scripts above. You should see a database file generated in the solution directory.
   
## Sample Request Models ##
These are some sample JSON models you can copy and paste, perhaps with some modifications if needed, in the Swagger UI and verify that the API ranks them correctly.

Single

* [FullHouse 1](sample-requests/fullhouse-1.json)
* [Flush 1](sample-requests/flush-1.json)

Multiple

* [HighCard 1](sample-requests/highcard-1.json)
* [HighCard 2](sample-requests/highcard-2.json)
* [HighCard 2](sample-requests/highcard-3.json)
* [StraightFlush and HighCard 1](sample-requests/straightflush-and-highcard-1.json)
* [Ace Low Five High 1](sample-requests/ace-low-five-high-1.json)
* [Ace Low Five High 2](sample-requests/ace-low-five-high-2.json)
* [FourOfAKind and Flush 1](sample-requests/fourOfAKind-and-flush-1.json)
* [Straight 1](sample-requests/straight-1.json)
* [Ace Low Five High 3](sample-requests/ace-low-five-high-3.json)
* [ThreeOfAKind 1](sample-requests/threeOfAKind-1.json)
* [TwoPairs 1](sample-requests/twoPairs-1.json)
* [Pair 1](sample-requests/pair-1.json)
* [__Mixed__ 1](sample-requests/mixed-1.json)

