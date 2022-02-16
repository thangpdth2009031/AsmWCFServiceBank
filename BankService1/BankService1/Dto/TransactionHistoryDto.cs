using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BankService1.Dto
{
    [DataContract]
    public class TransactionHistoryDto
    {
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public string SenderAccountNumber { get; set; }
        [DataMember]
        public string ReceiverAccountNumber { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public DateTime CreateAt { get; set; }
        [DataMember]
        public DateTime UpdatedAt { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public int TransactionType { get; set; }
    }
}