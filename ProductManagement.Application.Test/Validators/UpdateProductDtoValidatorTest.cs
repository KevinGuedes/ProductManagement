using FluentValidation.TestHelper;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Application.Test.Validators
{
    public class UpdateProductDtoValidatorTest
    {
        private readonly UpdateProductDtoValidator _sut;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Faker _faker;

        public UpdateProductDtoValidatorTest()
        {
            _faker= new Faker();
            _productRepository = new Mock<IProductRepository>();
            _sut = new UpdateProductDtoValidator(_productRepository.Object);
        }


        [Fact]
        public async Task ShouldHaveValidationErrorWhenManufacturingDateIsHigherThanExpirationDate()
        {
            var invalidUpdateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            invalidUpdateProductDto.ManufacturingDate = invalidUpdateProductDto.ExpirationDate.AddMinutes(1);

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenManufacturingDateIsEqualToExpirationDate()
        {
            var invalidUpdateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            invalidUpdateProductDto.ManufacturingDate = invalidUpdateProductDto.ExpirationDate;

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldNotHaveValidationErrorWhenManufacturingDateIsLowerThanExpirationDate()
        {
            var validUpdateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            validUpdateProductDto.ManufacturingDate = validUpdateProductDto.ExpirationDate.AddMinutes(-3);

            var result = await _sut.TestValidateAsync(validUpdateProductDto, default);

            result.ShouldNotHaveValidationErrorFor(updateProductDto => updateProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenCodeIsZero()
        {
            var invalidUpdateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            invalidUpdateProductDto.Code = 0;

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.Code);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenDescriptionIsNull()
        {
            var invalidUpdateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            invalidUpdateProductDto.Description = null;

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.Description);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenDescriptionIsEmpty()
        {
            var invalidUpdateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            invalidUpdateProductDto.Description = string.Empty;

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.Description);
        }

        [Fact]
        public async Task ShouldNotHaveValidationErrorWhenProductExistsOnDatabase()
        {
            var validUpdateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(ProductDataFaker.GetFakeProduct(_faker));

            var result = await _sut.TestValidateAsync(validUpdateProductDto, default);

            result.ShouldNotHaveValidationErrorFor(updateProductDto => updateProductDto.Id);
        }

        [Fact]
        public async Task ShouldNHaveValidationErrorWhenProductDoesNotExistOnDatabase()
        {
            var validUpdateProductDto = ProductDataFaker.GetFakeUpdateProductDto(_faker);
            _productRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>(), default).Result)
                .Returns(null as Product);

            var result = await _sut.TestValidateAsync(validUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.Id);
        }
    }
}
