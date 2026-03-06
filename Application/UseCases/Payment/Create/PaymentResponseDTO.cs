using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Payment.Create
{
    public class PaymentResponseDTO
    {
        public string QrCodePayload { get; set; } = string.Empty;
        public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.UtcNow.AddMinutes(30);

        public PaymentResponseDTO() { }

        public PaymentResponseDTO(string qrCodePayload)
        {
            QrCodePayload = qrCodePayload;
        }
    }
}
