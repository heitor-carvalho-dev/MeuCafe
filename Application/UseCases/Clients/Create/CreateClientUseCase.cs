using Domain.Repositories;
using Domain.Entities;
using Domain.Security;

namespace Application.UseCases.Clients.Create;

public class CreateClientUseCase
{
    private readonly IClientRepository _clientRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateClientUseCase(IClientRepository clientRepository, IPasswordHasher hasher)
    {
        _clientRepository = clientRepository;
        _passwordHasher = hasher;
    }

    public async Task<ClientCreatedDTO> ExecuteAsync(ClientCreateRequestDTO dto)
    {
        var client = new Client(
            dto.Name, 
            dto.Email, 
            _passwordHasher.Hash(dto.Password)
        );

        await _clientRepository.CreateNewClient(client);

        var link = "http://MeuCafe/u/";

        var result = new ClientCreatedDTO(link + client.Name);

        return result;
    }
}
