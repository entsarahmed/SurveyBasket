using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Models;

namespace SurveyBasket.Api.Controllers;
[Route("api/[controller]")]// /api/polls
[ApiController]
public class PollsController : ControllerBase
{
    private readonly List<Poll> _polls = [];
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_polls);
    }
}
