# Blazor Todo List
This is a project template that consist of:
- A blazor frontend
- A API backend

The tricky part is to make backend url configurable in Blazor app, such that it can locate the backend no matter it is in docker compose network, standalone docker or start locally by `dotnet run`. The technique consists of 3 parts:
1. Config the backend url in docker compsoe `environemnt` attribute / pass by `-e` from `docker run`
2. Config docker to generate a new `nginx.conf` with reverse proxy set and substitute by environment variables passed from step 1, and overwrite the default config whenever the container starts.
3. Set the blazor app point to the reverse proxy path set in step 2 for all call to backend API

## How to run
- Local development
    - Set `backend_url` config in `wwwroot/appsettings.json`
- Manual docker deployment
    - `./build-run-docker`
- Docker compose
    - `docker compose build && docker compose up`