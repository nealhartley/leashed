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

#Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
#look into defining https and http also look into ipv4 and ipv6 bindings
ENV ASPNETCORE_URLS http://*:5000;https//*:5001 
ENTRYPOINT ["dotnet", "leashApi.dll"]
#COPY --from=build /src/leashApi/entrypoint.sh .
#RUN chmod +x ./entrypoint.sh
#CMD /bin/bash ./entrypoint.sh