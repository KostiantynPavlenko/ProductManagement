

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using ProductManagement.API.IntegrationTests.Auth;
using ProductManagement.API.IntegrationTests.Moqs;

namespace ProductManagement.API.IntegrationTests.Infrastructure;

internal class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MockFactory _mockFactory;
    public string DefaultUserId { get; set; } = "1";


    public TestWebApplicationFactory(MockFactory mockFactory)
    {
        _mockFactory = mockFactory;
    }
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(ReplaceDependencies);
        builder.ConfigureServices(services =>
        {
            services.Configure<JwtBearerOptions>(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.Configuration = new OpenIdConnectConfiguration
                    {
                        Issuer = JwtTokenProvider.Issuer,
                    };
                    options.TokenValidationParameters.ValidIssuer = JwtTokenProvider.Issuer;
                    options.TokenValidationParameters.ValidAudience = JwtTokenProvider.Issuer;
                    options.Configuration.SigningKeys.Add(JwtTokenProvider.SecurityKey);
                }
            );
        });
        return base.CreateHost(builder);
    }

    private void ReplaceDependencies(IServiceCollection services)
    {
        foreach (var (interfaceType, serviceMock) in _mockFactory.GetMocks())
        {
            var existingService = services.SingleOrDefault(x => x.ServiceType == interfaceType);
            services.Remove(existingService!);

            services.AddSingleton(interfaceType, serviceMock);
        }
    }
}