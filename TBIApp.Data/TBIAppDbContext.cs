using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TBIApp.Data.Configuration;
using TBIApp.Data.Models;

namespace TBIApp.Data
{
    public class TBIAppDbContext : IdentityDbContext<User>
    {
        public TBIAppDbContext() { }
        public TBIAppDbContext(DbContextOptions<TBIAppDbContext> options)
          : base(options) { }

        public DbSet<Email> Emails { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AttachmentConfiguration());
            builder.ApplyConfiguration(new EmailConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
