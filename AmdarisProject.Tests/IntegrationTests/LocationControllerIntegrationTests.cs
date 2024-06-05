using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WorkoutReservations.Application.DTOs.Auth;
using WorkoutReservations.Application.DTOs.Location;

namespace AmdarisProject.Tests.IntegrationTests
{
    public class LocationControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public LocationControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Add_ShouldReturnOk_WhenLocationAddedSuccessfully()
        {
            var addLocationDto = new AddLocationDto
            {
                City = "Test City",
                Address = "123 Test Street",
                Latitude = "40.7128", 
                Longitude = "-74.0060", 
                MaxCapacity = 50, 
                Region = "Test Region",
                ZipCode = "12345" 
            };

            var token = await GetAuthToken("admin@mail.com", "adminAdmin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJsonAsync("/api/Location/add", addLocationDto);

            response.EnsureSuccessStatusCode();
        }

        private async Task<string> GetAuthToken(string email, string password)
        {
            var loginDto = new LoginUserDto { Email = email, Password = password };
            var response = await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<AuthResultDto>();
            return responseData!.Token;
        }

        [Fact]
        public async Task LocationsByWorkout_ShouldReturnLocations_WhenWorkoutExists()
        {
            var existingWorkoutId = Guid.Parse("4308B37C-78D0-4323-AEC6-4BAEBBCF9D84");

            var token = await GetAuthToken("customer22@gmail.com", "customer22");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"/api/Location/byWorkout?id={existingWorkoutId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task LocationsByWorkout_ShouldReturnBadRequest_WhenWorkoutDoesNotExist()
        {
            var nonExistentWorkoutId = Guid.NewGuid();

            var token = await GetAuthToken("customer22@gmail.com", "customer22");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"/api/Location/byWorkout?id={nonExistentWorkoutId}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task All_ShouldReturnAllLocations()
        {
            var token = await GetAuthToken("ivan_trainer@gmail.com", "ivan_");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("/api/Location/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
