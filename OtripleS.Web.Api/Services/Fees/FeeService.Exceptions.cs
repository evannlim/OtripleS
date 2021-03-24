﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OtripleS.Web.Api.Models.Fees;
using OtripleS.Web.Api.Models.Fees.Exceptions;

namespace OtripleS.Web.Api.Services.Fees
{
    public partial class FeeService
    {
        private delegate ValueTask<Fee> ReturningFeeFunction();

        private async ValueTask<Fee> TryCatch(ReturningFeeFunction returningFeeFunction)
        {
            try
            {
                return await returningFeeFunction();
            }
            catch (NullFeeException nullFeeException)
            {
                throw CreateAndLogValidationException(nullFeeException);
            }
        }

        private FeeValidationException CreateAndLogValidationException(Exception exception)
        {
            var feeValidationException = new FeeValidationException(exception);
            this.loggingBroker.LogError(feeValidationException);

            return feeValidationException;
        }
    }
}