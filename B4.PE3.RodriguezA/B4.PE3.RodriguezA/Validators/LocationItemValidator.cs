using B4.PE3.RodriguezA.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace B4.PE3.RodriguezA.Validators
{
    public class LocationItemValidator : AbstractValidator<LocationItem>
    {
        public LocationItemValidator()
        {
            RuleFor(item => item.ItemName)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Length(3, 50)
                .WithMessage("Length must be between 3 and 50");

        }

    }
}
