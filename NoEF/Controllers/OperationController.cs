using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoEF.Repository;
using NoEF.Models;

namespace NoEF.Controllers
{
    public class OperationController : Controller
    {
        // GET: Operations        
        public ActionResult GetOperations()
        {
            OperRepository OpRepo = new OperRepository();
            ModelState.Clear();
            return View(OpRepo.GetOperations());
            // return View();
        }

        // GET: Operation/AddOperation
        public ActionResult AddOperation()
        {
            AccRepository AccRepo = new AccRepository();
            ViewBag.Accounts = AccRepo.GetAccounts().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Title }).ToList();
            return View();
        }

        // POST: Operation/AddOperation
        [HttpPost]
        public ActionResult AddOperation(Operation Op)
        {
            AccRepository AccRepo = new AccRepository();
            ViewBag.Accounts = AccRepo.GetAccounts().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Title }).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    OperRepository OpRepo = new OperRepository();
                    if (!OpRepo.CheckCurrencies(Op.DebetAccId, Op.CreditAccId))
                        throw new Exception("Accounts should have the same currency");
                    if (OpRepo.AddOperation(Op))
                        ViewBag.Message = "Operation details added successfully.";
                    else
                        ViewBag.Message = "Operation details failed! No enough money.";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Account/DeleteAccount/5    
        public ActionResult DeleteOperation(int id)
        {
            try
            {
                OperRepository OpRepo = new OperRepository();
                if (OpRepo.DeleteOperation(id))
                {
                    ViewBag.AlertMsg = "Operation details deleted successfully";
                }
                return RedirectToAction("GetOperations");
            }
            catch
            {
                return View();
            }
        }
    }
}