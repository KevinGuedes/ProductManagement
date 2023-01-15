using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Application.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(updateProductDto => updateProductDto.Code)
                .GreaterThan(0);

            RuleFor(updateProductDto => updateProductDto.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(updateProductDto => updateProductDto.ManufacturingDate)
                .Must((updateProductDto, manufacturingDate) => manufacturingDate < updateProductDto.ExpirationDate)
                .WithMessage("Manufacturing Date must not be equal or higher than the Expiration Date");
        }
    }
}
