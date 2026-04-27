using FluentValidation;
using ShopProject.Application.Features.Customers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Application.Features.Customers.Validation
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer Name is required")
                .MaximumLength(100).WithMessage("Customer Name should be less than 100 characters");

            RuleFor(c => c.Email)
                .EmailAddress().When(x=> !string.IsNullOrEmpty(x.Email)).WithMessage("Invalid email format");
            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Customer Phone is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format");
        }
    }
}
