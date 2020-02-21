# Some user

This project was made for the purpose of implementing an api similar to <https://randomuser.me/>.

## Prerequisites

- .Net Core 3.1
- Docker, or an instance of SQL Server that you can connect to.

## Getting started

Clone the repository.

```bash
git clone https://github.com/isaac-brown/some-user.git
```

Start a SQL Server instance using docker.

```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Welcome12' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

> If you don't have docker, then you'll need access to another SQL Server instance, and will have to configure the `"ConnectionStrings:SomeUser"` property of `appsettings.json` in the `SomeUser.Api` project as follows:
> ```json
>  ...
>  "ConnectionStrings": {
>     "SomeUser": "Server={yourServer};User={yourUser};Password={yourPassword};Database=SomeUser"
>  }
> ...
> ```
> The connection string can be anything you want as long as it's in a valid format.

Change directory to the API project folder.

```bash
cd src/SomeUser.Api
```

Start the web api on <http://localhost:8080>.

```bash
dotnet run
```

## Running tests

To run all tests you can just run:

```bash
# assuming you're in the top level folder
dotnet test
```

This will run integration and unit tests. Note that integration tests interact directly with a SQL Server instance, so ensure that connectivity has been sussed out.
