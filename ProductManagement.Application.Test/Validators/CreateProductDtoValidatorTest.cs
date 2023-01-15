using FluentValidation.TestHelper;

namespace ProductManagement.Application.Test.Validators
{
    public class CreateProductDtoValidatorTest
    {
        private readonly CreateProductDtoValidator _sut;
        private readonly ProductDataFaker _productDataFaker;

        public CreateProductDtoValidatorTest()
        {
            _productDataFaker = new ProductDataFaker();
            _sut = new CreateProductDtoValidator();
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenManufacturingDateIsHigherThanExpirationDate()
        {
            var invalidCreateProductDto = _productDataFaker.GetCreateProductDto();
            invalidCreateProductDto.ManufacturingDate = invalidCreateProductDto.ExpirationDate.AddMinutes(1);

            var result = await _sut.TestValidateAsync(invalidCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenManufacturingDateIsEqualToExpirationDate()
        {
            var invalidCreateProductDto = _productDataFaker.GetCreateProductDto();
            invalidCreateProductDto.ManufacturingDate = invalidCreateProductDto.ExpirationDate;

            var result = await _sut.TestValidateAsync(invalidCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldNotHaveValidationErrorWhenManufacturingDateIsLowerThanExpirationDate()
        {
            var validCreateProductDto = _productDataFaker.GetCreateProductDto();
            validCreateProductDto.ManufacturingDate = validCreateProductDto.ExpirationDate.AddMinutes(-3);

            var result = await _sut.TestValidateAsync(validCreateProductDto, default);

            result.ShouldNotHaveValidationErrorFor(createProductDto => createProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenCodeIsZero()
        {
            var validCreateProductDto = _productDataFaker.GetCreateProductDto();
            validCreateProductDto.Code = 0;

            var result = await _sut.TestValidateAsync(validCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.Code);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenDescriptionIsNull()
        {
            var invalidCreateProductDto = _productDataFaker.GetCreateProductDto();
            invalidCreateProductDto.Description = null;

            var result = await _sut.TestValidateAsync(invalidCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.Description);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenDescriptionIsEmpty()
        {
            var invalidCreateProductDto = _productDataFaker.GetCreateProductDto();
            invalidCreateProductDto.Description = string.Empty;

            var result = await _sut.TestValidateAsync(invalidCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.Description);
        }
    }
}
