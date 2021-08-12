using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDBBank.Models
{
    public class NeftRecords
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string ConfirmAccountNo { get; set; }
        public string BeneficiaryName { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }
       // public BankUser BankUsers { get; set; }
    }
}
