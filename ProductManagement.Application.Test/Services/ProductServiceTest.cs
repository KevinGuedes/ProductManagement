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
        private readonly ProductDataFaker _productDataFaker;

        public ProductServiceTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _productRepository = new Mock<IProductRepository>();
            _mapper = new Mock<IMapper>();
            _productDataFaker = new ProductDataFaker();

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

            _productRepository.Verify(productRepository => productRepository.GetByCodeAsync(code, default));
        }

        [Fact]
        public async Task ShouldReturnFailureWhenTryingToGetByCodeAndProductDoesNotExist()
        {
            _productRepository
             .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
             .Returns(null as Product);

            var result = await _sut.GetProductByCodeAsync(1, default);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenProductIsSuccessfullyRetrievedByCode()
        {
            _productRepository
              .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
              .Returns(_productDataFaker.GetProduct());

            var result = await _sut.GetProductByCodeAsync(1, default);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldCallAddFromRepositoryWhenCreatingANewProduct()
        {
            var createProductDto = _productDataFaker.GetCreateProductDto();

            await _sut.CreateProductAsync(createProductDto, default);

            _productRepository.Verify(productRepository => productRepository.AddAsync((It.Is<Product>(p =>
                p.Code == createProductDto.Code &&
                p.Description == createProductDto.Description &&
                p.Status == createProductDto.Status &&
                p.ManufacturingDate == createProductDto.ManufacturingDate &&
                p.ExpirationDate == createProductDto.ExpirationDate &&
                p.SupplierData.SupplierCode == createProductDto.SupplierCode &&
                p.SupplierData.SupplierDescription == createProductDto.SupplierDescription &&
                p.SupplierData.SupplierCnpj == createProductDto.SupplierCnpj
            )), default));
        }

        [Fact]
        public async Task ShouldCallCommitChangesFromUnitOfWorkWhenCreatingANewProduct()
        {
            var createProductDto = _productDataFaker.GetCreateProductDto();

            await _sut.CreateProductAsync(createProductDto, default);

            _unitOfWork.Verify(unitOfWork => unitOfWork.CommitChangesAsync(default));
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenProductIsSuccessfullyCreated()
        {
            var createProductDto = _productDataFaker.GetCreateProductDto();
            _productRepository
               .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
               .Returns(null as Product);

            var result = await _sut.CreateProductAsync(createProductDto, default);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFailureWhenThereIsAProductWithTheSameCode()
        {
            var createProductDto = _productDataFaker.GetCreateProductDto();
            var existingProduct = _productDataFaker.GetProduct();
            createProductDto.Code = existingProduct.Code;

            _productRepository
               .Setup(repository => repository.GetByCodeAsync(createProductDto.Code, default).Result)
               .Returns(existingProduct);

            var result = await _sut.CreateProductAsync(createProductDto, default);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldGetExistingProductFromDatabaseWhenDeletingAProduct()
        {
            var code = 3;
            _productRepository
               .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
               .Returns(_productDataFaker.GetProduct());

            await _sut.DeleteProductByCodeAsync(code, default);

            _productRepository.Verify(productRepository => productRepository.GetByCodeAsync(code, default));
        }

        [Fact]
        public async Task ShouldReturnFailureWhenTryingToDeleteByCodeAndProductDoesNotExist()
        {
            _productRepository
             .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
             .Returns(null as Product);

            var result = await _sut.DeleteProductByCodeAsync(1, default);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenProductIsSuccessfullyDeletedByCode()
        {
            _productRepository
              .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
              .Returns(_productDataFaker.GetProduct());

            var result = await _sut.DeleteProductByCodeAsync(1, default);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldCallUpdateFromRepositoryWhenDeletingAProduct()
        {
            _productRepository
                .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                .Returns(_productDataFaker.GetProduct());

            await _sut.DeleteProductByCodeAsync(3, default);

            _productRepository
                .Verify(productRepository => productRepository.Update(It.Is<Product>(p => p.Status == ProductStatus.Inactive)));
        }

        [Fact]
        public async Task ShouldCallCommitChangesFromUnitOfWorkWhenDeletingAProduct()
        {
            _productRepository
                .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                .Returns(_productDataFaker.GetProduct());

            await _sut.DeleteProductByCodeAsync(3, default);

            _unitOfWork.Verify(unitOfWork => unitOfWork.CommitChangesAsync(default));
        }

        [Fact]
        public async Task ShouldCallGetExistingProductFromRepositoryWhenUpdatingAProduct()
        {
            var updateProductDto = _productDataFaker.GetUpdateProductDto();
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(_productDataFaker.GetProduct());
            _productRepository
                 .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                 .Returns(null as Product);

            await _sut.UpdateProductAsync(updateProductDto, default);

            _productRepository
                .Verify(productRepository => productRepository.GetByIdAsync(It.Is<int>(id => id == updateProductDto.Id), default));
        }

        [Fact]
        public async Task ShouldCallUpdateFromRepositoryWithUpdatedDataWhenUpdatingAProduct()
        {
            var updateProductDto = _productDataFaker.GetUpdateProductDto();
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(_productDataFaker.GetProduct());
            _productRepository
                 .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                 .Returns(null as Product);

            await _sut.UpdateProductAsync(updateProductDto, default);

            _productRepository.Verify(productRepository => productRepository.Update(It.Is<Product>(p =>
                p.Code == updateProductDto.Code &&
                p.Description == updateProductDto.Description &&
                p.Status == updateProductDto.Status &&
                p.ManufacturingDate == updateProductDto.ManufacturingDate &&
                p.ExpirationDate == updateProductDto.ExpirationDate &&
                p.SupplierData.SupplierCode == updateProductDto.SupplierCode &&
                p.SupplierData.SupplierDescription == updateProductDto.SupplierDescription &&
                p.SupplierData.SupplierCnpj == updateProductDto.SupplierCnpj
            )));
        }

        [Fact]
        public async Task ShouldCallCommitChangesFromUnitOfWorkWhenUpdatingAProduct()
        {
            var updateProductDto = _productDataFaker.GetUpdateProductDto();
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(_productDataFaker.GetProduct());
            _productRepository
                .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                .Returns(null as Product);

            await _sut.UpdateProductAsync(updateProductDto, default);

            _unitOfWork.Verify(unitOfWork => unitOfWork.CommitChangesAsync(default));
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenProductIsSuccessfullyUpdated()
        {
            var updateProductDto = _productDataFaker.GetUpdateProductDto();
            _productRepository
                 .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                 .Returns(null as Product);
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(_productDataFaker.GetProduct());

            var result = await _sut.UpdateProductAsync(updateProductDto, default);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFailureWhenTryingToUpdateProductAndProductThatDoesNotExist()
        {
            var updateProductDto = _productDataFaker.GetUpdateProductDto();
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(null as Product);

            var result = await _sut.UpdateProductAsync(updateProductDto, default);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenAProductWithTheSameCodeHasTheSameId()
        {
            var productWithTheSameId = _productDataFaker.GetProduct();
            var productWithTheSameCode = _productDataFaker.GetProduct();
            var updateProductDto = _productDataFaker.GetUpdateProductDto();
            updateProductDto.Id = productWithTheSameCode.Id;

            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(productWithTheSameId);

            _productRepository
                .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                .Returns(productWithTheSameCode);

            var result = await _sut.UpdateProductAsync(updateProductDto, default);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnFailureWhenAProductWithTheSameCodeHasADifferentId()
        {
            var productWithTheSameId = _productDataFaker.GetProduct();
            var productWithTheSameCode = _productDataFaker.GetProduct();
            var updateProductDto = _productDataFaker.GetUpdateProductDto();
            updateProductDto.Id = productWithTheSameCode.Id + 1;

            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(productWithTheSameId);

            _productRepository
                .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                .Returns(productWithTheSameCode);

            var result = await _sut.UpdateProductAsync(updateProductDto, default);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnSuccessWhenThereIsNoProductWithTheSameCode()
        {
            var product = _productDataFaker.GetProduct();
            var updateProductDto = _productDataFaker.GetUpdateProductDto();

            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(product);

            _productRepository
                .Setup(repository => repository.GetByCodeAsync(It.IsAny<int>(), default).Result)
                .Returns(null as Product);

            var result = await _sut.UpdateProductAsync(updateProductDto, default);

            result.IsSuccess.Should().BeTrue();
        }
    }
}