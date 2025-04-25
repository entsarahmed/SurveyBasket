
namespace SurveyBasket.Api.Services;

public class PollService : IPollService
{
    private readonly List<Poll> _polls = [
       new Poll{
            Id = 1,
            Title = "Poll 1",
            Description = "My first poll"
        }
       ];
    public IEnumerable<Poll> GetAll()  => _polls;

    public Poll? Get(int id) => _polls.SingleOrDefault(x => x.Id == id);
    
}
