using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TBIApp.Data;
using TBIBankApp.Infrastructure.Extensions;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Mappers;
using TBIApp.Data.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Mappers;
using TBIApp.MailClient.Client;
using TBIApp.MailClient.Client.Contracts;
using TBIApp.MailClient.ParseManagers;
using TBIApp.MailClient.ParseManagers.Contracts;
using TBIApp.MailClient.Mappers;
using TBIApp.MailClient.Mappers.Contracts;
using TBIBankApp.Infrastructure.HostedServices;
using TBIBankApp.Infrastructure.Middleware;
using TBIBankApp.Hubs;
using Serilog;

namespace TBIBankApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<TBIAppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(1, 0, 0);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = true;
               
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TBIAppDbContext>();

            //We register servcies here
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IGmailAPIService, GmailAPIService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IDecodeService, DecodeService>();
            services.AddScoped<ICheckEgnService, CheckEgnService>();
            services.AddScoped<IEncryptService, EncryptService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IcheckCardIdService, CheckCardIdService>();
            services.AddScoped<ICheckPhoneNumberService, CheckPhoneNumberService>();
            //We registerMappers here

            //ViewModelMappers
            services.AddScoped<IEmailViewModelMapper, EmailViewModelMapper>();
            services.AddScoped<IAttachmentViewModelMapper, AttachmentViewModelMapper>();
            services.AddScoped<IApplicationViewModelMapper, ApplicationViewModelMapper>();
            services.AddScoped<IReportDiagramViewModelMapper, ReportDiagramViewModelMapper>();
            services.AddScoped<IUserViewModelMapper, UserViewModelMapper>();
            services.AddScoped<IRegisterViewModelMapper, RegisterViewModelMapper>();

            //ServiceMapper
            services.AddScoped<IReportDiagramDTOMapper, ReportDiagramDTOMapper>();
            services.AddScoped<IAttachmentDTOMapper, AttachmentDTOMapper>();
            services.AddScoped<IEmailDTOMapper, EmailDTOMapper>();
            services.AddScoped<ILoanApplicationDTOMapper, LoanApplicationDTOMapper>();
            services.AddScoped<IUserDTOMapper, UserDTOMapper>();

            //MailClient 
            services.AddScoped<IGmailParseManager, GmailParseManager>();
            services.AddScoped<IMessageToEmailDTOMapper, MessageToEmailDTOMapper>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/");
                opt.AccessDeniedPath = new PathString("/Identity/Account/AccessDenied");
            });

            services.AddSignalR();

            //HostedServices added here
            services.AddHostedService<HostedGetEmailsService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UpdateDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(); 
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseCookiePolicy();
            app.UseAuthentication();
            //Use extension method 
            app.UseMiddleware<PageNotFoundMiddleware>();
            app.UseMiddleware<BadRequestMiddleware>();


            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notification");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
