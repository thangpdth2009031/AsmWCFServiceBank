using BankService1.Dto;
using BankService1.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankService1.Data
{
    public class BankServiceContext:DbContext
    {
        public BankServiceContext():base("name=MyConnectionString")
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
    }
}