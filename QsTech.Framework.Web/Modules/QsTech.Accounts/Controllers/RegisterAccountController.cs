using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Accounts.Models.AccountServices;
using QsTech.Accounts.Models.Accounts;
using QsTech.Accounts.Models.Registers;

namespace QsTech.Accounts.Controllers
{
    public class RegisterAccountController : Controller
    {
        private readonly IAccountService _accountService;

        public RegisterAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        #region  申请帐号管理
        public ActionResult List(RegisterAccounSearchParameter searchParameter)
        {
            var data = _accountService.GetRegisterAccounts(searchParameter);
            ViewBag.SearchParameter = searchParameter;
            return View(data);
        }



        public ActionResult View(int id)
        {
            var account = _accountService.GetRegisterAccountById(id);
            var model = new EditAccountModel(account);
            return View(model);

        }

        #endregion


        public ActionResult AuthorizeRegisterAccount(int id)
        {
            var account = _accountService.GetRegisterAccountById(id);
            var model = new CreateAccountModel(account);
            return View(model);
        }

        [HttpPost]
        public ActionResult Authorize(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.ExistsAccount(model.LoginName))
                {
                    ModelState.AddModelError("LoginName", string.Format("登录账号 {0} 已经存在", model.LoginName));
                    return View("AuthorizeRegisterAccount", model);
                }
                var account = _accountService.GetRegisterAccountById((int)model.Id);
                account.AccountName = model.LoginName;
                account.Password = model.ConfirmPassword;
                account.Name = model.Name;
                account.Age = model.Age;
                account.Birthday = model.Birthday;
                account.Gender = model.Gender;
                account.Telephone = model.Telephone;
                account.Mobile = model.Mobile;
                account.Email = model.Email;
                account.JobNumber = model.JobNumber;
                _accountService.AuditAccount(account);
                return RedirectToAction("Edit","Account", new { id = account.Id});
            }
            return View("AuthorizeRegisterAccount",model);
        }


        #region 删除账户信息
        public ActionResult Delete(int id)
        {
            var account = _accountService.GetRegisterAccountById(id);
            account.Deleted = true;
            _accountService.UpdateRegisterAccount(account);
            return RedirectToAction("List");
        }

        #endregion
    }
}
