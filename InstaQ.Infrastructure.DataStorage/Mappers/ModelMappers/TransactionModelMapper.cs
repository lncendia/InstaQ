using InstaQ.Domain.Transactions.Entities;
using InstaQ.Infrastructure.DataStorage.Context;
using InstaQ.Infrastructure.DataStorage.Mappers.Abstractions;
using InstaQ.Infrastructure.DataStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaQ.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class TransactionModelMapper : IModelMapperUnit<TransactionModel, Transaction>
{
    private readonly ApplicationDbContext _context;

    public TransactionModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<TransactionModel> MapAsync(Transaction model)
    {
        var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == model.Id) ??
                          new TransactionModel { Id = model.Id };

        transaction.Amount = model.Amount;
        transaction.ConfirmationDate = model.ConfirmationDate;
        transaction.CreationDate = model.CreationDate;
        transaction.IsSuccessful = model.IsSuccessful;
        transaction.UserId = model.UserId;
        transaction.PaymentSystemId = model.PaymentSystemId;
        transaction.PaymentSystemUrl = model.PaymentSystemUrl;

        return transaction;
    }
}