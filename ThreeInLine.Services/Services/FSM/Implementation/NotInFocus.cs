using ThreeInLine.Services.Container;
using ThreeInLine.Services.Interaction;

namespace ThreeInLine.Services.FSM.Implementation
{
	internal class NotInFocus : StateTemplate
	{
		private readonly IFocusing _focusing;

		public NotInFocus(IContainer container) : base(container)
		{
			_focusing = Container.Resolve<IFocusing>();
		}

		public override void ResetState() { }

		public override bool Rising()
		{
			return false;
		}

		public override bool Idle()
		{
			return ReferenceEquals(_focusing.InFocus, null);
		}

		public override bool Fading()
		{
			return false;
		}
	}
}