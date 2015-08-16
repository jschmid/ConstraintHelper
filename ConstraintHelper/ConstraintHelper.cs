using System;
using System.Linq;
using System.Collections.Generic;

using UIKit;

namespace SR.MonoTouchHelpers
{
	public class ConstraintHelper
	{
		#region Fields

    // the view this instance uses for appending elements
		readonly UIView _view;

		ConstraintContainer _lastTop;
		ConstraintContainer _lastRight;
		ConstraintContainer _lastBottom;
		ConstraintContainer _lastLeft;

		List<ConstraintContainer> _items;

		ConstraintContainer _currentItem;

		#endregion


		#region Lifecycle

    /// <summary>
    /// Initializes a new instance of the <see cref="SR.MonoTouchHelpers.ConstraintHelper"/> class.
    /// </summary>
    /// <param name="parentView">Parent view.</param>
		public ConstraintHelper(UIView parentView)
		{
			_view = parentView;
			_items = new List<ConstraintContainer>();
		}

		#endregion


		#region Attaching functionalities

    /// <summary>
    /// Adds the given view as a subview and the given view as current view to work with
    /// </summary>
    /// <returns>ConstraintHelper</returns>
    /// <param name="view">View.</param>
		public ConstraintHelper Attach(UIView view)
		{
			_currentItem = _items.FirstOrDefault(items => (items.View == view));
			if (_currentItem == null) { 
				view.TranslatesAutoresizingMaskIntoConstraints = false;
				_currentItem = new ConstraintContainer(view);
        if(view is IConstraintHelpedView) {
          _currentItem.Margin = ((IConstraintHelpedView)view).Margin;
        }
				_view.AddSubview(view);
			}
			_items.Add(_currentItem);
			return this; 
		}

    /// <summary>
    /// Sets the current view to work with to the given one.
    /// Note: The view has to have been attached previously using the 'Attach' method.
    /// </summary>
    /// <returns>ConstraintHelper</returns>
    /// <param name="view">View.</param>
		public ConstraintHelper WorkWith(UIView view)
		{
			_currentItem = _items.FirstOrDefault(items => (items.View == view));
			if (_currentItem == null) { throw new UnattachedViewException(); }
			return this;
		}

    /// <summary>
    /// Removes the currently selected view and all related constraints from the parent view.
    /// </summary>
    /// <returns>ConstraintHelper</returns>
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

    /// <summary>
    /// Removes all views and related constraints added through the ConstraintHelper object.
    /// </summary>
    /// <returns>ConstraintHelper</returns>
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

    /// <summary>
    /// Returns the container object that is used for the current view.
    /// </summary>
    /// <returns>ConstraintHelper</returns>
		public ConstraintContainer GetContainer()
		{
			return _currentItem;
		}

		#endregion


		#region Positioning Top functionalities

    /// <summary>
    /// Sets the top constraint of the current view to the parents top.
    /// If there is a view that is stacked to the top, 
    /// this method will behave exactly like BelowOf (the stacked view)
    /// </summary>
    /// <param name="margin">Margin.</param>
    /// <returns>ConstraintHelper</returns>
		public ConstraintHelper Top(float? margin = null) 
		{
			if (_currentItem == null) { throw new UnattachedViewException(); }
			if (_lastTop != null) {
				BelowOf(_lastTop, margin);
			} else {
				if (margin != null) {
          _currentItem.Margin.Top = (float)margin;
				}
				_currentItem.ConstraintTop = NSLayoutConstraint.Create(
					_currentItem.View, NSLayoutAttribute.Top, 
					NSLayoutRelation.Equal,
					_view, NSLayoutAttribute.Top, 
					1f, _currentItem.Margin.Top
				);
				_view.AddConstraint(_currentItem.ConstraintTop);
			}
			return this;
		}

    /// <summary>
    /// Sets the top constraint of the view to the bottom of the given view.
    /// Note: The view given has to have been attached using the Attach method.
    /// Any margin of the given view and the current view will be added (no margin collapse).
    /// </summary>
    /// <returns>ConstraintHelper</returns>
    /// <param name="view">View.</param>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper BelowOf(UIView view, float? margin = null)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return BelowOf(item, margin);
		}


    /// <summary>
    /// Sets the top constraint of the view to the bottom of the view within the given constraint container.
    /// Any margin of the given view and the current view will be added (no margin collapse).
    /// </summary>
    /// <returns>ConstraintHelper</returns>
    /// <param name="container">Container.</param>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper BelowOf(ConstraintContainer container, float? margin = null)
		{
			if (margin != null) {
				_currentItem.Margin.Top = (float)margin;
			}
			_currentItem.ConstraintTop = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Top, 
				NSLayoutRelation.Equal,
				container.View, NSLayoutAttribute.Bottom, 
        1f, _currentItem.Margin.Top + container.Margin.Bottom
			);
			_view.AddConstraint(_currentItem.ConstraintTop);
			return this;
		}

		#endregion


		#region Positioning Bottom functionalities


    /// <summary>
    /// Sets the bottom constraint of the current view to the parents bottom.
    /// If there is a view that is stacked to the bottom, 
    /// this method will behave exactly like AboveOf (the stacked view)
    /// </summary>
    /// <returns>ConstraintHelper</returns>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper Bottom(float? margin = null)
		{
			if (_lastBottom != null) {
				this.AboveOf(_lastBottom, margin);
			} else {
				if (margin != null) {
          _currentItem.Margin.Bottom = (float)margin;
				}
				_currentItem.ConstraintBottom = NSLayoutConstraint.Create(
					_currentItem.View, NSLayoutAttribute.Bottom, 
					NSLayoutRelation.Equal,
					_view, NSLayoutAttribute.Bottom, 
          1f, 0 - _currentItem.Margin.Bottom
				);
				_view.AddConstraint(_currentItem.ConstraintBottom);
			}
			return this;
		}

    /// <summary>
    /// Sets the bottom constraint of the current view to the top of the given view.
    /// Note: The view given has to have been attached using the Attach method.
    /// Any margin of the given view and the current view will be added (no margin collapse).
    /// </summary>
    /// <returns>ConstraintHelper</returns>
    /// <param name="view">View.</param>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper AboveOf(UIView view, float? margin = null)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return AboveOf(item, margin);	
		}

    /// <summary>
    /// Sets the bottom constraint of the current view to the bottom of the view within the given constraint container.
    /// Any margin of the given view and the current view will be added (no margin collapse).
    /// </summary>
    /// <returns>ConstraintHelper</returns>
    /// <param name="container">Container.</param>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper AboveOf(ConstraintContainer container, float? margin = null)
		{
			if (margin != null) {
        _currentItem.Margin.Bottom = (float)margin;
			}
			_currentItem.ConstraintBottom = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Bottom, 
				NSLayoutRelation.Equal,
				container.View, NSLayoutAttribute.Top, 
        1f, 0 - (_currentItem.Margin.Bottom + container.Margin.Top)
			);
			_view.AddConstraint(_currentItem.ConstraintBottom);
			return this;
		}

		#endregion


		#region Positioning Right functionalities

    /// <summary>
    /// Sets the right constraint of the current view to the parents right.
    /// If there is a view that is stacked to the right, 
    /// this method will behave exactly like RightOf (the stacked view)
    /// </summary>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper Right(float? margin = null) 
		{
			if (_lastRight != null) {
				this.AboveOf(_lastRight, margin);
			} else {
				if (margin != null) {
          _currentItem.Margin.Right = (float)margin;
				}
				_currentItem.ConstraintRight = NSLayoutConstraint.Create(
					_currentItem.View, NSLayoutAttribute.Right, 
					NSLayoutRelation.Equal, 
					_view, NSLayoutAttribute.Right, 
          1f, 0 - _currentItem.Margin.Right
				);
				_view.AddConstraint(_currentItem.ConstraintRight);
			}
			return this;
		}

    /// <summary>
    /// Sets the right constraint of the current view to the left of the given view.
    /// Note: The view given has to have been attached using the Attach method.
    /// Any margin of the given view and the current view will be added (no margin collapse).
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="view">View.</param>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper RightOf(UIView view, float? margin = null) 
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return RightOf(item, margin);	
		}


    /// <summary>
    /// Sets the right constraint of the current view to the left of the view within the given constraint container.
    /// Any margin of the given view and the current view will be added (no margin collapse).
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="container">Container.</param>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper RightOf(ConstraintContainer container, float? margin = null)
		{
			if (margin != null) {
        _currentItem.Margin.Right = (float)margin;
			}
			_currentItem.ConstraintRight = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Right, 
				NSLayoutRelation.Equal,
				container.View, NSLayoutAttribute.Left, 
        1f, 0 - (_currentItem.Margin.Right + container.Margin.Left)
			);
			_view.AddConstraint(_currentItem.ConstraintRight);
			return this;
		}

		#endregion


		#region Positioning Left functionalities

    /// <summary>
    /// Sets the left constraint of the current view to the parents left.
    /// If there is a view that is stacked to the left, 
    /// this method will behave exactly like LeftOf (the stacked view)
    /// </summary>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper Left(float? margin = null)
		{
			if (_lastLeft != null) {
				this.LeftOf(_lastLeft, margin);
			} else {
				if (margin != null) {
          _currentItem.Margin.Left = (float)margin;
				}
				_currentItem.ConstraintLeft = NSLayoutConstraint.Create(
					_currentItem.View, NSLayoutAttribute.Left, 
					NSLayoutRelation.Equal, 
					_view, NSLayoutAttribute.Left, 
          1f, _currentItem.Margin.Left
				);
				_view.AddConstraint(_currentItem.ConstraintLeft);
			}
			return this;
		}

    /// <summary>
    /// Sets the left constraint of the current view to the right of the given view.
    /// Note: The view given has to have been attached using the Attach method.
    /// Any margin of the given view and the current view will be added (no margin collapse).
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="view">View.</param>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper LeftOf(UIView view, float? margin = null)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return LeftOf(item, margin);		
		}

    /// <summary>
    /// Sets the left constraint of the current view to the right of the view within the given constraint container.
    /// Any margin of the given view and the current view will be added (no margin collapse).
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="container">Container.</param>
    /// <param name="margin">Margin.</param>
		public ConstraintHelper LeftOf(ConstraintContainer container, float? margin = null)
		{
			if (margin != null) {
        _currentItem.Margin.Left = (float)margin;
			}
			_currentItem.ConstraintLeft = NSLayoutConstraint.Create(
				_currentItem.View, NSLayoutAttribute.Left, 
				NSLayoutRelation.Equal,
				container.View, NSLayoutAttribute.Right, 
        1f, _currentItem.Margin.Left + container.Margin.Right
			);
			_view.AddConstraint(_currentItem.ConstraintLeft);
			return this;
		}

		#endregion


		#region Positioning Width functionalities

    /// <summary>
    /// Sets a width constraint for the current view to be exact given value
    /// </summary>
    /// <param name="width">Width.</param>
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

    /// <summary>
    /// Sets the width constraint of the current view to the width of the given view.
    /// Note: The view given has to have been attached using the Attach method.
    /// You can optionally multiply the views width and/or modifie it.
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="view">View.</param>
    /// <param name="multiplier">Multiplier.</param>
    /// <param name="modifier">Modifier.</param>
		public ConstraintHelper WidthOf(UIView view, float multiplier = 1, float modifier = 0)
		{
			ConstraintContainer item = _items.FirstOrDefault(items => (items.View == view));
			if (item == null) { throw new UnattachedViewException(); }
			return WidthOf(item, multiplier);	
		}


    /// <summary>
    /// Sets the width constraint of the current view to the width of the given view.
    /// Note: The view given has to have been attached using the Attach method.
    /// You can optionally multiply the views width and/or modifie it.
    /// </summary>
    /// <returns>The of.</returns>
    /// <param name="container">Container.</param>
    /// <param name="multiplier">Multiplier.</param>
    /// <param name="modifier">Modifier.</param>
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
      _currentItem.Margin = new ConstraintMargin(top, right, bottom, left);
			return this;
		}

		public ConstraintHelper SetMargin(float topAndBottom, float rightAndLeft)
		{
			if (_currentItem == null) { throw new UnattachedViewException(); }
      _currentItem.Margin = new ConstraintMargin(topAndBottom, rightAndLeft);
			return this;
		}

		public ConstraintHelper SetMargin(float allSides)
		{
			if (_currentItem == null) { throw new UnattachedViewException(); }
      _currentItem.Margin = new ConstraintMargin(allSides);
			return this;
		}

    public ConstraintHelper SetMargin(ConstraintMargin marginObject)
    {
      if (_currentItem == null) { throw new UnattachedViewException(); }
      _currentItem.Margin = marginObject;
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
