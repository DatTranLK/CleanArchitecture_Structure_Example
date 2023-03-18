using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFStore.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(p => p.ProductId)
                .NotEmpty().WithMessage("{ProductId} is required.")
                .NotNull()
                .Must(num => num > 0).WithMessage("The ProductId must greater than 0");
            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("{CategoryId} is required.")
                .NotNull()
                .Must(num => num > 0).WithMessage("The CategoryId must greater than 0");
            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("{ProductName} is required.")
                .NotNull();
            RuleFor(p => p.Weight)
                .NotEmpty().WithMessage("{Weight} is required.")
                .NotNull();
            RuleFor(p => p.UnitPrice)
                .NotEmpty().WithMessage("{UnitPrice} is required.")
                .NotNull();
            RuleFor(p => p.UnitsInStock)
                .NotEmpty().WithMessage("{UnitsInStock} is required.")
                .NotNull()
                .Must(num => num > 0).WithMessage("The UnitsInStock must greater than 0");
        }
    }
}
