docker build . -t toolbox.blazor.todo.backend:local  --file backend/Dockerfile
docker run -d -p 5000:5000 toolbox.blazor.todo.backend:local
docker build . -t toolbox.blazor.todo.frontend:local  --file frontend/Dockerfile
docker run --rm -p 80:80 -e API_URL=http://host.docker.internal:5000/ toolbox.blazor.todo.frontend:local