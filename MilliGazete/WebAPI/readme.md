# API

This web API is developed with .Net Core 3.1 to demonstrate crud operations using Entity Framework Core 3.1 with PostgreSql Server as backend and returns json as response.
To see all the available endpoints please visit : [Swagger](http://milligazete.istmedyaapi.com/swagger/index.html)

## Prerequisites
* in order to run this app, you need .Net Core SDK that includes .Net CLI tools and .Net Core Runtime installed on our machine. So download and install the latest version of .Net Core SDK available from this [link](https://dotnet.microsoft.com/download).
* Postman(optional) which a famous API Client tool used in testing web API by sending web requests and inspecting response from the API. So You can download and install Postman for free from this [link](https://www.getpostman.com/downloads/).
* And last, we need to have PostgreSql Server installed on the machine.Otherwise download and install latest PostgreSql Server from this [link](https://www.postgresql.org/download/).

## Tips
To update and synchronize database with project we can use Commands.txt file.
There is command which uses Npgsql nuget package to retrieve models from Postgresql database
(if your current project is not in the WebApi project directory please first enter this command : 
cd .\WebApi 
 and then enter run related command).


## Built With
* .Net core 3.1
* PostgreSql
* Entity Framework Core
* Fluent Validation
* Autofac DI Resolver
* Jwt Authenticator

## Packages
* Autofac.Extensions.DependencyInjection
* MicroKnights.Log4NetAdoNetAppender
* Microsoft.AspNetCore.Authentication.JwtBearer
* Microsoft.EntityFrameworkCore.Tools
* Npgsql.EntityFrameworkCore.PostgreSQL
* Swashbuckle.AspNetCore

Please make sure to update tests as appropriate.

## License
[MilliGazete](http://milligazete.istmedyaapi.com/)