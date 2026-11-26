using ClientManagementAPI.DTOs;
using ClientManagementAPI.Interfaces;
using FluentValidation;

namespace ClientManagementAPI.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        private readonly IOrdersService _ordertService;
        public OrderCreateDtoValidator(IOrdersService ordertService)
        {
            _ordertService = ordertService;
        }
    }
}
