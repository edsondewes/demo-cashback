FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY ["AppCashback.Api/AppCashback.Api.csproj", "AppCashback.Api/"]
COPY ["AppCashback.Core/AppCashback.Core.csproj", "AppCashback.Core/"]
COPY ["AppCashback.MongoDb/AppCashback.MongoDb.csproj", "AppCashback.MongoDb/"]
RUN dotnet restore "AppCashback.Api/AppCashback.Api.csproj"

COPY . .
WORKDIR "/src/AppCashback.Api"
RUN dotnet publish "AppCashback.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AppCashback.Api.dll"]