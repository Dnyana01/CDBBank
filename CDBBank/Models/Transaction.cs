using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CDBBank.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public double? Withdrawal { get; set; }
        public double? Deposit { get; set; }

        public double ClosingBalance { get; set; }

        [Display(Name ="Date of Transaction")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }

        public string Narration { get; set; }
        public int UserId { get; set; }
        public BankUser BankUsers { get; set; }

    }
}
