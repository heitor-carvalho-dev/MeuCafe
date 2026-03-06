using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuCafe.Handlers;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Error: {Message}", exception.Message);

        //RFC 7807 MODEL
        var (statusCode, title) = exception switch
        {
            BusinessException => (StatusCodes.Status400BadRequest, "Violated Business Rule"),
            ClientNotFoundException => (StatusCodes.Status404NotFound, "Client not Found"),
            DbUpdateException => (StatusCodes.Status409Conflict, "Database Conflict"),
            DomainException => (StatusCodes.Status400BadRequest, "Domain validation error"),
            NoValuePaymentException =>(StatusCodes.Status400BadRequest, "Payment must have a value"),
            RecipientNotFoundException => (StatusCodes.Status404NotFound, "Recipient not found"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}