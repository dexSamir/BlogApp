using System;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.ExternalServices.Interfaces;

public interface IEmailService
{
	void SendEmailConfirmation(HttpRequest request, string reciever, string name, string token);	
}

