using InstaQ.Application.Abstractions.Payments.DTOs;

namespace InstaQ.Application.Abstractions.Payments.ServicesInterfaces;

public interface IPaymentCreatorService
{
    Task<PaymentData> CreateAsync(Guid id, decimal cost);
    Task<bool> CheckAsync(string billId);
}