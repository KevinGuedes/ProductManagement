using FluentValidation;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(updateProductDto => updateProductDto.Code)
                .GreaterThanOrEqualTo(0);

            RuleFor(updateProductDto => updateProductDto.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(updateProductDto => updateProductDto.ManufacturingDate)
                .Must((updateProductDto, manufacturingDate) => manufacturingDate < updateProductDto.ExpirationDate)
                .WithMessage("Manufacturing Date must not be equal or higher than the Expiration Date");
        }
    }
}
