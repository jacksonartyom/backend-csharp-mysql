using OfficeOpenXml;

public class UploadService : IUploadService
{
    private readonly ITransactionService _transactionService;

    public UploadService(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    public async Task ReadExcel(IFormFile file)
    {
        var transactionList = new List<CreateTransactionDto>();

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0]; // sheet แรก

                int rowCount = worksheet.Dimension.Rows;

                string? userId = null;

                for (int row = 2; row <= rowCount; row++) // ข้าม header
                {
                    var item = new CreateTransactionDto
                    {
                        Name = worksheet.Cells[row, 1].Text,
                        Note = worksheet.Cells[row, 2].Text,
                        Amount = decimal.Parse(worksheet.Cells[row, 3].Text),
                        Type = worksheet.Cells[row, 4].Text,
                        TransactionDate = worksheet.Cells[row, 5].Text,
                        WalletId = worksheet.Cells[row, 7].Text,
                        CategoryId = worksheet.Cells[row, 8].Text
                    };
                    userId = worksheet.Cells[row, 6].Text;
                    transactionList.Add(item);
                }

                await _transactionService.Create(transactionList, userId);
            }
        }
    }
}