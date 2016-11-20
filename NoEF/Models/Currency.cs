using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoEF.Models
{
    public class Currency
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Currency Name is required.")]
        [Display(Name = "Валюта")]
        public string Name { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}