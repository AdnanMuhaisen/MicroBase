using FluentValidation;

namespace MicroBase.PlatformService.Api.Commands;

public class CreatePlatformCommand
{
    public string Name { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public decimal Cost { get; set; }
}

public class CreatePlatformCommandValidator : AbstractValidator<CreatePlatformCommand>
{
    public CreatePlatformCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Publisher)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Cost)
            .NotEmpty();
    }
}