using Bogus.Extensions.Brazil;
using ProductManagement.Domain.Enums;
using ProductManagement.TestUtils;

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
            var product = ProductDataFaker.GetFakeProduct(_faker);

            product.UpdateStatus(newStatus);

            product.Status.Should().Be(newStatus);
        }

        [Fact]
        public void UpdateDateShouldNotBeNullWhenStatusIsUpdated()
        {
            var newStatus = ProductStatus.Inactive;
            var product = ProductDataFaker.GetFakeProduct(_faker);

            product.UpdateStatus(newStatus);

            product.UpdateDate.Should().NotBeNull();
        }

        [Fact]
        public void UpdateDateShouldNotBeNullWhenProductIsEntirelyUpdated()
        {
            var product = ProductDataFaker.GetFakeProduct(_faker);

            product.Update(
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow,
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                _faker.Company.Cnpj());

            product.UpdateDate.Should().NotBeNull();
        }
    }
}
