using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CDBBank.Models
{
    
    public class ElectricityBill
    {
        public int Id { get; set; }

        public float? UnitsPrice { get; set; }

        [Display(Name = "Units Consumed")]
        public int UnitsConsumed { get; set; }

        [Required]
        [Display(Name ="Bill Number")]
        public string BillNumber { get; set; }

        [DisplayFormat(DataFormatString =("{0:dd-MM-yyyy}"), ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        [Display(Name ="Date of Bill Generation")]
        public DateTime GeneratedBillDate { get; set; }

        public float Penalty { get; set; } = 40;

        [Display(Name =("Bill Amount"))]
        public float BillAmount { get; set; }

        [ForeignKey("AllUser")]
        public int UserId { get; set; }
        public AllUser BillUsers { get; set; }

        
        
    }
}
