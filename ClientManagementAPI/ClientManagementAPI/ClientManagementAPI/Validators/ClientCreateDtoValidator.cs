using ClientManagementAPI.DTO.ClientDTO;
using ClientManagementAPI.Interfaces;
using ClientManagementAPI.Services;
using FluentValidation;

namespace ClientManagementAPI.Validators
{
    public class ClientCreateDtoValidator : AbstractValidator<ClientCreateDto>
    {
        private readonly IClientsService _clientService;
        public ClientCreateDtoValidator(IClientsService clientService)
        {
            _clientService = clientService;

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()                
                .WithMessage("A client with this Email already exists.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("FullName is required.")
                .MinimumLength(2)
                .WithMessage("FullName must contain at least 2 characters.")
                .MaximumLength(100)
                .WithMessage("FullName cannot exceed 100 characters.");
            
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone is required.")
                .Matches(@"^\+?[0-9\s\-\(\)]{7,20}$")
                .WithMessage("Phone must contain a valid phone number.");
        }
    }

}
