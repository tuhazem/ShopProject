using FluentValidation;
using ShopProject.Application.Common.Interfaces;
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
        private readonly IUnitOfWork uow;

        public CreateCustomerValidator(IUnitOfWork uow)
        {
            this.uow = uow;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer Name is required")

                .MaximumLength(100).WithMessage("Customer Name should be less than 100 characters");

            RuleFor(c => c.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Invalid email format")
                .MustAsync(BeUniqueEmail).WithMessage("This Email is Already Exists");


            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Customer Phone is required")
                .MustAsync(BeUniquePhone).WithMessage("This Phone number is Already Exists")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format");

        }

        public async Task<bool> BeUniquePhone(string phone, CancellationToken ct)
        {

            var customer = await uow.Customers.GetAllAsync();

            return !customer.Any(c => c.Phone == phone);
        }

        public async Task<bool> BeUniqueEmail(string? email, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(email))
                return true;
            var customer = await uow.Customers.GetAllAsync();
            return !customer.Any(c => c.Email == email);
        }
    }
}
