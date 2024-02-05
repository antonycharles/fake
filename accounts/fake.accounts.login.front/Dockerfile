FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as base
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /
COPY . .
RUN dotnet restore "/src/Accounts.Login.WebApp/Accounts.Login.WebApp.csproj"
WORKDIR "/src/Accounts.Login.WebApp"
RUN dotnet build "Accounts.Login.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.Login.WebApp.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Accounts.Login.WebApp.dll" ]