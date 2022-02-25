using BankClient1.BankService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankClient1.Controllers
{
    
    
    public class BanksController : Controller
    {
        private Random _random = new Random();
        BankService.Service1Client service = new BankService.Service1Client();          
        [HttpGet]
        public ActionResult GetInformation()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var userName = Session["UserName"].ToString();
                var password = Session["Password"].ToString();
                List<AccountDto> accountDtos = new List<AccountDto>();
                var acc = service.GetInformation(userName, password);
                accountDtos.Add(acc);                
                return View(accountDtos);
            }                       
        }       
        public ActionResult GetTransaction()
        {
            if (Session["UserName"] == null)
            {
                TempData["name"] = "Vui lòng đăng nhập để tiếp tục!";
                return RedirectToAction("Login");
            }
            else
            {
                var userName = Session["UserName"].ToString();
                var password = Session["Password"].ToString();
                var acc = service.GetInformation(userName, password);
                return View(service.GetTransactionHistory(acc.AccountNumber));
            }                          
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AccountDto account)
        {
            var accountNumber = _random.Next(1000, 9999).ToString() + _random.Next(1000, 9999).ToString() + _random.Next(1000, 9999).ToString() + _random.Next(1000, 9999).ToString();
            var accounts = new Account()
            {
                AccountNumber = accountNumber,
                FirstName = account.FirstName,
                LastName = account.LastName,
                UserName = account.UserName,
                Password = service.GetMD5(account.Password),
                PasswordComfirm = account.PasswordConfirm,
                Address = account.Address,
                Phone = account.Phone,
                Email = account.Email,
                IdentityNumber = account.IdentityNumber,
                Birthday = account.Birthday,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = 1,
            };
            service.CreateAccount(accounts);
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
            var acc = service.Login(userName, password);
            if (acc != null)
            {               
                Session["AccountNumber"] = acc.AccountNumber;
                Session["UserName"] = acc.UserName;                
                Session["FullName"] = acc.LastName + acc.FirstName;             
                Session["Password"] = password;
                return RedirectToAction("GetInformation");
            }
            return View("Login");
        }


        public ActionResult Deposit()
        {
            if (Session["UserName"] == null)
            {
                TempData["name"] = "Vui lòng đăng nhập để tiếp tục!";
                return RedirectToAction("Login");
            }
            else
            {                
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(double amount)
        {
            var userName = Session["UserName"].ToString();
            var password = Session["Password"].ToString();
            var account = service.Deposit(userName, password, amount);
            if (account != null)
            {
                TempData["name"] = "Nạp tiền thành công";
                return RedirectToAction("GetInformation");
            } else
            {
                TempData["name"] = "Vui lòng đăng nhập để tiếp tục!";
                return RedirectToAction("Login");
            }            
        }
        public ActionResult WithDraw()
        {
            if (Session["UserName"] == null)
            {
                TempData["name"] = "Vui lòng đăng nhập để tiếp tục!";
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WithDraw(double amount)
        {
            var userName = Session["UserName"].ToString();
            var password = Session["Password"].ToString();
            var account = service.WithDraw(userName, password, amount);
            if (account != null)
            {
                TempData["name"] = "Rút tiền thành công";
                return RedirectToAction("GetInformation");
            }
            else
            {
                TempData["name"] = "Vui lòng đăng nhập để tiếp tục!";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Transfer()
        {
            if (Session["UserName"] == null)
            {
                TempData["name"] = "Vui lòng đăng nhập để tiếp tục!";
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(double amount, string receiverAccountNumber)
        {
            var userName = Session["UserName"].ToString();
            var password = Session["Password"].ToString();
            var account = service.Transfer(userName, password, amount, receiverAccountNumber);
            if (account != null)
            {
                TempData["name"] = "Chuyển tiền thành công";
                return RedirectToAction("GetInformation");
            }
            else
            {
                TempData["name"] = "Vui lòng đăng nhập để tiếp tục!";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("Login");
        }
    }
}