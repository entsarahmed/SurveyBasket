
using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyBasket.Api.Entities;

namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]// /api/polls
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;


    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var polls =await _pollService.GetAllAsync(cancellationToken);
        var response = polls.Adapt<IEnumerable<Poll>>();
        return Ok(response);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] int id,CancellationToken cancellationToken)
    {
        var poll = await _pollService.GetAsync(id, cancellationToken);
        if (poll is null)
            return NotFound();

        var response = poll.Adapt<PollResponse>();

        return Ok(response);
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] CreatePollRequest request,
        CancellationToken cancellationToken = default)
    {

        var newPoll = await _pollService.AddAsync(request.Adapt<Poll>());
        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
    }
    //[HttpPut("{id}")]
    //public IActionResult Update([FromRoute] int id, [FromBody] CreatePollRequest request)
    //{
    //    var IsUpdated = _pollService.Update(id, request.Adapt<Poll>());
    //    if (!IsUpdated)
    //        return NotFound();
    //    return NoContent();

    //}
    //[HttpDelete("{id}")]
    //public IActionResult Delete([FromRoute]int id)
    //{
    //    var ISDeleted  = _pollService.Delete(id);
    //    if (!ISDeleted)
    //        return NotFound();
    //    return NoContent();
    //}


}
