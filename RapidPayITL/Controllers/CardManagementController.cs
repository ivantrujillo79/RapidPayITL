using Microsoft.AspNetCore.Mvc;

namespace RapidPayITL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardManagementController : ControllerBase
    {
        public CardManagementController() { }

        [HttpGet]
        public async Task<IActionResult> Balance(string cardNumber)
        {
            return Ok();
        }
    }
}
