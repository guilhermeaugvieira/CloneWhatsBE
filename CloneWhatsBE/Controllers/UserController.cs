using Microsoft.AspNetCore.Mvc;

namespace CloneWhatsBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType<IEnumerable<User>>(StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            return Ok(UserFakeDb.Users);
        }
    }
}
