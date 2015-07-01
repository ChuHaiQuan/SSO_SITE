using QsTech.Framework.Mvc.Layouts;

namespace QsTech.Layout
{
    public class DefaultLayoutProvider : ILayoutProvider
    {
        public string GetLayoutPath(string theme)
        {
            switch (theme)
            {
                case "Content":
                    return "~/Modules/QsTech.Core/Views/Shared/Content.cshtml";
                case "NoMenu":
                    return "~/Modules/QsTech.Core/Views/Shared/NoMenu.cshtml";
                case "Home":
                    return "~/Modules/QsTech.Core/Views/Shared/Home.cshtml";
                case "Layout":
                    return "~/Modules/QsTech.Core/Views/Shared/Layout.cshtml";
                case "NoHeadLayout":
                    return "~/Modules/QsTech.Core/Views/Shared/NoHeadLayout.cshtml";
                case "PortalLayout":
                    return "~/Modules/QsTech.Core/Views/Shared/PortalLayout.cshtml";
                case "PortalManagerLayout":
                    return "~/Modules/QsTech.Core/Views/Shared/PortalManagerLayout.cshtml";
                case "NoAuthorizeLayout":
                    return "~/Modules/QsTech.Core/Views/Shared/NoAuthorizeLayout.cshtml";
                default:
                    return "~/Modules/QsTech.Core/Views/Shared/Content.cshtml";
            }
        }
    }
}