using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankService1.Entity
{
    public class TransactionHistory
    {
        [Key]
        public string TransactionId { get; set; }
        public double Amount { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
        public int TransactionType { get; set; }
    }
}