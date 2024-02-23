using API_ClinicalMedics.Domain.Entities;
using FluentValidation;

namespace API_ClinicalMedics.Service.Validators
{
    public class AttachamentValidator :  AbstractValidator< Attachaments>
    {
        public AttachamentValidator()
        {
            RuleFor(c => c.FileName)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");


            RuleFor(c => c.ContentPDF)
                .NotEmpty().WithMessage("Please enter the email.")
                .NotNull().WithMessage("Please enter the email.");

            RuleFor(c => c.TypeDocument)
                .NotEmpty().WithMessage("Please enter the password.")
                .NotNull().WithMessage("Please enter the password.");
        }
    }
}
