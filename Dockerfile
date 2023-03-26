FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
EXPOSE 8080

# copy .csproj and restore as distinct layers
COPY "Reactivities.sln" "Reactivities.sln"
COPY "Reactivities.API/Reactivities.API.csproj" "Reactivities.API/Reactivities.API.csproj"
COPY "Reactivities.Application/Reactivities.Application.csproj" "Reactivities.Application/Reactivities.Application.csproj"
COPY "Reactivities.Persistence/Reactivities.Persistence.csproj" "Reactivities.Persistence/Reactivities.Persistence.csproj"
COPY "Reactivities.Domain/Reactivities.Domain.csproj" "Reactivities.Domain/Reactivities.Domain.csproj"
COPY "Reactivities.Infrastructure/Reactivities.Infrastructure.csproj" "Reactivities.Infrastructure/Reactivities.Infrastructure.csproj"

RUN dotnet restore "Reactivities.sln"

# copy everything else build
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out

# build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Reactivities.API.dll" ]