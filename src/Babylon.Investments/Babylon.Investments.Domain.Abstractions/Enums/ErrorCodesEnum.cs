﻿using System.ComponentModel;

namespace Babylon.Investments.Domain.Abstractions.Enums
{
    public enum ErrorCodesEnum
    {
        [Description("Ticker is not provided.")]
        TickerNotProvided = -100001,
        
        [Description("Ticker provided is not valid.")]
        TickerNotValid,
        
        [Description("The User Id provided is not valid. Please provide a valid GUID.")]
        UserIdProvidedNotValid,
        
        [Description("The Tenant Identifier is not provided.")]
        TenantIdNotProvided,

        [Description("Date provided is a future Date which is not valid.")]
        DateIsFutureDate,
        
        [Description("Units are not positive. Please enter a valid value.")]
        UnitsNotPositive,
        
        [Description("Price is not positive. Please enter a valid price.")]
        PriceNotPositive,
        
        [Description("The Transaction Id provided is not valid. Please provide a valid GUID.")]
        TransactionIdProvidedNotValid,
        
        [Description("Expected string number..")]
        StringNumberNotValid
    }
}