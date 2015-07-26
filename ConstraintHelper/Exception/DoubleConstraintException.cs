using System;

namespace SemiRoot.MonoTouchHelpers
{
	public class DoubleConstraintException : Exception
	{
		public DoubleConstraintException() : base("For this positioning entry, a constraint already exists") { }
	}
}

