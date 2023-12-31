﻿namespace Application.Services;

using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

public class EmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

   

    public string SendEmail(string toAddress, string subject, string body)
    {
        string smtpHost = _configuration["SmtpSettings:Host"];
        int smtpPort = _configuration.GetValue<int>("SmtpSettings:Port");
        string smtpUsername = _configuration["SmtpSettings:Username"];
        string smtpPassword = _configuration["SmtpSettings:Password"];
        bool enableSsl = _configuration.GetValue<bool>("SmtpSettings:EnableSsl");

        using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = enableSsl;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUsername);
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mailMessage);
                return ("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return ("Failed to send email. Error: " + ex.Message);
            }
        }
    }
}