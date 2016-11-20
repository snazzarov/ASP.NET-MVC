using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoEF.Models
{
    public class AccountCurrency
    {
        // [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Префикс имени счета")]
        public string PrefixTitle { get; set; }

        [Required(ErrorMessage = "Account Title is required.")]
        [MaxLength(30)]
        [Display(Name = "Имя счета")]
        public string Title { get; set; }

        [Display(Name = "Остаток")]
        [Required(ErrorMessage = "Account Balance is required.")]
        public decimal Balance { get; set; }

        [Display(Name = "Валюта счета")]
        [Required(ErrorMessage = "Account Currency is required.")]
        public string CurName { get; set; }

        [Required(ErrorMessage = "Currency Id is required.")]
        public int CurrencyId { get; set; }
    }
}