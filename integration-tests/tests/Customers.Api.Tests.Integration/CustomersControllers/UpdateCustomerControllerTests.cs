﻿using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomersControllers
{
    public class UpdateCustomerControllerTests : IClassFixture<CustomerApiFactory>
    {
        private readonly HttpClient _client;

        private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
            .RuleFor(x => x.Email, faker => faker.Person.Email)
            .RuleFor(x => x.FullName, faker => faker.Person.FullName)
            .RuleFor(x => x.GitHubUsername, faker => CustomerApiFactory.ValidGitHubUser)
            .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);


        public UpdateCustomerControllerTests(CustomerApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
        }


        [Fact]
        public async Task Update_UpdateUser_WhenDataIsValid()
        {
            // Arrange
            var customer = _customerGenerator.Generate();
            var createdResponse = await _client.PostAsJsonAsync("customers", customer);
            var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            customer = _customerGenerator.Generate();

            //Act
            var response = await _client.PutAsJsonAsync($"customers/{createdCustomer!.Id}", customer);

            //Assert
            var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
            customerResponse.Should().BeEquivalentTo(customer);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_ReturnsValidationError_WhenEmailIsInvalid()
        {
            // Arrange
            var customer = _customerGenerator.Generate();
            var createdResponse = await _client.PostAsJsonAsync("customers", customer);
            var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            const string invalidEmail = "asdfs3";
            customer = _customerGenerator.Clone()
                .RuleFor(x => x.Email, invalidEmail).Generate();

            //Act
            var response = await _client.PutAsJsonAsync($"customers/{createdCustomer!.Id}", customer);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error!.Title.Should().Be("One or more validation errors occurred.");
            error!.Errors["Email"][0].Should().Be($"{invalidEmail} is not a valid email address");
        }

        [Fact]
        public async Task Update_ReturnsValidationError_WhenGitHubUserDoesNotExists()
        {
            // Arrange
            var customer = _customerGenerator.Generate();
            var createdResponse = await _client.PostAsJsonAsync("customers", customer);
            var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            const string invalidGitHubUser = "asdfs3";
            customer = _customerGenerator.Clone()
                .RuleFor(x => x.GitHubUsername, invalidGitHubUser).Generate();

            //Act
            var response = await _client.PutAsJsonAsync($"customers/{createdCustomer!.Id}", customer);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            error!.Status.Should().Be(400);
            error!.Title.Should().Be("One or more validation errors occurred.");
            error!.Errors["GitHubUsername"][0].Should().Be($"There is no GitHub user with username {invalidGitHubUser}");
        }
    }
}
