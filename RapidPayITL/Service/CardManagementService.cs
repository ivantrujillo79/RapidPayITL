using Microsoft.EntityFrameworkCore;
using RapidPayITL.Data;
using RapidPayITL.Data.Entity;
using RapidPayITL.Model;

namespace RapidPayITL.Service
{
    public class CardManagementService
    {
        RapidPayDbContext _rapidPayDbContext;
        FeeService _feeService;

        public CardManagementService(RapidPayDbContext rapidPayDBContext, FeeService feeService) 
        { 
            _rapidPayDbContext = rapidPayDBContext;
            _feeService = feeService;
        }


        public async Task<ProcessorResponse> CreateCard(NewCard newCard)
        {
            try
            {
                var returnedCard = await _rapidPayDbContext.Cards.Include(c => c.Payments).SingleOrDefaultAsync(c => c.CardNumber == newCard.CardNumber);

                if (returnedCard == null)
                {
                    var newEntityCard = new Card
                    {
                        CardNumber = newCard.CardNumber,
                        HolderName = newCard.HolderName
                    };

                    await _rapidPayDbContext.AddAsync(newEntityCard);
                    await _rapidPayDbContext.SaveChangesAsync();

                    return new ProcessorResponse
                    {
                        Success = true,
                        Message = $"The card {newCard.CardNumber} has been successfully created."
                    };
                }
                return new ProcessorResponse
                {
                    Success = false,
                    //No additional information is provided given the card already exists, providing information can lead to a security breach
                    Message = $"Rejected request."
                };
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ProcessorResponse> ProcessPayment(NewPayment payment)
        {
            try
            {
                var returnedCard = await _rapidPayDbContext.Cards.Include(c => c.Payments).SingleOrDefaultAsync(c => c.CardNumber == payment.CardNumber);

                if (returnedCard != null)
                {
                    var newEntityPayment = new Payment
                    {
                        CardNumber = payment.CardNumber,
                        Amount = payment.Amount,
                        FeeAmount = _feeService.FeeFactor,
                        PaymentDate = payment.PaymentDate,
                        PaymentBeneficiary = payment.PaymentBeneficiary
                    };

                    returnedCard.Payments.Add(newEntityPayment);
                    await _rapidPayDbContext.SaveChangesAsync();

                    return new ProcessorResponse
                    {
                        Success = true,
                        Message = $"The card {payment.CardNumber} has been successfully charged with ${payment.Amount.ToString()}."
                    };
                }

                return new ProcessorResponse
                {
                    Success = true,
                    //Given the card number was not found, the transaction details are not returned given it can lead to a security breach
                    Message = $"Error: The charge has been rejected."
                };
            }
            catch (Exception)
            {
                throw;
            }
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
