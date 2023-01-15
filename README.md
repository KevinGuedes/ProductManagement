# Product Management Api

## About

A simple api for product management

## Requirements

### Functional

- The system is able new products. Before creating a product, the system validates the input data to ensure that the manufacturing date is not equal or higher than the expiration date
- The system is able able to list the existing products with paginated resutls. This results can be filtered
- The system is able to retrieve a product through its code
- The system is able to logically delete products
- The system is able to update existing products. Before updating a produt, the system validates the input data to ensure that the manufacturing date is not equal or higher than the expiration date

### Nonfunctional

- The system must use an ORM
- The system must use AutoMapper to map entities to dtos
- The systems must have unit tests for its main components

## Technologies Used:

- C#
- ASP.NET Core 7.0 / ASP.NET Core Web Api
- Entity Framework Core 7.0 (ORM)
- SQL Server
- ASP.NET Core Native DI
- OData Protocol
- Open Api Documentation
- Unit Tests

### Packages

- Swagger
- Fluent Assertion
- Fluent Validation
- Moq
- XUnit
- Bogus
- Microsoft OData

## Architecture

Clean Architecture

## Tools For Development

- Visual Studio
- Azure Data Studio
- Thunder Client (HTTP Client For VSCode)
- Git
