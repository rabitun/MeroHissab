using MeroHissab.Services;
using System.Text.Json;
using System.Transactions;
namespace MeroHissab.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly string _filePath;
        private List<Transaction> _transactions = new();

        public TransactionService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "transactions.json");
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return _transactions;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _transactions.Add(transaction);
            await SaveToFileAsync();
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            var existing = _transactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (existing != null)
            {
                _transactions.Remove(existing);
                _transactions.Add(transaction);
                await SaveToFileAsync();
            }
        }

        public async Task DeleteTransactionAsync(string id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                _transactions.Remove(transaction);
                await SaveToFileAsync();
            }
        }

        public async Task SaveToFileAsync()
        {
            var json = JsonSerializer.Serialize(_transactions);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task LoadFromFileAsync()
        {
            if (File.Exists(_filePath))
            {
                var json = await File.ReadAllTextAsync(_filePath);
                _transactions = JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
            }
        }
    }
}