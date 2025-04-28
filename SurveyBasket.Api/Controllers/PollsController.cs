namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]// /api/polls
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok(_pollService.GetAll());
    }
    [HttpGet("{Id:int}")]
    public IActionResult Get(int id)
    {
        var poll = _pollService.Get(id);

        return poll is null ? NotFound() : Ok(poll);
    }

    [HttpPost("")]
    public IActionResult Add(Poll reqest)
    {
        var newPoll = _pollService.Add(reqest);
        return CreatedAtAction(nameof(Get), new {id = newPoll.Id}, newPoll);

    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, Poll request)
    {
      var IsUpdated =  _pollService.Update(id, request);
        if (!IsUpdated)
            return NotFound();
        return NoContent(); //204
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var ISDeleted  = _pollService.Delete(id);
        if (!ISDeleted)
            return NotFound();
        return NoContent();
    }
}
