### ACL Authentication Service

# Install tools

```
dotnet tool install -g dotnet-format
dotnet tool install -g dotnet-stryker
dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet tool install -g JetBrains.ReSharper.GlobalTools
dotnet tool install -g dotnet-outdated-tool
```

# Restore tools

```
dotnet tool restore
```

## Update dependencies

```
dotnet outdated -vl Major -t -u
```

# Restore application

```
dotnet restore
```

# Clean application

```
dotnet clean
```

# Build application

```
dotnet build
```

# Tests application

```
dotnet test
```

# Mutation Tests application

```
dotnet dotnet-stryker --output . -m:1
```

# Coverage report

```
dotnet test /p:MergeWith="../coverage/coverage.json" -m:1
dotnet reportgenerator -reports:"./coverage/coverage.cobertura.xml" -targetdir:"./reports/coverage" -reporttypes:Html
```

## Lint

```
dotnet jb inspectcode Acl.Authentication.Service.sln --jobs=1 --output=./reports/lint/index.html --format=Html --exclude=Acl.Authentication.Service.Api.Tests/TestStartup.cs
```

## Format and lint Fix

```
dotnet format && dotnet jb cleanupcode Acl.Authentication.Service.sln
```

## Dependencies

```
dotnet list package --include-transitive
```

## Dependency check

```
dependency-check --scan . --format HTML --project "acl-authentication-service" --out ./reports/
```

## Sonar

```
sonar-scanner -Dsonar.host.url=http://localhost:9000
```

# Run application

```
dotnet run --project Acl.Authentication.Service/Acl.Authentication.Service.csproj -c Release --urls=http://localhost:10000/
```

# Run application in watch mode

```
dotnet watch --project Acl.Authentication.Service/Acl.Authentication.Service.csproj run -c Debug --urls=http://localhost:10000/
```
