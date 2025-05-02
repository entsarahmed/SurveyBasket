namespace SurveyBasket.Api.Models;

public class StudentResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public int? Age { get; set; }
  //  [AdaptIgnore]
    public string DepartmentName { get; set; } = string.Empty;
}
