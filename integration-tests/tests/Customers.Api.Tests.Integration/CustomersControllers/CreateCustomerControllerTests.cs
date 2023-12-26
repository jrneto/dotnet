using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomersControllers
{
    public class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
    {
        private readonly CustomerApiFactory _apiFactory;

        public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
        {
            _apiFactory = apiFactory;
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            //// Act
            //var response = await _apiFactory.hett.GetAsync($"customers/{Guid.NewGuid()}");

            //// Assert
            //response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            await Task.Delay(5000);
        }

        //[Fact]
        //public async Task Create_ReturnsCreated_WhenCustomerIsCreated()
        //{
        //    //// Act


        //    //var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        //    //// Assert
        //    //response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        //}
    }
}
