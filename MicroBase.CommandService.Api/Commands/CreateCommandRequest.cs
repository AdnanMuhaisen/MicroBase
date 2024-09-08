using FluentValidation;

namespace MicroBase.CommandService.Api.Commands;

public class CreateCommandRequest
{
    public string Activity { get; set; } = null!;

    public string CommandLine { get; set; } = null!;
}

public class CreateCommandRequestValidator : AbstractValidator<CreateCommandRequest>
{
    public CreateCommandRequestValidator()
    {
        RuleFor(x => x.Activity)
            .NotEmpty()
            .MaximumLength(400);

        RuleFor(x => x.CommandLine)
            .NotEmpty()
            .MaximumLength(400);
    }
}