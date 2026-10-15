using FluentValidation;
using Products.Application.Features.Products.Commands;


namespace Products.Application.Validators
{
    public class UpdateProductCommandValidator
    : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0);
        }
    }
}
