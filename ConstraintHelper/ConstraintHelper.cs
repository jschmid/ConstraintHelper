using System;
using System.Linq;
using System.Collections.Generic;

using UIKit;

namespace SR.MonoTouchHelpers
{
	public class ConstraintHelper
	{
		#region Fields

		readonly UIView _view;

		ConstraintContainer _lastTop;
		ConstraintContainer _lastRight;
		ConstraintContainer _lastBottom;
		ConstraintContainer _lastLeft;

		List<ConstraintContainer> _items;

		ConstraintContainer _currentItem;

		#endregion


		#region Lifecycle

		public ConstraintHelper(UIView parentView)
		{
			_view = parentView;
			_items = new List<ConstraintContainer>();
		}

		#endregion


		#region Attaching functionalities

		public ConstraintHelper Attach(UIView view)
		{
			_currentItem = _items.FirstOrDefault(items => (items.View == view));
			if (_currentItem == null) { 
				view.TranslatesAutoresizingMaskIntoConstraints = false;
				_currentItem = new ConstraintContainer(view);
				_view.AddSubview(view);
			}
			_items.Add(_currentItem);
			return this; 
		}

		public ConstraintHelper WorkWith(UIView view)
		{
			_currentItem = _items.FirstOrDefault(items => (items.View == view));
			if (_currentItem == null) { throw new UnattachedViewException(); }
			return this;
		}

		public ConstraintHelper Remove()
		{
			if (_currentItem == null) { throw new UnattachedViewException(); }
			var constraints = _currentItem.GetAllConstraints();
			if(constraints.Count > 0) {
				_view.RemoveConstraints(constraints.ToArray());
				_currentItem.EmptyConstraints();
			}
			_currentItem.View.RemoveFromSuperview();
			_items.Remove(_currentItem);
			_currentItem = null;
			return this;
		}

		public ConstraintHelper RemoveAll()
		{
			foreach (var item in _items) {
				var constraints = item.GetAllConstraints();
				if (constraints.Count > 0) {
					_view.RemoveConstraints(constraints.ToArray());
					item.EmptyConstraints();
				}
				item.View.RemoveFromSuperview();
			}
			_items = new List<ConstraintContainer>();
			_currentItem = null;
			return this;
		}

		public ConstraintContainer GetContainer()
		{
			return _currentItem;
		}

		#endregion


		#region Positioning Top functionalities

		public ConstraintHelper Top(float? margin = null) 
		{
			if (_currentItem == null) { throw new UnattachedViewException(); }
			if (_lastTop != null) {
				BelowOf(_lastTop, margin);
			} else {
				if (margin != null) {
					_currentItem.MarginTop = (float)margin;
				}
				_currentItem.ConstraintTop = NSLayoutConstraint.Create(
					_currentItem.View, NSLayoutAttribute.Top, 
					NSLayoutRelation.Equal,
					_view, NSLayoutAttribute.Top, 
					1f, _currentItem.MarginTop
				);
				_view.AddConstraint(_currentItem.ConstraintTop);
			}
			return this;
		}

		public ConstraintHelper BelowOf(UIView view, float? margin = null)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return BelowOf(item, margin);
		}

		public ConstraintHelper BelowOf(ConstraintContainer container, float? margin = null)
		{
			if (margin != null) {
				_currentItem.MarginTop = (float)margin;
			}
			_currentItem.ConstraintTop = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Top, 
				NSLayoutRelation.Equal,
				container.View, NSLayoutAttribute.Bottom, 
				1f, _currentItem.MarginTop + container.MarginBottom
			);
			_view.AddConstraint(_currentItem.ConstraintTop);
			return this;
		}

		#endregion


		#region Positioning Bottom functionalities

		public ConstraintHelper Bottom(float? margin = null)
		{
			if (_lastBottom != null) {
				this.AboveOf(_lastBottom, margin);
			} else {
				if (margin != null) {
					_currentItem.MarginBottom = (float)margin;
				}
				_currentItem.ConstraintBottom = NSLayoutConstraint.Create(
					_currentItem.View, NSLayoutAttribute.Bottom, 
					NSLayoutRelation.Equal,
					_view, NSLayoutAttribute.Bottom, 
					1f, 0 - _currentItem.MarginBottom
				);
				_view.AddConstraint(_currentItem.ConstraintBottom);
			}
			return this;
		}

		public ConstraintHelper AboveOf(UIView view, float? margin = null)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return AboveOf(item, margin);	
		}

		public ConstraintHelper AboveOf(ConstraintContainer container, float? margin = null)
		{
			if (margin != null) {
				_currentItem.MarginBottom = (float)margin;
			}
			_currentItem.ConstraintBottom = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Bottom, 
				NSLayoutRelation.Equal,
				container.View, NSLayoutAttribute.Top, 
				1f, 0 - (_currentItem.MarginBottom + container.MarginTop)
			);
			_view.AddConstraint(_currentItem.ConstraintBottom);
			return this;
		}

		#endregion


		#region Positioning Right functionalities

		public ConstraintHelper Right(float? margin = null) 
		{
			if (_lastRight != null) {
				this.AboveOf(_lastRight, margin);
			} else {
				if (margin != null) {
					_currentItem.MarginRight = (float)margin;
				}
				_currentItem.ConstraintRight = NSLayoutConstraint.Create(
					_currentItem.View, NSLayoutAttribute.Right, 
					NSLayoutRelation.Equal, 
					_view, NSLayoutAttribute.Right, 
					1f, 0 - _currentItem.MarginRight
				);
				_view.AddConstraint(_currentItem.ConstraintRight);
			}
			return this;
		}

		public ConstraintHelper RightOf(UIView view, float? margin = null) 
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return RightOf(item, margin);	
		}

		public ConstraintHelper RightOf(ConstraintContainer container, float? margin = null)
		{
			if (margin != null) {
				_currentItem.MarginRight = (float)margin;
			}
			_currentItem.ConstraintRight = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Right, 
				NSLayoutRelation.Equal,
				container.View, NSLayoutAttribute.Left, 
				1f, 0 - (_currentItem.MarginRight + container.MarginLeft)
			);
			_view.AddConstraint(_currentItem.ConstraintRight);
			return this;
		}

		#endregion


		#region Positioning Left functionalities

		public ConstraintHelper Left(float? margin = null)
		{
			if (_lastLeft != null) {
				this.LeftOf(_lastLeft, margin);
			} else {
				if (margin != null) {
					_currentItem.MarginLeft = (float)margin;
				}
				_currentItem.ConstraintLeft = NSLayoutConstraint.Create(
					_currentItem.View, NSLayoutAttribute.Left, 
					NSLayoutRelation.Equal, 
					_view, NSLayoutAttribute.Left, 
					1f, _currentItem.MarginLeft
				);
				_view.AddConstraint(_currentItem.ConstraintLeft);
			}
			return this;
		}

		public ConstraintHelper LeftOf(UIView view, float? margin = null)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return LeftOf(item, margin);		
		}

		public ConstraintHelper LeftOf(ConstraintContainer container, float? margin = null)
		{
			if (margin != null) {
				_currentItem.MarginLeft = (float)margin;
			}
			_currentItem.ConstraintLeft = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Left, 
				NSLayoutRelation.Equal,
				container.View, NSLayoutAttribute.Right, 
				1f, _currentItem.MarginLeft + container.MarginRight
			);
			_view.AddConstraint(_currentItem.ConstraintLeft);
			return this;
		}

		#endregion


		#region Positioning Width functionalities

		public ConstraintHelper Width(float width) 
		{
			_currentItem.ConstraintWidth = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Width, 
				NSLayoutRelation.Equal, 
				null, NSLayoutAttribute.Width, 
				0f, width
			);
			_view.AddConstraint(_currentItem.ConstraintWidth);
			return this;
		}

		public ConstraintHelper WidthOf(UIView view, float multiplier = 1, float modifier = 0)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return WidthOf(item, multiplier);	
		}

		public ConstraintHelper WidthOf(ConstraintContainer container, float multiplier = 1, float modifier = 0)
		{
			_currentItem.ConstraintWidth = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Width, 
				NSLayoutRelation.Equal, 
				container.View, NSLayoutAttribute.Width, 
				multiplier, modifier
			);
			_view.AddConstraint(_currentItem.ConstraintWidth);
			return this;
		}

		public ConstraintHelper WidthOfParent(float multiplier = 1, float modifier = 0)
		{
			_currentItem.ConstraintWidth = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Width, 
				NSLayoutRelation.Equal, 
				_view, NSLayoutAttribute.Width, 
				multiplier, modifier
			);
			_view.AddConstraint(_currentItem.ConstraintWidth);
			return this;
		}

		public ConstraintHelper WidthFromHeight(float multiplier = 1, float modifier = 0)
		{
			_currentItem.ConstraintWidth = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Width, 
				NSLayoutRelation.Equal, 
				_currentItem.View, NSLayoutAttribute.Height, 
				multiplier, modifier
			);
			_view.AddConstraint(_currentItem.ConstraintWidth);
			return this;
		}

		#endregion


		#region Positioning Height functionalities

		public ConstraintHelper Height(float height) 
		{
			_currentItem.ConstraintHeight = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Height, 
				NSLayoutRelation.Equal, 
				null, NSLayoutAttribute.Height, 
				0f, height
			);
			_view.AddConstraint(_currentItem.ConstraintHeight);
			return this;
		}

		public ConstraintHelper HeightOf(UIView view, float multiplier = 1, float modifier = 0)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return HeightOf(item, multiplier);
		}

		public ConstraintHelper HeightOf(ConstraintContainer container, float multiplier = 1, float modifier = 0)
		{
			_currentItem.ConstraintHeight = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Height, 
				NSLayoutRelation.Equal, 
				container.View, NSLayoutAttribute.Height, 
				multiplier, modifier
			);
			_view.AddConstraint(_currentItem.ConstraintHeight);
			return this;
		}

		public ConstraintHelper HeightOfParent(float multiplier = 1, float modifier = 0)
		{
			_currentItem.ConstraintHeight = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Height, 
				NSLayoutRelation.Equal, 
				_view, NSLayoutAttribute.Height, 
				multiplier, modifier
			);
			_view.AddConstraint(_currentItem.ConstraintHeight);
			return this;
		}

		public ConstraintHelper HeightFromWidth(float multiplier = 1, float modifier = 0) 
		{
			_currentItem.ConstraintHeight = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Height, 
				NSLayoutRelation.Equal, 
				_currentItem.View, NSLayoutAttribute.Width, 
				multiplier, modifier
			);
			_view.AddConstraint(_currentItem.ConstraintHeight);
			return this;
		}

		#endregion


		#region Positioning XY functionalities

		public ConstraintHelper Center(float modifier = 0)
		{
			_currentItem.ConstraintCenter = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.CenterX, 
				NSLayoutRelation.Equal, 
				_view, NSLayoutAttribute.CenterX, 
				1.0f, modifier
			);
			_view.AddConstraint(_currentItem.ConstraintCenter);
			return this;
		}

		public ConstraintHelper Middle(float modifier = 0)
		{
			_currentItem.ConstraintMiddle = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.CenterY, 
				NSLayoutRelation.Equal, 
				_view, NSLayoutAttribute.CenterY, 
				1.0f, modifier
			);
			_view.AddConstraint(_currentItem.ConstraintMiddle);
			return this;
		}

		#endregion


		#region Constraint Removal functionalities


		public ConstraintHelper UpdateConstraints()
		{
			_view.LayoutIfNeeded();
      foreach(var item in _items) {
        item.View.LayoutIfNeeded();
      }
			return this;
		}

		public ConstraintHelper RemoveAllConstraints()
		{
			var constraints = _currentItem.GetAllConstraints();
			if(constraints.Count > 0) {
				_view.RemoveConstraints(constraints.ToArray());
				_currentItem.EmptyConstraints();
			}
			return this;
		}

		public ConstraintHelper RemoveTopConstraint()
		{
			if(_currentItem.ConstraintTop != null) {
				_view.RemoveConstraint(_currentItem.ConstraintTop);
				_currentItem.ConstraintTop = null;
			}
			return this;
		}

		public ConstraintHelper RemoveRightConstraint()
		{
			if(_currentItem.ConstraintRight != null) {
				_view.RemoveConstraint(_currentItem.ConstraintRight);
				_currentItem.ConstraintRight = null;
			}
			return this;
		}


		public ConstraintHelper RemoveBottomConstraint()
		{
			if(_currentItem.ConstraintBottom != null) {
				_view.RemoveConstraint(_currentItem.ConstraintBottom);
				_currentItem.ConstraintBottom = null;
			}
			return this;
		}


		public ConstraintHelper RemoveLeftConstraint()
		{
			if(_currentItem.ConstraintLeft != null) {
				_view.RemoveConstraint(_currentItem.ConstraintLeft);
				_currentItem.ConstraintLeft = null;
			}
			return this;
		}

		public ConstraintHelper RemoveCenterConstraint()
		{
			if(_currentItem.ConstraintCenter != null) {
				_view.RemoveConstraint(_currentItem.ConstraintCenter);
				_currentItem.ConstraintCenter = null;
			}
			return this;
		}

		public ConstraintHelper RemoveMiddleConstraint()
		{
			if(_currentItem.ConstraintMiddle != null) {
				_view.RemoveConstraint(_currentItem.ConstraintMiddle);
				_currentItem.ConstraintMiddle = null;
			}
			return this;
		}

		public ConstraintHelper RemoveHeightConstraint()
		{
			if(_currentItem.ConstraintHeight != null) {
				_view.RemoveConstraint(_currentItem.ConstraintHeight);
				_currentItem.ConstraintHeight = null;
			}
			return this;
		}

		public ConstraintHelper RemoveWidthConstraint()
		{
			if(_currentItem.ConstraintWidth != null) {
				_view.RemoveConstraint(_currentItem.ConstraintWidth);
				_currentItem.ConstraintWidth = null;
			}
			return this;
		}
		#endregion


		#region Margin functionalities

		public ConstraintHelper SetMargin(float top, float right, float bottom, float left)
		{
			if (_currentItem == null) { throw new UnattachedViewException(); }
			_currentItem.MarginTop = top;
			_currentItem.MarginRight = right;
			_currentItem.MarginBottom = bottom;
			_currentItem.MarginLeft = left;
			return this;
		}

		public ConstraintHelper SetMargin(float topAndBottom, float rightAndLeft)
		{
			if (_currentItem == null) { throw new UnattachedViewException(); }
			_currentItem.MarginTop = _currentItem.MarginBottom = topAndBottom;
			_currentItem.MarginRight = _currentItem.MarginLeft = rightAndLeft;
			return this;
		}

		public ConstraintHelper SetMargin(float allSides)
		{
			if (_currentItem == null) { throw new UnattachedViewException(); }
			_currentItem.MarginTop = _currentItem.MarginRight = 
			_currentItem.MarginBottom = _currentItem.MarginLeft = allSides;
			return this;
		}

		#endregion


		#region Stacking functionalities

		public ConstraintHelper StackTop()
		{
			_lastTop = _currentItem;
			return this;
		}

		public ConstraintHelper StackRight()
		{
			_lastRight = _currentItem;
			return this;
		}

		public ConstraintHelper StackBottom()
		{
			_lastBottom = _currentItem;
			return this;
		}

		public ConstraintHelper StackLeft()
		{
			_lastLeft = _currentItem;
			return this;
		}

		public ConstraintHelper ResetTopStack()
		{
			_lastTop = null;
			return this;
		}

		public ConstraintHelper ResetRightStack()
		{
			_lastRight = null;
			return this;
		}

		public ConstraintHelper ResetBottomStack()
		{
			_lastBottom = null;
			return this;
		}

		public ConstraintHelper ResetLeftStack()
		{
			_lastLeft = null;
			return this;
		}

		#endregion
	}
}
