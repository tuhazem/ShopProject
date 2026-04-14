using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IApplicationDbContext context;

        public CreateProductCommandValidator(IApplicationDbContext context)
        {

            RuleFor(p => p.Name).
                MaximumLength(100).WithMessage("Product Should be less than 100 characters").
                NotEmpty().WithMessage("Product Name is required");

            RuleFor(p => p.Price).
                GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(p => p.Description).
                NotEmpty().WithMessage("Description is required");

            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("Category Id is required")
                .MustAsync(CategoryExists).WithMessage("Category Id is not valid");

            this.context = context;
        }

        private async Task<bool> CategoryExists(int categoryid, CancellationToken cancellationToken)
        { 
            return await context.Categories.AnyAsync(c => c.Id == categoryid, cancellationToken);


        }

    }
}
