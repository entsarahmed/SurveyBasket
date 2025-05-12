namespace SurveyBasket.Api.Services;

public class PollService(ApplicationDbContext context) : IPollService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default)  => 
      await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);

    public async  Task<Poll?> GetAsync(int id, CancellationToken cancellationToken  = default) => 
        await _context.Polls.FindAsync(id);

    public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
       await _context.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);// If it happen problem, stop Request
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
