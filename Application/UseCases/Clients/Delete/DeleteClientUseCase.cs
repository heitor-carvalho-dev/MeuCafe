using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Security;

namespace Application.UseCases.Clients.Delete;

public class DeleteClientUseCase
{
    private readonly IClientRepository _clientRepository;
    private readonly IPaymentRepository _paymentRepository;

    public DeleteClientUseCase(IClientRepository clientRepository, 
        IPaymentRepository paymentRepository)
    {
        _clientRepository = clientRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task DeleteClientById(Guid id)
    {
        int rows = await _clientRepository.DeleteClientById(id);

        if (rows == 0) throw new ClientNotFoundException();

    }
}

