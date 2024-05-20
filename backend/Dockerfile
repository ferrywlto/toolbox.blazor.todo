FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN apt update
RUN apt install -y clang zlib1g-dev
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out -r linux-musl-arm64

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS deploy
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://+:5000 
ENTRYPOINT ["dotnet", "backend.dll"]