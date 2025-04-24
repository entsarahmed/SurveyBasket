namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]// /api/polls
[ApiController]
public class PollsController : ControllerBase
{
    private readonly List<Poll> _polls = [
        new Poll{
            Id = 1,
            Title = "Poll 1",
            Description = "My first poll"
        }
        ];

    
    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok(_polls);
    }
    [HttpGet("{Id}")]
    public IActionResult Get(int id)
    {
        var poll = _polls.SingleOrDefault(x => x.Id == id);

        return poll is null ? NotFound() : Ok(poll);
    }

}
