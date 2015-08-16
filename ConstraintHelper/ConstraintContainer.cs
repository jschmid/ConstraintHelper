using System;
using System.Collections.Generic;

using UIKit;

namespace SR.MonoTouchHelpers
{
	public class ConstraintContainer
	{
		#region Fields

		public readonly UIView View;

		#endregion


		#region Lifecycle

		public ConstraintContainer(UIView view)
		{
			View = view;
      Margin = new ConstraintMargin();
		}

		#endregion


		#region Attributes Margin

    public ConstraintMargin Margin { get; set; }

		#endregion


		#region Attributes Constraints

		NSLayoutConstraint _constraintTop;
		NSLayoutConstraint _constraintMiddle;
		NSLayoutConstraint _constraintBottom;
		NSLayoutConstraint _constraintRight;
		NSLayoutConstraint _constraintCenter;
		NSLayoutConstraint _constraintLeft;
		NSLayoutConstraint _constraintHeight;
		NSLayoutConstraint _constraintWidth;

		public NSLayoutConstraint ConstraintTop {
			get { 
				return _constraintTop; 
			}
			set {
				if (_constraintTop != null && value != null) { throw new DoubleConstraintException(); }
				_constraintTop = value; 
			}
		}

		public NSLayoutConstraint ConstraintMiddle {
			get { 
				return _constraintMiddle; 
			}
			set {
				if (_constraintMiddle != null && value != null) { throw new DoubleConstraintException(); }
				_constraintMiddle = value; 
			}
		}

		public NSLayoutConstraint ConstraintBottom {
			get { 
				return _constraintBottom; 
			}
			set {
				if (_constraintBottom != null && value != null) { throw new DoubleConstraintException(); }
				_constraintBottom = value; 
			}
		}

		public NSLayoutConstraint ConstraintRight {
			get { 
				return _constraintRight; 
			}
			set {
				if (_constraintRight != null && value != null) { throw new DoubleConstraintException(); }
				_constraintRight = value; 
			}
		}

		public NSLayoutConstraint ConstraintCenter {
			get { 
				return _constraintCenter; 
			}
			set {
				if (_constraintCenter != null && value != null) { throw new DoubleConstraintException(); }
				_constraintCenter = value; 
			}
		}

		public NSLayoutConstraint ConstraintLeft {
			get { 
				return _constraintLeft; 
			}
			set {
				if (_constraintLeft != null && value != null) { throw new DoubleConstraintException(); }
				_constraintLeft = value; 
			}
		}

		public NSLayoutConstraint ConstraintHeight {
			get { 
				return _constraintHeight; 
			}
			set {
				if (_constraintHeight != null && value != null) { throw new DoubleConstraintException(); }
				_constraintHeight = value; 
			}
		}

		public NSLayoutConstraint ConstraintWidth {
			get { 
				return _constraintWidth; 
			}
			set {
				if (_constraintWidth != null && value != null) { throw new DoubleConstraintException(); }
				_constraintWidth = value; 
			}
		}

		#endregion


		#region Constraint helpers

		public List<NSLayoutConstraint> GetAllConstraints()
		{
			var list = new List<NSLayoutConstraint>();

			if(null != _constraintTop)   { list.Add(_constraintTop); }
			if(null != _constraintMiddle){ list.Add(_constraintMiddle); }
			if(null != _constraintBottom){ list.Add(_constraintBottom); }
			if(null != _constraintRight) { list.Add(_constraintRight); }
			if(null != _constraintCenter){ list.Add(_constraintCenter); }
			if(null != _constraintLeft)  { list.Add(_constraintLeft); }
			if(null != _constraintHeight){ list.Add(_constraintHeight); }
			if(null != _constraintWidth) { list.Add(_constraintWidth); }

			return list;
		}

		public void EmptyConstraints()
		{
			_constraintTop = null;
			_constraintMiddle = null;
			_constraintBottom = null;
			_constraintRight = null;
			_constraintCenter = null;
			_constraintLeft = null;
			_constraintHeight = null;
			_constraintWidth = null;
		}

		#endregion

	}
}
