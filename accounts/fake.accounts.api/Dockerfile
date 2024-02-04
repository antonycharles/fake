FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as base
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /
COPY . .
RUN dotnet restore "/src/Accounts.API/Accounts.API.csproj"
WORKDIR "/src/Accounts.API"
RUN dotnet build "Accounts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.API.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Accounts.API.dll" ]