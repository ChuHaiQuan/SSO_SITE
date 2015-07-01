using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QsTech.Core.Interface.Clients;
using QsTech.Framework;
using QsTech.Framework.Security;
using QsTech.Framework.Pagination;
using QsTech.Core.Interface.JqGrid;
using QsTech.Core.Interface.Common;
using QsTech.Authentication.Sso.Clients;
using QsTech.Authentication.Sso;
using QsTech.Framework.Logging;
using QsTech.Core.Interface.Extensions;
using QsTech.Framework.Security;
using QsTech.Framework.Json;
using QsTech.Core;

namespace QsTech.Authentication.Sso.Controllers
{
   
    public class ClientController : Controller
    {
        private readonly IClientManager _clientManager;
        private readonly IUserClientService _svcUserClient;
        private readonly IAuthorizer _authorizer;
        public ClientController(IClientManager clientManager, IUserClientService svcUserClient, IAuthorizer authorizer)
        {
            _clientManager = clientManager;
            _svcUserClient = svcUserClient;

            Logger = NullLogger.Instance;
            _authorizer = authorizer;
        }
        public ILogger Logger { get; set; }

        public ActionResult List(SearchParameterBase search)
        {
            //_authorizer.Authorize(Permissions.ManagerClient, "没有权限查看应用列表!");
            var data = _clientManager.GetClients();
            return View(data.ToPagedList(0,100,data.Count()));
        }

        public ActionResult Edit(int id)
        {
           // _authorizer.Authorize(Permissions.ManagerClient, "没有权限查看应用!");
            var userClient = _clientManager.GetClientById(id);
            var userIds = _svcUserClient.GetUserClientByClientId(id).Select(m=>m.UserId).ToArray();
            ViewBag.Selected = string.Join(",", userIds);
            if (userClient == null)
            {//异常处理 
            
            }
            return View(userClient);
        }


        public ActionResult ClientUsersData(JqGridPageInfo pi,UserClientQueryModel search)
        {
            IPagedList<UserClient> cis = _svcUserClient.GetUsersByClientId(search, pi.page - 1, pi.rows);
            JqGridList<UserClient> list = new JqGridList<UserClient>(pi);
            list.records = cis.TotalItemCount;
            list.rows = cis;
            return this.QsJson(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddUserClient( [JsonModelBinderAttribute]ClientUserAddParameter data)
        {
            //REVIEW: use jsonData  //DONE
            List<UserClient> ucs = new List<UserClient>();
            int i = 0;
            foreach (var item in data.userIds)
            {
                UserClient uc = new UserClient();
                uc.ClientId = data.clientId;
                uc.UserId = item;
                uc.UserName = data.userNames[i];
                ucs.Add(uc);
                i++;
            }
            _svcUserClient.AddUserClients(ucs);
            return new EmptyResult();
        }


        [HttpPost]
        public ActionResult RemoveUserClient(int Id)
        {
            _authorizer.Authorize(Permissions.ManagerClient, "没有权限管理子应用用户!");
            var userClient = _svcUserClient.GetUserClient(Id);
            if (userClient == null)
            { 
            }
            _svcUserClient.DeleteUserClient(userClient);
            return new EmptyResult();
        }


        #region 各应用导航
        public ActionResult ClientZone(int userId)
        {
            //REVIEW: 用户相关的连接和应用导航分开  //DONE

             var menus = _clientManager.GetClientByUserId(userId);
             return View(menus);
        }



        #endregion


        #region 

        /// <summary>
        /// 获取用户的客户端数据
        /// </summary>
        /// <param name="userId"></param>
        [NoAuthorize]
        public  ActionResult GetClientUserData(int userId)
        {
            var menus = _clientManager.GetClientByUserId(userId);
            return this.Json(menus,JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
