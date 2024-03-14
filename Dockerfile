FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.sln .
COPY OnlineDictionary.csproj ./
RUN dotnet restore "./OnlineDictionary.csproj"

COPY . .
RUN dotnet publish "OnlineDictionary.csproj" -c Release -o out
# RUN dotnet build "OnlineDictionary.csproj" -o /out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "OnlineDictionary.dll"]
