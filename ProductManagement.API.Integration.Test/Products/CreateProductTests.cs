using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using ProductManagement.API.IntegrationTests.Auth;
using ProductManagement.API.IntegrationTests.Infrastructure;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Application.Products.DTO;
using ProductManagement.Domain.Products;
using MockFactory = ProductManagement.API.IntegrationTests.Moqs.MockFactory;

namespace ProductManagement.API.IntegrationTests.Products;

public class CreateProductTests
{
    private readonly TestWebApplicationFactory _webApplicationFactory;
    private readonly MockFactory _mockFactory;
    private readonly HttpClient _client;
    
    public CreateProductTests()
    {
        _mockFactory = new MockFactory();
        _webApplicationFactory = new TestWebApplicationFactory(_mockFactory);
        _client = _webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_WithValidInput_ReturnsSuccessResult()
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
        
        var productRequest = new ProductDto()
        {
            Name = "TestProduct",
            Sku = "TEST_12311111111111111111111",
            CategoryId = Guid.NewGuid(),
            Price = 123
        };

        _mockFactory.ProductRepository.Setup(x => x.Create(It.IsAny<Product>()))
            .ReturnsAsync(true);
        
        var productJson = JsonConvert.SerializeObject(productRequest);
        var requestContent = new StringContent(productJson, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync($"products/", requestContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task CreateProduct_WithInvalidSkuInput_ReturnsBadRequest()
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
        
        var productRequest = new CreateProductCommand()
        {
            Name = "TestProduct",
            Sku = null,
            CategoryId = Guid.NewGuid(),
            Price = 123
        };
        
        var productJson = JsonConvert.SerializeObject(productRequest);
        var requestContent = new StringContent(productJson, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync($"products/", requestContent);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}