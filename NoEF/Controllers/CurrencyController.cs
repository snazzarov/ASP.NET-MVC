using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoEF.Models;
using NoEF.Repository;

namespace NoEF.Controllers
{
    public class CurrencyController : Controller
    {


        /*------------------------------------------------------------------------------------------------*/

        // GET: Currency/GetCurrencys    
        public ActionResult GetCurrencies()
        {
            CurRepository CurRepo = new CurRepository();
            ModelState.Clear();
            return View(CurRepo.GetCurrencies());
        }

        // GET: Currency/AddCurrency    
        public ActionResult AddCurrency()
        {
            return View();
        }

        // POST: Currency/AddCurrency    
        [HttpPost]
        public ActionResult AddCurrency(Currency Cur)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CurRepository CurRepo = new CurRepository();

                    if (CurRepo.AddCurrency(Cur))
                    {
                        ViewBag.Message = "Currency details added successfully";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Currency/EditCurrency/5    
        public ActionResult EditCurrency(int id)
        {
            CurRepository CurRepo = new CurRepository();
            return View(CurRepo.GetCurrencies().Find(Cur => Cur.Id == id));
        }

        // POST: Currency/EditCurrency/5    
        [HttpPost]
        public ActionResult EditCurrency(int id, Currency obj)
        {
            try
            {
                CurRepository CurRepo = new CurRepository();
                CurRepo.UpdateCurrency(obj);
                return RedirectToAction("GetCurrencies");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Currency/DeleteCurrency/5    
        public ActionResult DeleteCurrency(int id)
        {
            try
            {
                CurRepository CurRepo = new CurRepository();
                if (CurRepo.DeleteCurrency(id))
                {
                    ViewBag.AlertMsg = "Currency details deleted successfully";

                }
                return RedirectToAction("GetCurrencies");
            }
            catch
            {
                return View();
            }
        }
    }
}
