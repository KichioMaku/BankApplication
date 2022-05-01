using BankApp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BankApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            List<User> users = File.ReadAllLines("osoby.csv").Skip(1).Select(x => CreateUser(x)).ToList();
            List<Account> accounts = File.ReadAllLines("konta.csv").Skip(1).Select(x => CreateAccount(x,users)).ToList();
            foreach (var user in users)
            {
                AssignAccountToUser(accounts, user);
            
            }

            #region Zadanie 1
            Console.WriteLine("\n-------Zadanie 1---------\n");
            WriteUserAccount(1, users);
            WriteUserAccount(2, users);
            WriteUserAccount(3, users);
            #endregion
            #region Zadanie 2
            Console.WriteLine("\n-------Zadanie 2---------\n");
            Withdrawal(1, 1000, users, 12441231451);
            Payment(1, 5000, users, 12441231451);
            #endregion

            #region Zadanie 3
            Console.WriteLine("\n-------Zadanie 3---------\n");
            WriteLockedAccounts(accounts);
            #endregion

            #region Zadanie 4
            Console.WriteLine("\n-------Zadanie 4---------\n");
            AccountsReport(users);

            #endregion

        }



        public static User CreateUser(string userCSV)
        {
            string[] values = userCSV.Split(';');
            User user = new User();
            user.UserId = int.Parse(values[0]);
            user.FirstName = values[1];
            user.LastName = values[2];
            user.Pesel = values[3];
            user.Adress = values[4];
            return user;
        }

        public static Account CreateAccount(string accountCSV, List<User> users)
        {
            string[] values = accountCSV.Split(';');
            Account account = new Account();
            User newUser = users.FirstOrDefault(x => x.UserId == int.Parse(values[1]));
            account.AccountId = long.Parse(values[0]);
            account.User = newUser;
            account.CurrencyType = EnumCurrencyType(values[2]);
            account.Balance = decimal.Parse(values[3]);
            account.AccountLock = AccoutLock(values[4]);
            return account;

        }

        public static Enums.CurrencyType EnumCurrencyType(string x)
        {
            if (x == "PLN")
                return Enums.CurrencyType.PLN;
            if (x == "EUR")
                return Enums.CurrencyType.EUR;
            if (x == "USD")
                return Enums.CurrencyType.USD;
            return Enums.CurrencyType.PLN;
        }
        public static bool AccoutLock(string x)
        {
            if (x == "NIE")
                return false;
            else 
                return true;
        }
        public static void AssignAccountToUser(List<Account> accounts, User user)
        {
            var listOfAccounts = accounts.Where(x=> x.User.UserId == user.UserId).ToList();
            user.Accounts = listOfAccounts;
        }
        public static void WriteLockedAccounts(List<Account> list)
        {
            List<Account> lockedAccounts = list.Where(x => x.AccountLock == true).ToList();
            foreach (var account in lockedAccounts)
            {
                Console.WriteLine("Id zablokowanego konta {0}, imię i nazwisko osoby z zablokowanym kontem: {1} {2}, waluta konta: {3}\n", account.AccountId, account.User.FirstName, account.User.LastName, account.CurrencyType);
            }
        }
        public static void WriteUserAccount(int i, List<User> list)
        {
            User newUser = list.FirstOrDefault(x => x.UserId == i);
            List<Account> accountList = newUser.Accounts.ToList();
            
            Console.WriteLine(newUser.FirstName + " " + newUser.LastName + " id="+ newUser.UserId);
            Console.WriteLine("Id, waluta i bilans posiadanych kont:\n");
            foreach (var account in accountList)
            {
                Console.WriteLine($"{account.AccountId}, {account.Balance} {account.CurrencyType}\n");
            }
            
        }

        public static void Withdrawal(int id, decimal value, List<User> list, long AccountId)
        {
            User user = list.FirstOrDefault(x => x.UserId == id);
            Account account = user.Accounts.FirstOrDefault(x => x.AccountId == AccountId);
            
            if (value <= account.Balance)
            {
                account.Balance -= value;
            }
            else
            {
                Console.WriteLine("Na koncie nie ma tylu środków");
            }
            Console.WriteLine($"Po wypłacie {value} {account.CurrencyType} na koncie pozostało: {account.Balance} {account.CurrencyType}");
        }

        public static void Payment(int id, decimal value, List<User> list, long AccountId)
        {
            User user = list.FirstOrDefault(x => x.UserId == id);
            Account account = user.Accounts.FirstOrDefault(x => x.AccountId == AccountId);
            account.Balance += value;
            Console.WriteLine($"Po wpłacie posiadasz na koncie {account.Balance}");
        }

        public static void AccountsReport(List<User> users)
        {
            
            foreach (var user in users)
            {
                List<Account> list = user.Accounts.ToList();
                Console.WriteLine($"Id:{user.UserId} Name:{user.FirstName} {user.LastName} Pesel:{user.Pesel} Adress:{user.Adress}");
                foreach (var account in list)
                {
                    Console.WriteLine($"{account.AccountId}, {account.Balance} {account.CurrencyType}");
                }
                Console.WriteLine();
            }
        }


    }

}


