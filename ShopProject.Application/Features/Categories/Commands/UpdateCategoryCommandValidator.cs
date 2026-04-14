using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
             
                RuleFor(x => x.Id)
                    .GreaterThan(0).WithMessage("Category Id must be greater than 0.");
    
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Category name is required.")
                    .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
        }
    }
}
