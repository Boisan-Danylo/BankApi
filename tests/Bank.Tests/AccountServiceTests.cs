using Bank.Application.Services;
using Bank.Domain.Errors;
using Bank.Infrastructure;

namespace Bank.Tests;

public class AccountServiceTests
{
    [Fact]
    public void Create_ValidAccount_Succeeds()
    {
        var repo = new InMemoryAccountRepository();
        var service = new AccountService(repo);

        var account = service.Create("John Doe", 100);

        Assert.NotNull(account);
        Assert.Equal("John Doe", account.OwnerName);
        Assert.Equal(100, account.Balance);
        Assert.Equal("USD", account.Currency);
        Assert.Equal(account, repo.Get(account.AccountNumber));
    }

    [Fact]
    public void Create_Throws_On_EmptyOwner()
    {
        var repo = new InMemoryAccountRepository();
        var service = new AccountService(repo);

        Assert.Throws<ValidationException>(() => service.Create("", 100));
        Assert.Throws<ValidationException>(() => service.Create("   ", 100));
    }

    [Fact]
    public void Create_Throws_On_NegativeBalance()
    {
        var repo = new InMemoryAccountRepository();
        var service = new AccountService(repo);

        Assert.Throws<ValidationException>(() => service.Create("John Doe", -1));
    }

    [Fact]
    public void Get_Returns_Account()
    {
        var repo = new InMemoryAccountRepository();
        var service = new AccountService(repo);

        var account = service.Create("Jane", 50);
        var fetched = service.Get(account.AccountNumber);

        Assert.Equal(account, fetched);
    }

    [Fact]
    public void List_Returns_AllAccounts_Sorted()
    {
        var repo = new InMemoryAccountRepository();
        var service = new AccountService(repo);

        var a1 = service.Create("A", 10);
        var a2 = service.Create("B", 20);

        var list = service.List();

        Assert.Equal(2, list.Count);
        Assert.True(String.Compare(list.First().AccountNumber, list.Last().AccountNumber, StringComparison.Ordinal) < 0);
    }
}