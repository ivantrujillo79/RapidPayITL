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

        public async Task<CardBalance> GetCardBalance(string cardNumber)
        {
            try
            {
                var calculatedBalance = new CardBalance();
                var queriedCard = await _rapidPayDbContext.Cards.Include(c => c.Payments).SingleOrDefaultAsync(c => c.CardNumber == cardNumber);

                if(queriedCard != null)
                {
                    calculatedBalance.CardHolder = queriedCard.HolderName;
                    calculatedBalance.CardNumber = queriedCard.CardNumber;

                    if(queriedCard.Payments.Count > 0)
                    {
                        calculatedBalance.Transactions = queriedCard.Payments.Count;
                        calculatedBalance.TotalAmount = queriedCard.Payments.Sum(p => p.Amount);
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
