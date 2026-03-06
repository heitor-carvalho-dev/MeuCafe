using Application.Exceptions;
using Application.UseCases.Payment.Create;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            try
            {
                var confirmUrl = $"https://MeuCafe.com/api/payment/{payment.PixTxId}/confirm";

                var qrGenerator = new QRCodeGenerator();
                var qrData = qrGenerator.CreateQrCode(confirmUrl, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new PngByteQRCode(qrData);
                var qrBytes = qrCode.GetGraphic(10);
                var qrBase64 = Convert.ToBase64String(qrBytes);

                payment.QrCodePayload = qrBase64;

                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();

                return payment;
            }
            catch (DbUpdateException ex) 
            {
                var pgEx = ex.InnerException as PostgresException;

                if (pgEx?.SqlState == "23503")
                    throw new RecipientNotFoundException();

                throw;
            }
            
        }
    }
}
