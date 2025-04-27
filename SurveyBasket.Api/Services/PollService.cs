
namespace SurveyBasket.Api.Services;

public class PollService : IPollService
{
    private static readonly List<Poll> _polls = [
       new Poll{
            Id = 1,
            Title = "Poll 1",
            Description = "My first poll"
        }
       ];
    public IEnumerable<Poll> GetAll()  => _polls;

    public Poll? Get(int id) => _polls.SingleOrDefault(x => x.Id == id);

    public Poll Add(Poll poll)
    {
        poll.Id = _polls.Count + 1;
       _polls.Add(poll);
        return poll;
    }

    public bool Update(int id, Poll poll)
    {
        var CurrentPoll = Get(id);
        if(CurrentPoll is null)
            return false;
        CurrentPoll.Title = poll.Title;
        CurrentPoll.Description = poll.Description;
        return true;
    }
}
