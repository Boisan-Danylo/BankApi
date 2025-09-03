using Bank.Application.Services;
using Bank.Domain.Errors;
using Bank.Infrastructure;

namespace Bank.Tests;

public class AccountTransactionsServiceTests
{
    [Fact]
    public void Deposit_AddsToBalance()
    {
        var repo = new InMemoryAccountRepository();
        var crud = new AccountService(repo);
        var tx = new AccountTransactionsService(repo);

        var acc = crud.Create("A", 100);
        tx.Deposit(acc.AccountNumber, 50);

        Assert.Equal(150, crud.Get(acc.AccountNumber).Balance);
    }

    [Fact]
    public void Withdraw_SubtractsFromBalance()
    {
        var repo = new InMemoryAccountRepository();
        var crud = new AccountService(repo);
        var tx = new AccountTransactionsService(repo);

        var acc = crud.Create("A", 100);
        tx.Withdraw(acc.AccountNumber, 40);

        Assert.Equal(60, crud.Get(acc.AccountNumber).Balance);
    }

    [Fact]
    public void Withdraw_Throws_On_InsufficientFunds()
    {
        var repo = new InMemoryAccountRepository();
        var crud = new AccountService(repo);
        var tx = new AccountTransactionsService(repo);

        var acc = crud.Create("A", 10);
        Assert.Throws<InsufficientFundsException>(() => tx.Withdraw(acc.AccountNumber, 20));
    }

    [Fact]
    public void Transfer_MovesFunds_BetweenAccounts()
    {
        var repo = new InMemoryAccountRepository();
        var crud = new AccountService(repo);
        var tx = new AccountTransactionsService(repo);

        var a = crud.Create("A", 100);
        var b = crud.Create("B", 50);

        tx.Transfer(a.AccountNumber, b.AccountNumber, 30);

        Assert.Equal(70, crud.Get(a.AccountNumber).Balance);
        Assert.Equal(80, crud.Get(b.AccountNumber).Balance);
    }

    [Fact]
    public void Transfer_Throws_On_SameAccount()
    {
        var repo = new InMemoryAccountRepository();
        var crud = new AccountService(repo);
        var tx = new AccountTransactionsService(repo);

        var a = crud.Create("A", 100);

        Assert.Throws<ValidationException>(() => tx.Transfer(a.AccountNumber, a.AccountNumber, 10));
    }

    [Fact]
    public void Transfer_Throws_On_NonPositiveAmount()
    {
        var repo = new InMemoryAccountRepository();
        var crud = new AccountService(repo);
        var tx = new AccountTransactionsService(repo);

        var a = crud.Create("A", 100);
        var b = crud.Create("B", 50);

        Assert.Throws<ValidationException>(() => tx.Transfer(a.AccountNumber, b.AccountNumber, 0));
        Assert.Throws<ValidationException>(() => tx.Transfer(a.AccountNumber, b.AccountNumber, -5));
    }

    [Fact]
    public void Transfer_Throws_On_CurrencyMismatch()
    {
        var repo = new InMemoryAccountRepository();
        var crud = new AccountService(repo);
        var tx = new AccountTransactionsService(repo);

        var a = crud.Create("A", 100);
        var b = crud.Create("B", 50, "EUR");

        Assert.Throws<ValidationException>(() => tx.Transfer(a.AccountNumber, b.AccountNumber, 10));
    }
}