FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY shared/shared.csproj shared/
RUN dotnet restore shared/shared.csproj

COPY frontend/frontend.csproj frontend/
RUN dotnet restore frontend/frontend.csproj

COPY shared shared
COPY frontend frontend
RUN dotnet publish frontend/frontend.csproj -c Release -o out -r linux-musl-arm64

# This is a working example on how to inject environment variable into nginx container
# It have to pass env variable during runtime, just before container start generate new nginx.conf 
# and overwrite the default one with variables replaced by passed environment variables
FROM nginx:alpine AS deploy
COPY --from=build /app/out /usr/share/nginx/html
COPY frontend/nginx.conf /etc/nginx/nginx.conf.template
# The cat /etc/nginx/nginx.conf is for debugging purpose, ensuring the variables are replaced successfully
CMD /bin/sh -c "envsubst '\$API_URL' < /etc/nginx/nginx.conf.template > /etc/nginx/nginx.conf && cat /etc/nginx/nginx.conf && exec nginx -g 'daemon off;'"