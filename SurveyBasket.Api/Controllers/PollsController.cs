namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]// /api/polls
[ApiController]
public class PollsController : ControllerBase
{
   

    
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
