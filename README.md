# RedisCachingDotnetFullstack
RedisCachingDotnetFullstack is a full-stack web application designed to manage a game store, utilizing **.NET Core Blazor** for the frontend and **.NET Core Web API** for the backend. 
**SQLite** facilitates database operations, while **Redis caching** enhances performance by storing frequently accessed data.

## Redis Caching
**Redis** is used in this project to improve the performance of the application by caching frequently accessed data. This reduces the load on the database and speeds up response times. 
**Redis Commander** is provided as a web-based administrative interface to manage and inspect the data stored in Redis.

### How Redis is Integrated
- The backend API uses Redis to cache game data that is frequently requested.
- When a game is added, updated, or deleted, the cache is updated accordingly to ensure consistency between the cache and the database.
- Redis is configured and managed through Docker, making it easy to set up and run locally.
- By using Redis, the application can handle higher loads more efficiently, providing a better user experience.
  
## Development Prerequisites
Before diving into development with this project, ensure you have the following prerequisites:
- **Development Environment**: Either Visual Studio 2022 (IDE) or Visual Studio Code (Source-code editor)
- **.NET 8**: Required framework version for building and running the project
- **Docker Desktop**: Required for running the containers of Redis Server and Redis Commander

## Getting Started
To run the app locally, follow these steps:
1. Clone this repository to your local machine.
2. Ensure you have Docker installed and running.
3. Open a shell and navigate to the tools folder within the cloned repository.
4. Run the following command to set up the local development infrastructure, including starting the containers of Redis Server and Redis Commander and applying database migrations:
   ```bash
   ./start.bat
5. Once the containers are running, you can connect to Redis Commander by visiting http://localhost:8082 in your web browser.
6. Run both the frontend and backend components of the application using the appropriate commands or methods for your development environment.

## Configuration Instructions
In your `appsettings.json` file located in the frontend project, replace `http://your-api-url.com` with the actual URL of your backend API.
```json 
{
  "GameStoreApiUrl": "http://your-api-url.com",
  // Other configurations...
}
```
## Screenshots
### Frontend Home Page (with Example Items):
![Frontend Screenshot](https://github.com/gsherwin360/RedisCachingDotnetFullstack/assets/17651320/d4e00305-df7c-460b-921e-5c21a98e50a8)

### API Swagger Documentation:
![Backend Screenshot](https://github.com/gsherwin360/RedisCachingDotnetFullstack/assets/17651320/12f468b2-5149-43e3-a835-3e0305a22fd1)

### Redis Commander:
![Redis Commander Screenshot](https://github.com/gsherwin360/RedisCachingDotnetFullstack/assets/17651320/e42597ee-452d-410e-941e-99ae81c1a42a)

### Docker:
![Docker Screenshot](https://github.com/gsherwin360/RedisCachingDotnetFullstack/assets/17651320/ebb81876-ac77-4d41-be41-8b21e13e0334)
