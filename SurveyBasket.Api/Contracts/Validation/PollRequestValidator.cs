namespace SurveyBasket.Api.Contracts.Validation;

public class PollRequestValidator:AbstractValidator<CreatePollRequest>
{
    public PollRequestValidator()
    {
        RuleFor(x => x.Title)
            .Length(3, 100)
            .WithMessage("Title should be at least {MinLength} and maximum {MaxLength}, You entered [{PropertyValue}]")
            .NotEmpty()
            //.MinimumLength(3)
            //.MaximumLength(100);
            //Add Error Message personal You
            // .WithMessage("Please Add a Title")
            //Add Error Message with Placeholder => https://docs.fluentvalidation.net/en/latest/built-in-validators.html#regular-expression-validator
            .WithMessage("Please add a {PropertyName}");
        RuleFor(x => x.Summary)
            .NotEmpty()
            .Length(3, 1500);
    }
}
