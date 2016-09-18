using System;
using Microsoft.Practices.Unity;
using MyoQuest.Common;
using MyoQuest.Game.UnityModule;
using MyoQuest.MyoController;
using MyoQuest.MyoController.UnityModule;

namespace MyoQuest.UnityModule
{
	public sealed class UnityBootstrapper : IGameRunner
	{
		private readonly IUnityContainer container;

		private IMyoDal myoDal;

		public UnityBootstrapper()
		{
			this.container = new UnityContainer();
		}

		public void Initialise()
		{
			this.container.Resolve<GameUnityModule>().Initialise();
			this.container.Resolve<MyoControllerUnityModule>().Initialise();
		}

		public void RunGame()
		{
			// No game logic yet, just set up the Myo connection
			this.myoDal = this.container.Resolve<IMyoDal>();
			this.myoDal.Initialise();
		}

		public void Shutdown()
		{
			// No game logic yet, just shut down the Myo connection
			this.myoDal.Shutdown();
			this.myoDal = null;
		}
	}
}
