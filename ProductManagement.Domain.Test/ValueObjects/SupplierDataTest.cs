using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Domain.Test.ValueObjects
{
    public class SupplierDataTest
    {
        public SupplierDataTest()
        {
        }

        [Fact]
        public void ValueObjectsMustBeEqualIfPropertiesAreTheSame()
        {
            var supplierDataA = new SupplierData(1, "description", "2445465");
            var supplierDataB = new SupplierData(1, "description", "2445465");

            var result = supplierDataA == supplierDataB;

            result.Should().BeTrue();
        }

        [Fact]
        public void ValueObjectsMustNotBeEqualIfPropertiesAreTheDifferent()
        {
            var supplierDataA = new SupplierData(1, "different", "2445465");
            var supplierDataB = new SupplierData(1, "description", "2445465");

            var result = supplierDataA == supplierDataB;

            result.Should().BeFalse();
        }
    }
}
