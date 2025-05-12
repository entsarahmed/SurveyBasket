namespace SurveyBasket.Api.Services;

public class PollService(ApplicationDbContext context) : IPollService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Poll>> GetAllAsync()  => 
      await _context.Polls.AsNoTracking().ToListAsync();

    public async  Task<Poll?> GetAsync(int id) => 
        await _context.Polls.FindAsync(id);

    public async Task<Poll> AddAsync(Poll poll)
    {
       await _context.AddAsync(poll);
        await _context.SaveChangesAsync();
        return poll;
    }

    //public bool Update(int id, Poll poll)
    //{
    //    var CurrentPoll = Get(id);
    //    if(CurrentPoll is null)
    //        return false;
    //    CurrentPoll.Title = poll.Title;
    //    CurrentPoll.Summary = poll.Summary;
    //    return true;
    //}

    //public bool Delete(int id)
    //{
    //    var poll = Get(id);
    //    if (poll is null) return false;
    //    _polls.Remove(poll);
    //    return true;

    //}

}
