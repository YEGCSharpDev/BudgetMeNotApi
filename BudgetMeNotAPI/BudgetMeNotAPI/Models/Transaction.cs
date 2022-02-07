using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetMeNotAPI.Models
{
    public class Transaction
    {
        public int Txn_ID { get; set; }
        public int Category_ID { get; set; }
        public int Sub_Category_ID { get; set; }
        public decimal Amount { get; set; }
        public string Direction { get; set; }
        public string Comment { get; set; }
        public int Attachment_ID { get; set; }
        public int Account_ID { get; set; }
        public string Soft_Deleted { get; set; }
        public DateTime Create_TS { get; set; }
        public DateTime Update_TS { get; set; }
    }
}