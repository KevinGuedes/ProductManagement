using AutoMapper;
using Bogus.Extensions.Brazil;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Products;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Application.Test.Services
{
    public class ProductServiceTest
    {
        private readonly ProductService _sut;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Faker _faker;

        public ProductServiceTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _productRepository = new Mock<IProductRepository>();
            _mapper = new Mock<IMapper>();
            _faker = new Faker();

            _sut = new ProductService(_mapper.Object, _productRepository.Object, _unitOfWork.Object);
        }

        [Fact]
        public async Task ShouldReturnProductDataWhenSearchingByCode()
        {
            _mapper.Setup(mapper => mapper.Map<ProductDto>(It.IsAny<Product>())).Returns(new ProductDto());
            
            await _sut.GetProductByCodeAsync(1, default);

            _productRepository.Verify(x => x.GetProductByCodeAsync(1, default));
        }

        [Fact]
        public async Task ShouldUpdateProductDataWhenUpdatingProduct()
        {
            _mapper
                .Setup(mapper => mapper.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(new ProductDto());
            
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(ProductDataFaker.GetFakeProduct(_faker));

            var updateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);

            await _sut.UpdateProductAsync(updateProductDto, default);

            _productRepository.Verify(x => x.Update(It.Is<Product>(p => 
                p.Code == updateProductDto.Code &&
                p.Description == updateProductDto.Description &&
                p.Status == updateProductDto.Status &&
                p.ManufacturingDate == updateProductDto.ManufacturingDate &&
                p.ExpirationDate == updateProductDto.ExpirationDate &&
                p.SupplierCode == updateProductDto.SupplierCode &&
                p.SupplierDescription == updateProductDto.SupplierDescription &&
                p.SupplierCnpj == updateProductDto.SupplierCnpj
            )));
        }
    }
}