using System;
using MyoQuest.Common;
using MyoQuest.Common.Enums;

namespace MyoQuest.MyoController
{
	public sealed class MyoControl : IGameController
	{
		private readonly IMyoDal dal;
		private readonly IPoseToControllerStateConverter poseConverter;

		public MyoControl(
			IMyoDal myoDal,
			IPoseToControllerStateConverter poseConverter)
		{
			this.dal = myoDal;
			this.poseConverter = poseConverter;

			this.dal.PoseChanged += this.Dal_PoseChanged;
		}

		public ControllerState CurrentState { get; private set; }

		private void Dal_PoseChanged(object sender, NewPoseEventArgs e)
		{
			this.CurrentState = this.poseConverter.ConvertFrom(e.Pose);
		}
	}
}
