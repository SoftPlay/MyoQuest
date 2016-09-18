using Microsoft.Practices.Unity;
using MyoQuest.Common;

namespace MyoQuest.MyoController.UnityModule
{
	public sealed class MyoControllerUnityModule
	{
		private readonly IUnityContainer container;

		public MyoControllerUnityModule(IUnityContainer container)
		{
			this.container = container;
		}

		public void Initialise()
		{
			this.container.RegisterType<IMyoDal, MyoDal>(new ContainerControlledLifetimeManager());

			this.container.RegisterType<IMyoObjectFactory, MyoObjectFactory>();
			this.container.RegisterType<IPoseToControllerStateConverter, StaticPoseToControllerStateConverter>();
			this.container.RegisterType<IGameController, MyoControl>();
		}
	}
}
