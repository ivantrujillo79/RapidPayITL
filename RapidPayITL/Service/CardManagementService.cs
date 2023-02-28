using Microsoft.EntityFrameworkCore;
using RapidPayITL.Data;
using RapidPayITL.Model;

namespace RapidPayITL.Service
{
    public class CardManagementService
    {
        RapidPayDbContext _rapidPayDbContext;

        public CardManagementService(RapidPayDbContext rapidPayDBContext) 
        { 
            _rapidPayDbContext = rapidPayDBContext;
        }


        public async Task<ProcessorResponse> CreateCard(Card newCard)
        {
            return new ProcessorResponse
            {
                Success = true,
                Message = "The card has been successfully created"
            };
        }

        public async Task<CardBalance> GetCardBalance(string cardNumber)
        {
            try
            {
                var calculatedBalance = new CardBalance();
                var returnedCard = await _rapidPayDbContext.Cards
                    .Include(c => c.Payments)
                    .SingleOrDefaultAsync(c => c.CardNumber == cardNumber);

                if(returnedCard != null)
                {
                    calculatedBalance.HolderName = returnedCard.HolderName;
                    calculatedBalance.CardNumber = returnedCard.CardNumber;

                    if(returnedCard.Payments.Count > 0)
                    {
                        calculatedBalance.Transactions = returnedCard.Payments.Count;
                        calculatedBalance.TotalAmount = returnedCard.Payments.Sum(p => p.Amount);
                    }
                }

                return calculatedBalance;
            }
            catch(Exception)
            {
                throw;
            }

        }


    }
}
