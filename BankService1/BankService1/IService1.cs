using BankService1.Dto;
using BankService1.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BankService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.    
    [ServiceContract(SessionMode = SessionMode.Required)]

    public interface IService1
    {
        [OperationContract]
        Account CreateAccount(Account account);
        [OperationContract]
        AccountDto Login(string userName, string password);
        [OperationContract]
        String GetMD5(string str);
        [OperationContract]
        AccountDto GetInformation(string userName, string password);
        [OperationContract]
        List<TransactionHistory> GetTransactionHistory(string accountNumber);
        [OperationContract]
        TransactionHistoryDto Deposit(string userName, string password, double amount);
        [OperationContract]
        TransactionHistoryDto WithDraw(string userName, string password, double amount);
        [OperationContract]
        TransactionHistoryDto Transfer(string userName, string password, double amount, string receiverAccountNumber);
    }
}
