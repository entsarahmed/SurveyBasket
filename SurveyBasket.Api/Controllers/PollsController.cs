

using SurveyBasket.Api.Contracts.Requests;

namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]// /api/polls
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var polls =_pollService.GetAll();
        return Ok(polls.MapToResponse());
    }
    [HttpGet("{Id}")]
    public IActionResult Get([FromRoute]int id)
    {
        var poll = _pollService.Get(id);

        return poll is null ? NotFound() : Ok(poll.MapToResponse());
    }

    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest request)
    {
        var newPoll = _pollService.Add(request.MapToPoll());
        return CreatedAtAction(nameof(Get), new {id = newPoll.Id}, newPoll);

    }
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody]CreatePollRequest request)
    {
      var IsUpdated =  _pollService.Update(id, request.MapToPoll());
        if (!IsUpdated)
            return NotFound();
        return NoContent(); //204
    }
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute]int id)
    {
        var ISDeleted  = _pollService.Delete(id);
        if (!ISDeleted)
            return NotFound();
        return NoContent();
    }
   
}
