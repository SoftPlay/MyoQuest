using MyoQuest.Common.Enums;

namespace MyoQuest.Common
{
	/// <summary>
	/// Very simple controller providing real-time state
	/// </summary>
	public interface IGameController
	{
		ControllerState CurrentState { get; }
	}
}
