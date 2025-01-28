using System;
using System.Net;
using System.Net.Mail;
using BlogApp.BL.DTOs.Options;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BlogApp.BL.ExternalServices.Implements;

public class EmailService : IEmailService
{
    readonly IOptions<SmtpOptions> _options;
    readonly IMemoryCache _cache; 
    readonly BlogAppDbContext _context;
    readonly IConfiguration _configuration;

    public EmailService(
        IOptions<SmtpOptions> options,
        BlogAppDbContext context,
        IConfiguration configuration,
        IMemoryCache cache)
    {
        _cache = cache;
        _options = options;
        _configuration = configuration; 
        _context = context;

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

