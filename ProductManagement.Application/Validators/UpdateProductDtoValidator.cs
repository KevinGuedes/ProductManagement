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

            RuleFor(productDto => productDto.Code)
                .NotNull();

            RuleFor(productDto => productDto.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(productDto => productDto.ManufacturingDate)
                .Must((productDto, manufacturingDate) => manufacturingDate < productDto.ExpirationDate)
                .WithMessage("Manufacturing Date must not be equal or higher than the Expiration Date");

            RuleFor(product => product.Id)
                .MustAsync(async (id, cancellationToken) => {
                    var existingProduct = await productRepository.GetByIdAsync(id, cancellationToken);
                    return existingProduct is not null;
                }).WithMessage("Product not found");
        }
    }
}
