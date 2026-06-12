using System.Globalization;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;

    public TransactionService(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TransactionDashboardResponse>> GetAll(
    string walletId, string month, string year)
    {
        int m = int.Parse(month);
        int y = int.Parse(year);

        var dateFrom = new DateTime(y, m, 1);
        var dateTo = dateFrom.AddMonths(1);

        return await _repo.FindByWalletId(walletId, dateFrom, dateTo);

    }

    public async Task<List<Transaction>> Create(List<CreateTransactionDto> dto, string userId)
    {
        var thaiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var dateNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, thaiTimeZone);

        var transactions = dto.Select(item =>
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

            return new Transaction
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
        }).ToList();

        return await _repo.CreateRange(transactions);
    }
}