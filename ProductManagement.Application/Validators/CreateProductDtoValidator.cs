using FluentValidation;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(createProductDto => createProductDto.Code)
                .GreaterThan(0);

            RuleFor(createProductDto => createProductDto.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(createProductDto => createProductDto.ManufacturingDate)
                .Must((createProductDto, manufacturingDate) => manufacturingDate < createProductDto.ExpirationDate)
                .WithMessage("Manufacturing Date must not be equal or higher than the Expiration Date");
        }
    }
}
