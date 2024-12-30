using System;
using BlogApp.BL.DTOs.UserDtos;
using FluentValidation;

namespace BlogApp.BL.Validators.UserValidators
{
	public class UserCreateDtoValidator : AbstractValidator<RegisterDto>
	{
		public UserCreateDtoValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.NotNull()
				.EmailAddress();
			RuleFor(x => x.Username)
				.NotNull()
				.NotEmpty(); 
		}
	}
}

