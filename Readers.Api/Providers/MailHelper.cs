using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Utils;
using ReadersApi.Controllers;

namespace ReadersApi.Providers
{
    public interface IMailHelper
    {
        void SendMail(string to, string subject, MailViewModel model);
    }

    public class MailHelper : IMailHelper
    {
        IConfiguration configuration;

        public MailHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendMail(string to, string subject, MailViewModel model)
        {
            string fromAddress = configuration["SmtpConfig:FromAddress"];
            string serverAddress = configuration["SmtpConfig:ServerAddress"];
            string username = configuration["SmtpConfig:Username"];
            string password = configuration["SmtpConfig:Password"];
            int port = Convert.ToInt32(configuration["SmtpConfig:Port"]);
            bool isSsl = Convert.ToBoolean(configuration["SmtpConfig:IsUseSsl"]);

            string str1 = "gmail.com";
            string str2 = fromAddress.ToLower();

            if (str2.Contains(str1))
            {
                try
                {
                    SendSmtpMail(fromAddress, serverAddress, username, password, to, subject, model, 587, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                try
                {
                    SendSmtpMail(fromAddress, serverAddress, username, password, to, subject, model, 25, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void SendSmtpMail(
            string from,
            string serverAddress,
            string username,
            string password,
            string to,
            string subject,
            MailViewModel messageContent,
            int port,
            bool isUseSsl)
        {
            try
            {

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(from, from));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;
                
                var builder = new BodyBuilder();
                var header = builder.LinkedResources.Add("header", File.OpenRead(messageContent.HeaderImage.ContentPath));
                header.ContentId = messageContent.HeaderImage.ContentId;
                builder.HtmlBody = messageContent.Content;
                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(serverAddress, port, isUseSsl);
                    client.Authenticate(username, password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}