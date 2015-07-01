using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using QsTech.Framework.Environment;


namespace QsTech.Framework.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
        }
        private IHost _host;
        private Preloader _preloader;

        public override void Init()
        {
            Application_PreStartInit();
            base.Init();
        }

        protected void Application_PreStartInit()
        {
            _preloader = new Preloader(this);
            _preloader.Setup();
        }

        protected void Application_Start()
        {
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            Application_PreStartInit();
            _host = new WebHost(this);
            _host.Init(_preloader);


            this.BeginRequest += OnBeginRequest;
            this.EndRequest += OnEndRequest;
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            _preloader.Dispose();
        }


        private void OnEndRequest(object sender, EventArgs eventArgs)
        {
            _host.EndRequest(new HttpContextWrapper(this.Context));
        }

        private void OnBeginRequest(object sender, EventArgs eventArgs)
        {
            _host.BeginRequest(new HttpContextWrapper(this.Context));
        }
    }
}