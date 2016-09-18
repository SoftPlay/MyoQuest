using Microsoft.Practices.Unity;

namespace MyoQuest.Game.UnityModule
{
	public sealed class GameUnityModule
	{
		private readonly IUnityContainer container;

		public GameUnityModule(IUnityContainer container)
		{
			this.container = container;
		}

		public void Initialise()
		{
		}
	}
}
