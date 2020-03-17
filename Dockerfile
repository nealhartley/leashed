# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.csproj leashApi/
WORKDIR /src/leashApi
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /src/leashApi
Run dotnet publish -c Release -o /src/publish

#Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "leashApi.dll"]