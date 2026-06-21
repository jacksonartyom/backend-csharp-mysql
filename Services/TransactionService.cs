using System.Globalization;
using Microsoft.Extensions.Logging;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;

    private readonly IWalletRepository _walletRepo;

    private readonly ILogger<WalletController> _logger;

    public TransactionService(ITransactionRepository repo, IWalletRepository walletRepo, ILogger<WalletController> logger)
    {
        _repo = repo;
        _walletRepo = walletRepo;
        _logger = logger;
    }

    public async Task<List<TransactionResponse>> GetByMonthYear(
    string walletId, string month, string year)
    {
        int m = int.Parse(month);
        int y = int.Parse(year);

        var dateFrom = new DateTime(y, m, 1);
        var dateTo = dateFrom.AddMonths(1);

        var result = await _repo.FindByWalletId(walletId, dateFrom, dateTo);
        var responseList = new List<TransactionResponse>();
        foreach (var item in result)
        {
            var respone = new TransactionResponse
            {
                TransactionId = item.TransactionId,
                Name = item.Name,
                Amount = item.Amount,
                Note = item.Note,
                Type = item.Type,
                TransactionDate = item.TransactionDate,
                UserId = item.UserId,
                WalletId = item.WalletId,
            };

            var category = new CategoryResponse
            {
                CategoryId = item.CategoryId,
                Name = item.CategoryName
            };
            respone.Category = category;

            responseList.Add(respone);
        }
        ;

        return responseList;

    }

    public async Task<List<Transaction>> Create(List<CreateTransactionDto> dto, string? userId)
    {
        var thaiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var dateNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, thaiTimeZone);

        decimal newBalance = 0;
        string? walletId = null;

        var transactions = new List<Transaction>();

        try
        {
            foreach (var item in dto)
            {
                if (!DateTime.TryParseExact(
                        item.TransactionDate,
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime date))
                {
                    throw new Exception($"Invalid date format: {item.TransactionDate}");
                }

                var transaction = new Transaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    Name = item.Name,
                    Amount = item.Amount,
                    Note = item.Note,
                    Type = item.Type,
                    TransactionDate = date,
                    UserId = userId,
                    WalletId = item.WalletId,
                    CategoryId = item.CategoryId,
                    CreateAt = dateNow,
                    UpdatedAt = dateNow
                };

                transactions.Add(transaction);

                walletId = transaction.WalletId;

                if (transaction.Type == "income")
                {
                    newBalance += transaction.Amount;
                }
                else
                {
                    newBalance -= transaction.Amount;
                }
            }
            await _repo.CreateRange(transactions);

            if (!string.IsNullOrEmpty(walletId))
            {
                await _walletRepo.UpdateBalance(walletId, newBalance, dateNow);
            }

            return transactions;
        }
        catch (Exception ex)
        {
            throw new BadRequestException(ex.ToString());
        }
    }

}