using System;

namespace SR.MonoTouchHelpers
{
	public class DoubleConstraintException : Exception
	{
		public DoubleConstraintException() : base("For this positioning entry, a constraint already exists") { }
	}
}

