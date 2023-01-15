using System.Collections.Generic;

namespace ProductManagement.Domain.ValueObjects
{
    public class SupplierData : ValueObject
    {
        public SupplierData(int supplierCode, string supplierDescription, string supplierCnpj)
        {
            SupplierCode = supplierCode;
            SupplierDescription = supplierDescription;
            SupplierCnpj = supplierCnpj;
        }

        public int SupplierCode { get; init; }
        public string SupplierDescription { get; init; }
        public string SupplierCnpj { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SupplierCode;
            yield return SupplierDescription;
            yield return SupplierCnpj;
        }
    }
}
