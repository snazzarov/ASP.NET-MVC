using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoEF.Models
{
    public class Operation
    {
        // [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Сумма операции")]
        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }

        [Display(Name = "Счет дебет")]
        [Required(ErrorMessage = "Debet Account is required.")]
        public int DebetAccId { get; set; }

        [Display(Name = "Счет кредит")]
        [Required(ErrorMessage = "Credit Account is required.")]
        public int CreditAccId { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
        
        public Account DebetAcc { get; set; }
        public Account CreditAcc { get; set; }
    }
}
