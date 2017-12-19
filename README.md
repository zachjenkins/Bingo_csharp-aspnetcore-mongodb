# Bingo_csharp-aspnetcore-mongodb

    Bingo!
    
    Bingo is a RESTful API that provides data about fitness.. and fitness related.. stuff...

# Tools Used

[Mongo DB](https://docs.mongodb.com/)

[Mongo DB C# Driver Library](https://github.com/mongodb/mongo-csharp-driver)

[ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)

# Deploying Bingo

The following things can be done in any order!

## Starting Local Mongo Instance

    Deploy MongoDB locally by running the following command in powershell:

    From \Bingo_csharp-aspnetcore-mongodb\database >> `start-process start_db.bat`

    Mongo will be hosted locally over port `27017` with a connection string of `mongodb//localhost:27017`

    You may consider downloading a free MongoDB client from [RoboMongo](https://robomongo.org/)

## Deploying API Locally

    There are 2 ways to deploy the API:

    1. Deploy from visual studio under the *debug* menu.
    1. Deploy from command-line using dotnet core build tools

    From the directory .\Bingo_csharp-aspnetcore-mongodb\src\Bingo.Api >> `dotnet run`

## Running Tests

    **Mongo does not need to be deployed for this to work**

    1. Run them in Visual Studio!
    1. If you do not have VS, run tests from command-line with the following:

    From the root of the project >> `dotnet test`