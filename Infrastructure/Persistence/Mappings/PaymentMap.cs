using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Mappings
{
    public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            //TABLE
            builder.ToTable("payment");

            //CHECK STATUS
            builder.ToTable("payment", table =>
                table.HasCheckConstraint(
                    PaymentConstraints.CheckPaymentStatus,
                    "\"status\" IN ('PENDING', 'CONFIRMED', 'EXPIRED', 'REVERSED')"
                    )
            );

            //ID
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedNever();

            //RECIPIENT ID
            builder.Property(p => p.RecipientId)
                .HasColumnName("recipient_id");

            //FK RECIPIENT
            builder.HasOne(p => p.Recipient)
                .WithMany()
                .HasForeignKey(p => p.RecipientId)
                .HasConstraintName(PaymentConstraints.FKRecipient)
                .OnDelete(DeleteBehavior.Restrict);

            //VALUE
            builder.Property(p => p.Value)
                .HasColumnName("value")
                .HasPrecision(4, 2)
                .IsRequired();

            //STATUS
            builder.Property(p => p.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasConversion(
                    v => v.ToString().ToUpper(),
                    V => Enum.Parse<PaymentStatus>(V, true))
                .IsRequired();

            //PIX TX ID
            builder.Property(p => p.PixTxId)
                .HasColumnName("pix_txid")
                .HasMaxLength(100);

            //QRCODE PAYLOAD
            builder.Property(p => p.QrCodePayload)
                .HasColumnName("qrcode_payload");

            //EXPIRES AT
            builder.Property(p => p.ExpiresAt)
                .HasColumnName("expires_at")
                .HasColumnType("timestamptz");

            //CREATED AT
            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            //CONFIRMED AT
            builder.Property(p => p.ConfirmedAt)
                .HasColumnName("confirmed_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            //REVERSED AT
            builder.Property(p => p.ReversedAt)
                .HasColumnName("reversed_at")
                .HasColumnType("timestamptz");

            //URL PROOF
            builder.Property(p => p.UrlProof)
                .HasColumnName("url_proof");
        }
    }
}
