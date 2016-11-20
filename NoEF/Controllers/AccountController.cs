using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoEF.Models;
using NoEF.Repository;

namespace NoEF.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account/GetAccounts    
        public ActionResult GetAccounts()
        {
            AccRepository AccRepo = new AccRepository();
            ModelState.Clear();
            // ViewBag.Account = GlobalVars.IncomeAccountPrefix;
            return View(AccRepo.GetAccounts());
        }

        // GET: Account/AddAccount    
        public ActionResult AddCurrencyAccount()
        {
            CurRepository CurRepo = new CurRepository();
            ViewBag.Currencies = CurRepo.GetCurrencies().Select(x=>new SelectListItem {Value = x.Id.ToString(),Text = x.Name}).ToList();
            ViewBag.Account = GlobalVars.IncomeAccountPrefix;
            return View();
        }

        // POST: Account/AddAccount    
        [HttpPost]
        public ActionResult AddCurrencyAccount(Account Acc)
        {
            CurRepository CurRepo = new CurRepository();
            ViewBag.Currencies = CurRepo.GetCurrencies().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    AccRepository AccRepo = new AccRepository();
                    Acc.Title = Acc.PrefixTitle + Acc.Title;
                    if (AccRepo.AddAccount(Acc, 1))
                    {
                        ViewBag.Message = "Account details added successfully";
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Account/AddAccount    
        public ActionResult AddAccount()
        {
            CurRepository CurRepo = new CurRepository();
            ViewBag.Currencies = CurRepo.GetCurrencies().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View();
        }
        // POST: Account/AddAccount    
        [HttpPost]
        public ActionResult AddAccount(Account Acc)
        {
            CurRepository CurRepo = new CurRepository();
            ViewBag.Currencies = CurRepo.GetCurrencies().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    AccRepository AccRepo = new AccRepository();
                    if (Acc.Title.Length >= 5 && Acc.Title.Substring(0, 5).Equals("Income") == true)
                        throw new Exception("Name should't include 'Income' prefix");
                    if (AccRepo.AddAccount(Acc, 0))
                    {
                        ViewBag.Message = "Account details added successfully";
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

        // GET: Account/EditAccount/5    
        public ActionResult EditAccount(int id)
        {
            CurRepository CurRepo = new CurRepository();
            ViewBag.Currencies = CurRepo.GetCurrencies().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            AccRepository AccRepo = new AccRepository();
            return View(AccRepo.GetAccounts().Find(Acc => Acc.Id == id));
        }

        // POST: Account/EditAccount/5    
        [HttpPost]
        public ActionResult EditAccount(int id, Account obj)
        {
            CurRepository CurRepo = new CurRepository();
            ViewBag.Currencies = CurRepo.GetCurrencies().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            try
            {
                AccRepository AccRepo = new AccRepository();
                AccRepo.UpdateAccount(obj);
                return RedirectToAction("GetAccounts");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult DeleteAccount(int Id)
        {
            AccRepository AccRepo = new AccRepository();
            return View(AccRepo.GetAccounts().Find(Acc => Acc.Id == Id));
            //return View();
        }
        // GET: Account/DeleteAccount/5    
        [HttpPost]
        public ActionResult DeleteAccount(int Id, Account obj)
        {
            try
            {
                AccRepository AccRepo = new AccRepository();
                if (AccRepo.DeleteAccount(obj.Id))
                    ViewBag.AlertMsg = "Account details deleted successfully";
                return RedirectToAction("GetAccounts");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.Message = "Account has references in Operation table. You have to delete corresponding operations";
                return View();
            }
        }
    }
}
