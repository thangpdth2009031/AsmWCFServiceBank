using BankService1.Data;
using BankService1.Dto;
using BankService1.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.ModelBinding;


namespace BankService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        private BankServiceContext db = new BankServiceContext();
        public Account CreateAccount(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
            return account;
        }
        
        public AccountDto GetInformation(string userName, string password)
        {
            var f_password = GetMD5(password);
            var account = db.Accounts.Where(s => s.UserName.Equals(userName) && s.Password.Equals(f_password)).FirstOrDefault();
            if (account != null)
            {
                var accountDto = new AccountDto();
                accountDto.AccountNumber = account.AccountNumber;
                accountDto.FirstName = account.FirstName;
                accountDto.LastName = account.LastName;
                accountDto.UserName = account.UserName;
                accountDto.Phone = account.Phone;
                accountDto.IdentityNumber = account.IdentityNumber;
                accountDto.Address = account.Address;
                accountDto.Email = account.Email;
                accountDto.Balance = account.Balance;
                accountDto.Birthday = account.Birthday;
                accountDto.Status = account.Status;
                return accountDto;                
            }
            return null;
        }
        public TransactionHistoryDto Transfer(string userName, string password, double amount, string receiverAccountNumber)
        {
            var f_password = GetMD5(password);
            var account = db.Accounts.Where(s => s.UserName.Equals(userName) && s.Password.Equals(f_password)).FirstOrDefault();
            var accountNumber = db.Accounts.Where(s => s.AccountNumber.Equals(receiverAccountNumber)).FirstOrDefault();
            if (account != null)
            {
                if (accountNumber != null)
                {
                    if (account.Balance < amount)
                    {
                        return null;
                    } else
                    {
                        using (DbContextTransaction transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                account.Balance -= amount;
                                accountNumber.Balance += amount;
                                var transactionHistory = new TransactionHistory()
                                {
                                    TransactionId = Guid.NewGuid().ToString(),
                                    Amount = amount,
                                    SenderAccountNumber = account.AccountNumber,
                                    ReceiverAccountNumber = accountNumber.AccountNumber,
                                    Message = "Transfer",
                                    CreateAt = DateTime.Now,
                                    UpdatedAt = DateTime.Now,
                                    Status = 1,
                                    TransactionType = 3,
                                };
                                db.Entry(account).State = EntityState.Modified;
                                db.Entry(accountNumber).State = EntityState.Modified;
                                db.TransactionHistories.Add(transactionHistory);
                                db.SaveChanges();
                                transaction.Commit();
                                return new TransactionHistoryDto()
                                {
                                    TransactionId = transactionHistory.TransactionId,
                                    Amount = transactionHistory.Amount,
                                    SenderAccountNumber = transactionHistory.SenderAccountNumber,
                                    ReceiverAccountNumber = transactionHistory.ReceiverAccountNumber,
                                    Message = transactionHistory.Message,
                                    CreateAt = transactionHistory.CreateAt,
                                    UpdatedAt = transactionHistory.UpdatedAt,
                                    Status = transactionHistory.Status,
                                    TransactionType = transactionHistory.TransactionType,
                                };
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                            }
                        }
                    }
                }                
            }
            return null;
        }        
        

        public TransactionHistoryDto Deposit(string userName, string password, double amount)
        {
            var f_password = GetMD5(password);
            var account = db.Accounts.Where(s => s.UserName.Equals(userName) && s.Password.Equals(f_password)).FirstOrDefault();
            if (account != null)
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        account.Balance += amount;
                        var transactionHistory = new TransactionHistory()
                        {
                            TransactionId = Guid.NewGuid().ToString(),
                            Amount = amount,
                            SenderAccountNumber = account.AccountNumber,
                            ReceiverAccountNumber = account.AccountNumber,
                            Message = "Deposit",
                            CreateAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            Status = 1,
                            TransactionType = 2,
                        };
                        db.Entry(account).State = EntityState.Modified;
                        db.TransactionHistories.Add(transactionHistory);
                        db.SaveChanges();
                        transaction.Commit();
                        return new TransactionHistoryDto()
                        {
                            TransactionId = transactionHistory.TransactionId,
                            Amount = transactionHistory.Amount,
                            SenderAccountNumber = transactionHistory.SenderAccountNumber,
                            ReceiverAccountNumber = transactionHistory.ReceiverAccountNumber,
                            Message = transactionHistory.Message,
                            CreateAt = transactionHistory.CreateAt,
                            UpdatedAt = transactionHistory.UpdatedAt,
                            Status = transactionHistory.Status,
                            TransactionType = transactionHistory.TransactionType,
                        };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return null;
        }

        public TransactionHistoryDto WithDraw(string userName, string password, double amount)
        {
            var f_password = GetMD5(password);
            var account = db.Accounts.Where(s => s.UserName.Equals(userName) && s.Password.Equals(f_password)).FirstOrDefault();
            if (account != null)
            {
                Debug.WriteLine(account.UserName);
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        account.Balance -= amount;
                        var transactionHistory = new TransactionHistory()
                        {
                            TransactionId = Guid.NewGuid().ToString(),
                            Amount = amount,
                            SenderAccountNumber = account.AccountNumber,
                            ReceiverAccountNumber = account.AccountNumber,
                            Message = "WithDraw",
                            CreateAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            Status = 1,
                            TransactionType = 1,
                        };
                        db.Entry(account).State = EntityState.Modified;
                        db.TransactionHistories.Add(transactionHistory);
                        db.SaveChanges();
                        transaction.Commit();
                        return new TransactionHistoryDto()
                        {
                            TransactionId = transactionHistory.TransactionId,
                            Amount = transactionHistory.Amount,
                            SenderAccountNumber = transactionHistory.SenderAccountNumber,
                            ReceiverAccountNumber = transactionHistory.ReceiverAccountNumber,
                            Message = transactionHistory.Message,
                            CreateAt = transactionHistory.CreateAt,
                            UpdatedAt = transactionHistory.UpdatedAt,
                            Status = transactionHistory.Status,
                            TransactionType = transactionHistory.TransactionType,
                        };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return null;
        }
        public string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;

        }
        public AccountDto Login(string userName, string password)
        {
            var f_password = GetMD5(password);
            var account = db.Accounts.Where(s => s.UserName.Equals(userName) && s.Password.Equals(f_password)).FirstOrDefault();
            if (account != null)
            {
                var accountDto = new AccountDto();
                accountDto.AccountNumber = account.AccountNumber;
                accountDto.LastName = account.LastName;
                accountDto.FirstName = account.FirstName;
                accountDto.UserName = account.UserName;
                accountDto.Phone = account.Phone;
                accountDto.IdentityNumber = account.IdentityNumber;
                accountDto.Address = account.Address;
                accountDto.Email = account.Email;
                accountDto.Balance = account.Balance;
                accountDto.Birthday = account.Birthday;
                accountDto.Status = account.Status;
                return accountDto;
            }
            else
            {
                return null;
            }
        }

        public List<TransactionHistory> GetTransactionHistory(string accountNumber)
        {
            return db.TransactionHistories.Where(
               m => m.ReceiverAccountNumber == accountNumber
                 || m.SenderAccountNumber == accountNumber).ToList();
        }
    }
}
