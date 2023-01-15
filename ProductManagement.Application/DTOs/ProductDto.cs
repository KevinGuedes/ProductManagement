using ProductManagement.Domain.Enums;
using System;

namespace ProductManagement.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SupplierDataDto SupplierData { get; set; }
    }
}
