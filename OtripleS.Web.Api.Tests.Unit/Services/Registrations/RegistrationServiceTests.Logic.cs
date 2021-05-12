﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using OtripleS.Web.Api.Models.Registrations;
using Xunit;

namespace OtripleS.Web.Api.Tests.Unit.Services.Registrations
{
    public partial class RegistrationServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllRegistrations()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            IQueryable<Registration> randomRegistrations =
                CreateRandomRegistrations(randomDateTime);

            IQueryable<Registration> storageRegistrations =
                randomRegistrations;

            IQueryable<Registration> expectedRegistrations =
                storageRegistrations;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllRegistrations())
                    .Returns(storageRegistrations);

            // when
            IQueryable<Registration> actualRegistrations =
                this.registrationService.RetrieveAllRegistrations();

            // then
            actualRegistrations.Should().BeEquivalentTo(expectedRegistrations);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllRegistrations(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveRegistrationByIdAsync()
        {
            // given
            Guid randomRegistrationId = Guid.NewGuid();
            Guid inputRegistrationId = randomRegistrationId;
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Registration randomRegistration = CreateRandomRegistration(randomDateTime);
            Registration storageRegistration = randomRegistration;
            Registration expectedRegistration = storageRegistration;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectRegistrationByIdAsync(inputRegistrationId))
                    .ReturnsAsync(storageRegistration);

            // when
            Registration actualRegistration =
                await this.registrationService.RetrieveRegistrationByIdAsync(inputRegistrationId);

            // then
            actualRegistration.Should().BeEquivalentTo(expectedRegistration);

            this.storageBrokerMock.Verify(broker =>
                 broker.SelectRegistrationByIdAsync(inputRegistrationId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}