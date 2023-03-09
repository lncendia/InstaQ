using System.Net;
using Newtonsoft.Json;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model.Out;
using RestSharp;
using InstaQ.Application.Abstractions.Payments.DTOs;
using InstaQ.Application.Abstractions.Payments.Exceptions;
using InstaQ.Application.Abstractions.Payments.ServicesInterfaces;

namespace InstaQ.Infrastructure.PaymentSystem.Services;

public class PaymentService : IPaymentCreatorService
{
    private readonly BillPaymentsClient _client;
    private readonly string _qiwiToken;

    public PaymentService(string qiwiToken)
    {
        _qiwiToken = qiwiToken;
        _client = BillPaymentsClientFactory.Create(qiwiToken);
    }

    public async Task<PaymentData> CreateAsync(Guid id, decimal cost)
    {
        try
        {
            var response = await _client.CreateBillAsync(
                new CreateBillInfo
                {
                    BillId = Guid.NewGuid().ToString(),
                    Amount = new MoneyAmount
                    {
                        ValueDecimal = cost,
                        CurrencyEnum = CurrencyEnum.Rub
                    },
                    ExpirationDateTime = DateTime.Now.AddDays(5),
                    Customer = new Customer
                    {
                        Account = id.ToString()
                    }
                });

            return new PaymentData(response.BillId, response.PayUrl.ToString());
        }
        catch (Exception ex)
        {
            throw new ErrorCreateBillException(ex.Message, ex);
        }
    }

    public async Task<bool> CheckAsync(string billId)
    {
        try
        {
            RestClient httpClient = new($"https://api.qiwi.com/partner/bill/v1/bills/{billId}");
            var request = new RestRequest();
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {_qiwiToken}");
            var response1 = await httpClient.ExecuteAsync(request);
            if (response1.StatusCode != HttpStatusCode.OK)
                throw new ErrorCheckBillException(response1.StatusDescription ?? "Bad status code", null);
            var response = JsonConvert.DeserializeObject<BillResponse>(response1.Content!);
            return response?.Status.ValueString == "PAID";
        }
        catch (Exception ex)
        {
            throw new ErrorCheckBillException(ex.Message, ex);
        }
    }
}