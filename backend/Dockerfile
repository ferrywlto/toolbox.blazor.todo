FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY shared/shared.csproj shared/
RUN dotnet restore shared/shared.csproj

COPY backend/backend.csproj backend/
RUN dotnet restore backend/backend.csproj

COPY . .
RUN cd backend
RUN dotnet publish -c Release -o out -r linux-musl-arm64 

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS deploy
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://+:5000 
ENTRYPOINT ["dotnet", "backend.dll"]
