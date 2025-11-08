# Docker Helper Scripts for Customer Management System

# Start the database
Write-Host "Starting SQL Server database..." -ForegroundColor Green
docker-compose up -d

# Wait for health check
Write-Host "Waiting for database to be ready..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

# Check if database is healthy
$status = docker inspect --format='{{.State.Health.Status}}' sqlserver 2>$null
if ($status -eq "healthy") {
    Write-Host "✓ Database is ready!" -ForegroundColor Green
} else {
    Write-Host "Database is starting up... checking logs:" -ForegroundColor Yellow
    docker-compose logs sqlserver
}

# Show connection info
Write-Host "`nConnection String:" -ForegroundColor Cyan
Write-Host "Server=localhost,1433;Database=CustomerDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true" -ForegroundColor White

Write-Host "`nNext steps:" -ForegroundColor Yellow
Write-Host "1. Apply migrations: dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server" -ForegroundColor White
Write-Host "2. Run the app: dotnet run --project Mc2.CrudTest.Presentation/Server" -ForegroundColor White

