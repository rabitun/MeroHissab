using System.Transactions;

public interface ITransactionService
{
    Task<List<Transaction>> GetTransactionsAsync();

    Task AddTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(Transaction transaction);
    Task DeleteTransactionAsync(string id);
    Task SaveToFileAsync();
    Task LoadFromFileAsync();
}