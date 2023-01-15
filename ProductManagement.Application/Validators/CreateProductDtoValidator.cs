using FluentValidation;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(productDto => productDto.Code)
                .NotNull();

            RuleFor(productDto => productDto.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(productDto => productDto.ManufacturingDate)
                .Must((productDto, manufacturingDate) => manufacturingDate < productDto.ExpirationDate)
                .WithMessage("Manufacturing Date must not be equal or higher than the Expiration Date");
        }
    }
}
