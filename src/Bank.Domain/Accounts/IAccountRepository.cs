namespace Bank.Domain.Accounts;

public interface IAccountRepository
{
    // CRUD Operations for storing and retrieving Account entities
    // Imitating a database repository
    // In a real-world application, this would be an interface with a database context
    Account Add(Account account);
    Account Get(string number);
    IReadOnlyCollection<Account> List();
    void Update(Account account);
}