using Microsoft.AspNetCore.Mvc;
using RapidPayITL.Service;

namespace RapidPayITL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardManagementController : ControllerBase
    {
        CardManagementService _cardManagementService;
        public CardManagementController(CardManagementService cardManagementService) 
        { 
            _cardManagementService = cardManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Balance(string cardNumber)
        {
            var balanceResult = await _cardManagementService.GetCardBalance(cardNumber);
            return Ok(balanceResult);
        }
    }
}
