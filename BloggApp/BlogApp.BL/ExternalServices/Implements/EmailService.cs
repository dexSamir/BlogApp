using System;
using System.Net;
using System.Net.Mail;
using BlogApp.BL.DTOs.Options;
using BlogApp.BL.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlogApp.BL.ExternalServices.Implements;

public class EmailService : IEmailService
{
    readonly SmtpClient _client;
    readonly MailAddress _from;
    readonly HttpContext Context;

    public EmailService(IOptions<SmtpOptions> option, IHttpContextAccessor acc)
    {
        var opt = option.Value;
        _client = new(opt.Host, opt.Port);
        _client.Credentials = new NetworkCredential(opt.Sender, opt.Password);
        _client.EnableSsl = true;
        _from = new MailAddress(opt.Sender, "Blog App");
        Context = acc.HttpContext; 
    }

    public void SendEmailConfirmation(HttpRequest request, string receiver, string name, string token)
    {
        if (request == null)
            throw new InvalidOperationException("Request cannot be null.");

        var url = $"{request.Scheme}://{request.Host}/confirm-email?token={token}";
        using (var client = new SmtpClient(_client.Host, _client.Port))
        {
            client.Credentials = _client.Credentials;
            client.EnableSsl = _client.EnableSsl;

            var msg = new MailMessage(_from, new MailAddress(receiver))
            {
                IsBodyHtml = true,
                Subject = "Confirm Your Email Address",
                Body = $@"
                <h3>Hello {name},</h3>
                <p>Click the link below to confirm your email:</p>
                <a href='{url}'>Confirm Email</a>"
            };
            client.Send(msg);
        }
    }

}

