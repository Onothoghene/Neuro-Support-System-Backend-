using Application.Interfaces;
using Domain.Settings;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            var mailOption = _config.GetSection(nameof(MailSettings));

            services.Configure<MailSettings>(mailOption);
            services.AddSingleton(_config.GetSection(nameof(PayStackOptions)).Get<PayStackOptions>());

            services.Configure<ZipFileSettings>(_config.GetSection(nameof(ZipFileSettings)));
            services.Configure<RoundRobinSecUserSettings>(_config.GetSection(nameof(RoundRobinSecUserSettings)));
            services.Configure<PeriodicLoginSettings>(_config.GetSection(nameof(PeriodicLoginSettings)));
            services.Configure<ResourceLinkSettings>(_config.GetSection(nameof(ResourceLinkSettings)));
            services.Configure<OutlookCredentialsSettings>(_config.GetSection(nameof(OutlookCredentialsSettings)));

            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IFileZipService, FileZipService>();

            var host = "smtp-relay.brevo.com";
            var user = "b0d7ac001@smtp-brevo.com";
            var password = "xsmtpsib-e182f93a905788400925ce46f0fa988466afb6113365694652b0527616bcf54f-xLJfaEXSeageEWHJ";
            var port = 587;
            
            var client = new SmtpClient
            {
                //Credentials = new NetworkCredential(mailOption[nameof(MailSettings.SmtpUser)], mailOption[nameof(MailSettings.SmtpPass)]),
                Credentials = new NetworkCredential(user, password),
                Host = host,
                //Host = mailOption[nameof(MailSettings.SmtpHost)],
                //Port = Convert.ToInt32(mailOption[nameof(MailSettings.SmtpPort)]),
                Port = port,
                //EnableSsl = Convert.ToBoolean(mailOption[nameof(MailSettings.EnableSsl)]),
                EnableSsl = true,
                UseDefaultCredentials = false
            };

            services.AddFluentEmail(mailOption[nameof(MailSettings.EmailFrom)])
                    .AddRazorRenderer(Directory.GetCurrentDirectory())
                    .AddSmtpSender(client);
                    


            services.AddSingleton<IFileUploadService, FileUploadService>();

        }
    }
}
