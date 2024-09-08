using FluentValidation;

namespace MicroBase.CommandService.Api.Commands;

public class CreatePlatformCommand
{
    public int ExternalId { get; set; }

    public string Name { get; set; } = null!;
}

public class CreatePlatformCommandValidator : AbstractValidator<CreatePlatformCommand>
{
    public CreatePlatformCommandValidator()
    {
        RuleFor(x => x.ExternalId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(400);
    }
}