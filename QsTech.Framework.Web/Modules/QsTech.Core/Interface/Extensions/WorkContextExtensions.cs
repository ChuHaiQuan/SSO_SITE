using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using QsTech.Framework;
using QsTech.Framework.Mvc;
using QsTech.Framework.Security.Permissions;
using QsTech.Framework.WorkContexts;
using QsTech.Framework.Security;
using System.Web;

namespace QsTech.Core.Interface.Extensions
{
    public static class WorkContextExtensions
    {

        public static IUser GetCurnentUser(this WorkContext ctx)
        {
            return ctx.Get<IUser>("CurrentUser");
        }

        public static IAccount GetCurnentAccount(this WorkContext ctx)
        {
            return ctx.Get<IAccount>("CurrentAccount");
        }

    }

    public class CurrentUserWorkContextItem : IWorkContextItemProvider
    {
        public bool TryGet<T>(WorkContext ctx, string name, out T value)
        {
            if (name == "CurrentUser")
            {
                var authSeivce = ctx.Resolve<IAuthenticationService>();
                value = (T)(object)authSeivce.GetAuthenticatedUser();
                return true;
            }
            else if (name == "CurrentAccount")
            {
                 var authSeivce = ctx.Resolve<IAuthenticationService>();
                value = (T)(object)authSeivce.GetAuthenticatedAccount();
                return true;
            }
            value = default(T);
            return false;
        }
    }


    public static class ViewPageExt
    {
        public static IUser GetUser<T>(this  QsTech.Framework.Mvc.WebViewPage<T> page)
        {
            return page.WorkContext.GetCurnentUser();
        }

        public static bool Authorize<T>(this  QsTech.Framework.Mvc.WebViewPage<T> page,Permission permission )
        {
            var authorizer=page.WorkContext.Resolve<IAuthorizer>();
            return authorizer.Authorize(permission);
        }


        public static IThemeManager GetTheme<T>(this  QsTech.Framework.Mvc.WebViewPage<T> page)
        {
            return page.WorkContext.Resolve<IThemeManager>();
        }
    }



}
