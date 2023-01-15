using ProductManagement.Domain.Enums;
using ProductManagement.Domain.ValueObjects;
using System;

namespace ProductManagement.Domain.Entities
{
    public class Product : AggregateRoot
    {
        public int Code { get; private set; }
        public string Description { get; private set; }
        public ProductStatus Status { get; private set; }
        public DateTime ManufacturingDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public SupplierData SupplierData { get; private set; }

        public Product(
            int code, 
            string description, 
            ProductStatus status, 
            DateTime manufacturingDate,
            DateTime expirationDate, 
            SupplierData supplierData)
        {
            Code = code;
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate;
            ExpirationDate = expirationDate;
            SupplierData = supplierData;
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
            SupplierData supplierData)
        {
            Code = code;
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate;
            ExpirationDate = expirationDate;
            SupplierData = supplierData;
            SetUpdateDate();
        }
    }
}