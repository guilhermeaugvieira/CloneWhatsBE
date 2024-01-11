using CloneWhatsBE.Users;
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

        [HttpPut("{userId:guid}/image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateImage(Guid userId, [FromForm] IFormFile file)
        {
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var userImage = UserFakeDb.UserImages.FirstOrDefault(userImage => userImage.UserId == userId);

            if (userImage is null)
                UserFakeDb.UserImages
                    .Add(new UserImage(userId, memoryStream.ToArray()));
            else
                userImage.UpdateImage(memoryStream.ToArray());

            return Ok();
        }

        [HttpGet("{userId:guid}/image")]
        [ProducesResponseType<IFormFile>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserImage(Guid userId)
        {
            var userImage = UserFakeDb.UserImages.FirstOrDefault(userImage => userImage.UserId == userId);

            return userImage == null ? NotFound() : File(userImage.Image, "image/png");
        }
    }
}
