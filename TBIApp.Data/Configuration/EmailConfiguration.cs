using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TBIApp.Data.Models;

namespace TBIApp.Data.Configuration
{
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(a => a.Attachments)
                .WithOne(e => e.Email);

            //builder.Property(e => e.IsOpne)
            //    .IsRequired();

            //builder.Property(e => e.Sender)
            //    .IsRequired();

            //builder.Property(e => e.GmailEmailId)
            //    .IsRequired();

            //builder.Property(e => e.RecievingDateAtMailServer)
            //    .IsRequired();

            //builder.Property(e => e.Body)
            //    .HasMaxLength(500);

            //builder.Property(e => e.Subject)
            //    .HasMaxLength(50);

        }
    }
}

