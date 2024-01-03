using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomersControllers
{
    public class GetAllCustomersControllerTests : IClassFixture<CustomerApiFactory>
    {
        private readonly HttpClient _client;

        private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
            .RuleFor(x => x.Email, faker => faker.Person.Email)
            .RuleFor(x => x.FullName, faker => faker.Person.FullName)
            .RuleFor(x => x.GitHubUsername, faker => CustomerApiFactory.ValidGitHubUser)
            .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);


        public GetAllCustomersControllerTests(CustomerApiFactory apiFactory)
        {
            _client = apiFactory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsAllCustomer_WhenCustomersExists()
        {
            //Arrange
            var customer = _customerGenerator.Generate();
            var createdResponse = await _client.PostAsJsonAsync("customers", customer);
            var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

            // Act
            var response = await _client.GetAsync("customers");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var customeresResponse = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
            customeresResponse!.Customers.Single().Should().BeEquivalentTo(createdCustomer);

            //Cleanup
            await _client.DeleteAsync($"customers/{createdCustomer.Id}");
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyResult_WhenNoCustomersExists()
        {

            // Act
            var response = await _client.GetAsync("customers");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var customeresResponse = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
            customeresResponse!.Customers.Should().BeEmpty();
        }
    }
}
