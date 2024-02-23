using API_ClinicalMedics.Domain.Entities;
using FluentValidation;
using System.Text.RegularExpressions;
using API_ClinicalMedics.Domain.DTO;

namespace API_ClinicalMedics.Service.Validators
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");
                

            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("Please enter the email.")
                .NotNull().WithMessage("Please enter the email.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Please enter the password.")
                .NotNull().WithMessage("Please enter the password.");

            RuleFor(c => c.Role)
                .NotEmpty().WithMessage("Please enter the password.")
                .NotNull().WithMessage("Please enter the password.");

            RuleFor(p => p.Name).Must(BeValidName).WithMessage("Name Invalid");
            RuleFor(p => p.CPF).Must(BevalidCPF).WithMessage("Data Inválida");
        }
        private static bool BeValidName(string name)
        {
            string pattern = @"[a-zA-Z]+";
            return Regex.IsMatch(name, pattern);
        }

        private static bool BevalidCPF(string cpf)
        {
            string pattern = @"([0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2})+$";
            return Regex.IsMatch(cpf, pattern);
        }
    }
}
