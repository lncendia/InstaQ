﻿using System.ComponentModel.DataAnnotations.Schema;
using InstaQ.Infrastructure.DataStorage.Models.Abstractions;

namespace InstaQ.Infrastructure.DataStorage.Models;

public class TransactionModel : IAggregateModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
    [Column(TypeName = "nvarchar(120)")] public string PaymentSystemId { get; set; } = null!;
    [Column(TypeName = "nvarchar(120)")] public string PaymentSystemUrl { get; set; } = null!;
    [Column(TypeName = "money")] public decimal Amount { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ConfirmationDate { get; set; }
    public bool IsSuccessful { get; set; }
}