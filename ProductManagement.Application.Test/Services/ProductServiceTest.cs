using AutoMapper;
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
        public async Task ShouldCallGetAllFromRepositoryWhenRetrievingAllProducts()
        {
            await _sut.GetAllAsync(default);

            _productRepository.Verify(productRepository => productRepository.GetAllAsync(default));
        }

        [Fact]
        public async Task ShouldCallGetProductByCodeFromRepositoryWhenRetrievingProductByCode()
        {
            var code = 1;

            await _sut.GetProductByCodeAsync(code, default);

            _productRepository.Verify(productRepository => productRepository.GetProductByCodeAsync(code, default));
        }

        [Fact]
        public async Task ShouldReturnFailureWhenTryingToGetByCodeAndProductDoesNotExist()
        {
            _productRepository
             .Setup(repository => repository.GetProductByCodeAsync(It.IsAny<int>(), default).Result)
             .Returns(null as Product);

            var result = await _sut.GetProductByCodeAsync(1, default);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenProductIsSuccessfullyRetrievedByCode()
        {
            _productRepository
              .Setup(repository => repository.GetProductByCodeAsync(It.IsAny<int>(), default).Result)
              .Returns(ProductDataFaker.GetFakeProduct(_faker));

            var result = await _sut.GetProductByCodeAsync(1, default);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldCallAddFromRepositoryWhenCreatingANewProduct()
        {
            var createProductDto = ProductDataFaker.GetFakeCreateProductDto(_faker);

            await _sut.CreateProductAsync(createProductDto, default);

            _productRepository.Verify(productRepository => productRepository.AddAsync((It.Is<Product>(p =>
                p.Code == createProductDto.Code &&
                p.Description == createProductDto.Description &&
                p.Status == createProductDto.Status &&
                p.ManufacturingDate == createProductDto.ManufacturingDate &&
                p.ExpirationDate == createProductDto.ExpirationDate &&
                p.SupplierCode == createProductDto.SupplierCode &&
                p.SupplierDescription == createProductDto.SupplierDescription &&
                p.SupplierCnpj == createProductDto.SupplierCnpj
            )), default));
        }

        [Fact]
        public async Task ShouldCallCommitChangesFromUnitOfWorkWhenCreatingANewProduct()
        {
            var createProductDto = ProductDataFaker.GetFakeCreateProductDto(_faker);

            await _sut.CreateProductAsync(createProductDto, default);

            _unitOfWork.Verify(unitOfWork => unitOfWork.CommitChangesAsync(default));
        }

        [Fact]
        public async Task ShouldGetExistingProductFromDatabaseWhenDeletingAProduct()
        {
            var code = 3;
            _productRepository
               .Setup(repository => repository.GetProductByCodeAsync(It.IsAny<int>(), default).Result)
               .Returns(ProductDataFaker.GetFakeProduct(_faker));

            await _sut.DeleteProductByCodeAsync(code, default);

            _productRepository.Verify(productRepository => productRepository.GetProductByCodeAsync(code, default));
        }

        [Fact]
        public async Task ShouldReturnFailureWhenTryingToDeleteByCodeAndProductDoesNotExist()
        {
            _productRepository
             .Setup(repository => repository.GetProductByCodeAsync(It.IsAny<int>(), default).Result)
             .Returns(null as Product);

            var result = await _sut.DeleteProductByCodeAsync(1, default);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenProductIsSuccessfullyDeletedByCode()
        {
            _productRepository
              .Setup(repository => repository.GetProductByCodeAsync(It.IsAny<int>(), default).Result)
              .Returns(ProductDataFaker.GetFakeProduct(_faker));

            var result = await _sut.DeleteProductByCodeAsync(1, default);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldCallUpdateFromRepositoryWhenDeletingAProduct()
        {
            _productRepository
                .Setup(repository => repository.GetProductByCodeAsync(It.IsAny<int>(), default).Result)
                .Returns(ProductDataFaker.GetFakeProduct(_faker));

            await _sut.DeleteProductByCodeAsync(3, default);

            _productRepository
                .Verify(productRepository => productRepository.Update(It.Is<Product>(p => p.Status == ProductStatus.Inactive)));
        }

        [Fact]
        public async Task ShouldCallCommitChangesFromUnitOfWorkWhenDeletingAProduct()
        {
            _productRepository
                .Setup(repository => repository.GetProductByCodeAsync(It.IsAny<int>(), default).Result)
                .Returns(ProductDataFaker.GetFakeProduct(_faker));

            await _sut.DeleteProductByCodeAsync(3, default);

            _unitOfWork.Verify(unitOfWork => unitOfWork.CommitChangesAsync(default));
        }

        [Fact]
        public async Task ShouldCallGetExistingProductFromRepositoryWhenUpdatingAProduct()
        {
            var updateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(ProductDataFaker.GetFakeProduct(_faker));

            await _sut.UpdateProductAsync(updateProductDto, default);

            _productRepository
              .Verify(productRepository => productRepository.GetByIdAsync(It.Is<int>(id => id == updateProductDto.Id), default));
        }

        [Fact]
        public async Task ShouldCallUpdateFromRepositoryWithUpdatedDataWhenUpdatingAProduct()
        {
            var updateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(ProductDataFaker.GetFakeProduct(_faker));

            await _sut.UpdateProductAsync(updateProductDto, default);

            _productRepository.Verify(productRepository => productRepository.Update(It.Is<Product>(p => 
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

        [Fact]
        public async Task ShouldCallCommitChangesFromUnitOfWorkWhenUpdatingAProduct()
        {
            var updateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(ProductDataFaker.GetFakeProduct(_faker));

            await _sut.UpdateProductAsync(updateProductDto, default);

            _unitOfWork.Verify(unitOfWork => unitOfWork.CommitChangesAsync(default));
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenProductIsSuccessfullyUpdated()
        {
            var updateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(ProductDataFaker.GetFakeProduct(_faker));

            var result = await _sut.UpdateProductAsync(updateProductDto, default);

            result.IsSuccess.Should().BeTrue(); 
        }

        [Fact]
        public async Task ShouldReturnFailureWhenTryingToUpdateProductAndProductDoesNotExist()
        {
            var updateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(null as Product);

            var result = await _sut.UpdateProductAsync(updateProductDto, default);

            result.IsSuccess.Should().BeFalse();
        }
    }
}