using MeuCafe.Controllers;
using Domain.Entities;
using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Application.UseCases.Clients.Create;

namespace Test;

public class Endpoints : IClassFixture<TesteApiFactory>
{
    private readonly HttpClient _client;

    public Endpoints(TesteApiFactory factory)
    {
        _client = factory.CreateClient();
    }



    [Fact]
    public async Task TestAllActiveClients()
    {
        var response = await _client.GetAsync("/api/client/AllActiveClients");

        response.EnsureSuccessStatusCode();

        var clients = await response.Content.ReadFromJsonAsync<List<Client>>();

        Assert.NotNull(clients);
        Assert.DoesNotContain(clients, c => c.IsActive == false);

    }
}
