FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out -r linux-musl-x64

FROM nginx:alpine AS deploy
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/
COPY --from=build /app/out /usr/share/nginx/html
ENTRYPOINT ["nginx", "-g", "daemon off;"]