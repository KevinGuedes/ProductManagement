using Bogus;
using Bogus.Extensions.Brazil;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.TestUtils
{
    public class ProductDataFaker
    {
        private readonly Faker _faker;

        public ProductDataFaker()
        {
            _faker = new Faker();
        }

        public Product GetProduct()
        {
            return new Product(
                _faker.Random.Int(1, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow,
                new SupplierData(_faker.Random.Int(1, 1000), _faker.Lorem.Sentences(), _faker.Company.Cnpj()));
        }

        public CreateProductDto GetCreateProductDto()
        {
            return new CreateProductDto(
                _faker.Random.Int(1, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow,
                _faker.Random.Int(1, 1000),
                _faker.Lorem.Sentences(),
                _faker.Company.Cnpj());
        }

        public UpdateProductDto GetUpdateProductDto()
        {
            return new UpdateProductDto(
                _faker.Random.Int(1, 1000),
                _faker.Random.Int(1, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow,
                _faker.Random.Int(1, 1000),
                _faker.Lorem.Sentences(),
                _faker.Company.Cnpj());
        }
    }
}