﻿using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings emailSettings {  get;}

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(EmailMessage email)
        {
            var client = new SendGridClient(emailSettings.ApiKey);

            var to = new EmailAddress(email.To);

            var from = new EmailAddress()
            {
                Email = emailSettings.FromAddress,
                Name = emailSettings.FromName
            };
            var msg = MailHelper.CreateSingleEmail(from, to, email.Subject , email.Body , email.Body);

            var response = await client.SendEmailAsync(msg);

            //return response.StatusCode == System.Net.HttpStatusCode.OK ||
            // response.StatusCode == System.Net.HttpStatusCode.Accepted;

            return response.IsSuccessStatusCode;
        }
    }
}
