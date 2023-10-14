using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using FluentAssertions;
using ProductManagement.API.IntegrationTests.Infrastructure;
using ProductManagement.API.IntegrationTests.Moqs;
using ProductManagement.Domain.Products;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using ProductManagement.API.IntegrationTests.Auth;
using ProductManagement.Application.Products.DTO;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace ProductManagement.API.IntegrationTests.Products;

public class GetAllProductsTests
{
    private readonly TestWebApplicationFactory _webApplicationFactory;
    private readonly MockFactory _mockFactory;
    private readonly HttpClient _client;

    public GetAllProductsTests()
    {
        _mockFactory = new MockFactory();
        _webApplicationFactory = new TestWebApplicationFactory(_mockFactory);
        _webApplicationFactory.DefaultUserId = "1";
        _client = _webApplicationFactory.CreateClient();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
    }

    [Fact]
    public async Task GetProducts_CallEndpoint_ProductListIsReturned()
    {
        var token = JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
            new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Issuer,
                new List<Claim> { new(ClaimTypes.Role, "Operator"), },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: JwtTokenProvider.SigningCredentials
            )
        );

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var product = Product.Create("TestProduct",
            Guid.NewGuid(),
            "TEST_1231111111111111111111111",
            123);

        var expected = new List<Product>()
        {
            product
        };

        _mockFactory.ProductRepository.Setup(x => x.GetAll())
            .Returns(Task.FromResult(expected));

        var response = await _client.GetAsync($"products/");
        var content = await response.Content.ReadAsStringAsync();
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var productResponse = JsonConvert.DeserializeObject<List<ProductDto>>(content);
        productResponse.Should().SatisfyRespectively(first =>
        {
            first.Name.Should().Be(product.Name);
            first.Sku.Should().Be(product.Sku.Value);
            first.Price.Should().Be(product.Price.Amount);
        });
    }
}