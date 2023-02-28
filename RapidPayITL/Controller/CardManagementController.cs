using Microsoft.AspNetCore.Mvc;
using RapidPayITL.Model;
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

        [HttpPost]
        public async Task<IActionResult> Create(Card newCard)
        {
            var result = await _cardManagementService.CreateCard(newCard);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Balance(string cardNumber)
        {
            var balanceResult = await _cardManagementService.GetCardBalance(cardNumber);
            return Ok(balanceResult);
        }
    }
}
