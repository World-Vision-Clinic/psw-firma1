using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel;
using Hospital_API;
using Hospital_API.Controllers;
using Hospital_API.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class RelocatingEquipmentTest
    {

        private readonly HttpClient _client;

        public RelocatingEquipmentTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }
        

        [Fact]
        public async Task Get_suggestion_for_relocation_periodAsync()
        {
            // Arrange
            var startDateTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(year: 2021, month: 12, day: 4, hour: 10, minute: 30, second: 0)).TotalMilliseconds)/1000;
            var endDateTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(year: 2021, month: 12, day: 10, hour: 10, minute: 30, second: 0)).TotalMilliseconds) / 1000;
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/transportPeriod?buildingId=0&transportDurationInHours=10&startDateTimeStamp="+startDateTimestamp+"&endDateTimeStamp=" + endDateTimestamp);

            //Act
            var response = await _client.SendAsync(request);
            var suggestedPeriods = JsonConvert.DeserializeObject<TransportationPeriodDTO[]>(
                            await response.Content.ReadAsStringAsync()
            );

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Single(suggestedPeriods);
           

        }

        [Fact]
        public async Task Get_suggestion_for_not_available_relocation_periodAsync()
        {
            // Arrange
            var startDateTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(year: 2021, month: 11, day: 28, hour: 10, minute: 30, second: 0)).TotalMilliseconds) / 1000;
            var endDateTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(year: 2021, month: 11, day: 30, hour: 10, minute: 30, second: 0)).TotalMilliseconds) / 1000;
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/transportPeriod?buildingId=0&transportDurationInHours=10&startDateTimeStamp=" + startDateTimestamp + "&endDateTimeStamp=" + endDateTimestamp);

            //Act
            var response = await _client.SendAsync(request);
            var suggestedPeriods = JsonConvert.DeserializeObject<TransportationPeriodDTO[]>(
                            await response.Content.ReadAsStringAsync()
            );

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Empty(suggestedPeriods);


        }

        [Fact]
        public async Task Setting_transport_period()
        {
            // Arrange
            var startDateTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(year: 2021, month: 11, day: 28, hour: 10, minute: 30, second: 0)).TotalMilliseconds) / 1000;
            var endDateTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(year: 2021, month: 11, day: 30, hour: 10, minute: 30, second: 0)).TotalMilliseconds) / 1000;
            var request = new HttpRequestMessage(new HttpMethod("POST"), "/api/transportPeriod?buildingId=0&transportDurationInHours=10&startDateTimeStamp=" + startDateTimestamp + "&endDateTimeStamp=" + endDateTimestamp);

            //Act
            var response = await _client.SendAsync(request);
            var suggestedPeriods = JsonConvert.DeserializeObject<TransportationPeriodDTO[]>(
                            await response.Content.ReadAsStringAsync()
            );

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Empty(suggestedPeriods);


        }
    }
}
