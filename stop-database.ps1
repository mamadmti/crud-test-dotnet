# Stop and remove database container
Write-Host "Stopping SQL Server database..." -ForegroundColor Yellow
docker-compose down

Write-Host "✓ Database stopped!" -ForegroundColor Green
Write-Host "Note: Data is preserved in the Docker volume 'sqlserver_data'" -ForegroundColor Cyan
Write-Host "To remove data as well, run: docker-compose down -v" -ForegroundColor Yellow

