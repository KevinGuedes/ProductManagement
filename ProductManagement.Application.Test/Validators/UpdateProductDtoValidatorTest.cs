using FluentValidation.TestHelper;
using ProductManagement.TestUtils;

namespace ProductManagement.Application.Test.Validators
{
    public class UpdateProductDtoValidatorTest
    {
        private readonly UpdateProductDtoValidator _sut;
        private readonly ProductDataFaker _productDataFaker;

        public UpdateProductDtoValidatorTest()
        {
            _productDataFaker = new ProductDataFaker();
            _sut = new UpdateProductDtoValidator();
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenManufacturingDateIsHigherThanExpirationDate()
        {
            var invalidUpdateProductDto = _productDataFaker.GetUpdateProductDto();
            invalidUpdateProductDto.ManufacturingDate = invalidUpdateProductDto.ExpirationDate.AddMinutes(1);

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenManufacturingDateIsEqualToExpirationDate()
        {
            var invalidUpdateProductDto = _productDataFaker.GetUpdateProductDto();
            invalidUpdateProductDto.ManufacturingDate = invalidUpdateProductDto.ExpirationDate;

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldNotHaveValidationErrorWhenManufacturingDateIsLowerThanExpirationDate()
        {
            var validUpdateProductDto = _productDataFaker.GetUpdateProductDto();
            validUpdateProductDto.ManufacturingDate = validUpdateProductDto.ExpirationDate.AddMinutes(-3);

            var result = await _sut.TestValidateAsync(validUpdateProductDto, default);

            result.ShouldNotHaveValidationErrorFor(updateProductDto => updateProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenCodeIsZero()
        {
            var invalidUpdateProductDto = _productDataFaker.GetUpdateProductDto();
            invalidUpdateProductDto.Code = 0;

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.Code);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenDescriptionIsNull()
        {
            var invalidUpdateProductDto = _productDataFaker.GetUpdateProductDto();
            invalidUpdateProductDto.Description = null;

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.Description);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenDescriptionIsEmpty()
        {
            var invalidUpdateProductDto = _productDataFaker.GetUpdateProductDto();
            invalidUpdateProductDto.Description = string.Empty;

            var result = await _sut.TestValidateAsync(invalidUpdateProductDto, default);

            result.ShouldHaveValidationErrorFor(updateProductDto => updateProductDto.Description);
        }
    }
}
