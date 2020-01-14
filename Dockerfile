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
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
# ENTRYPOINT ["dotnet", "leashApi.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet leashApi.dll