﻿using System;
using System.Reflection;
using System.ServiceModel.DomainServices.Server;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Extras.DomainServices;
using Autofac.Integration.Mvc;
using Remember.Persistence.NHibernate;
using Remember.Service;
using Remember.Web.Areas.Integration.Models;

namespace Remember.Web
{
    public class GlobalApplication : System.Web.HttpApplication
    {
        static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                "Default",                              // Route Name
                "{controller}/{action}/{id}",           // Route URL (pattern)
                new
                {                                   // Route Detauls
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                new[] { "Remember.Web.Controllers" }      // Route Namespaces that take preference
            );
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterModule<NHibernateModule>();

            // Change controller action parameter injection by changing web.config.
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().InstancePerHttpRequest();

            // MVC integration test items
            builder.RegisterType<InvokerDependency>().As<IInvokerDependency>();

            // DomainServices
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<DomainService>();
            builder.RegisterModule<AutofacDomainServiceModule>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            DomainService.Factory = new AutofacDomainServiceFactory(new MvcContainerProvider());

            RegisterRoutes(RouteTable.Routes);
        }
    }
}