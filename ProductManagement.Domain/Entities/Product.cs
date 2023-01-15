using ProductManagement.Domain.Enums;
using System;

namespace ProductManagement.Domain.Entities
{
    public class Product : Entity
    {
        public int Code { get; private set; }
        public string Description { get; private set; }
        public ProductStatus Status { get; private set; }
        public DateTime ManufacturingDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public int SupplierCode { get; private set; }
        public string SupplierDescription { get; private set; }
        public string SupplierCnpj { get; private set; }

        public Product(
            int code,
            string description,
            ProductStatus status,
            DateTime manufacturingDate,
            DateTime expirationDate,
            int supplierCode,
            string supplierDescription,
            string supplierCnpj)
        {
            Code = code;
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate;
            ExpirationDate = expirationDate;
            SupplierCode = supplierCode;
            SupplierDescription = supplierDescription;
            SupplierCnpj = supplierCnpj;
        }

        protected Product() { }

        public void UpdateStatus(ProductStatus status)
        {
            Status = status;
            SetUpdateDate();
        }

        public void Update(
             int code,
            string description,
            ProductStatus status,
            DateTime manufacturingDate,
            DateTime expirationDate,
            int supplierCode,
            string supplierDescription,
            string supplierCnpj)
        {
            Code = code;
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate;
            ExpirationDate = expirationDate;
            SupplierCode = supplierCode;
            SupplierDescription = supplierDescription;
            SupplierCnpj = supplierCnpj;
            SetUpdateDate();
        }
    }
}