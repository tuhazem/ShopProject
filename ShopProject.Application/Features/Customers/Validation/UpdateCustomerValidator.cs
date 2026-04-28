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
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly IUnitOfWork uow;

        public UpdateCustomerValidator(IUnitOfWork uow)
        {
            this.uow = uow;


            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Customer Id is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer Name is required")
                .MaximumLength(100).WithMessage("Customer Name should be less than 100 characters");

            RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .MustAsync(async (model, phone, ct) =>
                await BeUniquePhone(model.Id, phone, ct))
            .WithMessage("This Phone Is Already Token");
        }

        private async Task<bool> BeUniquePhone(int id, string phone, CancellationToken ct)
        {
            var customer = await uow.Customers.GetAllAsync();
            return !customer.Any(c => c.Phone == phone && c.Id != id);

        }
    }
}
