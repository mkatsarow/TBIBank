using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TBIApp.Data.Models;

namespace TBIApp.Data.Configuration
{
    public class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
    {
        public void Configure(EntityTypeBuilder<LoanApplication> builder)
        {
            builder.HasKey(la => la.Id);

            //builder.Property(la => la.FirstName)
            //    .IsRequired();

            //builder.Property(la => la.LastName)
            //    .IsRequired();

            //builder.Property(la => la.EGN)
            //    .IsRequired();

            ////builder.Property(la => la.CardId)
            //    .IsRequired();

            //builder.Property(la => la.PhoneNumber)
            //    .IsRequired();

        }
    }
}
