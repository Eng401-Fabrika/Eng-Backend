.PHONY: help db-build db-up db-down db-restart db-logs db-clean restore build run migrate

help:
	@echo "Available commands:"
	@echo "  make db-build     - Build PostgreSQL Docker image"
	@echo "  make db-up        - Start PostgreSQL container"
	@echo "  make db-down      - Stop PostgreSQL container"
	@echo "  make db-restart   - Restart PostgreSQL container"
	@echo "  make db-logs      - Show PostgreSQL logs"
	@echo "  make db-clean     - Remove PostgreSQL container and volume"
	@echo "  make restore      - Restore NuGet packages"
	@echo "  make build        - Build the solution"
	@echo "  make run          - Run the API"
	@echo "  make migrate      - Run database migrations"

# Docker PostgreSQL commands
db-build:
	docker build -t eng-backend-postgres -f Dockerfile.postgres .

db-up:
	docker run -d \
		--name eng_backend_postgres \
		-e POSTGRES_DB=eng_backend_db \
		-e POSTGRES_USER=postgres \
		-e POSTGRES_PASSWORD=postgres \
		-p 5434:5432 \
		-v eng_backend_postgres_data:/var/lib/postgresql/data \
		eng-backend-postgres

db-down:
	docker stop eng_backend_postgres
	docker rm eng_backend_postgres

db-restart:
	make db-down
	make db-up

db-logs:
	docker logs -f eng_backend_postgres

db-clean:
	docker stop eng_backend_postgres || true
	docker rm eng_backend_postgres || true
	docker volume rm eng_backend_postgres_data || true

# .NET commands
restore:
	dotnet restore

build:
	dotnet build

run:
	dotnet run --project Eng-BackendAPI/Eng-BackendAPI.csproj

migrate:
	dotnet ef database update --project Eng-Backend.DAL/Eng-Backend.DAL.csproj --startup-project Eng-BackendAPI/Eng-BackendAPI.csproj
