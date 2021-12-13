using Integration_API.Controller;
using Integration_API.Dto;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Integration_API;
using System.Threading.Tasks;
using System.Net;
using Shouldly;
using Integration.Pharmacy.Model;
using Newtonsoft.Json;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTests.IntegrationTests
{
    public class MedicinesTests
    {

        private readonly HttpClient _client;

        public MedicinesTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        /*[Fact]
        public async Task Check_response_when_medicine_is_available()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/medicines/check?name=Aspirin&dosage=200&quantity=2");

            // Act
            var response = await _client.SendAsync(request);

            var pharmacies = JsonConvert.DeserializeObject<PharmacyDto[]>(
                await response.Content.ReadAsStringAsync()
            );

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Single(pharmacies);
        }*/

        /*[Fact]
        public async Task Check_response_when_medicine_is_not_available()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/medicines/check?name=Aspirin&dosage=500&quantity=2");

            // Act
            var response = await _client.SendAsync(request);
            var pharmacies = JsonConvert.DeserializeObject<PharmacyDto[]>(
                            await response.Content.ReadAsStringAsync()
            );

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Empty(pharmacies);
        }*/
    }
}
