using Bogus;
using Bogus.Extensions.Brazil;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;

namespace ProductManagement.TestUtils
{
    public static class ProductDataFaker
    {

        public static Product GetFakeProduct(Faker faker)
        {
            return new Product(
                faker.Random.Int(1, 1000),
                faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow,
                faker.Random.Int(1, 1000),
                faker.Lorem.Sentences(),
                faker.Company.Cnpj());
        }

        public static CreateProductDto GetFakeCreateProductDto(Faker faker)
        {
            return new CreateProductDto(
                faker.Random.Int(1, 1000),
                faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow,
                faker.Random.Int(1, 1000),
                faker.Lorem.Sentences(),
                faker.Company.Cnpj());
        }

        public static UpdateProductDto GetFakeUpdateProductDto(Faker faker)
        {
            return new UpdateProductDto(
                faker.Random.Int(1, 1000),
                faker.Random.Int(1, 1000),
                faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow,
                faker.Random.Int(1, 1000),
                faker.Lorem.Sentences(),
                faker.Company.Cnpj());
        }
    }
}