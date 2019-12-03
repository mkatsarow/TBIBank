using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TBIApp.Data.Models;

namespace TBIApp.Data.Configuration
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.Id);

            //builder.Property(a => a.FileName)
            //    .IsRequired();

            //builder.Property(a => a.SizeKb)
            //    .IsRequired();

            //builder.Property(a => a.SizeMb)
            //    .IsRequired();

        }
    }
}
