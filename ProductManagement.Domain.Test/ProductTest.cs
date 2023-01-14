using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;

namespace ProductManagement.Domain.Test
{
    public class ProductTest
    {
        [Fact]
        public void ShouldSetANewStatusWhenUpdatingStatus()
        {
            var product = new Product(123, "Nice product", ProductStatus.Active, DateTime.UtcNow)
        }
    }
}