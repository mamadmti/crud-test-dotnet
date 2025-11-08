# Docker Setup

This project uses Docker Compose to manage the SQL Server database.

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running

## Quick Start

### Start the Database

```bash
docker-compose up -d
```

This will:

- Start SQL Server 2022 in a container
- Expose it on port `1433`
- Create a persistent volume for data
- Use credentials: `sa` / `YourStrong!Passw0rd`

### Check Database Status

```bash
docker-compose ps
```

### View Logs

```bash
docker-compose logs -f sqlserver
```

### Stop the Database

```bash
docker-compose down
```

### Stop and Remove Data

```bash
docker-compose down -v
```

## Apply Migrations

Once the database is running, apply migrations from the project root:

```bash
dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```

## Connection String

The application is configured to connect to:

```
Server=localhost,1433;Database=CustomerDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true
```

## Troubleshooting

### Port Already in Use

If port 1433 is already in use, you can change it in `docker-compose.yml`:

```yaml
ports:
  - "1434:1433" # Use host port 1434 instead
```

Then update your connection string to use port `1434`.

### Container Won't Start

Check the logs:

```bash
docker-compose logs sqlserver
```

### Reset Everything

```bash
docker-compose down -v
docker-compose up -d
dotnet ef database update --project Mc2.CrudTest.Infrastructure --startup-project Mc2.CrudTest.Presentation/Server
```
