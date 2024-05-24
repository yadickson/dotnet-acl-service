FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

COPY . .
RUN dotnet restore "Acl.Authentication.Service/Acl.Authentication.Service.csproj"
RUN dotnet publish "Acl.Authentication.Service/Acl.Authentication.Service.csproj" -c Release --no-restore -o /application

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /application
COPY --from=build /application .

ENTRYPOINT ["./Acl.Authentication.Service"]