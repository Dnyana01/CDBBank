using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CDBBank.Models
{
    public class AllUser
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name="Account Number")]
        [RegularExpression("^[A-Z]{4}[0-9A-Z]{7}", ErrorMessage = "Please Enter Valid Account No")]
        public string AccountNo { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }


        public long? Mobile { get; set; }
        public double? Balance { get; set; }

        [StringLength(10, ErrorMessage ="Enter a Valid Consumer Number")]
        public string ElectricityConsumerNum { get; set; }
    }
}
