using MyoQuest.Common.Enums;
using MyoSharp.Poses;

namespace MyoQuest.MyoController
{
	public interface IPoseToControllerStateConverter
	{
		ControllerState ConvertFrom(Pose pose);
	}
}
