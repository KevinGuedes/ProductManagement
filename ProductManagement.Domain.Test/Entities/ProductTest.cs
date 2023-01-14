using Bogus.Extensions.Brazil;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;

namespace ProductManagement.Domain.Test.Entities
{
    public class ProductTest
    {
        private readonly Faker _faker;

        public ProductTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void ShouldSetANewStatusWhenStatusIsUpdated()
        {
            var newStatus = ProductStatus.Inactive;
            var product = new Product(
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow.AddDays(-3),
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                _faker.Company.Cnpj());

            product.UpdateStatus(newStatus);

            product.Status.Should().Be(newStatus);
        }

        [Fact]
        public void UpdateDateShouldNotBeNullWhenStatusIsUpdated()
        {
            var newStatus = ProductStatus.Inactive;
            var product = new Product(
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow.AddDays(-3),
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                _faker.Company.Cnpj());

            product.UpdateStatus(newStatus);

            product.UpdateDate.Should().NotBeNull();
        }

        [Fact]
        public void UpdateDateShouldNotBeNullWhenProductIsEntirelyUpdated()
        {
            var product = new Product(
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow.AddDays(-3),
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                _faker.Company.Cnpj());

            product.Update(
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow.AddDays(-3),
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                _faker.Company.Cnpj());

            product.UpdateDate.Should().NotBeNull();
        }
    }
}