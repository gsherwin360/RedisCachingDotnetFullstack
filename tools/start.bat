@REM // Start Docker containers
docker-compose up -d

@REM // Wait for containers to be fully up
timeout  /t 30 /nobreak

@REM // Navigate to the backend project directory
cd ../backend/src/GameStore.Api

@REM // Add EF migrations
dotnet ef migrations add InitialCreate --output-dir Data\Migrations

@REM // Apply migrations
dotnet ef database update

@REM // Navigate back to the tools directory
cd ../../../tools