using gameForMood.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gameForMood.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class Main(IMainService mainService) : ControllerBase
    {
        private readonly IMainService _mainService = mainService;

        [HttpGet("")]
        public async Task<IActionResult> GetContacts(CancellationToken ct) => Ok(await _mainService.GetContact(ct));
    }
}
