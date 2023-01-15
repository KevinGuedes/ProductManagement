using Bogus.Extensions.Brazil;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.ValueObjects;
using ProductManagement.TestUtils;

namespace ProductManagement.Domain.Test.Entities
{
    public class ProductTest
    {
        private readonly Faker _faker;
        private readonly ProductDataFaker _productDataFaker;

        public ProductTest()
        {
            _faker = new Faker();
            _productDataFaker = new ProductDataFaker();
        }

        [Fact]
        public void ShouldSetANewStatusWhenStatusIsUpdated()
        {
            var newStatus = ProductStatus.Inactive;
            var product = _productDataFaker.GetProduct();

            product.UpdateStatus(newStatus);

            product.Status.Should().Be(newStatus);
        }

        [Fact]
        public void UpdateDateShouldNotBeNullWhenStatusIsUpdated()
        {
            var newStatus = ProductStatus.Inactive;
            var product = _productDataFaker.GetProduct();

            product.UpdateStatus(newStatus);

            product.UpdateDate.Should().NotBeNull();
        }

        [Fact]
        public void UpdateDateShouldNotBeNullWhenProductIsEntirelyUpdated()
        {
            var product = _productDataFaker.GetProduct();

            var supplierData = new SupplierData(_faker.Random.Int(0, 1000), _faker.Lorem.Sentences(), _faker.Company.Cnpj());
            product.Update(
                _faker.Random.Int(0, 1000),
                _faker.Lorem.Sentences(),
                ProductStatus.Active,
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow,
                supplierData);

            product.UpdateDate.Should().NotBeNull();
        }
    }
}
