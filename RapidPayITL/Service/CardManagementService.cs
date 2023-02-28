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
                return new CardBalance { CardHolder = "Ivan", CardNumber="123456789012345", TotalAmount=500, Transactions = 5 };
            }
            catch(Exception)
            {
                throw;
            }

        }


    }
}
