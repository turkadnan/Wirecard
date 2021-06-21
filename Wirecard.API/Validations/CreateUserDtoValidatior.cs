using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Wirecard.Core.Dtos;

namespace Wirecard.API.Validations
{
    public class CreateUserDtoValidatior : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidatior()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress();
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
