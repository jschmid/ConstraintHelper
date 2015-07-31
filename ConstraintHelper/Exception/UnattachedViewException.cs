using System;

namespace SR.MonoTouchHelpers
{
	public class UnattachedViewException : Exception
	{
		public UnattachedViewException() : base("No view has been attached.") { }
	}
}

