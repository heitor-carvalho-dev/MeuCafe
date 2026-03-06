using Application.UseCases.Clients.List;
using Application.UseCases.Clients.Create;
using Application.UseCases.Clients.Delete;
using Application.UseCases.Payment.Create;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace MeuCafe.Controllers;

[ApiController]
[Route("api/client")]
public class RequestsController : ControllerBase
{
    private readonly ListClientsUseCase _listClientsUseCase;
    private readonly CreateClientUseCase _createClientUseCase;
    private readonly DeleteClientUseCase _deleteClientUseCase;
    private readonly CreatePaymentUseCase _createPaymentUseCase;

    public RequestsController(
        ListClientsUseCase listClientsUseCase, 
        CreateClientUseCase createClientUseCase, 
        DeleteClientUseCase deleteClientUseCase,
        CreatePaymentUseCase createPaymentUseCase
        )
    {
        _listClientsUseCase = listClientsUseCase;
        _createClientUseCase = createClientUseCase;
        _deleteClientUseCase = deleteClientUseCase;
        _createPaymentUseCase = createPaymentUseCase;
    }

    [HttpGet("AllActiveClients")]
    public async Task<IActionResult> GetAllClients()
    {
        var result = await _listClientsUseCase.ExecuteAsync();
        return Ok(result);
    }

    [HttpPost("CreateClient")]
    public async Task<ActionResult<ClientCreatedDTO>> CreateNewClient(
        [FromBody] ClientCreateRequestDTO dto)
    {
       
        var result = await _createClientUseCase.ExecuteAsync(dto);

        return Created(result.URL, result);
        
    }

    [HttpDelete("DeleteClientById")]
    public async Task<IActionResult> DeleteClientById(Guid id)
    {
        
        await _deleteClientUseCase.DeleteClientById(id);

        return NoContent();
    }

    [HttpPost("CreatePayment")]
    public async Task<ActionResult<PaymentCreateRequestDTO>> CreatePayment(
        [FromBody] PaymentCreateRequestDTO dto)
    {
        var result = await _createPaymentUseCase.ExecuteAsync(dto);

        return Ok(result);
    }

}
