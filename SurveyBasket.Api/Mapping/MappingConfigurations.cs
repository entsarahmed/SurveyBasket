namespace SurveyBasket.Api.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Poll, PollResponse>()
            .Map(dest => dest.Notes, src => src.Description);


        config.NewConfig<Student, StudentResponse>()
            .Map(dest => dest.FullName, src => $"{src.FirstName} {src.MiddleName} {src.LastName}")
            .Map(dest => dest.Age, src => DateTime.Now.Year - src.DateOfBirth!.Value.Year,
            srcCond => srcCond.DateOfBirth.HasValue)
            //.Ignore(dest => dest.DepartmentName)
            .Map(dest => dest.DepartmentName, src => src.Department.Name);
 //       config.NewConfig<Student, StudentResponse>().TwoWays(); //Reverse Mapping
    }
}
