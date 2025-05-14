using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace WebApi.IntegrationTest.Users.Register;
public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string MEHTOD = "api/User";

    private readonly HttpClient _httpClient;
    public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        //var httpClient = new HttpClient();
        //httpClient.BaseAddress = new Uri("");

        var result =  await _httpClient.PostAsJsonAsync("api/user",request);

        result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

    }
}
