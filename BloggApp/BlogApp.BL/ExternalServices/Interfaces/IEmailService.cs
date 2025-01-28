using System;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.ExternalServices.Interfaces;

public interface IEmailService
{
	Task<string> EmailVereficationToken(string email);	
	Task<string> SendEmailVereficationAsync(string email);
    Task<bool> VerifyEmailAsync(string email, int code);
}

