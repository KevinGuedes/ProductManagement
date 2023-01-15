using FluentValidation.TestHelper;

namespace ProductManagement.Application.Test.Validators
{
    public class CreateProductDtoValidatorTest
    {
        private readonly CreateProductDtoValidator _sut;
        private readonly Faker _faker;

        public CreateProductDtoValidatorTest()
        {
            _faker = new Faker();
            _sut = new CreateProductDtoValidator();
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenManufacturingDateIsHigherThanExpirationDate()
        {
            var invalidCreateProductDto = ProductDataFaker.GetFakeCreateProductDto(_faker);
            invalidCreateProductDto.ManufacturingDate= invalidCreateProductDto.ExpirationDate.AddMinutes(1);

            var result = await _sut.TestValidateAsync(invalidCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenManufacturingDateIsEqualToExpirationDate()
        {
            var invalidCreateProductDto = ProductDataFaker.GetFakeCreateProductDto(_faker);
            invalidCreateProductDto.ManufacturingDate = invalidCreateProductDto.ExpirationDate;

            var result = await _sut.TestValidateAsync(invalidCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldNotHaveValidationErrorWhenManufacturingDateIsLowerThanExpirationDate()
        {
            var validCreateProductDto = ProductDataFaker.GetFakeCreateProductDto(_faker);
            validCreateProductDto.ManufacturingDate = validCreateProductDto.ExpirationDate.AddMinutes(-3);

            var result = await _sut.TestValidateAsync(validCreateProductDto, default);

            result.ShouldNotHaveValidationErrorFor(createProductDto => createProductDto.ManufacturingDate);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenCodeIsZero()
        {
            var validCreateProductDto = ProductDataFaker.GetFakeCreateProductDto(_faker);
            validCreateProductDto.Code = 0;

            var result = await _sut.TestValidateAsync(validCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.Code);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenDescriptionIsNull()
        {
            var invalidCreateProductDto = ProductDataFaker.GetFakeCreateProductDto(_faker);
            invalidCreateProductDto.Description = null;

            var result = await _sut.TestValidateAsync(invalidCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.Description);
        }

        [Fact]
        public async Task ShouldHaveValidationErrorWhenDescriptionIsEmpty()
        {
            var invalidCreateProductDto = ProductDataFaker.GetFakeCreateProductDto(_faker);
            invalidCreateProductDto.Description = string.Empty;

            var result = await _sut.TestValidateAsync(invalidCreateProductDto, default);

            result.ShouldHaveValidationErrorFor(createProductDto => createProductDto.Description);
        }
    }
}
