using Microsoft.Practices.Unity;
using MyoQuest.Game.UnityModule;
using MyoQuest.MyoController.UnityModule;

namespace MyoQuest.UnityModule
{
	public class UnityBootstrapper
	{
		private readonly IUnityContainer container;

		public UnityBootstrapper()
		{
			this.container = new UnityContainer();
		}

		public void Initialise()
		{
			this.container.Resolve<GameUnityModule>().Initialise();
			this.container.Resolve<MyoControllerUnityModule>().Initialise();
		}
	}
}
