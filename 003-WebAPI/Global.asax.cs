using SimpleInjector;
using System.Web.Http;
using System.Web.Mvc;

namespace AlphaTestSystem
{
	public class WebApiApplication : System.Web.HttpApplication
    {
		private void ConfigureApi()
		{
			var container = new Container();
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			if (GlobalVariable.logicType == 0)
			{
				container.Register<IUserRepository, EntityUserManager>();
			}
			else if (GlobalVariable.logicType == 1)
			{
				container.Register<IUserRepository, SqlUserManager>();
			}
			else if (GlobalVariable.logicType == 2)
			{
				container.Register<IUserRepository, MySqlUserManager>();
			}
			else if (GlobalVariable.logicType == 3)
			{
				container.Register<IUserRepository, MongoUserManager>();
			}
			container.Verify();
			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);
		}

		protected void Application_Start()
        {
			AreaRegistration.RegisterAllAreas();
			ConfigureApi();

			GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());
			GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
