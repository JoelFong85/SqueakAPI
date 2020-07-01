using FluentValidation;
using Squeak.Models.CreateGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squeak.Validators.CreateGame
{
    public class CreateSessionRequestModelValidator : AbstractValidator<CreateSessionRequestModel>
    {
        public CreateSessionRequestModelValidator()
        {
            RuleFor(model => model.Name).NotNull().NotEmpty();
            RuleFor(model => model.DeviceId).NotNull().NotEmpty();
            RuleFor(model => model.AppId).NotNull().NotEmpty();            
        }
    }
}