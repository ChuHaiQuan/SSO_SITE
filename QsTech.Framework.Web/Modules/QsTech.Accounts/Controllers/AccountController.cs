using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using QsTech.Accounts.Models.AccountServices;
using QsTech.Accounts.Models.Accounts;
using QsTech.Accounts.Models.BindUsers;
using QsTech.Accounts.Models.ClientAccounts;
using QsTech.Accounts.Models.Profiles;
using QsTech.Accounts.Models.Publicties;
using QsTech.Accounts.Models.Registers;
using QsTech.Accounts.Models.RoleAccount;
using QsTech.Core.Interface;
using QsTech.Core.Interface.Clients;
using QsTech.Core.Interface.Extensions;
using QsTech.Core.Interface.User;
using QsTech.Framework;
using QsTech.Framework.Json;
using QsTech.Framework.Mvc.Html.Notify;
using QsTech.Framework.Pagination;
using QsTech.Framework.Security;

namespace QsTech.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICoreSetting _coreSetting;
        private readonly IAccountService _accountService;
        private readonly IAuthorizer _authorizer;
        private readonly IRoleManager _roleManager;
        private readonly IAccountPermissionsManager _accountPermissionsManager;
        private readonly IClientManager _clientManager;

        public AccountController(ICoreSetting coreSetting,
            IAccountService accountService,
             IAuthorizer authorizer,
            IRoleManager roleManager,
            IAccountPermissionsManager accountPermissionsManager,
            IClientManager clientManager)
        {
            _coreSetting = coreSetting;
            _accountService = accountService;
            _authorizer = authorizer;
            _roleManager = roleManager;
            _accountPermissionsManager = accountPermissionsManager;
            _clientManager = clientManager;
        }

        public ActionResult List(AccountSearchParameter searchParameter)
        {
            var data = _accountService.GetAccounts(searchParameter);
            ViewBag.IsSso = _coreSetting.IsSsoServer;
            ViewBag.SearchParameter = searchParameter;
            return View(data);
        }


        #region 创建账户
        public  ActionResult Create()
        {
            if (!_authorizer.Authorize(Permissions.CreateAccount))
                throw new QsTechException(string.Format("没有{0}的权限!", Permissions.CreateAccount.Description),true);
            var account = new CreateAccountModel();
            return View(account);
        }

        [HttpPost]
        public  ActionResult Create(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.ExistsAccount(model.LoginName))
                {
                    ModelState.AddModelError("LoginName", string.Format("登录账号 {0} 已经存在", model.LoginName));
                    return View(model);
                }
                var newAccount = new Account()
                {
                    AccountName = model.LoginName,
                    Password = model.ConfirmPassword,
                    Name =string.IsNullOrEmpty(model.Name)?" ":model.Name,
                    Age = model.Age,
                    Birthday = model.Birthday,
                    Gender = model.Gender,
                    Telephone = model.Telephone,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    JobNumber = model.JobNumber,
                };
                _accountService.AddAccount(newAccount);
                return RedirectToAction("Edit", new { id = newAccount.Id});
            }
            return View(model);
        }
        #endregion


        #region 编辑账户
        public ActionResult Edit(int id)
        {
            var account = _accountService.GetAccountById(id);
            var model = new EditAccountModel(account);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var  account = _accountService.GetAccountById(model.Id);
                account.Name = string.IsNullOrEmpty(model.Name) ? " " : model.Name;
                account.Age = model.Age;
                account.Birthday = model.Birthday;
                account.Gender = model.Gender;
                account.Telephone = model.Telephone;
                account.Mobile = model.Mobile;
                account.Email = model.Email;
                account.JobNumber = model.JobNumber;
                _accountService.UpdateAccount(account);
                ViewBag.Success = true;
            }
            return View(model);
        }
        #endregion

        #region 删除账户信息
        public  ActionResult Delete(int id)
        {
            var account = _accountService.GetAccountById(id);
            account.Deleted = true;
            _accountService.UpdateAccount(account);
            return RedirectToAction("List");
        }

        #endregion

        #region 账户角色 分配
        public ActionResult AccountRoles(int accountId)
        {
            var account = _accountService.GetAccountById(accountId);
            var model = new AuthorizeRoleModel
            {
                UserId = account.Id
            };
            IEnumerable<IRole> allRoles;
            allRoles = _roleManager.GetAllRoles().ToArray();
            var ownedRoles = _roleManager.GetAccountRoles(account).ToArray();
            model.RoleDescriptors = allRoles.Select(r => new RoleDescriptor(r)
            {
                Allow = ownedRoles.Any(or => or.Id == r.Id)
            });
            return PartialView("AccountRolesPartial", model);
        }


        [HttpPost]
        public ActionResult AuthorizeRole(int accountId, int roleId)
        {
            _accountPermissionsManager.AuthorizeRole(accountId, roleId);
            return this.Json(null);
        }

        [HttpPost]
        public ActionResult UnauthorizeRole(int accountId, int roleId)
        {
            _accountPermissionsManager.UnAuthorizeRole(accountId, roleId);
            return this.Json(null);
        }

        #endregion

        #region 初始化密码
        public ActionResult InitPassword(int id)
        {
            var account = _accountService.GetAccountById(id);
            var model = new InitPasswordModel(account);
            return View(model);
        }

        [HttpPost]
        public ActionResult InitPassword(InitPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // var account = _accountService.GetAccountById(model.Id);
                _accountService.InitPassword(model.Id, model.ConfirmPassword);
                return this.Json(null);
            }
            else
                throw new QsTechException(this.GetModelErrorsMessage());
        }
        #endregion

        #region 拥有角色的用户管理
        public ActionResult AccountManage(RoleAccountSearchParameter parameter)
        {
            var datas = _accountService.GetAccounts(parameter);
            ViewBag.SearchParameter = parameter;
            ViewBag.Role = _roleManager.GetRole(parameter.RoleId);
            return View(datas);
        }

        [HttpPost]
        public ActionResult AddRoleAccount(int roleId, [JsonModelBinder]List<int> userIds)
        {
            foreach (var u in userIds)
            {
                _accountPermissionsManager.AuthorizeRole(u, roleId);
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult RemoveRoleAccount(int roleId, [JsonModelBinder]List<int> userIds)
        {
            foreach (var u in userIds)
            {
                _accountPermissionsManager.UnAuthorizeRole(u, roleId);
            }
            return new EmptyResult();
        }
        #endregion

        #region  拥有应用访问权限的账户管理 
        public ActionResult ClientAccountManage(ClientAccountSearchParameter parameter)
        {
            var datas = _accountService.GetAccounts(parameter);
            ViewBag.SearchParameter = parameter;
            ViewBag.Client = _clientManager.GetClientById(parameter.ClientId);
            ViewBag.Accounts = _accountService.GetAccountsByClientId(parameter.ClientId);
            return View(datas);
        }

        [HttpPost]
        public ActionResult AddClientAccount(int clientId,[JsonModelBinder]List<int> userIds)
        {
            foreach (var u in userIds)
            {
                _accountService.AuthorizeClient(u, clientId);
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult RemoveClientAccount(int clientId, [JsonModelBinder]List<int> userIds)
        {
            foreach (var u in userIds)
            {
                _accountService.UnAuthorizeClient(u, clientId);
            }
            return new EmptyResult();
        }
        #endregion

        #region 账户 访问应用权限 分配
        public ActionResult AccountClients(int accountId)
        {
            var account = _accountService.GetAccountById(accountId);
            var model = new AuthorizeRoleModel
            {
                UserId = account.Id
            };
            IEnumerable<IClient> allClients;
            allClients = _clientManager.GetClients().ToArray();
            var ownedClients = account.Clients.Select(m=>m.ClientId).ToArray(); //_accountService.GetClientsByAccountId(accountId);
            model.RoleDescriptors = allClients.Select(r => new RoleDescriptor(r)
            {
                Allow = ownedClients.Any(or => or == r.Id)
            });
            return PartialView("AccountClientsPartial", model);
        }


        [HttpPost]
        public ActionResult AuthorizeClient(int accountId, int clientId)
        {
            _accountService.AuthorizeClient(accountId, clientId);
            return this.Json(null);
        }

        [HttpPost]
        public ActionResult UnauthorizeClient(int accountId, int clientId)
        {
            _accountService.UnAuthorizeClient(accountId, clientId);
            return this.Json(null);
        }

        #endregion

        #region  修改信息、密码
        public ActionResult Profile()
        {
            var account = _accountService.GetAccountById(this.GetAccount().Id);
            var model = new EditAccountProfileModel(account);
            return View(model);
        }

        [HttpPost]
        public ActionResult Profile(EditAccountProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _accountService.GetAccountById(this.GetAccount().Id);
                account.Name = model.Name;
                account.Age = model.Age;
                account.Birthday = model.Birthday;
                account.Gender = model.Gender;
                account.Telephone = model.Telephone;
                account.Mobile = model.Mobile;
                account.Email = model.Email;
                _accountService.UpdateAccount(account);
                ViewBag.Success = true;
            }
            return View(model);
        }

        public ActionResult Security()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Security(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accountService.ChangePassword(this.GetAccount().Id, model.OriginalPassword, model.NewPassword);
                    //_notifier.Add(NotifyType.Information, "密码修改成功");
                    ViewBag.Succeed = true;
                }
                catch (SecurityException ex)
                {
                    //_transactionManager.Cancel();
                    ModelState.AddModelError(string.Empty, ex.Message);
                    ViewBag.Failed = true;
                }
            }
            return View(model);
        }

        #endregion


        #region 登录部分页
        public ActionResult LogOnDisplayZone()
        {
            var currentUser = _accountService.GetAccountById(this.GetAccount().Id);
            var model = new LogOnDisplayModel
            {
                UserName = currentUser.Name,
                AntiPhishing = currentUser.AntiPhishing
            };
            return View(model);
        }
        #endregion


        #region 绑定用户

        public ActionResult BindUser(int id)
        {
            var account = _accountService.GetAccountById(id);
            var model = new BindUserModel(account);
            return View(model);
        }

        [HttpPost]
        public ActionResult BindUser(BindUserModel model)
        {
            var account = _accountService.GetAccountById(model.AccountId);
            _accountService.BindAccountUser(account,model);
            return this.QsJson(null);
        }
        #endregion

        #region 旧系统改用户密码
        /// <summary>
        /// 
        /// </summary>
        /// <param name="redirect_uri"></param>
        /// <returns></returns>
        public ActionResult ModifyPassword(string redirect_uri)
        {
            var cp = new ChangePasswordModel()
                {
                    Redirect_uri = redirect_uri
                };
            return View(cp);
        }

        [HttpPost]
        public ActionResult ModifyPassword(ChangePasswordModel model, string redirect_uri)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accountService.ChangePassword(this.GetAccount().Id, model.OriginalPassword, model.NewPassword);
                    ViewBag.Succeed = true;
                }
                catch (SecurityException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    ViewBag.Failed = true;
                }
            }
            model.Redirect_uri = redirect_uri;
            return View(model);
        }


        #endregion
    }
}
