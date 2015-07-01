using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using QsTech.Accounts.Models.AccountServices;
using QsTech.Accounts.Models.Accounts;
using QsTech.Accounts.Models.Profiles;
using QsTech.Accounts.Models.RoleAccount;
using QsTech.Accounts.Models.SelectAccounts;
using QsTech.Core.Interface;
using QsTech.Core.Interface.Account;
using QsTech.Core.Interface.Clients;
using QsTech.Framework;
using QsTech.Framework.Json;
using QsTech.Framework.Security;
using QsTech.Framework.Pagination;

namespace QsTech.Accounts.Controllers
{
    [NoAuthorize]
    public class ApiController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IClientManager _clientManager;
        private readonly IEncryption _encryption;

        public ApiController(IAccountService accountService,
            IClientManager clientManager, IEncryption encryption)
        {
            _accountService = accountService;
            _clientManager = clientManager;
            _encryption = encryption;
        }


        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            if (requestContext.HttpContext.Request.HttpMethod == "OPTIONS")
            {
                requestContext.HttpContext.Response.Write("ok");
            }
            else
            {
                base.Execute(requestContext);
            }

        }

        #region 账户选择
        public ActionResult Selector(SelectAccountSearchParameter parameter)
        {
            var datas = _accountService.GetAccounts(parameter);
            ViewBag.Multiable = parameter.Multiable;
            ViewBag.ExcludeArray = parameter.ExcludeArray;
            ViewBag.SearchParameter = parameter;
            return PartialView("SelectorPartial", datas);
        }
        #endregion


        #region 客户端接口
        /// <summary>
        /// 获取账户列表
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        public ActionResult GetAccountsData([JsonModelBinder]AccountSearchParameter searchParameter)
        {
           var resultData = new JsonData<PagedListWrapper<Account>>();
           try
           {
               resultData.d = _accountService.GetAccounts(searchParameter).ToPagedListWrapper();
           }
           catch (Exception ex)
           {

               resultData.m = "获取账户信息失败!";
           }
           return this.QsJson(resultData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取账户列表
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        public ActionResult GetAccountsDataList(AccountSearchNParameter searchParameter)
        {

            try
            {
                var datas = _accountService.GetAccountPagedList(searchParameter);
                if (datas.rows == null)
                    datas.rows = new List<Account>();
                var ndatas =
                    datas.rows.Select(m => new AccountDto() { id = m.Id, accountName = m.AccountName, name = m.Name });
                var pagelList = new QsTech.Accounts.Models.PagedList<AccountDto>(ndatas,searchParameter.pageSize, searchParameter.pageIndex+1, datas.total);
                return this.QsJson(pagelList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new QsTechException("获取帐户列表失败!");

            }

        }


        /// <summary>
        /// 添加SSO帐户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateSsoAccount(CreateAccountModel model)
        {
            var re =new  JsonData<int>();
            try
            {
                if (_accountService.ExistsAccount(model.LoginName))
                {
                    re.s = false;
                    re.m ="帐户名称已存在!";
                    Response.StatusCode = 500;
                    return Json("帐户名称已存在!");
                }
                var newAccount = new Account()
                {
                    AccountName = model.LoginName,
                    Password = model.Password,
                    Name = string.IsNullOrEmpty(model.Name) ? " " : model.Name,
                    Age = model.Age,
                    Birthday = model.Birthday,
                    Gender = model.Gender,
                    Telephone = model.Telephone,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    JobNumber = model.JobNumber,
                };
                _accountService.AddAccount(newAccount);
                re.d = newAccount.Id;
                if (model.Clients != null)
                {
                    _accountService.AuthorizeClients(newAccount.Id, model.Clients);
                }
                return Json(newAccount.Id);
            }
            catch (Exception ex)
            {
                re.s = false;
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
            return Json(re);
        }

        /// <summary>
        /// 修改SSO密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateSsoPassword(ChangePasswordModel model)
        {
            var re = new JsonData<bool>();
            try
            {
                if (string.IsNullOrEmpty(model.NewPassword))
                {
                    re.s = false;
                    Response.StatusCode = 500;
                    return Json("新密码不能为空!");
                }
                //var user = _accountService.GetAccountById(model.Id);
                //if (user == null)
                //{
                //    re.s = false;
                //    Response.StatusCode = 500;
                //    return Json("帐户不存在!");
                //}
                _accountService.InitPassword(model.Id,model.NewPassword);
                return Json(true);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }           
        }


        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  ActionResult GetAccountById(int id)
        {
            var resultData = new JsonData<Account>();
            try
            {
                resultData.d=_accountService.GetAccountById(id);
            }
            catch (Exception ex)
            {

                resultData.m = "获取账户信息失败!";
            }
            return this.QsJson(resultData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新账户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAccount([JsonModelBinder]ClientAccountCreateModel model)
        {

            var resultData = new JsonData<int>();
            try
            {
                if(string.IsNullOrEmpty(model.ApplicationId))
                {
                    resultData.m = string.Format("应用ApplicationId为空!");
                    return this.QsJson(resultData);
                }
                var client = _clientManager.GetClientByApplicationId(model.ApplicationId);
                if(client==null)
                {
                    resultData.m = string.Format("找不到id为{0}的应用!",model.ApplicationId);
                    return this.QsJson(resultData);
                }
                if (_accountService.ExistsAccount(model.Password))
                {
                    resultData.m = string.Format("密码不能为空!");
                    return this.QsJson(resultData);
                }
                if (_accountService.ExistsAccount(model.AccountName))
                {
                    resultData.m = string.Format("账户名{0}已存在!",model.AccountName);
                    return this.QsJson(resultData);
                }
                var account = model.ConvertToEntity();
                _accountService.AddAccount(account);
                _accountService.NewAccountAuthorizeClient(account.Id,client.Id);
                resultData.d = account.Id;
            }
            catch (Exception ex)
            {

                resultData.m = "创建账户失败!";
            }
            return this.QsJson(resultData);
        }


        /// <summary>
        /// 更新账户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAccount([JsonModelBinder]SsoAccountModel model)
        {
            var resultData = new JsonData<bool>();
            try
            {
                var account = _accountService.GetAccountById(model.Id);
                if (account != null)
                {
                    account.Age = model.Age;
                    account.Name = model.Name;
                    account.Telephone = model.Telephone;
                    account.Mobile = model.Mobile;
                    account.Gender = model.Gender;
                    account.Email = model.Email;
                    _accountService.UpdateAccount(account);
                }
                resultData.d = true;
            }
            catch (Exception ex)
            {
                resultData.m = "更新账户信息失败!";
            }
            return this.QsJson(resultData);
        }

        /// <summary>
        /// 更新账户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            var resultData = new JsonData<bool>();
            try
            {
                var account = _accountService.GetAccountById(id);
                if (account != null)
                {
                    account.Deleted = true;
                    _accountService.UpdateAccount(account);
                    resultData.d = true;
                }
                else
                {
                    resultData.m = "删除账户失败!";
                }
            }
            catch (Exception ex)
            {
                resultData.m = "更新账户信息失败!";
            }
            return this.QsJson(resultData);
        }


        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InitPassward([JsonModelBinder]InitPasswordModel model)
        {
            var resultData = new JsonData<bool>();
            try
            {
                if(string.IsNullOrEmpty(model.ConfirmPassword))
                {
                    resultData.m = "密码不能为空!";
                    return this.QsJson(resultData);
                }
                if(model.Password!=model.ConfirmPassword)
                {
                    resultData.m = "密码不一致!";
                    return this.QsJson(resultData);
                }
                _accountService.InitPassword(model.Id,model.ConfirmPassword);
                resultData.d = true;
            }
            catch (Exception ex)
            {

                resultData.m = "初始化账户密码失败!";
            }
            return this.QsJson(resultData);
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePassward([JsonModelBinder]ChangePasswordModel model)
        {
            var resultData = new JsonData<bool>();
            try
            {
                if (string.IsNullOrEmpty(model.OriginalPassword) || string.IsNullOrEmpty(model.ConfirmationPassword))
                {
                    resultData.m = "密码不能为空!";
                    return this.QsJson(resultData);
                }
                if (model.ConfirmationPassword != model.NewPassword)
                {
                    resultData.m = "新密码与确认密码不一致!";
                    return this.QsJson(resultData);
                }
                _accountService.ChangePassword(model.Id, model.OriginalPassword,model.ConfirmationPassword);
                resultData.d = true;
            }
            catch (Exception ex)
            {

                resultData.m = "更改账户密码失败!";
            }
            return this.QsJson(resultData);
        }
        #endregion


        #region   客户端

        /// <summary>
        /// 获取用户的客户端数据
        /// </summary>
        /// <param name="accountId"></param>
        [NoAuthorize]
        public ActionResult GetClientUserData(int accountId)
        {
            var resultData = new JsonData<IClient[]>();
            try
            {
                var account = _accountService.GetAccountById(accountId);
                var model = new AuthorizeRoleModel
                {
                    UserId = account.Id
                };
                IEnumerable<IClient> allClients;
                allClients = _clientManager.GetClients().ToArray();
                var ownedClients = account.Clients.Select(m => m.ClientId).ToArray();
                resultData.d = allClients.Where(m => ownedClients.Contains(m.Id)).ToArray();
            }
            catch (Exception ex)
            {
                resultData.m = "获取用户客户端列表失败!";
            }
            return this.QsJson(resultData);
        }
        #endregion



        #region 客户端登陆
        /// <summary>
        /// 客户端登陆验证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckUser(ClientUserLoginModel model)
        {
            var re = new JsonData<int>();
            try
            {
                var user = _accountService.GetAllAccounts().FirstOrDefault(u => u.AccountName == model.LoginName);
                if (user != null)
                {
                    var encryptionPassword = _encryption.GetBase64Hash(System.Text.Encoding.UTF8.GetBytes(model.Password.ToCharArray()));
                    if (string.Compare(encryptionPassword, user.Password, false, System.Globalization.CultureInfo.InvariantCulture) == 0)
                    {
                        re.d=user.Id;
                    }
                    else
                        re.m="密码错误!";
                }
                else
                {
                    re.m = "登录名不存在!";
                }

                return Json(re);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }

        /// <summary>
        /// 根据登陆账户获取用户名称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUserNameByLoginName(string loginName)
        {
            var re = new JsonData<string>();
            try
            {
                var user = _accountService.GetAllAccounts().FirstOrDefault(u => u.AccountName == loginName);
                if (user != null)
                {
                    re.d=user.Name;
                }
                else
                {
                    re.m = "用户不存在!";
                }

                return Json(re, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                re.m =string.Format("系统出错，{0}",ex.Message);
                return Json(re,JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
