namespace SurveyBasket.Api.Services;

public class PollService(ApplicationDbContext context) : IPollService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default)  => 
      await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);

    public async  Task<Poll?> GetAsync(int id, CancellationToken cancellationToken  = default) => 
        await _context.Polls.FindAsync(id,cancellationToken);

    public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
       await _context.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);// If it happen problem, stop Request
        return poll;
    }

    public async Task<bool> UpdateAsync(int id, Poll poll,CancellationToken cancellationToken=default)
    {
        var CurrentPoll = await GetAsync(id, cancellationToken);
        if (CurrentPoll is null)
            return false;
        CurrentPoll.Title = poll.Title;
        CurrentPoll.Summary = poll.Summary;
        CurrentPoll.StartsAt = poll.StartsAt;
        CurrentPoll.EndsAt = poll.EndsAt;
        await _context.SaveChangesAsync(cancellationToken); 
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await GetAsync(id,cancellationToken);
        if (poll is null) return false;
        _context.Remove(poll);
        await _context.SaveChangesAsync(cancellationToken);
        return true;

    }

}
