using Domain.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;

namespace Application.UseCases.Payment.Create
{
    public class CreatePaymentUseCase
    {
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentUseCase(
            IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentResponseDTO> ExecuteAsync(PaymentCreateRequestDTO requestDTO)
        {
            if ( requestDTO.Value <= (decimal)0 ) throw new NoValuePaymentException();

            var payment = new Domain.Entities.Payment(
                requestDTO.RecipientId,
                requestDTO.Value
                );

            var result = await _paymentRepository.CreatePayment(payment);

            var dto = new PaymentResponseDTO(result.QrCodePayload);

            return dto;
        }
    }
}
