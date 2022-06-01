using System;
using Whatsapp.Models;

namespace Whatsapp.AppCode.HelperClass
{
    public class HelperService
    {
        public Ledger CalculateDebitLedger(UserBalance adminBalance, double RequestedAmount)
        {
            Ledger ledger = new Ledger();
            ledger.ClosingBalance = adminBalance.Balance;
            ledger.Amount = RequestedAmount;
            ledger.OpeningBalance = adminBalance.PreviousBalance;
            ledger.TransactionType = "dr";
            ledger.CreatedDate = DateTime.Now;
            ledger.ModifiedDate = DateTime.Now;
            ledger.UserId = adminBalance.UserId;
            return ledger;
        }

        public Ledger CalculateCreditLedger(UserBalance userBalance, double RequestedAmount)
        {
            Ledger ledger = new Ledger();
            ledger.ClosingBalance = userBalance.Balance;
            ledger.Amount = RequestedAmount;
            ledger.OpeningBalance = userBalance.PreviousBalance;
            ledger.TransactionType = "cr";
            ledger.CreatedDate = DateTime.Now;
            ledger.ModifiedDate = DateTime.Now;
            ledger.UserId = userBalance.UserId;
            return ledger;
        }
    }
}
