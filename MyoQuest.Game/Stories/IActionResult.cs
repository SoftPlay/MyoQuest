namespace MyoQuest.Game.Stories
{
	public interface IActionResult
	{
		int ID { get; }
		string ResultText { get; }
		int NextStory { get; }
	}
}
