using ThreeInLine.Services.Container;
using ThreeInLine.Services.Interaction;
using ThreeInLine.Services.Logging;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class NotInFocus : StateTemplate
	{
		private readonly IFocusing _focusing;

		public NotInFocus(IContainer container) : base(container)
		{
			_focusing = Container.Resolve<IFocusing>();
		}

		public override bool Rising()
		{
			Container.Resolve<ILog>().Log(GetType(), Level.Debug, "not in focus RISE");
			return false;
		}

		public override bool Idle()
		{
			return ReferenceEquals(_focusing.InFocus, null);
		}

		public override bool Fading()
		{
			Container.Resolve<ILog>().Log(GetType(), Level.Debug, "not in focus FADE");
			return false;
		}
	}
}