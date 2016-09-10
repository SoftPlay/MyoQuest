using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoQuest.Stories
{
	public interface IActionResult
	{
		int ID { get; }
		string ResultText { get; }
		int NextStory { get; }
	}
}
