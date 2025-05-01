using MapsterMapper;

namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]// /api/polls
[ApiController]
public class PollsController(IPollService pollService,IMapper mapper) : ControllerBase
{
    private readonly IPollService _pollService = pollService;
    private readonly IMapper _mapper = mapper;

    [HttpGet("")]
    public IActionResult GetAll()
    {
        var polls =_pollService.GetAll();
        return Ok(polls);
    }
    [HttpGet("{Id}")]
    public IActionResult Get([FromRoute]int id)
    {
        var poll = _pollService.Get(id);
        if(poll is null)
            return NotFound();
        //var config = new TypeAdapterConfig();
        //config.NewConfig<Poll, PollResponse>()
        //    .Map(dest => dest.Notes, src => src.Description);
        //var response = poll.Adapt<PollResponse>(config);
        var response = _mapper.Map<PollResponse>(poll);
        return Ok(response);
    }

    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest request)
    {
        //var newPoll = _pollService.Add((Poll)request);
        //return CreatedAtAction(nameof(Get), new {id = newPoll.Id}, newPoll);
        return Ok();

    }
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody]CreatePollRequest request)
    {
      //var IsUpdated =  _pollService.Update(id, (Poll)request);
      //  if (!IsUpdated)
      //      return NotFound();
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
