using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {

            RuleFor(p => p.Name).
                MaximumLength(100).WithMessage("Product Should be less than 100 characters").
                NotEmpty().WithMessage("Product Name is required");

            RuleFor(p => p.Price).
                GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(p => p.Description).
                NotEmpty().WithMessage("Description is required");
        }

    }
}
