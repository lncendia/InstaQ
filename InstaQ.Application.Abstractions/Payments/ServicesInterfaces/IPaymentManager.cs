
using InstaQ.Application.Abstractions.Payments.DTOs;

namespace InstaQ.Application.Abstractions.Payments.ServicesInterfaces;

public interface IPaymentManager
{
    public Task<CreatePaymentDto> CreateAsync(Guid userId, decimal amount);
    public Task ConfirmAsync(Guid userId, Guid transactionId);
}