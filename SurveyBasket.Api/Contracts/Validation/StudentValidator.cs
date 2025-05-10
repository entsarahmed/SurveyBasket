namespace SurveyBasket.Api.Contracts.Validation;

public class StudentValidator: AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.DateOfBirth)
            .Must(BeMoreThan18Years)
            .When(x => x.DateOfBirth.HasValue)
            .WithMessage("{PropertyName} is invalid, age should be 18 Years at least");
        
    }
    private bool BeMoreThan18Years(DateTime? dateOfBirth)
    {
        return DateTime.Today > dateOfBirth!.Value.AddYears(18);
    }
}
