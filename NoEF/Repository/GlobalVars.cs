using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoEF.Repository
{
    public static class GlobalVars
    {
        // readonly variable
        public static string IncomeAccountPrefix
        {
            get
            {
                return "Income";
            }
        }
    }
}