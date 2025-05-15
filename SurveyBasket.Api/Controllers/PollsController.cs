
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
    public async Task<IActionResult> Add([FromBody] PollRequest request,
        CancellationToken cancellationToken = default)
    {

        var newPoll = await _pollService.AddAsync(request.Adapt<Poll>());
        return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
    {
        var IsUpdated =  await _pollService.UpdateAsync(id, request.Adapt<Poll>(), cancellationToken);
        if (!IsUpdated)
            return NotFound();
        return NoContent();

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id,CancellationToken cancellationToken)
    {
        var ISDeleted = await _pollService.DeleteAsync(id, cancellationToken);
        if (!ISDeleted)
            return NotFound();
        return NoContent();
    }


}
