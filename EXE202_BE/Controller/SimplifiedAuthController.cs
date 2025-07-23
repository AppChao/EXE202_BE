using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Ingredient;

namespace EXE202_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimplifiedAuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<SimplifiedAuthController> _logger;
        private readonly HttpClient _httpClient;

        public SimplifiedAuthController(
            IAuthService authService,
            ILogger<SimplifiedAuthController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("simplified-signup")]
        public async Task<IActionResult> SimplifiedSignUp([FromBody] SimplifiedSignUpRequest model)
        {
            try
            {
                // Generate full SignUpRequest
                var fullSignUpRequest = await GenerateFullSignUpRequest(model);

                // Call existing SignUp method
                var response = await _authService.SignUp(fullSignUpRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Simplified signup failed");
                return Unauthorized(new { Message = "Sign up failed.", Error = ex.Message });
            }
        }

        private async Task<SignUpRequest> GenerateFullSignUpRequest(SimplifiedSignUpRequest model)
        {
            var random = new Random();
            var genders = new[] { "Male", "Female", "Other" };

            // Fetch health conditions and allergies asynchronously
            var healthConditionIds = await FetchHealthConditionIds();
            var allergyIds = await FetchAllergyIds();

            // Generate random data
            var signUpRequest = new SignUpRequest
            {
                email = model.email,
                password = model.password,
                role = "USER",
                age = random.Next(18, 81), // Random age between 18 and 80
                gender = genders[random.Next(genders.Length)], // Random gender
                height = Math.Round(random.NextDouble() * (2.0 - 1.5) + 1.5, 2), // Random height between 1.5m and 2.0m, rounded to 2 decimals
                weight = random.Next(40, 121), // Random weight between 40kg and 120kg
                goalWeight = random.Next(40, 121), // Random goal weight between 40kg and 120kg
                goalId = random.Next(1, 11), // Random goal ID between 1 and 10
                mealScheduledDTO = new MealScheduledDTO
                {
                    BreakFastTime = "08:00", // String thời gian cố định
                    LunchTime = "12:00",
                    DinnerTime = "18:00"
                },
                deviceId = Guid.NewGuid().ToString(), // Random device ID
                listAllergies = GenerateRandomList(allergyIds, 1, 3), // 1-3 random allergies từ Ingredients
                listHConditions = GenerateRandomList(healthConditionIds, 1, 3) // 1-3 random health conditions
            };

            return signUpRequest;
        }

        private async Task<List<int>> FetchHealthConditionIds()
        {
            var response = await _httpClient.GetAsync("https://appchao.azurewebsites.net/api/HealthCondition/health-conditions?page=1&pageSize=20");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Parse JSON using System.Text.Json
            var pageResponse = JsonSerializer.Deserialize<PageListResponse<HealthConditionResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (pageResponse == null || pageResponse.Items == null || !pageResponse.Items.Any())
            {
                throw new Exception("Failed to fetch health conditions: Empty response");
            }

            return pageResponse.Items.Select(h => h.HealthConditionId).ToList();
        }

        private async Task<List<int>> FetchAllergyIds()
        {
            var response = await _httpClient.GetAsync("https://appchao.azurewebsites.net/api/Ingredients?page=1&pageSize=20");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Parse JSON using System.Text.Json
            var pageResponse = JsonSerializer.Deserialize<PageListResponse<Ingredient1Response>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (pageResponse == null || pageResponse.Items == null || !pageResponse.Items.Any())
            {
                throw new Exception("Failed to fetch allergies (ingredients): Empty response");
            }

            return pageResponse.Items.Select(a => a.IngredientId).ToList();
        }

        private List<int> GenerateRandomList(List<int> source, int minCount, int maxCount)
        {
            if (!source.Any())
            {
                return new List<int>(); // Trả về empty nếu source rỗng
            }

            var random = new Random();
            int count = random.Next(minCount, maxCount + 1);
            return source.OrderBy(x => random.Next()).Take(count).ToList();
        }
    }

    public class SimplifiedSignUpRequest
    {
        public string? email { get; set; }
        public string? password { get; set; }
    }
}