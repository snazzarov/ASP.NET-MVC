using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoEF.Models
{
    public class OperationAccount
    {
        // [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Account Title is required.")]
        [MaxLength(30)]
        [Display(Name = "Сумма операции")]
        public decimal Amount { get; set; }

        
        [Required(ErrorMessage = "Debet Account is required.")]
        public int DebetAccId { get; set; }

        [Required(ErrorMessage = "Credit Account is required.")]
        public int CreditAccId { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
        [Display(Name = "Счет дебет")]
        public string DebetAcc { get; set; }
        [Display(Name = "Счет кредит")]
        public string CreditAcc { get; set; }
    }
}
