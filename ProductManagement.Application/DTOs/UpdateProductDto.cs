using ProductManagement.Domain.Enums;
using System;

namespace ProductManagement.Application.DTOs
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCnpj { get; set; }

        public UpdateProductDto(
            int id, 
            int code,
            string description, 
            ProductStatus status, 
            DateTime manufacturingDate, 
            DateTime expirationDate,
            int supplierCode, 
            string supplierDescription, 
            string supplierCnpj)
        {
            Id = id;
            Code = code;
            Description = description;
            Status = status;
            ManufacturingDate = manufacturingDate;
            ExpirationDate = expirationDate;
            SupplierCode = supplierCode;
            SupplierDescription = supplierDescription;
            SupplierCnpj = supplierCnpj;
        }
    }
}
