# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WebApp/*.csproj ./WebApp/
COPY WebApp.Core/*.csproj ./WebApp.Core/
COPY WebApp.Domain/*.csproj ./WebApp.Domain/
COPY WebApp.Infrastructure/*.csproj ./WebApp.Infrastructure/
COPY WebApp.Test/*.csproj ./WebApp.Test/
COPY WebApp.Utility/*.csproj ./WebApp.Utility/
RUN dotnet restore

# copy everything else and build app
COPY WebApp/. ./WebApp/
COPY WebApp.Core/. ./WebApp.Core/
COPY WebApp.Domain/. ./WebApp.Domain/
COPY WebApp.Infrastructure/. ./WebApp.Infrastructure/
COPY WebApp.Test/. ./WebApp.Test/
COPY WebApp.Utility/. ./WebApp.Utility/
WORKDIR /source/WebApp
RUN dotnet publish -c Release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebApp.dll"]
