
# Freaky Fashion API

A REST API built with ASP.NET Core Web API, C#, Entity Framework Core and SQL Server.

## Requirements

- .NET 9 SDK
- SQL Server LocalDB

## Getting Started

1. Run `dotnet restore`
2. Run `dotnet ef database update`
3. Run `dotnet run`

## API Endpoints

### Products
- GET /api/products
- GET /api/products/{id}
- GET /api/products?slug={slug}
- POST /api/products
- DELETE /api/products/{id}

### Categories
- GET /api/categories
- GET /api/categories/{id}
- GET /api/categories?slug={slug}
- POST /api/categories
- DELETE /api/categories/{id}

## Features

- Create, retrieve and delete products and categories
- Search by URL slug
- Associate products with categories
- Input validation
- Entity Framework Core with SQL Server
- DTOs for API responses
