using System;
using System.Collections.Generic;
using Babylon.Investments.Domain.Abstractions.Requests;
using Babylon.Investments.Domain.Objects.Base;
using Babylon.Investments.Domain.Objects.Interfaces;

namespace Babylon.Investments.Domain.Objects
{
    public class BuyOperation : Transaction, IOperation
    {
        private readonly IReadOnlyCollection<Transaction> _companyTransactionHistory;
        
        public BuyOperation(
            TransactionPostRequest transactionPostRequest,
            IReadOnlyCollection<Transaction> companyTransactionHistory) : base(transactionPostRequest)
        {
            _companyTransactionHistory = companyTransactionHistory ?? throw new ArgumentNullException(nameof(companyTransactionHistory));
            
            //PreviousUnits = _companyTransactionHistory.
        }
    }
}