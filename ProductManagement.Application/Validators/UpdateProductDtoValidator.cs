using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Application.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator(IProductRepository productRepository)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(updateProductDto => updateProductDto.Code)
                .GreaterThan(0);

            RuleFor(updateProductDto => updateProductDto.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(updateProductDto => updateProductDto.ManufacturingDate)
                .Must((updateProductDto, manufacturingDate) => manufacturingDate < updateProductDto.ExpirationDate)
                .WithMessage("Manufacturing Date must not be equal or higher than the Expiration Date");

            RuleFor(updateProductDto => updateProductDto.Id)
                .MustAsync(async (id, cancellationToken) => {
                    var existingProduct = await productRepository.GetByIdAsync(id, cancellationToken);
                    return existingProduct is not null;
                }).WithMessage("Product not found");
        }
    }
}
