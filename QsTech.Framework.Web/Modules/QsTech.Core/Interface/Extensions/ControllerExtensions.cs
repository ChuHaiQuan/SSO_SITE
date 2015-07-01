using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework.Security;
using System.Web.Mvc;
using QsTech.Framework.WorkContexts;
using QsTech.Framework;
using System.Web;
using QsTech.Framework.Security.Permissions;

namespace QsTech.Core.Interface.Extensions
{

    public static class ControllerExtensions
    {
        public static IUser GetUser(this Controller controller)
        {
            return controller.ControllerContext.GetWorkContext().GetCurnentUser();
        }

        public static IAccount GetAccount(this Controller controller)
        {
            return controller.ControllerContext.GetWorkContext().GetCurnentAccount();
        }

      
        public static IEnterprise GetOwner(this Controller controller)
        {
            var _ownerManager = controller.ControllerContext.GetWorkContext().Resolve<IOwnerEnterpriseManager>();
            return _ownerManager.GetOwner(controller.GetUser());
        }

        public static IEnumerable<string> GetUserPermissions(this Controller controller)
        {
            var _upManager = controller.ControllerContext.GetWorkContext().Resolve<QsTech.Core.Interface.User.IAccountPermissionsManager>();
            var account = controller.GetUser();
            return _upManager.GetUserPermissions(account).ToArray();
        }

        public static string GetModelErrorsMessage(this Controller controller)
        {
            string errorString = controller.ModelState.Keys.Select(key => controller.ModelState[key].Errors.FirstOrDefault())
                .Where(error => error != null)
                .Aggregate("", (current, error) => current + (error.ErrorMessage + "\r\n"));

            return errorString = "请检查以下输入项：\r\n" + errorString;
        }
    }
}
