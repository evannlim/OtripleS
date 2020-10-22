﻿//---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
//----------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using OtripleS.Web.Api.Brokers.Loggings;
using OtripleS.Web.Api.Brokers.Storage;
using OtripleS.Web.Api.Models.GuardianContacts;

namespace OtripleS.Web.Api.Services.GuardianContacts
{
    public partial class GuardianContactService : IGuardianContactService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuardianContactService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<GuardianContact> AddGuardianContactAsync(GuardianContact guardianContact) =>
        TryCatch(async () =>
        {
            ValidateGuardianContactOnCreate(guardianContact);

            return await this.storageBroker.InsertGuardianContactAsync(guardianContact);
        });
    }
}