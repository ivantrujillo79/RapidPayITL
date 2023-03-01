using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayITL.Model;
using RapidPayITL.Service;

namespace RapidPayITL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardManagementController : ControllerBase
    {
        CardManagementService _cardManagementService;
        public CardManagementController(CardManagementService cardManagementService) 
        { 
            _cardManagementService = cardManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewCard newCard)
        {
            var result = await _cardManagementService.CreateCard(newCard);
            return Ok(result);
        }

        [HttpPost]
        [Route("payment")]
        public async Task<IActionResult> Pay(NewPayment payment)
        {
            var result = await _cardManagementService.ProcessPayment(payment);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Balance(string cardNumber)
        {
            var result = await _cardManagementService.GetCardBalance(cardNumber);
            return Ok(result);
        }
    }
}
