using MapsterMapper;

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
        var response = polls.Adapt<IEnumerable<Poll>>();
        return Ok(response);
    }
    [HttpGet("{Id}")]
    public IActionResult Get([FromRoute]int id)
    {
        var poll = _pollService.Get(id);
        if(poll is null)
            return NotFound();
     
         var response = poll.Adapt<PollResponse>();
     
        return Ok(response);
    }

    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest request)
    {
        var newPoll = _pollService.Add(request.Adapt<Poll>());
        return CreatedAtAction(nameof(Get), new {id = newPoll.Id}, newPoll);


    }
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody]CreatePollRequest request)
    {
        var IsUpdated = _pollService.Update(id, request.Adapt<Poll>());
        if (!IsUpdated)
            return NotFound();
        return NoContent();

    }
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute]int id)
    {
        var ISDeleted  = _pollService.Delete(id);
        if (!ISDeleted)
            return NotFound();
        return NoContent();
    }
    [HttpGet("test")]
    public IActionResult Test()
    {
        var student = new Student
        {
            Id = 1,
            FirstName = "Mohamed",
            MiddleName = "Ali",
            LastName = "Elmelaty",
            DateOfBirth = new DateTime(1999, 1, 1),
            Department = new Department
            {
                Id = 1,
                Name = "Test",
            }
        };
        var response = student.Adapt<StudentResponse>();
        return Ok(response);
    }
   
}
